﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneGrid))]
public class SceneGridEditor : Editor {

    public override void OnInspectorGUI() {
        SceneGrid sg = (SceneGrid)target;

        sg.save_images = EditorGUILayout.Toggle("Save Images", sg.save_images);

        if (sg.save_images) {
            sg.folderpath = EditorGUILayout.TextField("Folderpath", sg.folderpath);
        }
        
        EditorGUILayout.Space();

        sg.triangle_limit = EditorGUILayout.IntField("Triangle Limit", sg.triangle_limit);

        EditorGUILayout.Space();

        sg.size = EditorGUILayout.IntField("Size", sg.size);
        sg.spread = EditorGUILayout.IntField("Spread", sg.size);

        EditorGUILayout.Space();

        sg.y_height = EditorGUILayout.FloatField("Y Height", sg.y_height);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Images", EditorStyles.boldLabel);

        sg.width = EditorGUILayout.IntField("Texture Width", sg.width);
        sg.height = EditorGUILayout.IntField("Texture Height", sg.height);

        EditorGUILayout.Space();

        if (GUILayout.Button("Simp")) {
            sg.simplify();
        }

        if (GUILayout.Button("Init")) {
            sg.init();
        }

        if (GUILayout.Button("Remove Cameras")) {
            sg.remove_cameras();
        }
    }

    private void OnSceneGUI() {
        SceneGrid sg = (SceneGrid)target;
        if (sg.transform.childCount == 0) {
            return;
        }

        if (!sg.is_initialized) {
            return;
        }

        Handles.color = Color.magenta;

        if (sg.grid == null) {
            sg.find_nodes();
        }

        for (int x = 0; x <= sg.size; ++x) {
            for (int z = 0; z <= sg.size; ++z) {
                if (sg.grid[x, z].has_frustum(Direction.NORTH)) {
                    Handles.DrawLine(sg.grid[x, z].transform.position, sg.grid[x, z].transform.position + new Vector3(0.0f, 0.0f, 1.5f));
                }

                if (sg.grid[x, z].has_frustum(Direction.EAST)) {
                    Handles.DrawLine(sg.grid[x, z].transform.position, sg.grid[x, z].transform.position + new Vector3(1.5f, 0.0f, 0.0f));
                }

                if (sg.grid[x, z].has_frustum(Direction.SOUTH)) {
                    Handles.DrawLine(sg.grid[x, z].transform.position, sg.grid[x, z].transform.position + new Vector3(0.0f, 0.0f, -1.5f));
                }

                if (sg.grid[x, z].has_frustum(Direction.WEST)) {
                    Handles.DrawLine(sg.grid[x, z].transform.position, sg.grid[x, z].transform.position + new Vector3(-1.5f, 0.0f, 0.0f));
                }
            }
        }
    }
}
