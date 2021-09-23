using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class MonstersController : MonoBehaviour
{
    public event Action KillAllMonsters;

    [HideInInspector] public List<Vector2> Path;
    [SerializeField] protected int monstersPerWave = 5;
    [SerializeField] protected float timeBtwSpawns = 1;
    [SerializeField] protected float timeBtwWaves = 7;

    private bool _spawn = true;


    public void Init() {
        Path = GameManager.Instance.GameBoard.GetPath();

        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        yield return new WaitForSeconds(2);

        int id = 0;
        while (_spawn)
        {
            for (int i = 0; i < monstersPerWave; i++)
            {
                // SFX
                GameManager.Instance.AudioManager.Play("MonsterSpawn");
                
                Monster monster = Instantiate(GameManager.Instance.GameConfig.Monster, Path[0], Quaternion.identity, transform);
                monster.Init(this, id);
                monster.Move(1);
                id += 1;

                yield return new WaitForSeconds(timeBtwSpawns);
            }

            yield return new WaitForSeconds(timeBtwWaves);
        }
    }

    // When a monster arrived to the end
    public void AMonsterArrived()
    {
        _spawn = false;
        KillAllMonsters?.Invoke();
        GameManager.Instance.GameOver();
    }
}
