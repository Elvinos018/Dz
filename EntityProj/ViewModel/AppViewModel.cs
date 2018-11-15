using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityProj.ViewModel
{
    class AppViewModel : ViewModelBase 
    {
        private ViewModelBase currentPage;
        public ViewModelBase CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        public AppViewModel()
        {
            Messenger.Default.Register<ViewModelBase>(this, param =>
            {
                CurrentPage = param;
            });
        }
    }
}
