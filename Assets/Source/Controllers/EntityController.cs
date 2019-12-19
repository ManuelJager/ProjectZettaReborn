using Blueprints;
using GridSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    // Singleton instance
    public static EntityController Instance;

    public EntityController()
    {
        // The set instance of the singleton
        Instance = this;
    }

    public PlayerEntity InstantiatePlayer(Blueprint bp, Vector2 position)
    {
        var playerBase = GameManager.Instance.playerBasePrefab;
        var playerObject = Instantiate(playerBase);
        playerObject.transform.position = position;

        var playerEntity = playerObject.GetComponent<PlayerEntity>();
        playerEntity.InstantiateShip(bp);

        CameraController.Instance.StartLerpZoom(playerEntity.Size);
        return playerEntity;
    }

    public PlayerEntity InstantiatePlayer(Blueprint bp)
    {
        return InstantiatePlayer(bp, new Vector2(0, 0));
    }


    public ShipEntity InstantiateShip(Blueprint bp, Vector2 position)
    {
        var shipBase = GameManager.Instance.shipBasePrefab;
        var shipObject = Instantiate(shipBase);
        shipObject.transform.position = position;

        var shipEntity = shipObject.GetComponent<ShipEntity>();
        shipEntity.InstantiateShip(bp);

        return shipEntity;
    }


}

