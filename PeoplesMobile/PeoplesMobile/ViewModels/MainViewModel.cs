namespace PeoplesMobile.ViewModels
{
    using PeoplesMobile.Models;
    using PeoplesMobile.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Services
        private ApiService apiservice;
        private DialogService dialogService;
        #endregion

        #region Atributtes

        #endregion

        #region Properties
        public ObservableCollection<ContactItemViewModel> Contacts { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            //Services:
            apiservice = new ApiService();
            dialogService = new DialogService();

            this.Contacts = new ObservableCollection<ContactItemViewModel>();

            //this.Contacts = new ObservableCollection<ContactItemViewModel>();
            LoadCoontacts();

        }

        #endregion

        #region Commands

        #endregion

        #region Methods
        private async void LoadCoontacts()
        {
            var connection = await apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.showMessage("Error",connection.Message);
                return;
            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var urlPrefix = Application.Current.Resources["UrlPrefix"].ToString();
            var contactsController = Application.Current.Resources["UrlContactsController"].ToString();

            var response = await apiservice.GetList<Contact>(url, urlPrefix, contactsController);

            if (!response.IsSuccess)
            {
                await dialogService.showMessage("Error",response.Message);
                return;
            }

            ReloadContacts((List<Contact>)response.Result);
            
        }

        private void ReloadContacts(List<Contact> contacts)
        {
           // Contacts.Clear();

            //var myListContactsIteViewModel = contacts.Select(c => new ContactItemViewModel()
            //{
            //    ContactId = c.ContactId,
            //    Email = c.Email,
            //    FirstName = c.FirstName,
            //    Image = c.Image,
            //    LastName = c.LastName,
            //    Phone = c.Phone,

            //});

            Contacts.Clear();
            foreach (var contact in contacts.OrderBy(c => c.FirstName).ThenBy(c => c.LastName))
            {
                Contacts.Add(new ContactItemViewModel
                {
                    ContactId = contact.ContactId,
                    Email = contact.Email,
                    FirstName = contact.FirstName,
                    Image = contact.Image,
                    LastName = contact.LastName,
                    Phone = contact.Phone,
                });
            }



        }

        #endregion
    }
}
