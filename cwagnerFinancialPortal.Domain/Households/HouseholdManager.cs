using cwagnerFinancialPortal.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwagnerFinancialPortal.Domain.Households
{
    public class HouseholdManager
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        public void SendInvite(int householdId, string email)
        {
            var invite = new HouseholdInvite
            {
                HouseholdId = householdId,
                InviteeEmail = email
            };
            db.HouseholdInvites.Add(invite);
            db.SaveChanges();
            //SEND EMAIL TO INVITEE
        }

        public void AcceptInvite(Guid inviteId, string email)
        {
            var invite = db.HouseholdInvites.SingleOrDefault(i => i.Id == inviteId && i.InviteeEmail == email);
            var user = db.Users.SingleOrDefault(u => u.Email == email);
            if(invite != null && user != null)
            {
                user.HouseholdId = invite.HouseholdId; //puts user in household
                db.SaveChanges();
            }
            //error handling
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
