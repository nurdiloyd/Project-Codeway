using UnityEngine;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] [Range(10, 50)] protected int damagePower = 10;
    [SerializeField] protected Transform top;

    private float _attackTimer;
    private Monster _target;
    private LineRenderer _line;


    private void Update() 
    {
        SetTarget();    // Sets if there is monster in the range
        Aim();          // Sets rotation of the Top towards target
        Attack();       // Attacks if there is target
    }

    private void SetTarget()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var coll in colls)
        {
            if (coll.CompareTag("Monster"))
            {
                Monster monster = coll.GetComponent<Monster>();
                if (_target == null || monster.ID < _target.ID)
                {
                    _target = monster;
                }
            }
        }

        if (_target && Vector2.Distance(transform.position, _target.transform.position) > range)
        {
            _target = null;
        }
    }

    private void Aim()
    {
        if (_target)
        {
            top.transform.eulerAngles = 
                Vector3.Lerp(top.transform.eulerAngles, 
                Rot2D.RotationZ(transform.position, _target.transform.position),
                10 * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (_target && _attackTimer < 0)
        {
            if (_target.Damage(damagePower))
            {
                GameManager.Instance.IncrementKill();
            }

            SetLine();
            _attackTimer = 1;
        }

        _attackTimer -= Time.deltaTime;
    }

    private void SetLine()
    {
        if (_line == null)
        {
            _line = GetComponent<LineRenderer>();
        }

        Vector3 pos = transform.position;
        _line.SetPosition(0, pos);
        _line.SetPosition(1, pos);
        _line.enabled = true;
        DOTween.To(()=> pos, x=> pos = x, _target.transform.position, 0.1f)
        .OnUpdate(() => 
        {
            _line.SetPosition(1, pos);
        })
        .OnComplete(() => 
        {
            _line.enabled = false;
        });
    }
}

// Helper for adjusting 2D rotation
public static class Rot2D  
{
    public static Vector3 RotationZ(Vector2 from, Vector2 to)
    {
        float rotZ = Vector2.SignedAngle(Vector2.right, (to - from));
        rotZ = (rotZ < 0) ? (360 + rotZ) : rotZ;
        return new Vector3(0, 0, rotZ);
    }
}