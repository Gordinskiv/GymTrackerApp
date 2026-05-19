using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    private readonly JsonDataService _dataService;
    private readonly AnalyticsViewModel _calculateProgress;
    
    [ObservableProperty] private string _userName;
    [ObservableProperty] private string _heightString;
    [ObservableProperty] private string _startWeightString;
    [ObservableProperty] private string _targetWeightString;
    [ObservableProperty] private string _workoutsPerWeekString;
    [ObservableProperty] private string _dailyCaloriesString;
    
    [ObservableProperty] private string _saveMessage;

    public ProfileViewModel()
    {
        _dataService = new JsonDataService();
        LoadProfileData();
    }

    private void LoadProfileData()
    {
        var profile = _dataService.LoadProfile();
        UserName = profile.Name;
        HeightString = profile.Height.ToString();
        StartWeightString = profile.StartWeight.ToString();
        TargetWeightString = profile.TargetWeight.ToString();
        WorkoutsPerWeekString = profile.WorkoutsPerWeek.ToString();
        DailyCaloriesString = profile.DailyCalories.ToString();
    }

    [RelayCommand]
    private void SaveProfile()
    {
        if (double.TryParse(HeightString, out double h) &&
            double.TryParse(StartWeightString, out double sw) &&
            double.TryParse(TargetWeightString, out double tw) &&
            int.TryParse(WorkoutsPerWeekString, out int workouts) &&
            int.TryParse(DailyCaloriesString, out int calories))
        {
            var profile = new UserProfile
            {
                Name = UserName,
                Height = h,
                StartWeight = sw,
                TargetWeight = tw,
                WorkoutsPerWeek = workouts,
                DailyCalories = calories
            };
                
            _dataService.SaveProfile(profile);
            SaveMessage = "Дані успішно збережено!";
        }
        else
        {
            SaveMessage = "Помилка! Введіть коректні числа.";
        }
    }
}