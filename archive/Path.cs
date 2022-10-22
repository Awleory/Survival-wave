using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private List<Transform> _pathPoints = new List<Transform>();

    public void AddPoint(Transform point)
    {
        _pathPoints.Add(point);
    }

    public Path(Transform startPoint)
    {
        AddPoint(startPoint);
    }

    public bool TryRemovePoint(Transform point)
    {
        if (_pathPoints.Contains(point))
        {
            _pathPoints.Remove(point);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool TryCutPath(Transform point, bool includeThePoint = false)
    {
        if (_pathPoints.Contains(point) == false)
        {
            return false;
        }

        int cutAtIndex = _pathPoints.IndexOf(point);
        
        if (includeThePoint == false)
        {
            cutAtIndex = cutAtIndex + 1;
        }

        if (cutAtIndex >= _pathPoints.Count)
        {
            return true;
        }

        _pathPoints.RemoveRange(cutAtIndex, _pathPoints.Count - cutAtIndex);

        return true;
    }

    private void ReplacePoint(Transform point, Vector3 newPosition)
    {

    }
}
