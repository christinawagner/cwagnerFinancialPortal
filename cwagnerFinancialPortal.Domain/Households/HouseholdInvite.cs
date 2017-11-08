using System;
using System.ComponentModel.DataAnnotations;

namespace cwagnerFinancialPortal.Domain.Households
{
    public class HouseholdInvite
    {
        [Key]
        public Guid Id { get; set; }

        public string InviteeEmail { get; set; }

        public int HouseholdId { get; set; }

        public virtual Household Household { get; set; }

        public HouseholdInvite()
        {
            Id = Guid.NewGuid();
        }
    }
}
