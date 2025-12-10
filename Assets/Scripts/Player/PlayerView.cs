using Unity.Cinemachine;
using UnityEngine;

public class PlayerView : MonoBehaviour,IModel
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _mouseSens = 0.2f;
    [SerializeField] private float _minLookValue = -71f;
    [SerializeField] private float _maxLookValue = 65f;

    //private Animator _ani;
    private Rigidbody _rig;
    private PlayerModel _playerModel;
    private Transform _camTransform;

    private Vector3 _moveVector;
    private float _lookValue;
    private Vector3 _localVelocity;

    private void Awake()
    {
        //_ani = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody>();
        _camTransform = transform.Find("CinemachineCamera");
        _playerModel = null;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        UpdateAnimationParameter();
    }
    public void SetPlayerModel(PlayerModel pm)
    {
        _playerModel = pm;
        _playerModel.AddSubscripber(this);
    }
    public void NotifyMoveValue(Vector3 move)
    {
        _moveVector = transform.TransformDirection( move);
        //_moveVector = move;
    }

    public void NotifyLookValue(Vector2 look)
    {
        //if(look == Vector2.zero) return;
        transform.eulerAngles += new Vector3(0, look.x * _mouseSens, 0);
        _moveVector = Quaternion.AngleAxis(look.x * _mouseSens, Vector3.up) * _moveVector;
        _lookValue = Mathf.Clamp(_lookValue - (look.y * _mouseSens), _minLookValue, _maxLookValue);
        _camTransform.localEulerAngles = new Vector3(_lookValue, 0, 0);

    }

    private void FixedUpdate()
    {
        UpdateMoveAction();
    }
    private void UpdateMoveAction()
    {
        if (_moveVector == Vector3.zero)
        {
            _rig.linearVelocity = Vector3.zero;
        }
        else
        {
            _rig.linearVelocity = _moveVector.normalized * _moveSpeed * Time.fixedDeltaTime;
            //_rig.MovePosition(_rig.position + _moveVector.normalized * _moveSpeed/10 * Time.fixedDeltaTime);

        }

    }
    private void UpdateAnimationParameter()
    {
        //if (_moveVector != Vector3.zero)
        //    _ani.SetFloat("Speed", 1);
        //else
        //    _ani.SetFloat("Speed", 0);
    }


}
