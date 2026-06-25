using System.Collections.Generic;
using System.Linq;
using PracticaGymTracker.Data;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class MeasurementService
{
    public MeasurementService()
    {
    }

    public List<BodyMeasurementItem> LoadMeasurements()
    {
        using var db = new AppDbContext();
        return db.Measurements.ToList();
    }
    
    public void AddMeasurement(BodyMeasurementItem measurement)
    {
        using var db = new AppDbContext();
        db.Measurements.Add(measurement);
        db.SaveChanges();
    }
    
    public void DeleteMeasurement(BodyMeasurementItem measurement)
    {
        using var db = new AppDbContext();
        var itemToRemove = db.Measurements.FirstOrDefault(m => m.Id == measurement.Id);
        if (itemToRemove != null)
        {
            db.Measurements.Remove(itemToRemove);
            db.SaveChanges();
        }
    }
}
