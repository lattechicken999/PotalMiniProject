using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    // 상호작용 중인지 확인 하는 함수 
    public bool GetGrabed();
    //잡았을 때 호출하는 함수
    public void Grab( Transform player);

    //물체를 던짐
    public void Throw(Vector3 direction);

    public void Drop();
}

