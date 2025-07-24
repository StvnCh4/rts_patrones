using UnityEngine;

namespace States.Units
{
    public interface IUnitState
    {
        void Enter(Unit unit);
        void Update(Unit unit );
        void Exit(Unit unit);
    }
}