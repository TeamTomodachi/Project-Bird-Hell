using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public const float PLAYER_JUMP_SPEED = 6.0f;
	public const float PLAYER_MOVEMENT_SPEED = 8.0f;

	[SerializeField]
	public PlayerInfo Player = new PlayerInfo();

	//CharacterController m_characterController;
	Rigidbody2D m_rigidbody2D;
	SpriteRenderer m_spriteRenderer;

	// Use this for initialization
	void Start () {
		//m_characterController = GetComponent<CharacterController>();
		m_rigidbody2D = GetComponent<Rigidbody2D>();
		m_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {        
	}

	// Called once per Physics Update
	private void FixedUpdate()
	{
		float horizontalMovement = Input.GetAxis(Player.ControllerPrefix + "Horizontal");
        // Flip the sprite to face the direction of movement 
        if (horizontalMovement > 0.001f) { m_spriteRenderer.flipX = false; } // Flip Right
        else if (horizontalMovement <= -0.001f) { m_spriteRenderer.flipX = true; } // Flip Left

		if (Input.GetButtonDown(Player.ControllerPrefix+"Jump"))
		{
			Debug.Log("Jump");
			var vel = m_rigidbody2D.velocity;
			vel.y = PLAYER_JUMP_SPEED;
			m_rigidbody2D.velocity = vel;
		}

		//m_characterController.Move(new Vector3(horizontalMovement, 0));
		m_rigidbody2D.transform.position += (new Vector3(horizontalMovement, 0.0f) * Time.fixedDeltaTime) * PLAYER_MOVEMENT_SPEED;
	}
}
