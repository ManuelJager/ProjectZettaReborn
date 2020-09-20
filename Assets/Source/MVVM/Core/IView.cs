using System;

namespace Zetta.MVVM.Core
{
    public interface IView<TModel, TViewModel> : IMVVMComponent
        where TModel : IModel
        where TViewModel : IViewModel<TModel>
    {
        TViewModel viewModel { get; }
        Action<TViewModel> OnUpdate { get; }
    }
}