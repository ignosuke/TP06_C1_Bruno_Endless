using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Data/Biomes/Biome Parallax Data")]
public class BiomeSo : ScriptableObject
{
    [SerializeField] private Sprite sky;
    [SerializeField] private Sprite[] layers;
    [SerializeField] private int layers_width;
    [SerializeField] private int layers_height;

    public Sprite GetSky() => sky;
    public Sprite[] GetLayers() => layers;
    public int GetLayersWidth() => layers_width;
    public int GetLayersHeight() => layers_height;
}
