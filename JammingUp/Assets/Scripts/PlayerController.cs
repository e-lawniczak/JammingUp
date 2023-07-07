using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Transform movePoint;

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        calculateMovePoint();
    }

    private void movePlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            movePoint.position,
            moveSpeed * Time.deltaTime
        );
    }

    private void calculateMovePoint()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");

        if (Vector3.Distance(transform.position, movePoint.position) >= .02f)
            // if the player didnt' reach the point yet - prevent moving 
            return;

        if (Mathf.Abs(inputH) == 1f)
        {
            // detect arrow left or arrow right
            movePoint.position += new Vector3(inputH, 0f, 0f);
        }
        else if (Mathf.Abs(inputV) == 1f)
        {
            // detect arrow up or arrow down
            movePoint.position += new Vector3(0f, inputV, 0f);
        }
    }
}
