using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStamina))]
public class PlayerController : MonoBehaviour {
	public const float PLAYER_JUMP_SPEED = 6.0f;
	public const float PLAYER_MOVEMENT_SPEED = 8.0f;
	public const float STAMINA_JUMP_COST = 10.0f;

	[SerializeField]
	public PlayerInfo Info = new PlayerInfo();

	// Unity Components
	Rigidbody2D m_rigidbody2D;
	// Custom Components
	PlayerStamina m_stamina;

	public bool CanJump { get { return (m_stamina.CurrentStamina - STAMINA_JUMP_COST) >= 0.0f; } }

	// Use this for initialization
	void Start () {
		m_rigidbody2D = GetComponent<Rigidbody2D>();
		m_stamina = GetComponent<PlayerStamina>();
	}
	
	// Update is called once per frame
	void Update () {
		// Preform Jump
		if (Input.GetButtonDown(Info.JoystickInputManagerPrefix + "Jump") &&
			CanJump)
		{
			//Debug.Log("Jump");
			m_stamina.DecreaseStamina(STAMINA_JUMP_COST);

			var vel = m_rigidbody2D.velocity;
			vel.y = PLAYER_JUMP_SPEED;
			m_rigidbody2D.velocity = vel;
		}
	}

	// Called once per Physics Update
	private void FixedUpdate()
	{
		float horizontalMovement = Input.GetAxis(Info.JoystickInputManagerPrefix + "Horizontal");
		m_rigidbody2D.transform.position += (new Vector3(horizontalMovement, 0.0f) * Time.fixedDeltaTime) * PLAYER_MOVEMENT_SPEED;

		// Flip the sprite to face the direction of movement 
		var scale = transform.localScale;
		if (horizontalMovement > 0.001f) { scale.x = 1; } // Flip Right
		else if (horizontalMovement <= -0.001f) { scale.x = -1; } // Flip Left
		transform.localScale = scale;
	}
}
