using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace CountingForms.ParentForms
{
    public partial class ReportShowForm : Form
    {
        public CrystalReportViewer crystalReportViewer1;
        public ReportDocument reportDocument;
        public string name;
        public ReportShowForm()
        {
            InitializeComponent();
        }

        private void ReportShowForm_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = reportDocument;

            crystalReportViewer1.ShowGroupTreeButton = false;// Отключить кнопку
            
            crystalReportViewer1.Visible = true;
            
            crystalReportViewer1.Parent = panel1;
            
            crystalReportViewer1.Dock = DockStyle.Fill;

                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // crystalReportViewer1.ExportReport();// ExportFormatType.PortableDocFormat);//, @"C:\sales.pdf") ;
            //crystalReportViewer1.ExportReport() =new CrystalReportViewer.ExportReport(Name = "111", ExportFormatType.PortableDocFormat);

            //crystalReportViewer1.ExportReport();

            //crystalReportViewer1.ExportReport("11.pdf");
            string date = DateTime.Now.ToShortDateString().ToString().Trim();
            string time = DateTime.Now.ToLongTimeString().ToString().Trim();
            string datetime = " " + date + "-" + time.Replace(':', '.');

            string dsk = //Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
                        Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            dsk+="/Отчеты/";
            if(!Directory.Exists(dsk))
            {
                Directory.CreateDirectory(dsk);
            }

            reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, @dsk + name + datetime +".pdf");
            MessageBox.Show("Отчет сохранен на рабочем столе в папке 'Отчеты'!");
        }
    }
}
