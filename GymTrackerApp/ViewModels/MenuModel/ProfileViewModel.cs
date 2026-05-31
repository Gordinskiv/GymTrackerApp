using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;


namespace PracticaGymTracker.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    [ObservableProperty] private string _username = "Гість";
    [ObservableProperty] private string _roleText = "Користувач";
    [ObservableProperty] private string _totalWorkouts = "0";
    [ObservableProperty] private string _totalAchievements = "0";
    [ObservableProperty] private string _avatarLetter = "U";
    [ObservableProperty] private string _currentGoalWeight = "0";

    public ProfileViewModel()
    {
        LoadProfileData();
    }

    private void LoadProfileData()
    {
        var currentUser = SessionManager.CurrentUser;
        if (currentUser != null)
        {
            Username = currentUser.Login;
            AvatarLetter = Username.Length > 0 ? Username.Substring(0, 1).ToUpper() : "U";
            RoleText = currentUser.Role == "Trainer" ? "Тренер (Адміністратор)" : "Спортсмен";
            CurrentGoalWeight = currentUser.GoalWeight ?? "80";
            var workoutService = new WorkoutService();
            var myWorkouts = workoutService.GetWorkoutsForCurrentUser();
            TotalWorkouts = myWorkouts.Count.ToString();
            int unlockedCount = 0;
            if (myWorkouts.Any())
            {
                if (myWorkouts.Any(w => w.Notes.Contains("100"))) unlockedCount++;
                if (myWorkouts.Any(w => w.Notes.Contains("150"))) unlockedCount++;
                if (myWorkouts.Any(w => w.Notes.ToLower().Contains("болгарськ") || w.Notes.ToLower().Contains("спліт"))) unlockedCount++;
            }
            TotalAchievements = unlockedCount.ToString();
        }
    }
    [RelayCommand]
    private void SaveGoal()
    {
        var currentUser = SessionManager.CurrentUser;
        if (currentUser != null)
        {
            currentUser.GoalWeight = CurrentGoalWeight;
            new AuthService().UpdateUserGoal(currentUser.Login, CurrentGoalWeight);
        }
    }
}