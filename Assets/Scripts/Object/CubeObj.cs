using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeObj : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed = 3f;
    private Vector3 _moveTo;
    private Rigidbody _rig;
    private Vector3 _oldPlayerPosition;
    private Vector3 _newPlayerPosition;

    private Stack<Vector3> _movePath;
    private Transform _player;

    private bool _isGrabed;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("interactable");
    }
    //private void FixedUpdate()
    //{
    //  if(_moveTo != Vector3.zero)
    //    {
    //        _rig.MovePosition(_moveTo * _speed * Time.fixedDeltaTime);
    //        if (Vector3.Distance(_moveTo, _rig.position) < 0.1)
    //            UpdateMoveTo();
    //    }
    //}

    private void Update()
    {
        if(_isGrabed)
        {
            _newPlayerPosition = _player.position;
            if (_oldPlayerPosition == _newPlayerPosition) return;
            _oldPlayerPosition = _newPlayerPosition;

            transform.position = _player.TransformPoint(0, 0.1f, 0.1f);
            transform.rotation = _player.rotation;
        }
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }


    public void Grab(Stack<Vector3> movePath, Transform player)
    {
        _isGrabed = true;
        //_movePath = movePath;
        _player = player;

    }

    public void Throw(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    //private void UpdateMoveTo()
    //{
    //    if(_movePath.Count == 0)
    //    {
    //        _moveTo = Vector3.zero;
    //        return;
    //    }

    //    _moveTo = _movePath.Pop();
    //    if (_movePath.Count == 0)
    //        _moveTo = _player.TransformPoint(new Vector3(0, 0.1f, 0.1f));

    //}
}
