using UnityEngine;


public class RedTriangle : MonoBehaviour, ISave
{
    private void Start()
    {
        Load();
    }


    public void Save()
    {
        // Create save data
        SaveData saveData = new()
        {
            active = false,
            name = Utils.GetSceneId(gameObject)
        };
        // Save data to level manager
        LevelManager.Instance.SetSaveData(Utils.GetSceneId(gameObject), saveData);

        // Hide object
        gameObject.SetActive(false);
    }

    public void Load()
    {
        object obj = LevelManager.Instance.GetSaveData(Utils.GetSceneId(gameObject));
        if (obj is not null and SaveData)
        {
            SaveData saveData = (SaveData)obj;
            Debug.Log(saveData.name);
            gameObject.SetActive(saveData.active);
        }
    }

    private class SaveData
    {
        public bool active;
        public string name;
    }
}
