using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStamina))]
[RequireComponent(typeof(PlayerLives))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerInfo Info = new PlayerInfo();

    [Serializable]
    public class SpeedStats
    {
        public float PlayerJump = 6.0f;
        public float PlayerMovement = 8.0f;
    }
    [SerializeField] public SpeedStats Speeds = new SpeedStats();

    [Serializable]
    public class ModifierStats
    {
        public float PlayerSprint = 1.35f;
        public float PlayerDash = 15.0f;
    }
    [SerializeField] public ModifierStats Modifiers = new ModifierStats();

    [Serializable]
    public class StaminaStats
    {
        public float JumpCost = 5.0f;
        public float DashCost = 20.0f;
        public float SprintCost = 0.25f;
    }
    [SerializeField] public StaminaStats StaminaCosts = new StaminaStats();

    // Unity Components
    Rigidbody2D m_rigidbody2D;
    SpriteRenderer m_spriteRenderer;

    // Custom Components
    public PlayerStamina Stamina { get; set; }
    public PlayerLives Lives { get; set; }
    public GameController Game { get; set; }
    public CountdownTimer RespawnTimer { get; set; }

    // Helper Properties
    public bool FacingRight { get { return transform.localScale.x > 0; } }
    public bool CanJump { get { return (Stamina.CurrentStamina - StaminaCosts.JumpCost) >= 0.0f; } }
    public bool CanDash { get { return (Stamina.CurrentStamina - StaminaCosts.DashCost) >= 0.0f; } }
    public bool CanSprint { get { return (Stamina.CurrentStamina - StaminaCosts.SprintCost) >= 0.0f; } }
    private bool m_isSprinting = false;

    // Use this for initialization
    void Start()
    {
        // Unity Components
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        // Custom Components
        Stamina = GetComponent<PlayerStamina>();
        Lives = GetComponent<PlayerLives>();

        // Setup the Respawn Timer
        RespawnTimer = GetComponent<CountdownTimer>();
        RespawnTimer.StopClockTimer();
        RespawnTimer.OnCompleted += RespawnTimer_OnCompleted;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Lives.IsAlive || !Lives.HasLives) return;

        // Preform Jump
        if (Input.GetButtonDown(Info.JoystickInputManagerPrefix + "Jump") && CanJump)
        {
            //Debug.Log("Jump");
            Stamina.DecreaseStamina(StaminaCosts.JumpCost);

            var vel = m_rigidbody2D.velocity;
            vel.y = Speeds.PlayerJump;
            if (m_isSprinting) { vel.y *= Modifiers.PlayerSprint; }

            m_rigidbody2D.velocity = vel;
        }
    }

    // Called once per Physics Update
    private void FixedUpdate()
    {
        if (!Lives.IsAlive || !Lives.HasLives) return;

        float horizontalMovement = Input.GetAxis(Info.JoystickInputManagerPrefix + "Horizontal");
        Vector3 movement = (new Vector3(horizontalMovement, 0.0f) * Time.fixedDeltaTime) * Speeds.PlayerMovement;

        if (Input.GetButton(Info.JoystickInputManagerPrefix + "Sprint") && CanSprint)
        {
            //Debug.Log("Sprint");
            Stamina.DecreaseStamina(StaminaCosts.SprintCost);
            movement *= Modifiers.PlayerSprint;
            m_isSprinting = true;
        }
        else { m_isSprinting = false; }

        if (Input.GetButtonDown(Info.JoystickInputManagerPrefix + "Dash") && CanDash)
        {
            //Debug.Log("Dash");
            Stamina.DecreaseStamina(StaminaCosts.DashCost);

            var vel = m_rigidbody2D.velocity;
            if (FacingRight) { vel.x = Modifiers.PlayerDash; }
            else { vel.x = -Modifiers.PlayerDash; }
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
