using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfAppClientToApi
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public Person(string Name, string LastName, string MiddleName, string PhoneNumber, string Address, string Description)
        {
            //id уже не нужно присваивать в конструкторе, так как это произойдет автоматически при добавлении в БД
            this.Name = Name;
            this.LastName = LastName;
            this.MiddleName = MiddleName;
            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.Description = Description;
        }
    }
}
