﻿using UnityEngine;
using System.Collections;

public enum DoorState { Closed, Opening, Closing, Open };

public class DoorController : MonoBehaviour
{

    public DoorState state = DoorState.Closed;

    public bool manualClosing = false;

    public Transform otherPos;

    private float stateTransitionDist = 0.01f;

    private Vector3 closedPos;
    private Vector3 openPos;

    public bool permaClosed;

    public DoorController connectedDoor;

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

    /// <summary>
    /// Does the appropriate doors action
    /// </summary>
    public void ActivateDoor()
    {
        if (!permaClosed)
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
        if (connectedDoor != null)
        {
            if (connectedDoor.state != state)
            {
                connectedDoor.ActivateDoor();
            }
        }
    }

    /// <summary>
    /// Locks and closes the door
    /// </summary>
    public void PermanentClose()
    {
        permaClosed = true;
        state = DoorState.Closing;
        if (connectedDoor != null)
        {
            connectedDoor.permaClosed = true;
            connectedDoor.state = DoorState.Closing;
        }
    }

    /// <summary>
    /// Instantly closes the door
    /// </summary>
    public void InstantClose()
    {
        transform.position = closedPos;
        state = DoorState.Closed;
        if (connectedDoor != null)
        {
            connectedDoor.transform.position = connectedDoor.closedPos;
            connectedDoor.state = DoorState.Closed;
        }
    }
}
