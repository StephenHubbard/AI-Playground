using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   private Transform _subject;
   private Vector2 _startPosition;
   private Camera _cam;
   private float _startZ;
   private Vector2 _travel => (Vector2)_cam.transform.position - _startPosition;
   private float _distanceFromSubject => transform.position.z - _subject.position.z;
   private float _clippingPlane => (_cam.transform.position.z + (_distanceFromSubject > 0 ? _cam.farClipPlane : _cam.nearClipPlane));
   private float _parallaxFactor => Mathf.Abs(_distanceFromSubject) / _clippingPlane;

    private void Awake() {
        _cam = Camera.main;
    }

   private void Start() {
        _subject = FindObjectOfType<CharacterController2D>().transform;
        _startPosition = transform.position;
        _startZ = transform.position.z;
   }

   private void FixedUpdate() {
        Vector2 newPos =  _startPosition + _travel * _parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, _startZ);
   }
}
