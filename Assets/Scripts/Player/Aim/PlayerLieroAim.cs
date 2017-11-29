using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLieroAim : AbstractPlayerAim
{


    public float m_aim_speed = 60f;

    private float m_horizontal;
    private float m_vertical;

    protected override void ExtendedStart() {

    }

    protected override void MoveAim()
    {
        float parent_x = m_parent.transform.position.x;
        float parent_y = m_parent.transform.position.y;
        float distance = m_distance_to_parent;

        float x = Mathf.Cos((m_degree - 90) * Mathf.Deg2Rad) * distance + parent_x;
        float y = Mathf.Sin((m_degree - 90) * Mathf.Deg2Rad) * distance + parent_y;
        this.transform.position = new Vector3(x, y, this.transform.position.z);
    }

    void TurnAround()
    {
        m_degree = Mathf.Abs(m_degree) * m_direction;
    }


    void Update()
    {
        m_vertical = Input.GetAxisRaw(m_keymappings["vertical"]);
        m_horizontal = Input.GetAxisRaw(m_keymappings["horizontal"]);
        
        float vertical = m_vertical;

        if ((Mathf.Abs(m_degree) > m_min_aim_degree && vertical < -0.1) || (m_direction * m_degree < m_max_aim_degree && vertical > 0.1))
        {
            m_degree += m_direction * m_aim_speed * Time.deltaTime * vertical;
        }
        
        FireShot();
    }

    void FixedUpdate()
    {
        float horizontal = m_horizontal;

        MoveAim();

        if ((horizontal > 0 && m_direction != 1) || (horizontal < 0 && m_direction != -1))
        {
            m_direction *= -1;
            TurnAround();
        }
    }
}
