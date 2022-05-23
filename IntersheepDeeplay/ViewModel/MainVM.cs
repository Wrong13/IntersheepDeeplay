using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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

        IEnumerable<Model.Worker> workers;
        IEnumerable<Model.Division> divisions;
        IEnumerable<Model.JobPosition> jobPositions;

        public IEnumerable<Model.Worker> Workers
        {
            get { return workers; }
            set
            {
                workers = value;
                OnPropertyChanged("Workers");
            }
        }
        public IEnumerable<Model.Division> Divisions
        {
            get { return divisions; }
            set
            {
                divisions = value;
                OnPropertyChanged("Divisions");
            }
        }
        public IEnumerable<Model.JobPosition> JobPositions
        {
            get { return jobPositions; }
            set
            {
                jobPositions = value;
                OnPropertyChanged("JobPositions");
            }
        }
        public List<string> JobPositionsList { get; set; } = new List<string>();
        public List<string> DivisionsList { get; set; } = new List<string>();


        public string gridWidth { get; set; }
        public string btnHideShowImg { get; set; }
        private bool IsShowPanel;
        private bool IsOpenAddUserWindow = false;

        private RelayCommand hideShowPanel;
        private RelayCommand deleteWorker;
        private RelayCommand addUser;
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
            //Model.Worker worker = new Model.Worker
            //{
            //    FirstName = "Илья",
            //    LastName = "Козлов",
            //    Birthday = Convert.ToDateTime(DateTime.Today.Date),
            //    Division = db.Divisions.FirstOrDefault(),
            //    Gender = Model.Gender.мужской,
            //    JobPosition = db.JobPositions.FirstOrDefault()
            //};
            //db.Workers.Add(worker);
            //db.SaveChanges();

            //Model.Division division = new Model.Division { Name = "Тест1" };
            //db.Divisions.Add(division);
            //db.SaveChanges();

            db.Divisions.Load();
            db.Workers.Load();
            db.JobPositions.Load();
            
            Workers = db.Workers.Local.ToBindingList();
            Divisions = db.Divisions.Local.ToBindingList();
            JobPositions = db.JobPositions.Local.ToBindingList();

            JobPositionsList.AddRange(JobPositions.Select(x => x.Name).ToList());
            DivisionsList.AddRange(Divisions.Select(x => x.Name).ToList());

        }

        private void LoadAddUserWindow()
        {

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

        public RelayCommand DeleteWorker
        {
            get
            {
                return deleteWorker ?? (deleteWorker = new RelayCommand((selectedWorker) =>
                {
                    if (selectedWorker == null) return;

                    var delWorker = selectedWorker as Model.Worker;
                    db.Workers.Remove(delWorker);
                    db.SaveChanges();
                }));
            }
        }

        public RelayCommand AddUser
        {
            get
            {
                return addUser ?? (addUser = new RelayCommand((obj) =>
                {
                    if (IsOpenAddUserWindow == false)
                    {
                        AddUserWindow addUserWindow = new AddUserWindow();
                        addUserWindow.Show();
                        IsOpenAddUserWindow = true;
                    }
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
