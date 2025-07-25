using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UnitMovement : MonoBehaviour
{
     Camera cam;
     NavMeshAgent agent;
     public LayerMask ground;

     public bool isCommandedToMove;
     private void Start()
     {
          cam = Camera.main;
          agent = GetComponent<NavMeshAgent>();
     }
     
     private void Update()
     {
          if (Input.GetMouseButton(1))
          {
               
               Ray ray = cam.ScreenPointToRay(Input.mousePosition);

               if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
               {
                    isCommandedToMove = true;
                    agent.SetDestination(hit.point);
               }
          }

          //Agent reached destination
          if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
          {
               isCommandedToMove = false;
          }

     }
     
     public void MoveTo(Vector3 position)
     {
          
          isCommandedToMove = false;
          agent.SetDestination(position);
     }

     public bool ReachedDestination()
     {
          return !agent.hasPath || agent.remainingDistance <= agent.stoppingDistance;
     }
}
