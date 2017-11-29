using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{

    public float speed = 5;
    private Vector2 m_movement;
    private Rigidbody2D m_player_body;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        m_movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        m_player_body = this.GetComponent<Rigidbody2D>();
        m_player_body.velocity = m_movement * speed;
    }
}
