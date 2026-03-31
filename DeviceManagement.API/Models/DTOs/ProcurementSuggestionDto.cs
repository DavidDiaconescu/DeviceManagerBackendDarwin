namespace DeviceManagement.API.Models.DTOs;

public class ProcurementSuggestionDto
{
    public decimal TotalBudget { get; set; }
    public int EmployeeCount { get; set; }
    public decimal BudgetPerEmployee { get; set; }
    public string Currency { get; set; } = "RON";
    public string Suggestion { get; set; } = string.Empty;
}
