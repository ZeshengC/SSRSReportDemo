using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSReportDemo
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public double Area { get; set; }
        public string CreatedBy { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
