using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    // リソースの管理クラス
    public class ResourceManager : MonoBehaviour
    {
        // GameObjectの名前
        public const string OBJECT_NAME = "ResourceManager";

        // ロードするリソースの一覧
        [SerializeField]
        private List<string> m_resourceKeys = new List<string>();

        // 読み込んだリソースのGameObject
        private Dictionary<string, GameObject> m_resources = new Dictionary<string, GameObject>();

        /// <summary>
        /// 設定しているリソースの一覧を読み込む
        /// </summary>
        public void Load() => m_resourceKeys.ForEach(path => m_resources.Add(path, Resources.Load(path) as GameObject));

        /// <summary>
        /// 指定したリソースを取得する
        /// </summary>
        /// <param name="resource">指定したリソース</param>
        /// <returns>GameObject</returns>
        public GameObject GetResource(string resource) => m_resources[resource];

        public void OnDestroy()
        {
            // リソースを解放する
            Resources.UnloadUnusedAssets();
        }
    }

}
