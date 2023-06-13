using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Quan_Ly_kho.DAL;
using WPF_Quan_Ly_kho.Model;

namespace WPF_Quan_Ly_kho.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        public StorageContext db = new StorageContext();

        private ObservableCollection<Customer> _List;
        public ObservableCollection<Customer> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Customer _SelectedItem;
        public Customer SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }


        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }


        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }


        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }


        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(db.Customers.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = db.Customers.ToList().Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;

            }, (p) =>
            {
                var Customer = new Customer() { DisplayName = DisplayName };

                db.Customers.Add(Customer);
                db.SaveChanges();

                List.Add(Customer);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = db.Customers.ToList().Where(x => x.ID == SelectedItem.ID);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var Customer = db.Customers.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();
                Customer.DisplayName = DisplayName;
                Customer.Phone = Phone;
                Customer.Address = Address;
                Customer.Email = Email;
                Customer.ContractDate = (DateTime)ContractDate;
                Customer.MoreInfo = MoreInfo;
                db.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                SelectedItem.Phone = Phone;
                SelectedItem.Address = Address;
                SelectedItem.Email = Email;
                SelectedItem.ContractDate = (DateTime)ContractDate;
                SelectedItem.MoreInfo = MoreInfo;
                OnPropertyChanged();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = db.Customers.ToList().Where(x => x.DisplayName == DisplayName);
                if (displayList == null)
                    return false;

                return true;

            }, (p) =>
            {
                var cus = db.Customers.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.Customers.Remove(cus);
                db.SaveChanges();

                List.Remove(cus);
            });
        }
    }
}
