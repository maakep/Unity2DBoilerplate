using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	private Rigidbody2D rigidbody;

	private float _audioTime = 0f;
	public float AudioInterval = 1f;

	private PlayerMovement _playerMovement;
	private bool _previouslyGrounded;

    private Animator animator;

    // Use this for initialization
    void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		_playerMovement = GetComponent<PlayerMovement>();
		_previouslyGrounded = _playerMovement.m_isGrounded;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(rigidbody.velocity.x) > 5 && _playerMovement.m_isGrounded)
        {
            animator.SetBool("PlayerRun", true);
            if (Time.time > _audioTime)
            {
                SoundManager.PlayRandom(this, "Footsteps", transform.position);
                _audioTime = Time.time + AudioInterval;
            }
        }

        else if (Mathf.Abs(rigidbody.velocity.x) < 5 && _playerMovement.m_isGrounded)
        {
            animator.SetBool("PlayerRun", false);
        }

            // Jump sounds
            if (_previouslyGrounded && !_playerMovement.m_isGrounded) {
            animator.SetBool("PlayerInAir", true);
			SoundManager.PlayRandom(this, "Jump", transform.position);
			//Debug.Log("Ungrounded");
		} else if(!_previouslyGrounded && _playerMovement.m_isGrounded) {
            animator.SetBool("PlayerInAir", false);
            SoundManager.PlayRandom(this, "Land", transform.position);
			//Debug.Log("Grounded");
		}

		_previouslyGrounded = _playerMovement.m_isGrounded;
	}
}
