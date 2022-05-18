using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public float _speed = 8.0f;
    protected float _jumpStrength = 50.0f;
    protected float _gravity = 5.0f;
    public float _verticalSpeed = 0.0f;
}
