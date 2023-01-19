using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public static class Simulator
    {
        private static volatile bool isRunning = true;
        private static volatile bool stopRequested = false;
        private static Random random = new Random();
        private static Thread simulationThread;
        public delegate void SimulationEvent();
        public static event SimulationEvent OnSimulationCompleted;
        public static event SimulationEvent OnObjectUpdate;
        public static event SimulationEvent OnSimulationStopped;
        static BlApi.IBl? bl = BlApi.Factory.Get();

        public static void RunSimulation()
        {
            simulationThread = new Thread(() =>
            {
                while (isRunning)
                {
                    // Request next treatment from logical layer
                    var treatment = bl.Order.idOrderUpdateNow();
                    if (treatment == null)
                    {
                        // Wait for a moment before checking again
                        Thread.Sleep(1000);
                        continue;
                    }
                    var order = bl.Order.GetOrderManager((int)treatment);
                    // Report treatment to the presentation layer
                    OnObjectUpdate?.Invoke();
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
                    // Calculate treatment time
                    int treatmentTime = 3 + (int)(7 * random.NextDouble());
                    Thread.Sleep(treatmentTime * 1000);

                    // Report treatment completion
                    OnSimulationCompleted?.Invoke();

                    // Request status update from BL layer
                    bl.Order.UpdateStatus((int)treatment);
                    Thread.Sleep(1000);
                    //Check if stop is requested
                    if (stopRequested)
                    {
                        isRunning = false;
                        OnSimulationStopped?.Invoke();
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

        public static void RegisterOnSimulationCompleted(SimulationEvent method)
        {
            OnSimulationCompleted += method;
        }

        public static void UnregisterOnSimulationCompleted(SimulationEvent method)
        {
            OnSimulationCompleted -= method;
        }

        public static void RegisterOnObjectUpdate(SimulationEvent method)
        {
            OnObjectUpdate += method;
        }

        public static void UnregisterOnObjectUpdate(SimulationEvent method)
        {
            OnObjectUpdate -= method;
        }

        public static void RegisterOnSimulationStopped(SimulationEvent method)
        {
            OnSimulationStopped += method;
        }

        public static void UnregisterOnSimulationStopped(SimulationEvent method)
        {
            OnSimulationStopped -= method;
        }

    }
}
