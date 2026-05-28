using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticaGymTracker.Models;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// ViewModel для панелі управління тренера.
/// Дозволяє керувати списком клієнтів та переглядати їхній прогрес.
/// </summary>
public partial class AdminPanelViewModel : ViewModelBase
{
    /// <summary>
    /// Динамічна колекція клієнтів. 
    /// Зміни в цій колекції автоматично відображаються в інтерфейсі (UI).
    /// </summary>
    [ObservableProperty] 
    private ObservableCollection<ClientItem> _clientsList;
    
    /// <summary>
    /// Загальна кількість зареєстрованих клієнтів (для верхньої картки).
    /// </summary>
    [ObservableProperty] private string _totalClients = "0";
    /// <summary>
    /// Кількість клієнтів, які були активні сьогодні (для верхньої картки).
    /// </summary>
    [ObservableProperty] private string _activeToday = "0";

    public AdminPanelViewModel()
    {
        var authService = new AuthService();
        var realAthletes = authService.GetAthletes();
        
        TotalClients = realAthletes.Count.ToString();
        ActiveToday = realAthletes.Count > 0 ? "1" : "0";
        
        ClientsList = new ObservableCollection<ClientItem>(
            realAthletes.Select(athlete => new ClientItem
            {
                Name = athlete.Login, 
                Goal = "Базова програма",
                LastActive = "Нещодавно",
                ProgressPercent = 10, 
                StatusColor = "#00FF00"
            })
        );
    }
}