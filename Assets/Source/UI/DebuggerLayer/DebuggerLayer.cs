using UnityEngine;
using UnityEngine.UI;

namespace Zetta.UI.Controllers
{
    public class DebuggerLayer : MonoBehaviour
    {
        public Button loadBlueprintButton;
        public InputField loadBlueprintInputField;
        public Button loadDefaultBlueprintButton;
        public Button setMaxPowerButton;
        public InputField setMaxPowerInputField;
        public Button setMaxIntegrityButton;
        public InputField setMaxIntegrityInputField;

        public void Awake()
        {
            loadBlueprintButton.onClick.AddListener(SpawnShip);
            setMaxPowerButton.onClick.AddListener(SetMaxPower);
            setMaxIntegrityButton.onClick.AddListener(SetMaxIntegrity);
            loadDefaultBlueprintButton.onClick.AddListener(SpawnDefaultShip);
        }

        public void SpawnShip()
        {
            Debugger.SpawnShipByName(loadBlueprintInputField.text);
        }

        public void SetMaxPower()
        {
            Debugger.SetMaxPower(setMaxPowerInputField.text);
        }

        public void SetMaxIntegrity()
        {
            Debugger.SetMaxIntegrity(setMaxIntegrityInputField.text);
        }

        public void SpawnDefaultShip()
        {
            Debugger.SpawnShipByName("Default Ship");
        }
    }
}