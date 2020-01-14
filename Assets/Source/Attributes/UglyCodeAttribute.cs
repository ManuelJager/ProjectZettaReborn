using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Callbacks;

namespace Zetta.Attributes
{

    [AttributeUsage(AttributeTargets.Method)]
    public class UglyCodeAttribute : Attribute
    {
    }
}
