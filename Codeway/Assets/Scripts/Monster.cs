using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    [HideInInspector] public int ID;

    [SerializeField] protected float speed = 1;
    [SerializeField] protected int health = 100;

    private MonstersController _mc;
    private Tween _moveTween;
    private Tween _rotTween;


    public void Init(MonstersController monstersController, int id) 
    {
        _mc = monstersController;
        ID = id;

        _mc.KillAllMonsters += Kill;
    }

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

    public bool Damage(int damage)
    {
        bool killed = false;

        health -= damage;

        if (health < 0)
        {
            killed = true;
            Kill();
        }

        return killed;
    }

    public void Arrived()
    {
        _mc.AMonsterArrived();
        Kill();
    }

    public void Kill() 
    {
        _mc.KillAllMonsters -= Kill;
        _rotTween.Kill();
        _moveTween.Kill();
        Destroy(gameObject);
    }
}
