using cwagnerFinancialPortal.Domain.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwagnerFinancialPortal.Domain.Households
{
    public class HouseholdManager : IHouseholdManager
    {
        private readonly ApplicationDbContext db;

        public HouseholdManager(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public Household Get(int id)
        {
            return db.Households.Find(id);
        }

        public int Add(Household household)
        {
            db.Households.Add(household);
            db.SaveChanges();

            return household.Id;
        }

        public void Edit(Household household)
        {
            if(!db.Households.Local.Any(h => h.Id == household.Id))
            {
                db.Households.Attach(household);
            }
            db.Entry(household).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Guid AddInvite(int householdId, string email)
        {
            var invite = new HouseholdInvite
            {
                HouseholdId = householdId,
                InviteeEmail = email
            };
            db.HouseholdInvites.Add(invite);
            db.SaveChanges();
            return invite.Id;
        }

        public int AcceptInvite(Guid inviteId, string email)
        {
            var invite = db.HouseholdInvites.SingleOrDefault(i => i.Id == inviteId && i.InviteeEmail == email);
            var user = db.Users.SingleOrDefault(u => u.Email == email);
            if(invite != null && user != null)
            {
                user.HouseholdId = invite.HouseholdId; //puts user in household
                db.SaveChanges();
            }
            //error handling
            return invite.HouseholdId;
        }

        public void AddUserToHousehold(string userId, int householdId)
        {
            var user = db.Users.Find(userId);
            if (user != null)
            {
                user.HouseholdId = householdId;
                db.SaveChanges();
            }

        }

        public void RemoveUserFromHousehold(string userId)
        {
            var user = db.Users.Find(userId);
            if(user != null)
            {
                user.HouseholdId = null;
                db.SaveChanges();
            }
        }
    }
}
