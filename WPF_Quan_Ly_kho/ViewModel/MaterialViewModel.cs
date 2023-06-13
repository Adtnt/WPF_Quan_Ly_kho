using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Quan_Ly_kho.DAL;
using WPF_Quan_Ly_kho.Model;

namespace WPF_Quan_Ly_kho.ViewModel
{
    public class MaterialViewModel : BaseViewModel
    {
        public StorageContext db = new StorageContext();

        private ObservableCollection<Model.Material> _List;
        public ObservableCollection<Model.Material> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Unit> _Unit;
        public ObservableCollection<Model.Unit> Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Suplier> _Suplier;
        public ObservableCollection<Model.Suplier> Suplier { get => _Suplier; set { _Suplier = value; OnPropertyChanged(); } }

        private Model.Material _SelectedItem;       
        public Material SelectedItem
        {
            get => _SelectedItem;
            set
            {
                    _SelectedItem = value;
                    OnPropertyChanged();
                    if (SelectedItem != null)
                    {
                        DisplayName = SelectedItem.DisplayName;
                        QRCode = SelectedItem.QRCode;
                        BarCode = SelectedItem.BarCode;
                        SelectedUnit = SelectedItem.Unit;
                        SelectedSuplier = SelectedItem.Suplier;
                    }
            }
            
        }
        private Unit _SelectedUnit;
        public Unit SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
            }
        }

        private Suplier _SelectedSuplier;
        public Suplier SelectedSuplier
        {
            get => _SelectedSuplier;
            set
            {
                _SelectedSuplier = value;
                OnPropertyChanged();
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _QRCode;
        public string QRCode { get => _QRCode; set { _QRCode = value; OnPropertyChanged(); } }

        private string _BarCode;
        public string BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }

        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public MaterialViewModel()
        {
            List = new ObservableCollection<Material>(db.Materials.Include(a => a.Unit));
            Unit = new ObservableCollection<Unit>(db.Units);
            Suplier = new ObservableCollection<Suplier>(db.Supliers);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSuplier == null || SelectedUnit == null)
                    return false;
                return true;

            }, (p) =>
            {
                var Object = new Model.Material() { DisplayName = DisplayName, BarCode = BarCode, QRCode = QRCode, IDSuplier = SelectedSuplier.ID, IDUnit = SelectedUnit.ID};

                db.Materials.Add(Object);
                db.SaveChanges();

                List.Add(Object);
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = db.Materials.ToList().Where(x => x.ID == SelectedItem.ID);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var Material = db.Materials.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();
                Material.DisplayName = DisplayName;
                Material.IDSuplier=SelectedSuplier.ID;
                Material.IDUnit = SelectedUnit.ID;
                Material.QRCode = QRCode;
                Material.BarCode = BarCode;
                db.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                SelectedItem.IDSuplier = SelectedSuplier.ID;
                SelectedItem.IDUnit = SelectedUnit.ID;
                SelectedItem.QRCode = QRCode;
                SelectedItem.BarCode = BarCode;
                OnPropertyChanged();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem==null)
                    return false;
                return true;

            }, (p) =>
            {
                var material = db.Materials.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.Materials.Remove(material);
                db.SaveChanges();

                List.Remove(material);
            });
        }
    }
}
