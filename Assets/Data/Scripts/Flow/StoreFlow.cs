using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFlow : Flow
{
    [SerializeField] private GameObject bar, make, store, title;
    public override void Enter()
    {
        bar.SetActive(false);
        make.SetActive(false);
        store.SetActive(true);
        title.SetActive(false);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        FlowChanger.Instance.StoreToBar();
    }

    public override void Exit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        store.SetActive(false);
    }
}
