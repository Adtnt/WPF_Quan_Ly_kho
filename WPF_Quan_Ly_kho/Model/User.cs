using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class User
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IDRole { get; set; }
        public virtual UserRole UserRole { get; set; }
        
    }
}
