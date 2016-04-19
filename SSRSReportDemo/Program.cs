using Microsoft.Reporting.WebForms;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSReportDemo
{
    class Program
    {
        public static ReportSource rs {get;set;}
        static void Main(string[] args)
        {
            rs = new ReportSource()
            {
                CreatedBy = "Jason",
                CreatedTime = DateTime.Now,
                ReportName = "TestReport",
                Units = new List<Unit>() {
                    new Unit() { UnitId = 1, UnitName = "Unit1", Area = 10, CreatedBy = "Tom", Activities = new List<Activity>() {
                        new Activity() { UnitId = 1, ActivityName = "Activity1", ActivityType = "Type1", StartDate = new DateTime(2016,2,12) },
                        new Activity() { UnitId = 1, ActivityName = "Activity2", ActivityType = "Type2", StartDate = new DateTime(2016,2,13) }
                        }
                    },
                    new Unit() {UnitId = 2, UnitName = "Unit2", Area = 50, CreatedBy = "Sam", Activities = new List<Activity>() {
                        new Activity() { UnitId = 2, ActivityName = "Activity3", ActivityType = "Type3", StartDate = new DateTime(2016,1,20) }
                        }
                    }
                }
            };
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            ReportViewer reportViewer = new ReportViewer();
          
            reportViewer.LocalReport.ReportPath = "SummaryReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("UnitDataSet",rs.GetFlattenedUnits().ToDataTable()));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("GeneralInfoDataSet", rs.GetFlattenedGeneralInfos().ToDataTable()));
            reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            System.IO.File.WriteAllBytes(@"SummaryReport.pdf", bytes);

        }
        public static void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {

            string unitId = e.Parameters["UnitId"].Values.First();
            var activitySubSource = rs.GetFlattenedActivity(int.Parse(unitId)).ToDataTable();
            e.DataSources.Add(new ReportDataSource("ActivityDataSet", activitySubSource));
        }

    }
}
