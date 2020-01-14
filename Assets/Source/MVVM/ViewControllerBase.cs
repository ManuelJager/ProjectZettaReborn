using System;
using System.Collections.Generic;
using Zetta.MVVM.Core;

namespace Zetta.MVVM
{
    public abstract class ViewControllerBase<TModel, TViewModel, TView> : ILinkable<TViewModel>
        where TModel : ModelBase
        where TViewModel : ViewModelBase<TModel>
        where TView : ViewBase<TModel, TViewModel>
    {
        private Dictionary<TViewModel, TView> linkDictionary =
            new Dictionary<TViewModel, TView>();

        // view generator, should handle instantiating
        public Func<TViewModel, TView> viewFactory;

        // view destroyer, should handle Destroy() or any other effects like fading
        public Action<TView> destructor;

        protected ViewControllerBase(Func<TViewModel, TView> viewFactory, Action<TView> destructor)
        {
            this.viewFactory = viewFactory;
            this.destructor = destructor;
        }

        // Couple viewmodel to model
        public virtual void Link(TViewModel viewModel)
        {
            // Create and get view
            var view = viewFactory(viewModel);

            // Set bindings
            viewModel.UpdateViewModel += view.PerformUpdate;
            linkDictionary[viewModel] = view;
        }

        // Decouple viewmodel from model
        public virtual void UnLink(TViewModel viewModel)
        {
            // Get view
            var view = linkDictionary[viewModel];

            // Remove bindings and destroy
            viewModel.UpdateViewModel -= view.PerformUpdate;
            linkDictionary.Remove(viewModel);
            destructor(view);
        }
    }
}