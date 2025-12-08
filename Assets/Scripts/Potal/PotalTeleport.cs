using UnityEngine;

public class PotalTeleport : MonoBehaviour
{
    private Transform _otherPotalCenter;
    private GameObject _copyObj;

    private void Awake()
    {
        _otherPotalCenter = null;
    }
    //포탈 진입
    // 복사체를 상대 포탈에 만듬
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("interactable"))
        {
            _otherPotalCenter = PotalManager.Instance.GetOtherPotalTransform(transform);
            _copyObj = Instantiate(other.gameObject);

            SynCopyObjTransform(other);
        }
    }

    //포탈에 겹치는 중
    //복사체 움직임을 겹쳐져 있는 포탈과 동기화 시킴
    private void OnTriggerStay(Collider other)
    {
        if (_copyObj == null) return;
        SynCopyObjTransform(other);
    }

    //포탈 나감
    //어느 방향으로 나갔는지에 따라 복사체가 있던 곳으로 위치를 바꿀지, 그냥 둘지 판단함
    // 복사체는 반드시 소멸
    private void OnTriggerExit(Collider other)
    {
        if (_copyObj == null) return;
        //위치 동기화 하고 텔레포트 시킴 (안하면 텔레포트 하자마자 상태 포탈에 닿음)
        SynCopyObjTransform(other);
        if(transform.TransformPoint(other.transform.position).z <0)
        {
            //충돌체가 뒤로 빠지면 이동 시킴
            other.transform.position = _copyObj.transform.position;
            other.transform.rotation = _copyObj.transform.rotation;
        }
        Destroy(_copyObj);
    }
    
    private void SynCopyObjTransform(Collider other)
    {
        var copyObjPosition = new Vector3(-_otherPotalCenter.position.x, _otherPotalCenter.position.y, -_otherPotalCenter.position.z);

        //진입 포탈 기준 로컬좌표계로 오브젝트가 바라봐야 할 방향 계산.
        var copyLookPosition = transform.TransformPoint(other.transform.forward);
        //반대편 포탈 기준으로 좌표 갱신
        copyLookPosition = _otherPotalCenter.InverseTransformPoint(copyLookPosition);
        //좌표 쉬프트
        copyLookPosition = copyLookPosition - (copyLookPosition - copyObjPosition);
        _copyObj.transform.position = copyObjPosition;
        _copyObj.transform.LookAt(copyLookPosition);
    }
   
}
