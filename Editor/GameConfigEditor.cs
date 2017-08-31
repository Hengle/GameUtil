using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameUtil 
{
    /// <summary>
    /// 所有以List存贮的，游戏内配置文件类，应该继承这个类。
    /// 提供了
    /// 1-设置脏数据位
    /// 2-Filter获取
    /// 3-Delete索引记录基本的功能。
    /// </summary>
    public abstract class GameConfigEditor<CONFIG_TYPE,KEY,USER_TYPE> : Editor where CONFIG_TYPE : GameConfigTemplate<KEY,USER_TYPE>
    {
        protected CONFIG_TYPE owner;
        protected string filter = null;

        private int pending_delete_index = -1;//注意这里必须是-1，不然后果自负。
      
        public override void OnInspectorGUI()
        {
            if (owner == null) 
            {
                owner = target as CONFIG_TYPE;
            }

            if (owner != null) 
            {
                MakeFilter();
            }
        }


        void MakeFilter() 
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Filter", GUILayout.Width(60));
            filter = EditorGUILayout.TextField(filter);

            if (GUILayout.Button("Add", GUILayout.Width(60)))
            {
                owner.AddEmpty();
            }

            if (GUILayout.Button("Save", GUILayout.Width(60)))
            {
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.EndHorizontal();
            ShowDataList();
            ExecuteDelete();
            EditorUtility.SetDirty(owner);
        }

        abstract protected void ShowDataList();

        protected void MarkAsDelete( int index ) 
        {
            pending_delete_index = index;
        }

        void ExecuteDelete() 
        {
            if (pending_delete_index != -1) 
            {
                owner.RemoveAt(pending_delete_index);
                pending_delete_index = -1;
            }
        }
    }

}
