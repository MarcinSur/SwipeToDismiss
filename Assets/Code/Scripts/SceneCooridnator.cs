using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneCoordinator
{
    public UnityEvent OnLoadCompleted = new UnityEvent();
    public UnityEvent OnUnLoadCompleted = new UnityEvent();

    private string _currenScene;
    private List<AsyncOperation> _sceneLoadOperations = new List<AsyncOperation>();
    private AsyncOperation _loadAsyncOperations;
    private AsyncOperation _unLoadAsyncOperations;

    public  void LoadScene(string scene)
    {
        _loadAsyncOperations = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        if (_loadAsyncOperations == null)
        {
            //TO DO Error handling
            Debug.LogError("[GameManager] error in " + scene);
            throw new NotImplementedException();
        }
        _loadAsyncOperations.completed += OnLoadSceneCompleted;
        _sceneLoadOperations.Add(_loadAsyncOperations);
        _currenScene = scene;
    }

    public void UnloadScene(string scene)
    {
        _unLoadAsyncOperations = SceneManager.UnloadSceneAsync(scene);
        if (_unLoadAsyncOperations == null)
        {
            //TO DO Error handling
            Debug.LogError("[GameManager] error in " + scene);
            throw new NotImplementedException();
        }
        _loadAsyncOperations.completed += OnUnloadSceneCompleted;
    }

    public  void OnLoadSceneCompleted(AsyncOperation obj)
    {
        if (_sceneLoadOperations.Contains(obj))
        {
            _sceneLoadOperations.Remove(obj);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currenScene));

            //Dispatch event
            OnLoadCompleted.Invoke();
            //Transitions between scenes
        }
    }

    private void OnUnloadSceneCompleted(AsyncOperation obj)
    {
        OnUnLoadCompleted.Invoke();
    }
}
