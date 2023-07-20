using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppClientToApi
{
    class Presenter
    {
        View View;
        ModelLogic Logic;

        public Presenter(View view)
        {
            Logic = new ModelLogic();
            View = view;
        }

    }
}
