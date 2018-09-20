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
        public EditContactViewModel EditContact { get; set; }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            //Singleton
            instance = this;

            Contacts = new ContactsPageViewModel();
            navigationService = new NavigationService();

        }

        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();

            }

            return instance;
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
