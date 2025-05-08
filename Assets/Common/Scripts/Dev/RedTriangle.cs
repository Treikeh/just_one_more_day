using System.Collections.Generic;
using UnityEngine;


public class RedTriangle : MonoBehaviour, ISave
{
    private void Start()
    {
        Load();
    }


    public void Save()
    {
        LevelManager.Instance.SetSaveData(Utils.GetSceneId(gameObject), false);
        gameObject.SetActive(false);
    }

    public void Load()
    {
        bool state = LevelManager.Instance.GetSaveData(Utils.GetSceneId(gameObject));
        gameObject.SetActive(state);
    }
}
