using NaughtyAttributes;
using UnityEngine;
[RequireComponent (typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Space(10)]
    [Header("Default")]
    [SerializeField] private float _targetSize = 16.9f;
    //=====================================================//
    private void OnValidate() {
        if(_camera == null) {
            _camera = GetComponent<Camera>();
        }
    }
    private void Start() {
        AdjustOrthographicSize();
    }
    //=====================================================//
    [Button]
    private void AdjustOrthographicSize() {
        float deviceAspect = (float) Screen.width / (float) Screen.height;

        _camera.orthographicSize = _targetSize / deviceAspect / 2f;
      //  Debug.Log($"cam size adjusted to {_camera.orthographicSize}");
    }
}
