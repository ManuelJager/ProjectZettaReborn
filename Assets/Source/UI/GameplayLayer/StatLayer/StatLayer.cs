using UnityEngine;
using Zetta.UI.Controllers.ValueDisplayers;

namespace Zetta.UI.Controllers
{
    public class StatLayer : MonoBehaviour
    {
        public PowerDrawBar powerDrawBar;
        public IntegrityDrawBar integrityDrawBar;
        public SpeedDisplayer speedDisplayer;
        public BatteryDisplayer batteryDisplayer;
        public RemainingFuelDisplayer remainingFuelDisplayer;
    }
}