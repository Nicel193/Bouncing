using UnityEngine;

namespace ObjectPool
{
    internal static class ObjectPoolUtils
    {
        public static T CreateObject<T>(this T @object) where T : Object
        {
            return Object.Instantiate(@object, ParentObjectCreator.GetParentObject().transform, true);
        } 
    }
}