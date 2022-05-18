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
        public string gridWidth;
        private bool IsShowPanel;
        private RelayCommand hideShowPanel;
        private void ShowPanel()
        {
            gridWidth = "0.3*";
            IsShowPanel = true;
        }
        private void HidePanel()
        {
            gridWidth = "0";
            IsShowPanel = false;
        }

        public MainVM()
        {
            IsShowPanel = true;  
        }

        public string GridWidth
        {
            get { return gridWidth; }
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
