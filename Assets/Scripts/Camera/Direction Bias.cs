using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace romnoelp
{
    public class DirectionBias : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;
        private bool isFacingRight;
        private Movement player;

        [Header("Camera Smoothing Values")]
        [SerializeField] private float yAxisRotationFlipTime = 0.5f;
        [SerializeField] private float easingStrength;

        private void Awake()
        {
            player = playerTransform.gameObject.GetComponent<Movement>();
            isFacingRight = player.getIsFacingRight;
        }

        void Update()
        {
            transform.position = playerTransform.position;
        }

        public void CallTurn()
        {
            float endRotation = DetermineEndRotation();

            transform.DORotate(new Vector3(0f, endRotation, 0f), yAxisRotationFlipTime).SetEase(Ease.InOutSine, easingStrength);
        }

        private float DetermineEndRotation()
        {
            isFacingRight = !isFacingRight;
            return isFacingRight ? 0f : 180f;
        }
    }
}
