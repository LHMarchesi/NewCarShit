using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Health;
    private Movement movement;
    private Health health;

    void OnEnable()
    {
        movement = GetComponent<Movement>();
        health = GetComponent<Health>();

        health.OnDead += HandleDeath;

        health.SetStartingHeal(Health);
    }

    // Update is called once per frame
    void Update()
    {
        movement.HandleTouchMovement();
    }

    private void HandleDeath()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.Lose);
        gameObject.SetActive(false);
    }
}
