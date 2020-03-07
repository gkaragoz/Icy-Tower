﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMap 
{
    public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget) {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

 
}

