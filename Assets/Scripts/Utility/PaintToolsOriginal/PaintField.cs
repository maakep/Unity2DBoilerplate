using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintField : MonoBehaviour
{

    public int x_length;
    public int y_length;
    public Transform m_paint;
    public int m_max_colors = 15;
    Transform m_field;


    int[,] m_field_matrix; // int and not bool since many colors MIGHT be allowed
    int[,] m_init_field_matrix; //predefined field for either comparison or if one wants to paint an initial state
    Transform[,] m_field_instances;

    Vector2 m_corner;
    Vector2 m_paint_size;
    Vector2 m_field_size;

    Vector2 last_paint_pos;



    void Start()
    {
        //initialize mfieldmatrice, code on computer
        m_field = this.transform;

        m_field_size = m_field.GetComponent<SpriteRenderer>().bounds.size;
        m_paint_size = m_paint.GetComponent<SpriteRenderer>().bounds.size;

        if (Mathf.RoundToInt(m_field_size.x*100)%Mathf.RoundToInt(m_paint_size.x*100) != 0 || Mathf.RoundToInt(m_field_size.y*100)%Mathf.RoundToInt(m_paint_size.y*100) != 0){
            Debug.Log("The size of the paint MUST be a divisible of the size of the field!!");
        }

        x_length = Mathf.RoundToInt(m_field_size.x/m_paint_size.x);
        y_length = Mathf.RoundToInt(m_field_size.y/m_paint_size.y);
        m_corner = (Vector2)m_field.position - new Vector2(m_field_size.x / 2, m_field_size.y / 2);

        m_field_matrix = new int[x_length, y_length];
        m_init_field_matrix = new int[x_length, y_length];
        m_field_instances = new Transform[x_length, y_length];
        for (int i = 0; i < m_field_matrix.GetLength(0); i++)
        {
            for (int j = 0; j < m_field_matrix.GetLength(1); j++)
            {
                m_field_matrix[i, j] = 0;
                m_init_field_matrix[i, j] = 0;
                m_field_instances[i, j] = null;
            }
        }

        last_paint_pos = new Vector2(-1, -1);

       
    }

    //function which calls this is done on computer
    public bool PaintMousePosition(Vector2 mousepos)
    {
        Vector2 where_on_field = mousepos - m_corner;

        Vector2 click = where_on_field - new Vector2(where_on_field.x % m_paint_size.x, where_on_field.y % m_paint_size.y); // (check wheter % is remainder or mod in c#)

        Vector2 pos = DetectField(click);

        return Paint(pos);
    }


    public Vector2 DetectField(Vector2 click)
    {
        //Calculates what field is pressed

        Vector2 square = new Vector2(click.x / m_paint_size.x, click.y / m_paint_size.y); //m_paint_size SHOULD divide click, making the result an "int" (still a float but with .0.... ) !! Might RETURN TOO high value (array put of bound later) if a perfect click at m_field_size is performed. NEED TO TEST THIS


        return square;
    }


    public void UpdateField(Vector2 pos, int value = 1)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        m_field_matrix[x, y] = value % m_max_colors;
    }

    public void ChangeColor(Vector2 pos){
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);

        Color color = new Color((m_field_matrix[x,y] * 20f % 256f) /256f, (m_field_matrix[x,y] * 40f % 256f)/256f, (m_field_matrix[x,y] * 60 %256)/256, 1);

        m_field_instances[x,y].GetComponent<SpriteRenderer>().color = color;
    }


    public bool Paint(Vector2 pos)
    {
        //refactor paintmouseposition to use this function instead. DONE
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        if (x == Mathf.RoundToInt(last_paint_pos.x) && y == Mathf.RoundToInt(last_paint_pos.y)){
            return false;
        } else {
            last_paint_pos = new Vector2(x,y);
        }


        if (m_field_instances[x, y] == null)
        {

            Vector2 paint_pos = m_corner + new Vector2(x * m_paint_size.x + m_paint_size.x/2, y * m_paint_size.y + m_paint_size.y/2);

            Transform instance = Instantiate(m_paint, paint_pos, m_paint.rotation); // should paint_pos be in corner or in the middle? Currently it spawns in the corner, add m_paint_size/2 otherwise
            
            instance.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.2f, 0.9f);
            instance.SetParent(this.transform, true);
            m_field_instances[x, y] = instance;
            UpdateField(pos);
            ChangeColor(pos);
        } else {
            UpdateField(pos, m_field_matrix[x, y] + 1);
            ChangeColor(pos);
        }
        return true;
    }


    public void InitializeField(int[,] field)
    {
        //sets the initialize field to field -- could be used for either debugging, comparing or paint an initial field.
        m_init_field_matrix = field;
    }

    public void PaintInitField()
    {
        //paints the initial field and updates the m_field to the newly painted one.
        for (int i = 0; i < m_field_matrix.GetLength(0); i++)
        {
            for (int j = 0; j < m_field_matrix.GetLength(1); j++)
            {
                if (m_init_field_matrix[i,j] == 1)
                    Paint(new Vector2(i, j));
            }
        }
    }

    public float CompareFields()
    {
        //Compares init field against m_field. If you want to compare the field against other, then just use the static PaintManager function.
        return PaintManager.CompareFields(m_field_matrix, m_init_field_matrix);
    }


    public int[,] GetField()
    {
        return m_field_matrix;
    }

}


  

