namespace DeviceManagement.API.Models.Requests;

public class ProcurementRequest
{
    public decimal TotalBudget { get; set; }
    public int EmployeeCount { get; set; }
    public string? Currency { get; set; } = "RON";
}
