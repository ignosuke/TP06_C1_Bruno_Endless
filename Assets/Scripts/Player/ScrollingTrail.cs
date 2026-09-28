using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ScrollingTrail : MonoBehaviour
{
    [SerializeField] private ScrollSettingsSo scrollSettings;

    private const float disabledVertexDistance = 1000f; // Tan grande que el trail nunca emite por su cuenta

    private TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();

        trail.minVertexDistance = disabledVertexDistance;
    }

    private void LateUpdate()
    {
        trail.AddPosition(transform.position);

        float offset = scrollSettings.GetSpeed() * Time.deltaTime;

        for (int i = 0; i < trail.positionCount; i++)
        {
            Vector3 position = trail.GetPosition(i);
            position.x -= offset;
            trail.SetPosition(i, position);
        }
    }
}