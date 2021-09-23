using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] [Range(10, 50)] protected int damagePower = 10;

    private float _attackTimer;
    private Monster _target;


    private void Update() 
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

        if (_attackTimer < 0)
        {
            _attackTimer = 1;

            if (_target && _target.Damage(damagePower))
            {
                GameManager.Instance.IncrementKill();
            }
        }

        _attackTimer -= Time.deltaTime;

        if (_target && Vector2.Distance(transform.position, _target.transform.position) > range)
        {
            _target = null;
        }
    }
}
