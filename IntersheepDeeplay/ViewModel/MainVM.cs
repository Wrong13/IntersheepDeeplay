using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IntersheepDeeplay.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public string gridWidth { get; set; }
        private bool IsShowPanel;
        private RelayCommand hideShowPanel;
        private void ShowPanel()
        {
            gridWidth = "0.3*";
            IsShowPanel = true;
            OnPropertyChanged("gridWidth");
        }
        private void HidePanel()
        {
            gridWidth = "0";
            IsShowPanel = false;
            OnPropertyChanged("gridWidth");
        }

        public MainVM()
        {
            IsShowPanel = true;
            gridWidth = "0.3*";
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
