using Microsoft.AspNetCore.Components;
using TravelFusionLean.Models;

namespace TravelFusionLean.Components.Pages;

public partial class CreateUser
{
    private User _user = new User();
    private List<UserRole> _userRoles = new List<UserRole>();
    private int selectedRoleId;
    private string _password = "";
    private bool passwordTouched = false;
    private string _passwordRepeat = "";
    private bool usernameIsTaken;
    private bool IsUsernameValid;
    private bool IsEmailTouched;

    private string Password
    {
        get => _password;
        set
        {
            _password = value;
            passwordTouched = true;
            StateHasChanged(); // Sikrer UI opdateres live
        }
    }

    private string PasswordRepeat
    {
        get => _passwordRepeat;
        set
        {
            _passwordRepeat = value;
            StateHasChanged();
        }
    }

    private bool IsPasswordValid;
    private bool PasswordsMatch => Password == PasswordRepeat;
    private bool IsUsernameAvailable;
    private bool IsEmailValid;

    protected override async Task OnInitializedAsync()
    {
        _userRoles = (await UserRoleService.GetAllAsync()).ToList();
        await base.OnInitializedAsync();
    }

    private bool canCreate => IsPasswordValid && PasswordsMatch && !usernameIsTaken && IsEmailValid;

    private async Task Create()
    {
        if (canCreate)
        {
            _user.UserRole = _userRoles.FirstOrDefault(role => role.Id == selectedRoleId);
            var user = await UserService.Create(_user, Password);
        }
    }

    private async Task CheckUsernameAvailability(ChangeEventArgs e)
    {
        string input = e.Value?.ToString() ?? "";
        _user.Username = input;

        if (!string.IsNullOrWhiteSpace(input))
        {
            usernameIsTaken = !await UserService.IsUsernameAvailableAsync(input);
            if (usernameIsTaken)
            {
                IsUsernameValid = false;
            }
            else
            {
                IsUsernameValid = true;
            }
        }
        else
        {
            usernameIsTaken = false;
        }

        StateHasChanged(); // Opdater UI
    }

    private async Task CheckPasswordStrength(ChangeEventArgs e)
    {
        string input = e.Value?.ToString() ?? "";
        if (!string.IsNullOrWhiteSpace(input))
        {
            IsPasswordValid = await UserService.IsPasswordStrongAsync(input);
        }

        StateHasChanged();
    }

    private async Task CheckEmail(ChangeEventArgs e)
    {
        string input = e.Value?.ToString() ?? "";
        if (!string.IsNullOrWhiteSpace(input))
        {
            IsEmailValid = await UserService.IsEmailValidAsync(input);
        }

        IsEmailTouched = true;
        StateHasChanged();
    }

    private string GetCssClass(bool Isvalid, string fieldInput)
    {
        if (string.IsNullOrWhiteSpace(fieldInput))
            return "form-control"; // Neutral grå

        return Isvalid ? "form-control is-valid" : "form-control is-invalid";
    }
}