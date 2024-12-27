using System.Collections;
using UnityEngine;
using Transform = UnityEngine.Transform;

public class EnemyIA : MonoBehaviour
{
    [Header("Obstacle Avoidance")]
    [SerializeField] private float rayLength;
    [SerializeField] private float laneChangeDistance;
  
    private Camera mainCamera;
    private Transform player;
    private EnemyConfig enemyConfig;
    private Rigidbody2D rb;
    private float baseSpeed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MinSpeed;
    private bool isChangingLane;

    [SerializeField] private LayerMask ObstacleLayer;
    [SerializeField] private LayerMask EnemyLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyConfig = GetComponent<EnemyController>().Config;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        AdjustSpeed();
        MoveForward();
        CheckForObstacles();
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(0, baseSpeed);
        AdjustToLimits();
    }

    private void AdjustSpeed()
    {
        if (player == null) return;

        // Calcula la distancia vertical con el jugador
        float distanceToPlayer = transform.position.y - player.position.y;

        float maxYDistance = 2f;
        float minYDistance = -1f;

        // Ajusta la velocidad según la distancia con el jugador
        if (distanceToPlayer > maxYDistance)
        {
            baseSpeed = Mathf.Lerp(baseSpeed, MinSpeed, Time.deltaTime * 2);
        }
        else if (distanceToPlayer < minYDistance)
        {
            baseSpeed = Mathf.Lerp(baseSpeed, MaxSpeed, Time.deltaTime / 2);
        }
        else
        {
            baseSpeed = enemyConfig.Speed;
        }
       
        rb.velocity = new Vector2(rb.velocity.x, baseSpeed);
    }

    private void CheckForObstacles()
    {
        if (isChangingLane) return;

        // Raycast hacia adelante para detectar obstáculos
        Vector3 rayStartPos = transform.position + new Vector3(0, 1, 0);

        RaycastHit2D hit = Physics2D.Raycast(rayStartPos, Vector2.down, rayLength);

        Debug.DrawRay(rayStartPos, Vector2.down * rayLength, Color.red);

        if (hit.collider != null && !isChangingLane)
        {
            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Player") || hit.collider.CompareTag("Obstacle"))
            {
                StartCoroutine(ChangeLane());
            }
        }
    }

    private IEnumerator ChangeLane()
    {
        isChangingLane = true;

        // Elegir aleatoriamente un lado para cambiar de carril (izquierda o derecha)
        float direction = Random.Range(0, 2) == 0 ? -1f : 1f;

        // Aplicar un desplazamiento lateral limitado a los límites de la pantalla
        Vector2 targetPosition = rb.position + new Vector2(direction * laneChangeDistance, 0);

        // Verificar que el nuevo carril está dentro de los límites de la pantalla
        float screenLeftLimit = mainCamera.ScreenToWorldPoint(Vector3.zero).x + 0.5f;
        float screenRightLimit = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 0.5f;

        if (targetPosition.x < screenLeftLimit || targetPosition.x > screenRightLimit)
        {
            isChangingLane = false;
            yield break;
        }

        // Realizar el cambio de carril suavemente
        float elapsedTime = 0f;
        float duration = 0.3f;

        Vector2 startPosition = rb.position;

        while (elapsedTime < duration)
        {
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPosition;

        yield return new WaitForSeconds(.3f); // Cooldown 

        isChangingLane = false;
    }

    private void AdjustToLimits()
    {
        float screenTopLimit = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        if (transform.position.y > screenTopLimit - 4f) 
        {
            baseSpeed = Mathf.Lerp(baseSpeed, -MaxSpeed, Time.deltaTime * 2);
            if (transform.position.y > screenTopLimit)
            {
                transform.position = new Vector3(transform.position.x, screenTopLimit, transform.position.z);
                baseSpeed = 0f;
            }
        }
    }
}

