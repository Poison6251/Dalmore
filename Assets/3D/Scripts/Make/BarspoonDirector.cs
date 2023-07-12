using UnityEngine;

public class BarspoonDirector : BartenderDirector
{

    private void Awake()
    {
        receiveData += ReceiveEvent;
    }
    private void ReceiveEvent(Vector3 data)
    {
        if (data.y == 0f) return;
        if(data.y == 1f)
        {
            print(data.x+"회 스터");
        }
        else if (data.y == 2f)
        {
            print("성공");
        }

    }
}
