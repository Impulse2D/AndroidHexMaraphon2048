using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class ObjectsPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefabObject;

        private List<T> _objects = new List<T>();

        public List<T> Objects => _objects;

        public void Initialize()
        {
            T newObject = Instantiate(_prefabObject);

            AddObject(newObject);

            DeactivateObject(newObject.gameObject);
        }

        public T GetObject(Vector3 targetPosition, Quaternion rotation)
        {
            TryExpand();

            T newObject = _objects[_objects.Count - 1];

            newObject.transform.position = targetPosition;
            newObject.transform.rotation = rotation;

            ActiveObject(newObject.gameObject);

            _objects.Remove(newObject);

            return newObject;
        }

        public T GetRemoveLastObject()
        {
            TryExpand();

            T lastObject = _objects[_objects.Count - 1];

            _objects.Remove(lastObject);

            return lastObject;
        }

        public void ActiveObject(GameObject obj)
        {
            obj.SetActive(true);
        }

        public void ReturnObject(T obj)
        {
            DeactivateObject(obj.gameObject);

            AddObject(obj);

            ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();
        }

        private void TryExpand()
        {
            float minValue = 0;

            if (_objects.Count <= minValue)
            {
                T instantiateObject = Instantiate(_prefabObject);

                instantiateObject.name = _prefabObject.name;

                DeactivateObject(instantiateObject.gameObject);

                AddObject(instantiateObject);
            }
        }

        private void AddObject(T obj)
        {
            _objects.Add(obj);
        }

        private void DeactivateObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
