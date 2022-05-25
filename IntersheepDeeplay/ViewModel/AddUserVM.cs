using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntersheepDeeplay.ViewModel
{
    public class AddUserVM : INotifyPropertyChanged
    {
        Model.IntersheepContext db;

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


        public AddUserVM()
        {
            db = new Model.IntersheepContext();

            db.Divisions.Load();
            db.Workers.Load();
            db.JobPositions.Load();

            Workers = db.Workers.Local.ToBindingList();
            Divisions = db.Divisions.Local.ToBindingList();
            JobPositions = db.JobPositions.Local.ToBindingList();

            JobPositionsList.AddRange(JobPositions.Select(x => x.Name).ToList());
            DivisionsList.AddRange(Divisions.Select(x => x.Name).ToList());
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
