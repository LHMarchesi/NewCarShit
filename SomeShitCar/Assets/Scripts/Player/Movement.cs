using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector2 speed;
    [SerializeField] private float constantAceleration;
    [SerializeField] public Transform joystickCircle;
    [SerializeField] public Transform outerCircle;

    Rigidbody2D rb;

    private bool touchStart;
    private Vector2 pointA;
    private Vector2 pointB;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
    }

    private void Update()
    {
        HandleTouchMovement();
    }

    public void HandleTouchMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            joystickCircle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            joystickCircle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }
    }

    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);

            joystickCircle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
        else
        {
            rb.MovePosition((Vector2)transform.position + Vector2.up * constantAceleration * Time.deltaTime);
            joystickCircle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
