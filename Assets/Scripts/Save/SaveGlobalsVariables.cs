using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

public class SaveGlobalsVariables : MonoBehaviour
{
    public static SaveGlobalsVariables Instance;
    private GlobalsVariables _variables;
    public static readonly string saveGlobalsVariablesKey = "InkGlobalsVariables";

    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }
        _variables = GetComponent<GlobalsVariables>();
    }

    private void Start()
    {
        LoadGlobalsData();
    }

    public void SaveGlobalsData()
    {
        Story globalsStory = _variables.globalsStory;
        GlobalsVariablesData save = new GlobalsVariablesData();
        foreach (string name in globalsStory.variablesState)
        {
            GlobalVariable variable = new GlobalVariable
            {
                name = name,
                value = globalsStory.variablesState.GetVariableWithName(name).ToString()
            };
            save.states.Add(variable);
        }

        string json = JsonUtility.ToJson(save);
        PlayerPrefs.SetString(saveGlobalsVariablesKey, json);
        PlayerPrefs.Save();
    }

    public void LoadGlobalsData()
    {
        if (PlayerPrefs.HasKey(saveGlobalsVariablesKey))
        {
            string json = PlayerPrefs.GetString(saveGlobalsVariablesKey);
            GlobalsVariablesData globalsVariablesData = JsonUtility.FromJson<GlobalsVariablesData>(json);

            foreach (GlobalVariable variable in globalsVariablesData.states)
            {
                _variables.SetVariable(variable.name, (object)variable.value);
            }
        }
    }

}

[System.Serializable]
public class GlobalsVariablesData
{
    public List<GlobalVariable> states = new List<GlobalVariable>();
}

[System.Serializable]
public class GlobalVariable
{
    public string name;
    public string value;
}

