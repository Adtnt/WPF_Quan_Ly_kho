using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class Unit
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public virtual ICollection<Material> Materials { get; set; }

    }
}
