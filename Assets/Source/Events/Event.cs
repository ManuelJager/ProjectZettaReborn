using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public abstract class Event : Attribute
    {
        // The name of the event
        public abstract string Name();
    }
}
