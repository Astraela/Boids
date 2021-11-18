using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsSpawner : MonoBehaviour
{
    public GameObject boid;
    public float range;
    public int amount;
    void Start()
    {
        BoidManager boidManager = FindObjectOfType<BoidManager>();
        UnityEngine.Random.InitState((DateTime.UtcNow - new DateTime(1970,1,1)).Milliseconds);
        for (int i = 0; i < amount; i++)
        {
            GameObject newBoid = Instantiate(boid);
            newBoid.name = i.ToString();
            newBoid.transform.position = new Vector3(UnityEngine.Random.Range(-range,range),UnityEngine.Random.Range(-range,range),UnityEngine.Random.Range(-range,range));
            boidManager.AddBoid(newBoid.transform);
        }
    }

}
