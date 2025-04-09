using UnityEngine;

public class CamuflajeGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    private Texture2D texture;

    void Start()
    {
        GenerateCamouflage();
    }

    void GenerateCamouflage()
    {
        texture = new Texture2D(width, height);
        // texture.filterMode = FilterMode.Point; 

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color color = Color.green; // Selecciona un color aleatorio
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        // Asigna la textura a un material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = texture;
        }
    }
}