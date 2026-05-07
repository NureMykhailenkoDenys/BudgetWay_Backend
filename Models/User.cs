namespace BudgetWay.Backend.Models;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public ICollection<Car> Cars { get; set; } = new List<Car>();
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}