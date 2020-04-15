using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class MovingEnemy : MonoBehaviour
{
    Transform player; // Does not need to be set in editor
    public bool seesPlayer = false;
    public bool canMove = true;

    Rigidbody2D rb;
    Animator anim;
    Vector3 movement; // Movement 3D vector
    AggroTimer aggroTimer;

    // A* stuff
    Path path;
    Seeker seeker;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public float moveVelocity = 100f;
    public float nextWaypointDistance = .1f;
    public float seekerOffsetX = 0.55f;
    public float seekerOffsetY = 1f;
    public bool usePathFinding = true;

    // Start is called before the first frame update
    void Start()
    {
        aggroTimer = transform.GetComponent<AggroTimer>();
        seeker = GetComponent<Seeker>(); // A* seeker

        if (player == null && SceneManager.GetActiveScene().name.Equals("MapA"))
        {
            player = GameObject.Find("Trevor").transform; // Sets trevor if MapA Scene
        }
        else if (player == null) { 
            player = GameObject.Find("Ruby").transform; // Sets Ruby
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Checks generated path for errors and assigns the path variable.
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void MoveTowardsPlayer()
    {
        if (player != null && canMove) // If the player doesn't exist an error will occur, so the player must exist
        {
            if (!usePathFinding)
            {
                float moveX = player.position.x - transform.position.x; // Checks the players coordinates on the x plane in comparison to the enemies
                float moveY = player.position.y - transform.position.y; // Checks the players coordinates on the y plane in comparison to the enemies

                movement = new Vector3(moveX, moveY, 0f); // Creates a movement vector of the difference between the player and the enemy

                movement.Normalize(); // Normalizes so it's not faster on diagonals

                rb.velocity = (movement * moveVelocity); // Moves the enemy towards the player

                float facingAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg - 90f; // Sets the angle the enemy is facing in degrees
                anim.SetFloat("Facing", facingAngle); // Tell the animator which way the enemy is facing to set the appropriate sprites
            }
            else
            {
                InvokeRepeating("UpdatePath", .1f, .1f);
            }

        }
        else
            {
                rb.velocity = Vector3.zero;
            }
        }

    void UpdatePath()
    {
        if (seeker.IsDone() && aggroTimer.isAggro && (GameObject.Find("Ruby") != null || GameObject.Find("Trevor") != null) && usePathFinding)
        {
            // Offset position to center of seeker
            Vector3 seekerOrigin = transform.position;
            seekerOrigin.x = seekerOrigin.x + seekerOffsetX; // apply offsets
            seekerOrigin.y = seekerOrigin.y + seekerOffsetY;

            seeker.StartPath(seekerOrigin, player.position, OnPathComplete); // Generates A* path
        }
    }
       
    public void setMove(bool move)
    {
        this.canMove = move;
    }

    public bool getMove()
    {
        return canMove;
    }

    public void setSee(bool see)
    {
        this.seesPlayer = see;
    }

    public bool getSee()
    {
        return seesPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameObject.Find("Ruby") != null || GameObject.Find("Trevor") != null) && usePathFinding)
        {
            float moveX = rb.velocity.x;
            float moveY = rb.velocity.y;
            float facingAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg - 90f; // Sets the angle the enemy is facing in degrees
            anim.SetFloat("Facing", facingAngle); // Tell the animator which way the enemy is facing to set the appropriate sprites

            // A*
            // Offset position to center of seeker
            Vector2 seekerOrigin = transform.position;
            seekerOrigin.x = seekerOrigin.x + seekerOffsetX; // apply offsets
            seekerOrigin.y = seekerOrigin.y + seekerOffsetY;

            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - seekerOrigin).normalized;
            Vector2 force = direction * moveVelocity * Time.deltaTime; // Multiplied by time to keep speed normal regardless of framerate

            rb.AddForce(force);

            float distance = Vector2.Distance(seekerOrigin, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }
}
