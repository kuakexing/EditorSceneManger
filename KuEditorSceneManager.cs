using System;
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
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 500);
            window.Show();
            mSceneInfoList = mScenePanel.GetSceneInfo();
        }
        private void OnGUI()
        {
            if (mSceneInfoList.Count == 0)
                mSceneInfoList = mScenePanel.GetSceneInfo();
            mPos = EditorGUILayout.BeginScrollView(mPos);
            ShowSceneInfo();
            EditorGUILayout.EndScrollView();
            //UnityEngine.Debug.Log(UnityEditor.SceneManagement.StageUtility.GetCurrentStageHandle().ToString());
        }
        private void ShowSceneInfo()
        {
            for (int i = 0; i < mSceneInfoList.Count; i++)
            {
                ShowSingleSceneInfo(i);
            }
        }

        private static void ShowSingleSceneInfo(int i)
        {
            GUILayout.Label(string.Format("{0}.{1}", (i + 1).ToString(), mSceneInfoList[i].sceneName), EditorStyles.boldLabel);
            GUILayout.Label("path : " + mSceneInfoList[i].scenePath);
            GUILayout.Label("active ： " + (mSceneInfoList[i].isActive ? "true" : "false"));
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("open"))
                mScenePanel.OpenScene(mSceneInfoList[i].scenePath);
            if (GUILayout.Button("delete"))
                mScenePanel.DeleteScene(mSceneInfoList[i].scenePath);
            EditorGUILayout.EndHorizontal();
            GUILayout.Label("");
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