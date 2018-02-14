using System;

namespace cwagnerFinancialPortal.Domain.Households
{
    public interface IHouseholdManager
    {
        int AcceptInvite(Guid inviteId, string email);
        int Add(Household household);
        Guid AddInvite(int householdId, string email);
        void AddUserToHousehold(string userId, int householdId);
        void Edit(Household household);
        Household Get(int id);
        void RemoveUserFromHousehold(string userId);
    }
}