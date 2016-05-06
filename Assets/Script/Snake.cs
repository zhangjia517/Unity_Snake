using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Snake self;

    public Transform trailGOs = null;
    // Current Movement Direction
    // (by default it moves to the right)
    private Vector2 dir = Vector2.right;

    private List<Transform> tail = new List<Transform>();

    // Did the snake eat something?
    private bool ate = false;

    // Tail Prefab
    public GameObject tailPrefab;

    public Action OnLose;
    private bool canChangeDir = false;

    public float moveSpeed = 0.1f;

    private void Awake()
    {
        self = this;
        moveSpeed = 0.2f;
    }

    private void Start()
    {
        InvokeRepeating("Accerlerate", 20, 20f);
        // Move the Snake every 300ms
        ResetSpeed();
    }

    private void ResetSpeed()
    {
        CancelInvoke("Move");
        InvokeRepeating("Move", 0, moveSpeed);
    }

    private void Accerlerate()
    {
        moveSpeed = moveSpeed / 1.05f;
        ResetSpeed();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("food"))
        {
            // Get longer in next Move call
            ate = true;
            Game.score++;
            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            if (Game.curStatus == GameStatus.GameOver) return;
            OnLose();
        }
    }

    private void Update()
    {
        Debug.Log(moveSpeed);
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (dir != -Vector2.right && canChangeDir)
            {
                dir = Vector2.right;
                canChangeDir = false;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (dir != Vector2.up && canChangeDir)
            {
                dir = -Vector2.up;    // '-up' means 'down'
                canChangeDir = false;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (dir != Vector2.right && canChangeDir)
            {
                dir = -Vector2.right; // '-right' means 'left'
                canChangeDir = false;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (dir != -Vector2.up && canChangeDir)
            {
                dir = Vector2.up;
                canChangeDir = false;
            }
        }
    }

    private void Move()
    {
        if (Game.curStatus != GameStatus.Play) return;

        canChangeDir = true;

        // Move head into new direction
        Vector2 v = transform.position;

        transform.Translate(dir);

        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                   v,
                                                   Quaternion.identity);

            g.transform.parent = trailGOs;

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
}