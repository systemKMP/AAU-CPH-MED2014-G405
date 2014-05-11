﻿using UnityEngine;
using System.Collections;

public enum DoorState { Closed, Opening, Closing, Open };

public class DoorController : MonoBehaviour
{

    public DoorState state = DoorState.Closed;

    public bool manualClosing = false;

    public Transform otherPos;

    private float stateTransitionDist = 0.05f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool permaClosed;

    void Start()
    {
        permaClosed = false;
        if (state == DoorState.Closed)
        {
            closedPos = transform.position;
            openPos = otherPos.position;
        }
        else
        {
            openPos = transform.position;
        }

    }

    void Update()
    {
        if (state == DoorState.Opening)
        {
            Debug.Log(state);
            transform.position = Vector3.MoveTowards(transform.position, openPos, Time.deltaTime * 2.0f);
            if ((transform.position - openPos).magnitude < stateTransitionDist)
            {
                state = DoorState.Open;
            }

        }
        else if (state == DoorState.Closing)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPos, Time.deltaTime * 2.0f);
            if ((transform.position - closedPos).magnitude < stateTransitionDist)
            {
                state = DoorState.Closed;
            }
        }

    }

    public void OpenDoor()
    {

        if (state == DoorState.Closed || state == DoorState.Closing)
        {
            state = DoorState.Opening;
        }
        else if ((state == DoorState.Open || state == DoorState.Opening) && manualClosing)
        {
            state = DoorState.Closing;
        }
    }

    public void PermanentClose()
    {
        throw new System.NotImplementedException();
    }

    public void InstantClose()
    {
        throw new System.NotImplementedException();
    }
}
