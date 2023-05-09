using System.Collections.Generic;
using UnityEngine;

namespace SM.State 
{
    public class WaitingState:IState 
    {
        private readonly StateMachine _stateMachine;
        

        public WaitingState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        

        public void Enter()
        {
            Debug.Log("Запуск состояния ожидания");
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            Debug.Log("Выходи из состояния ожидания");
        }

        
    }
}