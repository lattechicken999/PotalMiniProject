using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractControll : MonoBehaviour
{
    [SerializeField] LayerMask _rayMask;
    public float _rayDist = 20f;
    private Camera _cam;
    private InputAction _selectAction;
    private Transform _selectedTransform;

    private Ray ray;
    private Ray Debugray1;
    private Ray Debugray2;
    private RaycastHit _rayHit;
    private Transform _selected;
    private void Awake()
    {
        _cam = Camera.main;
        _selectAction = InputSystem.actions["Attack"];
        _selectedTransform = null;
    }
    private void OnEnable()
    {
        _selectAction.performed += SelectObj;
    }
    private void OnDisable()
    {
        _selectAction.performed -= SelectObj;
    }

    private void SelectObj(InputAction.CallbackContext ctx)
    {
        ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debugray1 = ray;
        if (Physics.Raycast(ray,out _rayHit, _rayDist,_rayMask))
        {
            int maxPotalCount = 10;
            while(_rayHit.collider.CompareTag("potal") && maxPotalCount >0)
            {
                //무한루프 방지용
                maxPotalCount--;
                //포탈에 닿았다면 레이를 다른 포탈에서 다시 쏘기
                var leftDist = _rayDist - Vector3.Distance(_rayHit.point, ray.origin);
                var otherPotalTransform = PotalManager.Instance.GetOtherPotalTransform(_rayHit.transform.parent);
                var newRayOrigin = otherPotalTransform.position +(_rayHit.point - _rayHit.transform.position);
                var localRayDir = _rayHit.transform.InverseTransformDirection((_rayHit.point - ray.origin));
                var globalRayDir = otherPotalTransform.TransformDirection(localRayDir);
                ray = new Ray(newRayOrigin, globalRayDir.normalized);
                Debugray2 = ray;

                if (!Physics.Raycast(ray, out _rayHit, _rayDist, _rayMask))
                    return; //근데 맞은게 없다면 리턴
            }
            if(_rayHit.collider.CompareTag("interactable"))
            {
                _selected = _rayHit.collider.transform;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Debugray1.origin, Debugray1.direction*10);
        Gizmos.DrawRay(Debugray2.origin, Debugray2.direction*10);
    }
}
