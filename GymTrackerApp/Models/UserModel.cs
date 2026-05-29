namespace PracticaGymTracker.Models;

public class UserModel
{
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; 
    public string Role { get; set; } = "User";
    public string GoalWeight { get; set; } = "80";
}