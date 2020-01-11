using System;
using UnityEngine;
using Zetta.Exceptions;

namespace Zetta.GridSystem.Blueprints
{
    public partial class BlueprintManager
    {
        public static void AddDefaultShipToLoadedBlueprints()
        {
            try
            {
                blueprints.Add(Import(DEFAULT_BLUEPRINT));
            }
            catch (DuplicateBlueprintException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}