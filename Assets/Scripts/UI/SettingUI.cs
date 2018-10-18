using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BigPlane {
    /// <summary>
    /// 设置界面
    /// </summary>
    public class SettingUI : UIPanel {

        [Header("滑块")]
        [SerializeField] private SliderSettingItem smallPlaneSetting;
        [SerializeField] private SliderSettingItem bigPlaneSetting;
        [SerializeField] private SliderSettingItem largePlaneSetting;
        [SerializeField] private SliderSettingItem autoShootSetting;
        [SerializeField] private SliderSettingItem trackSetting;
        [SerializeField] private SliderSettingItem shootSpeedSetting;

        [Header("Buttons")]
        [SerializeField] private Button btnClose;


        private CanvasGroup cg;

        protected override void Awake() {
            base.Awake();
            cg = GetComponent<CanvasGroup>();            

            ShowData(
                float.Parse(GameSetting.GetSetting("SmallPlaneProp")),
                float.Parse(GameSetting.GetSetting("BigPlaneProp")),
                float.Parse(GameSetting.GetSetting("LargePlaneProp")),
                float.Parse(GameSetting.GetSetting("AutoShootProp")),
                float.Parse(GameSetting.GetSetting("AutoChangeDirProp")),
                float.Parse(GameSetting.GetSetting("ShootSpeed"))
                
                );
            Init();


            btnClose.onClick.AddListener(Hide);
        }



        public void ShowData(float small, float big, float large, float autoshoot, float track,float shootSpeed) {
            smallPlaneSetting.SetValue(small);
            bigPlaneSetting.SetValue(big);
            largePlaneSetting.SetValue(large);
            autoShootSetting.SetValue(autoshoot);
            trackSetting.SetValue(track);
            shootSpeedSetting.SetValue(shootSpeed);
        }

        public override void Init() {
            smallPlaneSetting.Init(float.Parse(GameSetting.GetSetting("SmallPlaneProp")),(value)=>GameSetting.UpdateValue("SmallPlaneProp",value.ToString()));
            bigPlaneSetting.Init(float.Parse(GameSetting.GetSetting("BigPlaneProp")),(value)=>GameSetting.UpdateValue("BigPlaneProp", value.ToString()));
            largePlaneSetting.Init(float.Parse(GameSetting.GetSetting("LargePlaneProp")),(value)=>GameSetting.UpdateValue("LargePlaneProp", value.ToString()));
            autoShootSetting.Init(float.Parse(GameSetting.GetSetting("AutoShootProp")),(value)=>GameSetting.UpdateValue("AutoShootProp", value.ToString()));
            trackSetting.Init(float.Parse(GameSetting.GetSetting("AutoChangeDirProp")),(value)=>GameSetting.UpdateValue("AutoChangeDirProp", value.ToString()));
            shootSpeedSetting.Init(float.Parse(GameSetting.GetSetting("ShootSpeed")),(value)=>GameSetting.UpdateValue("ShootSpeed", value.ToString()));
        }


        public override void Hide() {
            base.Hide();
            GameManager.instance.ReadData();
        }

        public void OnDestroy() {
            btnClose.onClick.RemoveAllListeners();
        }

        public override void Show() {
            
            base.Show();

            StartCoroutine(FadeIn());

        }

        IEnumerator FadeIn() {
            while (cg.alpha<0.99f) {
                cg.alpha += 0.01f;
                yield return null;
            }
            cg.alpha = 1f;
        }
    }


}