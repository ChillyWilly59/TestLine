using System;
using System.Collections.Generic;
using SM.State;
using UnityEngine;
using IState = SM.IState;

public class GameInstanse : MonoBehaviour 
{
    public LineRenderer lineRenderer;
    private List<Vector3> _points = new List<Vector3>();
    public GameObject[] characters;
    public List<Color> colors;
    public BoxCollider2D[] targetCollider;
    

    private StateMachine _stateMachine;
    private static object _character;
    private ICharacterController _characterController;
    private List<List<Vector3>> _savedLines = new List<List<Vector3>>();
    private Func<bool> _condition;
    //private OtherObject _otherObject;

    private void Awake()
    {
        _stateMachine = new StateMachine(lineRenderer);
        IState drawing = new DrawingState(_stateMachine, lineRenderer, _points, SaveLine, IsOtherDrawingComplete);
        IState waiting = new WaitingState(_stateMachine);
        IState waitingForLine = new WaitingForLineState(_stateMachine, () => !HasDrawnLine(characters[1]), characters, _points, waiting);
        IState moving = new MovingState(_stateMachine,characters,_points);
        IState changePlayer = new ChangePlayerState(_stateMachine,characters,colors,lineRenderer,targetCollider,_characterController);
        //IState victori = new VictoriState(_stateMachine);
        

        _stateMachine.AddTransition(waiting, drawing, () => Input.GetMouseButtonDown(0))
            .AddTransition(drawing, waiting, () => Input.GetMouseButtonUp(0))
            .AddTransition(waiting, moving, () => _points.Count == characters.Length)
            .AddTransition(waiting, changePlayer, () => Input.GetKeyDown(KeyCode.Tab))
            .AddTransition(changePlayer, drawing, () => Input.GetKeyUp(KeyCode.Tab))
            .AddTransition(waiting, moving, AreAllCharactersDrawnLines)
            .AddTransition(waiting, waitingForLine, () => !HasDrawnLine(characters[1])); 

        _stateMachine.AddTransition(waitingForLine, waiting, () => HasDrawnLine(characters[1]));
        
        _stateMachine.SetState(waiting);
        _stateMachine.SetActive(true);
    }

    private bool IsOtherDrawingComplete()
    {
        for (int i = 1; i < characters.Length; i++)
        {
            ICharacterController characterController = characters[i].GetComponent<ICharacterController>();
            if (characterController != null && !characterController.HasDrawnLine())
            {
                return false;
            }
        }

        return true;
    }

    private void SaveLine(List<Vector3> obj)
    {
        _savedLines.Add(_points);

        //_otherObject.ProcessLine(_points);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
    
    private bool AreAllCharactersDrawnLines()
    {
        // Проверка для первого персонажа
        if (HasDrawnLineToCollider(characters[0], targetCollider[0]))
        {
            return true;
        }

        // Проверка для остальных персонажей
        for (int i = 1; i < characters.Length; i++)
        {
            if (!HasDrawnLine(characters[i]))
            {
                return false;
            }
        }

        return true;
    }
    
    private bool HasDrawnLineToCollider(GameObject character, BoxCollider2D collider)
    {
        LineRenderer lineRenderer = character.GetComponent<LineRenderer>();
        if (lineRenderer != null && lineRenderer.positionCount > 0)
        {
            Vector3 lineEnd = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            return collider.OverlapPoint(lineEnd);
        }

        return false;
    }
    
    private bool HasDrawnLine(GameObject character)
    {
        LineRenderer lineRenderer = character.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            return lineRenderer.positionCount > 0;
        }
        
        return false;
    }

    
}