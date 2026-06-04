using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace PracticaGymTracker.ViewModels;

public partial class AnalyticsViewModel : ViewModelBase
{
    private readonly JsonDataService _dataService;
    
    private List<BodyMeasurementItem> _allMeasurements;
    [ObservableProperty] private ObservableCollection<BodyMeasurementItem> _measurementsHistory;
    [ObservableProperty] private double _currentWeight;
    [ObservableProperty] private double _progressPercentage;
    [ObservableProperty] private double _targetWeight;
    
    [ObservableProperty]
    private ISeries[] _weightSeries;
    [ObservableProperty]
    private Axis[] _xAxes;
    
    [ObservableProperty] 
    private BodyMeasurementItem _selectedMeasurement;
    [ObservableProperty] private string _newWeight;
    [ObservableProperty] private string _newChest;
    [ObservableProperty] private string _newBiceps;
    [ObservableProperty] private string _newWaist;

    public AnalyticsViewModel()
    {
        _dataService = new JsonDataService();
        LoadData();
    }

    private void LoadData()
    {
        _allMeasurements = _dataService.LoadMeasurements()?.ToList() ?? new List<BodyMeasurementItem>();
        
        var currentUser = SessionManager.CurrentUser;
        if (currentUser != null)
        {
            var userMeasurements = _allMeasurements.Where(m => m.UserLogin == currentUser.Login).ToList();
            MeasurementsHistory = new ObservableCollection<BodyMeasurementItem>(userMeasurements);
        }
        else
        {
            MeasurementsHistory = new ObservableCollection<BodyMeasurementItem>();
        }

        CalculateProgress();
        UpdateChart();
    }

    public void CalculateProgress()
    {
        var currentUser = SessionManager.CurrentUser;
        
        if (currentUser == null)
        {
            TargetWeight = 80;
        }
        else
        {
            if (double.TryParse(currentUser.GoalWeight, out double parsedGoal))
            {
                TargetWeight = parsedGoal;
            }
            else
            {
                TargetWeight = 80;
            }
        }
        
        if (MeasurementsHistory.Any())
        {
            CurrentWeight = MeasurementsHistory.Last().Weight;
        }
        else
        {
            CurrentWeight = 0;
        }
        
        if (TargetWeight > 0 && CurrentWeight > 0)
        {
            if (CurrentWeight >= TargetWeight)
            {
                ProgressPercentage = 100;
            }
            else
            {
                ProgressPercentage = (CurrentWeight / TargetWeight) * 100;
            }
        }
        else
        {
            ProgressPercentage = 0;
        }
    }

    [RelayCommand]
    private void AddMeasurement()
    {
        if (double.TryParse(NewWeight, out double parsedWeight))
        {
            double.TryParse(NewChest, out double parsedChest);
            double.TryParse(NewBiceps, out double parsedBiceps);
            double.TryParse(NewWaist, out double parsedWaist);
            var currentUser = SessionManager.CurrentUser;

            var newItem = new BodyMeasurementItem
            {
                UserLogin = currentUser?.Login,
                Date = DateTime.Now,
                Weight = parsedWeight,
                Chest = parsedChest,
                Biceps = parsedBiceps,
                Waist = parsedWaist
            };
            MeasurementsHistory.Add(newItem);
            _allMeasurements.Add(newItem);
            _dataService.SaveMeasurements(_allMeasurements);
            CalculateProgress();
            UpdateChart();
            
            NewWeight = string.Empty;
            NewChest = string.Empty;
            NewBiceps = string.Empty;
            NewWaist = string.Empty;
        }
    }

    [RelayCommand]
    private void DeleteMeasurement()
    {
        if (SelectedMeasurement != null)
        {
            _allMeasurements.Remove(SelectedMeasurement);
            MeasurementsHistory.Remove(SelectedMeasurement);
            _dataService.SaveMeasurements(MeasurementsHistory.ToList());
            CalculateProgress();
            UpdateChart();
        }
    }
    private void UpdateChart()
    {
        var weights = MeasurementsHistory.Select(m => m.Weight).ToArray();
        var dates = MeasurementsHistory.Select(m => m.DisplayDate).ToArray();
        
        WeightSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = weights,
                Name = "Вага (кг)",
                Stroke = new SolidColorPaint(SKColors.DarkOrange) { StrokeThickness = 3 },
                GeometryFill = new SolidColorPaint(SKColors.DarkOrange),
                GeometryStroke = new SolidColorPaint(SKColors.DarkOrange) { StrokeThickness = 3 },
                Fill = null,
                LineSmoothness = 0.5
            }
        };
        XAxes = new Axis[]
        {
            new Axis
            {
                Labels = dates,
                LabelsPaint = new SolidColorPaint(SKColors.LightGray),
                TextSize = 14
            }
        };
    }
}
