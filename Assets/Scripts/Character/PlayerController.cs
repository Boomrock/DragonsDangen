using Zenject;

public class PlayerController 
{
    private PlayerMover _playerMover;
    private PlayerView _playerView;
    
    public PlayerController(
        [Inject(Id = BindId.Player)] Mover playerMover, 
        [Inject(Id = BindId.Player)] PlayerView playerView)
    {
        _playerMover = playerMover as PlayerMover;
        _playerView = playerView;
        Enable();
    }

    public void Enable()
    {
        _playerView.OnViewDisable += OnViewDisableHandler;
        _playerView.OnViewEnable += OnViewEnableHandler;
        _playerMover.Enable();
    }

    public void Disable()
    {
        _playerView.OnViewDisable -= OnViewDisableHandler;
        _playerView.OnViewEnable -= OnViewEnableHandler;
    }
    
    private void OnViewEnableHandler()
    {
        _playerMover.OnMove += _playerView.ComputePlayerDirectionToAnimator;
    }
    
    private void OnViewDisableHandler()
    {
        _playerMover.OnMove -= _playerView.ComputePlayerDirectionToAnimator;
    }
}
