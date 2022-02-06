using UnityEngine;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] [Range(10, 50)] protected int damagePower = 10;
    [SerializeField] protected ParticleSystem buildVFX;
    [SerializeField] protected Transform top;
    [SerializeField] protected ParticleSystem bulletVFX;


    private float _attackTimer;
    private Monster _target;
    private Animator _animator;

    
    private void Start() {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);

        _animator = GetComponent<Animator>();
        Instantiate(buildVFX, transform.position, Quaternion.identity);
    }

    private void Update() 
    {
        SetTarget();    
        Aim();          
        Attack();       
    }

    // Sets if there is monster in the range
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

    // Sets rotation of the Top towards target
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

    // Attacks if there is target
    private void Attack()
    {
        if (_target && _attackTimer < 0)
        {
            if (_target.Damage(damagePower))
            {
                GameManager.Instance.IncrementKill();
            }

            // SFX
            GameManager.Instance.AudioManager.Play("Fire");
            // VFX
            _animator.SetTrigger("Fire");
            SetLine();

            _attackTimer = 1;
        }

        _attackTimer -= Time.deltaTime;
    }

    private void SetLine()
    {
        bulletVFX.gameObject.SetActive(false);
        bulletVFX.transform.position = transform.position;     
        bulletVFX.gameObject.SetActive(true);

        bulletVFX.transform.DOMove(_target.transform.position, 0.05f).SetEase(Ease.Linear)
        .OnComplete(() => 
        {
            bulletVFX.Stop();
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