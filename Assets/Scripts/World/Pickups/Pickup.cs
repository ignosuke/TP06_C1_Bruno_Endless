using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupDataSo pickupData;
    private ParticleSystem particles;
    public PickupDataSo GetData() => pickupData;
    public int GetScoreValue() => pickupData.GetScoreValue();

    public void Collect()
    {
        particles = pickupData.GetCollectEffect();

        if (particles != null)
            Instantiate(particles, transform.position, Quaternion.identity, transform.parent);

        Destroy(gameObject);
    }
}