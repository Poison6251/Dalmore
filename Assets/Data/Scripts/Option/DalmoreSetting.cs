using M_DB;
using UnityEngine;

[CreateAssetMenu]
public class DalmoreSetting : ScriptableObject
{ 

    // 환경설정 파일 경로
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

    // 전역 변수 설정
    [Header("성분표")]
    [SerializeField] private RecipeData makingLog;
    public static RecipeData MakingLog => Setting.makingLog;
    [Header("주문서")]
    [SerializeField] private OrderList orderSheet;
    public static OrderList OrderSheet => Setting.orderSheet;
    [Header("주문창 목록")]
    [SerializeField] private OrderList orderWindow;
    public static OrderList OrderWindowList => Setting.orderWindow;
    [Header("메뉴판")]
    [SerializeField] private Menu menu;
    public static Menu DalmoreMenu => Setting.menu;
    [Header("아이템 DB")]
    [SerializeField] private ItemDB itemDB;
    public static ItemDB DB_Item => Setting.itemDB;
    [Header("레시피 DB")]
    [SerializeField] private RecipeDB recipeDB;
    public static RecipeDB DB_Recipe => Setting.recipeDB;
}
