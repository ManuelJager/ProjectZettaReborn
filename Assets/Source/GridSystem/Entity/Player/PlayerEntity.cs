using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace GridSystem.Entity
{
    public class PlayerEntity : ShipEntity
    {
        private Quaternion quaternion;
        private Camera orthographicCamera;

        public void Awake()
        {
            InputManager.InputAxis += OnAxis;
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Start()
        {
            orthographicCamera = CameraExtensions.GetMainOrthgraphicCamera();
            orthographicCamera.enabled = false;
        }

        public void Update()
        {
            quaternion = GridUtilities.GetMouseWorldPos(transform, orthographicCamera);
            transform.rotation = GridUtilities.MouseLookAtRotation(transform, 200);
        }

        public void OnAxis(Vector2 input)
        {
            var inputRotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            inputRotation += quaternion.eulerAngles.z + 270f;
            inputRotation %= 360f;
            input = GridUtilities.DegreeToVector2(inputRotation);
            rigid.AddForce(input);
        }
    }
}
