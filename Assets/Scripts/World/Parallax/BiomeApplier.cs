using UnityEngine;

public class BiomeApplier : MonoBehaviour
{
    [SerializeField] private BiomeSo biome;
    [SerializeField] private SpriteRenderer sky;
    [SerializeField] private ParallaxLayer[] layers; // Mismo orden que los sprites del bioma

    private void Awake()
    {
        Apply(biome);
    }

    public void Apply(BiomeSo newBiome)
    {
        biome = newBiome;

        ApplySky();
        ApplyLayers();
    }

    // El cielo es un sprite de un solo color
    private void ApplySky()
    {
        Camera mainCamera = Camera.main;

        float visibleHeight = mainCamera.orthographicSize * 2f;
        float visibleWidth = visibleHeight * mainCamera.aspect;

        sky.sprite = biome.GetSky();
        sky.size = new Vector2(visibleWidth, visibleHeight);
    }

    private void ApplyLayers()
    {
        Sprite[] biomeLayers = biome.GetLayers();
        int count = Mathf.Min(biomeLayers.Length, layers.Length);

        float width = biome.GetLayersWidth();
        float height = biome.GetLayersHeight();

        for (int i = 0; i < count; i++)
            layers[i].Apply(biomeLayers[i], width, height);
    }
}