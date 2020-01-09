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
        public static BackgroundManager Instance;

        public Color backgroundColor;
        public GameObject defaultBackgroundLayer;
        public int backgroundChunkSize = 500;

        public GameObject background;

        private List<BackgroundLayer> layers;

        public BackgroundManager()
        {
            Instance = this;
            
            layers = new List<BackgroundLayer>();
            layers.Add(new BackgroundLayer(1f, 0.00075f, new Vector2(0.15f, 0.2f)));
            layers.Add(new BackgroundLayer(2f, 0.00060f, new Vector2(0.1f, 0.15f)));
            layers.Add(new BackgroundLayer(3f, 0.0005f, new Vector2(0.075f, 0.1f)));
            layers.Add(new BackgroundLayer(4f, 0.00025f, new Vector2(0.05f, 0.15f)));

        }

        public void UpdateParallax(Vector2 vector)
        {
            //for(int i = 0; i < layers.Count; i++)
            //{
            //    BackgroundLayer layer = layers[i];
            //    GameObject layerObject = layer.gameObject;
            //    if(layerObject != null)
            //    {
            //        MeshRenderer meshRenderer = layerObject.GetComponent<MeshRenderer>();
            //        meshRenderer.material.mainTextureOffset += new Vector2(layer.parallaxSpeed, 0f);
            //    }
            //}
        }

        public void Start()
        {
            //layerObject.AddComponent<Texture>();

            InitializeLayer(layers[0]);
            InitializeLayer(layers[1]);
            InitializeLayer(layers[2]);
            InitializeLayer(layers[3]);
        }

        public void InitializeLayer(BackgroundLayer layer)
        {
            GameObject layerObject = Instantiate(defaultBackgroundLayer);
            MeshRenderer meshRenderer = layerObject.GetComponent<MeshRenderer>();
            meshRenderer.material = CreateMaterial(CreateQuadTexture(layer), meshRenderer);

            Vector3 position = layerObject.transform.position;
            position.z = layers.IndexOf(layer) * 2 + 1;
            layerObject.transform.position = position;
            layer.gameObject = layerObject;
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
                    bool isStar = System.Math.Round((decimal)UnityEngine.Random.Range(0, 1 / layer.starDensity)) == 1;
                    if(texture.GetPixel(x, y) == startingColor)
                    {
                        texture.SetPixel(x, y, empty);
                    }
                    if (isStar)
                    {
                        int starSize = (int)System.Math.Round((decimal)UnityEngine.Random.Range(1, 3));
                        Color starColor = new Color(1, 1, 1);
                        starColor.a = UnityEngine.Random.Range(layer.brightnessRange.x, layer.brightnessRange.y);
                        for (int xs = 0; xs < starSize; xs++)
                        {
                            for (int ys = 0; ys < starSize; ys++)
                            {
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
