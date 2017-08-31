using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{

    [System.Serializable]
    public class AnimControllerSwitchItem
    {
        public string controller_name;
        public RuntimeAnimatorController controller;
    }

    [System.Serializable]
    public class AnimControllerSwitch
    {
        public AnimControllerSwitchItem[] anim_controllers;
        private Dictionary<string, AnimControllerSwitchItem> anim_controller_switch_item_index = new Dictionary<string, AnimControllerSwitchItem>();
        private Animator m_ator;

        public AnimControllerSwitch(Animator ator)
        {
            this.m_ator = ator;
            foreach (var acer in anim_controllers)
            {
                anim_controller_switch_item_index[acer.controller_name] = acer;
            }
        }

        public bool SelectRuntimeAnimController(string controller_name)
        {
            AnimControllerSwitchItem acsi = null;
            anim_controller_switch_item_index.TryGetValue(controller_name, out acsi);
            if (acsi != null)
            {
                m_ator.runtimeAnimatorController = acsi.controller;
                return true;
            }
            return false;
        }
    }
}