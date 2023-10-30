using UnityEngine;
using Zenject;

namespace ObjectPool
{
    public static class ObjectPoolCreators
    {
        public static T Preload<T>(T @object) where T : Object 
            => Object.Instantiate(@object, ParentObjectCreator.GetParentObject().transform, true);
        
        public static T ZenjectPreload<T>(IFactory<T> factory) 
            => factory.Create();
    }
}