using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IntersheepDeeplay
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        Model.IntersheepContext db;
        public List<string> divisions = new List<string>();
        public List<string> jobpositions = new List<string>();

        public Model.Worker Worker { get; private set; }
        public AddUserWindow(Model.Worker worker)
        {
            db = new Model.IntersheepContext();

            Worker = worker;
            this.DataContext = Worker;

            InitializeComponent();

            LoadItemsSource();

        }

        private void LoadItemsSource()
        {
            divisions.Add(Worker.Division.Name);
            divisions.AddRange(db.Divisions.Where(x => x.Name != Worker.Division.Name).Select(x => x.Name));

            jobpositions.Add(Worker.JobPosition.Name);
            jobpositions.AddRange(db.JobPositions.Where(x=>x.Name != Worker.JobPosition.Name).Select(x=>x.Name));


            JobPositionBox.SelectedIndex = 0;
            JobPositionBox.ItemsSource = jobpositions;

            DivisionBox.SelectedIndex = 0;
            DivisionBox.ItemsSource = divisions;
        }

        private void SaveWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            Worker.JobPositionId = db.JobPositions.Where(x => x.Name == JobPositionBox.SelectedItem.ToString()).Select(x=>x.Id).FirstOrDefault();
            Worker.Gender = (Model.Gender)Enum.Parse(typeof(Model.Gender), GenderBox.SelectedValue.ToString());
            Worker.DivisionId = db.Divisions.Where(x => x.Name == DivisionBox.SelectedItem.ToString()).Select(x=>x.Id).FirstOrDefault();
            Worker.Birthday = Convert.ToDateTime(BirthdayCal.SelectedDate).Date;
            Worker.FirstName = FirstNameBox.Text;
            Worker.LastName = LastNameBox.Text;

            this.DialogResult = true;
        }
    }
}
