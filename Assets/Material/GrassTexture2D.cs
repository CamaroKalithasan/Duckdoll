using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the terrain data
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;

        // Create a new Texture2D for the grass texture
        Texture2D grassTexture = CreateLowPolyGrassTexture();

        // Get the terrain material
        Material terrainMaterial = terrain.materialTemplate;

        // Set the grass texture to the first texture slot in the terrain material's "Splat" texture array
        terrainMaterial.SetTexture("_Splat0", grassTexture);
    }

    private Texture2D CreateLowPolyGrassTexture()
    {
        int textureSize = 128;

        // Create a new Texture2D with the desired size
        Texture2D texture = new Texture2D(textureSize, textureSize);

        // Define the low-poly grass colors
        Color color1 = new Color(0.2f, 0.4f, 0.1f);
        Color color2 = new Color(0.2f, 0.5f, 0.1f);

        // Define the size of each grass segment
        int segmentSize = 8;

        // Loop through each pixel in the texture
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                // Calculate the position within the grass segment
                int segmentX = x % segmentSize;
                int segmentY = y % segmentSize;

                // Determine the color based on the segment position
                Color color = (segmentX < segmentSize / 2 && segmentY < segmentSize / 2) ? color1 : color2;

                // Set the pixel color
                texture.SetPixel(x, y, color);
            }
        }

        // Apply the changes to the texture
        texture.Apply();

        return texture;
    }
}
