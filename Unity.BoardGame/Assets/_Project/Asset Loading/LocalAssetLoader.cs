using Architecture_Base.Asset_Loading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._Project.Asset_Loading
{
    public abstract class LocalAssetLoader<T> : AssetLoader<T> where T : class
    {
        public override T Load()
        {
            if (Asset != null)
                return (T)Asset;

            AsyncOperationHandle<T> loading = Addressables.LoadAssetAsync<T>(Key);
            loading.WaitForCompletion();
            Asset = loading.Result;
            return loading.Result;
        }

        public override IList<T> LoadAll()
        {
            AsyncOperationHandle<IList<T>> loading = Addressables.LoadAssetsAsync<T>(Key, null);
            loading.WaitForCompletion();
            Asset = loading.Result;
            return loading.Result;
        }

        public override async Task<T> LoadAsync()
        {
            if (Asset != null)
                return (T)Asset;

            T asset = await Addressables.LoadAssetAsync<T>(Key).Task;
            Asset = asset;
            return asset;
        }

        public override async Task<IList<T>> LoadAllAsync()
        {
            if (Asset != null)
                return (IList<T>)Asset;

            IList<T> assets = await Addressables.LoadAssetsAsync<T>(Key, null).Task;
            Asset = assets;
            return assets;
        }

        public override T LoadAndInstantiate(Transform parent, bool isActive = true)
        {
            if (Asset != null)
                return (T)Asset;

            AsyncOperationHandle<GameObject> instantiate = Addressables.InstantiateAsync(Key, parent);
            instantiate.WaitForCompletion();
            instantiate.Result.SetActive(isActive);

            if (instantiate.Result.TryGetComponent(out T component) == false)
            {
                throw new NullReferenceException(
                    $"There is no such component on the {instantiate.Result.name}");
            }

            Asset = component;
            return component;
        }

        public override async Task<T> LoadAndInstantiateAsync(Transform parent, bool isActive = true)
        {
            if (Asset != null)
                return (T)Asset;

            GameObject instance = await Addressables.InstantiateAsync(Key, parent).Task;
            instance.SetActive(isActive);

            if (instance.TryGetComponent(out T component) == false)
            {
                throw new NullReferenceException(
                    $"There is no such component on the {instance.name}");
            }

            Asset = component;
            return component;
        }

        public override async Task<IList<T>> LoadAndInstantiateAllAsync(Transform parent, bool isActive = true)
        {
            if (Asset != null)
                return (IList<T>)Asset;

            IList<T> components = new List<T>();
            IList<GameObject> assets = await Addressables.LoadAssetsAsync<GameObject>(Key, null).Task;

            foreach (GameObject asset in assets)
            {
                if (asset.TryGetComponent(out T component) == false)
                {
                    throw new NullReferenceException(
                        $"There is no such component on the {asset.name}");
                }

                components.Add(component);
            }

            Asset = components;
            return components;
        }

        protected override void ReleaseAsset()
        {
            Addressables.Release(Asset);
        }
    }
}
