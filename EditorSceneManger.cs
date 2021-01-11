using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace KuFramework.EditorTools
{
    public class KuEditorSceneManager : EditorWindow
    {
        private string myString = "Hello World";
        private bool groupEnabled;
        private bool myBool = true;
        private float myFloat = 1.23f;
        private string notification = "This is a Notification";
        private Texture texture;

        private static IEditorScenePanel ScenePanel = new EditorScenePanel();
        public static List<SceneInfo> mSceneInfoList = new List<SceneInfo>();
        private static Vector2 mPos;
        [MenuItem("Tools/ScenesManager _1")]
        private static void ShowSceneManagerPanel()
        {
            EditorWindow window = GetWindow(typeof(KuEditorSceneManager), false, "EditorSceneManager", true);
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 500);
            window.Show();
            mSceneInfoList = ScenePanel.GetSceneInfo();
        }
        private void OnGUI()
        {
            if (mSceneInfoList.Count == 0)
                mSceneInfoList = ScenePanel.GetSceneInfo();
            mPos = EditorGUILayout.BeginScrollView(mPos);
            ShowSceneInfo();
            EditorGUILayout.EndScrollView();
            //UnityEngine.Debug.Log(UnityEditor.SceneManagement.StageUtility.GetCurrentStageHandle().ToString());
        }
        private void ShowSceneInfo()
        {
            for (int i = 0; i < mSceneInfoList.Count; i++)
            {
                GUILayout.Label(string.Format("{0}.{1}", (i + 1).ToString(), mSceneInfoList[i].sceneName), EditorStyles.boldLabel);
                GUILayout.Label("path : " + mSceneInfoList[i].scenePath);
                GUILayout.Label("active ： " + (mSceneInfoList[i].isActive ? "true" : "false"));
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("open"))
                    ScenePanel.OpenScene(mSceneInfoList[i].scenePath);
                if (GUILayout.Button("delete"))
                    ScenePanel.DeleteScene(mSceneInfoList[i].scenePath);
                EditorGUILayout.EndHorizontal();
                GUILayout.Label("");
            }
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