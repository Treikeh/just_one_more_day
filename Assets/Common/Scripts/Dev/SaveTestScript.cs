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
        LevelManager.Instance.saveDict.Add(gameObject.name, value);
        Debug.Log($"{gameObject.name} saved a value of {value}");
    }

    public void Load()
    {
        if (LevelManager.Instance.saveDict.ContainsKey(gameObject.name))
        {
            Debug.Log($"{gameObject.name} had a value of {LevelManager.Instance.saveDict[gameObject.name]}");
        }
    }
}
