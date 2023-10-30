using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPool
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObjectPool(Func<GameObject> preloadFunk, int preloadCount) : base(preloadFunk, GetAction, 
            ReturnAction, preloadCount)
        {
            SpawnObjects();
        }
        
        private static void GetAction(GameObject @object) => @object.SetActive(true);
        private static void ReturnAction(GameObject @object) => @object.SetActive(false);
    }
}