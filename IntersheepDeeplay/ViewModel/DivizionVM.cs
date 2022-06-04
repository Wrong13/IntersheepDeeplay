using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IntersheepDeeplay.ViewModel
{
    public class DivizionVM : INotifyPropertyChanged
    {
        IntersheepDeeplay.Model.IntersheepContext db;

        IEnumerable<Model.Division> divisions;

        public IEnumerable<Model.Division> Divisions
        {
            get { return divisions; }
            set
            {
                divisions = value;
                OnPropertyChanged("Divisions");
            }
        }

        public string gridWidth { get; set; }
        public string btnHideShowImg { get; set; }
        public string SaveEditBtn { get; set; }
        public string NameDivisionBox {
            get; 
            set;
            }
        private bool IsShowPanel;
        private bool IsEdit;
        public Model.Division _EditDivision;

        private RelayCommand hideShowPanel;
        private RelayCommand addDivision;
        private RelayCommand delDivision;
        private RelayCommand editDivision;

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

        public DivizionVM()
        {
            IsShowPanel = true;
            IsEdit = false;
            gridWidth = "0.3*";
            btnHideShowImg = "/res/btn/mainLeft.png";
            SaveEditBtn = "Добавить";

            db = new Model.IntersheepContext();


            db.Divisions.Load();

            Divisions = db.Divisions.Local.ToBindingList();
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

        public RelayCommand AddDivision
        {
            get
            {
                return addDivision ?? (addDivision = new RelayCommand((obj) =>
                {
                    if (obj == null) return;
                    
                    if (IsEdit == false)
                    {
                        Model.Division division = new Model.Division { Name = obj as string };
                        if (Divisions.Where(x => x.Name == obj as string).ToList().Count > 0)
                        {
                            MessageBox.Show("Такая запись уже существует");
                            return;
                        }
                        db.Divisions.Add(division);
                    }
                    else if (IsEdit == true)
                    {
                        _EditDivision.Name = NameDivisionBox;
                        db.Entry(_EditDivision).State = EntityState.Modified;
                        IsEdit = false;
                        SaveEditBtn = "Добавить";
                        OnPropertyChanged("SaveEditBtn");
                    }
                    db.SaveChanges();

                    db = new Model.IntersheepContext();

                    db.Divisions.Load();

                    Divisions = db.Divisions.Local.ToBindingList();
                    NameDivisionBox = "";
                    OnPropertyChanged("NameDivisionBox");
                }));
            }

        }

        public RelayCommand EditDivision
        {
            get
            {
                return editDivision ?? (editDivision = new RelayCommand((obj) =>
                {
                    if (obj == null) return;
                    var divisionEdit = obj as Model.Division;
                    _EditDivision = divisionEdit;
                    NameDivisionBox = _EditDivision.Name;
                    OnPropertyChanged("NameDivisionBox");
                    IsEdit = true;
                    SaveEditBtn = "Изменить";
                    OnPropertyChanged("SaveEditBtn");
                }));
            }
        }

        public RelayCommand DeleteDivision
        {
            get
            {
                return delDivision ?? (delDivision = new RelayCommand((selDivision) =>
                {
                    if (delDivision == null) return;
                    var DelDivivision = selDivision as Model.Division;
                    db.Divisions.Remove(DelDivivision);
                    db.SaveChanges();
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
