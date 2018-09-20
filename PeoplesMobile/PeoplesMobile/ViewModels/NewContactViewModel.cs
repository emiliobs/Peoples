namespace PeoplesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PeoplesMobile.Helpers;
    using PeoplesMobile.Models;
    using PeoplesMobile.Services;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
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
        private ImageSource imageSource;
        private MediaFile file;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public ImageSource ImageSource
        {
            get => imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public ICommand TakePictureCommand { get => new RelayCommand(TakePicture); }


        #endregion

        #region Methods

        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.showMessage("No Camera", ":( No camera available.");
            }

            file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                PhotoSize = PhotoSize.Small,
            });

            IsRunning = true;

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
                     IsRunning = false;



        }

        private async void SaveNewContact()
        {
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.showMessage("Error", connection.Message);

                return;
            }

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

            //aqui todo para guardar la foto:
            byte[] imageArray = null;
            if (file != null)
            {
                //aqui convierto un arreglo de string a byte:
                imageArray = FilesHelper.ReadFully(file.GetStream());
            }

            var contact = new Contact()
            {
               Email = Email,
               FirstName = FirstName,
               ImageArray = imageArray,
               LastName = LastName,
               Phone = Phone,
               
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var urlPrefix = Application.Current.Resources["UrlPrefix"].ToString();
            var contactsController = Application.Current.Resources["UrlContactsController"].ToString();

            var response = await apiService.Post(url,urlPrefix,contactsController, contact);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.showMessage("Error",response.Message);
                return;
            }


            var newContact = (Contact)response.Result; ;

            var contactViewModel = ContactsPageViewModel.GetInstance();
            contactViewModel.ListContacts.Add(newContact);
           contactViewModel.Refresh();

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
