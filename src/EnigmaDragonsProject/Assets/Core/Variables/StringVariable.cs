using UnityEngine;

public class StringVariable : ScriptableObject
{
    [SerializeField]
    private string value = "";

    public string Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void SetValue(string str) => value = str;
}
