using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private int _lives = 3;
    [SerializeField] private bool _died;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager Null");
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (_died == false)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 direction = new Vector3(horizontalInput, 0, 0);
            Vector3 velocity = direction * _speed;

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

            _controller.Move(velocity * Time.deltaTime);
        }
    }
    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }
    public void LoseLife()
    {
        _died = true;
        _lives--;
        //transform.position = new Vector3(0, 1.58f, 0);
        if (_lives > 0)
        {
            StartCoroutine(ControlPlayer());
            _uiManager.UpdateLivesDisplay(_lives);
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            _uiManager.UpdateLivesDisplay(0);
            StartCoroutine(RestartGame());
        }
    }
    IEnumerator ControlPlayer()
    {
        yield return new WaitForSeconds(1.5f);       
        _died = false;
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }
}
