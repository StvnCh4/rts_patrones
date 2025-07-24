using System;
using UnityEngine;

namespace States.Units
{
    public class AttackState:IUnitState
    {

        public void Enter(Unit unit)
        {
            unit.attackController.SetAttackMaterial();
        }

        public void Update(Unit unit)
        { 
            var target = unit.attackController.targetToAttack;
            
            //si no hay targe o hay indicacion de movimiento
            if (target == null || unit.movement.isCommandedToMove)
            {
                unit.ChangeState(new IdleState());
                return;
            }
            
            LookAtTarget(unit);

            float distance = Vector3.Distance(unit.transform.position, target.position);

            if (distance > unit.stopAttackingDistance)
            {
                unit.ChangeState(new IdleState());
                return;
            }

            if (distance > unit.attackingDistance)
            {
                unit.agent.SetDestination(target.position);
            }
            else
            {
                unit.agent.ResetPath();
                unit.animator.SetTrigger("Attack");
                //unit.animator.SetBool("isAttacking",true);
                //cooldown
                // if (unit.attackCooldown <= 0f)
                // {
                //     unit.animator.SetTrigger("Attack");
                //     unit.attackController.DealDamage(target);
                //     unit.attackCooldown = unit.attackRate;
                // }
                // else
                // {
                //     unit.attackCooldown -= Time.deltaTime;
                // }

            }
            // if (target != null && !unit.movement.isCommandedToMove)
            // {
            //     LookAtTarget(unit);
            //     //moverse hacia el enemigo
            //     unit.agent.SetDestination(target.position);
            //     
            //     //deberia la unidad seguir atacando
            //     float distance = Vector3.Distance(unit.transform.position, target.position);
            //     if (distance > unit.stopAttackingDistance)
            //     {
            //       unit.ChangeState(new IdleState());
            //       return;
            //     }
            // }
        }

        private void LookAtTarget(Unit unit)
        {
            Vector3 direction = unit.attackController.targetToAttack.position - unit.transform.position;
            direction.y = 0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

        public void Exit(Unit unit)
        {
            unit.agent.ResetPath();
        }
    }
}