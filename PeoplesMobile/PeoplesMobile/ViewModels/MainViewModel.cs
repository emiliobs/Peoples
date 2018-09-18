namespace PeoplesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PeoplesMobile.Models;
    using PeoplesMobile.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Services
        private NavigationService navigationService;
        #endregion

        #region Atributtes

        #endregion

        #region Properties
        public ContactsPageViewModel Contacts  { get; set; }
        public  NewContactViewModel NewContact { get; set; }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            Contacts = new ContactsPageViewModel();
            navigationService = new NavigationService();

        }

        #endregion

        #region Commands
        public ICommand NewContactCommand { get => new RelayCommand(GoToNewContact); }

        #endregion

        #region Methods
        private async void GoToNewContact()
        {
            NewContact = new NewContactViewModel();
            await navigationService.Navigate("NewContactPage");
        }

        #endregion
    }
}
