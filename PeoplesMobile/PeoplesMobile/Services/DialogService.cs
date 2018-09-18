namespace PeoplesMobile.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class DialogService
    {
        public async Task showMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Accept");
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, "Yes","No");
        }
    }
}
