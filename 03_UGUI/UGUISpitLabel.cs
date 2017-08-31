using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace GameUtil.UI
{
    /// <summary>
    /// 一个字一个字吐的效果。我他妈就是个天灾，啊，不是，是天才。
    /// </summary>
    public class UGUISpitLabel :MonoBehaviour
    {
        public Text spit_target;
        public float delay;
        [Range(0.01f,1f)]
        public float spit_speed = 0.03f;

        string all_words;
        StringBuilder spitted_words = new StringBuilder();

        void OnEnable() 
        {
            all_words = spit_target.text;
            spit_target.text = "";

            if (delay > 0)
            {
                Invoke("BeginSpit", delay);
            }
            else 
            {
                BeginSpit();
            }
        }

        void BeginSpit() 
        {
            StartCoroutine(Spit());
        }

        IEnumerator Spit() 
        {
            int index = 0;
            while (spitted_words.Length < all_words.Length) 
            {
                spitted_words.Append(all_words[index]);
                spit_target.text = spitted_words.ToString();
                index++;
                yield return new WaitForSeconds(spit_speed);
            }
        }
    }

}