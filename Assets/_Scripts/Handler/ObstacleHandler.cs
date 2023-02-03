using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleHandler : MonoBehaviour, IPointerClickHandler
{

    #region ----- VARIALBE -----

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

    public int heal
    {
        get;
        private set;
    }

    #endregion

    #region ----- PUBLIC FUNCTION -----

    public ObstacleHandler Initialize(int id, TargetType type, int heal)
    {
        _renderer.material.color = type switch
        {
            TargetType.StartPoint => Color.yellow,
            TargetType.Path => Color.black,
            TargetType.FinishPoint => Color.green
        };
        this.id = id;
        this.type = type;
        this.heal = heal;
        return this;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (heal == 0)
        {
            return;
        }
        GameManager.instance.PathSelect(this);
    }
    
    public void Select(bool isSelect = true)
    {
        _renderer.material.color = type switch
        {
            TargetType.StartPoint => Color.yellow,
            TargetType.Path => isSelect ? Color.blue : Color.black,
            TargetType.FinishPoint => isSelect ? Color.blue : Color.green
        };
    }
    #endregion
}
