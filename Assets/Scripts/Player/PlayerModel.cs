using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    private Vector3 _moveValue;
    private Vector2 _lookValue;
    private List<IModel> _subs;

    public PlayerModel()
    {
        _subs = new List<IModel>();
    }
    public void UpdateMove(Vector2 moveValue)
    {
        _moveValue = new Vector3( moveValue.x,0, moveValue.y);
        MoveNotiToSub();
    }
    public void UpdateLook(Vector2 lookValue)
    {
        _lookValue = lookValue;
        LookNotiToSub();
    }
    public void AddSubscripber(IModel sub)
    {
        _subs.Add(sub);
    }

    private void MoveNotiToSub()
    {
        foreach (var sub in _subs)
        {
            sub.NotifyMoveValue(_moveValue);
        }
    }
    private void LookNotiToSub()
    {
        foreach (var sub in _subs)
        {
            sub.NotifyLookValue(_lookValue);
        }
    }
}
