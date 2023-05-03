using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

namespace BladeOfNight
{
    public static class CharacterAnimatorParamId
    {
        public static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        public static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    }

    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Character _character;
        public InputAction ComboAction;

        [SerializeField]
        private AnimationClip _comboAnimation;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _character = GetComponent<Character>();
        }

        private void OnEnable()
        {
            ComboAction.Enable();
            ComboAction.started += PerformCombo;
        }

        private void OnDisable()
        {
            ComboAction.Disable();
            ComboAction.started -= PerformCombo;
        }

        public void UpdateState()
        {
            float normHorizontalSpeed = _character.HorizontalVelocity.magnitude / _character.MovementSettings.MaxHorizontalSpeed;
            _animator.SetFloat(CharacterAnimatorParamId.HorizontalSpeed, normHorizontalSpeed);

            float jumpSpeed = _character.MovementSettings.JumpSpeed;
            float normVerticalSpeed = _character.VerticalVelocity.y.Remap(-jumpSpeed, jumpSpeed, -1.0f, 1.0f);
            _animator.SetFloat(CharacterAnimatorParamId.VerticalSpeed, normVerticalSpeed);

            _animator.SetBool(CharacterAnimatorParamId.IsGrounded, _character.IsGrounded);
        }

        private void PerformCombo(InputAction.CallbackContext context)
        {
            _animator.SetBool("Combo", true);
            StopCombo();
        }

        private async void StopCombo()
        {
            int animationClipLength = (int)_comboAnimation.length;
            Debug.Log(animationClipLength);
            await Task.Delay(animationClipLength);
            Debug.Log("Stop");
            _animator.SetBool("Combo", false);
        }
    }
}