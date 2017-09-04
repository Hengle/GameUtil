using UnityEngine;
using UnityEditor;
using GameUtil.UI;
using GameUtil.Algorithm.Maze;
using GameUtil.TileGen;
using GameUtil.Examples;

namespace GameUtil 
{

    public class CreateScriptableAsset : MonoBehaviour
    {

        public static T CreateAsset<T>(string path) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

            return asset;
        }

        [MenuItem("Custom Scriptable Asset/UGUI ConfigFile")]
        public static void CreateSceneInitTemplateAsest()
        {
            CreateAsset<UGUIConfigFile>("Assets/GameUtil/Resources/");
        }
        
        [MenuItem("Custom Scriptable Asset/Inventory/OriginalInventoryItem")]
        public static void CreateInventoryItem()
        {
            CreateAsset<UGUIInventoryItemConfig>("Assets/GameUtil/UGUIManager/Resources/InventoryItems/");
        }

        /// <summary>
        /// 自己的东西，在自己的类里面生成。
        /// </summary>
        [MenuItem("Custom Scriptable Asset/Database/Inventory Database")]
        public static void CreateInventoryDatabase()
        {
            GameUtil.CreateScriptableAsset.CreateAsset<UGUIInventoryDatabase>("Assets/GameUtil/UGUIManager/Resources/");
        }

        [MenuItem("Custom Scriptable Asset/Database/String Table")]
        public static void CreateInventoryStringTable()
        {
            CreateAsset<UGUIStringTable>("Assets/GameUtil/Resources/Configs/");
        }

        ///<--- TODO ADD USER DEFINED INVENTORY ITEM CONFIG HERE, WHICH DRIVED FORM UGUIInventoryItemConfig
        [MenuItem("Custom Scriptable Asset/Inventory/BuildingBlock")]
        public static void CreateInventoryBuildingBlock()
        {
            CreateAsset<BuildingBlockConfig>("Assets/GameUtil/Resources/Configs/InventoryItems/");
        }
        ///--->
        
        [MenuItem("Custom Scriptable Asset/GeneratorConfig/Prefab Based Maze")]
        public static void CreatePrefabBasedMazeConfig()
        {
            CreateAsset<MazeGenerator_Cell_Config>("Assets/GameUtil/Resources/Configs/");
        }

        [MenuItem("Custom Scriptable Asset/GeneratorConfig/Basic Perlin Map")]
        public static void CreatePerlinMapConfig() 
        {
            CreateAsset<MapGenerator_Perlin_Config>("Assets/GameUtil/Resources/Configs/");
        }

        [MenuItem("Custom Scriptable Asset/GeneratorConfig/TileBased Maze Config")]
        public static void CreateTileBasedMazeMapConfig()
        {
            CreateAsset<MazeGenerator_Tile_Config>("Assets/GameUtil/Resources/Configs/");
        }

        [MenuItem("Custom Scriptable Asset/GeneratorConfig/Tile Prefab Group")]
        public static void CreateTilePrefabGroup() 
        {
            CreateAsset<TilePrefabConfig>("Assets/GameUtil/Resources/Configs/TileMapGroups");
        }

        [MenuItem("Custom Scriptable Asset/GeneratorConfig/Tile Prefab Theme")]
        public static void CreateTileThemeGroup() 
        {
            CreateAsset<TileThemeConfig>("Assets/GameUtil/Resources/Configs/TileMapGroups");
        }
        
        [MenuItem("Custom Scriptable Asset/TrajectoryConfig/UnrealisticTrajectory")]
        public static void CreateUnrealisticTrajectory()
        {
            CreateAsset<GameUtil.Trajectory.UnrealisticDeviationParams>("Assets/GameUtil/Resources/Configs/Trajectory");
        }
    }
}
