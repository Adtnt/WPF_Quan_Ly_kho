using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class Material
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public int IDUnit { get; set; }
        public int IDSuplier { get; set; }
        public string QRCode { get; set; }
        public string BarCode { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Suplier Suplier { get; set; }
        public virtual ICollection<InputInfo> InputInfos { get; set; }
        public virtual ICollection<OutputInfo> OutputInfos { get; set; }
    }
}
