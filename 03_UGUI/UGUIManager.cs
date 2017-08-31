using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GameUtil.UI
{
    /// <summary>
    /// 即便是个小游戏，也要有超级牛逼的UI系统。
    /// </summary>
    public class UGUIManager : Singleton<UGUIManager>
    {
        public bool always_show_cursor = false;
        public List<string> auto_load_pages = new List<string>();
        public static int Language = 0;//0-english,1-chinese i do not know other languages....

        [SerializeField]
        Canvas canvas;

        public Camera UICamera 
        {
            get 
            {
                return canvas.worldCamera;
            }
        }

        List<UGUIWidget> page_stack = new List<UGUIWidget>();

        public bool NeedShowCursor 
        {
            get 
            {
                if (always_show_cursor) return true;

                for (int i = 0; i < page_stack.Count; i++) 
                {
                    if (page_stack[i].dialog_type != UGUIWidget.EDialogType.NonDialog) 
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override void OnAwake()
        {
            foreach (var page in auto_load_pages) 
            {
                UGUIPageData pd = GameSettings.FindPageData(page);
                MethodInfo mi = typeof(UGUIManager).GetMethod("OpenDialog").MakeGenericMethod(System.Type.GetType(pd.type_index));
                mi.Invoke(this, new object[] { pd.name_index,"" });
            }
        }

        /// <summary>
        /// 规定任何同一个页面不可以同时开两个.如果一定要这么做，那么通过extra_index区分
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page_name"></param>
        /// <returns></returns>
        public T OpenDialog<T>(string page_name,string extra_index = "") where T : UGUIWidget
        {
            UGUIPageData data = GameSettings.FindPageData(page_name);

            if (data == null)
            {
                Debug.LogError(typeof(T) + " not found or type does not patch!");
                return default(T);
            }
            
            if (canvas == null)
            {
                Debug.LogError("No root canvas found");
                return default(T);
            }
            
            T new_page = ResourceManager.Instance.CreateInstance<T>(data.prefab_path, canvas.transform);
            new_page.transform.localScale = Vector3.one;
            (new_page.transform as RectTransform).anchoredPosition = Vector2.zero;
            

            //TODO:暂时不考虑太多，只要打开就加入队列。关闭一个，自动把队列里面上一个恢复了。或者上一个批次恢复，这个是应该做的。
            //通过设置HideBatch字段，来实现这个效果。每次调用Hide方法，HideBatch+1，关闭一个上层窗体，则会将所有页面的HideBatch-1，恢复HideBatch为0的所有窗体（这是自动的）
            if (new_page != null) 
            {   
                new_page.name = string.IsNullOrEmpty(extra_index) ? page_name : page_name + "#" + extra_index;
                page_stack.Add(new_page);
                CheckHideOthers(new_page);
            }

            return new_page;
        }

        public void CloseDialog(string page_name, string extra_index = "") 
        {
            string key = string.IsNullOrEmpty(extra_index) ? page_name : page_name + "#" + extra_index;
            UGUIWidget page = page_stack.Find(x => x.name == key);
            if( page != null )
            {
                page_stack.Remove(page);
                page.Dispose();
            }
        }

        public void CloseDialog(UGUIWidget page) 
        {   
            page_stack.Remove(page);
            page.Dispose();
        }

        public bool IsPageContainTagOpened(string tag) 
        {
            return page_stack.Find(x=>x.name.Contains(tag)) != null;
        }

        public bool IsPageOpened(string page_name, string extra_index = "") 
        {
            string key = string.IsNullOrEmpty(extra_index) ? page_name : page_name + "#" + extra_index;
            return page_stack.Find(x => x.name == key) != null;
        }

        public T FindPage<T>(string page_name, string extra_index = "") where T:UGUIWidget
        {
            string key = string.IsNullOrEmpty(extra_index) ? page_name : page_name + "#" + extra_index;
            return (T)page_stack.Find(x => x.name == key);
        }

        /// <summary>
        /// Hack Me:增加打开一个模态对话框，关闭其他对话框的功能。
        /// TODO:
        /// (1)根据对话框类别不同，决定是否关闭其他对话框。
        /// (2)每次调用关闭，hideBatch+1，表示一个隐藏批次
        /// (3)每次调用恢复，hideBatch-1，
        /// (4)在UGUI内部进行处理，如果hideBatch>0则隐藏，反之则显示。
        /// 实现隐藏一个批次，显示一个批次的旧的对话框的效果——————》》》》》虽然这可能根本就没用。
        /// </summary>
        /// <param name="widget"></param>
        void CheckHideOthers(UGUIWidget widget) 
        {

        }
    }

}
