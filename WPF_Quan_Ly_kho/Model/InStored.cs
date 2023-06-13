using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class InStored
    {
        public int Sequence { get; set; }
        public int Amount { get; set; }
        public virtual Material Material { get; set; }

    }
}
