using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor
{
    
    public override void OnInspectorGUI() //2
    {
        base.DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        // Call base class method
        
        // Custom form for Player Preferences
        ObjectPooler  _objectPooler= (ObjectPooler)target;
        
        /// <summary>
        /// Create a button to click to increase the current size of the pool in the inspector
        /// </summary>
        if (GUILayout.Button("Increase Pool Size")) //8
        {
            _objectPooler.IncreasePoolSize();
        }

        GUILayout.EndHorizontal();

    }
}
