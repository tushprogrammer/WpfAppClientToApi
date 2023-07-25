using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
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
using Newtonsoft.Json;

namespace WpfAppClientToApi
{
    /// <summary>
    /// Логика взаимодействия для authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private readonly HttpClient httpClient;
        private readonly ModelLogicApi api;
        public Authorization()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            api = new ModelLogicApi();
            httpClient.BaseAddress = new Uri(@"https://localhost:7068/");
        }

        /// <summary>
        /// Обработчик кнопки "Войти"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Log_enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserModel user = new UserModel()
                {
                    LoginProp = login_box.Text,
                    Password = password_box.Password
                };

                if (api.Login(user))
                {
                    //создание и открытие нового окна
                    MainWindow main = new MainWindow(user);
                    main.Show();
                    this.Close(); //после успешного входа, это окно уже не требуется
                }
                else
                {
                    ErrorBox.Text = "Пользователь не найден";
                }
            }
            catch (SocketException E)
            {
                ErrorBox.Text = "Приложение Api не запущено";
            }
            catch (Exception a)
            {
                ErrorBox.Text = a.Message; //вывод об ошибке и возврат
            }

        }
    }
}
