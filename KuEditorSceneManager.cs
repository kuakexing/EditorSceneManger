﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace KuFramework.EditorTools
{

    internal class KuEditorSceneManager : EditorWindow
    {
        private string myString = "Hello World";
        private bool groupEnabled;
        private bool myBool = true;
        private float myFloat = 1.23f;
        private Texture texture;

        private static IEditorScenePanel mScenePanel = new EditorScenePanel();
        private static List<SceneInfo> mSceneInfoList = new List<SceneInfo>();
        private static Vector2 mPos;
        [MenuItem("Tools/ScenesManager %`")]
        private static void ShowSceneManagerPanel()
        {
            EditorWindow window = GetWindow(typeof(KuEditorSceneManager), false, "EditorSceneManager", true);
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 600, 500);
            window.Show();
            mSceneInfoList = mScenePanel.GetSceneInfo();
        }
        private void OnGUI()
        {
            UpdateInfoList();
            ShowScrollSceneInfo();
            ShowFixedButton();
        }

        private static void ShowFixedButton()
        {
            if (GUILayout.Button("Open Build Settings"))
                mScenePanel.OpenBuildSettingPanel();
        }

        private static void UpdateInfoList()
        {
            if (mSceneInfoList.Count == 0)
                mSceneInfoList = mScenePanel.GetSceneInfo();
        }

        private void ShowScrollSceneInfo()
        {
            mPos = EditorGUILayout.BeginScrollView(mPos);
            for (int i = 0; i < mSceneInfoList.Count; i++)
            {
                ShowSingleSceneInfo(i);
            }
            EditorGUILayout.EndScrollView();
            //UnityEngine.Debug.Log(UnityEditor.SceneManagement.StageUtility.GetCurrentStageHandle().ToString());
        }

        private static void ShowSingleSceneInfo(int i)
        {
            GUILayout.Label(string.Format("{0}.{1}", (i + 1).ToString(), mSceneInfoList[i].sceneName), EditorStyles.boldLabel);
            GUILayout.Label(string.Format("active ： {1}\t\tsize : {0}KB", mSceneInfoList[i].sceneSize, (mSceneInfoList[i].isActive ? "true" : "false")));
            GUILayout.Label("path : " + mSceneInfoList[i].scenePath);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open"))
                mScenePanel.OpenScene(mSceneInfoList[i].scenePath);
            if (GUILayout.Button("Delete"))
                mScenePanel.DeleteScene(mSceneInfoList[i].scenePath);
            if (GUILayout.Button("Add To BuildList"))
                mScenePanel.AddSceneToBuildList(mSceneInfoList[i].scenePath);
            if (GUILayout.Button("Open In Directory"))
                mScenePanel.OpenInDirectory(mSceneInfoList[i].scenePath, mSceneInfoList[i].sceneName);
            EditorGUILayout.EndHorizontal();
            GUILayout.Label("");
            //EditorGUI.ColorField(new Rect(0,0,200,200),"test123" , new Color(1, 1, 1,0.5f));
        }

        internal static void UpdatePanel()
        {
            mSceneInfoList.Clear();
        }

        private void ShowOtherInfo()
        {
            //UnityEngine.Debug.Log("GUI");
            GUILayout.Label("Title", EditorStyles.boldLabel);

            myString = EditorGUILayout.TextField("Text field", myString);

            groupEnabled = EditorGUILayout.BeginToggleGroup("Toggle group", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
            texture = EditorGUILayout.ObjectField("Add texture", texture, typeof(Texture), true) as Texture;

        }

    }
}