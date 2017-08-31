using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameUtil 
{
    public class CharacterNameGen : Singleton<CharacterNameGen>
    {
        public List<string> prefix;
        public List<string> postfix;

        public override void OnAwake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public bool blank_in_prefix_and_postfix = true;
        public string GetFullName 
        {
            get 
            {
                if (blank_in_prefix_and_postfix == false) 
                {
                    return prefix[Random.Range(0, prefix.Count)] + postfix[Random.Range(0, postfix.Count)];
                }
                return prefix[Random.Range(0, prefix.Count)] +" "+ postfix[Random.Range(0, postfix.Count)];                
            }
        }

        public string GetSingleName
        {
            get 
            {
                return prefix[Random.Range(0, prefix.Count)];
            }
        }
    }

}

