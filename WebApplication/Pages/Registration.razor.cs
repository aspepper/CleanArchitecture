using AdviceCompliance.WebApplication.ViewModels.Account;
using MudBlazor;

namespace AdviceCompliance.WebApplication.Pages
{
    public partial class Registration
    {
        RegistrationVM registrationModel = new();

        RegistrationValidationVm registrationValidator = new();

        MudForm form;

        private async Task RegisterAsync()
        {
            await form.Validate();
            if (form.IsValid)
            {
                // invoke register API call.
            }
        }
    }
}
