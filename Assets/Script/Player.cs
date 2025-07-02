using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;
    [SerializeField, Header("ジャンプ速度")]
    private float _jumpSpeed;
    [SerializeField, Header("体力")]
    private int _hp;

    private Vector2 _inputDirection;
    private Rigidbody2D _rigid;
    private bool _bJump;

    void Start() {
        _rigid = GetComponent<Rigidbody2D>();
        _bJump = false;
    }

    void Update() {
        _Move();
        Debug.Log(_hp);
    }

    private void _Move(){
        _rigid.linearVelocity = new Vector2(_inputDirection.x * _moveSpeed, _rigid.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            _bJump = false;
        }
        if (collision.gameObject.tag == "Enemy") {
            _HitEnemy(collision.gameObject);
        }
    }

    private void _HitEnemy(GameObject enemy) {
        float halfScaleY = enemy.transform.lossyScale.y / 2.0f;
        float enemyHalfScaleY = transform.lossyScale.y / 2.0f;
        if(transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f)) {
            Destroy(enemy);
            _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
        else {
            enemy.GetComponent<Enemy>().PlayerDamage(this);
        }
    }

    private void _Dead() {
        if (_hp <= 0) {
            Destroy(gameObject);
        }
    }

    public void _OnMove(InputAction.CallbackContext context) {
        _inputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (!context.performed || _bJump) return;

        _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        _bJump = true;
    }

    public void Damage(int damage) {
        _hp = Mathf.Max(_hp - damage, 0);
        _Dead();
    }

    public int GetHP() {
        return _hp;
    }
}