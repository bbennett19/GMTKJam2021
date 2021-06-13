using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PheromoneTrail _pheromoneTrail;

    private PlayerController _seeingPlayer;
    private PlayerController _blindPlayer;

    private bool _seeingActive = true;
    private bool _swapDisabled = false;

    private static PlayerManager _instance;

    public static PlayerManager Instance {  get { return _instance; } }


    private void Awake()
    {
        _instance = this;
    }

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

        _seeingPlayer.SetPlayerActive(true);
        _blindPlayer.SetPlayerActive(false);
        LevelExitManager.Instance.ToggleActivePlayer(true);
    }

    public GameObject GetCurrentPlayer()
    {
        return (_seeingActive ? _seeingPlayer : _blindPlayer).gameObject;
    }

    public GameObject GetClosestPlayer(Vector3 position)
    {
        float distSeeing = (position - _seeingPlayer.gameObject.transform.position).sqrMagnitude;

        if ((position - _blindPlayer.gameObject.transform.position).sqrMagnitude < distSeeing && !_blindPlayer._hiding)
        {
            return _blindPlayer.gameObject;
        }

        return _seeingPlayer._hiding ? null : _seeingPlayer.gameObject;
    }

    public void SwapPlayers()
    {
        if (!_swapDisabled)
        {
            _seeingActive = !_seeingActive;
            _seeingPlayer.SetPlayerActive(_seeingActive);
            _blindPlayer.SetPlayerActive(!_seeingActive);
            _pheromoneTrail.SetTrailActive(!_seeingActive);
            LevelExitManager.Instance.ToggleActivePlayer(_seeingActive);
        }
    }

    public void DisableSwap(bool dead = false)
    {
        _swapDisabled = true;
        if (!dead)
        {
            // disabling the swap means the blind player made it out
            _blindPlayer.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapPlayers();
        }
    }
}
