using System.Collections.Generic;
using Zetta.MVVM.Core;

namespace Zetta.MVVM
{
    public abstract class ModelControllerBase<TModel> : List<TModel>
        where TModel : IModel
    {
        // Cast to ViewModelManagerBase<TModel, TViewModel, TView>
        private List<ILinkable<TModel>> viewModelManagers =
            new List<ILinkable<TModel>>();

        // Add model to controller. 
        // This links the model to all view model managers
        public new void Add(TModel model)
        {
            foreach (var viewModelManager in viewModelManagers)
            {
                viewModelManager.Link(model);
            }
            base.Add(model);
        }

        public new void Remove(TModel model)
        {
            foreach (var viewModelManager in viewModelManagers)
            {
                viewModelManager.UnLink(model);
            }
            base.Remove(model);
        }

        // Connect ViewModelManager to this
        // Calling this will make sure models are fed to the ViewModelManager
        public TViewModelManager Connect<TViewModelManager>(TViewModelManager viewModelManager)
            where TViewModelManager : ILinkable<TModel>
        {
            viewModelManagers.Add(viewModelManager);
            foreach (var model in this)
            {
                viewModelManager.Link(model);
            }
            return viewModelManager;
        }

        // Disconnect ViewModelManager to this
        // Once disconnected all relations with the ViewModelManager and the viewManagers under it, are stopped
        public void Disconnect<TViewModelManager>(TViewModelManager viewModelManager)
            where TViewModelManager : ILinkable<TModel>
        {
            viewModelManagers.Remove(viewModelManager);
            foreach (var model in this)
            {
                viewModelManager.UnLink(model);
            }
        }

        public void DisconnectFromAll<TViewModelManager>()
            where TViewModelManager : ILinkable<TModel>
        {
            foreach (var obj in viewModelManagers)
            {
                Disconnect((TViewModelManager)obj);
            }
        }
    }
}