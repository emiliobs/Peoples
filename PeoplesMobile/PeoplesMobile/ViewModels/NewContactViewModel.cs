namespace PeoplesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PeoplesMobile.Models;
    using PeoplesMobile.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NewContactViewModel : BaseViewModel
    {
        #region Service
        private DialogService dialogService;
        private ApiService apiService;
        private NavigationService navigationService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsRunning
        {
            get => isRunning;
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        #endregion

        #region Constructor
        public NewContactViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveNewContactCommand { get => new RelayCommand(SaveNewContact); }


        #endregion

        #region Methods
        private async void SaveNewContact()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.showMessage("Error", "You must enter a first name");
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.showMessage("Error", "You must enter a last name");
                return;
            }

            //if (string.IsNullOrEmpty(Image))
            //{
            //    await dialogService.showMessage("Error", "You must enter a last Image");
            //    return;
            //}

            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.showMessage("Error", "You must enter a last Email");
                return;
            }

            if (string.IsNullOrEmpty(Phone))
            {
                await dialogService.showMessage("Error", "You must enter a last Phone");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var urlPrefix = Application.Current.Resources["UrlPrefix"].ToString();
            var contactsController = Application.Current.Resources["UrlContactsController"].ToString();

            var response = await apiService.Post(url,urlPrefix,contactsController, this);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.showMessage("Error",response.Message);
                return;
            }

            IsRunning = false;
            IsEnabled = true;

            await navigationService.Back();

        }
        #endregion
    }
}
