using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeObj : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed = 3f;
    private Rigidbody _rig;
    private Collider _col;

    private Transform _player;
    private Transform _parent;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        gameObject.layer = LayerMask.NameToLayer("interactable");
    }

    public void Drop()
    {
        UngrabedState();
    }


    public void Grab(Transform player)
    {
        _player = player;
        GrabedState ();
    }

    public void Throw(Vector3 direction)
    {
        UngrabedState();
        _rig.AddForce(direction.normalized*15,ForceMode.Impulse);
    }

    private void GrabedState()
    {
        if (_col == null || _rig == null) return;
        _col.isTrigger = true;
        _rig.useGravity = false;
        _rig.isKinematic = true;

        _parent = transform.parent;
        transform.SetParent(_player);
        transform.position = _player.TransformPoint(0, 0.1f, 1f);
    }
    private void UngrabedState()
    {
        if (_col == null || _rig == null) return;
        _col.isTrigger = false;
        _rig.useGravity = true;
        _rig.isKinematic = false;
        transform.SetParent(_parent);
    }
}
