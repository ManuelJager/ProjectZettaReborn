namespace Zetta.MVVM.Core
{
    // Spec definition for managers to handle binding of mvvmcomponents
    public interface ILinkable<T>
        where T : IMVVMComponent
    {
        void Link(T item);

        void UnLink(T item);
    }
}