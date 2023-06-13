using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class OutputInfo
    {
        public int ID { get; set; }
        public int IDMaterial { get; set; }
        public int IDOutput { get; set; }
        public int IDCustomer { get; set; }
        public int Count { get; set; }
        public int OutputPrice { get; set; }
        public virtual Output Output { get; set; }
        public virtual Material Material { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
