using EntityProj.Services;
using EntityProj.ViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityProj
{
    class ViewModelLocator
    {
        AppViewModel appViewModel;
        AddProductViewModel addProductViewModel;
        OptionAddViewModel optionAddViewModel;

        INavigationService navigationService;

        public ViewModelLocator()
        {
            navigationService = new NavigationService();

            appViewModel = new AppViewModel();
            addProductViewModel = new AddProductViewModel(navigationService);
            optionAddViewModel = new OptionAddViewModel(navigationService);

            navigationService.AddPage("AddProduct", addProductViewModel);
            navigationService.AddPage("OptionAdd", optionAddViewModel);

            navigationService.Navigate("AddProduct");
        }

        public ViewModelBase GetMainViewModel()
        {
            return appViewModel;
        }
    }
}
