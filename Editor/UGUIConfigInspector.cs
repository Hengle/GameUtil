using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameUtil;

namespace GameUtil.UI 
{
    [CustomEditor(typeof( UGUIConfigFile ))]
    public class UGUIConfigInspector : GameConfigEditor<UGUIConfigFile,string,UGUIPageData>
    {
        protected override void ShowDataList()
        {

            for (int i = 0; i < owner.Count; i++)
            {
                UGUIPageData temp = owner.GetValueAt(i);

                if (
                    (string.IsNullOrEmpty(filter) == false && temp.name_index.ToLower().Contains(filter.ToLower())) ||
                    string.IsNullOrEmpty(filter) ||
                    string.IsNullOrEmpty(temp.name_index) ||
                    string.IsNullOrEmpty(temp.type_index) ||
                    string.IsNullOrEmpty(temp.prefab_path))
                {
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Page Name", GUILayout.Width(80));
                    temp.name_index = EditorGUILayout.TextField(temp.name_index, GUILayout.Width(100));
                    EditorGUILayout.LabelField("Page Type", GUILayout.Width(80));
                    temp.type_index = EditorGUILayout.TextField(temp.type_index);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Prefab", GUILayout.Width(80));
                    temp.prefab_path = EditorGUILayout.TextField(temp.prefab_path);
                    if (GUILayout.Button("Remove"))
                    {
                        MarkAsDelete(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

}
