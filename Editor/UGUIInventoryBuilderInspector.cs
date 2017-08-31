using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using GameUtil.UI;

[CustomEditor(typeof(UGUIInventoryBuilder))]
public class UGUIInventoryBuilderInspector : Editor
{
    public UGUIInventoryBuilder builder;

    public override void OnInspectorGUI()
    {
        builder = this.target as UGUIInventoryBuilder;

        base.OnInspectorGUI();

        if (GUILayout.Button("Build Inventory") == true ) 
        {
            builder.BuildInventory();
        }
    }
}
