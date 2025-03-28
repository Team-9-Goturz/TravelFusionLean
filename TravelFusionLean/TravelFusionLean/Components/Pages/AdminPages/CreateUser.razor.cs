using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using TravelFusionLean.Models;

namespace TravelFusionLean.Components.Pages;

public partial class CreateUser
{
    // ==== MODELS & DATA ====
    private User _user = new();
    private List<UserRole> _userRoles = new();
    private int selectedRoleId;
    private EditContext editContext;


    // ==== PASSWORD ====
    private string _password = "";
    private string _passwordRepeat = "";
    private bool passwordTouched = false;
    private bool IsPasswordValid;
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

    // ==== LIFECYCLE ====
    protected override async Task OnInitializedAsync()
    {
        editContext = new EditContext(_user);
        editContext.OnFieldChanged += (_, __) => EvaluateFormState();
        _userRoles = (await UserRoleService.GetAllAsync()).ToList();
        await base.OnInitializedAsync();
    }

    // ==== FORM HANDLING ====
    private bool isFormValid = false;
    private void EvaluateFormState()
    {
        isFormValid =
            IsModelValid() &&
            IsUsernameValid &&
            IsPasswordValid &&
            PasswordsMatch;

        StateHasChanged();
    }
    private bool IsModelValid()
    {
        var userResults = new List<ValidationResult>();
        var contactResults = new List<ValidationResult>();

        var userContext = new ValidationContext(_user);
        var contactContext = new ValidationContext(_user.Contact);

        var isUserValid = Validator.TryValidateObject(_user, userContext, userResults, validateAllProperties: true);
        var isContactValid = Validator.TryValidateObject(_user.Contact, contactContext, contactResults, validateAllProperties: true);

        return isUserValid && isContactValid;
    }


    private async Task Create()
    {
        if (!editContext.Validate())
            return;

        if (!IsPasswordValid || !PasswordsMatch || usernameIsTaken)
            return;

        _user.UserRole = _userRoles.FirstOrDefault(r => r.Id == selectedRoleId);
        var user = await UserService.Create(_user, Password);

        // TODO: Vis success-besked eller redirect
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
        EvaluateFormState();
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
        EvaluateFormState();
        StateHasChanged();
    }

    // ==== STYLING HELPERS ====

    private string GetCssClassFor<T>(Expression<Func<T>> accessor)
    {
        var field = FieldIdentifier.Create(accessor);
        var isValid = !editContext.GetValidationMessages(field).Any();
        var fieldValue = accessor.Compile().Invoke()?.ToString();
        return GetCssClass(isValid, fieldValue);
    }

    private string GetCssClass(bool isValid, string fieldInput)
    {
        if (string.IsNullOrWhiteSpace(fieldInput))
            return "form-control";

        return isValid ? "form-control is-valid" : "form-control is-invalid";
    }
}
