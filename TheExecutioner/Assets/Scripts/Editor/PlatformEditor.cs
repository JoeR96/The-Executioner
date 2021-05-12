using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
[CanEditMultipleObjects]
[CustomEditor(typeof(PlatformManager))]
public class PlatformManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;
        
        PlatformManager PlatformManager = (PlatformManager)target;
        #region Platform Height GUI Buttons

        EditorGUILayout.BeginHorizontal();
        GUI.skin.button.wordWrap = true;

        if (GUILayout.Button("1",GUILayout.Width(80), GUILayout.Height(80)))
        { 
            PlatformManager.PlatformHeightManager.SetPlatformHeight(0);
        }
        if (GUILayout.Button("2",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(1);
        }
        if (GUILayout.Button("3",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(2);
        }
        if (GUILayout.Button("4",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(3);
        }
        if (GUILayout.Button("5",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(4);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("6",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(5);
        }
        if (GUILayout.Button("7",GUILayout.Width(80), GUILayout.Height(80)))
        { 
            PlatformManager.PlatformHeightManager.SetPlatformHeight(6);
        }
        if (GUILayout.Button("8",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(7);
        }
        if (GUILayout.Button("9",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(8);
        }
  
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("10",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(9);
        }
        if (GUILayout.Button("11",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(10);
        }
        if (GUILayout.Button("+",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.RaiseOneLevel();
        }
        if (GUILayout.Button("-",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.DownOneLevel();
        }
        if (GUILayout.Button("Reset Platform",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(16);
        }
        EditorGUILayout.EndHorizontal();

        #endregion
        #region Platform Ramp GUI Buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Toggle Ramp", GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformRampManager.ActivateRamp(PlatformManager.PlatformRampManager.ReturnRampValue());
        }
        if (PlatformManager.PlatformRampManager.PlatformRampActive)
        {
            if (GUILayout.Button("1", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(0);
            }
            if (GUILayout.Button("2", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(1);
            }
            if (GUILayout.Button("3", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(2);
            }
            if (GUILayout.Button("4", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(3);
            }
        }
        EditorGUILayout.EndHorizontal();


        #endregion
        #region PlatformBridgeRamp
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Toggle Bridge Ramp", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeRampManager.ToggleBridge();
                PlatformManager.PlatformBridgeRampManager.ActivateBridge(
                    PlatformManager.PlatformBridgeRampManager.ReturnBridgeValue());
            }
           if (PlatformManager.PlatformBridgeRampManager.PlatformBridgeRampActive )
            { 
                if (GUILayout.Button("1",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetBridgeHeight(0);
                }
                if (GUILayout.Button("2",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetBridgeHeight(1);
                }
                if (GUILayout.Button("3",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetBridgeHeight(2);
                }
                if (GUILayout.Button("4",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetBridgeHeight(3);
                }
            
            EditorGUILayout.EndHorizontal ();
            EditorGUILayout.BeginHorizontal();
     
            if (GUILayout.Button("5",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeRampManager.SetBridgeHeight(4);
            }
            if (GUILayout.Button("6",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeRampManager.SetBridgeHeight(5);
            }
            if (GUILayout.Button("0",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeRampManager.SetBridgeHeight(6);
            }
            if (GUILayout.Button("+",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeRampManager.RaiseOneLevel();
            }
            if (GUILayout.Button("-",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeRampManager.DownOneLevel();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rotation", GUILayout.Width(80), GUILayout.Height(80)))
                {
                }
                if (GUILayout.Button("1",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetRampRotation(0);
                }
                if (GUILayout.Button("2",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetRampRotation(1);
                }
                if (GUILayout.Button("3",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetRampRotation(2);
                }
                if (GUILayout.Button("4",GUILayout.Width(80), GUILayout.Height(80)))
                {
                    PlatformManager.PlatformBridgeRampManager.SetRampRotation(3);
                }
            }
            EditorGUILayout.EndHorizontal ();
        }
        #endregion
        #region Platform Bridge GUI buttons
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Toggle Bridge", GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformBridgeManager.ToggleBridge();
            PlatformManager.PlatformBridgeManager.ActivateBridge(PlatformManager.PlatformBridgeManager.ReturnBridgeValue());
        }
       
        if (PlatformManager.PlatformBridgeManager.PlatformBridgeActive )
        {
            if (GUILayout.Button("1",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(0);
            }
            if (GUILayout.Button("2",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(1);
            }
            if (GUILayout.Button("3",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(2);
            }
            if (GUILayout.Button("4",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(3);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("5",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(4);
            }
            if (GUILayout.Button("6",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(5);
            }
            if (GUILayout.Button("7",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(6);
            }
            if (GUILayout.Button("+", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.RaiseOneLevel();
            }
            if (GUILayout.Button("-", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.DownOneLevel();
            }
            
        }
        EditorGUILayout.EndHorizontal ();

        #endregion
        #region Platform Spawn Point GUI Buttons

        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Toggle Spawn Point",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformSpawnManager.ActivateSpawnPoint();
        }
        if (GUILayout.Button("Toggle Spawn Point On",GUILayout.Width(80), GUILayout.Height(80))) { 
            PlatformManager.PlatformSpawnManager.DeactivateSpawnPoint();
        }
        if (GUILayout.Button("Toggle Event Spawn Point On",GUILayout.Width(80), GUILayout.Height(80))) { 
            PlatformManager.PlatformSpawnManager.ActivateEventSpawnPoint();
        }
        if (GUILayout.Button("Toggle Event Spawn Point Off",GUILayout.Width(80), GUILayout.Height(80))) { 
            PlatformManager.PlatformSpawnManager.DeactivateSpawnPoint();
        }
        if (GUILayout.Button("Reset All",GUILayout.Width(80), GUILayout.Height(80))) { 
            PlatformManager.ResetPlatformStates();
        }
        EditorGUILayout.EndHorizontal ();

        #endregion
    }
}
