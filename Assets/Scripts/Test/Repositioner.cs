using UnityEngine;
using System.Collections;

public class Repositioner : MonoBehaviour
{

    public Transform pos1;
    public Transform pos2;

    public float movementSpeed;
    private float changeOffset = 0.2f;
    bool towardsFirst = true;
    // Update is called once per frame
    void Update()
    {
        if (towardsFirst)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos1.position, Time.deltaTime * movementSpeed);
            if ((transform.position - pos1.position).magnitude < changeOffset) towardsFirst = !towardsFirst;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2.position, Time.deltaTime * movementSpeed);
            if ((transform.position - pos2.position).magnitude < changeOffset) towardsFirst = !towardsFirst;
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.LoadLevel(1);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), "Press Esc to begin test");
    }
}
