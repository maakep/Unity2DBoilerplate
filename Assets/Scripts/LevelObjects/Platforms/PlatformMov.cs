using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class PlatformMov : MonoBehaviour
{

    //[Space(5)]
    [Header("Startvalues")]
    private Vector3 _startPosition;
    // private Vector3 _startScale;
    // private Quaternion _startRotation;

    [Header("Movement")]
    public float movementSpeed = 0.01f;
    public enum moveDirection { Left, Right, Up, Down, Up_Left, Up_Right, Down_Left, Down_Right }
    public moveDirection MoveDirection = moveDirection.Right;
    public float movingDistance = 10;
    private Vector3 direction;
    private Vector3 currentDirection;


    public Vector3 rotationValues = new Vector3(0, 0, 0);
    public float roationFactor = 0;

    public Vector3 finalScale = new Vector3(0, 0, 0);
    public float scaleFactor = 0;

    [Header("Properties")]
    public bool isSlippery = false;
    public bool weightAffectedByPassenger = false; //Tanken här är ifall man lägger en platform på vatten och den ska sjunka ner lite när det står någon på den
    public bool automaticScaling = false; //Tanken här är ifall man ska öka/minska storleken på platformen av någon anledning
    public bool automaticRotation = false; //Tanken här är ifall platformen ska rotera per automatik (dvs inte av någon trigger)
    public bool rotateBackAndForth = false; //Tanken här är att platformen kan rotera till en viss vinkel, sedan roterar den tillbaka


    public void Start()
    {
        this._startPosition = this.transform.position;
        // this._startScale = this.transform.localScale;
        // this._startRotation = this.transform.localRotation;

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

    // Update is called once per frame
    public void Update()
    {

    }

    public void FixedUpdate()
    {
        MovePlatform();
        ScalePlatform();
        RotatePlatform();
    }

    public void MovePlatform()
    {
        if (this.transform.position == _startPosition)
            currentDirection = direction;
        else if ((this.transform.position - _startPosition).magnitude > movingDistance)
            currentDirection = direction * -1;

        this.transform.position = this.transform.position + currentDirection * movementSpeed;
    }

    public void ScalePlatform() { }
    public void RotatePlatform() { }

    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("TriggerStay");
        if (other.gameObject.tag == "Player")
            other.transform.parent = this.transform;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("TriggerExit");
        if (other.gameObject.tag == "Player")
            other.transform.parent = null;
    }
}
