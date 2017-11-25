using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour {
    public const float MIN_STAMINA = 0.0f;
    public const float MAX_STAMINA = 100.0f;
    public float StaminaIncreaseRate = 10.0f;
    public float CurrentStamina = MAX_STAMINA;

    public void IncreaseStamina(float amount)
    {
        CurrentStamina = Mathf.Min(CurrentStamina + amount, MAX_STAMINA);
    }
    public void DecreaseStamina(float amount)
    {
        CurrentStamina = Mathf.Max(CurrentStamina - amount, MIN_STAMINA);
    }

    public void SetMaxStamina()
    {
        CurrentStamina = MAX_STAMINA;
    }

    bool OnFloor = false;

    private void Update()
    {
        if (OnFloor)
        {
            IncreaseStamina(StaminaIncreaseRate * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            OnFloor = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            OnFloor = false;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
    }
}
