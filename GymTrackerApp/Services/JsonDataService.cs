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
    
    public void SaveWorkouts(IEnumerable<WorkoutItem> workouts)
    {
        string json = JsonSerializer.Serialize(workouts);
        File.WriteAllText(_filePath, json);
    }
    
    public List<WorkoutItem> LoadWorkouts()
    {
        if (!File.Exists(_filePath))
        {
            return new List<WorkoutItem>();
        }
        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<WorkoutItem>>(json) ?? new List<WorkoutItem>();
        }
        catch (Exception)
        {
            return new List<WorkoutItem>();
        }
    }
    
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
    public UserProfile LoadProfile()
    {
        if (!File.Exists(_profileFilePath))
            return new UserProfile();

        string json = File.ReadAllText(_profileFilePath);
        return JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();
    }
    public void SaveProfile(UserProfile profile)
    {
        string json = JsonSerializer.Serialize(profile);
        File.WriteAllText(_profileFilePath, json);
    }
}