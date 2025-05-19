using UnityEngine;


public class SaveActiveState : MonoBehaviour, ISave
{
    private void Start()
    {
        Load();
    }


    public void Save()
    {
        // Create save data
        LevelSaveData saveData = new()
        {
            active = false,
            name = Utils.GetSceneId(gameObject),
        };
        // Save data to level manager
        LevelManager.Instance.SetSaveData(Utils.GetSceneId(gameObject), saveData);

        // Hide object
        gameObject.SetActive(false);
    }

    public void Load()
    {
        LevelSaveData saveData = LevelManager.Instance.GetSaveData(Utils.GetSceneId(gameObject));
        if (saveData != null)
        {
            Debug.Log(saveData.name);
            gameObject.SetActive(saveData.active);
        }
    }
}
