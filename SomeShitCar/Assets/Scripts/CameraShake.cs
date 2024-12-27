using System.Collections;
using UnityEditor;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1f;

    private void OnEnable()
    {
        Health.OnTakeDamage += Shake;
    }
    private void Shake()
    {
        StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;

            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;

            yield return null;
        }
        transform.position = startPosition;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= Shake;
    }
}
