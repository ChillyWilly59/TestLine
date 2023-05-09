using System;
using System.Collections.Generic;
using UnityEngine;

namespace SM.State 
{
    public class DrawingState : IState 
    {
        private readonly StateMachine _stateMachine;
        private Vector3 _lastPoint;
        private readonly LineRenderer _lineRenderer;
        private readonly List<Vector3> _points;
        private Action<List<Vector3>> _saveLineAction;
        private Func<bool> _isOtherDrawingComplete;

        public DrawingState(StateMachine stateMachine, LineRenderer lineRenderer, List<Vector3> points,
            Action<List<Vector3>> saveLineAction, Func<bool> isOtherDrawingComplete)
        {
            _stateMachine = stateMachine;
            _lineRenderer = lineRenderer;
            _points = points;
            _saveLineAction = saveLineAction;
            _isOtherDrawingComplete = isOtherDrawingComplete;
        }

        public void Enter()
        {
            Debug.Log("Вход в состояние рисования");
            _points.Clear();
        }

        public void Update()
        {
            _lineRenderer.positionCount = 1;

            if (Input.GetMouseButtonDown(0))
            {
                _points.Clear();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                _points.Add(mousePosition);

                _lineRenderer.positionCount = _points.Count;

                for (int i = 0; i < _points.Count; i++)
                {
                    _lineRenderer.SetPosition(i, _points[i]);
                }

                _lastPoint = _points[_points.Count - 1];
            }

            if (_isOtherDrawingComplete())
            {
                _saveLineAction?.Invoke(_points);

                
                //_stateMachine.ChangeState(/* следующее состояние */);
            }
        }

        public void Exit()
        {
            Debug.Log("Выход из состояния рисования");
        }
    }
}
