using UnityEngine;

public class PotalManager : Singleton<PotalManager>
{
    [Header("Potal Setting")]
    [SerializeField] GameObject _potal1Prefeb, _potal2Prefeb;
    [SerializeField] Material _potal1Material, _potal2Material;
    [SerializeField] Material _noneMaterial;
    [Header("Player Setting")]
    [SerializeField] Transform _player;

    private GameObject _potal1Instane, _potal2Instane;
    private Transform _potal1AreaTransform, _potal2AreaTransform;
    private Renderer _potal1Renderer, _potal2Renderer;

    protected override void init()
    {
        _potal1Instane = Instantiate(_potal1Prefeb);
        _potal2Instane = Instantiate(_potal2Prefeb);

        _potal1Instane.SetActive(false);
        _potal2Instane.SetActive(false);
    }
    private void Start()
    {
        var potal1Transform = _potal1Instane.transform.Find("Camera");
        var potal2Transform = _potal2Instane.transform.Find("Camera");
        _potal1AreaTransform = _potal1Instane.transform.Find("PotalArea");
        _potal2AreaTransform = _potal2Instane.transform.Find("PotalArea");

        potal1Transform.GetComponent<PotalCameraMove>()._otherPotalTransform = _potal2Instane.transform.Find("PotalCenter");
        potal2Transform.GetComponent<PotalCameraMove>()._otherPotalTransform = _potal1Instane.transform.Find("PotalCenter");

        potal1Transform.GetComponent<PotalCameraMove>()._playerTransform = _player;
        potal2Transform.GetComponent<PotalCameraMove>()._playerTransform = _player;

        _potal1Renderer = _potal1AreaTransform.GetComponent<Renderer>();
        _potal2Renderer = _potal2AreaTransform.GetComponent<Renderer>();

        _potal1Renderer.material = _noneMaterial;
        _potal2Renderer.material = _noneMaterial;

    }

    //임시 코드
    private void Update()
    {
        SetLinkedPotalMaterial();
    }
    private void SetLinkedPotalMaterial()
    {
        if (_potal1Instane.activeSelf && _potal2Instane.activeSelf)
        {
            _potal1Renderer.material = _potal1Material;
            _potal2Renderer.material = _potal2Material;
        }
        else
        {
            _potal1Renderer.material = _noneMaterial;
            _potal2Renderer.material = _noneMaterial;
        }
    }

    public Transform GetOtherPotalTransform(Transform potal)
    {
        if(_potal1Instane.activeSelf && _potal2Instane.activeSelf)
        {
            if (_potal1Instane.transform == potal.gameObject)
                return _potal2AreaTransform;
            if (_potal2Instane == potal.gameObject)
                return _potal1AreaTransform;
        }
        return null;
    }

}
