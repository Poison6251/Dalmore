using M_DB;
using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance;
    [SerializeField] private ItemDB itemDB; public ItemDB ItemDB => itemDB;


    [SerializeField]
    public DataSet data;

    private void Awake()
    {
        Instance = this;
    }

}

[Serializable]
public class DataSet
{

}
