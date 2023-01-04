using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleHandler : MonoBehaviour, IPointerClickHandler
{
    public enum TargetType
    {
        Path,
        FinishPoint
    }

    public TargetType type;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.PathSelect(this);
    }
}
