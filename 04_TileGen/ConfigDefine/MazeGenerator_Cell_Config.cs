using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.TileGen
{
    using GameUtil;
    using GameUtil.Algorithm.Maze;

    public class MazeGenerator_Cell_Config : TileMapBaseConfig
    {
        public EMazeAlgorithm aglorithm = EMazeAlgorithm.MazeAlgorithm_RecursiveDivision;
        public List<MazePrefab> cell_list;
    }
}

