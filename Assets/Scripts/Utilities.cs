using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static bool IsPlayer(GameObject gameObject) {
        return gameObject.CompareTag("Player") || gameObject.name == "PlayerMeshContainer";
    }

    public static void LookAt(Transform source, Transform target)
    {
        var angleAboutY = Utilities.AngleAroundY(source, target);
        source.rotation = Quaternion.Euler(source.eulerAngles.x, (float)angleAboutY, source.eulerAngles.z);
    }
    public static double AngleAroundY(Transform source, Transform target)
    {
        var a = target.position.z - source.position.z;
        var o = target.position.x - source.position.x;
        return Math.Atan2(o, a) * (180 / Math.PI);
    }

}
