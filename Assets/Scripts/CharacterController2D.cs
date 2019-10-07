using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    public float speed = 2.0f;

    [SerializeField]
    public float jump = 100.0f;

    [SerializeField]
    private float groundedDistance = 0.2f;

    private new Rigidbody2D rigidbody = null;

    private new BoxCollider2D collider = null;

    [SerializeField]
    private bool isGrounded = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        RaycastHit2D cast = Physics2D.BoxCast(
            rigidbody.worldCenterOfMass,
            collider.size,
            0.0f,
            Vector2.down,
            groundedDistance,
            1 << LayerMask.NameToLayer("World"));

        isGrounded = cast.collider != null;

        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector2 velocity = rigidbody.velocity;

        velocity.x = horizontalAxis * speed;

        if (isGrounded && Input.GetAxis("Vertical") > 0)
        {
            velocity.y = jump;
        }

        rigidbody.velocity = velocity;

        int timePos;
        if (Dimension2DManager.HorizontalAxis == Dimension2DManager.Dimension.AxisT)
        {
            Vector2 pos = gameObject.transform.position;
            timePos = (int)gameObject.transform.position.x;
            Dimension2DManager.CurrentFrame = timePos;
            gameObject.transform.position = pos;
        }
        else if (Dimension2DManager.VerticalAxis == Dimension2DManager.Dimension.AxisT)
        {
            Vector2 pos = gameObject.transform.position;
            timePos = (int)gameObject.transform.position.y;
            Dimension2DManager.CurrentFrame = timePos;
            gameObject.transform.position = pos;
        }
    }

    private void OnDrawGizmos()
    {
        collider = GetComponent<BoxCollider2D>();

        if (collider == null)
        {
            return;
        }

        Gizmos.DrawCube(new Vector3(0.0f, (collider.size.y + groundedDistance) / -2.0f, 0.0f), new Vector3(collider.size.x, groundedDistance, 1.0f));
    }
}
