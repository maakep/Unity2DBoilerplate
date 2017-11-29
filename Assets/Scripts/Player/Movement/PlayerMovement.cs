using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour {

	// Serialized
	[Header("Moving")]
	public float m_movementSpeed = 10;
	public bool m_movementSmoothing = false;
	[Space(10)]
	[Header("Jumping")]
	public int m_maxJumps = 1;
	public float m_jumpForce = 20;
	[Range(0, 1)]
	public float m_smallJumpModifier = 0.3f;
	public float m_fallForce = 20;
	public bool m_requireGroundToJump = false;
	public bool m_enableWallJump = true;
	public bool m_bufferJumps = true;
	[Range(0, 1)]
	public float m_bufferJumpTime = 0;
	[Space(10)]
	[Header("Misc/Settings")]

	[SerializeField]
	private LayerMask m_layerMask;

	// Privates
	private int m_jumpsLeft = 1;
	private bool m_shouldJump = false;
	private bool m_jumpPressed = false;
	private bool m_jumpReleased = true;
	private bool m_isGrounded = true;
	private Vector2 m_movement;



	// Components
	Rigidbody2D m_rb;
	SpriteRenderer m_sprite;

	void Start () {
		 m_rb = GetComponent<Rigidbody2D>();
		 m_sprite = GetComponent<SpriteRenderer>();

		 #if UNITY_EDITOR
		 if (gameObject.layer != Layers.Player) {
			 Debug.LogError(name + " must be on layer called 'Player'. Is currently on layer: '" + LayerMask.LayerToName(gameObject.layer) + "'");
		 }
		 #endif
	}
	
	void Update () {
		// Get movement input
		m_movement = new Vector2(
			(m_movementSmoothing ? Input.GetAxis("Horizontal") : Input.GetAxisRaw("Horizontal")) * m_movementSpeed,
			m_rb.velocity.y
		);

		// Get jump input states
		if (Input.GetButtonDown(KeyMappings.Jump)) {
			m_shouldJump = true;
			m_jumpReleased = false;
			m_jumpPressed = true;
		}
		if (Input.GetButtonUp(KeyMappings.Jump)) {
			m_jumpReleased = true;
		}
	}

	private bool m_decreaseJumpsOnce = true;
	private bool m_airborneBecauseOfJump = false;
	private bool m_usedRightWalljump = false;
	private bool m_usedLeftWalljump = false;
	
	void FixedUpdate() {
		var groundDetected = ScanForGround();
		var rightWallDetected = ScanForRightWall();
		var leftWallDetected = ScanForLeftWall();
		var wallDetected = rightWallDetected || leftWallDetected;

		// Handle ground states
		if (groundDetected) {
			m_jumpsLeft = m_maxJumps;
			m_isGrounded = true;
			m_decreaseJumpsOnce = true;
			m_airborneBecauseOfJump = false;
			m_usedLeftWalljump = false;
			m_usedRightWalljump = false;
		} else {
			m_isGrounded = false;
			
			// When walking out an edge with requireGroundToJump setting one jump should be removed
			// which results in no jumps if single jump & 1 jump if double jump, etc
			if (m_requireGroundToJump && !m_airborneBecauseOfJump && m_decreaseJumpsOnce) {
				m_jumpsLeft--;
				m_decreaseJumpsOnce = false;
			}
		}

		// Handle jump
		if (m_shouldJump) {
			var doJump = false;

			// Jumps left, or have Wall jump setting enabled
			if (m_jumpsLeft > 0 || m_enableWallJump && wallDetected) {
				if (m_enableWallJump && !m_isGrounded && wallDetected) {
					if (rightWallDetected && !m_usedRightWalljump) { // Right wall jump
						m_usedRightWalljump = true;
						m_usedLeftWalljump = false;
						m_jumpsLeft++;
						doJump = true;
					} else if (leftWallDetected && !m_usedLeftWalljump) { // Left wall jump
						m_usedRightWalljump = false;
						m_usedLeftWalljump = true;
						m_jumpsLeft++;
						doJump = true;
					} else if (m_jumpsLeft > 0) {
						doJump = true;
					}
				} else {
					doJump = true;
				}
			}
			
			// Jump buffer setting
			if (!m_bufferJumps) {
				m_shouldJump = false;
			} else {
				if (!m_bufferJumpIsRunning && m_jumpPressed && m_shouldJump) {
					StartCoroutine(BufferJump());
				}
			}
			
			if (doJump) {
				PerformJump();
			}

			m_jumpPressed = false;
		}
		
		// Alter jump height
		if (m_jumpReleased) {
			PerformSmallJump();
		}

		// Apply falling force to combat Unity float
		if (!m_isGrounded) {
			m_rb.AddForce(Vector2.down * m_fallForce);
		}

			// Apply movement
			if (!m_isGrounded && (rightWallDetected && m_movement.x > 0 || leftWallDetected && m_movement.x < 0)) {
				m_movement.x = 0;
			}
			m_rb.velocity = m_movement;
	}

	void PerformJump(bool fullJump = true) {
		m_movement.y = m_jumpForce;
		m_airborneBecauseOfJump = true;
		m_jumpsLeft--;
		m_shouldJump = false;
	}

	void PerformSmallJump() {
		if (m_rb.velocity.y > m_jumpForce * m_smallJumpModifier)
			m_movement.y = m_jumpForce * m_smallJumpModifier;
	}

	private bool m_bufferJumpIsRunning = false;
	IEnumerator BufferJump() {
		m_bufferJumpIsRunning = true;
		yield return new WaitForSeconds(m_bufferJumpTime);
		m_shouldJump = false;
		m_bufferJumpIsRunning = false;
	}

	bool ScanForGround() {
		// +- 0.1f to bring it in from the edges a bit, the down-raycasts would register on walls when
		// moving towards it otherwise
		var btmLeftEdgeOfPlayer = new Vector2(m_sprite.bounds.min.x + 0.1f, m_sprite.bounds.min.y);
		var btmRightEdgeOfPlayer = new Vector2(m_sprite.bounds.max.x - 0.1f, m_sprite.bounds.min.y);

		var objectBelow = Physics2D.Raycast(btmLeftEdgeOfPlayer, Vector2.down, 0.02f, m_layerMask);
		if (objectBelow.collider == null) {
			objectBelow = Physics2D.Raycast(btmRightEdgeOfPlayer, Vector2.down, 0.02f, m_layerMask);
		}

		return objectBelow.collider != null && objectBelow.collider.tag == Tags.Ground;
	}
	bool ScanForRightWall() {
		// +- 0.1f to bring it in from the edges a bit, the down-raycasts would register on walls when
		// moving towards it otherwise
		var rightTopEdgeOfPlayer  = new Vector2(m_sprite.bounds.max.x, m_sprite.bounds.max.y - 0.1f);
		var rightBtmEdgeOfPlayer  = new Vector2(m_sprite.bounds.max.x, m_sprite.bounds.min.y + 0.1f);

		Debug.DrawRay(rightTopEdgeOfPlayer, Vector2.right * 0.02f, Color.red);
		Debug.DrawRay(rightBtmEdgeOfPlayer, Vector2.right * 0.02f, Color.red);

		var posToCheck = new Vector3[]{rightTopEdgeOfPlayer, rightBtmEdgeOfPlayer};
		
		foreach(var pos in posToCheck) {
			var wallTouched = Physics2D.Raycast(pos, Vector2.right, 0.02f, m_layerMask);

			if (wallTouched.collider != null && wallTouched.collider.tag == Tags.Ground) {
				m_usedLeftWalljump = false;
				return true;
			}
		}
		return false;
	}
	bool ScanForLeftWall() {
		// +- 0.1f to bring it in from the edges a bit, the down-raycasts would register on walls when
		// moving towards it otherwise
		var leftTopEdgeOfPlayer  = new Vector2(m_sprite.bounds.min.x, m_sprite.bounds.max.y - 0.1f);
		var leftBtmEdgeOfPlayer  = new Vector2(m_sprite.bounds.min.x, m_sprite.bounds.min.y + 0.1f);
		Debug.DrawRay(leftTopEdgeOfPlayer, -Vector2.right * 0.02f, Color.red);
		Debug.DrawRay(leftBtmEdgeOfPlayer, -Vector2.right * 0.02f, Color.red);
		var posToCheck = new Vector3[]{leftTopEdgeOfPlayer, leftBtmEdgeOfPlayer};
		foreach(var pos in posToCheck) {
			var wallTouched = Physics2D.Raycast(pos, -Vector2.right, 0.02f, m_layerMask);

			if (wallTouched.collider != null && wallTouched.collider.tag == Tags.Ground) {
				m_usedRightWalljump = false;
				return true;
			}
		}
		return false;
	}
}




