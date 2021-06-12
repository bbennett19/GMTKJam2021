using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneEmitter : MonoBehaviour
{
    [SerializeField]
    PheromoneTrail _pheromoneTrail;
    [SerializeField]
    private float _distanceBetweenPhermonones;

    private Vector3 _lastPheromonePosition;

    // Start is called before the first frame update
    void Start()
    {
        DropPheromone();
    }

    // Update is called once per frame
    void Update()
    {
        if ((_lastPheromonePosition - transform.position).magnitude >= _distanceBetweenPhermonones)
        {
            DropPheromone();
        }
    }

    private void DropPheromone()
    {
        _pheromoneTrail.AddPheromone(transform.position);
        _lastPheromonePosition = transform.position;
    }
}
