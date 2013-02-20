using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mischief.Plots;

namespace Mischief
{
    class Program
    {
        static void Main(string[] args)
        {
            //RandomPlot();
            new WorkstationLocker().Plot();
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
            IPlot mischief = (IPlot)o;
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
                IPlot mischief = (IPlot)o;
                mischief.Plot();
            }
        }

    }
}
