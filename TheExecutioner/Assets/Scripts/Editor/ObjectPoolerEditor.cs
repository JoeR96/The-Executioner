

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
        

        if (GUILayout.Button("Increase Pool Size")) //8
        {
            _objectPooler.IncreasePoolSize();
        }

        if (GUILayout.Button("Return")) //10
        {
            
            //ObjectPooler.Instance.ReturnObject();
        }

        GUILayout.EndHorizontal();
        
        // Custom Button with Image as Thumbnail
    }
}
