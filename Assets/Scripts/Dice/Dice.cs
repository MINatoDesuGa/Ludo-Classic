using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using DG.Tweening;
public class Dice : MonoBehaviour, IPointerEnterHandler
{
    public static event Action<int> OnDiceRolled;

    [SerializeField] private Image _diceImage;
    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private Sprite _defaultSprite;
    
    private int _rolledValue;
    private bool _canRoll = false;
    //===============================================================//
    private void Start() {
        _diceImage.sprite = _defaultSprite;
    }
    //===============================================================//
    public void OnPointerEnter(PointerEventData eventData) {
        if (_canRoll) return;
        OnDiceRoll();
    }
    private void OnDiceRoll() {
        _canRoll = true;
        _diceImage.sprite = _defaultSprite;
        transform.DOShakePosition(1, 10).OnComplete(() => {
            _rolledValue = UnityEngine.Random.Range(0, _diceFaces.Count);
            _diceImage.sprite = _diceFaces[_rolledValue];

            OnDiceRolled?.Invoke(_rolledValue);
            _canRoll = false;
        });

        
    }
}
