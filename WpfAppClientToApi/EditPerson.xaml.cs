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
    /// Логика взаимодействия для EditPerson.xaml
    /// </summary>
    public partial class EditPerson : Window
    {
        public Person СhangedPerson;
        public EditPerson(Person person)
        {
            InitializeComponent();
            Load(person);
            EditButt.Click += delegate
            {
                if (Namebox.Text == string.Empty
                || lastnamebox.Text == string.Empty
                || middlenamebox.Text == string.Empty
                || phonebox.Text == string.Empty
                || addressbox.Text == string.Empty
                || descriptionbox.Text == string.Empty
                )
                {
                    MessageBox.Show("Заполните все поля");
                }
                СhangedPerson.Name = Namebox.Text;
                СhangedPerson.LastName = lastnamebox.Text;
                СhangedPerson.MiddleName = middlenamebox.Text;
                СhangedPerson.PhoneNumber = phonebox.Text;
                СhangedPerson.Address = addressbox.Text;
                СhangedPerson.Description = descriptionbox.Text;
                this.DialogResult = true;
                this.Close();//после изменнеия это окно уже не нужно
            };
        }
        /// <summary>
        /// Вывод данных о контакте в соответсвующие textbox'ы
        /// </summary>
        /// <param name="person"></param>
        private void Load(Person person)
        {
            Namebox.Text = person.Name;
            lastnamebox.Text = person.LastName;
            middlenamebox.Text = person.MiddleName;
            phonebox.Text = person.PhoneNumber;
            addressbox.Text = person.Address;
            descriptionbox.Text = person.Description;
        }

     
    }
}
