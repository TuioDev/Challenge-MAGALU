using Codice.Client.Common.FsNodeReaders;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    private Vector2 scroll;
    private GUIStyle style;
    private Texture2D texture;
    private void OnEnable()
    {
        style = new();
        texture = new(2, 2);
        var fillColorArray = texture.GetPixels32();
        for(int i = 0; i < fillColorArray.Length; ++i)
        {
            fillColorArray[i] = new Color32(50,0,0,255);
        }
        style.normal.background = texture;
        
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        EnemySpawner enemySpawner = (EnemySpawner)target;

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));
        for (int i = 0; i < (int)enemySpawner.TotalRoundTime.Value; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Time: " + i, GUILayout.Width(100));
            EditorGUILayout.LabelField("Exponential: " + (int)enemySpawner.ExponentialCalculationCurve.Evaluate(i), GUILayout.Width(100));
            EditorGUILayout.LabelField("Linear: " + (int)enemySpawner.LinearCalculationCurve.Evaluate(i), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }
}
