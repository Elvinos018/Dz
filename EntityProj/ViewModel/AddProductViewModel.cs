using EntityProj.Message;
using EntityProj.Model;
using EntityProj.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EntityProj.ViewModel
{
    class AddProductViewModel : ViewModelBase
    {
        private string search;
        public string Search { get => search; set => Set(ref search, value); }

        private string result;
        public string Result { get => result; set => Set(ref result, value); }


        private readonly INavigationService navigationService;

        private ObservableCollection<Product> list = new ObservableCollection<Product>();
        public ObservableCollection<Product> List { get => list; set => Set(ref list, value); }


        public AddProductViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Messenger.Default.Register<ProductMessage>(this, param =>
            {
                Result = param.Product.ProductName;
            });
        }

        //Product Product = new Product();
        void Searchproduct()
        {
            using (var db = new ShopEntities())
            {
                var nameData = db.Products.Include("Supplier").Where(x => x.ProductName.Contains(Search)).ToList();
                List.Clear();
                foreach (var item in nameData)
                {
                    List.Add(item);
                }
            }
        }

        void DeleteProduct(Product Product)
        {
            using (var db = new ShopEntities())
            {
                List.Clear();
                db.Products.Remove(db.Products.Where(x=> x.Id == Product.Id).FirstOrDefault());
                db.SaveChanges();
            }
        }

        private RelayCommand productSearch;
        public RelayCommand ProductSearch
        {
            get => productSearch ?? (productSearch = new RelayCommand(
                () =>
                {
                    try
                    {
                        Searchproduct();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Wrong Search", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                ));
        }

        private RelayCommand nextWindow;
        public RelayCommand NextWindow
        {
            get => nextWindow ?? (nextWindow = new RelayCommand(
            () =>
            {
                try
                {
                    Messenger.Default.Send("Add");
                    navigationService.Navigate("OptionAdd");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            ));
        }

        private RelayCommand<Product> deleteFromdata;
        public RelayCommand<Product> DeleteFromData
        {
            get => deleteFromdata ?? (deleteFromdata = new RelayCommand<Product>(
            param =>
            {v
                try
                {
                    DeleteProduct(param);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            ));
        }

        private RelayCommand<Product> editElement;
        public RelayCommand<Product> EditElement
        {
            get => editElement ?? (editElement = new RelayCommand<Product>(
          param =>
          {
              try
              {
                  Messenger.Default.Send("Edit");
                  navigationService.Navigate("OptionAdd");
              }
              catch (Exception ex)
              {
                 MessageBox.Show(ex.Message, "Wrong", MessageBoxButton.OK, MessageBoxImage.Error);
              }
          }
          ));
        }
    }
}
