using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace KuFramework.EditorTools
{
    internal interface IEditorScenePanel
    {
        List<SceneInfo> GetSceneInfo();
        void OpenScene(string path);
        void DeleteScene(string path);
    }
    internal class EditorScenePanel : IEditorScenePanel
    {
        //public List<SceneInfo> mSceneInfoList;
        public List<SceneInfo> GetSceneInfo()
        {
            return SearchScene();
        }
        public List<SceneInfo> SearchScene()
        {
            List<SceneInfo> sceneinfolist = new List<SceneInfo>();
            string curscenename = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name;
            foreach (string sceneGuid in AssetDatabase.FindAssets("t:Scene", new string[] { "Assets" }))
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);
                FileInfo fileinfo = new FileInfo(Application.dataPath + "/" + scenePath.Replace("Assets/", ""));
                sceneinfolist.Add(new SceneInfo(sceneName, scenePath, curscenename.Equals(sceneName)));
            }
            sceneinfolist.Sort();
            return sceneinfolist;
        }

        public void OpenScene(string path)
        {
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(path);
                UpdateSceneState();
            }
        }

        private void UpdateSceneState()
        {
            KuEditorSceneManager.UpdatePanel();
        }

        public void DeleteScene(string path)
        {
            if (EditorUtility.DisplayDialog("Confirm", string.Format("About to delete scene : {0}", path), "confirm"))
            {
                File.Delete(Application.dataPath + "/" + path.Replace("Assets/", ""));
                UpdateSceneState();
                AssetDatabase.Refresh();
                Debug.Log("successful");
            }
            else Debug.Log("cancel");
        }

    }
    internal class SceneInfo : IComparable<SceneInfo>
    {
        public readonly string sceneName;
        public readonly string scenePath;
        public readonly bool isActive;

        public SceneInfo(string sceneName, string scenePath, bool isActive)
        {
            this.sceneName = sceneName;
            this.scenePath = scenePath;
            this.isActive = isActive;
        }
        public int CompareTo(SceneInfo other)
        {
            char[] scenename1 = this.sceneName.ToCharArray();
            char[] scenename2 = other.sceneName.ToCharArray();
            return scenename1[0] == scenename2[0] ? 0 : (scenename1[0] < scenename2[0] ? -1 : 1);
        }

    }
}
