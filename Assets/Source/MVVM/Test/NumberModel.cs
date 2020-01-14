namespace Zetta.MVVM.Test
{
    public class NumberModel : ModelBase
    {
        public int number = 5;

        public void SetNumber(int number)
        {
            this.number = number;
            PerformUpdate();
        }
    }
}