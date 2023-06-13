﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Quan_Ly_kho.Model
{
    public class Customer
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MoreInfo { get; set; }
        public DateTime ContractDate { get; set; }
        public virtual ICollection<OutputInfo> OutputInfos { get; set; }
    }
}
