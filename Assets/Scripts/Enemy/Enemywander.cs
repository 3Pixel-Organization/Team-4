using UnityEngine;
using System.Collections;
using UnityEngine.AI;
 
public class Enemywander :EnemyBehaviour {
 
    public float wanderRadius;
    public float wanderTimer;
 
    private Transform target;
    private NavMeshAgent agent;
    private float timer = 0f;
    
    
    // Use this for initialization
    void OnEnable () {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
       
    }
 
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
            Debug.Log(newPos); 
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
        Debug.Log(randDirection);
        NavMeshHit navHit;
 
        NavMesh.SamplePosition(randDirection, out navHit, dist, NavMesh.AllAreas);
        Debug.Log(navHit.position);
 
        return navHit.position;
    }
}