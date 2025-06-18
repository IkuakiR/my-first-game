using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    private Rigidbody2D _rigid;


    void Start() {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update() {
        _Move();
    }

    private void _Move() {
        _rigid.linearVelocity = new Vector2(Vector2.left.x * _moveSpeed, _rigid.linearVelocity.y);
    }
}
