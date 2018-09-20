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

    public class ContactsPageViewModel :BaseViewModel
    {
        #region Services
        private ApiService apiservice;
        private DialogService dialogService;
       
        #endregion

        #region Attributtes
        ObservableCollection<ContactItemViewModel> listContacts;
        private bool isRefreshing;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<ContactItemViewModel> ListContacts
        {
            get => listContacts;

            set
            {
                if (listContacts != value)
                {
                    listContacts = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Contact>  myListContacts { get; set; }

        private string filter;

        public string Filter
        {
            get =>  filter;
            set
            {
                if (filter != value)
                {
                    filter = value;
                    this.RefreshList();
                } 
            }
        }

        #endregion

        #region Cosntructors
        public ContactsPageViewModel()
        {

            instance = this;
            //Services:
            apiservice = new ApiService();
            dialogService = new DialogService();
           

            //this.Contacts = new ObservableCollection<ContactItemViewModel>();
           LoadContacts();

          
      
        }
        #endregion

        #region Singlenton

        private static ContactsPageViewModel instance;

        public static ContactsPageViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ContactsPageViewModel();
            }

            return instance;
        }

        #endregion

        #region Commands
       
        public ICommand SearchCommand { get => new RelayCommand(RefreshList); }
        public ICommand RefreshCommand { get => new RelayCommand(Refresh); }


        #endregion

        #region Methods    

      

        public void Refresh()
        {
            IsRefreshing = true;
            LoadContacts();
            IsRefreshing = false;
        }

        private async void LoadContacts()
        {
            IsRefreshing = true;

            var connection = await apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.showMessage("Error", connection.Message);
                return;
            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var urlPrefix = Application.Current.Resources["UrlPrefix"].ToString();
            var contactsController = Application.Current.Resources["UrlContactsController"].ToString();

            var response = await apiservice.GetList<Contact>(url, urlPrefix, contactsController);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.showMessage("Error", response.Message);
                return;
            }

             myListContacts = (List<Contact>)response.Result;

            var MyContacRefres = myListContacts.Select(c => new ContactItemViewModel()
            {
                ContactId = c.ContactId,
                Email = c.Email,
                FirstName = c.FirstName,
                Image = c.Image,
                ImageArray = c.ImageArray,
                LastName = c.LastName,
                Phone = c.Phone,


            });

            ListContacts = new ObservableCollection<ContactItemViewModel>(MyContacRefres.OrderBy(c => c.FullName));
            IsRefreshing = false;

            this.RefreshList();

        }

        private void RefreshList()
        {

            if (string.IsNullOrEmpty(Filter))
            {
                var MyContacRefres = myListContacts.Select(c => new ContactItemViewModel()
                {
                    ContactId = c.ContactId,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    Image = c.Image,
                    ImageArray = c.ImageArray,
                    LastName = c.LastName,
                    Phone = c.Phone,


                });

                ListContacts = new ObservableCollection<ContactItemViewModel>(MyContacRefres.OrderBy(c => c.FullName));
                IsRefreshing = false;
            }
            else
            {
                var MyContacRefres = myListContacts.Select(c => new ContactItemViewModel()
                {
                    ContactId = c.ContactId,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    Image = c.Image,
                    ImageArray = c.ImageArray,
                    LastName = c.LastName,
                    Phone = c.Phone,


                }).Where(c=>c.FirstName.ToLower().Trim().Contains(Filter.ToLower().Trim()));

                ListContacts = new ObservableCollection<ContactItemViewModel>(MyContacRefres.OrderBy(c => c.FullName));
                IsRefreshing = false;
            }

          
        }

        //private void ReloadContacts(List<Contact> contacts)
        //{
        //    // Contacts.Clear();

        //    //var myListContactsIteViewModel = contacts.Select(c => new ContactItemViewModel()
        //    //{
        //    //    ContactId = c.ContactId,
        //    //    Email = c.Email,
        //    //    FirstName = c.FirstName,
        //    //    Image = c.Image,
        //    //    LastName = c.LastName,
        //    //    Phone = c.Phone,

        //    //});

        //   // ListContacts.Clear();
        //    foreach (var contact in contacts.OrderBy(c => c.FirstName).ThenBy(c => c.LastName))
        //    {
        //        ListContacts.Add(new Contact
        //        {
        //            ContactId = contact.ContactId,
        //            Email = contact.Email,
        //            FirstName = contact.FirstName,
        //            Image = contact.Image,
        //            LastName = contact.LastName,
        //            Phone = contact.Phone,
        //        });
        //    }



        //}
        #endregion
    }
}
