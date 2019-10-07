using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fizz6.LudumDare45
{
    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField]
        private Vector2 speed = Vector2.zero;

        [SerializeField]
        private int period = 120;

        private new Rigidbody2D rigidbody = null;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rigidbody.velocity = new Vector2(Dimension2DManager.CurrentFrame % period < period / 2 ? -speed.x : speed.x, Dimension2DManager.CurrentFrame % period < period / 2 ? -speed.y : speed.y);
        }
    }
}
