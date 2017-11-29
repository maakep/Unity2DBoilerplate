using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayerAim : MonoBehaviour {

    public float m_distance_to_parent = -1;
    public int m_max_distance = 10;

    public int m_power_increase = 3;
    public GameObject weapon;
    public float m_bullet_modifier = 100;
    public float m_bullet_spawn_distance_modifier = 0.5f;
    public float m_reload_time = 0.2f;



    protected Dictionary<string, string> m_keymappings = new Dictionary<string, string>()
    {
        {"horizontal", "Horizontal"},
        {"vertical", "Vertical"},
        {"fire", "Fire1"}
    };
    protected Transform m_parent;
    protected int m_max_aim_degree = 180; //degrees, 0 = down, 180 = up
    protected int m_min_aim_degree = 30;
    protected float m_degree;
    protected int m_direction;
    protected float m_normal_distance;
    protected float m_last_shot = 0;


	// Use this for initialization
	void Start (){
		 m_parent = m_parent ?? this.transform.parent;
        if (m_parent == null)
        {
            throw new UnityException("PlayerLieroAim has no parent");
        }

        float parent_x = m_parent.transform.position.x;
        float parent_y = m_parent.transform.position.y;
        float aim_x, aim_y;
        aim_x = this.transform.position.x - parent_x;
        aim_y = this.transform.position.y - parent_y;

        m_degree = Mathf.Atan2(aim_y, aim_x) * Mathf.Rad2Deg - 270; // Setting m_degree +90 to set North to be degree == 0, -360 for it to be at the right intervals


        if (m_distance_to_parent == -1)
        {
            m_distance_to_parent = Mathf.Sqrt(aim_y * aim_y + aim_x * aim_x);
        }
        else
        {
            this.transform.position = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (m_degree - 90)) * m_distance_to_parent,
                                                  Mathf.Sin(Mathf.Deg2Rad * (m_degree - 90)) * m_distance_to_parent,
                                                  this.transform.position.z);
        }

        m_normal_distance = m_distance_to_parent;
        m_direction = (aim_x > 0) ? 1 : -1;

        

        ExtendedStart();
	}

    protected abstract void ExtendedStart();
	

    protected void Shoot(float power)
    {
        Vector2 p_player = m_parent.transform.position;
        Vector2 p_aim = this.gameObject.transform.position;

        float angle = Mathf.Atan2(p_aim.y - p_player.y, p_aim.x - p_player.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0, 0, angle - 90);
        Vector2 spawn_direction = new Vector2((p_aim.x - p_player.x), (p_aim.y - p_player.y)).normalized * m_bullet_spawn_distance_modifier;

        Vector2 p_bullet = p_player + spawn_direction;

        GameObject bullet = Instantiate(weapon, p_bullet, q);
        Rigidbody2D bullet_body = bullet.GetComponent<Rigidbody2D>();
        bullet_body.AddForce(bullet.transform.up * power * m_bullet_modifier);
    }

    protected abstract void MoveAim();

	protected void FireShot(){
        if(m_last_shot + m_reload_time< Time.time){
            if (Input.GetButton(m_keymappings["fire"]))
            {
                if (m_distance_to_parent < m_max_distance)
                {
                    m_distance_to_parent += m_power_increase * Time.deltaTime;
                }
            }

            if (Input.GetButtonUp(m_keymappings["fire"]))
            {
                Shoot(m_distance_to_parent - m_normal_distance);
                m_distance_to_parent = m_normal_distance;
                m_last_shot = Time.time;
            }
        }
	}
}
