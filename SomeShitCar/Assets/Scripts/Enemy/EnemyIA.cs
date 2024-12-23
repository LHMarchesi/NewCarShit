using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("Obstacle Avoidance")]
    [SerializeField] private float rayLength;
    [SerializeField] private float laneChangeDistance;
    [SerializeField] private LayerMask obstacleLayer;

    private float baseSpeed;
    private EnemyConfig enemyConfig;
    private Transform player;
    private Rigidbody2D rb;
    private bool isChangingLane;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyConfig = GetComponent<EnemyController>().Config;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        mainCamera = Camera.main;

        baseSpeed = enemyConfig.Speed;
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
    }

    private void AdjustSpeed()
    {
        if (player == null) return;

        // Calcula la distancia vertical con el jugador
        float distanceToPlayer = transform.position.y - player.position.y;

        float maxYDistance = 3f;
        float minYDistance = -1f;

       //assings Base Speed Fo
        float minSpeed = baseSpeed - 1;
        float maxSpeed = baseSpeed + 2;

        float targetSpeed = rb.velocity.y;

        // Ajusta la velocidad según la distancia con el jugador
        if (distanceToPlayer > maxYDistance)
        {
            targetSpeed = Mathf.MoveTowards(targetSpeed, maxSpeed, Time.deltaTime * 5f);
            Debug.Log("Se pasó");
        }
        else if (distanceToPlayer < minYDistance)
        {
            targetSpeed = Mathf.MoveTowards(targetSpeed, minSpeed, Time.deltaTime * 5f); // Mover hacia arriba
            Debug.Log("Está atrás");
        }
        else
        {
            targetSpeed = Mathf.MoveTowards(targetSpeed, 0, Time.deltaTime * 3f); // Detenerse suavemente
        }
        Debug.Log(targetSpeed);
        rb.velocity = new Vector2(rb.velocity.x, targetSpeed);
    }

    private void CheckForObstacles()
    {
        if (isChangingLane) return;

        // Raycast hacia adelante para detectar obstáculos
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, obstacleLayer);

        if (hit.collider != null && !isChangingLane)
        {
            StartCoroutine(ChangeLane());
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
        float duration = 0.5f;

        Vector2 startPosition = rb.position;

        while (elapsedTime < duration)
        {
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPosition;

        yield return new WaitForSeconds(1f); // Cooldown 

        isChangingLane = false;
    }

    private void OnDrawGizmos()
    {
        // Dibujar el raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }
}

