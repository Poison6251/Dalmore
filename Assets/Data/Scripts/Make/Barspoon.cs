using Item;
using System.Collections;
using UnityEngine;

/*
    [Flow] ( + : �̺�Ʈ ȣ�� �ʿ�; # : ����; )
    + Grab(true)    : ��� ���
    + StartEvent()  : ��� ����
  # + Grab(false)   : ��� �Ͻ�����
  # + Grab(true)    : ��� �ٽý���
    + EndEvent()    : ��� �ߴ�
    + Grab(false)   : ��� ��� X

    [ä��]
    data ���� (x : angle; y : 0 ��� �ߴ�, 1 ��� ��, 2 ����;)
 */

public class Barspoon : BartenderData
{
    private float angle = 0f;                          
    private bool isGrabed;
    private Collider selectCol;
    private WaitForSeconds delay = new WaitForSeconds(0.3f);    // ��� ������
    private InputEventData inputEventData;
    [Header("���̴� ��"),SerializeField] private float pow = 1f;
    [Header("ȸ�� Ƚ��"),SerializeField] private int count;


    // isGarbed�� true���� ������
    public void Grab(bool _is)
    {
        print("grab  "+_is);
        isGrabed  = _is;
    }

    // ��� ����
    public void StartEvent()
    {
        EndEvent();
        StartCoroutine(Stir());
    }
    // ��� �ߴ�
    public void EndEvent()
    {
        StopAllCoroutines();
        // ��Ʈ�ѷ� ��ġ ���� OFF
        DalmoreInputEventManager.Instance.RemoveInputEvent(inputEventData);
    }
    
    // ���
    private IEnumerator Stir()
    {
        // ������ �ʱ�ȭ
        Vector3 before= Vector3.zero;
        Vector3 after = Vector3.zero;
        angle = 0;
        data.y = 1f;

        // ��Ʈ�ѷ� ��ġ ���� ON
        inputEventData = new InputEventData();
        inputEventData.rightHandPosition += (x) => {after = x; };
        DalmoreInputEventManager.Instance.AddInputEvent(inputEventData);

        while (true)
        {
            if (angle > 2 * Mathf.PI * count) break;
            if (isGrabed)
            {
                // �Ŀ� ���
                var distance = Vector3.Distance(before, after);
                angle += pow * distance;
                before = after;

                // ������ ���� (x ä�� ���)
                data.x = angle;
                SendData();
            }

            yield return delay;
        }

        // ����
        Success();
        data.y = 2f;
        SendData();

        // ������ ���� ���Ḧ �˷� �� (y ä�� ���)
        data.y = 0f;
        SendData();

        // ��Ʈ�ѷ� ��ġ ���� OFF
        DalmoreInputEventManager.Instance.RemoveInputEvent(inputEventData);
    }

    #region 3D
    private void OnTriggerEnter(Collider other)
    {
        // ��ȣ�ۿ� ������ Ÿ������ �˻�
        var interaction = other.gameObject.GetComponent<IItemDataInteractable>();
        if (interaction != null)
        {
            print("start");
            // ��ȣ�ۿ�
            StartEvent();
            // Ÿ�� Ȯ��
            selectCol = other;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Ÿ�ٿ��� ����� ����
        if (selectCol == other)
        {
            print("end");
            EndEvent();
            selectCol = null;
        }

    }
    #endregion
}