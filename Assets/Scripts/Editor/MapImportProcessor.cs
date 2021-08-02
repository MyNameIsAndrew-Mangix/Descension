using UnityEditor;
using UnityEngine;

public class MapImportProcessor : AssetPostprocessor
{

    void OnPreprocessTexture() //only work with images imported by the map importer.
    {
        if (assetPath.Contains("Map_Layouts"))
        {
            TextureImporter importer = (TextureImporter)assetImporter;
            importer.isReadable = true;
            importer.alphaIsTransparency = true;
            importer.textureType = TextureImporterType.Sprite;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.spriteImportMode = SpriteImportMode.Single;
        }
    }
    void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
    {
        Debug.Log("importing sprite to: " + assetPath);
    }
    //void OnPostprocessTexture(Texture2D texture)  //only work with images imported by the map importer.
    //{
    //}
}

