using Architecture_Base.Asset_Loading;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._Project.Actors_Base
{
    public abstract class Actor : IInstanceLoader
    {
        private GameObject _instance;

        public IActorData Data { get; }
        public bool IsInUse { get; set; }
        public bool HasInstance => _instance != null;

        protected Actor(IActorData data)
        {
            Data = data;
        }

        public GameObject GetInstance(string id, Action<GameObject> onLoaded = null)
        {
            if (_instance != null)
                return _instance;

            AsyncOperationHandle<GameObject> instantiate = Addressables.InstantiateAsync(id);
            instantiate.WaitForCompletion();
            _instance = instantiate.Result;
            OnInstanceLoaded(_instance);
            onLoaded?.Invoke(_instance);
            return _instance;
        }

        protected virtual void OnInstanceLoaded(GameObject instance) 
        {
            instance
                .AddComponent<ActorInstance>()
                .Construct(Data.ID);
        }

        public GameObject GetInstance(string id, Vector3 position, Quaternion rotation, Transform parent, Action<GameObject> onLoaded = null)
        {
            GetInstance(id, onLoaded);
            _instance.transform.SetLocalPositionAndRotation(position, rotation);
            _instance.transform.SetParent(parent);
            return _instance;
        }

        public void UnloadInstance(Action<GameObject> onBeforeUnload = null)
        {
            if (_instance)
            {
                _instance.SetActive(false);
                _instance.transform.SetParent(null);
                OnBeforeUnload(_instance);
                onBeforeUnload?.Invoke(_instance);
                Addressables.ReleaseInstance(_instance);
                _instance = null;
            }
        }

        protected virtual void OnBeforeUnload(GameObject instance) { }
    }
}