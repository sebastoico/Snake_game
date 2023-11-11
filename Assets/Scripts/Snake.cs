using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;

    public Text scoreText;
    public int scoreCount;

    private Vector2 _direction = Vector2.zero;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initalSize = 4;

    // Start is called before the first frame update
    void Start()
    {
        ResetState();
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_direction == Vector2.left || _direction == Vector2.right || _direction == Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _direction = Vector2.down;
            }
        }
        if (_direction == Vector2.up || _direction == Vector2.down || _direction == Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _direction = Vector2.right;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 0; i < initalSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = new Vector3(0,2,0);
        _direction = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            scoreText.text = "Score: " + (_segments.Count - initalSize - 1);
        }
        else if (other.tag == "Obstacle" && _direction != Vector2.zero)
        {
            ResetState();
            gameOverMenu.SetActive(true);
        }
    }
}
