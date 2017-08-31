using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using GameUtil;

namespace GameUtil.OpenWorld 
{
    [CustomEditor(typeof(WorldManager))]
    public class WorldManagerInspector : Editor
    {
        protected WorldManager owner;

        public override void OnInspectorGUI()
        {
            if (owner == null) 
            {
                owner = target as WorldManager;
            }

            if (owner != null) 
            {
                EditorGUILayout.LabelField("Profile: " + WorldProfile.GetProfilePath(true));

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("SaveWorld")) 
                {
                    owner.SaveWorld();
                }

                if (GUILayout.Button("ClearWorld")) 
                {
                    if (EditorUtility.DisplayDialog("Warning!", "This will remove all WorldSaveObjects. Are you sure?", "Im sure", "Nooooops!"))
                    {
                        owner.ClearWorld();
                    }
                }
                
                if (GUILayout.Button("LoadWorld")) 
                {
                    owner.LoadWorld();
                }
                EditorGUILayout.EndHorizontal();


            }
        }
    }

}
