using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DriverAIState
{
    driving,
    stopping,
    waiting,
    idle
}

public class DriverAI : MonoBehaviour
{
    public List<Vector2> destinations;
    public float speed;

    [SerializeField]
    private DriverAIState currentState;

    private int nextDestinationIndex = 1;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        currentState = DriverAIState.driving;
        rb2D = GetComponent<Rigidbody2D>();

        // Start at the first destination
        transform.position = new Vector3(destinations[0].x, destinations[0].y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var step = speed * Time.deltaTime;
        var destination = destinations[nextDestinationIndex];
        var newPosition = Vector2.MoveTowards(transform.position, destination, step);

        rb2D.MovePosition(newPosition);

        if (destination == new Vector2(transform.position.x, transform.position.y))
        {
            nextDestinationIndex++;
            if (nextDestinationIndex >= destinations.Count)
            {
                nextDestinationIndex = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.isTrigger && !collision.otherCollider.isTrigger)
        {
            currentState = DriverAIState.waiting;
        }
    }
}
