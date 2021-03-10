using System.Collections;
using System.Collections.Generic;
using eDmitriyAssets.NavmeshLinksGenerator;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( NavMeshLinks_AutoPlacer ) )]
[CanEditMultipleObjects]
public class NavMeshLinks_AutoPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
 
        if( GUILayout.Button( "Generate" ) )
        {
            foreach ( var targ in targets )
            {
                ( ( NavMeshLinks_AutoPlacer ) targ ).Generate();
            }
        }
 
        if ( GUILayout.Button ( "ClearLinks" ) )
        {
            foreach ( var targ in targets )
            {
                ( (NavMeshLinks_AutoPlacer)targ ).ClearLinks();
            }
        }
    }
}
