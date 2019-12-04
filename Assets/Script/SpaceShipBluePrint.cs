using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpaceShipBluePrint : ScriptableObject
{
    public List<C> components;

    [Serializable]
    public class C
    {
        public ComponentBluePrint bluePrint;
        public List<DirectionAndConnection> status;

        public C()
        {
            status = new List<DirectionAndConnection>() {
            new DirectionAndConnection(SpaceShipComponent.Direction.UP, ConnectionStatus.Status.AVAILABLE),
            new DirectionAndConnection(SpaceShipComponent.Direction.LEFT, ConnectionStatus.Status.AVAILABLE),
            new DirectionAndConnection(SpaceShipComponent.Direction.DOWN, ConnectionStatus.Status.AVAILABLE),
            new DirectionAndConnection(SpaceShipComponent.Direction.RIGHT, ConnectionStatus.Status.AVAILABLE)
            };
        }

        [Serializable]
        public class DirectionAndConnection
        {
            public DirectionAndConnection(SpaceShipComponent.Direction d = default, ConnectionStatus.Status s = default)
            {
                direction = d;
                status = s;
            }

            public SpaceShipComponent.Direction direction;
            public ConnectionStatus.Status status;
            public int componentIndex = -1;
        }
    }
}
