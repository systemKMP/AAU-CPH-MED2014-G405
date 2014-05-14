using UnityEngine;
using System.Collections;
using TBE_3DCore;

public class DroneController : MonoBehaviour
{

    public AudioSource source2d;
    public TBE_Source source3d;

    void OnEnable()
    {
        if (GameManager.instance.hrtfMode)
        {
            source3d.mute = false;
        }
        else
        {
            source2d.mute = false;
        }
    }

}
