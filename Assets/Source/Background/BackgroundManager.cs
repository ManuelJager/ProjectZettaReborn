using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.Generics;

namespace Zetta.Background
{
    public class BackgroundManager : MonoBehaviour
    {
        public Color backgroundColor;
        public GameObject defaultBackgroundLayer;
        public int backgroundChunkSize = 500;

        public GameObject background;

        private List<BackgroundLayer> layers;

        public BackgroundManager()
        {
            layers = new List<BackgroundLayer>();

            layers.Add(new BackgroundLayer(1, 0.005f, new Vector2(0.5f, 0.8f)));
        }
        
        public void Start()
        {
            //layerObject.AddComponent<Texture>();

            InitializeLayer(layers[0]);
        }
        
        public void InitializeLayer(BackgroundLayer layer)
        {
            GameObject layerObject = Instantiate(defaultBackgroundLayer);
            MeshRenderer meshRenderer = layerObject.GetComponent<MeshRenderer>();
            meshRenderer.material = CreateMaterial(CreateQuadTexture(layer), meshRenderer);
        }



        private Texture2D CreateQuadTexture(BackgroundLayer layer)
        {
            Texture2D texture = new Texture2D(backgroundChunkSize, backgroundChunkSize);
            Color startingColor = texture.GetPixel(0, 0);

            Color empty = new Color(0, 0, 0, 0);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    //texture.SetPixel(x, y, Color.black);

                    bool isStar = System.Math.Round((decimal)UnityEngine.Random.Range(0, 1 / layer.starDensity)) == 1;
                    if(texture.GetPixel(x, y) == startingColor)
                    {
                        texture.SetPixel(x, y, empty);
                    }
                    if (isStar)
                    {
                        int starSize = (int)System.Math.Round((decimal)UnityEngine.Random.Range(1, 3));
                        //int starSize = 5;
                        Color starColor = new Color(1, 1, 1, UnityEngine.Random.Range(layer.brightnessRange.x, layer.brightnessRange.y));

                        for (int xs = 0; xs < starSize; xs++)
                        {
                            for (int ys = 0; ys < starSize; ys++)
                            {
                                texture.SetPixel(x, y, empty);
                                texture.SetPixel(x + xs, y + ys, starColor);
                            }
                        }
                    }
                }
            }
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Repeat;
            texture.Apply(true, true);
            return texture;
        }

        private Material CreateMaterial(Texture2D texture, MeshRenderer meshRenderer)
        {
            Material material = new Material(meshRenderer.material);
            material.mainTexture = texture;
            material.name += " (layer)";
            return material;
        }

    }
}
