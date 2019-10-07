using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fizz6.LudumDare45
{
    public class BaseObject : MonoBehaviour
    {
        public bool Achronal = false;

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
            UpdateState(currentFrame, nextFrame, Dimension2DManager.HorizontalAxis, Dimension2DManager.VerticalAxis, Dimension2DManager.HorizontalAxis, Dimension2DManager.VerticalAxis);
        }

        private void UpdateState(int currentFrame, int nextFrame, Dimension2DManager.Dimension currentHorizontalDimension, Dimension2DManager.Dimension currentVerticalDimension, Dimension2DManager.Dimension nextHorizontalDimension, Dimension2DManager.Dimension nextVerticalDimension)
        {
            float[] previousPositionState = currentFrame == 0
                ? null
                : positionHistory[currentFrame - 1];
            float[] previousVelocityState = currentFrame == 0
                ? null
                : velocityHistory[currentFrame - 1];

            float[] positionState = positionHistory[currentFrame] == null
                ? new float[4] { previousPositionState[0], previousPositionState[1], previousPositionState[2], previousPositionState[3] }
                : positionHistory[currentFrame];

            float[] velocityState = velocityHistory[currentFrame] == null
                ? new float[4] { previousVelocityState[0], previousVelocityState[1], previousVelocityState[2], previousVelocityState[3] }
                : velocityHistory[currentFrame];

            positionState[(int)currentHorizontalDimension] = transform.position.x;
            positionState[(int)currentVerticalDimension] = transform.position.y;

            velocityState[(int)currentHorizontalDimension] = rigidbody.velocity.x;
            velocityState[(int)currentVerticalDimension] = rigidbody.velocity.y;

            positionHistory[currentFrame] = positionState;
            velocityHistory[currentFrame] = velocityState;

            if (currentFrame == nextFrame - 1)
            {
                return;
            }

            float[] nextPositionState = positionHistory[nextFrame];
            float[] nextVelocityState = velocityHistory[nextFrame];
            if (positionState == null || velocityState == null) // TODO: fix
            {
                gameObject.SetActive(false);
                return;
            }

            if (!(Achronal && (nextHorizontalDimension == Dimension2DManager.Dimension.AxisT || nextVerticalDimension == Dimension2DManager.Dimension.AxisT)))
            {
                float horizontalPosition = nextPositionState[(int)nextHorizontalDimension];
                float verticalPosition = nextPositionState[(int)nextVerticalDimension];
                Vector3 position = new Vector3(horizontalPosition, verticalPosition, 0.0f);
                transform.position = position;

                float horizontalVelocity = nextVelocityState[(int)nextHorizontalDimension];
                float verticalVelocity = nextVelocityState[(int)nextVerticalDimension];
                Vector3 velocity = new Vector3(horizontalVelocity, verticalVelocity, 0.0f);
                rigidbody.velocity = velocity;
            }
        }

        private void OnDimensionsChanged(Dimension2DManager.Dimension currentHorizontalDimension, Dimension2DManager.Dimension currentVerticalDimension, Dimension2DManager.Dimension nextHorizontalDimension, Dimension2DManager.Dimension nextVerticalDimension)
        {
            UpdateState(Dimension2DManager.CurrentFrame, Dimension2DManager.CurrentFrame, currentHorizontalDimension, currentVerticalDimension, nextHorizontalDimension, nextVerticalDimension);
        }
    }
}
