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
    /// Логика взаимодействия для AddNewPerson.xaml
    /// </summary>
    public partial class AddNewPerson : Window
    {
        public Person NewPerson;
        public AddNewPerson()
        {
            InitializeComponent();
            AddButt.Click += delegate
            {
                if (Namebox.Text == string.Empty
                || lastnamebox.Text == string.Empty
                || middlenamebox.Text == string.Empty
                || phonebox.Text == string.Empty
                || adressbox.Text == string.Empty
                || descriptionbox.Text == string.Empty
                )
                {
                    MessageBox.Show("Заполните все поля");
                }
                NewPerson = new Person(Namebox.Text, lastnamebox.Text,
                    middlenamebox.Text, phonebox.Text, 
                    adressbox.Text, descriptionbox.Text);
                this.DialogResult = true;
                //окно сразу не закрывается, как edit, так как может быть добавленно несколько пользвоателей сразу
            };
        }

   
    }
}
