using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    static Dictionary<GameObject, List<GameObject>> ObjectPools = new Dictionary<GameObject, List<GameObject>>();

    public static GameObject PoolThis(GameObject _object, Vector3 _position = default, Quaternion _rotation = default)
    {
        if (ObjectPools.ContainsKey(_object))
        {
            foreach (GameObject obj in ObjectPools[_object])
            {
                if (obj == null)
                {
                    ObjectPools[_object].Remove(obj);
                }
                else if (!obj.activeInHierarchy)
                {
                    obj.transform.position = _position;
                    obj.transform.rotation= _rotation;

                    obj.SetActive(true);
                    return obj;
                }
            }

            ObjectPools[_object].Add(Instantiate(_object, _position, _rotation));
            return ObjectPools[_object][ObjectPools[_object].Count - 1];
        }

        ObjectPools.Add(_object, new List<GameObject>());

        ObjectPools[_object].Add(Instantiate(_object, _position, _rotation));
        return ObjectPools[_object][ObjectPools[_object].Count - 1];
    }
}
