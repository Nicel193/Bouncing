using UnityEngine;

namespace ObjectPool
{
    internal class ParentObjectCreator
    {
        private static GameObject _parentObject;

        public static GameObject GetParentObject()
        {
            if (_parentObject == null) _parentObject = new GameObject {name = "Game-Object-Pool"};

            return _parentObject;
        }
    }
}