using M_DB;
using UnityEngine;

[CreateAssetMenu]
public class DalmoreSetting : ScriptableObject
{ 

    // ȯ�漳�� ���� ���
    private static DalmoreSetting setting;
    private static DalmoreSetting Setting
    {
        get
        {
            if(setting == null)
            {
                setting = Resources.Load("Dalmore Settings") as DalmoreSetting;
            }
            return setting;
        }
    }

    // ���� ���� ����
    [Header("����ǥ")]
    [SerializeField] private RecipeData makingLog;
    public static RecipeData MakingLog => Setting.makingLog;
    [Header("�ֹ���")]
    [SerializeField] private OrderList orderSheet;
    public static OrderList OrderSheet => Setting.orderSheet;
    [Header("�ֹ�â ���")]
    [SerializeField] private OrderList orderWindow;
    public static OrderList OrderWindowList => Setting.orderWindow;
    [Header("�޴���")]
    [SerializeField] private Menu menu;
    public static Menu DalmoreMenu => Setting.menu;
    [Header("������ DB")]
    [SerializeField] private ItemDB itemDB;
    public static ItemDB DB_Item => Setting.itemDB;
    [Header("������ DB")]
    [SerializeField] private RecipeDB recipeDB;
    public static RecipeDB DB_Recipe => Setting.recipeDB;
}
