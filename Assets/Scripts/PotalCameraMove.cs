using UnityEngine;

public class PotalCameraMove : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _otherPotalTransform;

    private Vector3 _oldMovePosition;
    private Vector3 _movePosition;
    private Camera _cam;
    private void Start()
    {
        _oldMovePosition = Vector3.zero;
        _movePosition = Vector3.zero;
        _cam = GetComponent<Camera>();
    }
    private void Update()
    {
        UpdatePosition();
    }

    private void GetMoveVector()
    {
        _movePosition = _playerTransform.position - _otherPotalTransform.position;
    }
    
    private void UpdatePosition()
    {

        GetMoveVector();

        if (_oldMovePosition == _movePosition)
            return;
        _oldMovePosition = _movePosition;

        transform.localPosition = _movePosition.normalized * 2.5f;
        transform.LookAt(gameObject.transform.parent);
        //_cam.nearClipPlane = (transform.parent.transform.position - transform.position).z;
    }
}
