using UnityEngine;

[CreateAssetMenu(fileName = "dialogue", menuName ="Dialogue/New")]
public class DialogueOption : ScriptableObject, ISerializationCallbackReceiver{

    [SerializeField]
    private string _title;
    [SerializeField]
    [Multiline]
    private string _description;
    [SerializeField]
    private Condition _condition;

    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
