using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject[] roadParts;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Vector3 offset;

    private Queue<GameObject> activeRoads = new Queue<GameObject>(); // Segmentos activos

    void Start()
    {
        // Encontrar los segmentos iniciales en la escena y ordenarlos por su posición Y
        GameObject[] activeRoadsOnStart = GameObject.FindGameObjectsWithTag("Road");
        List<GameObject> sortedRoads = new List<GameObject>(activeRoadsOnStart);

        // Ordenar los segmentos por posición Y de menor a mayor
        sortedRoads.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));

        foreach (GameObject road in sortedRoads)
        {
            activeRoads.Enqueue(road);
        }
    }

    void Update()
    {
        MoveRoads();
    }

    public void SpawnRoad()
    {
        GameObject roadPart = Instantiate(GetRandomRoadPart(), offset, Quaternion.identity, this.transform);
        activeRoads.Enqueue(roadPart);
    }

    public void DespawnRoad()
    {
        if (activeRoads.Count > 0)
        {
            Destroy(activeRoads.Dequeue());
        }
    }

    // Mover todos los segmentos activos hacia abajo
    private void MoveRoads()
    {
        foreach (GameObject road in activeRoads)
        {
            road.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }

    // Seleccionar una parte de carretera aleatoria
    private GameObject GetRandomRoadPart()
    {
        int index = Random.Range(0, roadParts.Length);
        return roadParts[index];
    }
}
