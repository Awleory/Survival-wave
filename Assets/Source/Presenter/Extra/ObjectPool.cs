using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<TObject> : MonoBehaviour where TObject : MonoBehaviour
{
    private Transform _container;
    private Dictionary<TObject, List<TObject>> _wholePool = new Dictionary<TObject, List<TObject>>();

    protected void Initialize(TObject template, int count, Transform container)
    {
        if (_wholePool.ContainsKey(template))
            return;

        _container = container;
        List<TObject> localPool = new List<TObject>();

        for (int i = 0; i < count; i++)
        {
            TObject newObject = Instantiate(template, _container);
            localPool.Add(newObject);
            newObject.gameObject.SetActive(false);
        }

        _wholePool.Add(template, localPool);
    }

    public bool TryGetObject(TObject template, out TObject result, bool getRandom)
    {
        result = null;
        List<TObject> localPool;

        if (_wholePool.ContainsKey(template))
            localPool = _wholePool[template];
        else
            return false;

        Predicate<TObject> condition = tObject => tObject.gameObject.activeSelf == false;

        if (getRandom)
        {
            var avaliableObjects = localPool.FindAll(condition);
            if (avaliableObjects.Count > 0)
                result = avaliableObjects[UnityEngine.Random.Range(0, avaliableObjects.Count)];
        }
        else
        {
            result = localPool.First(tObject => tObject.gameObject.activeSelf == false);
        }

        return result != null;
    }
}
