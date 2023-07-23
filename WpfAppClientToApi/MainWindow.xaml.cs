using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppClientToApi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter presenter;
        public MainWindow() //для анонимных пользователей
        {
            InitializeComponent();
            
            presenter = new Presenter(this);
            presenter.Info();
            AddButton.Visibility = Visibility.Hidden;
            EditButton.Visibility = Visibility.Hidden;
            DelButton.Visibility = Visibility.Hidden;
            AddUserButton.Visibility = Visibility.Hidden;

        }
        public MainWindow(UserModel usernow) // для авторизованных пользователей 
        {
            InitializeComponent();
            Load(usernow);
            
        }
        private void Load(UserModel usernow)
        {
            presenter = new Presenter(this);
            presenter.Info();
            //если зашел под польз
            AuthorizationButton.Visibility = Visibility.Hidden;
            if (!presenter.IsAdminUser(usernow.LoginProp))
            {
                //если не админ
                EditButton.Visibility = Visibility.Hidden;
                DelButton.Visibility = Visibility.Hidden;
                AddUserButton.Visibility = Visibility.Hidden;
            }
           
        }

        public ObservableCollection<Person> Books
        {
            set => listViewBook.ItemsSource = value;
        }
        public Person PersonNow
        {
            get => (Person)listViewBook.SelectedItem;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //открыть новое окно с формой заполнения 
            AddNewPerson add = new AddNewPerson();
            add.ShowDialog();
            if (add.DialogResult.Value)
            {
                presenter.AddNewPerson(add.NewPerson);
            }
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonNow is null)
            {
                MessageBox.Show("Выберете контакт в таблице");
                return;
            }
            presenter.DeletePerson();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonNow is null)
            {
                MessageBox.Show("Выберете контакт в таблице");
                return;
            }
            presenter.EditPerson();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            authorization authorization = new authorization();
            authorization.Show();
            this.Close();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            bool isAdmin = true;
            Registration registration = new Registration(isAdmin);
            registration.ShowDialog();
            if (registration.DialogResult.Value)
            {
                presenter.AddNewUser(registration.Newuser);
            }
        }
    }
}
