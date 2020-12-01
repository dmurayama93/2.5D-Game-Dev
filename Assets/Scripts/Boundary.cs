using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private Player _player;
    [SerializeField] private GameObject _respawnPoint;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.LoseLife();

            other.transform.position = _respawnPoint.transform.position;
        }
    }
}
