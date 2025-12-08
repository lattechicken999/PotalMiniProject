using UnityEngine;

public class PotalCameraMove : MonoBehaviour
{
    [SerializeField] public Transform _playerTransform;
    [SerializeField] public Transform _otherPotalTransform;

    private Vector3 _oldMovePosition;
    private Vector3 _movePosition;
    private Camera _cam;
    private Transform _myPotarCenter;
    private void Start()
    {
        _oldMovePosition = Vector3.zero;
        _movePosition = Vector3.zero;
        _cam = GetComponent<Camera>();
        _myPotarCenter = transform.parent.Find("PotalCenter");
    }
    private void Update()
    {
        UpdatePosition();
    }

    private void GetMoveVector()
    {
        //_movePosition = _playerTransform.position - _otherPotalTransform.position;
        _movePosition = _otherPotalTransform.InverseTransformPoint(_playerTransform.position);
    }
    
    private void UpdatePosition()
    {

        GetMoveVector();

        if (_oldMovePosition == _movePosition)
            return;
        _oldMovePosition = _movePosition;

        _movePosition = new Vector3(-_movePosition.x, _movePosition.y, -_movePosition.z);

        //로컬 포지션의 기준위치가 카메라의 위치로 되는 것으로 보임
        //자신의 포탈 센터 기준으로 상대좌표를 월드 좌표로 변환 한 후 카메라 좌표를 잡아줘야 할 것 같음

        //transform.localPosition = _movePosition.normalized * 2.5f;
        transform.position = _myPotarCenter.TransformPoint(_movePosition);

        transform.LookAt(gameObject.transform.parent);
        _cam.nearClipPlane = Mathf.Abs((_myPotarCenter.position - transform.position).z);
    }
}
