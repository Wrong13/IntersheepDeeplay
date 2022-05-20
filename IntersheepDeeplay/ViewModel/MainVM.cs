using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IntersheepDeeplay.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        IntersheepDeeplay.Model.IntersheepContext db;
        public string gridWidth { get; set; }
        public string btnHideShowImg { get; set; }


        private bool IsShowPanel;
        private RelayCommand hideShowPanel;
        private void ShowPanel()
        {
            gridWidth = "0.3*";
            btnHideShowImg = "/res/btn/mainLeft.png";
            IsShowPanel = true;
            OnPropertyChanged("gridWidth");
            OnPropertyChanged("btnHideShowImg");
        }
        private void HidePanel()
        {
            gridWidth = "0";
            btnHideShowImg = "/res/btn/mainRight.png";
            IsShowPanel = false;
            OnPropertyChanged("gridWidth");
            OnPropertyChanged("btnHideShowImg");
        }

        public MainVM()
        {
            IsShowPanel = true;
            gridWidth = "0.3*";
            btnHideShowImg = "/res/btn/mainLeft.png";

            db = new Model.IntersheepContext();
            Model.Worker worker = new Model.Worker
                { FirstName = "Илья",
                LastName = "Козлов",
                 Birthday = Convert.ToDateTime(DateTime.Now),
                  Division = "q1ew",
                   Gender = Model.Gender.male,
                    
            };
            db.Workers.Add(worker);
            db.SaveChanges();
        }

        public RelayCommand HideShowPanel
        {
            get
            {
                return hideShowPanel ?? (hideShowPanel = new RelayCommand((FindWidth) =>
                {
                    if (IsShowPanel == true)
                        HidePanel();
                    else ShowPanel();
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
