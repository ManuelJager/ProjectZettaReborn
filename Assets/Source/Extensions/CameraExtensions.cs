using UnityEngine;

namespace Zetta.Extensions
{
    public static class CameraExtensions
    {
        public static Camera GetMainOrthgraphicCamera()
        {
            return GameObject.FindWithTag("OrthographicMainCamera").GetComponent<Camera>();
        }
    }
}