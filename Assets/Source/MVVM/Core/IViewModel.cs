using System;

namespace Zetta.MVVM.Core
{
    public interface IViewModel<TModel> : IMVVMComponent, IUpdateEventProvider
        where TModel : IModel
    {
        TModel model { get; }
        Action<TModel> OnUpdate { get; }
    }
}