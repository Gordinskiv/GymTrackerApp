using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class AuthService
{
    private readonly string _filePath = "users_db.json";
        private List<UserModel> _users;

        public AuthService()
        {
            _users = LoadUsers();
        }

        private List<UserModel> LoadUsers()
        {
            if (!File.Exists(_filePath)) return new List<UserModel>();
            
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }

        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        
        public (bool Success, string Message) RegisterUser(string login, string password, string role = "User")
        {
            if (string.IsNullOrWhiteSpace(login)) return (false, "Логін не може бути порожнім.");
            if (password.Length < 4) return (false, "Пароль має містити мінімум 4 символи.");
            if (_users.Any(u => u.Login.ToLower() == login.ToLower())) return (false, "Користувач з таким логіном вже існує.");

            var newUser = new UserModel
            {
                Login = login,
                PasswordHash = PasswordHasher.HashPassword(password),
                Role = role
            };

            _users.Add(newUser);
            SaveUsers();
            return (true, "Реєстрація успішна!");
        }
        
        public (bool Success, string Message, UserModel? User) LoginUser(string login, string password)
        {
            var user = _users.FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if (user == null) return (false, "Користувача не знайдено.", null);

            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
                return (false, "Невірний пароль.", null);

            return (true, "Вхід успішний!", user);
        }
}