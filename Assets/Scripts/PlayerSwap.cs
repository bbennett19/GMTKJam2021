using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    [SerializeField]
    private PheromoneTrail _pheromoneTrail;

    private PlayerController _seeingPlayer;
    private PlayerController _blindPlayer;

    private bool _seeingActive = true;
    // Start is called before the first frame update
    void Start()
    {
        _seeingPlayer = GameObject.Find("SeeingPlayer").GetComponent<PlayerController>();
        _blindPlayer = GameObject.Find("BlindPlayer").GetComponent<PlayerController>();

        if (_seeingPlayer == null) 
        {
            Debug.LogError("SeeingPlayer null");
        }

        if (_blindPlayer == null)
        {
            Debug.LogError("BlindPlayer null");
        }
    }

    public void SwapPlayers()
    {
        _seeingActive = !_seeingActive;
        _seeingPlayer.SetPlayerActive(_seeingActive);
        _blindPlayer.SetPlayerActive(!_seeingActive);
        _pheromoneTrail.SetTrailActive(!_seeingActive);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapPlayers();
        }
    }
}
