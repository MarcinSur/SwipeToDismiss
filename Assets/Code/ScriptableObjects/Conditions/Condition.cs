using UnityEngine;

[System.Serializable]
public class Condition : ScriptableObject, ICondition
{
    public string Desription { get; set; }
    public bool Satisfied { get; set; }
    public int Hash { get; set; }
}
