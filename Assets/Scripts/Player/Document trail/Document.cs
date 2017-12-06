using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(DistanceJoint2D), typeof(LineRenderer))]
public class Document : MonoBehaviour, IInteractable
{
    public DocumentTypes DocumentType;
    public Vector2 SpawnPosition { get; set; }
    public GameObject Parent { get; set; }

    private DistanceJoint2D joint;
    private LineRenderer line;
    private Animator _animator;
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        line = GetComponent<LineRenderer>();
        _animator = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(Layers.Documents, Layers.Documents, true);
        SpawnPosition = this.transform.position;
        Debug.Log("Doc type: " + DocumentType);

        if (DocumentType == DocumentTypes.Red) {
            GetComponent<SpriteRenderer>().color = Color.red;
        } else if(DocumentType != DocumentTypes.White) {
            DocumentType = DocumentTypes.White;
        }
    }

    public void Interact(GameObject sender)
    {
        var pu = sender.GetComponent<PickUpDocuments>();
        if (pu != null && !pu.m_DocumentTrail.Contains(gameObject))
        {
            pu.AddDocument(this);
            this.tag = Tags.Untagged;
            SoundManager.Play(this, "paper_pickup", transform.position);
        }
    }

    void Update()
    {
        joint.enabled = (joint.connectedBody != null);
        if (line != null)
        {
            line.enabled = (joint.connectedBody != null);
            line.SetPosition(0, this.transform.position);
            if (Parent != null) {
                line.SetPosition(1, Parent.transform.position);
                _animator.SetBool("PickUp", true);
            }
        }
    }
}

public enum DocumentTypes
{    
    White,
    Red,
}