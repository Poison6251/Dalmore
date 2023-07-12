using TMPro;
using UnityEngine;

public class BottleDirector : BartenderDirector
{
    [SerializeField]  private TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        receiveData += ViewData;
    }
    private void ViewData(Vector3 data)
    {
        textMeshProUGUI.text = data.x + " ml";
    }
}
