namespace PracticaGymTracker.Models;

public class AchievementItem
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string IconPathData { get; set; } = string.Empty;
    public bool IsUnlocked { get; set; } = false;
    public string IconColor => IsUnlocked ? "#FFD700" : "#555555";
    public double CardOpacity => IsUnlocked ? 1.0 : 0.5;
}