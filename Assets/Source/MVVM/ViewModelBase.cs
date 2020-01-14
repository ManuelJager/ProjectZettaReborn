using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetta.MVVM.Core;

namespace Zetta.MVVM
{

    /// <summary>
    /// Standard implementantion for a view model component
    /// Set OnUpdate to handle to create data ready for presentation
    /// </summary>
    public abstract class ViewModelBase<TModel> : IViewModel<TModel, ModelBase>
        where TModel : ModelBase
    {
        public delegate void UpdateViewModelDelegate();

        public event UpdateViewModelDelegate UpdateViewModel;

        public Action<TModel> OnUpdate { get; protected set; }

        public TModel model { get; }

        protected ViewModelBase(TModel model)
        {
            this.model = model;
        }

        public void PerformUpdate()
        {
            OnUpdate?.Invoke(model);
            UpdateViewModel?.Invoke();
        }
    }
}
