using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
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
        public MainWindow() //для анонимных пользователей (открывается при запуске)
        {
            InitializeComponent();
            Load();
        }
        public MainWindow(UserModel usernow) // для авторизованных пользователей 
        {
            InitializeComponent();
            Load(usernow);
            
        }

        /// <summary>
        /// Метод загрузки данных для авторизованного пользователя
        /// </summary>
        /// <param name="usernow">Модель авторизованного пользователя</param>
        private void Load(UserModel usernow)
        {
            presenter = new Presenter(this);
            try
            {
                presenter.Info();
            }
            catch (SocketException E)
            {
                MessageBox.Show("api не запущена, данных не будет");
            }
            catch (Exception E)
            {
                MessageBox.Show($"Error: {E.Message} {Environment.NewLine}" +
                    $"Проверьте, запущен ли API сервис.");

            }
            //если зашел под польз
            AuthorizationButton.Visibility = Visibility.Hidden;
            RegistrButton.Visibility = Visibility.Hidden;
            if (!presenter.IsAdminUser(usernow.LoginProp))
            {
                //если не админ
                EditButton.Visibility = Visibility.Hidden;
                DelButton.Visibility = Visibility.Hidden;
                AddUserButton.Visibility = Visibility.Hidden;
            }
           
        }
        /// <summary>
        /// Метод загрузки данных для анонимного пользователя
        /// </summary>
        private void Load()
        {
            presenter = new Presenter(this);
            try
            {
                presenter.Info();
            }
            catch (SocketException E)
            {
                MessageBox.Show("api не запущена, данных не будет");
            }
            catch (Exception E)
            {
                MessageBox.Show($"Error: {E.Message} {Environment.NewLine}" +
                    $"Проверьте, запущен ли API сервис.");

            }
            AddButton.Visibility = Visibility.Hidden;
            EditButton.Visibility = Visibility.Hidden;
            DelButton.Visibility = Visibility.Hidden;
            AddUserButton.Visibility = Visibility.Hidden;
        }

        #region Связи с объектами через Presenter
        public ObservableCollection<Person> Books
        {
            set => listViewBook.ItemsSource = value;
        }
        public Person PersonNow
        {
            get => (Person)listViewBook.SelectedItem;
        }
        #endregion

        #region Кнопки
        /// <summary>
        /// Обработчик кнопки "Добавить контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработчик кнопки "удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonNow is null)
            {
                MessageBox.Show("Выберете контакт в таблице");
                return;
            }
            presenter.DeletePerson();
        }

        /// <summary>
        /// Обработчик кнопки "изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonNow is null)
            {
                MessageBox.Show("Выберете контакт в таблице");
                return;
            }
            presenter.EditPerson();
        }

        /// <summary>
        /// Обработчик кнопки "Вход"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
        /// <summary>
        /// Обработчик кнопки "Добавить пользователя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработчик кнопки "Регистрация"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
            if (registration.DialogResult.Value)
            {
                presenter.AddNewUser(registration.Newuser);
                //после успешной регистрации происходит повторная загрузка данных 
                UserModel userModel = new UserModel()
                {
                    LoginProp = registration.Newuser.LoginProp,
                    Password = registration.Newuser.Password
                };
                Load(userModel);
            }
        }
        #endregion
    }
}
