using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Services;

namespace PracticaGymTracker.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    private readonly Action<bool> _onRegisterSuccess;
    private readonly Action _onBackToLogin;
    private readonly AuthService _authService;
    
    [ObservableProperty] private bool _isUserRole = true;
    [ObservableProperty] private bool _isAdminRole = false;
    
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;
    [ObservableProperty] private string _errorMessage = string.Empty;

    public RegisterViewModel(Action<bool> onRegisterSuccess, Action onBackToLogin)
    {
        _onRegisterSuccess = onRegisterSuccess;
        _onBackToLogin = onBackToLogin;
        _authService = new AuthService();
    }

    [RelayCommand]
    private void Register()
    {
        ErrorMessage = string.Empty;

        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Помилка: Паролі не співпадають!";
            return;
        }

        string role = IsAdminRole ? "Trainer" : "User";
        
        var result = _authService.RegisterUser(Username, Password, role);
        
        if (result.Success)
        {
            _onRegisterSuccess?.Invoke(IsAdminRole);
        }
        else
        {
            ErrorMessage = result.Message;
        }
    }

    [RelayCommand]
    private void GoToLogin()
    {
        _onBackToLogin?.Invoke();
    }
}