using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameUtil;

namespace GameUtil.UI 
{



    [CustomEditor(typeof(UGUIStringTable))]
    public class UGUIStringTableInspector : GameConfigEditor<UGUIStringTable,string,UGUIStringItem>
    {
        protected override void ShowDataList()
        {
            for (int i = 0; i < owner.Count; i++)
            {
                UGUIStringItem temp = owner.GetValueAt(i);

                if (string.IsNullOrEmpty(filter) || string.IsNullOrEmpty(temp.key) || (string.IsNullOrEmpty(filter) == false && temp.key.ToLower().Contains(filter.ToLower())))
                {
                    EditorGUILayout.BeginHorizontal();
                    temp.key = EditorGUILayout.TextField(temp.key, GUILayout.Width(120));
                    temp.value = EditorGUILayout.TextField(temp.value);
                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        MarkAsDelete(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        //public UGUIStringTable owner;
        //public string filter = null;
        //public int pending_delete_index = -1;
        //public override void OnInspectorGUI()
        //{
        //    if (owner == null) 
        //    {
        //        owner = target as UGUIStringTable;            
        //    }

        //    if (owner != null) 
        //    {
        //        EditorGUILayout.BeginHorizontal();
        //        EditorGUILayout.LabelField("Filter",GUILayout.Width(60));

        //        filter = EditorGUILayout.TextField(filter);

        //        if (GUILayout.Button("Add",GUILayout.Width(60))) 
        //        {
        //            owner.AddEmpty();
        //            Debug.Log("owner.count " + owner.Count);
        //        }

        //        if (GUILayout.Button("Save", GUILayout.Width(60)))
        //        {
        //            AssetDatabase.SaveAssets();
        //        }
                
        //        EditorGUILayout.EndHorizontal();
        //        EditorGUILayout.Space();


 

        //        if (pending_delete_index >= 0) 
        //        {
        //            owner.RemoveAt(pending_delete_index);
        //            pending_delete_index = -1;
        //        }

        //        EditorUtility.SetDirty(owner);
        //    }
        //}
    }


}
