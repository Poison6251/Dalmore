using System.Collections;
using UnityEngine;

public class CapacityChecker : MonoBehaviour
{
    public float GetCapacity
    {
        get
        {
            float result = (transform.localPosition.y - Height.floor) / (Height.top-Height.floor) * capacity;
            return result <0 ? 0 : result;
        }
    }
    private Rigidbody rigid;
    private WaitForSeconds delay = new WaitForSeconds(0.5f);
    [SerializeField] private Height Height;
    [SerializeField] private float capacity;
    [SerializeField] float pow;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(limitHeight());
    }
    public void Reset()
    {
        SetPosition(Height.floor);
    }
    private IEnumerator limitHeight()
    {
        while (true)
        {
            if(transform.localPosition.y > Height.top*1.2f || transform.localPosition.y <0)
            {
                SetPosition(Height.top);
            }
            yield return delay;
        }
    }
    private void SetPosition(float height)
    {
        var pos = transform.localPosition;
        pos.y = height;
        transform.localPosition = pos;
        rigid.velocity = Vector3.zero;
    }
}


[System.Serializable]
public struct Height 
{
    public float floor;
    public float top;
}

