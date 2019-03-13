using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionsCollection", menuName = "Create ConditionsCollection")]
public class ConditionCollection : ScriptableObject
{

    [SerializeField]
    public List<ICondition> conditions = new List<ICondition>();

    void AddTolist(ICondition condition)
    {
        condition.Satisfied = true;
        conditions.Add(condition);
    }
}
public class Factory
{
    public static Condition CreateCondition()
    {
        return new Condition();
    }
}