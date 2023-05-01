using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    public Vector3 TargetPos => _targetPos;

    // [SerializeField] private bool _testBool = false;
    [SerializeField] private LayerMask _groundLayer = new LayerMask();
    [SerializeField] private Transform _legPoint; 
    [SerializeField] private GameObject _mainBone;
    
    private float _legMovementSpeed = 6f;
    private SpiderMovement _spiderMovement;
    private bool _movingLegForward = false;
    private bool _canDetectBackDistance = true;

    private Vector3 _targetPos;
    private Vector3 _targetRot;
    private Vector3 _startingRot;
    private float _current, _target;
    private Vector3 raycastSpawnPoint;

    private void Awake() {
        _spiderMovement = FindObjectOfType<SpiderMovement>();
        _legMovementSpeed = _spiderMovement.MoveSpeed * 3f;
    }

    private void Start() {
        _targetPos = _legPoint.position;
        _startingRot = _mainBone.transform.localEulerAngles;
        _targetRot = new Vector3(0, 0, _mainBone.transform.localEulerAngles.z - 30f);
    }
    
    private void Update() {
        MoveLeg();
        RotateMainBone();
    }

    private void RotateMainBone() {
        if (_movingLegForward) {
            _target = 1;
        } else {
            _target = 0;
        }

        _current = Mathf.MoveTowards(_current, _target, _spiderMovement.MoveSpeed / 4 * Time.deltaTime);

        _mainBone.transform.rotation = Quaternion.Lerp(Quaternion.Euler(_startingRot), Quaternion.Euler(_targetRot), _current * 4f);
    }

    private void MoveLeg() {

        if (Vector2.Distance(transform.position, _legPoint.position) > 1.8f && _canDetectBackDistance && !_movingLegForward)
        {
            _targetPos = _legPoint.position + new Vector3(Random.Range(2f, 4f), 0, 0);

            raycastSpawnPoint = new Vector3(_targetPos.x, _targetPos.y + 10f, _targetPos.z);

            RaycastHit2D hit = Physics2D.Raycast(raycastSpawnPoint, Vector3.down, Mathf.Infinity, _groundLayer);
            if (hit.collider != null)
            {
                _targetPos = hit.point;
            }

            _movingLegForward = true;
            
            _targetRot = new Vector3(0, 0, _mainBone.transform.localEulerAngles.z - 30f);
        }

        if (_movingLegForward) {
            transform.position = Vector3.Lerp(transform.position, _targetPos, _legMovementSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, _targetPos) < .1f && _movingLegForward) {
            _movingLegForward = false;
            _canDetectBackDistance = false;
            StartCoroutine(CanDetectBackDistanceRoutine());
        }
    }

    private IEnumerator CanDetectBackDistanceRoutine() {
        yield return new WaitForSeconds(1 / _spiderMovement.MoveSpeed);
        _canDetectBackDistance = true;
    }
}
