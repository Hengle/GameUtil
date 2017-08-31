using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtil;
using GameUtil.UI;

namespace GameUtil.Examples
{
    /// <summary>
    /// You can drive from UGUIInventoryItem and make your own version.
    /// Also drive from UGUIInventory to make your own package.
    /// </summary>
    public class UIInventory : UGUIInventory<UGUIInventoryItem>
    {
        protected override void OnShowPage()
        {
            LocalPlayer lp = GameFacade.Instance.GetLocalPlayer();
            lp.operation_state = EOperationState.Managing_Inventory;
        }

        protected override void OnHidePage()
        {
            LocalPlayer lp = GameFacade.Instance.GetLocalPlayer();
            lp.operation_state = EOperationState.Default;
        }
    }
}