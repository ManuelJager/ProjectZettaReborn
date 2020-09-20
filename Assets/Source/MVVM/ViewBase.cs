using System;
using Zetta.MVVM.Core;

namespace Zetta.MVVM
{
    /// <summary>
    /// Standard implementantion for a view component
    /// Set OnUpdate to handle data presentation
    /// </summary>
    public abstract class ViewBase<TModel, TViewModel> : IView<TModel, TViewModel>
        where TModel : IModel
        where TViewModel : IViewModel<TModel>
    {
        public Action<TViewModel> OnUpdate { get; protected set; }
        public TViewModel viewModel { get; }

        protected ViewBase(TViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public void PerformUpdate()
        {
            OnUpdate?.Invoke(viewModel);
        }
    }
}