using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float MoveSpeed => _moveSpeed;

    [SerializeField] public float _moveSpeed = 10f;
    [SerializeField] private SpiderLeg[] _allSpiderLegs;

    private Rigidbody2D _rb;
    private float _current, _target;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);

        float allY = 0;
        float backLegsY = 0;
        float frontLegsY = 0;

        for (int i = 0; i < _allSpiderLegs.Length; i++)
        {
            allY += _allSpiderLegs[i].TargetPos.y;

            if (i <= 3) {
                backLegsY += _allSpiderLegs[i].TargetPos.y;
            } else {
                frontLegsY += _allSpiderLegs[i].TargetPos.y;
            }
        }

        float averageY = allY / 8;
        float averageBackY = backLegsY / 8;
        float averageFrontY = frontLegsY / 8;

        float diff = averageFrontY / averageBackY;

        // _current = Mathf.MoveTowards(_current, 1, _moveSpeed * 10f * Time.deltaTime);

        // Vector3 goalPos = new Vector3(transform.position.x, averageY + 3f, 0);
        transform.position = new Vector3(transform.position.x, averageY + 3f, 0);

        // transform.position = Vector3.Lerp(transform.position, goalPos, _current);

        transform.rotation = Quaternion.Euler(0, 0, (diff - 1f) * 10f);
    }
}
