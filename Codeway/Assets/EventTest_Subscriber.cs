using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest_Subscriber : MonoBehaviour
{
    void Start() {
        EventTest_Publisher publisher = GetComponent<EventTest_Publisher>();

        publisher.FloatEvent += EventTest_FloatEvent; 

        publisher.ActionEvent += EventTest_ActionEvent;
    }

    private void EventTest_FloatEvent(float f) {
        Debug.Log(f);
    }

    private void EventTest_ActionEvent(bool b, int i) {
        Debug.Log(b + "" + i);
    }
}
