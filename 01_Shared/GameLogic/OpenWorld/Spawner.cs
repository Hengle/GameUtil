using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil 
{
    public interface ISpawnObject 
    {
        ESpawnType spawnType
        {
            get;
            set;
        }
    }

    public enum ESpawnType
    {
        SpawnOnce,
        SpawnByDuration,
        SpawnByTrigger,
    }

    public class Spawner : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
