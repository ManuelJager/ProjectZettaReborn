using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zetta.Background
{
    public class BackgroundLayer
    {
        public float parallaxSpeed;
        public float starDensity;
        public Vector2 brightnessRange;
        public GameObject gameObject;

        public BackgroundLayer(float parallaxSpeed, float starDensity, Vector2 brightnessRange)
        {
            this.parallaxSpeed = parallaxSpeed;
            this.starDensity = starDensity;
            this.brightnessRange = brightnessRange;
        }

        public void SetGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        } 
    }
}
