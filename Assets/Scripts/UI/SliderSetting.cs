using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BigPlane {

    public class SliderSetting : MonoBehaviour {


        [SerializeField] private Text m_Title;
        [SerializeField] private Slider m_slider;
        [SerializeField] private Text m_value;



        private void Awake() {
            m_slider.onValueChanged.AddListener(SetValue);
        }

        public void SetValue(float value) {
            m_slider.value = value;
            m_value.text = $"{(value * 100f):f1}/%";
        }


        public void Init(float value,UnityAction<float> callback) {
            SetValue(value);
            m_slider.onValueChanged.AddListener(callback);
        }

        public void Init(string name,float value) {
            this.m_Title.text = name;
            this.m_slider.value = value;
        }

        private void OnDestroy() {
            m_slider.onValueChanged.RemoveAllListeners();
        }






    }
}