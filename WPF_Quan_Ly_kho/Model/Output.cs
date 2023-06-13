using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class Output
    {
        public int ID { get; set; }
        public DateTime DateOutput { get; set; }
        public virtual ICollection<OutputInfo> OutputInfos { get; set; }
    }
}
