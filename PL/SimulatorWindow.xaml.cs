using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        bool _myClosing = false;
        DispatcherTimer Timer = new DispatcherTimer();
        public string ClockContent { get; set; }
        public string CurrentID { get; set; }
        public string OldStat { get; set; }
        public string NewStat { get; set; }
        BackgroundWorker worker;
        public SimulatorWindow()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync("argument");
            
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

        }
        private void UpdateFunc(int ID, object? order, object? oldStatus, object? newStatus, object? beginTime, object? finishTime)
        {
            CurrentID = String.Format("CurrentID is: {0}", ID);
            OldStat = String.Format(@"Old Status is: {0}, 
                                      The time of the begining: {1}", ((BO.Enums.Status)oldStatus).ToString(), ((DateTime)beginTime).ToString("hh/:mm/:ss"));
            NewStat = String.Format(@"New Status is: {0}, 
                                      The time of the begining: {1}", ((BO.Enums.Status)newStatus).ToString(), ((DateTime)finishTime).ToString("hh/:mm/:ss"));
        }
        private void StopFunc()
        {

        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // metodot mashkifot
            Simulator.Simulator.Update += UpdateFunc;
            Simulator.Simulator.Stop += StopFunc;
            

            Simulator.Simulator.RunSimulation();
            while (!worker.CancellationPending) 
            {
                worker.ReportProgress(1);
                System.Threading.Thread.Sleep(1000);
            }

        }



        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }
        private void Timer_Click(object sender, EventArgs e)
        { 
            DateTime d;
       //     ClockContent = DateTime.Now.ToString(@"hh\:mm\:ss");  
       /// Problem!!!! FIXXX!!!
           Clock.Content = DateTime.Now.ToString(@"hh\:mm\:ss");
        }

        // Example for avoiding closing the window...
        void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_myClosing) // Won't allow to cancel the window!!! It is not me!!!
            {
                e.Cancel = true;
                MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
    }
}
