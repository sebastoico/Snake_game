using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    public Snake snake;

    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    // Update is called once per frame
    void Update() { }

    private void RandomizePosition()
    {
        bool isOverSnake;
        Bounds bounds = this.gridArea.bounds;
        float x;
        float y;

        do
        {
            isOverSnake = false;
            x = Random.Range(bounds.min.x, bounds.max.x);
            y = Random.Range(bounds.min.y, bounds.max.y);
            foreach (Transform snakePosition in snake._segments)
            {
                isOverSnake = (
                    (snakePosition.position.x == x || snakePosition.position.y == y)
                    && isOverSnake == false
                );
            }
        } while (isOverSnake);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
