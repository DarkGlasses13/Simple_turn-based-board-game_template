using Architecture_Base.Asset_Loading;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._Project.Asset_Loading
{
    public class LocalInstanceLoader : IInstanceLoader
    {
        private GameObject _instance;

        public bool HasInstance => _instance != null;

        public GameObject GetInstance(string id, Action<GameObject> onLoaded = null)
        {
            if (_instance != null)
                return _instance;

            AsyncOperationHandle<GameObject> loading = Addressables.InstantiateAsync(id);
            loading.WaitForCompletion();
            _instance = loading.Result;
            onLoaded?.Invoke(_instance);
            return _instance;
        }

        public GameObject GetInstance(string id, Vector3 position, Quaternion rotation,
            Transform parent, Action<GameObject> onLoaded = null)
        {
            GetInstance(id, onLoaded);
            _instance.transform.SetLocalPositionAndRotation(position, rotation);
            _instance.transform.SetParent(parent);
            return _instance;
        }

        public void UnloadInstance()
        {
            if (_instance)
            {
                _instance.SetActive(false);
                _instance.transform.SetParent(null);
                Addressables.ReleaseInstance(_instance);
                _instance = null;
            }
        } 
    }
}
