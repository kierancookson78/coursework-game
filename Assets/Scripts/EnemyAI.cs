using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float detectionDistance;
    private float distanceToPlayer;

    void Update()
    {
        if (player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distanceToPlayer < detectionDistance )
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle - 90), Time.fixedDeltaTime * rotationSpeed);
            }
        }
    }
}
