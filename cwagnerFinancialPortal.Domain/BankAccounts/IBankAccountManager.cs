using System.Linq;
using cwagnerFinancialPortal.Domain.Transactions;

namespace cwagnerFinancialPortal.Domain.BankAccounts
{
    public interface IBankAccountManager
    {
        int Add(BankAccount bankAccount);
        int AddTransaction(Transaction transaction);
        void Delete(int id);
        void DeleteTransaction(int transactionId);
        void Edit(BankAccount bankAccount);
        void EditTransaction(Transaction transaction);
        BankAccount Get(int id);
        IQueryable<BankAccount> GetAll(int householdId);
        IQueryable<Transaction> GetAllTransaction(int bankAccountId);
        Transaction GetTransaction(int transactionId);
    }
}