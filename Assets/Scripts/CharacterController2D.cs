using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float right = Input.GetAxis("Horizontal");
        float down = Input.GetAxis("Vertical");
        gameObject.transform.Translate(right * speed, down * speed, 0);
    }
}
