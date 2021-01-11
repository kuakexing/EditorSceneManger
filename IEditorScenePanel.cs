using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace KuFramework.EditorTools
{
    public interface IEditorScenePanel
    {
        List<SceneInfo> GetSceneInfo();
        void OpenScene(string path);
        void DeleteScene(string path);
    }
    public class EditorScenePanel : IEditorScenePanel
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
            return sceneinfolist;
        }

        public void OpenScene(string path)
        {
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(path);
        }
        public void DeleteScene(string path)
        {
            if (EditorUtility.DisplayDialog("Confirm", string.Format("About to delete scene : {0}", path), "confirm"))
            {
                File.Delete(Application.dataPath + "/" + path.Replace("Assets/", ""));
                KuEditorSceneManager.mSceneInfoList.Clear();
                AssetDatabase.Refresh();
                Debug.Log("successful");
            }
            else Debug.Log("cancel");
        }

    }
    public class SceneInfo
    {
        public readonly string sceneName;
        public readonly string scenePath;
        public bool isActive;

        public SceneInfo(string sceneName, string scenePath, bool isActive)
        {
            this.sceneName = sceneName;
            this.scenePath = scenePath;
            this.isActive = isActive;
        }
    }
}
