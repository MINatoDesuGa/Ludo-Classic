using UnityEngine;
using System.Collections.Generic;
public class GlobalVars : MonoBehaviour
{
    public static GlobalVars Instance;
    public const int MAX_WAYPOINT_INDEX = 57;

    public PlayerColor CurrentPlayerTurnColor;

    public Dictionary<PlayerColor, List<Transform>> PlayerWayPoint = new();
    public RectTransform CanvasRectTransform;
    [SerializeField] private List<WayPoint> _wayPoints;
    //======================================================//
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }
    private void Start() {
        CurrentPlayerTurnColor = PlayerColor.Blue;
        InitWaypoints();
    }
    //=======================================================//
    private void InitWaypoints() {
        foreach (var wayPoint in _wayPoints) {
            PlayerWayPoint[wayPoint.PlayerColor] = wayPoint.WayPoints;
        }
        _wayPoints.Clear();
    }
}
/*================================================*/
[System.Serializable]
public class WayPoint {
    public PlayerColor PlayerColor;
    public List<Transform> WayPoints;
}