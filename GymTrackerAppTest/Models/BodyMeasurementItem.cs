using System;
using System.ComponentModel.DataAnnotations;

namespace PracticaGymTracker.Models;

public class BodyMeasurementItem
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Weight { get; set; }
    public double Chest { get; set; }
    public double Biceps { get; set; }
    public double Waist { get; set; }
    public string UserLogin { get; set; }
    public string DisplayDate => Date.ToString("dd.MM.yy");
}