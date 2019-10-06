using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float speed = 2f;
    public float jump = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float right = Input.GetAxis("Horizontal");
        var cast = Physics2D.BoxCast(gameObject.GetComponent<Rigidbody2D>().worldCenterOfMass, gameObject.GetComponent<BoxCollider2D>().size, 0, Vector2.down, 10, 1 << LayerMask.NameToLayer("World"));
        bool willJump = Input.GetAxis("Vertical") > 0 && cast.rigidbody != null && cast.distance < gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.02f && gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(right * speed, willJump ? jump : gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }
}
