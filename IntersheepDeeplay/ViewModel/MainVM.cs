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

        IEnumerable<string> jobNames;
        IEnumerable<string> divizionNames;

        public IEnumerable<string> JobNames
        {
            get { return jobNames; }
            set
            {
                jobNames = value;
                OnPropertyChanged("JobNames");
            }
        }

        public IEnumerable<string> DivizionNames
        {
            get { return divizionNames; }
            set
            {
                divizionNames = value;
                OnPropertyChanged("DivizionNames");
            }
        }

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

        public string gridWidth { get; set; }
        public string btnHideShowImg { get; set; }
        private bool IsShowPanel;
        public string findJobNames { get; set; }
        public string findDivizionNames { get; set; }

        private RelayCommand hideShowPanel;
        private RelayCommand deleteWorker;
        private RelayCommand addWorker;
        private RelayCommand editWorker;
        private RelayCommand editDiv;
        private RelayCommand editJob;
        private RelayCommand goFiltr;
        private RelayCommand dropFiltr;
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

        private void UpdateBind()
        {
            db = new Model.IntersheepContext();


            db.Divisions.Load();
            db.Workers.Load();
            db.JobPositions.Load();


            Workers = db.Workers.Local.ToBindingList();
            
            Divisions = db.Divisions.Local.ToBindingList();
            JobPositions = db.JobPositions.Local.ToBindingList();

            JobNames = db.JobPositions.Select(x => x.Name).ToList();
            DivizionNames = db.Divisions.Select(x => x.Name).ToList();

        }

        public MainVM()
        {
            IsShowPanel = true;
            gridWidth = "0.3*";
            btnHideShowImg = "/res/btn/mainLeft.png";

            db = new Model.IntersheepContext();


            UpdateBind();
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

        public RelayCommand AddWorker
        {
            get
            {
                return addWorker ?? (addWorker = new RelayCommand((obj) =>
                {

                    AddUserWindow addUserWindow = new AddUserWindow(new Model.Worker());
                    if (addUserWindow.ShowDialog() == true)
                    {
                        Model.Worker worker = addUserWindow.Worker;

                        db.Workers.Add(worker);
                        db.SaveChanges();
                    }
                }));
            }
        }

        public RelayCommand EditWorker
        {
            get
            {
                return editWorker ?? (editWorker = new RelayCommand((selectedWorker)=>
                {
                    if (selectedWorker == null) return;
                    Model.Worker editWorker = selectedWorker as Model.Worker;
                    AddUserWindow addUserWindow = new AddUserWindow(editWorker,true);
                    if (addUserWindow.ShowDialog() == true)
                    {
                        editWorker = addUserWindow.Worker;
                        db.Entry(editWorker).State = EntityState.Modified;
                        db.SaveChanges();

                        UpdateBind();
                    }
                }));
            }
        }

        public RelayCommand EditDiv
        {
            get
            {
                return editDiv ?? (editDiv = new RelayCommand(obj =>
                {
                    
                    RedactorWindow redactor = new RedactorWindow();
                    if (redactor.ShowDialog() == true)
                    {


                        UpdateBind();
                    }
                }));
            }
        }

        public RelayCommand EditJob
        {
            get
            {
                return editJob ?? (editJob = new RelayCommand(obj =>
                {
                    JobRedactWindow jobRedact = new JobRedactWindow();
                    if (jobRedact.ShowDialog() == true)
                    {


                        UpdateBind();
                    }
                }));
            }
        }

        public RelayCommand GoFiltr
        {
            get
            {
                return goFiltr ?? (goFiltr = new RelayCommand(obj =>
                {
                    if (findDivizionNames != null && findJobNames != null) Workers = db.Workers.Where(x => x.JobPosition.Name == findJobNames).
                        Where(x => x.Division.Name == findDivizionNames).ToList();
                    else
                    {
                        if (findJobNames != null) Workers = db.Workers.Where(x => x.JobPosition.Name == findJobNames).ToList();
                        if (findDivizionNames != null) Workers = db.Workers.Where(x => x.Division.Name == findDivizionNames).ToList();
                    }

                    OnPropertyChanged("Workers");
                }));
            }
        }

        public RelayCommand DropFiltr
        {
            get
            {
                return dropFiltr ?? (dropFiltr = new RelayCommand(obj =>
                {
                    findDivizionNames = "";
                    findJobNames = "";
                    OnPropertyChanged("findDivizionNames");
                    OnPropertyChanged("findJobNames");
                    
                    UpdateBind();
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
