using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector2 speed;
   
    Rigidbody2D rb;
    private float deltaY, deltaX;
    private float deceleration = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    public void HandleTouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    Vector2 targetPos = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
                    Vector2 direction = targetPos - (Vector2)transform.position;
                    rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
                    break;

                case TouchPhase.Ended:
                    StartCoroutine(ApplyDeceleration());
                    break;
            }
        }
    }

    private IEnumerator ApplyDeceleration()
    {
        while (rb.velocity.magnitude > 0.1f) 
        {
            // Reducimos la velocidad con un factor de desaceleración
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, deceleration * Time.deltaTime);
            yield return null; 
        }
        // Asegura que la velocidad sea exactamente cero al final
        rb.velocity = Vector2.zero;
    }
}
