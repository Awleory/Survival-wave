using UnityEngine;

public class BulletsFactory : ObjectPool<BulletPresenter>
{
    public void Initialize(BulletPresenter bulletPresenter, Transform container, float shootsPerSecond)
    {
        int rateCapacity = 2;
        int poolCapacity = (int)(shootsPerSecond * bulletPresenter.GetComponent<BulletConfig>().LifeTime + shootsPerSecond) * rateCapacity;
        Initialize(bulletPresenter, poolCapacity, container);
    }

    public bool TryGetBullet(BulletPresenter template, out BulletPresenter bulletPresenter, Vector2 direction)
    {
        if (TryGetObject(template, out bulletPresenter, false) == false)
            return false;

        if (bulletPresenter.Model == null)
        {
            bulletPresenter.Initialize(direction);
            bulletPresenter.EndInitialize();
        }

        return bulletPresenter != null;
    }
}
