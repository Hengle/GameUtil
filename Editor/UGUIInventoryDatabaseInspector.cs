using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameUtil;

namespace GameUtil.UI
{
    [CustomEditor(typeof(UGUIInventoryDatabase))]
    public class UGUIInventoryDatabaseInspector : GameConfigEditor<UGUIInventoryDatabase,int,UGUIInventoryItemConfig>
    {
        protected override void ShowDataList()
        {
            for (int i = 0; i < owner.Count; i++)
            {
                UGUIInventoryItemConfig temp = owner.GetValueAt(i);

                if (string.IsNullOrEmpty(filter) ||
                    temp == null ||
                    (temp != null && temp.item_name.ToLower().Contains(filter.ToLower()))
                    )
                {
                    EditorGUILayout.BeginHorizontal();
                    if (temp != null)
                    {
                        EditorGUILayout.LabelField(temp.item_name, GUILayout.Width(80));
                    }
                    else
                    {
                        EditorGUILayout.LabelField("Empty", GUILayout.Width(80));
                    }

                    temp = EditorGUILayout.ObjectField(temp, typeof(UGUIInventoryItemConfig), false) as UGUIInventoryItemConfig;
                    owner.SetValueAt(i, temp);

                    if (GUILayout.Button("Delete") == true)
                    {
                        MarkAsDelete(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
    
}
