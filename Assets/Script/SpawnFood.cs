using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public SpawnFood self;

    // Food Prefab
    public GameObject foodPrefab;
    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;
    public bool Continuous = false;
    public Transform foodGOs = null;

    private void Awake()
    {
        self = this;
    }

    private void Start()
    {
        InvokeRepeating("Spawn", 0, 0.1f);
    }

    // Spawn one piece of food
    private void Spawn()
    {
        if (Continuous)
        {
            // x position between left & right border
            int x = (int)Random.Range(borderLeft.position.x,
                                      borderRight.position.x);

            // y position between top & bottom border
            int y = (int)Random.Range(borderBottom.position.y,
                                      borderTop.position.y);

            // Instantiate the food at (x, y)
            Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity); // default rotation
        }
        else
        {
            GameObject food = GameObject.Find("food(Clone)");
            if (food == null)
            {
                // x position between left & right border
                int x = (int)Random.Range(borderLeft.position.x,
                                          borderRight.position.x);

                // y position between top & bottom border
                int y = (int)Random.Range(borderBottom.position.y,
                                          borderTop.position.y);

                // Instantiate the food at (x, y)
                GameObject tGO = Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity) as GameObject; // default rotation

                tGO.transform.parent = foodGOs;
            }
        }
    }
}