using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid
{
    public Transform self;
    public Vector3 currentVelocity;

    public Boid(Transform self){
        this.self = self;        
        currentVelocity = Random.insideUnitSphere.normalized;
    }

}
