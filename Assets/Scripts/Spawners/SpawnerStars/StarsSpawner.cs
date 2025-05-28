using System;
using Spawners;
using SpawnerStars;
using Stars;
using UnityEngine;

public class StarsSpawner : Spawner<StarsPool>
{
    [SerializeField] private float _duractionFakeCoin;

    public event Action<Star> StarReleasedReported;

    public void Init()
    {
        int maxQuantityObjects = 20;
            
        for (int i = 0; i < maxQuantityObjects; i++)
        {
            ObjectsPool.Initialize();
        }
    }

    public Star GetCreate(Vector3 position)
    {
        Star star = ObjectsPool.GetObject(position, Quaternion.Euler(new Vector3(90f, 0f, 0f)));

        star.Released += ReportFakeCoinReleased;

        return star;
    }

    private void ReportFakeCoinReleased(Star star)
    {
        star.Released -= ReportFakeCoinReleased;

        StarReleasedReported?.Invoke(star);
    }
}
