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
        private Stopwatch stopWatch;
        private bool isTimerRun;
        public string CurrentID
        {
            get { return (string)GetValue(CurrentIDProperty); }
            set { SetValue(CurrentIDProperty, value); }
        }
        public static readonly DependencyProperty CurrentIDProperty = DependencyProperty.Register("currentID", typeof(string), typeof(SimulatorWindow));
        public string OldStat
        {
            get { return (string)GetValue(OldStatProperty); }
            set { SetValue(OldStatProperty, value); }
        }
        public static readonly DependencyProperty OldStatProperty = DependencyProperty.Register("oldStat", typeof(string), typeof(SimulatorWindow));
        public string NewStat
        {
            get { return (string)GetValue(NewStatProperty); }
            set { SetValue(NewStatProperty, value); }
        }
        public static readonly DependencyProperty NewStatProperty = DependencyProperty.Register("newStat", typeof(string), typeof(SimulatorWindow));
        // public string OldStat { get; set; }
        // public string NewStat { get; set; }
        BackgroundWorker worker;
        public SimulatorWindow()
        {
            CurrentID = "";
            OldStat = "";
            NewStat = "";
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            stopWatch = new Stopwatch();

            stopWatch.Restart();
            isTimerRun = true;
            worker.RunWorkerAsync();


        }
        private void UpdateFunc(int ID, object? order, object? oldStatus, object? newStatus, object? beginTime, object? finishTime)
        {
            var t = new Tuple<int, object?, object?, object?, object?, object?>(ID, order, oldStatus, newStatus, beginTime, finishTime);
            worker.ReportProgress(0, t);
        }
        private void StopFunc()
        {
            _myClosing = true;
            Simulator.Simulator.Update -= UpdateFunc;
            Simulator.Simulator.Stop -= StopFunc;
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
            if (e.ProgressPercentage == 0) // it means we need to update the order
            {
                var tuple = ((Tuple<int, object?, object?, object?, object?, object?>)e.UserState);
                //CurrentID = tuple.Item1.ToString();
                  lab1.Content = tuple.Item1.ToString();
                //OldStat = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item3).ToString(), ((DateTime)tuple.Item5).ToString("hh/mm/ss"));
                  lab2.Content = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item3).ToString(), ((DateTime)tuple.Item5).ToString());
                //NewStat = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item4).ToString(), ((DateTime)tuple.Item6).ToString("hh/mm/ss"));
                 lab3.Content = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item4).ToString(), ((DateTime)tuple.Item6).ToString());
                System.Threading.Thread.Sleep(1000);
            }
            else if (e.ProgressPercentage == 1) // it means we need to update the clock
            {
                string timerText = stopWatch.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);
                this.timerTextBlock.Text = timerText;
            }
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Simulator.Simulator.StopSimulation();
            this.Close();
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



//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using System.Windows.Threading;
//using Simulator;

//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for SimulatorWindow.xaml
//    /// </summary>
//    public partial class SimulatorWindow : Window
//    {
//        bool _myClosing = false;
//        private Stopwatch stopWatch;
//        private bool isTimerRun;
//        public string CurrentID { get { return (string)GetValue(CurrentIDProperty); } set { SetValue(CurrentIDProperty, value); } }
//        public static readonly DependencyProperty CurrentIDProperty = DependencyProperty.Register("currentID", typeof(string), typeof(SimulatorWindow));
//        public string OldStat { get { return (string)GetValue(OldStatProperty); } set { SetValue(OldStatProperty, value); } }
//        public static readonly DependencyProperty OldStatProperty = DependencyProperty.Register("oldStat", typeof(string), typeof(SimulatorWindow));
//        public string NewStat { get { return (string)GetValue(NewStatProperty); } set { SetValue(NewStatProperty, value); } }
//        public static readonly DependencyProperty NewStatProperty = DependencyProperty.Register("newStat", typeof(string), typeof(SimulatorWindow));
//        // public string OldStat { get; set; }
//        // public string NewStat { get; set; }
//        BackgroundWorker worker;
//        BackgroundWorker timeWorker;
//        public SimulatorWindow()
//        {
//            CurrentID = "";
//            OldStat = "";
//            NewStat = "";
//            InitializeComponent();
//            worker = new BackgroundWorker();
//            worker.DoWork += Worker_DoWork;
//            worker.ProgressChanged += Worker_ProgressChanged;
//            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

