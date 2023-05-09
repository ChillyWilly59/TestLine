using System.Collections.Generic;
using UnityEngine;

namespace SM.State 
{
    public class MovingState : IState {
        private readonly StateMachine _stateMachine;
        private readonly LineRenderer _lineRenderer;
        private int _currentIndex;
        private readonly GameObject[] _characters;
        private readonly List<Vector3> _points;
        private float _speed = 2.5f;
        private bool _hasCollided;

        public MovingState(StateMachine stateMachine, GameObject[] characters, List<Vector3> points)
        {
            _stateMachine = stateMachine;
            _characters = characters;
            _points = points;
        }

        public void Enter()
        {
            Debug.Log("Запуск состояния движения");
            _currentIndex = 0;
            _characters[0].transform.position = _points[_currentIndex];
            _hasCollided = false;
        }

        public void Update()
        {
            if (_currentIndex < _points.Count - 1)
            {
                Vector3 nextPosition = _points[_currentIndex + 1];

                _characters[0].transform.position = Vector3.MoveTowards(_characters[0].transform.position, nextPosition,
                    Time.deltaTime * _speed);

                if (Vector3.Distance(_characters[0].transform.position, nextPosition) < 0.01f)
                {
                    _currentIndex++;
                }

                if (IsColliding())
                {
                    _hasCollided = true;
                    // сюда надо прописать логику поражения или победы 
                }
            }

            if (!HasDrawnLine())
            {
                //если линия не нарисованна переключаемся в ожидание 
            }
        }

        public void Exit()
        {
            Debug.Log("Выход из состояния движения");
        }

        private bool HasDrawnLine()
        {
            LineRenderer lineRenderer = _characters[0].GetComponent<LineRenderer>();
            return lineRenderer != null && lineRenderer.positionCount > 0;
        }

        private bool IsColliding()
        {
            return false;
        }
    }

}