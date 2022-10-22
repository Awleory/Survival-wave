using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    [SerializeField] private LayerMask _collisionLayerMask;

    private Entity _entity;
    private Entity _target;

    public PathFinder(Entity entity)
    {
        _entity = entity;
    }

    public Path GetNewPath(Entity target, bool isGhost = false)
    {
        Path path = new Path(_entity.transform);

        UpdatePath(path);

        return path;
    }

    public void UpdatePath(Path path)
    {
        path.AddPoint(_entity.transform);
    }
}
