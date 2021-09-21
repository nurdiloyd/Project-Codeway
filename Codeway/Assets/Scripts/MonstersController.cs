using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MonstersController : MonoBehaviour
{
    private GameManager _gm;
    private List<Monster> _monsters = new List<Monster>();


    public void Init(GameManager manager) {
        _gm = manager;

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(_gm.GameConfig.Monster, Vector3.zero, Quaternion.identity, transform);
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(10);
        }        
    }
}
