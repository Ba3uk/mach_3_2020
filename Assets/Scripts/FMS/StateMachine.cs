using FMS;
using GameCore;
using Logic;
using System;
using System.Collections;
using UnityEngine;
namespace GameCore
{
    public class StateMachine : MonoBehaviour
    {
        public static event Action<IState> OnChangeCurrentState;

        private IState currentState;
        public IState CurrentState
        {
            private set 
            {
                if (currentState == value)
                    return;
                currentState = value;

                OnChangeCurrentState?.Invoke(currentState);
            }

            get => currentState;
        }

        private Logic.Grid grid;

        public void SetGrid(Logic.Grid grid)
        {
            this.grid = grid;
        }


        /// <summary>
        /// Сетаем новый стейт
        /// </summary>
        /// <param name="nextState">новый стейт</param>
        /// <param name="waitTime">Время между сменами стейта</param>
        public void SetState(IState nextState, float waitTime = 0)
        {
            if (CurrentState != null)
                CurrentState.Exit();

            if (waitTime > 0)
            {
                StartCoroutine(WaitTime(nextState, waitTime));
                return;
            }

            CurrentState = nextState;
            CurrentState.Enter(grid);
        }


        private void FixedUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.Execute();
            }
        }

        private IEnumerator WaitTime(IState nextState, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SetState(nextState);
        }
    }
}