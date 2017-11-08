using cwagnerFinancialPortal.Domain.Transactions;
using System;
using System.Data.Entity;
using System.Linq;

namespace cwagnerFinancialPortal.Domain.BankAccounts
{
    public class BankAccountManager
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public BankAccount Get(int id)
        {
            return db.BankAccounts.Find(id);
        }

        public IQueryable<BankAccount> GetAll(int householdId)
        {
             return db.BankAccounts.Where(a => a.HouseholdId == householdId);
        }

        public int Add(BankAccount bankAccount)
        {
            var initialTransaction = new Transaction
            {
                Amount = bankAccount.Balance,
                Type = bankAccount.Balance >= 0 ? TransactionType.Credit : TransactionType.Debit,
                Date = DateTimeOffset.Now,
                IsReconciled = true,
                Description = "Initial Account Balance"
            };
            bankAccount.Transactions.Add(initialTransaction);
            db.BankAccounts.Add(bankAccount);
            db.SaveChanges();

            return bankAccount.Id;
        }

        public void Edit(BankAccount bankAccount)
        {
            if(!db.BankAccounts.Local.Any(b => b.Id == bankAccount.Id))
            {
                db.BankAccounts.Attach(bankAccount);
            }
            db.Entry(bankAccount).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var account = db.BankAccounts.Find(id);
            if(account != null)
            {
                foreach(var transaction in account.Transactions.ToList())
                {
                    db.Transactions.Remove(transaction);
                }
                db.BankAccounts.Remove(account);
                db.SaveChanges();
            }
        }

        //TRANSACTIONS
        public Transaction GetTransaction(int transactionId)
        {
            return db.Transactions.Find(transactionId);
        }

        public IQueryable<Transaction> GetAllTransaction(int bankAccountId)
        {
            return db.Transactions.Where(t => t.BankAccountId == bankAccountId);
        }

        public int AddTransaction(Transaction transaction)
        {
            var account = db.BankAccounts.Find(transaction.BankAccountId);
            if(transaction.Type == TransactionType.Debit)
            {
                transaction.Amount = transaction.Amount * -1;
            }
            account.Balance += transaction.Amount;
            db.Transactions.Add(transaction);
            db.SaveChanges();

            return transaction.Id;
        }

        public void EditTransaction(Transaction transaction)
        {
            var account = db.BankAccounts.Find(transaction.BankAccountId);
            var oldAmount = db.Transactions.Where(x => x.Id == transaction.Id).Select(s => s.Amount).Single();

            if (!db.Transactions.Local.Any(t => t.Id == transaction.Id))
            {
                db.Transactions.Attach(transaction);
            }
            account.Balance -= oldAmount;
            account.Balance += transaction.Amount;
            db.Entry(transaction).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteTransaction(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);
            if (transaction != null)
            {
                transaction.BankAccount.Balance -= transaction.Amount;
                db.Transactions.Remove(transaction);
                db.SaveChanges();
            }
        }
    }
}
