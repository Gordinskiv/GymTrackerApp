namespace PracticaGymTracker.Models;

public class WorkoutModel
{
    public int Id { get; set; } 
    public string Login { get; set; } = string.Empty; 
    public string Date { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string Intensity { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}