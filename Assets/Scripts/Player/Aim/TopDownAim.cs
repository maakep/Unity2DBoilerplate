using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAim : AbstractPlayerAim {


    public bool m_aim_position_at_mouse = false;
	private Vector2 m_mouse;

    protected override void ExtendedStart() {

    }

    protected override void MoveAim()
    {
        if(!m_aim_position_at_mouse){
        float parent_x = m_parent.transform.position.x;
        float parent_y = m_parent.transform.position.y;
        float distance = m_distance_to_parent;

        float x = Mathf.Cos((m_degree) * Mathf.Deg2Rad) * distance + parent_x;
        float y = Mathf.Sin((m_degree) * Mathf.Deg2Rad) * distance + parent_y;
        this.transform.position = new Vector3(x, y, this.transform.position.z);
        } else {
            this.transform.position = new Vector3(m_mouse.x, m_mouse.y, this.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
		m_mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FireShot();
        
	}

    void FixedUpdate()
    {
		m_degree = Mathf.Atan2(m_mouse.y - m_parent.transform.position.y, m_mouse.x - m_parent.transform.position.x) * Mathf.Rad2Deg;
        MoveAim();
    }
}
