using Ink.Runtime;
using System.Linq;
using UnityEngine;

public class GlobalsVariables : MonoBehaviour
{
    public static GlobalsVariables Instance;

    [SerializeField] private TextAsset globalsInk;

    private Story globalsStory;

    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }
        globalsStory = new Story(globalsInk.text);
    }

    public void SetVariable(string name, object value)
    {

        if (globalsStory.variablesState.Contains(name))
        {
            globalsStory.variablesState[name] = value;
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

}
