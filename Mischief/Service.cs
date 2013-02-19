using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Mischief.Plots;
using System.Timers;

namespace Mischief
{
    public partial class Service : ServiceBase
    {
        private static Timer mischiefTimer;
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: log
            // start a timer, to run a random plot at the specified timer interval
            mischiefTimer = new Timer(60000);

            // the random plot will be run by the timer event handler
            mischiefTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            mischiefTimer.Enabled = true;

            // disable garbage collection for the timer
            GC.KeepAlive(mischiefTimer);
        }

        protected override void OnStop()
        {
            // TODO: log
        }


        /// <summary>
        /// Using the current assembly, get a list of classes that implement the T type (ie. IPlot interface) 
        /// </summary>
        /// <typeparam name="T">The type (ie. interface) to search for in the program</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetPlots<T>()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                   .Where(type => typeof(T).IsAssignableFrom(type))
                   .Where(type => type.FullName != typeof(T).FullName);
        }


        /// <summary>
        /// Launches 1 random mischief attack from the list of Plot classes in the program
        /// </summary>
        static void RandomPlot()
        {
            var plots = GetPlots<IPlot>();
            var plot = plots.ElementAt(new Random().Next(plots.Count()));

            // run the random plot
            Object o = Activator.CreateInstance(plot);
            IPlot mischief = (IPlot) o;
            mischief.Plot();
        }


        /// <summary>
        /// Unleash a full/largescale attack. Launch all mischief plots (declare war on enemy) one after another
        /// </summary>
        static void Nuke()
        {
            var plots = GetPlots<IPlot>();
            foreach (Type plot in plots)
            {
                // run the current plot
                Object o = Activator.CreateInstance(plot);
                IPlot mischief = (IPlot) o;
                mischief.Plot();
            }
        }


        /// <summary>
        /// OnDebug will call the protected OnStart when debugging the service
        /// </summary>
        public void OnDebug()
        {
            OnStart(null);
        }


        /// <summary>
        /// Event handler for the timer (when elapsed event is raised by mischiefTimer)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // When the timer event fires.. run a random plot
            RandomPlot();
        }
    }
}
