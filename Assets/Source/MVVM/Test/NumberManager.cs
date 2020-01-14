using UnityEngine;

namespace Zetta.MVVM.Test
{
    public class NumberManager : MonoBehaviour
    {
        private NumberModelController numberModels;
        private NumberViewModelController numberViewModels;
        private NumberViewController numberViews;

        public void Awake()
        {
            // Create controllers
            numberModels = new NumberModelController();
            numberViewModels = new NumberViewModelController();
            numberViews = new NumberViewController();

            // Add controllers
            numberModels.Connect(numberViewModels).Connect(numberViews);

            // Only models need to be created
            var model = new NumberModel();

            // Add model
            numberModels.Add(model);

            // Update model, and manually update the views
            model.number = 3;
            model.PerformUpdate();

            // Update model and let model call PerformUpdate on itself
            model.SetNumber(5);
        }
    }

    public class NumberModelController : ModelControllerBase<NumberModel>
    {
    }

    public class NumberViewModelController : ViewModelControllerBase<NumberModel, NumberViewModel>
    {
        public NumberViewModelController()
            : base(null)
        {
            viewModelFactory = (NumberModel) =>
            {
                return new NumberViewModel(NumberModel);
            };
        }
    }

    public class NumberViewController : ViewControllerBase<NumberModel, NumberViewModel, NumberView>
    {
        public NumberViewController()
            : base(null, null)
        {
            viewFactory = (NumberViewModel viewModel) =>
            {
                return new NumberView(viewModel);
            };
            destructor = (NumberView view) =>
            {
                Debug.Log($"The value was : {view.viewModel.message} when it was destroyed");
            };
        }
    }
}