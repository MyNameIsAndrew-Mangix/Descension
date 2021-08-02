using System;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Map
{
    public class LevelGenerator : MonoBehaviour
    {
        public Texture2D[] map;

        public ColorToPrefab[] colorMappings;

        private float chunkOffset; //Y-axis offset, used for where to generate the next map chunk.
        private List<GameObject> chunkContainers = new List<GameObject>();
        private bool isFirstPass = true;
        private void Start()
        {
            InitializeContainers();
            GenerateLevel();
        }
        private void InitializeContainers()
        {
            for (int i = 0; i < map.Length; i++)
            {
                chunkContainers.Add(GenerateChunkContainers(map[i]));
            }
        }
        private GameObject GenerateChunkContainers(Texture2D texture2D)
        {
            GameObject generatedParent = new GameObject(texture2D.name + " container");
            generatedParent.transform.parent = this.gameObject.transform;
            return generatedParent;
        }

        private void GenerateLevel()
        {
            for (int i = 0; i < map.Length; i++)
            {
                if (i != 0)
                {
                    isFirstPass = false;
                    chunkOffset += (map[i].height);
                }
                for (int x = 0; x < map[i].width; x++)
                {
                    for (int y = 0; y < map[i].height; y++)
                    {
                        GenerateTile(x, y, map[i], !isFirstPass, chunkContainers[i].transform);
                    }
                }
            }
        }
        private void GenerateTile(int x, int y, Texture2D chunk, bool isOffset)
        {
            Color pixelColor = chunk.GetPixel(x, y);

            if (pixelColor.a == 0)
                return; //transparent pixel: ignore.

            foreach (ColorToPrefab tile in colorMappings)
            {
                if (tile.color.Equals(pixelColor))
                {
                    Vector2 position = new Vector2(x, y);
                    if (isOffset)
                        position.y += chunkOffset;
                    Instantiate(tile.prefab, position, Quaternion.identity);
                }
            }
        }
        private void GenerateTile(int x, int y, Texture2D chunk, bool isOffset, Transform parent)
        {
            Color pixelColor = chunk.GetPixel(x, y);

            if (pixelColor.a == 0)
                return; //transparent pixel: ignore.

            foreach (ColorToPrefab tile in colorMappings)
            {
                if (tile.color.Equals(pixelColor))
                {
                    Vector2 position = new Vector2(x, y);
                    if (isOffset)
                        position.y += chunkOffset;
                    if (tile.prefab.name == "Player")
                        Instantiate(tile.prefab, position, Quaternion.identity);
                    else
                        Instantiate(tile.prefab, position, Quaternion.identity, parent);
                }
            }
        }
    }
}