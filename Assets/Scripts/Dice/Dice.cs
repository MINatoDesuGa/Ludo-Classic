using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using DG.Tweening;
public class Dice : MonoBehaviour, IPointerEnterHandler
{
    public static event Action OnDiceRolled;

    public static int RolledValue;

    [SerializeField] private Image _diceImage;
    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private Sprite _defaultSprite;
    
    private bool _canRoll = true;
    private bool _isRolling = false;
    //===============================================================//
    private void Start() {
        _diceImage.sprite = _defaultSprite;
    }
    private void OnEnable() {
        PlayerPiece.OnPlayerMoveComplete += OnPlayerMoveComplete;
    }
    private void OnDisable() {
        PlayerPiece.OnPlayerMoveComplete -= OnPlayerMoveComplete;
    }
    //===============================================================//
    public void OnPointerEnter(PointerEventData eventData) {
        if (!_canRoll || _isRolling) return;
        OnDiceRoll();
    }
    private void OnPlayerMoveComplete() {
        _canRoll = true;
    }
    private void OnDiceRoll() {
        _isRolling = true;
        _canRoll = false;
        _diceImage.sprite = _defaultSprite;
        transform.DOShakePosition(1, 10).OnComplete(() => {
            RolledValue = UnityEngine.Random.Range(0, _diceFaces.Count);
            _diceImage.sprite = _diceFaces[RolledValue];

            OnDiceRolled?.Invoke();
            _isRolling = false;

            //for now roll true , to be changed later
            _canRoll = true;
        });

        
    }
}
