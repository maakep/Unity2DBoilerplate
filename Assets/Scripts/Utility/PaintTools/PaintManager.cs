using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : MonoBehaviour {

	public int m_paint_precision = 10;
	bool m_painting;
	bool m_paint_between;
	Vector2 m_last_paint;

	void Start () {
		m_painting = false;
		m_paint_between = false;
		m_last_paint = new Vector2(0,0);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)){ // 0 = left mousebutton pressed
			
			m_painting = true;
			
		}

		if (Input.GetMouseButtonUp(0)){ // 0 = left mousebutton released
			m_painting = false;
			m_paint_between = m_painting;
		}


		if (m_painting){ 
			Vector2 mouse_position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 mouse_in_world = Camera.main.ScreenToWorldPoint(mouse_position);
			RaycastHit2D hit = Physics2D.Raycast(mouse_in_world, Vector2.zero);

			if (hit.collider != null) {
    			if (hit.collider.gameObject.tag == "PaintField"){
					GameObject paint_field = hit.collider.gameObject;
					paint_field.GetComponent<PaintField>().PaintMousePosition(mouse_in_world);
					
					if (m_paint_between){
						PaintBetween(mouse_in_world - m_last_paint , paint_field);
					}
					m_last_paint = mouse_in_world;
					m_paint_between = m_painting;
				} else {
					m_paint_between = false;
				}  // theese two false fix bug "Holding down, leave the box, enter another place when still holding".
			} else {
				m_paint_between = false;
			}
		}
	
	}


	public void PaintBetween(Vector2 to_paint_vector, GameObject paint_field){
		for (int i = 1; i < m_paint_precision; i++){
			paint_field.GetComponent<PaintField>().PaintMousePosition(m_last_paint + i * to_paint_vector / m_paint_precision);
		}
	}


	public static float CompareFields(int[,] f1, int[,] f2)
	{
			//compares each field of the two matrix, and then return the procent that matches.
			int same = 0;
			if(f1.GetLength(0) != f2.GetLength(0) || f1.GetLength(1) != f2.GetLength(1))
				Debug.Log("fields are not of same size, this is a bug. Either handmade initialization is wrong or automatic detection of squarematrice, pls fix");

			for (int i = 0; i < f1.GetLength(0); i++)
			{
					for (int j = 0; j < f1.GetLength(1); j++)
					{
							same += f1[i, j] == f2[i, j] ? 1 : 0;
					}
			}
	float length = f1.GetLength(0) * f1.GetLength(1);
	return same / length;
	}


	public static bool CheckAllFields(){
		GameObject[] fields = GameObject.FindGameObjectsWithTag("PaintField");
		foreach(GameObject field in fields){
			if (!field.GetComponent<PaintField>().CheckField()){
				return false;
			}
		}
		return true;
	}

	public static void ClearAllFields() {
		GameObject[] fields = GameObject.FindGameObjectsWithTag("PaintField");
		foreach(GameObject field in fields) {
			field.GetComponent<PaintField>().ClearField();
		}
	}

	public static int PaintUsed(){
		GameObject[] fields = GameObject.FindGameObjectsWithTag("PaintField");
		int used_paint = 0;
		foreach(GameObject field in fields){
			used_paint += field.GetComponent<PaintField>().PaintUsed();
		}
		return used_paint;
	}


}
