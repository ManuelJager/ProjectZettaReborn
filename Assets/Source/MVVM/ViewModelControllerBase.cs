using System;
using System.Collections.Generic;
using Zetta.MVVM.Core;

namespace Zetta.MVVM
{
    public abstract class ViewModelControllerBase<TModel, TViewModel> : List<TViewModel>, ILinkable<TModel>
        where TModel : IModel
        where TViewModel : IViewModel<TModel>
    {
        private Dictionary<TModel, TViewModel> linkDictionary =
            new Dictionary<TModel, TViewModel>();

        private List<ILinkable<TViewModel>> viewManagers =
            new List<ILinkable<TViewModel>>();

        // viewModel generator, should handle
        protected Func<TModel, TViewModel> viewModelFactory;

        protected ViewModelControllerBase(Func<TModel, TViewModel> viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        // Set internal reference of model to given viewModel
        // Creates a view model
        public virtual void Link(TModel model)
        {
            // Get viewModel
            var viewModel = viewModelFactory(model);

            foreach (var viewManager in viewManagers)
            {
                viewManager.Link(viewModel);
            }

            // Set bindings
            model.Update += viewModel.PerformUpdate;
            linkDictionary[model] = viewModel;
            Add(viewModel);
        }

        // Remove internal reference of model to already given viewModel
        public virtual void UnLink(TModel model)
        {
            // Get viewModel
            var viewModel = linkDictionary[model];

            foreach (var viewManager in viewManagers)
            {
                viewManager.UnLink(viewModel);
            }

            // Remove bindings
            model.Update -= viewModel.PerformUpdate;
            linkDictionary.Remove(model);
            Remove(viewModel);
        }

        // Connect view manager to this
        public void Connect<TViewManager>(TViewManager viewManager)
            where TViewManager : ILinkable<TViewModel>
        {
            foreach (var viewModel in this)
            {
                viewManager.Link(viewModel);
            }
            viewManagers.Add(viewManager);
        }

        // Disconnect view manager to this
        public void Disconnect<TViewManager>(TViewManager viewManager)
            where TViewManager : ILinkable<TViewModel>
        {
            foreach (var viewModel in this)
            {
                viewManager.UnLink(viewModel);
            }
            viewManagers.Remove(viewManager);
        }

        internal void DisconnectFromAll<TViewManager>()
            where TViewManager : ILinkable<TViewModel>
        {
            foreach (var obj in viewManagers)
            {
                Disconnect((TViewManager)obj);
            }
        }
    }
}