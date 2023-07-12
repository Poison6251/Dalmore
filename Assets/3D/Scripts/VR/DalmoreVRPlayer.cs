using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DalmoreVRPlayer : DalmorePlayer
{
    [SerializeField] private GameObject TrackingSpace;
    [SerializeField] private GameObject OVRInteractoin;

    public override void Enalbe()
    {
        base.Enalbe();
        isEnable = true;

        TrackingSpace.SetActive(true);
        OVRInteractoin.SetActive(true);
    }
    public override void Disalbe()
    {
        base.Disalbe();
        isEnable = false;

        //TrackingSpace.SetActive(false);
        //OVRInteractoin?.SetActive(false);
    }
}
