using Scripts.Enemy.Behaviour;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BossAttackData))]
public class BossAttackDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string elementIndex = label.text.Split(' ')[1];
        label.text = $"Attack {elementIndex}";
        EditorGUI.PropertyField(position, property, label, true);
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 
        EditorGUI.GetPropertyHeight(property, label, true);
}


[CustomPropertyDrawer(typeof(BossPhase))]
public class BossPhaseDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string elementIndex = label.text.Split(' ')[1];
        label.text = $"Phase {elementIndex}";
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
        EditorGUI.GetPropertyHeight(property, label, true);
}
[CustomPropertyDrawer(typeof(BossPhaseStep))]
public class BossPhaseStepDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string elementIndex = label.text.Split(' ')[1];
        label.text = $"Phase Step {elementIndex}";
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
        EditorGUI.GetPropertyHeight(property, label, true);
}
