using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    [SerializeField]
    private PlayableDirector _introScene;
    private bool _introFinished;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !_introFinished)
        {
            if(_introScene.time < 56f)
                _introScene.time = 56f;
            _introFinished = true;
        }
    }
}
