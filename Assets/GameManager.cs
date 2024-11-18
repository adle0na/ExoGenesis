using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<EventScript> eventPointList;

    private PlayerScript _player;

    private void Start()
    {
        _player = PlayerScript.Instance;
    }

    public void MovePlayer(int eventIndex)
    {
        _player.GetComponent<Transform>().position = eventPointList[eventIndex].transform.position;
    }
}
