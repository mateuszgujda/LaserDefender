using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig= null;
    List<Transform> waypoints;
    float moveSpeed = 2f;
    int WaypointIndex= 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
        transform.position = waypoints[WaypointIndex].transform.position;
    }

    public void SetWaveConfig(WaveConfig waveToSet)
    {
        waveConfig = waveToSet;
    }
    // Update is called once per frame
    void Update()
    {
        if(WaypointIndex < waypoints.Count)
        {
            Vector2  targetPosition = waypoints[WaypointIndex].position;
            float movementInOneFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementInOneFrame);
            if((Vector2)transform.position == targetPosition )
            {
                WaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
