using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppClientToApi
{
    interface IView
    {
        ObservableCollection<Person> Books { set; }

        Person PersonNow { get; }
    }
}
