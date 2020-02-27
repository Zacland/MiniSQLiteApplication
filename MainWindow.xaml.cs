using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLiteApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PersonModel> people = new List<PersonModel>();

        public MainWindow()
        {
            InitializeComponent();

            LoadPeopleList();
            SQLiteDataAccess.LoadGridPeople(PeopleGrid);
        }

        private void LoadPeopleList()
        {
            // TODO - Get real data here
            people = SQLiteDataAccess.LoadPeople();

            WireUpPeopleList();
        }

        private void WireUpPeopleList()
        {
            listPeopleListBox.ItemsSource = null;
            listPeopleListBox.ItemsSource = people;
            listPeopleListBox.DisplayMemberPath = "FullName";
        }

        private void refreshListButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPeopleList();
            SQLiteDataAccess.LoadGridPeople(PeopleGrid);
        }

        private void addPersonButton_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = new PersonModel();

            p.FirstName = firstNameText.Text;
            p.LastName = lastNameText.Text;

            SQLiteDataAccess.SavePerson(p);

            firstNameText.Text = "";
            lastNameText.Text = "";
        }
    }
}
