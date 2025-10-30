using UnityEngine;
using UnityEngine.U2D;

public class PathSpawner : MonoBehaviour
{
    [Header("1. Đối tượng cần gán")]
    [SerializeField] SpriteShapeController spriteShapeController;
    [SerializeField] GameObject snowflakePrefab;

    [Header("2. Tùy chỉnh Spawning")]
    [SerializeField] float startOffset = 50f;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float verticalOffset = 1.5f;
    [Range(0, 1)]
    [SerializeField] float spawnProbability = 0.7f;

    [Header("3. Tùy chỉnh Độ chính xác")]
    [SerializeField] int stepsPerSegment = 30;

    void Start()
    {
        if (spriteShapeController == null || snowflakePrefab == null)
        {
            Debug.LogError("Lỗi PathSpawner: Vui lòng gán 'Sprite Shape Controller' và 'Snowflake Prefab' trong Inspector.");
            return;
        }
        SpawnItemsAlongPath();
    }

    void SpawnItemsAlongPath()
    {
        Spline spline = spriteShapeController.spline;
        if (spline.GetPointCount() < 2) return;

        float distanceTraveled = 0f;
        float nextSpawnDistance = startOffset;

        for (int i = 0; i < spline.GetPointCount() - 1; i++)
        {
            Vector2 p0 = spriteShapeController.transform.TransformPoint(spline.GetPosition(i));
            Vector2 p3 = spriteShapeController.transform.TransformPoint(spline.GetPosition(i + 1));
            Vector2 p1 = p0 + (Vector2)spriteShapeController.transform.TransformVector(spline.GetRightTangent(i));
            Vector2 p2 = p3 + (Vector2)spriteShapeController.transform.TransformVector(spline.GetLeftTangent(i + 1));
            Vector2 previousPoint = p0;

            for (int step = 1; step <= stepsPerSegment; step++)
            {
                float t = (float)step / stepsPerSegment;
                Vector2 currentPoint = GetBezierPoint(p0, p1, p2, p3, t);
                distanceTraveled += Vector2.Distance(previousPoint, currentPoint);

                if (distanceTraveled >= nextSpawnDistance)
                {
                    if (Random.value < spawnProbability)
                    {
                        Vector2 spawnPosition = new Vector2(currentPoint.x, currentPoint.y + verticalOffset);
                        Instantiate(snowflakePrefab, spawnPosition, Quaternion.identity, this.transform);
                    }
                    nextSpawnDistance += spawnInterval;
                }
                previousPoint = currentPoint;
            }
        }
    }

    Vector2 GetBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }
}
