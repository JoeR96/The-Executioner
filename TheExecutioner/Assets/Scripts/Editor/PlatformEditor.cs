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
        PlatformManager PlatformManager = (PlatformManager)target;
        #region Platform Height GUI Buttons

                EditorGUILayout.BeginHorizontal();
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
        if (GUILayout.Button("Level 10",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(9);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("11",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(10);
        }
        if (GUILayout.Button("12",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(11);
        }
        if (GUILayout.Button("13",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(12);
        }
        if (GUILayout.Button("14",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformHeightManager.SetPlatformHeight(13);
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
            if (GUILayout.Button("One", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(0);
            }
            if (GUILayout.Button("Two", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(1);
            }
            if (GUILayout.Button("Three", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(2);
            }
            if (GUILayout.Button("Four", GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(3);
            }
        }
        EditorGUILayout.EndHorizontal();
        

        #endregion
        #region Platform Bridge GUI buttons
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Toggle Bridge", GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformBridgeManager.ToggleBridge();
            PlatformManager.PlatformBridgeManager.ActivateBridge(PlatformManager.PlatformBridgeManager.ReturnBridgeValue());
        }
        if (PlatformManager.PlatformBridgeManager.PlatformBridgeActive)
        {
            if (GUILayout.Button("Set Bridge One",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(0);
            }
            if (GUILayout.Button("Set Bridge Two",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(1);
            }
            if (GUILayout.Button("Set Bridge Three",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(2);
            }
            if (GUILayout.Button("Set Bridge Four",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(3);
            }
            if (GUILayout.Button("Set Bridge Five",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(4);
            }
            if (GUILayout.Button("Set Bridge Six",GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(5);
            }
        }
        EditorGUILayout.EndHorizontal ();
        

        #endregion
        #region Platform Spawn Point GUI Buttons

        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Toggle Spawn Point",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformManager.PlatformSpawnManager.ReturnPlatformSpawnPointValue();
        }
        if (GUILayout.Button("Toggle Event Spawn Point",GUILayout.Width(80), GUILayout.Height(80))) { 
            PlatformManager.PlatformSpawnManager.ReturnPlatformEventSpawnPointValue();
        }
        EditorGUILayout.EndHorizontal ();

        #endregion
    }
}
