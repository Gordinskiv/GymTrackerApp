using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// ViewModel для екрана авторизації.
/// </summary>
public partial class LoginViewModel : ViewModelBase
{
    private readonly Action<bool> _onLoginSuccess;
    private readonly AuthService _authService;
    
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _errorMessage = string.Empty;

    public LoginViewModel(Action<bool> onLoginSuccess)
    {
        _onLoginSuccess = onLoginSuccess;
        _authService = new AuthService();
    }

    [RelayCommand]
    private void Login()
    {
        ErrorMessage = string.Empty;
        
        var result = _authService.LoginUser(Username, Password);
        
        if (result.Success && result.User != null)
        {
            bool isAdmin = result.User.Role == "Trainer";
            _onLoginSuccess?.Invoke(isAdmin);
        }
        else
        {
            ErrorMessage = result.Message;
        }
    }
}