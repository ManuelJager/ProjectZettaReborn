using System.Collections.Generic;
using UnityEngine;

namespace Zetta.InputWrapper
{
    public partial class InputManager
    {
        private static Dictionary<KeyCode, char> keyCodeCharMap = new Dictionary<KeyCode, char>
        {
            { KeyCode.A, 'a'},
            { KeyCode.B, 'b'},
            { KeyCode.C, 'c'},
            { KeyCode.D, 'd'},
            { KeyCode.E, 'e'},
            { KeyCode.F, 'f'},
            { KeyCode.G, 'g'},
            { KeyCode.H, 'h'},
            { KeyCode.I, 'i'},
            { KeyCode.J, 'j'},
            { KeyCode.K, 'k'},
            { KeyCode.L, 'l'},
            { KeyCode.M, 'm'},
            { KeyCode.N, 'n'},
            { KeyCode.O, 'o'},
            { KeyCode.P, 'p'},
            { KeyCode.Q, 'q'},
            { KeyCode.R, 'r'},
            { KeyCode.S, 's'},
            { KeyCode.T, 't'},
            { KeyCode.U, 'u'},
            { KeyCode.V, 'v'},
            { KeyCode.W, 'w'},
            { KeyCode.X, 'x'},
            { KeyCode.Y, 'y'},
            { KeyCode.Z, 'z'},
            { KeyCode.Space, ' '}
        };

        private static bool TryParseKeycode(KeyCode keyCode, out char result)
        {
            var contains = keyCodeCharMap.ContainsKey(keyCode);
            result = contains ? keyCodeCharMap[keyCode] : '\0';
            return contains;
        }
    }
}