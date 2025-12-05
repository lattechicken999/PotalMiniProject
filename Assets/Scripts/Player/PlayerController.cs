using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private PlayerView _playerView;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private PlayerModel _playerModel;

    private void Awake()
    {
        _moveAction = InputSystem.actions["Move"];
        _lookAction = InputSystem.actions["Look"];
        _playerView = GetComponent<PlayerView>();
        _playerModel = new PlayerModel();
    }
    private void Start()
    {
        _playerView.SetPlayerModel(_playerModel);
    }

    private void OnEnable()
    {
        _moveAction.performed += ctx => _playerModel.UpdateMove(ctx.ReadValue<Vector2>());
        _moveAction.canceled += ctx => _playerModel.UpdateMove(Vector3.zero);
        _lookAction.performed += ctx => _playerModel.UpdateLook(ctx.ReadValue<Vector2>());
        _lookAction.canceled += ctx => _playerModel.UpdateLook(Vector3.zero);
    }
    private void OnDisable()
    {
        _moveAction.performed -= ctx => _playerModel.UpdateMove(ctx.ReadValue<Vector2>());
        _moveAction.canceled -= ctx => _playerModel.UpdateMove(Vector3.zero);
        _lookAction.performed -= ctx => _playerModel.UpdateLook(ctx.ReadValue<Vector2>());
        _lookAction.canceled -= ctx => _playerModel.UpdateLook(Vector3.zero);
    }

}
