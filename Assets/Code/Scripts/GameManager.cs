using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SceneCoordinator _sceneCoordinator = new SceneCoordinator();
    SaveManager _saveManager;

    private void Start()
    {
        //TODO 
        //Load Game Data
        //if(_saveManager.haveSavedData)
        //    LoadManager.Laod()
        // else 
        //    CreatePlayerData()
        //ShowGameMenu()
    }

    private void SceneUnloaded()
    {
        throw new NotImplementedException();
    }

    private void SceneLoaded()
    {
        throw new NotImplementedException();
    }
}