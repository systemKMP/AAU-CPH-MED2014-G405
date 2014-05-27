using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{

    bool showDoorToolpit;
    string doorTooltipText = "PRESS MOUSE TO OPEN";

    private int doorTag = 1 << 8;

    void Start()
    {
        Screen.lockCursor = true;
        showDoorToolpit = false;
    }

    void OnApplicationFocus()
    {
        Screen.lockCursor = true;
    }

    void Update()
    {
        RaycastHit ray; //Check if the door is ahead of the player
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out ray, 2.0f, doorTag))
        {
            DoorController dc = ray.transform.gameObject.GetComponent<DoorController>();
            if ((dc.state == DoorState.Closed || dc.state == DoorState.Closing) && !dc.permaClosed)
            {
                doorTooltipText = "PRESS E TO OPEN";
                showDoorToolpit = true;
            }
            else if (dc.manualClosing)
            {
                doorTooltipText = "PRESS E TO CLOSE";
                showDoorToolpit = true;
            }
            else if (dc.permaClosed && dc.state == DoorState.Closed)
            {
                doorTooltipText = "    LOCKED";
                showDoorToolpit = true;
            }
            else
            {
                showDoorToolpit = false;
            }
    if (Input.GetKeyDown(KeyCode.E))
    {//Acivate the appropriate action to the door
        dc.ActivateDoor();
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
        {//Shows the tooltip
            GUI.Label(new Rect(Screen.width / 2 - 80.0f, Screen.height / 1.5f, 160.0f, 40.0f), doorTooltipText); 
        }
    }

}
