using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticaGymTracker.Models;
using System.Linq;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// Відображення сторінку "Головна".
/// </summary>
public partial class HomeViewModel : ViewModelBase
{
    [ObservableProperty] private string _welcomeMessage = "Привіт!";
    [ObservableProperty] private string _lastWorkoutText = "Ще немає тренувань. Час почати!";
    [ObservableProperty] private string _totalWorkouts = "0";

    public HomeViewModel()
    {
        LoadDashboardData();
    }

    private void LoadDashboardData()
    {
        var currentUser = SessionManager.CurrentUser;
        if (currentUser != null)
        {
            WelcomeMessage = $"Привіт, {currentUser.Login}!";
                
            var workoutService = new WorkoutService();
            var myWorkouts = workoutService.GetWorkoutsForCurrentUser();
                
            TotalWorkouts = myWorkouts.Count.ToString();

            if (myWorkouts.Any())
            {
                var last = myWorkouts.Last();
                LastWorkoutText = $"{last.Date} — {last.Type} ({last.DurationMinutes} хв)";
            }
        }
    }
}