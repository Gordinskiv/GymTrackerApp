using Microsoft.EntityFrameworkCore;
using PracticaGymTracker.Models;
using System.IO;
using System;

namespace PracticaGymTracker.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<WorkoutModel> Workouts { get; set; }
    public DbSet<BodyMeasurementItem> Measurements { get; set; }
    public DbSet<UserAchievementModel> UserAchievements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Зберігаємо файл бази даних прямо в робочій папці (щоб його було видно в Rider)
        optionsBuilder.UseSqlite("Data Source=gymtracker.db");
    }
}
