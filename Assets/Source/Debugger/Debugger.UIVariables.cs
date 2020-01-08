#pragma warning disable CS4014
using UnityEngine;
using Zetta.UI;

namespace Zetta
{
    public static partial class Debugger
    {
        private static bool drawChunkBorders = false;

        public static bool DrawChunkBorders {
            get => drawChunkBorders;
            set {
                // Give a notice
                var statusString = value ? "ON" : "OFF";
                NoticeManager.Instance.Prompt($"Drawing chunk borders is now {statusString}");

                DrawChunkBordersChanged?.Invoke(value);
                drawChunkBorders = value;
            }
        }

        public static void SetMaxPower(string input)
        {
            float value;
            if (float.TryParse(input, out value))
            {
                UIManager.Instance.gameplayLayer.statLayer.powerDrawBar.max = value;
            }
            else
            {
                Debug.LogWarning("Invalid format - input float only");
            }
        }

        public static void SetMaxIntegrity(string input)
        {
            float value;
            if (float.TryParse(input, out value))
            {
                UIManager.Instance.gameplayLayer.statLayer.integrityDrawBar.max = value;
            }
            else
            {
                Debug.LogWarning("Invalid format - input float only");
            }
        }
    }
}