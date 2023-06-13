using Microsoft.SqlServer.Management.Sdk.Sfc;
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
    public class SuplierViewModel : BaseViewModel
    {
        public StorageContext db = new StorageContext();

        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Suplier _SelectedItem;
        public Suplier SelectedItem
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
        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(db.Supliers.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = db.Supliers.ToList().Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;

            }, (p) =>
            {
                var Suplier = new Suplier() { DisplayName = DisplayName };

                db.Supliers.Add(Suplier);
                db.SaveChanges();

                List.Add(Suplier);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = db.Supliers.ToList().Where(x => x.ID == SelectedItem.ID);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var Suplier = db.Supliers.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();
                Suplier.DisplayName = DisplayName;
                Suplier.Phone = Phone;
                Suplier.Address = Address;
                Suplier.Email = Email;
                Suplier.ContractDate = (DateTime)ContractDate;
                Suplier.MoreInfo = MoreInfo;
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
                var sup = db.Supliers.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.Supliers.Remove(sup);
                db.SaveChanges();

                List.Remove(sup);
            });
        }
    }
}
