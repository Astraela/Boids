using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public List<Boid> boids = new List<Boid>();
    public Transform origin ;
    public float boundsRadius;
    public float detectionRadius;

    public void AddBoid(Transform boid){
        Boid newBoid = new Boid(boid);
        boids.Add(newBoid);
    }

    List<Boid> GetCloseBoids(Boid boid){
        List<Boid> closeBoids = new List<Boid>();
        foreach(Boid other in boids){
            if(other == boid) continue;
            if(Vector3.Distance(boid.self.position, other.self.position) <= detectionRadius)
                closeBoids.Add(other);
        }
        return closeBoids;
    }

    Vector3 Rule1(Boid boid, List<Boid> closeBoids){
        Vector3 center = Vector3.zero;
        foreach(Boid other in closeBoids){
            center += other.self.position;
        }
        if(closeBoids.Count > 1)
            center = center / (closeBoids.Count-1);
        return (center - boid.self.position)/40;
    }

    Vector3 Rule2(Boid boid, List<Boid> closeBoids){
        Vector3 push = Vector3.zero;
        foreach(Boid other in closeBoids){
            if(Vector3.Distance(boid.self.position,other.self.position) < .4f){
                push -= other.self.position - boid.self.position;
            }
        }
        return push/40;
    }

    Vector3 Rule3(Boid boid, List<Boid> closeBoids){
        Vector3 velocity = Vector3.zero;
        foreach(Boid other in closeBoids){
            velocity += other.currentVelocity;
        }
        if(closeBoids.Count > 1)
            velocity = velocity / (closeBoids.Count-1);
        return (velocity - boid.currentVelocity)/300;
    }

    Vector3 Rule4(Boid boid, List<Boid> closeBoids){
        if(Vector3.Distance(boid.self.position,origin.position) >= boundsRadius){
            return (origin.position - boid.self.position)/10;
        }
        return Vector3.zero;
    }

    void BoidUpdate(Boid boid){
        List<Boid> closeBoids = GetCloseBoids(boid);

        boid.currentVelocity = (boid.currentVelocity + Rule2(boid,closeBoids) + Rule1(boid,closeBoids) + Rule3(boid,closeBoids) + Rule4(boid,closeBoids)).normalized;

        boid.self.GetComponent<Rigidbody>().velocity = boid.currentVelocity;
        boid.self.rotation = Quaternion.LookRotation(boid.currentVelocity);
    }

    void Update()
    {
        for (int i = 0; i < boids.Count; i++)
        {
            BoidUpdate(boids[i]);
        }
    }
}
