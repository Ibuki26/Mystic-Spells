using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _followSpeed = 8f;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private CameraArea _currentArea;

    [SerializeField] private LayerMask _cameraAreaLayerMask;

    public void SetArea(CameraArea area)
    {
        _currentArea = area;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        UpdateCurrentArea();

        Vector3 targetPosition = _target.position;

        if (_currentArea != null)
        {
            targetPosition.x = Mathf.Clamp(
                targetPosition.x,
                _currentArea.MinX,
                _currentArea.MaxX);

            targetPosition.y = Mathf.Clamp(
                targetPosition.y,
                _currentArea.MinY,
                _currentArea.MaxY);
        }
        
        targetPosition.y = Mathf.Lerp(
            transform.position.y,
            targetPosition.y,
            _followSpeed * Time.deltaTime);
       
        targetPosition.z = transform.position.z;

        transform.position = targetPosition;
    }
    
    private void UpdateCurrentArea()
    {
        Collider2D hit = Physics2D.OverlapPoint(
            _target.position,
            _cameraAreaLayerMask);

        if (hit == null)
            return;

        if (hit.TryGetComponent<CameraArea>(out var area))
        {
            _currentArea = area;
        }
    }
}