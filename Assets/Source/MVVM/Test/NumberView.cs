using UnityEngine;
using UnityEngine.UI;

namespace Zetta.MVVM.Test
{
    public class NumberView : ViewBase<NumberModel, NumberViewModel>
    {
        public Text text;

        public NumberView(NumberViewModel viewModel)
            : base(viewModel)
        {
            OnUpdate = (NumberViewModel updatedViewModel) =>
            {
                Debug.Log(updatedViewModel.message);
            };
            PerformUpdate();
        }
    }
}