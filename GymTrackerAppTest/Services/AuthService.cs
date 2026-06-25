using System.Collections.Generic;
using System.Linq;
using PracticaGymTracker.Data;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class AuthService
{
    public AuthService()
    {
    }

    public (bool Success, string Message) RegisterUser(string login, string password, string role = "User")
    {
        if (string.IsNullOrWhiteSpace(login)) return (false, "Логін не може бути порожнім.");
        if (string.IsNullOrWhiteSpace(password)) return (false, "Пароль не може бути пустий");
        if (password.Length < 4) return (false, "Пароль має містити мінімум 4 символи.");

        using var db = new AppDbContext();
        if (db.Users.Any(u => u.Login.ToLower() == login.ToLower()))
            return (false, "Користувач з таким логіном вже існує.");

        var newUser = new UserModel
        {
            Login = login,
            PasswordHash = PasswordHasher.HashPassword(password),
            Role = role
        };

        db.Users.Add(newUser);
        db.SaveChanges();
        return (true, "Реєстрація успішна!");
    }

    public (bool Success, string Message, UserModel? User) LoginUser(string login, string password)
    {
        using var db = new AppDbContext();
        var user = db.Users.FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
        if (user == null) return (false, "Користувача не знайдено.", null);

        if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
            return (false, "Невірний пароль.", null);

        return (true, "Вхід успішний!", user);
    }

    public List<UserModel> GetAthletes()
    {
        using var db = new AppDbContext();
        return db.Users.Where(u => u.Role == "User").ToList();
    }
    
    public void UpdateUserGoal(string login, string newGoalWeight)
    {
        using var db = new AppDbContext();
        var user = db.Users.FirstOrDefault(u => u.Login == login);
        if (user != null)
        {
            user.GoalWeight = newGoalWeight;
            db.SaveChanges();
        }
    }
}