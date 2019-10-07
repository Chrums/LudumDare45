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
            Dimension2DManager.DimensionChangeEvent += OnDimensionsChanged;

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
        private void UpdatePosition(Dimension2DManager.Dimension currentHorizontalDimension, Dimension2DManager.Dimension currentVerticalDimension, Dimension2DManager.Dimension nextHorizontalDimension, Dimension2DManager.Dimension nextVerticalDimension)
        {
            float[] previousPositionState = Dimension2DManager.CurrentFrame == 0
                ? null
                : positionHistory[Dimension2DManager.CurrentFrame - 1];

            float[] positionState = positionHistory[Dimension2DManager.CurrentFrame] == null
                ? new float[4] { previousPositionState[0], previousPositionState[1], previousPositionState[2], previousPositionState[3] }
                : positionHistory[Dimension2DManager.CurrentFrame];

            positionState[(int)currentHorizontalDimension] = transform.position.x;
            positionState[(int)currentVerticalDimension] = transform.position.y;

            positionHistory[Dimension2DManager.CurrentFrame] = positionState;

            float[] nextState = positionHistory[Dimension2DManager.CurrentFrame];

            float horizontal = nextState[(int)nextHorizontalDimension];
            float vertical = nextState[(int)nextVerticalDimension];
            Vector3 position = new Vector3(horizontal, vertical, 0.0f);
            transform.position = position;
        }

        private void UpdateVelocity(Dimension2DManager.Dimension currentHorizontalDimension, Dimension2DManager.Dimension currentVerticalDimension, Dimension2DManager.Dimension nextHorizontalDimension, Dimension2DManager.Dimension nextVerticalDimension)
        {
            float[] previousPositionState = Dimension2DManager.CurrentFrame == 0
                ? null
                : positionHistory[Dimension2DManager.CurrentFrame - 1];

            float[] positionState = positionHistory[Dimension2DManager.CurrentFrame] == null
                ? new float[4] { previousPositionState[0], previousPositionState[1], previousPositionState[2], previousPositionState[3] }
                : positionHistory[Dimension2DManager.CurrentFrame];

            positionState[(int)currentHorizontalDimension] = rigidbody.velocity.x;
            positionState[(int)currentVerticalDimension] = rigidbody.velocity.y;

            positionHistory[Dimension2DManager.CurrentFrame] = positionState;

            float[] nextState = positionHistory[Dimension2DManager.CurrentFrame];

            float horizontal = nextState[(int)nextHorizontalDimension];
            float vertical = nextState[(int)nextVerticalDimension];
            Vector3 position = new Vector3(horizontal, vertical, 0.0f);
            rigidbody.velocity = position;
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

        private void OnDimensionsChanged(Dimension2DManager.Dimension currentHorizontalDimension, Dimension2DManager.Dimension currentVerticalDimension, Dimension2DManager.Dimension nextHorizontalDimension, Dimension2DManager.Dimension nextVerticalDimension)
        {
            UpdatePosition(currentHorizontalDimension, currentVerticalDimension, nextHorizontalDimension, nextVerticalDimension);
            UpdateVelocity(currentHorizontalDimension, currentVerticalDimension, nextHorizontalDimension, nextVerticalDimension);
        }
    }
}
