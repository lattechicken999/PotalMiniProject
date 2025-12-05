using UnityEngine;
public interface IModel 
{
    public void NotifyMoveValue(Vector3 move);
    public void NotifyLookValue(Vector2 look);
}
