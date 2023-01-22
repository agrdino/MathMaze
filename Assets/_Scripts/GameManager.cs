using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region ----- VARIABLE ------

    public static GameManager instance;

    [SerializeField] private PlayerHandler _playerHandler;
    [SerializeField] private GameObject _ground;
    [SerializeField] private ObstacleHandler _base;
    private List<Target> _targets;

    private List<ObstacleHandler> _paths;

    private Player _player;
    #endregion

    #region ----- UNITY EVENT -----

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _paths ??= new List<ObstacleHandler>();
        _targets ??= new List<Target>();
        GenerateLevel(new Map()
        {
            notes = DataManager.instance.map
        });
    }

    #endregion

    #region ----- PUBLIC FUNCTION -----

    public void PathSelect(ObstacleHandler path)
    {
        if (path.id == 0)
        {
            return;
        }

        if (_paths.Contains(path))
        {
            var index = _paths.IndexOf(path);
            while (_paths.Count > index)
            {
                var oldPath = _paths[index];
                oldPath.Select(false);
                _paths.RemoveAt(index);
            }

            UpdatePath();
            return;
        }

        var existPath = _targets[_paths[^1].id].path.Keys.Contains(path.id);
        if (!existPath)
        {
            return;
        }

        path.GetComponent<Renderer>().material.color = Color.blue;
        _paths.Add(path);
        UpdatePath();

        // if (path.type == TargetType.FinishPoint)
        // {
        //     StartCoroutine(StartMove());
        // }
    }

    #endregion

    #region ----- PRIVATE FUNCTION -----

    private void UpdatePath()
    {

    }

    public IEnumerator StartMove()
    {
        if (_paths.Count <= 1)
        {
            yield break;
        }

        var delayMove = new WaitForSeconds(0.5f);
        var delayRotate = new WaitForSeconds(0.25f);

        for (var i = 0; i < _paths.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }

            var path = _targets[_paths[i - 1].id].path[_paths[i].id];
            for (var j = 0; j < path.Count; j++)
            {
                if (j == 0)
                {
                    _playerHandler.transform.DOLookAt(path[j].position + Vector3.up, 0.25f).SetEase(Ease.Linear);
                    yield return delayRotate;
                }

                _playerHandler.transform.DOMove(path[j].position + Vector3.up, 0.5f).SetEase(Ease.Linear);
                yield return delayMove;
            }
        }
    }

    private void GenerateLevel(Map map)
    {
        //Create note
        for (int i = 0; i < map.notes.Count; i++)
        {
            var basePosition = Instantiate(_base, map.notes[i].position - Vector3.up, Quaternion.identity)
                .Initialize(i, map.notes[i].type);
            basePosition.gameObject.SetActive(true);
            _targets.Add(new Target()
            {
                target = basePosition,
                path = new Dictionary<int, List<Transform>>()
            });

            if (i == 0)
            {
                _paths.Add(basePosition);
            }
        }

        //Create ground
        for (var i = 0; i < map.notes.Count; i++)
        {
            var baseNote = map.notes[i];
            var relateNote = map.notes[i].relate;
            for (var j = 0; j < relateNote.Count; j++)
            {
                //Calculate amount and scale of path
                var note = relateNote[j];
                var direction = (map.notes[note].position - baseNote.position).normalized;
                var distance = Vector3.Distance(baseNote.position, map.notes[note].position);
                var count = (int) distance;
                var scale = distance / (int) distance;
                Vector3 startPosition = baseNote.position - Vector3.up;
                var path = new List<Transform>();

                for (var k = 0; k < count; k++)
                {
                    var ground = Instantiate(_ground, startPosition + direction * scale, Quaternion.identity);
                    ground.SetActive(true);
                    ground.transform.localScale = scale * Vector3.one;
                    ground.transform.forward = direction;
                    startPosition = ground.transform.position;
                    path.Add(ground.transform);
                }

                _targets[i].path.Add(relateNote[j], path);
            }
        }
    }

    private void Complete()
    {

    }

    #endregion
}
