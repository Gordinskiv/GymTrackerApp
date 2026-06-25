using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticaGymTracker.Data;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class AchievementService
{
    /// <summary>
    /// Отримує список назв розблокованих досягнень для конкретного користувача.
    /// </summary>
    public List<string> GetUnlockedAchievements(string login)
    {
        if (string.IsNullOrWhiteSpace(login)) return new List<string>();

        using (var db = new AppDbContext())
        {
            db.Database.ExecuteSqlRaw(@"
                CREATE TABLE IF NOT EXISTS ""UserAchievements"" (
                    ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_UserAchievements"" PRIMARY KEY AUTOINCREMENT,
                    ""UserLogin"" TEXT NOT NULL,
                    ""AchievementTitle"" TEXT NOT NULL,
                    ""UnlockedDate"" TEXT NOT NULL
                );
            ");

            return db.UserAchievements
                .Where(a => a.UserLogin == login)
                .Select(a => a.AchievementTitle)
                .ToList();
        }
    }

    /// <summary>
    /// Перевіряє нотатки тренування на наявність ключових слів і розблоковує досягнення, якщо воно ще не розблоковане.
    /// </summary>
    public void CheckAndUnlockAchievements(string login, string workoutNotes)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(workoutNotes)) return;

        var notesLower = workoutNotes.ToLower();
        var newlyUnlocked = new List<string>();

        // Перевіряємо умови
        if (notesLower.Contains("100") && notesLower.Contains("жим"))
        {
            newlyUnlocked.Add("ПЕРША СОТНЯ");
        }
        if (notesLower.Contains("150") && notesLower.Contains("тяга"))
        {
            newlyUnlocked.Add("МАЙСТЕР ТЯГИ");
        }
        if (notesLower.Contains("болгарськ") || notesLower.Contains("спліт"))
        {
            newlyUnlocked.Add("СТАЛЕВІ НОГИ");
        }
        if (notesLower.Contains("ранок") || notesLower.Contains("встав рано") || notesLower.Contains("рання"))
        {
            newlyUnlocked.Add("РАННЯ ПТАШКА");
        }

        if (newlyUnlocked.Any())
        {
            using (var db = new AppDbContext())
            {
                db.Database.ExecuteSqlRaw(@"
                    CREATE TABLE IF NOT EXISTS ""UserAchievements"" (
                        ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_UserAchievements"" PRIMARY KEY AUTOINCREMENT,
                        ""UserLogin"" TEXT NOT NULL,
                        ""AchievementTitle"" TEXT NOT NULL,
                        ""UnlockedDate"" TEXT NOT NULL
                    );
                ");
                
                // Отримуємо ті досягнення, які користувач вже має
                var existingAchievements = db.UserAchievements
                    .Where(a => a.UserLogin == login)
                    .Select(a => a.AchievementTitle)
                    .ToList();

                foreach (var achievementTitle in newlyUnlocked)
                {
                    // Якщо користувач ще не має цього досягнення, записуємо його в БД
                    if (!existingAchievements.Contains(achievementTitle))
                    {
                        var newAchievement = new UserAchievementModel
                        {
                            UserLogin = login,
                            AchievementTitle = achievementTitle,
                            UnlockedDate = DateTime.Now.ToString("dd.MM.yyyy")
                        };
                        db.UserAchievements.Add(newAchievement);
                    }
                }

                // Фізично зберігаємо зміни в SQLite
                db.SaveChanges();
            }
        }
    }
}
