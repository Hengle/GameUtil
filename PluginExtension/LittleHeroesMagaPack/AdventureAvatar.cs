using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.PluginExtension
{
    /// <summary>
    /// 2017年6月20日21:58:21
    /// 增加新的Class，用来获取Avatar的Transform和挂件的GameObject
    /// </summary>
    [System.Serializable]
    public class AdventureAvatar
    {
        public const int C_INDEX_HEAD = 0;
        public const int C_INDEX_BACK = 1;
        public const int C_INDEX_LH = 2;
        public const int C_INDEX_RH = 3;
        public const int C_AVATAR_COUNT = 4;

        public const string C_HEAD = "RigPelvis/RigSpine1/RigSpine2/RigRibcage/RigNeck/RigHead/Dummy Prop Head";
        public const string C_BACK = "RigPelvis/RigSpine1/RigSpine2/RigRibcage/Dummy Prop Back";
        public const string C_LEFT_HAND = "RigPelvis/RigSpine1/RigSpine2/RigRibcage/RigLArm1/RigLArm2/RigLArmPalm/Dummy Prop Left";
        public const string C_RIGHT_HAND = "RigPelvis/RigSpine1/RigSpine2/RigRibcage/RigRArm1/RigRArm2/RigRArmPalm/Dummy Prop Right";

        [SerializeField]
        private List<Transform> avatar_trans = new List<Transform>();
        private List<GameObject> avatar_objects = new List<GameObject>();

        public GameObject avatar_head
        {
            get
            {
                return __INNER_GET(C_INDEX_HEAD);
            }
            set
            {
                __INNER_SET(C_INDEX_HEAD, value);
            }
        }

        public GameObject avatar_back
        {
            get
            {
                return __INNER_GET(C_INDEX_BACK);
            }
            set
            {
                __INNER_SET(C_INDEX_BACK, value);
            }
        }

        public GameObject avatar_left_hand
        {
            get
            {
                return __INNER_GET(C_INDEX_LH);
            }
            set
            {
                __INNER_SET(C_INDEX_LH, value);
            }
        }


        public GameObject avatar_right_hand
        {
            get
            {
                return __INNER_GET(C_INDEX_RH);
            }
            set
            {
                __INNER_SET(C_INDEX_RH, value);
            }
        }

        private GameObject __INNER_GET(int index)
        {
            if (avatar_trans[index] != null)
            {
                if (avatar_objects[index] == null)
                {
                    avatar_objects[index] = avatar_trans[index].GetChild(0).gameObject;
                }
                return avatar_objects[index];
            }
            return null;
        }

        private void __INNER_SET(int index, GameObject value)
        {
            if (avatar_objects[index] != null && avatar_objects[index] != value)
            {
                if (Application.isPlaying)
                {
                    GameObject.Destroy(avatar_objects[index]);
                }
                else
                {
                    GameObject.DestroyImmediate(avatar_objects[index]);
                }
            }

            avatar_objects[index] = value;
        }

        public void FindAvatarTrans(Transform root)
        {
            avatar_trans.Clear();
            for (int i = 0; i < C_AVATAR_COUNT; i++) avatar_trans.Add(null);

            avatar_trans[C_INDEX_HEAD] = root.Find(C_HEAD);
            avatar_trans[C_INDEX_BACK] = root.Find(C_BACK);
            avatar_trans[C_INDEX_LH] = root.Find(C_LEFT_HAND);
            avatar_trans[C_INDEX_RH] = root.Find(C_RIGHT_HAND);
        }
    }
}