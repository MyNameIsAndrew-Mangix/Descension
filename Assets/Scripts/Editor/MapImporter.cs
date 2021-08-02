using UnityEngine;
using UnityEditor;
using System.IO;
public class MapImporter : EditorWindow
{
    private Texture2D map;
    private string path;

    [MenuItem("/Map Tool/Map Importer")]
    private static void ShowWindow()
    {
        var window = GetWindow<MapImporter>();
        window.maxSize = new Vector2(100, 100);
        window.position = new Rect(500, 500, 100, 100);
        window.titleContent = new GUIContent("Map Importer");
        window.Show();
    }

    private void OnGUI()
    {

        GUILayout.Label("Import a PNG", EditorStyles.boldLabel);

        if (GUILayout.Button("Import map"))
        {
            ImportMap();
        }
        if (map != null)
        {
            map = EditorGUILayout.ObjectField("Imported Map", map, typeof(Texture2D), false) as Texture2D;
        }
    }

    private void ImportMap()
    {
        if (SystemInfo.operatingSystem.Contains("Windows"))
        {
            path = EditorUtility.OpenFilePanel("Select png", "", ".png");
        }
        else
        {
            path = EditorUtility.OpenFilePanel("Select png", "", "png");
        }
        string dest = Application.dataPath + "/Map/Map_Layouts/" + Path.GetFileName(path);
        System.IO.File.Copy(path, dest, true);
        map = LoadPNG(path);

    }
    private Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
        }
        return tex;
    }
}