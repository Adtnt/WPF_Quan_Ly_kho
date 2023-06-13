using Microsoft.SqlServer.Management.Sdk.Sfc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Quan_Ly_kho.DAL;
using WPF_Quan_Ly_kho.Model;

namespace WPF_Quan_Ly_kho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //Lấy tồn kho
        public StorageContext db = new StorageContext();
        private ObservableCollection<InStored> _InStoredList;
        public ObservableCollection<InStored> InStoredList { get => _InStoredList;set { _InStoredList = value;OnPropertyChanged(); } }

        private DateTime _startDate { get; set; }
        private DateTime _endDate { get; set; }
        public DateTime startDate { get => _startDate ; set { _startDate = value; OnPropertyChanged(); } }
        public DateTime endDate { get => _endDate; set { _endDate = value; OnPropertyChanged(); } }

        //Nơi xử lý DataContext của MainWindow
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand MaterialCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand FilterCommand { get; set; }

        // mọi thứ xử lý sẽ nằm trong này
        public MainViewModel()
        {
            _startDate = DateTime.Parse("6/6/2023 12:00:00 AM");
            _endDate = DateTime.Parse("6/6/2023 12:00:00 AM");
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    
                    p.Show();
                    LoadInStored();

                }
                else
                {
                    p.Close();
                }
            }
              );

            void LoadInStored()
            {
                InStoredList = new ObservableCollection<InStored>();
                var materialList = db.Materials.ToList();

                int i = 1;
                foreach (var item in materialList)
                {
                    var inputList = db.InputInfos.ToList().Where(p => p.IDMaterial == item.ID);
                    var outputList = db.OutputInfos.ToList().Where(p => p.IDMaterial == item.ID);

                    int sumInput = 0;
                    int sumOutput = 0;

                    if (inputList != null)
                        foreach (var u in inputList)
                        {
                            sumInput += u.Count;
                        }

                    if (outputList != null)
                        foreach (var u in outputList)
                        {
                            sumOutput += u.Count;
                        }

                    InStored inStored = new InStored();
                    inStored.Sequence = i;
                    inStored.Amount = sumInput - sumOutput;
                    inStored.Material = item;

                    InStoredList.Add(inStored);

                    i++;
                }
            }

            void Filter()
            {
                InStoredList = new ObservableCollection<InStored>();
                var materialList = db.Materials.ToList();
                int i = 1;

                var inList = db.Inputs.Where(p => p.DateInput > startDate && p.DateInput < endDate).ToList();
                var outList = db.Outputs.Where(p => p.DateOutput > startDate && p.DateOutput < endDate).ToList();

                foreach (var item in materialList)
                {
                    InStored inStored = new InStored();

                    int sumInput = 0;
                    int sumOutput = 0;

                    foreach (var v in inList)
                    {
                        var inputList = db.InputInfos.ToList().Where(p => p.IDMaterial == item.ID).Where(p => p.IDInput == v.ID);

                        if (inputList != null)
                            foreach (var u in inputList)
                            {
                                sumInput += u.Count;
                            }                      
                    }
                    foreach (var v in outList)
                    {
                        var outputList = db.OutputInfos.ToList().Where(p => p.IDMaterial == item.ID).Where(p => p.IDOutput == v.ID);

                        if (outputList != null)
                            foreach (var u in outputList)
                            {
                                sumOutput += u.Count;
                            }
                    }
                    inStored.Sequence = i;
                    inStored.Amount = sumInput - sumOutput;
                    inStored.Material = item;

                    InStoredList.Add(inStored);

                    i++;
                }
            }

            FilterCommand = new RelayCommand<object>((p) => 
            {
                
                if (startDate > DateTime.Parse("1/1/2023 12:00:00 AM") || endDate < DateTime.Parse("1/1/2024 12:00:00 AM"))
                    return true;

                return false;
            }, (p) => {
                Filter();

            }
              );

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                UnitWindow wd = new UnitWindow();
                wd.ShowDialog();
            }
              );

            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                SuplierWindow wd = new SuplierWindow();
                wd.ShowDialog();
            }
              );

            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                InputWindow wd = new InputWindow();
                wd.ShowDialog();
            }
              );

            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                OutputWindow wd = new OutputWindow();
                wd.ShowDialog();
            }
              );

            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CustomerWindow wd = new CustomerWindow();
                wd.ShowDialog();
            }
              );

            MaterialCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MaterialWindow wd = new MaterialWindow();
                wd.ShowDialog();
            }
              );
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                UserWindow wd = new UserWindow();
                wd.ShowDialog();
            }
              );
        }
    }
}
