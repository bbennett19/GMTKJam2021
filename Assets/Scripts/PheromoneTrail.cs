using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneTrail : MonoBehaviour
{
    [SerializeField]
    private GameObject _pheromonePrefab;

    public void SetTrailActive(bool active)
    {
        foreach (Transform tr in transform)
        {
            tr.gameObject.SetActive(active);
        }
    }

    public void AddPheromone(Vector3 position)
    {
        Instantiate(_pheromonePrefab, position, Quaternion.identity, this.transform);
    }
}
