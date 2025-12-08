using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    //잡았을 때 호출하는 함수
    public void Grab(Stack<Vector3> movePath, Transform player);

    //물체를 던짐
    public void Throw(Vector3 direction);

    public void Drop();
}

