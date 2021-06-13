using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneTrail : MonoBehaviour
{
    [SerializeField]
    private GameObject _pheromonePrefab;
    [SerializeField]
    private GameObject _hideyPheromone;

    public void SetTrailActive(bool active)
    {
        foreach (Transform tr in transform)
        {
            tr.gameObject.SetActive(active);
        }
    }

    public void AddPheromone(Vector3 position, bool isForHidey = false)
    {
        if (!isForHidey)
        {
            Instantiate(_pheromonePrefab, position, Quaternion.identity, this.transform);
        }
        else
        {
            Instantiate(_hideyPheromone, position, Quaternion.identity, this.transform);
        }
    }
}
