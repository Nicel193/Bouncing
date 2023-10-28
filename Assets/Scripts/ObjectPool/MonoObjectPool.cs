using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPool
{
    public class MonoObjectPool<T> : ObjectPool<T> where T : MonoBehaviour
    {
        public MonoObjectPool(T @object, int preloadCount) : base(
            () => Preload(@object), GetAction, ReturnAction,
            preloadCount)
        { }

        private static T Preload(T @object) => @object.CreateObject();
        private static void GetAction(T @object) => @object.gameObject.SetActive(true);
        private static void ReturnAction(T @object) => @object.gameObject.SetActive(false);

        public GameObject GetGameObject() => Get().gameObject;
        public Transform GetTransform() => Get().transform;
    }
}