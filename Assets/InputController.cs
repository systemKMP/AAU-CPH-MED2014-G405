﻿using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{

    bool showDoorToolpit;
    string doorTooltipText = "PRESS MOUSE TO OPEN";

    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;
        showDoorToolpit = false;
    }

    void OnApplicationFocus()
    {
        Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit ray;
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out ray, 1.5f, 1 << 8))
        {
            DoorController dc = ray.transform.gameObject.GetComponent<DoorController>();
            if (dc.state == DoorState.Closed || dc.state == DoorState.Closing)
            {
                doorTooltipText = "PRESS E TO OPEN";
            }
            else
            {
                doorTooltipText = "PRESS E TO CLOSE";

            }
            showDoorToolpit = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                dc.OpenDoor();
            }
        }
        else
        {
            showDoorToolpit = false;
        }
    }

    void OnGUI()
    {
        if (showDoorToolpit)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80.0f, Screen.height / 1.5f, 160.0f, 40.0f), doorTooltipText); 
        }
    }

}
