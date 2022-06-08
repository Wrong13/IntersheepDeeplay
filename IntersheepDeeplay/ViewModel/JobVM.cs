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
    public class JobVM : INotifyPropertyChanged
    {
        IntersheepDeeplay.Model.IntersheepContext db;

        IEnumerable<Model.JobPosition> jobs;

        public IEnumerable<Model.JobPosition> Jobs
        {
            get { return jobs; }
            set
            {
                jobs = value;
                OnPropertyChanged("Jobs");
            }
        }

        public string gridWidth { get; set; }
        public string btnHideShowImg { get; set; }
        public string SaveEditBtn { get; set; }
        
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string JobDescription2 { get; set; }

        private bool IsShowPanel;
        private bool IsEdit;
        public Model.JobPosition _EditJob { get; set; }

        private RelayCommand hideShowPanel;
        private RelayCommand deleteJob;
        private RelayCommand editJob;
        private RelayCommand addJob;
        
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

        public JobVM()
        {
            IsShowPanel = true;
            IsEdit = false;
            gridWidth = "0.3*";
            btnHideShowImg = "/res/btn/mainLeft.png";
            SaveEditBtn = "Добавить";

            db = new Model.IntersheepContext();


            db.JobPositions.Load();

            Jobs = db.JobPositions.Local.ToBindingList();
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

        public RelayCommand AddJob
        {
            get
            {
                return addJob ?? (addJob = new RelayCommand(obj =>
                {
                    
                    if (IsEdit == false)
                    {
                        if (JobName == null) return;
                        else if (JobName.Length < 4) return;


                        _EditJob = new Model.JobPosition();
                        _EditJob.Name = JobName;
                        _EditJob.Description = JobDescription;
                        _EditJob.Description2 = JobDescription2;
                        if (Jobs.Where(x => x.Name == _EditJob.Name).ToList().Count > 0)
                        {
                            MessageBox.Show("Такая запись уже существует");
                            return;
                        }


                        db.JobPositions.Add(_EditJob);

                        JobName = "";
                        JobDescription = "";
                        JobDescription2 = "";

                        OnPropertyChanged("JobName");
                        OnPropertyChanged("JobDescription");
                        OnPropertyChanged("JobDescription2");
                    }
                    if (IsEdit == true)
                    {
                        _EditJob.Name = JobName;
                        _EditJob.Description = JobDescription;
                        _EditJob.Description2 = JobDescription2;
                        db.Entry(_EditJob).State = EntityState.Modified;
                        IsEdit = false;


                        SaveEditBtn = "Добавить";
                        OnPropertyChanged("SaveEditBtn");
                    }
                    db.SaveChanges();

                    _EditJob = null;
                    OnPropertyChanged("_EditJob");

                    db = new Model.IntersheepContext();

                    db.JobPositions.Load();

                    Jobs = db.JobPositions.Local.ToBindingList();


                }));
            }
        }

        public RelayCommand EditJob
        {
            get
            {
                return editJob ?? (editJob = new RelayCommand(obj =>
                {
                    if (obj == null) return;

                    _EditJob = obj as Model.JobPosition;

                    JobName = _EditJob.Name;
                    JobDescription = _EditJob.Description;
                    JobDescription2 = _EditJob.Description2;
                    OnPropertyChanged("JobName");
                    OnPropertyChanged("JobDescription");
                    OnPropertyChanged("JobDescription2");

                    IsEdit = true;
                    SaveEditBtn = "Изменить";
                    OnPropertyChanged("_EditJob");
                    OnPropertyChanged("SaveEditBtn");
                }));
            }
        }

        public RelayCommand DeleteJob
        {
            get
            {
                return deleteJob ?? (deleteJob = new RelayCommand(obj =>
                {
                    if (obj == null) return;
                    var _delJob = obj as Model.JobPosition;
                    db.JobPositions.Remove(_delJob);
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
