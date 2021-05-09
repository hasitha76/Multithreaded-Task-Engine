using System;
using System.Collections.Generic;
using System.Text;

namespace SEB.DAL.Entity
{
    public class Audits: BaseEntity
    {
        public int Total { get; set; }
        public string Who { get; set; }
        public DateTime Created { get; set; }
        public string Filters { get; set; }
    }
}
