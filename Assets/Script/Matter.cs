using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matter : MonoBehaviour
{
    public static float G = 6.67408E-11f;
    public static List<Matter> matters;

    public bool trillion = false;
    Rigidbody2D body;

    public void FixedUpdate()
    {
        foreach(Matter matter in matters) {
            if(matter != this && !matter.trillion)
            {
                Attract(matter);
            }
        }   
    }

    private void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();

        if (matters == null)
        {
            matters = new List<Matter>();
        }

        matters.Add(this);
    }

    private void OnDisable()
    {
        matters.Remove(this);
    }

    public void Attract(Matter matter)
    {
        Rigidbody2D opponent = matter.GetComponent<Rigidbody2D>();

        float bodyMass = trillion ? body.mass * 1.0E12f : body.mass;
        float opponentMass = matter.trillion ? opponent.mass * 1.0E12f : opponent.mass;

        Vector2 direction = body.position - opponent.position;
        float distance = direction.magnitude;

        if(distance == 0)
        {
            return;
        }

        float forceMagnitude = G * (bodyMass * opponentMass) / Mathf.Pow(distance, 2);

        Vector3 force = direction.normalized * forceMagnitude;

        opponent.AddForce(force);
    }
}
