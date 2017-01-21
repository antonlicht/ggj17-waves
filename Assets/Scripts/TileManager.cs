using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
  public GameObject[] TilePrefabs;
  public Transform TargetObject;
  public float worldToGridScale = 2006f;
  public int GridSize = 3;

  private int _seed;

  private Grid<GameObject> _grid;
  private int _currentX;
  private int _currentY;
  private Dictionary<int, List<GameObject>> _pool = new Dictionary<int, List<GameObject>>();
  private Dictionary<GameObject, int> _activeObjects = new Dictionary<GameObject, int>();

  void Awake()
  {
    _seed = Random.Range(1, int.MaxValue);
    _grid = new Grid<GameObject>(GridSize, GridSize);
    SetGridPosition(0, 0, true);
  }

  void Update()
  {
    Vector3 targetPos = TargetObject.position;
    SetGridPosition(Mathf.FloorToInt((-targetPos.x / worldToGridScale) + GridSize / 2f),
      Mathf.FloorToInt((-targetPos.z / worldToGridScale) + GridSize / 2f));
  }

  private GameObject GetInstanceForGridPosition(int x, int y)
  {
    var computed = Math.Abs(x * _seed + y * _seed * _seed);
    int index = computed % TilePrefabs.Length;

    GameObject instance = null;
    if (_pool.ContainsKey(index) && _pool[index].Any())
    {
      instance = _pool[index][0];
      _pool[index].RemoveAt(0);
    }
    else
    {
      instance = Instantiate(TilePrefabs[index]);
      instance.transform.SetParent(transform);
    }

    instance.SetActive(true);
    instance.transform.position = new Vector3(x * worldToGridScale, 0f, y * worldToGridScale);
    _activeObjects.Add(instance, index);
    return instance;
  }

  private void Release(GameObject instance)
  {
    if (instance == null)
    {
      return;
    }

    instance.SetActive(false);

    int index = _activeObjects[instance];
    _activeObjects.Remove(instance);

    if (!_pool.ContainsKey(index))
    {
      _pool.Add(index, new List<GameObject>());
    }
    _pool[index].Add(instance);
  }

  private void SetGridPosition(int x, int y, bool force = false)
  {
    if (!force)
    {
      if (x == _currentX && y == _currentY)
      {
        return;
      }
    }

    int deltaX = _currentX - x;
    int deltaY = _currentY - y;
    _currentX = x;
    _currentY = y;

    Grid<GameObject> contentGrid = Grid<GameObject>.CreateEmpty(_grid);
    contentGrid.SetIDOffset(_grid.IDOffsetX, _grid.IDOffsetY);

    for (int i = contentGrid.IDOffsetY; i < contentGrid.IDOffsetY + contentGrid.Height; i++)
    {
      for (int j = contentGrid.IDOffsetX; j < contentGrid.IDOffsetX + contentGrid.Width; j++)
      {
        contentGrid[j, i] = _grid[j, i];
      }
    }

    _grid.Shift(-deltaX, -deltaY);

    for (int i = contentGrid.IDOffsetY; i < contentGrid.IDOffsetY + contentGrid.Height; i++)
    {
      for (int j = contentGrid.IDOffsetX; j < contentGrid.IDOffsetX + contentGrid.Width; j++)
      {
        _grid[j, i] = contentGrid[j, i];
        if (_grid[j, i] == null && contentGrid[j, i] != null)
        {
          Release(contentGrid[j, i]);
        }
      }
    }

    for (int i = _grid.IDOffsetY; i < _grid.IDOffsetY + _grid.Height; i++)
    {
      for (int j = _grid.IDOffsetX; j < _grid.IDOffsetX + _grid.Width; j++)
      {
        if (_grid[j, i] == null)
        {
          _grid[j, i] = GetInstanceForGridPosition(j, i);
        }
        _grid[j, i].name = "cell_" + j + "_" + i;
      }
    }
  }
}