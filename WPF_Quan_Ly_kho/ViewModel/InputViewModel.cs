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
    public class InputViewModel : BaseViewModel
    {
        public StorageContext db = new StorageContext();

        private ObservableCollection<Model.Input> _List;
        public ObservableCollection<Model.Input> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.InputInfo> _InputInfo;
        public ObservableCollection<Model.InputInfo> InputInfo { get => _InputInfo; set { _InputInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Material> _Material;
        public ObservableCollection<Model.Material> Material { get => _Material; set { _Material = value; OnPropertyChanged(); } }

        private Model.Input _SelectedItem;
        public Input SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DateInput = SelectedItem.DateInput;
                }
            }

        }

        private InputInfo _SelectedInputInfo;
        public InputInfo SelectedInputInfo
        {
            get => _SelectedInputInfo;
            set
            {
                _SelectedInputInfo = value;
                OnPropertyChanged();
                if (SelectedInputInfo != null)
                {
                    SelectedItem = SelectedInputInfo.Input;
                    SelectedMaterial = SelectedInputInfo.Material;
                    SelectedMaterial.DisplayName = SelectedInputInfo.Material.DisplayName;
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
                    SelectedMaterial.DisplayName = SelectedInputInfo.Material.DisplayName;
                    SelectedMaterial = SelectedInputInfo.Material;
                }
            }
        }

        private DateTime? _DateInput;
        public DateTime? DateInput { get => _DateInput; set { _DateInput = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand AddInfoCommand { get; set; }
        public ICommand EditInfoCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteInfoCommand { get; set; }
        public InputViewModel()
        {
            
            InputInfo = new ObservableCollection<InputInfo>(db.InputInfos.Include(a=> a.Input).Include(a=> a.Material));
            Material = new ObservableCollection<Material>(db.Materials.Include(a=> a.InputInfos));
            List = new ObservableCollection<Input>(db.Inputs.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (DateInput == null)
                    return false;

                var displayList = db.Inputs.ToList().Where(x => x.DateInput == DateInput);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                var input = new Input() { DateInput = (DateTime)DateInput };

                db.Inputs.Add(input);
                db.SaveChanges();

                List.Add(input);
            });

            AddInfoCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedMaterial == null)
                    return false;

                var displayList = db.InputInfos.ToList().Where(x => x.IDInput == SelectedItem.ID);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                var displayMaterial = db.InputInfos.ToList().Where(x => x.IDMaterial == SelectedMaterial.ID);
                if (displayMaterial == null || displayMaterial.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                var inputinfo = new InputInfo() { IDInput = SelectedItem.ID, IDMaterial = SelectedMaterial.ID, Count=SelectedInputInfo.Count, InputPrice=SelectedInputInfo.InputPrice};

                db.InputInfos.Add(inputinfo);
                db.SaveChanges();

                InputInfo.Add(inputinfo);
            });

            EditInfoCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedMaterial == null)
                    return false;

                var displayList = db.InputInfos.ToList().Where(x => x.ID == SelectedInputInfo.ID);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                var displayMaterial = db.InputInfos.ToList().Where(x => x.IDMaterial == SelectedMaterial.ID);
                if (displayMaterial == null || displayMaterial.Count() != 0)
                    return false;

                return false;

            }, (p) =>
            {
                var inputinfo = db.InputInfos.ToList().Where(x => x.ID == SelectedInputInfo.ID).SingleOrDefault();
                inputinfo.IDInput = SelectedItem.ID;
                inputinfo.IDMaterial = SelectedMaterial.ID;
                inputinfo.Count = SelectedInputInfo.Count;
                inputinfo.InputPrice = SelectedInputInfo.InputPrice;
                db.SaveChanges();

                SelectedInputInfo.IDInput = SelectedItem.ID;
                SelectedInputInfo.IDMaterial = SelectedMaterial.ID;
                SelectedInputInfo.Count = SelectedInputInfo.Count;
                SelectedInputInfo.InputPrice = SelectedInputInfo.InputPrice;
                OnPropertyChanged();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem==null)
                    return false;

                return true;

            }, (p) =>
            {
                var input = db.Inputs.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.Inputs.Remove(input);
                db.SaveChanges();

                List.Remove(input);
            });

            DeleteInfoCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedInputInfo==null)
                    return false;

                return true;

            }, (p) =>
            {
                var info = db.InputInfos.ToList().Where(x => x.ID == SelectedInputInfo.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.InputInfos.Remove(info);
                db.SaveChanges();

                InputInfo.Remove(info);
            });
        }
    }
}

