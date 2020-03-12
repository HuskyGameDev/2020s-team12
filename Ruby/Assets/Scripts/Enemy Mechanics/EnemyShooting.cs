using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float      fireDelay = .2f; // Delay between shots
    public float      dispersion; // TODO will implement later
    public float      offsetY = 0.0f; // offsets projectile origin Y
    public float      offsetX = 0.0f; // offsets projectile origin X
    public GameObject projectilePrefab;

    private GameObject  player;
    private LayerMask   obstacles;
    public MovingEnemy  movingEnemy;
    public float       timer = 0;

    private AggroTimer aggroTimer;


    // Start is called before the first frame update
    void Start()
    {
        movingEnemy = GetComponent<MovingEnemy>();
        obstacles = 1<<8; // This sets the layerMask to the "Obstacle" unity layer. It's a literal bit mask. Ask Kasey if you need clarification.
        player = GameObject.FindWithTag("Player");
        aggroTimer = GetComponent<AggroTimer>();
    }

    // coroutine that spawns projectile and moves it towards a given point
    void Shoot()
    {
        // Determine angle projectile needs to move in
        float angleX = player.transform.position.x - transform.position.x;
        float angleY = player.transform.position.y - transform.position.y;
        float shotAngle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg - 90f;

        // Instantiate given prefab
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(offsetX, offsetY, shotAngle)));

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //seesPlayer = movingEnemy.seesPlayer;
        Vector3 playerDirection = transform.position - player.transform.position;

        // if enemy sees player and is within firerate
        if (aggroTimer.isAggro)
        {
            Shoot();
            timer = 0.0f;
        }
    }
}
