using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigPlane {

    public class SliderSetting : MonoBehaviour {


        [SerializeField] private Text m_text;
        [SerializeField] private Slider m_slider;
        [SerializeField] private Text m_value;



        private void Awake() {
            m_slider.onValueChanged.AddListener(AddValue);
        }

        private void AddValue(float value) {
            m_value.text = $"{(int)(value * 100f)}/%";
        }

        public void Init(string name,float value) {
            this.m_text.text = name;
            this.m_slider.value = value;
        }




        

    }
}