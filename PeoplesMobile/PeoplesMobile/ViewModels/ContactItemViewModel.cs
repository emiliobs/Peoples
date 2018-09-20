namespace PeoplesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PeoplesMobile.Models;
    using PeoplesMobile.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    public class ContactItemViewModel: Contact
    {
        #region Service
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;

        #endregion

        #region Attributtes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public ContactItemViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand EditContactCommand { get => new RelayCommand(EditContact); }
        #endregion

        #region Methods
        private async void EditContact()
        {
            MainViewModel.GetInstance().EditContact = new EditContactViewModel(this);
            await navigationService.Navigate("EditContactPage");
        }
        #endregion
    }
}
