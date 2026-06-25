using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// Відображення сторінку Тренування.
/// </summary>
public partial class WorkoutsViewModel : ViewModelBase
{
    private readonly WorkoutService _workoutService;
    
    [ObservableProperty]
    private ObservableCollection<WorkoutModel> _myWorkouts;
    
    [ObservableProperty] private string _workoutType = string.Empty;
    [ObservableProperty] private string _duration = string.Empty;
    [ObservableProperty] private string _intensity = "Середня";
    [ObservableProperty] private string _notes = string.Empty;
    
    public List<string> IntensityOptions { get; } = new List<string> { "Низька", "Середня", "Висока" };

    public WorkoutsViewModel()
    {
        _workoutService = new WorkoutService();
        LoadMyWorkouts();
    }

    private void LoadMyWorkouts()
    {
        var workouts = _workoutService.GetWorkoutsForCurrentUser();
        if (MyWorkouts == null)
        {
            MyWorkouts = new ObservableCollection<WorkoutModel>(workouts);
        }
        else
        {
            MyWorkouts.Clear();
            foreach (var w in workouts)
            {
                MyWorkouts.Add(w);
            }
        }
    }

    [RelayCommand]
    private void SaveWorkout()
    {
        if (!int.TryParse(Duration, out int durationMins))
            durationMins = 0;

        var newWorkout = new WorkoutModel
        {
            Date = DateTime.Now.ToString("dd.MM.yyyy"),
            Type = WorkoutType ?? string.Empty,
            DurationMinutes = durationMins,
            Intensity = Intensity ?? string.Empty,
            Notes = Notes ?? string.Empty
        };
        _workoutService.AddWorkout(newWorkout);
        
        var currentUser = SessionManager.CurrentUser;
        if (currentUser != null)
        {
            var achievementService = new AchievementService();
            achievementService.CheckAndUnlockAchievements(currentUser.Login, newWorkout.Notes);
        }

        LoadMyWorkouts();
        
        WorkoutType = string.Empty;
        Duration = string.Empty;
        Notes = string.Empty;
    }
    [RelayCommand]
    private void DeleteWorkout(WorkoutModel workout)
    {
        if (workout != null && MyWorkouts.Contains(workout))
        {
            MyWorkouts.Remove(workout);
            
            _workoutService.DeleteWorkout(workout); 
        }
    }
}