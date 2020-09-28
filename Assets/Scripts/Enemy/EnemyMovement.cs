using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        //private Transform player;

        private NavMeshAgent _enemy;
        private Vector3 startingPos;
        private Vector3 currentPos;

        [SerializeField] private float RandomRange;
        
        
        private enum State
        {
            ROAM,
            CHASE,
            ATTACK,
            
        }
        
        private void Start()
        {
            _enemy = GetComponent<NavMeshAgent>();
        }


        private void Update()
        {
            do
            { 
                currentPos = roam();
                //Debug.Log(currentPos);
            } 
            while (currentPos == transform.position);
             
        }

        private Vector3 roam()
        {
            startingPos = transform.position;
            Vector3 randRoamPos = createRandomVector3(startingPos);
            if (startingPos != randRoamPos)
            {
                MovetoDestination(randRoamPos);
            }

            return randRoamPos;
        }
        
        /// <summary>
        /// Sets the Destination of the Current NavMesh Agent
        /// Requires a Vector3 as a parameter.
        /// </summary>
        private void MovetoDestination(Vector3 Pos)
        {
            if (transform.position != Pos)
            {
                _enemy.destination = Pos;
            }
        }
        /// <summary>
        /// Generates a rondom Vector3 from the provided Vector3.
        /// </summary>
        /// <param name="Pos">
        /// Pos needs to be transform.position of the gameobject
        /// </param>
        /// <returns>
        /// Random Vector3 
        /// </returns>
        private Vector3 createRandomVector3(Vector3 Pos)
        {
           /Vector3 randVect = Pos + new Vector3(Random.Range(Pos.x - RandomRange, Pos.x+RandomRange), 0, Random.Range(Pos.z - RandomRange, Pos.z+RandomRange)).normalized; 
            return randVect;
            

           
        }
    }
}
