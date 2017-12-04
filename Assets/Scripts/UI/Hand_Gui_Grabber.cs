using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Gui_Grabber : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 destination;
    public GameObject objectToGrab;
    public bool currentlyHandlingItem;
    public float speed = 1.0f;

    private Animator _animator;

    void Start()
    {
        this.startPosition = transform.position;
        //this.GetComponentInChildren<Animator>().enabled = false;
        _animator = GetComponentInChildren<Animator>();
        //this.GetComponentInChildren<Animator>().speed = 1 > (this.speed - 5) ? 1 : (this.speed - 5) + 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !currentlyHandlingItem && PlayerStats.GoldCount > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "Grabbable")
            {
                currentlyHandlingItem = true;
                objectToGrab = hit.collider.gameObject;
                transform.position = this.startPosition = new Vector2(objectToGrab.transform.position.x, objectToGrab.transform.position.y + 10);
                destination = objectToGrab.transform.position;
                SoundManager.Play(this, "hand_pickup", transform.position);
                PlayerStats.GoldCount--;
            }
        }
        var step = speed * Time.deltaTime;
        if (destination != Vector2.zero)
            transform.position = Vector2.MoveTowards(transform.position, destination, step);

        if (objectToGrab != null)
            if (Vector2.Distance(objectToGrab.transform.position, this.transform.position) <= 0.7f)
            {
                //this.GetComponentInChildren<Animator>().enabled = true;
                //this.GetComponentInChildren<Animator>().StartPlayback();
                _animator.SetBool("Grab", true);

                destination = startPosition;
                objectToGrab.transform.SetParent(this.transform);
                objectToGrab.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            }

        if ((Vector2)transform.position == startPosition && objectToGrab != null)
        {
            currentlyHandlingItem = false;
            GameObject.Destroy(objectToGrab);
            PlayerStats.DocumentsDepositedCount++;
            objectToGrab = null;
            transform.position = this.startPosition = this.destination = new Vector2(200, 200);
            //this.GetComponentInChildren<Animator>().enabled = false;
            _animator.SetBool("Grab", false);
        }
    }
}
