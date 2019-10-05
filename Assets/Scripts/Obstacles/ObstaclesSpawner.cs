using System.Collections;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public CObstacle ObstaclePrefab;
    public float MoveTime;
    public float SpawnPause;
    public Transform SpawnPoint;
    public Transform FinishPoint;

    private Coroutine _spwanCoroutine;
    private CObstacle _obstacle;

    public void Activate()
    {
        Deactivate();
        _spwanCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void Deactivate()
    {
        if (_spwanCoroutine != null)
        {
            StopCoroutine(_spwanCoroutine);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        if (_obstacle == null)
        {
            _obstacle = Instantiate(ObstaclePrefab);
        }

        while (true)
        {
            var t = 0f;

            _obstacle.gameObject.SetActive(true);

            do
            {
                t += Time.deltaTime;
                _obstacle.transform.position = Vector3.Lerp(SpawnPoint.position, FinishPoint.position, t / MoveTime);
                yield return null;
            } while (t < MoveTime);

            _obstacle.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(SpawnPause);
        }
    }
}
