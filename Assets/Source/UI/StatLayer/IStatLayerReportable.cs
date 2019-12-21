using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IStatLayerReportable
{
    float value { get; set; }
    float displayValue { get; set; }
    float max { get; set; }
    float displayMax { get; set; }
    float multiplier { get; set; }
    float step { get; set; }
    void RefreshDisplayFields();
    void UpdateBar(float displayValue, float displayMax);
}