//            worker.WorkerReportsProgress = true;
//            worker.WorkerSupportsCancellation = true;

//            timeWorker = new BackgroundWorker();
//            timeWorker.DoWork += timeWorker_DoWork;
//            timeWorker.ProgressChanged += timeWorker_ProgressChanged;
//            timeWorker.RunWorkerCompleted += timeWorker_RunWorkerCompleted;

//            timeWorker.WorkerReportsProgress = true;
//            timeWorker.WorkerSupportsCancellation = true;


//            stopWatch = new Stopwatch();

//            stopWatch.Restart();
//            isTimerRun = true;
//            worker.RunWorkerAsync();
//            //timeWorker.RunWorkerAsync();


//        }
//        private void UpdateFunc(int ID, object? order, object? oldStatus, object? newStatus, object? beginTime, object? finishTime)
//        {
//            var t = new Tuple<int, object?, object?, object?, object?, object?>(ID, order, oldStatus, newStatus, beginTime, finishTime);
//            worker.ReportProgress(0, t);
//        }
//        private void StopFunc()
//        {
//            _myClosing = true;
//            Simulator.Simulator.Update -= UpdateFunc;
//            Simulator.Simulator.Stop -= StopFunc;
//        }

//        private void timeWorker_DoWork(object sender, DoWorkEventArgs e)
//        {
//            while (!isTimerRun)
//            {
//                worker.ReportProgress(1);
//                System.Threading.Thread.Sleep(1000);
//            }
//        }
//        private void timeWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        {
//            string timerText = stopWatch.Elapsed.ToString();
//            timerText = timerText.Substring(0, 8);
//            this.timerTextBlock.Text = timerText;

//        }
//        private void timeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {
//       //     timeWorker.CancelAsync();

//        }

//        private void Worker_DoWork(object sender, DoWorkEventArgs e)
//        {
//            // metodot mashkifot
//            Simulator.Simulator.Update += UpdateFunc;
//            Simulator.Simulator.Stop += StopFunc;


//            Simulator.Simulator.RunSimulation();

//        }
//        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        {
//            var tuple = ((Tuple<int, object?, object?, object?, object?, object?>)e.UserState);
//            CurrentID = tuple.Item1.ToString();
//            //  lab1.Content = tuple.Item1.ToString();
//            OldStat = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item3).ToString(), ((DateTime)tuple.Item5).ToString("hh/:mm/:ss"));
//            //  lab2.Content = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item3).ToString(), ((DateTime)tuple.Item5).ToString("hh/:mm/:ss"));
//            NewStat = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item4).ToString(), ((DateTime)tuple.Item6).ToString("hh/:mm/:ss"));
//            // lab3.Content = String.Format(@"{0}, The time of the begining: {1}", ((BO.Enums.Status)tuple.Item4).ToString(), ((DateTime)tuple.Item6).ToString("hh/:mm/:ss"));
//            System.Threading.Thread.Sleep(1000);


//        }
//        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {

//            Simulator.Simulator.StopSimulation();
//            this.Close();
//        }


//        // Example for avoiding closing the window...
//        void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
//        {
//            if (!_myClosing) // Won't allow to cancel the window!!! It is not me!!!
//            {
//                e.Cancel = true;
//                MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
//            }
//        }

//        private void CloseButton_Click(object sender, RoutedEventArgs e)
//        {
//            worker.CancelAsync();
//        }
//    }
//}
