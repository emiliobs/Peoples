namespace PeoplesMobile.Infrastructure
{
    using PeoplesMobile.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class InstanceLocator
    {
        #region Properties

        public MainViewModel Main { get; set; }

        #endregion

        #region Contructors

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }

        #endregion
    }
}
