using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BigPlane {

    public class SettingUI : MonoBehaviour {

        [SerializeField] private SliderSetting smallPlaneSetting;
        [SerializeField] private SliderSetting bigPlaneSetting;
        [SerializeField] private SliderSetting largePlaneSetting;
        [SerializeField] private SliderSetting autoShootSetting;
        [SerializeField] private SliderSetting trackSetting;


        private void Awake() {
            Show(
                float.Parse(GameSetting.GetSetting("SmallPlaneProp")),
                float.Parse(GameSetting.GetSetting("BigPlaneProp")),
                float.Parse(GameSetting.GetSetting("LargePlaneProp")),
                float.Parse(GameSetting.GetSetting("AutoShootProp")),
                float.Parse(GameSetting.GetSetting("AutoChangeDirProp"))
                );
            Init();
        }

        public void Show(float small, float big, float large, float autoshoot, float track) {
            smallPlaneSetting.SetValue(small);
            bigPlaneSetting.SetValue(big);
            largePlaneSetting.SetValue(large);
            autoShootSetting.SetValue(autoshoot);
            trackSetting.SetValue(track);
        }

        public void Init() {
            smallPlaneSetting.Init(float.Parse(GameSetting.GetSetting("SmallPlaneProp")),(value)=>GameSetting.UpdateValue("SmallPlaneProp",value.ToString()));
            bigPlaneSetting.Init(float.Parse(GameSetting.GetSetting("BigPlaneProp")),(value)=>GameSetting.UpdateValue("BigPlaneProp", value.ToString()));
            largePlaneSetting.Init(float.Parse(GameSetting.GetSetting("LargePlaneProp")),(value)=>GameSetting.UpdateValue("LargePlaneProp", value.ToString()));
            autoShootSetting.Init(float.Parse(GameSetting.GetSetting("AutoShootProp")),(value)=>GameSetting.UpdateValue("AutoShootProp", value.ToString()));
            trackSetting.Init(float.Parse(GameSetting.GetSetting("AutoChangeDirProp")),(value)=>GameSetting.UpdateValue("AutoChangeDirProp", value.ToString()));
        }

        public void Hide() {
            GameManager.instance.ReadData();
            transform.gameObject.SetActive(false);
        }


    }
}