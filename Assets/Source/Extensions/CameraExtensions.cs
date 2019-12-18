using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class CameraExtensions
    {
        public static Camera GetMainOrthgraphicCamera()
        {
            return GameObject.FindWithTag("OrthographicMainCamera").GetComponent<Camera>();
        }
    }
}
