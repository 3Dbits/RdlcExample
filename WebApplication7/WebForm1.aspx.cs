using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataSet ds = new DataSet();

                // replace the connection string with your true database information
                using (SqlConnection sqlconn = new SqlConnection("Data Source=WMAU5958;Initial Catalog=AdventureWorks2019;Integrated Security=True"))
                {
                    // query to fetch data from database, repleace the query with your true SQL statements
                    SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM [AdventureWorks2019].[Production].[Product]", sqlconn);
                    adap.Fill(ds);
                }

                // set to local report mode
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                // load local report path, throw an error when report path is invalid
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report1.rdlc");

                // clear any default data source
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource dataSource = new ReportDataSource("DataSet1", ds.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Add(dataSource);

                // generate PDF format file and store data stream in buffer
                byte[] buffer = ReportViewer1.LocalReport.Render(format: "PDF", deviceInfo: "");

                // save PDF file to physical path, replace the path with your true file path
                using (FileStream stream = new FileStream(Server.MapPath("~/output.pdf"), FileMode.Create))
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}