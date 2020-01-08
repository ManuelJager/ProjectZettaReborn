using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zetta.Controllers
{
    public partial class PlayerController
    {
        public delegate void PlayerStartAcceleratingDelegate(Vector2 acceleration);
        public event PlayerStartAcceleratingDelegate PlayerStartAccelerating;

        public delegate void PlayerStoppedAcceleratingDelegate();
        public event PlayerStoppedAcceleratingDelegate PlayerStoppedAccelerating;
    }
}
