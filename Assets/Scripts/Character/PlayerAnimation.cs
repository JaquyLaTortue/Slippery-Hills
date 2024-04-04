using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        _playerMain.Movement.UpdateJumpAnimation += JumpAnimation;
        _playerMain.Movement.UpdateSlideAnimation += SlideAnimation;
        _playerMain.Movement.UpdateRunningAnimation += RunningAnimation;
    }

    private void JumpAnimation()
    {
        _animator.SetTrigger("IsJumping");
    }

    private void SlideAnimation(bool isSliding)
    {
        _animator.SetBool("IsSliding", isSliding);
    }

    private void RunningAnimation(bool isRunning)
    {
        _animator.SetBool("IsRunning", isRunning);
    }

    public void EndSlideAnim()
    {
        _playerMain.Movement.ResetSliding();
    }

    public void ResetFrictions()
    {
        _playerMain.Movement.ResetFrictions();
    }
}
