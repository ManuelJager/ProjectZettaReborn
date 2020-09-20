using System;
using Zetta.MVVM.Core;
using UnityEngine;

namespace Zetta.MVVM
{
    /// <summary>
    /// Monobehaviour implementantion for a view component
    /// Set OnUpdate to handle data presentation
    /// </summary>
    public abstract class MonoViewBase<TModel, TViewModel> : MonoBehaviour, IView<TModel, TViewModel>, IInitializable<TViewModel>
        where TModel : IModel
        where TViewModel : IViewModel<TModel>
    {
        public Action<TViewModel> OnUpdate { get; protected set; }
        public TViewModel viewModel { get; protected set; }

        // Call after before referencing viewModel from anywhere
        public void Initialize(TViewModel value)
        {
            viewModel = value;
        }

        public void PerformUpdate()
        {
            OnUpdate?.Invoke(viewModel);
        }
    }
}