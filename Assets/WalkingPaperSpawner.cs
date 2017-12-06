using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPaperSpawner : MonoBehaviour
{

    private Vector3 _startPosition;
    [Header("Paper prefab")]
    public GameObject PaperPrefab;

    [Header("Movement")]
    public float movementSpeed = 0.01f;
    public enum moveDirection { Left, Right, Up, Down, Up_Left, Up_Right, Down_Left, Down_Right }
    public moveDirection MoveDirection = moveDirection.Right;
    public float movingDistance = 10;
    private Vector3 direction;
    private Vector3 currentDirection;


    void Start()
    {
        this._startPosition = this.transform.position;

        switch (MoveDirection)
        {
            case moveDirection.Left:
                this.direction = new Vector3(-1, 0, 0);
                break;
            case moveDirection.Right:
                this.direction = new Vector3(1, 0, 0);
                break;
            case moveDirection.Up:
                this.direction = new Vector3(0, 1, 0);
                break;
            case moveDirection.Down:
                this.direction = new Vector3(0, -1, 0);
                break;
            case moveDirection.Up_Left:
                this.direction = new Vector3(-0.5f, 0.5f, 0);
                break;
            case moveDirection.Up_Right:
                this.direction = new Vector3(0.5f, 0.5f, 0);
                break;
            case moveDirection.Down_Left:
                this.direction = new Vector3(-0.5f, -0.5f, 0);
                break;
            case moveDirection.Down_Right:
                this.direction = new Vector3(0.5f, -0.5f, 0);
                break;
            default:
                this.direction = new Vector3(1, 0, 0);
                break;
        }
        this.currentDirection = direction;
    }

    void FixedUpdate()
    {
        if (this.transform.position == _startPosition)
            currentDirection = direction;
        else if ((this.transform.position - _startPosition).magnitude > movingDistance)
            currentDirection = direction * -1;

        this.transform.position = this.transform.position + currentDirection * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.Player)
        {
            GameObject go = (GameObject)Instantiate(PaperPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
