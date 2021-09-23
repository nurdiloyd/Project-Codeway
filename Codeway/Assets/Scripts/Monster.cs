using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    [HideInInspector] public int ID;

    [SerializeField] protected float speed = 1;
    [SerializeField] protected int health = 100;
    [SerializeField] protected ParticleSystem deathVFX;

    private MonstersController _mc;
    private Tween _moveTween;
    private Tween _rotTween;
    private Animator _animator;


    public void Init(MonstersController monstersController, int id) 
    {
        _mc = monstersController;
        ID = id;

        _mc.KillAllMonsters += Kill;
        _animator = GetComponent<Animator>();
    }

    // Moves the monster by Tween
    public void Move(int pathIndex)
    {
        if (pathIndex < _mc.Path.Count)
        {
            _rotTween = transform.DORotate(Rot2D.RotationZ(transform.position, _mc.Path[pathIndex]), 1 / (speed * 2));
            _moveTween = transform.DOMove(_mc.Path[pathIndex], 1 / speed).SetEase(Ease.Linear).OnComplete(() => 
            {
                Move(pathIndex + 1);
            });
        }
        else if (pathIndex == _mc.Path.Count)
        {
            Arrived();
        }
    }

    // Damages the monster by the value
    public bool Damage(int damage)
    {
        bool killed = false;

        health -= damage;
        _animator.SetTrigger("Damage");

        if (health < 0)
        {
            killed = true;
            Kill();
        }

        return killed;
    }

    // When the monster arrived to the end
    public void Arrived()
    {
        _mc.AMonsterArrived();
        Kill();
    }

    // Kills the monster
    public void Kill() 
    {
        _mc.KillAllMonsters -= Kill;

        // SFX
        GameManager.Instance.AudioManager.Play("MonsterDeath");
        // VFX
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        
        _rotTween.Kill();
        _moveTween.Kill();
        
        Destroy(gameObject);
    }
}
