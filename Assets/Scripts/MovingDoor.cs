using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : Gear
{
    [Tooltip("移动点列表")]
    [SerializeField] protected List<Transform> movePoints;
    [Tooltip("移动速度")]
    [SerializeField] protected float moveSpeed = 1;
    [Tooltip("抵达误差距离")]
    [SerializeField] protected float reachDistance = 0.02f;

    /// <summary>
    /// 移动点列表
    /// </summary>
    public List<Transform> MovePoints { get { return movePoints; } }
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed { get { return moveSpeed; } }

    private int _moveIndex;
    private Vector2 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _moveIndex = 0;
        _initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            if (_moveIndex < movePoints.Count)
            {
                float distance = Vector2.Distance(movePoints[_moveIndex].position, transform.position);
                transform.position = Vector2.MoveTowards(transform.position, movePoints[_moveIndex].position,
                    moveSpeed * Time.deltaTime);
                if (distance <= reachDistance)
                    ++_moveIndex;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 position = triggered ? new Vector3(_initialPosition.x, _initialPosition.y, 0f) : transform.position;
        for (var i = 0; i < movePoints.Count; ++i)
        {
            Gizmos.DrawLine(position, movePoints[i].position);
            Gizmos.DrawWireSphere(movePoints[i].position, reachDistance);
            position = movePoints[i].position;
        }
    }

    protected virtual void OnReStart()
    {
        _moveIndex = 0;
        transform.position = _initialPosition;
    }
}
