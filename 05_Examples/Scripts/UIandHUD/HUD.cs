using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameUtil;
using GameUtil.UI;

namespace GameUtil.Examples 
{

    public class HUD : MonoBehaviour
    {
        public Image cross_aim;
        public GameObject center_hint_panel;
        public Text center_hint;
        public UISwiftInventory ui_swift_inventory;
        
        LocalPlayer player;

        void OnEnable() 
        {
            player = GameFacade.Instance.GetLocalPlayer();
            InitSwiftInventroy();
        }

        public void InitSwiftInventroy() 
        {
            Inventory swift_inventory = player.swift_inventory;
            ui_swift_inventory.LoadInventory(swift_inventory);
        }
        
        public void RedCross()
        {
            cross_aim.color = Color.red;
        }

        public void GreenCross()
        {
            cross_aim.color = Color.green;
        }

        public string centerhint
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    center_hint_panel.SetActive(false);
                }
                else
                {
                    center_hint_panel.SetActive(true);
                    center_hint.text = value;
                }
            }
        }

        float inventory_cool_down;
        void Update()
        {
            if (UGUIManager.Instance.NeedShowCursor )
            {
                Cursor.visible = true;
#if UNITY_EDITOR
                Cursor.lockState = CursorLockMode.None;
#else
            Cursor.lockState = CursorLockMode.Confined;
#endif
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }


            if (Input.GetButton("Inventory") && inventory_cool_down <= 0)
            {
                inventory_cool_down = 5;
                if ( player.operation_state != EOperationState.Managing_Inventory )
                {
                    UIInventory ui_inventory = UGUIManager.Instance.OpenDialog<UIInventory>("Player50Slot");
                    ui_inventory.LoadInventory(player.inventory);
                }
                else
                {
                    UGUIManager.Instance.CloseDialog("Player50Slot");
                }
            }

            if (inventory_cool_down > 0) inventory_cool_down--;

            DetectInteractable();
        }

        private InteractObject last_interact_object = null;
        public void DetectInteractable()
        {
            if (UGUIManager.Instance.NeedShowCursor == true) return;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit result;

            if (Physics.Raycast(ray, out result, 2, LayerMask.GetMask("Default","Buildings")))
            {
                if (result.collider.gameObject.tag == "Interactable") 
                {
                    if (   last_interact_object == null ||( last_interact_object != null && last_interact_object.gameObject != result.collider.gameObject))
                    {
                        last_interact_object = result.collider.gameObject.GetComponent<InteractObject>();
                    }

                    if (last_interact_object != null) 
                    {
                        centerhint = last_interact_object.HintString;

                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            last_interact_object.OnInteracted(player);
                        }
                        return;
                    }
                }
            }

            GreenCross();
            centerhint = "";
        }
    }

}
