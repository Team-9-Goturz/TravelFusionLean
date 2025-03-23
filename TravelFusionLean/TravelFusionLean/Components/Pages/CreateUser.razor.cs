using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using TravelFusionLean.Models;

namespace TravelFusionLean.Components.Pages;

public partial class CreateUser
{
    // ==== MODELS & DATA ====
    private User _user = new();
    private List<UserRole> _userRoles = new();
    private int selectedRoleId;

    // ==== PASSWORD ====
    private string _password = "";
    private string _passwordRepeat = "";
    private bool passwordTouched = false;
    private bool IsPasswordValid;
    private bool _passwordTouchedCheck => passwordTouched;
    private bool PasswordsMatch => Password == PasswordRepeat;

    private string Password
    {
        get => _password;
        set
        {
            _password = value;
            passwordTouched = true;
            StateHasChanged();
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

    // ==== VALIDATION FLAGS ====
    private bool usernameIsTaken;
    private bool IsUsernameValid;
    private bool IsUsernameAvailable => !usernameIsTaken;

    private bool IsEmailValid;
    private bool IsEmailTouched;

    private bool IsNameValid;
    private bool IsNameTouched;

    private bool IsPhoneNumberValid;
    private bool IsPhoneNumberTouched;

    private bool canCreate =>
        IsUsernameValid &&
        IsPasswordValid &&
        PasswordsMatch && 
        IsNameValid&&
        IsEmailValid &&
        IsPhoneNumberValid
        ;

    // ==== LIFECYCLE ====
    protected override async Task OnInitializedAsync()
    {
        _userRoles = (await UserRoleService.GetAllAsync()).ToList();
        await base.OnInitializedAsync();
    }

    // ==== FORM HANDLING ====
    private async Task Create()
    {
        if (canCreate)
        {
            _user.UserRole = _userRoles.FirstOrDefault(role => role.Id == selectedRoleId);
            var user = await UserService.Create(_user, Password);
            // Eventuelt redirect eller success-feedback her
        }
    }

    // ==== VALIDATION METHODS ====
    private async Task CheckUsernameAvailability(ChangeEventArgs e)
    {
        string input = e.Value?.ToString() ?? "";
        _user.Username = input;

        if (!string.IsNullOrWhiteSpace(input))
        {
            usernameIsTaken = !await UserService.IsUsernameAvailableAsync(input);
            IsUsernameValid = !usernameIsTaken;
        }
        else
        {
            usernameIsTaken = false;
            IsUsernameValid = false;
        }

        StateHasChanged();
    }

    private async Task CheckPasswordStrength(ChangeEventArgs e)
    {
        string input = e.Value?.ToString() ?? "";
        if (!string.IsNullOrWhiteSpace(input))
        {
            passwordTouched = true;
            IsPasswordValid = await UserService.IsPasswordStrongAsync(input);
        }

        StateHasChanged();
    }

    private async Task CheckName(ChangeEventArgs e)
    {
        string name = e.Value?.ToString() ?? "";
        if (string.IsNullOrWhiteSpace(name))
            IsNameValid = false;

        // Tillader bogstaver fra alle sprog, mellemrum og bindestreger
        var regex = new Regex(@"^[\p{L} \-']+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        if (regex.IsMatch(name))
        {
            IsNameValid = true;
        }

        IsNameTouched = true;
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

    private async Task CheckPhoneNumber(ChangeEventArgs e)
    {
        string phoneNumber = e.Value?.ToString() ?? "";
        if (string.IsNullOrWhiteSpace(phoneNumber))
            IsNameValid = false;

        // Tillader + eller 00 i starten, derefter 6-15 cifre og mellemrum/-
        var regex = new Regex(@"^(?:\+|00)?[0-9\s\-]{6,20}$");

        if (regex.IsMatch(phoneNumber))
        {
            IsPhoneNumberValid = true;
        }

        IsPhoneNumberTouched = true;
    }

    // ==== STYLING HELPERS ====
    private string GetCssClass(bool isValid, string fieldInput)
    {
        if (string.IsNullOrWhiteSpace(fieldInput))
            return "form-control";

        return isValid ? "form-control is-valid" : "form-control is-invalid";
    }
}
