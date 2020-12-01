﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 15.0f;
    private float _yVelocity;
    [SerializeField] private bool _doubleJump;
    private int _coins;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        //if grounded do nothing
        //else apply gravity
        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {        
                _yVelocity = _jumpHeight;
                _doubleJump = true;
            }
        }
       
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _doubleJump == true)
            {
                _yVelocity += _jumpHeight * 1.25f;
                _doubleJump = false;
            }
            _yVelocity -= _gravity;
        }
        velocity.y = _yVelocity;

        //velocity = direction with speed
        _controller.Move(velocity * Time.deltaTime);

    }
    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }
}
