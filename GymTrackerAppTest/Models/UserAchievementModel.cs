using System;
using System.ComponentModel.DataAnnotations;

namespace PracticaGymTracker.Models;

public class UserAchievementModel
{
    [Key]
    public int Id { get; set; }
    
    public string UserLogin { get; set; } = string.Empty;
    
    // Зберігаємо назву досягнення (наприклад, "ПЕРША СОТНЯ"), щоб знати, що саме розблоковано
    public string AchievementTitle { get; set; } = string.Empty;
    
    // Коли це було розблоковано
    public string UnlockedDate { get; set; } = string.Empty;
}
