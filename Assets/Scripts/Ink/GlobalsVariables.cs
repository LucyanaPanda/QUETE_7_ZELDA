using Ink.Runtime;
using System.Linq;
using UnityEngine;

public class GlobalsVariables : MonoBehaviour
{
    public Story globalsStory;
    [SerializeField] private TextAsset globalsInk;

    private void Awake()
    {
        globalsStory = new Story(globalsInk.text);
    }

    public void StartListeningStory(Story story)
    {
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListeningStroy( Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    public void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("Variable Changed: "+ name +" = " + value);
    }

    public void SetVariable(string name, object value)
    {
        if (globalsStory.variablesState.Contains(name))
        {
            globalsStory.variablesState[name] = value;
            Debug.Log("name: " + name + " value: " + globalsStory.variablesState[name]);
            SaveGlobalsVariables.Instance.SaveGlobalsData();
        }
        else
        {
            Debug.LogError("Couldn't find the variable");
        }
    }

    public object GetVariable(string name)
    {
        if (globalsStory.variablesState.Contains(name)) { return globalsStory.variablesState[name]; }
        Debug.LogError("Couldn't find the variable");
        return null;
    }

    public void BindToStory(Story story)
    {
        CopyFrom(story);
        story.variablesState.variableChangedEvent += (string varName, Ink.Runtime.Object newValue) =>
        {
            globalsStory.variablesState[varName] = newValue;
        };
    }

    private void CopyFrom(Story story)
    {
        foreach (string name in globalsStory.variablesState)
        {
            object value = globalsStory.variablesState[name];
            story.variablesState[name] = value;
        }
    }

    public ref Story GetStory()
    {
        return ref globalsStory;
    }
}
