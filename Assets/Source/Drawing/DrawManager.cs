using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.Generics;

namespace Zetta.Drawing
{
    public class DrawManager : LazySingleton<DrawManager>
    {


        [RuntimeInitializeOnLoadMethod]
        public static void EchoThis() => Echo();
    }
}
