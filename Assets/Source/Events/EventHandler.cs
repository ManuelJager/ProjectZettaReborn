using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class EventHandler
    {
        /// <summary>
        /// Fires an event
        /// </summary>
        /// <param name="e">The event to fire</param>
        public static void Fire(Event e)
        {
            // Searches for all methods with the specified event type as attribute
            var methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass)
                .SelectMany(x => x.GetMethods())
                .Where(x => x.GetCustomAttributes(e.GetType(), false).FirstOrDefault() != null);

            foreach (var method in methods)
            {
                // Instantiates the listener
                var listener = Activator.CreateInstance(method.DeclaringType);

                // Fires the event handle method with the event as argument
                method.Invoke(listener, new object[1] { e });
            }
        }
    }
}
