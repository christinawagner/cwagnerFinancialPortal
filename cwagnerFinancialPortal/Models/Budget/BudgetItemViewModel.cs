namespace cwagnerFinancialPortal.Models.Budget
{
    public class BudgetItemViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}