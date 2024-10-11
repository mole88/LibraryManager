using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Client
{
    
    static class ManagerInstance
    {
        private static Manager? _instance {  get; set; }
        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Manager();
                }
                return _instance;
            }
        }
    }
}
