using System.Collections.Generic;
using System.Linq;
using PracticaGymTracker.Data;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.Services;

public class WorkoutService
{
    public WorkoutService()
    {
    }

    public List<WorkoutModel> GetWorkoutsForCurrentUser()
    {
        var currentUser = SessionManager.CurrentUser;
        string login = currentUser?.Login ?? string.Empty;

        using var db = new AppDbContext();
        return db.Workouts.Where(w => w.Login == login).ToList();
    }
    
    public void AddWorkout(WorkoutModel workout)
    {
        var currentUser = SessionManager.CurrentUser;
        workout.Login = currentUser?.Login ?? string.Empty; 
        
        using var db = new AppDbContext();
        db.Workouts.Add(workout);
        db.SaveChanges();
    }
    
    public void DeleteWorkout(WorkoutModel workoutToDelete)
    {
        using var db = new AppDbContext();
        var workoutToRemove = db.Workouts.FirstOrDefault(w => w.Id == workoutToDelete.Id);
    
        if (workoutToRemove != null)
        {
            db.Workouts.Remove(workoutToRemove);
            db.SaveChanges(); 
        }
    }
}