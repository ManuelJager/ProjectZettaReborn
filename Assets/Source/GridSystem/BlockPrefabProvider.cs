#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEngine;
using Zetta.Generics;
using Zetta.GridSystem.Blocks;
using Zetta.Exceptions;
using Zetta.MVVM;

namespace Zetta.GridSystem
{
    public class BlockPrefabProvider : AutoInstanceMonoBehaviour<BlockPrefabProvider>, IInitializeable
    {
        [SerializeField] private GameObject[] prefabObjects;

        public Dictionary<string, BlockPrefab> Dictionary { get; private set; } = new Dictionary<string, BlockPrefab>();

        public void Initialize()
        {
            base.Awake();
            foreach (var prefab in prefabObjects)
            {
                var component = (GridBlockBase)prefab.GetComponent(typeof(GridBlockBase));
                var id = component.BlockTypeID;
                var blockPrefab = new BlockPrefab(prefab, component);
                Dictionary[id] = blockPrefab;
            }
            Blueprints.RuntimeValues.Initialize();
        }

        public GameObject GetPrefab(string id)
        {
            try
            {
                return Dictionary[id].prefab;
            }
            catch
            {
                throw new PrefabNotFoundException();
            }
        }

        public GridBlockBase GetGridBlockBase(string id)
        {
            try
            {
                return Dictionary[id].gridBlockBase;
            }
            catch
            {
                throw new PrefabNotFoundException();
            }
        }
    }

    public struct BlockPrefab
    {
        public GameObject prefab { get; }
        public GridBlockBase gridBlockBase { get; }

        public BlockPrefab(GameObject prefab, GridBlockBase gridBlockBase)
        {
            this.prefab = prefab;
            this.gridBlockBase = gridBlockBase;
        }
    }
}