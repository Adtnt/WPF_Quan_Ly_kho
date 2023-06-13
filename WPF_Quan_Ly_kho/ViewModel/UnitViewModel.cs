using Microsoft.SqlServer.Management.Sdk.Sfc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WPF_Quan_Ly_kho.DAL;
using WPF_Quan_Ly_kho.Model;
using MessageBox = System.Windows.MessageBox;

namespace WPF_Quan_Ly_kho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        public StorageContext db = new StorageContext();

        public ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public Unit _SelectedItem;
        public Unit SelectedItem
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


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(db.Units.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = db.Units.ToList().Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;

            }, (p) =>
            {
                var unit = new Unit() { DisplayName = DisplayName };

                db.Units.Add(unit);
                db.SaveChanges();

                List.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                    return false;

                var displayList = db.Units.ToList().Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;

            }, (p) =>
            {
                var unit = db.Units.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();
                unit.DisplayName = DisplayName;
                db.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                OnPropertyChanged();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;

            }, (p) =>
            {
                var unit = db.Units.ToList().Where(x => x.ID == SelectedItem.ID).SingleOrDefault();

                var result = MessageBox.Show("Are u sure??", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                db.Units.Remove(unit);
                db.SaveChanges();

                List.Remove(unit);
            });
        }
    }
}
