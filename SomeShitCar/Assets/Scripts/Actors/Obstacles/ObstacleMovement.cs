using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float speed;

    public float Speed { get => speed; private set { } }

    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
