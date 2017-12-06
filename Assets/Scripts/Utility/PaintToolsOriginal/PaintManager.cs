using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : MonoBehaviour {
	bool m_painting;

	void Start () {
		m_painting = false;
	}


	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)){ // 0 = left mousebutton pressed
			m_painting = true;
			
		}

		if (Input.GetMouseButtonUp(0)){ // 0 = left mousebutton released
			m_painting = false;
		}



		if (m_painting){  
			//Might paint faster than when in fixed update.. Not fast enough though. COuld be solved by calculating the distance from last paint til
			//this paint, and check if m_paint is true between the paint. IF so, then add paint in that direction. Estimated time for this fix: 3-6h.
			Vector2 mouse_position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 mouse_in_world = Camera.main.ScreenToWorldPoint(mouse_position);
			RaycastHit2D hit = Physics2D.Raycast(mouse_in_world, Vector2.zero);

			if (hit.collider != null) {
    			if (hit.collider.gameObject.name == "PaintField"){
					GameObject paint_field = hit.collider.gameObject;
					//Debug.Log(paint_field.transform.position.x - paint_field.GetComponent<SpriteRenderer>().bounds.size.x/2 + mouse_in_world.x);
					paint_field.GetComponent<PaintField>().PaintMousePosition(mouse_in_world);
					
				}
			}
			//Instantiate(m_paint_transform, mouse_in_world, m_paint_transform.rotation);
		}
	
	}


    public static float CompareFields(int[,] f1, int[,] f2)
    {
        //compares each field of the two matrix, and then return the procent that matches.
        int same = 0;
        if(f1.Length != f2.Length || f1.GetLength(0) != f2.GetLength(0))
        Debug.Log("fields are not of same size, this is a bug. Either handmade initialization is wrong or automatic detection of squarematrice, pls fix");

        for (int i = 0; i < f1.Length; i++)
        {
            for (int j = 0; j < f1.GetLength(0); j++)
            {
                same += f1[i, j] == f2[i, j] ? 1 : 0;
            }
        }
		float length = f1.Length * f1.GetLength(0);
		return same / length;
    }


}
