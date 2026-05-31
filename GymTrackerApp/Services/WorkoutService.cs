using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class WorkoutService
{
    private readonly string _filePath = "trainings.json";
    private List<WorkoutModel> _workouts;

    public WorkoutService()
    {
        _workouts = LoadWorkouts();
    }

    private List<WorkoutModel> LoadWorkouts()
    {
        if (!File.Exists(_filePath)) return new List<WorkoutModel>();
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<WorkoutModel>>(json) ?? new List<WorkoutModel>();
    }

    private void SaveWorkouts()
    {
        string json = JsonSerializer.Serialize(_workouts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
    
    public List<WorkoutModel> GetWorkoutsForCurrentUser()
    {
        var currentUser = SessionManager.CurrentUser;
        if (currentUser == null) return new List<WorkoutModel>();

        return _workouts.Where(w => w.Login == currentUser.Login).ToList();
    }
    
    public void AddWorkout(WorkoutModel workout)
    {
        var currentUser = SessionManager.CurrentUser;
        if (currentUser == null) return;

        workout.Login = currentUser.Login; 
        
        workout.Id = _workouts.Count > 0 ? _workouts.Max(w => w.Id) + 1 : 1;

        _workouts.Add(workout);
        SaveWorkouts();
    }
    public void DeleteWorkout(WorkoutModel workoutToDelete)
    {
        var workoutToRemove = _workouts.FirstOrDefault(w => w.Id == workoutToDelete.Id);
    
        if (workoutToRemove != null)
        {
            _workouts.Remove(workoutToRemove);
            
            SaveWorkouts(); 
        }
    }
}