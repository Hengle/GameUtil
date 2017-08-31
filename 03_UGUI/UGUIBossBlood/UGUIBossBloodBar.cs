using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUtil.UI
{
    public class UGUIBossBloodBar : MonoBehaviour
    {
        public float bleeding_speed = 10;
        public int init_blood_per_frame = 100;
        public int total_blood;
        public int blood_per_line;

        public Image image_back_bar;
        public Image image_effect_bar;
        public Image image_front_bar;

        public Sprite[] blood_sprites;
        public Text txt_blood_lines;
        public Text txt_blood_digits;

        private int target_blood;
        private int effect_blood;
        private int last_index = -1;

        void Start() 
        {
            //Test Code Here.
            Reset();
        }

        public void OnTestHit() 
        {
            int current_blood = target_blood - Random.Range( 100,400 );

            if(current_blood < 0)
                current_blood = 0;

            UpdateBlood(current_blood);
        }

        public void Reset( int value = -1 ) 
        {
            if (value < 0)
            {
                target_blood = total_blood;
            }
            else 
            {
                target_blood = value;
            }

            UpdateBlood(target_blood);
       }

        void UpdateImage() 
        {
            int new_index = target_blood / blood_per_line;
            if (new_index != last_index) 
            {
                //交换图片的时候，要更新一次effect,直接取消掉没播完的effect
                last_index = new_index;
                effect_blood = target_blood;

                int image_index = last_index % blood_sprites.Length;

                image_front_bar.sprite = blood_sprites[image_index];
                image_effect_bar.sprite = blood_sprites[image_index];
                image_back_bar.color = last_index == 0 ? new Color(0, 0, 0, 0) : Color.white;

                int back_image_index = image_index - 1;
                if (back_image_index < 0)
                    back_image_index = blood_sprites.Length - 1;
                image_back_bar.sprite = blood_sprites[back_image_index % blood_sprites.Length];
            }
        }

        void UpdateFillAndText() 
        {
            int temp_val =  target_blood - last_index * blood_per_line;
            float temp_rate = (float)temp_val / (float)blood_per_line;

            Debug.Log("Front Rate is " + temp_rate);

            image_front_bar.fillAmount = temp_rate;
            image_back_bar.fillAmount = 1;

            txt_blood_lines.text = "x" + last_index;
            txt_blood_digits.text = target_blood + "/" + total_blood;  
        }


        void UpdateEffectBlood()
        {
            int temp_val = effect_blood - last_index  * blood_per_line;
            float temp_rate = (float)temp_val / (float)blood_per_line;
            image_effect_bar.fillAmount = temp_rate;
        }

        public void UpdateBlood(int new_blood_value) 
        {
            target_blood = new_blood_value;
            UpdateImage();
            UpdateFillAndText();
        }

        void Update()
        {
            effect_blood = (int)Mathf.Lerp(effect_blood, target_blood, Time.deltaTime * bleeding_speed);
            UpdateEffectBlood();
        }
    }
}
