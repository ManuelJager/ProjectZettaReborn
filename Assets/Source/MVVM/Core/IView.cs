using System;

namespace Zetta.MVVM.Core
{
    public interface IView<TModel, TModelImplementation, TViewModel, TViewModelImplentation> : IMVVMComponent
        where TModel : TModelImplementation
        where TModelImplementation : IModel
        where TViewModel : TViewModelImplentation
        where TViewModelImplentation : IViewModel<TModel, TModelImplementation>
    {
        Action<TViewModel> OnUpdate { get; }
    }
}