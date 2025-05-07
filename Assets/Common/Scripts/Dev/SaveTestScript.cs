using System.Collections.Generic;
using UnityEngine;

public class SaveTestScript : MonoBehaviour, ISave
{
    private void Start()
    {
        Load();
    }


    public void Save()
    {
        int value = Random.Range(0, 100);
        Dictionary<string, dynamic> dict = new()
        {
            { "name", gameObject.name },
            { "value", value}
        };
        string key = Utils.GetSceneId(gameObject);
        LevelManager.Instance.saveDict.Add(key, dict);
    }

    public void Load()
    {
        string key = Utils.GetSceneId(gameObject);
        if (LevelManager.Instance.saveDict.ContainsKey(key))
        {
            Dictionary<string, dynamic> dict = LevelManager.Instance.saveDict[key];
        }
    }
}
