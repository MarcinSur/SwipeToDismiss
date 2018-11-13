using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionsCollection", menuName = "Create ConditionsCollection")]
public class ConditionCollection : ScriptableObject{

    public List<Condition> conditions;
    
}
