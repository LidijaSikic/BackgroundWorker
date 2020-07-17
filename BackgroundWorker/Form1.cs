using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorkerApp
{
    public partial class Form1 : Form
    {

        //BackgroundWorker worker;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            bcgWorker.WorkerReportsProgress = true;
            bcgWorker.WorkerSupportsCancellation = true;           
        }

        private void bcgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i+=5)
            {                
                Thread.Sleep(500);               
                bcgWorker.ReportProgress(i);

                if (bcgWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void bcgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblPostotak.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bcgWorker.RunWorkerAsync();           
        }

        private void btnStop_Click(object sender, EventArgs e)
        {          
            bcgWorker.CancelAsync();          
            progressBar.Value = 0;
            lblPostotak.Text = "";          
        }

        private void bcgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = 0;
            lblPostotak.Text = "";
        }
    }
}
