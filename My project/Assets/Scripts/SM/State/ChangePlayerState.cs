using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace SM.State 
{
    public class ChangePlayerState : IState 
    {
        private readonly StateMachine _stateMachine;
        private readonly GameObject[] _characters;
        private readonly List<Color> _colors;
        private readonly LineRenderer _lineRenderer;
        private int _currentCharacterIndex;
        private readonly BoxCollider2D[] _targetColliders;
        private IState _drawing;
        private ICharacterController _characterController;

        public ChangePlayerState(StateMachine stateMachine, GameObject[] characters, List<Color> colors,
            LineRenderer lineRenderer, BoxCollider2D[] targetColliders,ICharacterController characterController)
        {
            _stateMachine = stateMachine;
            _characters = characters;
            _colors = colors;
            _lineRenderer = lineRenderer;
            _targetColliders = targetColliders;
            _characterController = characterController;
        }

        public void Enter()
        {
            Debug.Log("Запуск состояния смены персонажа");
            _currentCharacterIndex = (_currentCharacterIndex + 1) % _characters.Length;
            _lineRenderer.startColor = _lineRenderer.endColor = _colors[_currentCharacterIndex];
            _characterController = _characters[_currentCharacterIndex].GetComponent<ICharacterController>();
        }

        public void Update()
        {
            if (IsTargetReached())
            {
                _stateMachine.AddAnyTransition(_drawing,IsDrawingCondition);
            }
        }
        public Func<bool> IsDrawingCondition { get; set; }
        bool IsTargetReached()
        {
            if (_characterController != null && _characterController.HasDrawnLine())
            {
                BoxCollider2D drawnLineCollider = _characters[_currentCharacterIndex].GetComponent<BoxCollider2D>();

                foreach (BoxCollider2D targetCollider in _targetColliders)
                {
                    if (drawnLineCollider.bounds.Intersects(targetCollider.bounds))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }

}
