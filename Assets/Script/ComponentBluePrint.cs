using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class ComponentBluePrint : ScriptableObject
{
    public enum ObjectID
    {
        UNKNOWN, CORE, BODY_1X1
    }

    public ObjectID id;
    public GameObject prefab;
    public int mass;
    public bool trillion = false;
}
