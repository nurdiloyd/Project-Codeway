using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest_Publisher : MonoBehaviour
{
    public event EventTestDelegate FloatEvent;
    public delegate void EventTestDelegate(float f);

    public event Action<bool, int> ActionEvent;

    private int _spaceCount;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            _spaceCount++;
            FloatEvent?.Invoke(_spaceCount);

            ActionEvent?.Invoke(true, 56);
        }
    }
}
