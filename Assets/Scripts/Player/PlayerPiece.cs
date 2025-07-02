using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using System.Collections;

public class PlayerPiece : MonoBehaviour, IPointerEnterHandler
{
    public static event Action OnPlayerMoveComplete;

    [SerializeField] private PlayerColor _playerColor;
    private bool _canMove = false;
    private bool _isMoving = false;
    private bool _reachedHome = false;

    private int _currentPositionIndex = -1;
    private Coroutine _moveCoroutine;
    private WaitForSeconds _moveDelayTime;
    //=======================================================//
    private void Start() {
        _moveDelayTime = new(0.5f);
    }
    private void OnEnable() {
        Dice.OnDiceRolled += OnDiceRolled;
    }
    private void OnDisable() {
        Dice.OnDiceRolled -= OnDiceRolled;
    }
    //=======================================================//
    public void OnPointerEnter(PointerEventData eventData) {
        if (!_canMove || _isMoving) return;
        _canMove = false;
        print($"moving {transform.name}");
        _isMoving = true;
        if (_currentPositionIndex == -1) {
            transform.DOMove(GlobalVars.Instance.PlayerWayPoint[_playerColor][0].position, 0.5f).OnComplete(()=> {
                _currentPositionIndex = 0;
                OnPlayerMoveComplete?.Invoke();
                _isMoving = false;
            }); 
            return;
        }
        _moveCoroutine = StartCoroutine( MovePlayer( _currentPositionIndex + Dice.RolledValue + 1) );
    }
    private void OnDiceRolled() {
        if (_reachedHome) return;
        if (GlobalVars.Instance.CurrentPlayerTurnColor != _playerColor) return;
        if (_currentPositionIndex == -1 && Dice.RolledValue != 5) return;
        if ((GlobalVars.MAX_WAYPOINT_INDEX - _currentPositionIndex) < Dice.RolledValue) return;

        _canMove = true;
    }
    private IEnumerator MovePlayer(int targetValue) {
        for (++_currentPositionIndex; _currentPositionIndex <= targetValue; ++_currentPositionIndex) {
            transform.DOMove(GlobalVars.Instance.PlayerWayPoint[_playerColor][_currentPositionIndex].position, 0.5f);
            yield return _moveDelayTime;
        }
        _currentPositionIndex--;

        if (_currentPositionIndex == GlobalVars.MAX_WAYPOINT_INDEX) _reachedHome = true;

        OnPlayerMoveComplete?.Invoke();
        _isMoving = false;
    }
}
public enum PlayerColor {
    Blue, Red, Green, Yellow
}