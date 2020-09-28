using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform player;

        private NavMeshAgent _enemy;

        private void Start()
        {
            _enemy = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (transform.position != player.transform.position)
            {
                _enemy.destination = player.transform.position;
            }
        }
    }
}
