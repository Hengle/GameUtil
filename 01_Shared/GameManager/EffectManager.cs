using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GameUtil 
{
    [RequireComponent(typeof(RectTransform))]
    public class EffectManager : Singleton<EffectManager>
    {

        #region INNER CLASSES
        [System.Serializable]
        public class PSEffectProfile
        {
            public string eff_name;
            public string path;
        }

        [System.Serializable]
        public class UIEffectProfile
        {
            public string effect_name;
            public string effect_prefab;
            public Transform reference_trans;
        }

        #endregion

        [SerializeField]
        List<PSEffectProfile> partical_effects = new List<PSEffectProfile>();

        [SerializeField]
        List<UIEffectProfile> ui_effects = new List<UIEffectProfile>();

        public void SpawnParticleEffectWithColor(string eff_name, Vector3 world_pos, Color color, float delay_kill = 2) 
        {
            PSEffectProfile profile = partical_effects.Find(x => x.eff_name == eff_name);
            if (profile != null)
            {
                ParticleSystem ps = ResourceManager.Instance.CreateInstance<ParticleSystem>(profile.path, null);
                ps.transform.position = world_pos;
                ps.transform.localScale = Vector3.one;

                ParticleSystem[] pss = ps.gameObject.GetComponentsInChildren<ParticleSystem>();
                foreach (var pppps in pss)
                {
                    //HackMe:看看有没有其他方式，设置粒子的颜色
                    //Debug.Log("ps name " + pppps.name +"Color is " + color);
                    pppps.startColor = color;
                }

                ps.Play(true);

                if (delay_kill > 0)
                {
                    Destroy(ps.gameObject, 2);
                }
            }
        } 

        public ParticleSystem SpawnParticleEffect(string eff_name, Vector3 world_pos, float delay_kill = 2)
        {
            PSEffectProfile profile = partical_effects.Find(x => x.eff_name == eff_name);
            if (profile != null)
            {
                ParticleSystem ps = ResourceManager.Instance.CreateInstance<ParticleSystem>(profile.path, null);
                //ps.transform.parent = GameManager.Instance.gameObject.transform;
                ps.transform.position = world_pos;
                ps.transform.localScale = Vector3.one;

                ps.Play(true);

                if (delay_kill > 0)
                {
                    Destroy(ps.gameObject, delay_kill);
                }

                return ps;
            }

            Debug.Log("Profile not found!");
            return null;
        }

		public ParticleSystem SpawnParticleEffectForUGUI( string eff_name, Graphic parent,float delay_kill = 2 )
		{
			PSEffectProfile profile = partical_effects.Find(x => x.eff_name == eff_name);
			if (profile != null)
			{
				ParticleSystem ps = ResourceManager.Instance.CreateInstance<ParticleSystem>(profile.path, null);
				ps.gameObject.layer = LayerMask.NameToLayer("UI");
				ps.transform.parent = parent.transform;
				ps.transform.localPosition = Vector3.zero;
				ps.transform.localScale = Vector3.one;
				
				ps.Play(true);
				
				if (delay_kill > 0)
				{
					Destroy(ps.gameObject, delay_kill);
				}

				return ps;
			}
			return null;
		}

        void WorldToUI(Graphic ui_obj, Vector3 world_pos)
        {
            Vector2 pos;
            Canvas canvas = GetComponentInParent<Canvas>();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Camera.main.WorldToScreenPoint(world_pos), canvas.worldCamera, out pos);
            RectTransform rect = ui_obj.transform as RectTransform;
            rect.anchoredPosition = pos;
        }

        public void SpawnUGUIImageEffect(string effect_name, Vector3 world_pos = default(Vector3))
        {
            UIEffectProfile profile = ui_effects.Find(x => x.effect_name == effect_name);
            if (profile != null)
            {
                Graphic ui_gra = ResourceManager.Instance.CreateInstance<Graphic>(profile.effect_prefab, this.transform);
                if (profile.reference_trans != null)
                {
                    ui_gra.transform.SetParent(profile.reference_trans);
                    ui_gra.rectTransform.anchoredPosition = Vector2.zero;
                }
                else
                {
                    WorldToUI(ui_gra, world_pos);
                }
            }
        }

		public void SpawnUGUITextEffect(string effect_name, string text, Color color,  Vector3 world_pos = default(Vector3))
        {
            UIEffectProfile profile = ui_effects.Find(x => x.effect_name == effect_name);
            if (profile != null)
            {
                Text ui_text = ResourceManager.Instance.CreateInstance<Text>(profile.effect_prefab, this.transform);

                if (ui_text != null)
                {
                    ui_text.text = text;
					ui_text.color = color;
                    if (profile.reference_trans != null)
                    {
                        ui_text.transform.SetParent(profile.reference_trans);
                        ui_text.rectTransform.anchoredPosition = Vector2.zero;
                    }
                    else
                    {
                        WorldToUI(ui_text, world_pos);
                    }
                }
                else
                {
                    Debug.LogError("错误：Prefab上没有找到Text控件");
                }
            }
        }


        public List<AnimTweener> bump_effect = new List<AnimTweener>();
        public void AddToBumpQueue(AnimTweener at)
        {
            for (int i = 0; i < bump_effect.Count; i++)
            {
                if (bump_effect[i] != null) 
                {
                    bump_effect[i].OnQueueBumped();
                }
            }

            if (bump_effect.Contains(at) == false)
            {
                bump_effect.Add(at);
            }

        }

        public void RemoveFromBumpQueue(AnimTweener at) 
        {
            bump_effect.Remove(at);
        }
    }

}

