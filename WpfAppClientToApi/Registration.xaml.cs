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

namespace WpfAppClientToApi
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public UserRegistration Newuser;
        public Registration()
        {
            InitializeComponent();
        }
        public Registration(bool isAdmin)
        {
            InitializeComponent();
            if (isAdmin)
            {
                header.Text = "Добавление нового пользователя";
                RegistrationButton.Content = "Добавить";
            }
            RegistrationButton.Click += delegate
            {
                if (LoginPropbox.Text == string.Empty )
                {
                    ErrorLabel.Text = "Заполните поле 'Логин'";
                    return;
                }
                if (LoginPropbox.Text.Length > 20 || LoginPropbox.Text.Length < 6)
                {
                    ErrorLabel.Text = "поле 'Логин' должно быть больше 6ти и меньше 20ти символов";
                    return;
                }
                if (Passwordbox.Text == string.Empty)
                {
                    ErrorLabel.Text = "Заполните поле 'Пароль'";
                    return;
                }
                if (Passwordbox.Text.Length < 6 )
                {
                    ErrorLabel.Text = "Пароль должен быть больше 6ти символов";
                    return;
                }
                if (ConfirmPasswordbox.Text == string.Empty)
                {
                    ErrorLabel.Text = "Заполните поле 'Продублируйте пароль'";
                    return;
                }
                if (Passwordbox.Text != ConfirmPasswordbox.Text)
                {
                    ErrorLabel.Text = "Поля пароль и подтверждение пароля не совпадают";
                    return;
                }
                //проверка у пароля, чтоб был символ заглавный, символ и цифра
                char[] pass = Passwordbox.Text.ToCharArray();
                IQueryable<char> pass2 = pass.AsQueryable();
                bool upp = false, ch = false, num = false;
                //проверка на содержание хотя б одной цифры в пароле
                char[] values = new char[]
                {
                    '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
                };
                foreach (var item in pass)
                {
                    if (Char.IsDigit(item)) //проверка на наличие хотя б одной цифры
                    {
                        num = true;
                    }
                    if (char.IsLetter(item)) //проверка на наличие хотя б одной буквы
                    {
                        if (char.IsLower(item)) // проверка на наличие не заглавных
                        {
                            ch = true;
                        }
                    }
                    if (char.IsLetter(item)) //проверка на наличие хотя б одной буквы
                    {
                        if (char.IsUpper(item)) // проверка на наличие заглавных
                        {
                            upp = true;
                        }
                    }
                }
                if (!num)
                {
                    ErrorLabel.Text = "В пароле должна быть хотя б 1 цифра";
                    return;
                }
                if (!ch)
                {
                    ErrorLabel.Text = "В пароле должна быть хотя б 1 латинская буква";
                    return;
                }
                if (!upp)
                {
                    ErrorLabel.Text = "В пароле должна быть хотя б 1 заглавная буква";
                    return;
                }

                ModelLogicApi model = new ModelLogicApi();
                UserModel tryuser = new UserModel()
                {
                    LoginProp = LoginPropbox.Text,
                    Password = Passwordbox.Text
                };
                if (model.Login(tryuser))
                {
                    ErrorLabel.Text = "Такой пользователь уже существует";
                    return;
                }
                
                Newuser = new UserRegistration()
                {
                    LoginProp = LoginPropbox.Text,
                    Password = Passwordbox.Text,
                    ConfirmPassword = ConfirmPasswordbox.Text
                };
                this.DialogResult = true;



            };
        }

      
    }
}
