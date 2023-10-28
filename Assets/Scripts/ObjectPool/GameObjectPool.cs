using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPool
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObjectPool(GameObject @object, int preloadCount) :
            base(() => Preload(@object), GetAction,
                ReturnAction, preloadCount)
        {
            SpawnObjects();
        }

        private static GameObject Preload(GameObject @object) => @object.CreateObject();
        private static void GetAction(GameObject @object) => @object.SetActive(true);
        private static void ReturnAction(GameObject @object) => @object.SetActive(false);
    }
}