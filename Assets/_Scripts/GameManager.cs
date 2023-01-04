using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerHandler _player;
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _base;
    private List<Target> _targets;

    private List<ObstacleHandler> _paths;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _paths ??= new List<ObstacleHandler>();
        _targets ??= new List<Target>();
        SetUpGround();
    }

    private void SetUpGround()
    {
        var _maps = DataManager.instance.map;
        //Setup base
        for (int i = 0; i < _maps.Count; i++)
        {
            var basePosition = Instantiate(_base, _maps[i].position, Quaternion.identity);
            basePosition.SetActive(true);
            _targets.Add(new Target()
            {
                target = basePosition,
                path = new Dictionary<int, List<Transform>>()
            });
        }
        for (int i = 0; i < _maps.Count; i++)
        {
            for (var i1 = 0; i1 < _maps[i].relate.Count; i1++)
            {
                var relate = _maps[i].relate[i1];
                var direction = (_maps[relate].position - _maps[i].position).normalized;
                var distance = Vector3.Distance(_maps[i].position, _maps[relate].position);
                var count = (int) distance;
                var scale = distance / (int) distance;
                Vector3 startPosition = _maps[i].position;
                var path = new List<Transform>();
                for (int j = 0; j < count; j++)
                {
                    var ground = Instantiate(_ground, startPosition + direction * scale, Quaternion.identity);
                    ground.SetActive(true);
                    ground.transform.localScale = scale * Vector3.one;
                    ground.transform.forward = direction;
                    startPosition = ground.transform.position;
                    path.Add(ground.transform);
                }
                _targets[i].path.Add(i1, path);
            }
        }
    }

    public void PathSelect(ObstacleHandler path)
    {
        if (_paths.Contains(path))
        {
            _paths.Remove(path);
            UpdatePath();
            return;
        }
        
        _paths.Add(path);
        UpdatePath();

        if (path.type == ObstacleHandler.TargetType.FinishPoint)
        {
            StartMove();
        }
    }

    private void UpdatePath()
    {
        
    }

    private void StartMove()
    {
        if (_paths.Count == 0)
        {
            return;
        }
        var target = _paths[0];
        _paths.Remove(target);
        _player.transform.DOMove(target.transform.position, 2).SetEase(Ease.Linear)
            .OnStart(() =>
            {
                _player.transform.DOLookAt(target.transform.position, 0.5f).SetEase(Ease.Linear);
            })
            .OnComplete(StartMove);
    }
}
