using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private ScrollSettingsSo scrollSettings;
    [SerializeField, Range(0f, 1f)] private float parallaxFactor = .5f; // 0 = fijo, 1 = a la par del suelo
    [SerializeField] private Transform[] pieces; // Los dos hijos que se van alternando

    private float pieceWidth;

    private void Start()
    {
        pieceWidth = GetPieceWidth();

        // Se acomodan en fila desde la posicion del primero, por si quedaron mal puestos en el editor
        for (int i = 1; i < pieces.Length; i++)
            pieces[i].position = pieces[0].position + Vector3.right * pieceWidth * i;
    }

    private void Update()
    {
        float step = scrollSettings.GetSpeed() * parallaxFactor * Time.deltaTime;

        foreach (Transform piece in pieces)
            piece.position += Vector3.left * step;

        RecycleOffscreenPieces();
    }

    // El que sale por la izquierda se manda al inicio del parallax por la derecha
    private void RecycleOffscreenPieces()
    {
        foreach (Transform piece in pieces)
        {
            if (piece.position.x > -pieceWidth) continue;

            piece.position += Vector3.right * pieceWidth * pieces.Length;
        }
    }

    private float GetPieceWidth()
    {
        return pieces[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Lo llama el BiomeApplier antes del Start, asi pieceWidth se calcula con el sprite ya puesto
    public void Apply(Sprite sprite, float width, float height)
    {
        foreach (Transform piece in pieces)
        {
            SpriteRenderer renderer = piece.GetComponent<SpriteRenderer>();

            renderer.sprite = sprite;
            renderer.size = new Vector2(width, height);
        }
    }
}