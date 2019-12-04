using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public SpaceShipBluePrint bluePrint;

    SpaceShipComponent core;
    List<GameObject> objectList;

    private void Start()
    {
        Initialize(bluePrint);
        core = objectList[0].GetComponent<SpaceShipComponent>();
    }

    public SpaceShip(SpaceShipBluePrint bp)
    {
        Initialize(bp);
        core = objectList[0].GetComponent<SpaceShipComponent>();
    }

    public void Initialize(SpaceShipBluePrint blueprint)
    {
        objectList = new List<GameObject>();

        foreach (SpaceShipBluePrint.C c in blueprint.components)
        {
            GameObject obj = Instantiate(c.bluePrint.prefab);
            objectList.Add(obj);
            obj.transform.position = transform.position;
        }

        for(int i = 0; i < blueprint.components.Count; ++i)
        {
            foreach (SpaceShipBluePrint.C.DirectionAndConnection directionAndConnection in blueprint.components[i].status)
            {
                if (directionAndConnection.status == ConnectionStatus.Status.OCCUPIED && directionAndConnection.componentIndex != -1)
                {
                    objectList[i].GetComponent<SpaceShipComponent>().ConvertBluePrint(blueprint.components[i].bluePrint, blueprint, objectList);
                }
            }
        }
    }
}
