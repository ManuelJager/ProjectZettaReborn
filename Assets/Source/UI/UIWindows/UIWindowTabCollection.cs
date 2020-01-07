using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Zetta.UI.UIWindows
{
    public class UIWindowTabCollection : List<UIWindowTabContent>
    {
        private UIWindowTabManager manager;

        public UIWindowTabCollection(UIWindowTabManager manager) 
        {
            this.manager = manager;
        }
    }
}