using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetta.MVVM.Core;

namespace Zetta.MVVM
{
    /// <summary>
    /// Standard implementantion for a model component base class
    /// Stores data not ready for presentation
    /// </summary>
    public abstract class ModelBase : IModel
    {
        public delegate void UpdateModelDelegate();

        public event UpdateModelDelegate UpdateModel;

        public void PerformUpdate()
        {
            UpdateModel?.Invoke();
        }
    }
}
