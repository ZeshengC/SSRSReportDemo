using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSReportDemo
{
    public class ReportSource
    {
        public string ReportName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<Unit> Units { get; set; }

        public List<FlattenedGeneralInfo> GetFlattenedGeneralInfos()
        {
            return new List<FlattenedGeneralInfo>() { new FlattenedGeneralInfo() { ReportName = this.ReportName, CreatedBy = this.CreatedBy, CreatedTime = this.CreatedTime.ToShortDateString() } };
        }

        public List<FlattenedUnit> GetFlattenedUnits()
        {
            List<FlattenedUnit> list = this.Units.Select(u => new FlattenedUnit() { Area = u.Area, CreatedBy = u.CreatedBy, UnitId = u.UnitId, UnitName = u.UnitName }).ToList();
            return list;
        }
        public List<FlattenedActivity> GetFlattenedActivity(int unitId)
        {
            List<FlattenedActivity> list = this.Units.Where(u => u.UnitId == unitId).FirstOrDefault().Activities.Select(a => new FlattenedActivity() { UnitId = a.UnitId, ActivityName = a.ActivityName, ActivityType = a.ActivityType, StartDate = a.StartDate.ToShortDateString() }).ToList();
            return list;
        }

    }
    public class FlattenedGeneralInfo
    {
        public string ReportName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedTime { get; set; }
    }
    public class FlattenedUnit
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public double Area { get; set; }
        public string CreatedBy { get; set; }
    }
    public class FlattenedActivity
    {
        public int UnitId { get; set; }
        public string ActivityType { get; set; }
        public string ActivityName { get; set; }
        public string StartDate { get; set; }
    }
}
