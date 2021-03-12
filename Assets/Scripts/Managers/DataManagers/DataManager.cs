using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager S;

    private void Awake()
    {
        if (S == null) S = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SaveToPrefs<T>(T data)
    {
        PlayerPrefs.SetString(typeof(T).Name, JsonUtility.ToJson(data));
    }
     
    public T LoadFromPrefs<T>() where T : DataClasses, new()
    {
        var loadedValue = PlayerPrefs.GetString(typeof(T).Name, null);
        return string.IsNullOrEmpty(loadedValue) ? new T() : JsonUtility.FromJson<T>(loadedValue);
    }
}
