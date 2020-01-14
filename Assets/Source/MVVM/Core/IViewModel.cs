using System;

namespace Zetta.MVVM.Core
{
    public interface IViewModel<TModel, TModelImplementation> : IMVVMComponent
        where TModel : TModelImplementation
        where TModelImplementation : IModel
    {
        Action<TModel> OnUpdate { get; }
    }
}