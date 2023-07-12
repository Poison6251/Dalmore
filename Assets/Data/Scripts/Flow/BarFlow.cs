using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFlow : Flow
{
    [SerializeField] private GameObject bar, make, store, title;

    public override void Enter()
    {
        bar.SetActive(true);
        make.SetActive(false);
        store.SetActive(false);
        title.SetActive(false);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public override void Exit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        bar.SetActive(false);
    }
}
