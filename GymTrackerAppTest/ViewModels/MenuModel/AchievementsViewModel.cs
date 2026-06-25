using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PracticaGymTracker.Models;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

public partial class AchievementsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<AchievementItem> _achievementsList;

    public AchievementsViewModel()
    {
        AchievementsList = new ObservableCollection<AchievementItem>
        {
            new AchievementItem { 
                Title = "ПЕРША СОТНЯ", 
                Subtitle = "Жим 100 кг", 
                IconPathData = "M12,17.27L18.18,21L16.54,13.97L22,9.24L14.81,8.62L12,2L9.19,8.62L2,9.24L7.45,13.97L5.82,21L12,17.27Z",
                IsUnlocked = false
            },
            new AchievementItem { 
                Title = "МАЙСТЕР ТЯГИ", 
                Subtitle = "150 кг в тязі",
                IconPathData = "M19,3H5C3.89,3 3,3.9 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3M12,16L7,11H10V7H14V11H17L12,16Z", 
                IsUnlocked = false
            },
            new AchievementItem { 
                Title = "СТАЛЕВІ НОГИ", 
                Subtitle = "Болгарські спліт-присідання", 
                IconPathData = "M7,2V13H10V22L17,10H13L17,2H7Z",
                IsUnlocked = false
            },
            new AchievementItem
            {
                Title = "РАННЯ ПТАШКА",
                Subtitle = "Тренування вранці",
                IconPathData = "M19,3H5C3.89,3 3,3.9 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3M12,16L7,11H10V7H14V11H17L12,16Z",
                IsUnlocked = false
            }
        };
        CheckRealProgress();
    }
    private void CheckRealProgress()
    {
        var currentUser = SessionManager.CurrentUser;
        if (currentUser == null) return;

        var achievementService = new AchievementService();
        var unlockedTitles = achievementService.GetUnlockedAchievements(currentUser.Login);

        foreach (var achievement in AchievementsList)
        {
            if (unlockedTitles.Contains(achievement.Title))
            {
                achievement.IsUnlocked = true;
            }
        }
    }
}