using System.Collections;
using UnityEngine;

namespace romnoelp
{
    public class DirectionBias : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;
        private bool isFacingRight;
        private Movement player;

        [Header("Camera Smoothing Values")]
        [SerializeField] private float yAxisRotationFlipTime = .5f;
        private Coroutine LerpYAxis;

        private void Awake() {
            player = playerTransform.gameObject.GetComponent<Movement>();
            isFacingRight = player.isFacingRight;
        }

        void Update()
        {
            transform.position = playerTransform.position;
        }

        public void CallTurn()
        {
            LeanTween.rotateY(gameObject, DetermineEndRotation(), yAxisRotationFlipTime).setEaseInOutSine();
        }

        private float DetermineEndRotation()
        {
            isFacingRight = !isFacingRight;
            if (isFacingRight)
            {
                return 0f;
            }
            else
            {
                return 180f;
            }
        }
    }
}