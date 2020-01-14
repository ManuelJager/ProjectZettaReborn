namespace Zetta.MVVM.Test
{
    public class NumberViewModel : ViewModelBase<NumberModel>
    {
        public string message;

        public NumberViewModel(NumberModel model)
            : base(model)
        {
            OnUpdate = (NumberModel updatedModel) =>
            {
                this.message = $"Dit is een manier om {model.number} te printen";
            };
            PerformUpdate();
        }
    }
}