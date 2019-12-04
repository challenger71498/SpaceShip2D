using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceShipComponent : MonoBehaviour
{
    public enum Direction
    {
        UNKNOWN, UP, RIGHT, DOWN, LEFT
    }

    public Dictionary<Direction, ConnectionStatus> connectionStatus = new Dictionary<Direction, ConnectionStatus>();

    GameObject prefab;
    bool converted = false;

    public SpaceShipComponent()
    {
        connectionStatus = new Dictionary<Direction, ConnectionStatus>();
    }

    public SpaceShipComponent(Dictionary<Direction, ConnectionStatus> cs)
    {
        connectionStatus = cs;
    }


    /// <summary>
    /// Sets status for a direction with a connection status.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="s"></param>
    public void SetStatus(Direction d, ConnectionStatus s)
    {
        if (connectionStatus.ContainsKey(d))
        {
            connectionStatus[d] = s;
        }
        else
        {
            connectionStatus.Add(d, s);
        }
    }

    /// <summary>
    /// Gets status of a direction.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public ConnectionStatus GetStatus(Direction d)
    {
        if (connectionStatus.ContainsKey(d))
        {
            return connectionStatus[d];
        }
        else
        {
            connectionStatus.Add(d, new ConnectionStatus());
            return connectionStatus[d];
        }
    }

    /// <summary>
    /// Attaches a component to this.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="c"></param>
    public void AttachComponent(Direction d, SpaceShipComponent c)
    {
        ConnectionStatus status = GetStatus(d);

        if (status.status == ConnectionStatus.Status.AVAILABLE)
        {
            status.status = ConnectionStatus.Status.OCCUPIED;
            c.GetStatus(d).status = ConnectionStatus.Status.OCCUPIED;
            FixedJoint2D joint = status.joint = gameObject.AddComponent<FixedJoint2D>();    // New joint for this gameObject.
            joint.connectedBody = c.gameObject.GetComponent<Rigidbody2D>();
            status.component = c;

            Debug.LogError(c.gameObject.name);

            switch (d)
            {
                case Direction.UP:
                    c.transform.position = transform.position + Vector3.up * 4.8f;
                    break;
                case Direction.LEFT:
                    c.transform.position = transform.position + Vector3.left * 4.8f;
                    break;
                case Direction.RIGHT:
                    c.transform.position = transform.position + Vector3.right * 4.8f;
                    break;
                case Direction.DOWN:
                    c.transform.position = transform.position + Vector3.down * 4.8f;
                    break;
            }
        }
    }

    public SpaceShipComponent DetachComponent(Direction d)
    {
        ConnectionStatus status = GetStatus(d);

        if (status.status == ConnectionStatus.Status.OCCUPIED)
        {
            status.status = ConnectionStatus.Status.AVAILABLE;

            FixedJoint2D joint = GetComponents<FixedJoint2D>().ToList().Find(delegate (FixedJoint2D j)
            {
                return j.connectedBody.GetInstanceID() == status.joint.connectedBody.GetInstanceID();
            });

            if (joint != null)
            {
                Destroy(joint);
                return status.component;
            }
            return default;
        }
        return default;
    }

    public void ConvertBluePrint(ComponentBluePrint bp, SpaceShipBluePrint ssbp, List<GameObject> list)
    {
        if(!converted)
        {
            SpaceShipBluePrint.C component = ssbp.components.Find(delegate (SpaceShipBluePrint.C compare)
            {
                return compare.bluePrint.id == bp.id;
            });

            if (component != null)
            {
                prefab = bp.prefab;

                foreach (SpaceShipBluePrint.C.DirectionAndConnection directionAndConnection in component.status)
                {
                    SetStatus(directionAndConnection.direction, new ConnectionStatus(ConnectionStatus.Status.AVAILABLE));
                    if (directionAndConnection.status == ConnectionStatus.Status.OCCUPIED)
                    {
                        AttachComponent(directionAndConnection.direction, list[directionAndConnection.componentIndex].GetComponent<SpaceShipComponent>());
                    }
                }
            }

            converted = true;
        }
    }
}
