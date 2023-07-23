using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppClientToApi
{
    class Presenter
    {
        IView View;
        IModel Logic;

        public Presenter(IView view)
        {
            Logic = new ModelLogicApi();
            View = view;
        }
        public void Info()
        {
            View.Books = Logic.GetData();
        }
        public void AddNewPerson(Person NewPerson)
        {
            Logic.AddNewPerson(NewPerson);
            this.Info();
        }
        public void DeletePerson()
        {
            Logic.DeletePerson(View.PersonNow);
            this.Info();
        }
        public void EditPerson()
        {
            Logic.UpdatePerson(View.PersonNow);
        }
        public bool IsAdminUser(string UserName)
        {
           return Logic.IsAdminUser(UserName);
        }
        public void AddNewUser(UserRegistration newuser)
        {
            Logic.AddNewUser(newuser);
        }
    }
}
