using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Transform _curTarget;

    [SerializeField] private float _speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        _curTarget = pointB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _curTarget.position, _speed * Time.deltaTime);
        if (transform.position == pointA.transform.position)
        {
            _curTarget = pointB;
        }
        if (transform.position == pointB.transform.position)
        {
            _curTarget = pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
