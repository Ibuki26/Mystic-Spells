using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioDatabase))]
public class AudioDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Auto Register"))
        {
            RegisterAudio();
        }
    }

    private void RegisterAudio()
    {
        AudioDatabase database = (AudioDatabase)target;

        string[] guids = AssetDatabase.FindAssets("t:AudioData");

        List<AudioData> list = new();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            AudioData data = AssetDatabase.LoadAssetAtPath<AudioData>(path);

            if (data != null)
                list.Add(data);
        }

        database.SetAudioList(list);

        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();

        Debug.Log($"AudioDataÇ{list.Count}åèìoò^ÇµÇ‹ÇµÇΩ");
    }
}