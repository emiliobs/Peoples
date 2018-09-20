namespace PeoplesMobile.Views
{
    using PeoplesMobile.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsPage : ContentPage
	{
		public ContactsPage ()
		{
			InitializeComponent ();

            //var mainViewModel = MainViewModel.GetInstance();
            //base.Appearing += (sender, e)=> 
            //{
            //    mainViewModel.Contacts.RefreshCommand.Execute(this);
            //};
		}
	}
}