using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigPlane {

    /// <summary>
    /// 游戏首屏
    /// </summary>
    public class StartUI : UIPanel {

        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnSetting;
        [SerializeField] private Button btnQuit;

        protected override void Awake() {
            base.Awake();
            btnStart.onClick.AddListener(StartButtonClick);
            btnSetting.onClick.AddListener(() => UIManager.instance.ShowUI(UIPanelType.SettingUI));
            btnQuit.onClick.AddListener(() => Application.Quit());
            //btnSetting.onClick.AddListener();
        }


        void StartButtonClick() {
            Hide();
            UIManager.instance.ShowUI(UIPanelType.PlayingUI);
            GameManager.instance.StartGame();
        }

        private void OnDestroy() {
            btnStart.onClick.RemoveAllListeners();
            btnSetting.onClick.RemoveAllListeners();
            btnQuit.onClick.RemoveAllListeners();
        }

    }
}