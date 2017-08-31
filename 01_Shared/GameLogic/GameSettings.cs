using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil.UI;
using GameUtil.Algorithm.Maze;
using GameUtil.Examples;

namespace GameUtil 
{

    public class GameSettings : Singleton<GameSettings>
    {
        public UGUIConfigFile ui_page_config;
        public UGUIStringTable string_table;
        public UGUIInventoryDatabase inventory_db;

        public override void OnAwake()
        {
            DontDestroyOnLoad(this.gameObject);
            ui_page_config.RebuildIndex();
            string_table.RebuildIndex();
            inventory_db.RebuildIndex();
        }

        public static UGUIInventoryItemConfig GetInventoryItemConfig(int key) 
        {
            return Instance.inventory_db[key];
        }

        public static string GetString(string key) 
        {
            return Instance.string_table[key].value;
        }

        public static UGUIPageData FindPageData( string page_name )
        {
            return Instance.ui_page_config[page_name];
        }
    }

}