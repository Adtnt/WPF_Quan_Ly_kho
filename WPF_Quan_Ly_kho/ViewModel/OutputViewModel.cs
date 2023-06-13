using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Quan_Ly_kho.DAL;
using WPF_Quan_Ly_kho.Model;

namespace WPF_Quan_Ly_kho.ViewModel
{
    public class OutputViewModel : BaseViewModel
    {
        public StorageContext db = new StorageContext();

        private ObservableCollection<Model.Output> _List;
        public ObservableCollection<Model.Output> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.OutputInfo> _OutputInfo;
        public ObservableCollection<Model.OutputInfo> OutputInfo { get => _OutputInfo; set { _OutputInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Material> _Material;
        public ObservableCollection<Model.Material> Material { get => _Material; set { _Material = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Customer> _Customer;
        public ObservableCollection<Model.Customer> Customer { get => _Customer; set { _Customer = value; OnPropertyChanged(); } }

        private Model.Output _SelectedItem;
        public Output SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DateOutput = SelectedItem.DateOutput;
                }
            }

        }

        private OutputInfo _SelectedOutputInfo;
        public OutputInfo SelectedOutputInfo
        {
            get => _SelectedOutputInfo;
            set
            {
                _SelectedOutputInfo = value;
                OnPropertyChanged();
                if (SelectedOutputInfo != null)
                {
                    SelectedItem = SelectedOutputInfo.Output;
                    SelectedMaterial = SelectedOutputInfo.Material;
                    SelectedCustomer = SelectedOutputInfo.Customer;
                }
            }
        }

        private Material _SelectedMaterial;
        public Material SelectedMaterial
        {
            get => _SelectedMaterial;
            set
            {
                _SelectedMaterial = value;
                OnPropertyChanged();
                if (SelectedMaterial != null)
                {
                    SelectedMaterial.DisplayName = SelectedOutputInfo.Material.DisplayName;
                }
            }
        }

        private Customer _SelectedCustomer;
        public Customer SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();
                if (_SelectedCustomer != null)
                { 
                    SelectedCustomer.DisplayName = SelectedCustomer.DisplayName;
                }
            }
        }

        private DateTime? _DateOutput;
        public DateTime? DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand AddInfoCommand { get; set; }
        public ICommand EditInfoCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteInfoCommand { get; set; }
        public OutputViewModel()
        {

            OutputInfo = new ObservableCollection<OutputInfo>(db.OutputInfos.Include(a => a.Output).Include(a => a.Material).Include(a=> a.Customer));
            Material = new ObservableCollection<Material>(db.Materials.Include(a=> a.OutputInfos));
            List = new ObservableCollection<Output>(db.Outputs.ToList());
            Customer = new ObservableCollection<Customer>(db.Customers.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (DateOutput == null)
                    return false;

                var displayList = db.Outputs.ToList().Where(x => x.DateOutput == DateOutput);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                var Output = new Output() { DateOutput = (DateTime)DateOutput };

                db.Outputs.Add(Output);
                db.SaveChanges();

                List.Add(Output);
            });

            AddInfoCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedMaterial == null)
                    return false;

                var displayList = db.OutputInfos.ToList().Where(x => x.IDOutput == SelectedItem.ID);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                var displayMaterial = db.OutputInfos.ToList().Where(x => x.IDMaterial == SelectedMaterial.ID);
                if (displayMaterial == null || displayMaterial.Count() != 0)
                    return false;

                var displayCustomer = db.OutputInfos.ToList().Where(x => x.IDCustomer == SelectedCustomer.ID);
                if (displayCustomer == null || displayCustomer.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                var Outputinfo = new OutputInfo() { IDOutput = SelectedItem.ID, IDMaterial = SelectedMaterial.ID, IDCustomer = SelectedCustomer.ID, Count = SelectedOutputInfo.Count, OutputPrice = SelectedOutputInfo.OutputPrice };

                db.OutputInfos.Add(Outputinfo);
                db.SaveChanges();

                OutputInfo.Add(Outputinfo);
            });

            EditInfoCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedMaterial == null)
                    return false;

                var displayList = db.OutputInfos.ToList().Where(x => x.ID == SelectedOutputInfo.ID);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                var displayMaterial = db.OutputInfos.ToList().Where(x => x.IDMaterial == SelectedMaterial.ID);
                if (displayMaterial == null || displayMaterial.Count() != 0)
                    return false;

                var displayCustomer = db.OutputInfos.ToList().Where(x => x.IDCustomer == SelectedCustomer.ID);
                if (displayCustomer == null || displayCustomer.Count() != 0)
                    return false;

                return false;

            }, (p) =>
            {
                var Outputinfo = db.OutputInfos.ToList().Where(x => x.ID == SelectedOutputInfo.ID).SingleOrDefault();
                Outputinfo.IDOutput = SelectedItem.ID;
                Outputinfo.IDMaterial = SelectedMaterial.ID;
                Outputinfo.IDCustomer = SelectedCustomer.ID;
                Outputinfo.Count = SelectedOutputInfo.Count;
                Outputinfo.OutputPrice = SelectedOutputInfo.OutputPrice;
                db.SaveChanges();

                SelectedOutputInfo.IDOutput = SelectedItem.ID;
                SelectedOutputInfo.IDMaterial = SelectedMaterial.ID;
                SelectedOutputInfo.IDCustomer = SelectedCustomer.ID;
                SelectedOutputInfo.Count = SelectedOutputInfo.Count;
                SelectedOutputInfo.OutputPrice = SelectedOutputInfo.OutputPrice;
                OnPropertyChanged();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;

            }, (p) =>
            {
                var Output = db.Outputs.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.Outputs.Remove(Output);
                db.SaveChanges();

                List.Remove(Output);
            });

            DeleteInfoCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedOutputInfo == null)
                    return false;

                return true;

            }, (p) =>
            {
                var info = db.OutputInfos.ToList().Where(x => x.ID == SelectedOutputInfo.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.OutputInfos.Remove(info);
                db.SaveChanges();

                OutputInfo.Remove(info);
            });
        }
    }
}
