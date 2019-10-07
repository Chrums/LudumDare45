using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fizz6.LudumDare45
{
    public class BaseObject : MonoBehaviour
    {
        private new Rigidbody2D rigidbody = null;

        private float[][] positionHistory = new float[60 * 60 * 30][];
        private float[][] velocityHistory = new float[60 * 60 * 30][];

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            Dimension2DManager.CurrentFrameChangeEvent += OnCurrentFrameChanged;

            float[] positionState = new float[4]
            {
                transform.position.x,
                transform.position.y,
                transform.position.z,
                0.0f
            };

            positionHistory[0] = positionState;

            float[] velocityState = new float[4]
            {
                rigidbody.velocity.x,
                rigidbody.velocity.y,
                0.0f,
                1.0f
            };

            velocityHistory[0] = velocityState;

        }
        public void OnDimensionsChanged()
        {

        }

        public void OnCurrentFrameChanged(int currentFrame, int nextFrame)
        {
            UpdatePosition(currentFrame, nextFrame);
            UpdateVelocity(currentFrame, nextFrame);
        }

        private void UpdatePosition(int currentFrame, int nextFrame)
        {
            float[] previousPositionState = currentFrame == 0
                ? null
                : positionHistory[currentFrame - 1];

            float[] positionState = positionHistory[currentFrame] == null
                ? new float[4] { previousPositionState[0], previousPositionState[1], previousPositionState[2], previousPositionState[3] }
                : positionHistory[currentFrame];

            positionState[(int)Dimension2DManager.HorizontalAxis] = transform.position.x;
            positionState[(int)Dimension2DManager.VerticalAxis] = transform.position.y;

            positionHistory[currentFrame] = positionState;

            if (currentFrame == nextFrame - 1)
            {
                return;
            }

            float[] nextState = positionHistory[nextFrame];
            if (positionState == null)
            {
                gameObject.SetActive(false);
                return;
            }

            float horizontal = nextState[(int)Dimension2DManager.HorizontalAxis];
            float vertical = nextState[(int)Dimension2DManager.VerticalAxis];
            Vector3 position = new Vector3(horizontal, vertical, 0.0f);
            transform.position = position;
        }

        private void UpdateVelocity(int currentFrame, int nextFrame)
        {
            float[] previousVelocityState = currentFrame == 0
                ? null
                : velocityHistory[currentFrame - 1];

            float[] velocityState = velocityHistory[currentFrame] == null
                ? new float[4] { previousVelocityState[0], previousVelocityState[1], previousVelocityState[2], previousVelocityState[3] }
                : velocityHistory[currentFrame];

            velocityState[(int)Dimension2DManager.HorizontalAxis] = rigidbody.velocity.x;
            velocityState[(int)Dimension2DManager.VerticalAxis] = rigidbody.velocity.y;

            velocityHistory[currentFrame] = velocityState;

            if (currentFrame == nextFrame - 1)
            {
                return;
            }

            float[] nextState = velocityHistory[nextFrame];
            if (velocityState == null)
            {
                gameObject.SetActive(false);
                return;
            }

            float horizontal = nextState[(int)Dimension2DManager.HorizontalAxis];
            float vertical = nextState[(int)Dimension2DManager.VerticalAxis];
            Vector3 velocity = new Vector3(horizontal, vertical, 0.0f);
            rigidbody.velocity = velocity;
        }
    }
}
