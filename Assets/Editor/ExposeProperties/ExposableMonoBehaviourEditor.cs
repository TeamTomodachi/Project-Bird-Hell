using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(ExposableMonoBehaviour), true)]
public class ExposableMonoBehaviourEditor : Editor
{
    ExposableMonoBehaviour m_Instance;
    PropertyField[] m_fields;

    public virtual void OnEnable()
    {
        m_Instance = target as ExposableMonoBehaviour;
        m_fields = ExposeProperties.GetProperties(m_Instance);
    }

    public override void OnInspectorGUI()
    {
        if (m_Instance == null)
            return;
        this.DrawDefaultInspector();
        ExposeProperties.Expose(m_fields);
    }
}