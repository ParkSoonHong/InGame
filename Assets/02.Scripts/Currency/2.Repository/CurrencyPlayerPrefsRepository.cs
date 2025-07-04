using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class CurrencyPlayerPrefsRepository 
{
    // Repository: 데이터의 영속성을 보장한다
    // 영속성 : 프로그램을 종료해도 데이터가 보존되는 것
    // Save/Load

    private const string SAVE_KEY = nameof(CurrencyPlayerPrefsRepository);

    // Save
    public void Save(List<CurrencyDTO> dataList , string id)
    {
        CurrencySaveDatas datas = new CurrencySaveDatas();
        datas.DataList = dataList.ConvertAll(data => new CurrencySaveData
        {
          Type =  data.Type,
          Value = data.Value
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY + "_" + id, json);
    }

    // Load
  //  public List<CurrencyDTO> Load(string id)
    public List<CurrencyDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            Debug.Log("LoadNull");
            return null;
        }

     //   string json = PlayerPrefs.GetString(SAVE_KEY + "_" + id);
        string json = PlayerPrefs.GetString(SAVE_KEY );
        CurrencySaveDatas datas = JsonUtility.FromJson<CurrencySaveDatas>(json);

        return datas.DataList.ConvertAll<CurrencyDTO>(data => new CurrencyDTO(data.Type,data.Value));
    }
}

[Serializable]
public struct CurrencySaveData
{
    public ECurrencyType Type;
    public int Value;
}

[Serializable]
public class CurrencySaveDatas
{
    public List<CurrencySaveData> DataList;
}