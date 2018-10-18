using UnityEngine;
using System.Collections;
using System;

using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace BigPlane {

    public enum UIPanelType {
        StartUI,
        SettingUI,
        PlayingUI
    }




    public class UIManager : MonoBehaviour {

        #region "单例"
        private static UIManager m_instance;

        public static UIManager instance {
            get {
                if (m_instance == null)
                    m_instance = FindObjectOfType<UIManager>() as UIManager;
                if (m_instance == null)
                    throw new UnityException("场景中没有UIManager");
                return m_instance;
            }
        }

        #endregion


        public StartUI startUI;
        public SettingUI settingUI;
        public PlayingUI playingUI;


        private Dictionary<UIPanelType, UIPanel> m_uiDict = new Dictionary<UIPanelType, UIPanel>();


        private void Awake() {

            m_uiDict.Add(UIPanelType.StartUI, startUI);
            m_uiDict.Add(UIPanelType.SettingUI, settingUI);
            m_uiDict.Add(UIPanelType.PlayingUI, playingUI);


            ShowUI(UIPanelType.StartUI);
            HideUI(UIPanelType.SettingUI);
            HideUI(UIPanelType.PlayingUI);
        }

        public void ShowUI(UIPanelType panel) {

            m_uiDict[panel].Show();
            
        }

        public void HideUI(UIPanelType panel) {
            m_uiDict[panel].Hide();
        }




    }
}