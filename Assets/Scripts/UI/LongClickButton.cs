using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Mobile version info")]
    [SerializeField] private FloatVariable RequiredHoldTime;
    [Space]
    [SerializeField] private UnityEvent OnFastClick;
    [SerializeField] private UnityEvent OnLongClickDown;
    [SerializeField] private UnityEvent OnLongClickUp;

    private bool IsPerforming;
    private bool IsPointerDown;
    private float PointerDownTimer;

    private void Awake()
    {
        IsPerforming = false;
        IsPointerDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PointerDownTimer < RequiredHoldTime.Value)
        {
            if (OnFastClick != null) OnFastClick.Invoke();
        }
        else OnLongClickUp.Invoke();

        Reset();
    }

    private void Update()
    {
        CheckTimer();
    }

    private void CheckTimer()
    {
        if (IsPointerDown)
        {
            PointerDownTimer += Time.deltaTime;

            if (PointerDownTimer >= RequiredHoldTime.Value && !IsPerforming)
            {
                if (OnLongClickDown != null) OnLongClickDown.Invoke();
                IsPerforming = true;
            }
        }
    }

    private void Reset()
    {
        IsPerforming = false;
        IsPointerDown = false;
        PointerDownTimer = 0;
    }
}