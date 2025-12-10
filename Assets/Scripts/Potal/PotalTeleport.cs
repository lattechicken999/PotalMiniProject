using System.Diagnostics;
using UnityEngine;

public class PotalTeleport : MonoBehaviour
{
    private Transform _otherPotalCenter;
    private GameObject _copyObj;

    private Vector3 Degub;
    private void Awake()
    {
        _otherPotalCenter = null;
    }
    //포탈 진입
    // 복사체를 상대 포탈에 만듬
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("interactable") || other.CompareTag("Player"))
        {
            _otherPotalCenter = PotalManager.Instance.GetOtherPotalTransform(transform);
            _copyObj = Instantiate(other.gameObject);
            _copyObj.GetComponent<Collider>().enabled = false;
            SynCopyObjTransform(other);
        }
    }

    //포탈에 겹치는 중
    //복사체 움직임을 겹쳐져 있는 포탈과 동기화 시킴
    private void OnTriggerStay(Collider other)
    {
        if (_copyObj == null) return;
        SynCopyObjTransform(other);

        if(other.CompareTag("Player"))
        {
            if(transform.InverseTransformPoint(other.transform.position).y < 0)
            {
                TeleportObject(other);
            }
        }
    }

    //포탈 나감
    //어느 방향으로 나갔는지에 따라 복사체가 있던 곳으로 위치를 바꿀지, 그냥 둘지 판단함
    // 복사체는 반드시 소멸
    private void OnTriggerExit(Collider other)
    {
        if (_copyObj == null) return;
        //위치 동기화 하고 텔레포트 시킴 (안하면 텔레포트 하자마자 상태 포탈에 닿음)
        if (other.CompareTag("interactable"))
        {
            if (other.gameObject.GetComponent<IInteractable>().GetGrabed()) { }
            else if (transform.InverseTransformPoint(other.transform.position).y < 0)
            {
                SynCopyObjTransform(other);
                TeleportObject(other);
            }
        }
        Destroy(_copyObj);
    }
    
    private void SynCopyObjTransform(Collider collisionObj)
    {
        var copyObjPosition = transform.InverseTransformPoint(collisionObj.transform.position);
        copyObjPosition = new Vector3(-copyObjPosition.x, -copyObjPosition.y, copyObjPosition.z);
        copyObjPosition = _otherPotalCenter.TransformPoint(copyObjPosition);
        _copyObj.transform.position = copyObjPosition;

        var copyLookPosition = transform.InverseTransformPoint(collisionObj.transform.position + collisionObj.transform.forward);
        copyLookPosition = new Vector3(-copyLookPosition.x, -copyLookPosition.y, copyLookPosition.z);
        copyLookPosition = _otherPotalCenter.TransformPoint(copyLookPosition);
        _copyObj.transform.LookAt(copyLookPosition);
        Degub = copyLookPosition;
    }
    private void TeleportObject(Collider collisionObj)
    {
        //충돌체가 뒤로 빠지면 이동 시킴
        collisionObj.transform.position = _copyObj.transform.position;
        collisionObj.transform.rotation = _copyObj.transform.rotation;

        var rigidbody = collisionObj.gameObject.GetComponent<Rigidbody>();
        var velVector3 = rigidbody.linearVelocity;
        velVector3 = transform.InverseTransformDirection(velVector3);
        velVector3 = new Vector3(-velVector3.x, -velVector3.y, velVector3.z);
        velVector3 = _otherPotalCenter.TransformDirection(velVector3);
        rigidbody.linearVelocity = velVector3;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Degub, 0.3f);
    }
}
