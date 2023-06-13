using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class Input
    {
        public int ID { get; set; }
        public DateTime DateInput { get; set; }
        public virtual ICollection<InputInfo> InputInfos { get; set; }

    }
}
