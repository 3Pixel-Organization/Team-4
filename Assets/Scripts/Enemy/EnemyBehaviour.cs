using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;



    public class EnemyBehaviour : MonoBehaviour
    {
        public float wanderRadius;
        public float wanderTimer;
        
        private NavMeshAgent _enemy;
        private float _timer = 0f;
        
        
        [SerializeField]private States defaultState;
        private States _currentState;
        private enum States
        {
            Wander,
            Patrol,
            Chase,
            Attack
        }

        private void Start()
        {
            _currentState = defaultState;
        }

        private void Update()
        {
            switch(_currentState)
            {
                case States.Wander:
                    Debug.Log("Wander State initiated");
                    break;
                case States.Patrol:
                    Debug.Log("Patrol State initiated");
                    break;
                case States.Chase:
                    Debug.Log("Chase State initiated");
                    break;
                case States.Attack:
                    Debug.Log("Attack State initiated");
                    break;
                
            }
        }
    }

