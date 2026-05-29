using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class SessionManager
{
    public static UserModel? CurrentUser { get; set; }
    
    public static void Logout()
    {
        CurrentUser = null;
    }
}