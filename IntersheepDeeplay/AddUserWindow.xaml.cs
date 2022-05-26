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
        public Model.Worker Worker { get; private set; }
        public AddUserWindow(Model.Worker worker)
        {
            db = new Model.IntersheepContext();

            Worker = worker;
            this.DataContext = Worker;

            InitializeComponent();

            DivisionBox.ItemsSource = db.Divisions.Select(x => x.Name).ToList();
            JobPositionBox.ItemsSource = db.JobPositions.Select(x => x.Name).ToList();
        }

        private void SaveWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            Worker.JobPosition = (Model.JobPosition)db.JobPositions.Where(x => x.Name == JobPositionBox.SelectedValue.ToString()).FirstOrDefault();
            Worker.Gender = (Model.Gender)Enum.Parse(typeof(Model.Gender), GenderBox.SelectedValue.ToString());
            Worker.Division = (Model.Division)db.Divisions.Where(x => x.Name == DivisionBox.SelectedValue.ToString()).FirstOrDefault();
            Worker.Birthday = Convert.ToDateTime(BirthdayCal.SelectedDate).Date;
            Worker.FirstName = FirstNameBox.Text;
            Worker.LastName = LastNameBox.Text;

            this.DialogResult = true;
        }
    }
}
