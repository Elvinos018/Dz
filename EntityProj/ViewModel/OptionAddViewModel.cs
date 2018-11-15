using EntityProj.Message;
using EntityProj.Model;
using EntityProj.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EntityProj.ViewModel
{
    class OptionAddViewModel : ViewModelBase
    {
        private string result;
        public string Result { get => result; set => Set(ref result, value); }

        private string buttonName;
        public string ButtonName { get => buttonName; set => Set(ref buttonName, value); }

        private readonly INavigationService navigationService;

        private Supplier selectedSupplier;
        public Supplier SelectedSupplier { get => selectedSupplier; set => Set(ref selectedSupplier, value); }

        private Product product = new Product();
        public Product Product { get => product; set => Set(ref product, value); }

        private ObservableCollection<Supplier> list = new ObservableCollection<Supplier>();
        public ObservableCollection<Supplier> List { get => list; set => Set(ref list, value); }

        void SupplierList()
        {
            using (var db = new ShopEntities())
            {
                var data = db.Suppliers.ToList();

                foreach (var item in data)
                {
                    List.Add(item);
                }
            }
        }
        public OptionAddViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Messenger.Default.Register<string>(this, (name) =>
            {
                ButtonName = name;
            });

            SupplierList();
        }

        void AddProduct()
        {

            using (var db = new ShopEntities())
            {
                Product.SupplierId = SelectedSupplier.Id;
                db.Products.Add(Product);
                db.SaveChanges();
            }
            Product = new Product();
            SelectedSupplier = null;
        }

        void DeleteProduct()
        {
            using (var db = new ShopEntities())
            {
                db.Products.Remove(Product);
            }
        }
        private RelayCommand previousWindow;
        public RelayCommand PreviousWindow
        {
            get => previousWindow ?? (previousWindow = new RelayCommand(
            () =>
            {
                try
                {

                    navigationService.Navigate("AddProduct");
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong", "ti che tupoy ?", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            ));
        }

        private RelayCommand addToData;
        public RelayCommand AddToData
        {
            get => addToData ?? (addToData = new RelayCommand(
            () =>
            {
                try
                {
                    AddProduct();
                    MessageBox.Show("Sucseed", "Molodec", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong", "ti che tupoy ?", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            ));
        }
    }
}
