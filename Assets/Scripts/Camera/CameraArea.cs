using Unity.VisualScripting;
using UnityEngine;

public class CameraArea : MonoBehaviour
{
    [SerializeField] private bool right;
    [SerializeField] private bool left;
    [SerializeField] private bool up;
    [SerializeField] private bool down;
    [SerializeField] private Vector3 offset;

    private BoxCollider2D _collider;
    private CameraController _controller;

    private float _maxX;
    private float _minX;
    private float _maxY;
    private float _minY;

    public float MaxX => _maxX;
    public float MinX => _minX;
    public float MaxY => _maxY;
    public float MinY => _minY;
    public Vector3 Offset => offset;

    public Bounds Bounds => _collider.bounds;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _controller = Camera.main.GetComponent<CameraController>();

        Bounds bounds = _collider.bounds;
        Camera camera = Camera.main;

        float halfHeight = camera.orthographicSize;
        float halfWidth = halfHeight * camera.aspect;

        _maxX = right ? bounds.max.x : bounds.max.x - halfWidth;

        _minX = left ? bounds.min.x : bounds.min.x + halfWidth;

        _maxY = up ? bounds.max.y : bounds.max.y - halfHeight;

        _minY = down ? bounds.min.y : bounds.min.y + halfHeight;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<WizardPresenter>(out _))
        {
            if(_controller == null)
            {
                Debug.Log("controller is null.");
                return;
            }

            _controller.SetArea(this);
        }
    }
    */
}
