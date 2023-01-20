using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Renderer _renderer;
    public TargetType type
    {
        get;
        private set;
    }
    
    public int id
    {
        get;
        private set;
    }
    public ObstacleHandler Initialize(int id, TargetType type)
    {
        _renderer.material.color = Color.black;
        this.id = id;
        this.type = type;
        return this;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.PathSelect(this);
    }

    public void Select(bool isSelect)
    {
        _renderer.material.color = isSelect ? Color.yellow : Color.black;
    }
}
