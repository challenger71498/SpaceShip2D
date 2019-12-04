using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionStatus
{
    public enum Status
    {
        UNKNOWN, AVAILABLE, OCCUPIED, UNAVAILABLE
    }

    public Status status;
    public SpaceShipComponent component;
    public FixedJoint2D joint;

    public ConnectionStatus(Status s = default, SpaceShipComponent c = default, FixedJoint2D j = default)
    {
        status = s;
        component = c;
        joint = j;
    }
}
