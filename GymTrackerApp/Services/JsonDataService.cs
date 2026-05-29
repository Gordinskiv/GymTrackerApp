using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class JsonDataService
{
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "workouts_data.json");
    private readonly string _measurementsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "measurements_data.json");
    private readonly string _profileFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile_data.json");
    
    public List<BodyMeasurementItem> LoadMeasurements()
    {
        if (!File.Exists(_measurementsFilePath))
            return new List<BodyMeasurementItem>();

        string json = File.ReadAllText(_measurementsFilePath);
        return JsonSerializer.Deserialize<List<BodyMeasurementItem>>(json) ?? new List<BodyMeasurementItem>();
    }
    
    public void SaveMeasurements(List<BodyMeasurementItem> measurements)
    {
        string json = JsonSerializer.Serialize(measurements);
        File.WriteAllText(_measurementsFilePath, json);
    }
}