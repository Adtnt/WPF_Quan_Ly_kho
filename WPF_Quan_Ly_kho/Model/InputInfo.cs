using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class InputInfo
    {
        public int ID { get; set; }
        public int IDMaterial { get; set; }
        public int IDInput { get; set; }
        public int Count { get; set; }
        public int InputPrice { get; set; }
        public virtual Input Input { get; set; }
        public virtual Material Material { get; set; }
    }
}
