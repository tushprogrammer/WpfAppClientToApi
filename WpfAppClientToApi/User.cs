using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WpfAppClientToApi
{
    public class UserModel
    {
        public string LoginProp { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } //пароль
    }
}
