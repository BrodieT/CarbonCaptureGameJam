using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "Create Save File", order = 0)]
public class SaveData : ScriptableObject
{
    [System.Serializable]
    public struct SaveDataStructure
    {
        public string playerName;
        public int playerScore;
    }

    public List<SaveDataStructure> saveData = new List<SaveDataStructure>();

    public bool CheckSaveData(string username)
    {
        foreach(SaveDataStructure s in saveData)
        {
            if(s.playerName == username)
            {
                return true;
            }
        }

        return false;
    }

    public List<SaveDataStructure> GetAllSaveData()
    {
        return saveData;
    }

    public void SortList()
    {
        for (int i = 1; i <= saveData.Count; i++)
        {
            for (int j = 0; j < saveData.Count - i; j++)
            {
                if (CompareTwoEntries(saveData[j], saveData[j + 1]))
                {
                    SaveData.SaveDataStructure temp = saveData[j];
                    SaveData.SaveDataStructure temp2 = saveData[j + 1];
                    saveData[j] = temp2;
                    saveData[j + 1] = temp;
                }
            }
        }

        saveData.Reverse();
    }

    bool CompareTwoEntries(SaveData.SaveDataStructure first, SaveData.SaveDataStructure second)
    {
        if (first.playerScore > second.playerScore)
        {
            return true;
        }

        return false;
    }
}
