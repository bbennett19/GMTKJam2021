using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _blindDoor;
    [SerializeField]
    private GameObject _visibileDoor;

    private bool _visibleDoorRevealed;

    private static LevelExitManager _instance;

    public static LevelExitManager Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public bool CanPlayerExit(bool isSeeingPlayer)
    {
        return (_visibleDoorRevealed && isSeeingPlayer) || !isSeeingPlayer;
    }

    public void ToggleActivePlayer(bool isSeeingPlayer)
    {
        Debug.Log("TOGGLE: " + isSeeingPlayer);
        _blindDoor.SetActive(!isSeeingPlayer);
        _visibileDoor.SetActive(isSeeingPlayer && _visibleDoorRevealed);
    }

    public void PlayerExited()
    {
        if (_visibleDoorRevealed)
        {
            // Both players exited reload the scene
            GameOverCanvas.Instance.TriggerLevelWin();
        }

        _visibleDoorRevealed = true;
        PlayerManager.Instance.SwapPlayers();
        PlayerManager.Instance.DisableSwap();
    }
}
