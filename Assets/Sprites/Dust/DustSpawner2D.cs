using UnityEngine;

public class DustSpawner2D : MonoBehaviour
{
    [Header("Prefabs & Spawn Area")]
    public GameObject dustPrefab;
    public BoxCollider2D spawnArea;

    [Header("Spawn Settings")]
    [Tooltip("Số lượng dust cần spawn")]
    public int spawnCount = 10;
    public float checkRadius = 0.3f;
    public LayerMask collisionMask;
    public int maxAttemptsPerDust = 50;

    private void Start()
    {
        SpawnDusts();
    }

    public void SpawnDusts()
    {
        Bounds bounds = spawnArea.bounds;
        int spawned = 0;
        int attempts = 0;

        while (spawned < spawnCount && attempts < spawnCount * maxAttemptsPerDust)
        {
            attempts++;

            Vector2 randomPos = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            if (Physics2D.OverlapCircle(randomPos, checkRadius, collisionMask) == null)
            {
                Instantiate(dustPrefab, randomPos, Quaternion.identity);
                spawned++;
            }
        }

        Debug.Log($"[Spawner] Đã spawn {spawned}/{spawnCount} dusts sau {attempts} lần thử.");
        GameManager.Instance?.SetTotalDusts(spawned);
    }

    private void OnDrawGizmos()
    {
        if (spawnArea != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(spawnArea.bounds.center, spawnArea.bounds.size);
        }
    }
}
