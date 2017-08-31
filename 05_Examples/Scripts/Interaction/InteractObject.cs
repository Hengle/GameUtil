using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUtil;
using GameUtil.OpenWorld;

namespace GameUtil.Examples
{
    public enum EInteractType 
    {
        Pickup,
        Storage,
        Door,
        Trigger,
        DefenceBuilding,
    }

    public enum EOwnerType 
    {
        Everyone,
        EveryFriend,
        EveryEnemy,
        OnlyOwner,
        OnlyLocalplayer,
    }

    public abstract class InteractObject : WorldSaveObject
    {
        public EInteractType interact_type = EInteractType.Pickup;
        public EOwnerType owner_type = EOwnerType.Everyone;
        
        abstract public void OnInteracted(LocalPlayer toucher);

        virtual public string HintString{get;}
    }

}
