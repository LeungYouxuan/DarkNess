using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void SaveableRegister()
    {
        LoaderManager.Instance.Register(this);
    }
    GameSaveData GenerateSaveData();

    void RestoreGameSaveData(GameSaveData data);
    
}
