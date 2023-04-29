using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxFactor = .9f;

    private Transform _subject;
    private Vector2 _startPosition;
    private Camera _cam;

    // private Vector2 _travel => (Vector2)_cam.transform.position - _startPosition;
    private Vector2 _travel => new Vector2(_cam.transform.position.x - _startPosition.x, 0);

    private void Awake() {
        _cam = Camera.main;
    }

    private void Start() {
            _subject = FindObjectOfType<CharacterController2D>().transform;
            _startPosition = transform.position;
    }

    private void FixedUpdate() {
            transform.position = _startPosition + _travel * _parallaxFactor;
    }
}
