using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simulator
{
    public static class Simulator
    {
        private static volatile bool isRunning = true;
        private static volatile bool stopRequested = false;
        private static Random random = new Random();
        private static Thread simulationThread;
        private static event Action? stop;
        public static event Action? Stop
        {
            add => stop += value;
            remove =>stop-=value;
        }
        private static event Action<int, object?, object?, object?, object?, object?>? update;
        public static event Action<int, object?, object?, object?, object?, object?>? Update
        {
            add => update += value;
            remove => update -= value;
        }
        static BlApi.IBl? bl = BlApi.Factory.Get();

        public static void RunSimulation()
        {
            simulationThread = new Thread(() =>
            {
                while (isRunning)
                {
                    // Request next treatment from logical layer
                    int? treatment = bl.Order.idOrderUpdateNow();
                    if (treatment == null)
                    {
                        // Wait for a moment before checking again
                        Thread.Sleep(1000);
                        continue;
                    }
                    var order = bl.Order.GetOrderManager((int)treatment);
                    // Report treatment to the presentation layer
                    
                    BO.Enums.Status status = (BO.Enums.Status)order.OrderStatus;
                    BO.Enums.Status next;
                    if (status == BO.Enums.Status.Confirmed)
                    {
                        next = BO.Enums.Status.Sent;
                    }
                    else
                    {
                        next = BO.Enums.Status.Arrived;
                    }
                    int treatmentTime = 3 + (int)(7 * random.NextDouble());
                    update?.Invoke(order.ID, order,status,next,DateTime.Now.Second,DateTime.Now.Second+treatmentTime);
                    // Calculate treatment time
                    Thread.Sleep(treatmentTime * 1000);


                    // Request status update from BL layer
                    bl.Order.UpdateStatus((int)treatment);
                    Thread.Sleep(1000);
                    //Check if stop is requested
                    if (stopRequested)
                    {
                        isRunning = false;
                        stop?.Invoke();
                    }
                }
            });
            simulationThread.Start();
        }

        public static void StopSimulation()
        {
            stopRequested = true;
            simulationThread.Join();
        }
    }
}
