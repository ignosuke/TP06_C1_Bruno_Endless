using UnityEngine;

public class Bobbing : MonoBehaviour
{
    [SerializeField] private float amplitude = .2f;
    [SerializeField] private float frequency = 2f;
    [SerializeField] private float rotationSpeed = 0f;

    private Vector3 startPosition;
    private float timeOffset;

    private void Start()
    {
        startPosition = transform.localPosition;

        timeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency + timeOffset) * amplitude;

        transform.localPosition = startPosition + Vector3.up * offset;

        if (rotationSpeed != 0f)
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}