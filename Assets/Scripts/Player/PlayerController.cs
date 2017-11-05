using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStamina))]
[RequireComponent(typeof(PlayerLives))]
public class PlayerController : MonoBehaviour
{
    public float Speed_PlayerJump = 6.0f;
    public float Speed_PlayerMovement = 8.0f;

    public float Modifier_PlayerSprint = 1.35f;
    public float Modifier_PlayerDash = 15.0f;

    public float Stamina_JumpCost = 5.0f;
    public float Stamina_DashCost = 20.0f;
    public float Stamina_SprintCost = 0.25f;

    [SerializeField]
    public PlayerInfo Info = new PlayerInfo();

    // Unity Components
    Rigidbody2D m_rigidbody2D;
    // Custom Components
    [HideInInspector]public PlayerStamina Stamina;
    [HideInInspector]public PlayerLives Lives;

    public bool CanJump { get { return (Stamina.CurrentStamina - Stamina_JumpCost) >= 0.0f; } }
    public bool CanDash { get { return (Stamina.CurrentStamina - Stamina_DashCost) >= 0.0f; } }
    public bool CanSprint { get { return (Stamina.CurrentStamina - Stamina_SprintCost) >= 0.0f; } }
    private bool m_isSprinting = false;

    public bool FacingRight { get { return transform.localScale.x > 0; } }

    // Use this for initialization
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        Stamina = GetComponent<PlayerStamina>();
        Lives = GetComponent<PlayerLives>();
    }

    // Update is called once per frame
    void Update()
    {
        // Preform Jump
        if (Input.GetButtonDown(Info.JoystickInputManagerPrefix + "Jump") && CanJump)
        {
            //Debug.Log("Jump");
            Stamina.DecreaseStamina(Stamina_JumpCost);

            var vel = m_rigidbody2D.velocity;
            vel.y = Speed_PlayerJump;
            if (m_isSprinting) { vel.y *= Modifier_PlayerSprint; }

            m_rigidbody2D.velocity = vel;
        }
    }

    // Called once per Physics Update
    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis(Info.JoystickInputManagerPrefix + "Horizontal");
        Vector3 movement = (new Vector3(horizontalMovement, 0.0f) * Time.fixedDeltaTime) * Speed_PlayerMovement;

        if (Input.GetButton(Info.JoystickInputManagerPrefix + "Sprint") && CanSprint)
        {
            //Debug.Log("Sprint");
            Stamina.DecreaseStamina(Stamina_SprintCost);
            movement *= Modifier_PlayerSprint;
            m_isSprinting = true;
        }
        else { m_isSprinting = false; }

        if (Input.GetButtonDown(Info.JoystickInputManagerPrefix + "Dash") && CanDash)
        {
            //Debug.Log("Dash");
            Stamina.DecreaseStamina(Stamina_DashCost);

            var vel = m_rigidbody2D.velocity;
            if (FacingRight) { vel.x = Modifier_PlayerDash; }
            else { vel.x = -Modifier_PlayerDash; }
            m_rigidbody2D.velocity = vel;
        }

        //Debug.Log("Movement: " + movement);
        m_rigidbody2D.transform.position += movement;

        // Flip the sprite to face the direction of movement 
        var scale = transform.localScale;
        if ((horizontalMovement > 0.001f && scale.x < 0) || (horizontalMovement <= -0.001f && scale.x > 0))
        {
            scale.x *= -1;

            // Cancel the Dash
            // Nullify Velocity with a Physics Hack
            var vel = m_rigidbody2D.velocity;
            vel.x = 0.0f;
            m_rigidbody2D.velocity = vel;
        }
        transform.localScale = scale;
    }
}
