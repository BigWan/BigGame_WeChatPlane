using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BigPlane {

    /// <summary>
    /// 游戏中的UI
    /// </summary>
    public class PlayingUI : UIPanel {

        [SerializeField] private Text txtScore;
        [SerializeField] private Button btnPause;

        [SerializeField] private GameObject group;

        [SerializeField] private Button btnResume;
        [SerializeField] private Button btnReStart;
        [SerializeField] private Button btnQuit;


        protected override void Awake() {
            base.Awake();

            txtScore.gameObject.SetActive(true);
            btnPause.gameObject.SetActive(true);
            group.SetActive(false);

            btnPause.onClick.AddListener(PauseClick);
            btnResume.onClick.AddListener(ResumeClick);

            btnReStart.onClick.AddListener(()=>GameManager.instance.ReStart());
            btnQuit.onClick.AddListener(()=>Application.Quit());

            txtScore.text = "0";
        }


        private void PauseClick() {
            GameManager.instance.Pause();
            btnPause.gameObject.SetActive(false);
            
            group.SetActive(true);
        }

        private void ResumeClick() {
            GameManager.instance.Resume();
            group.SetActive(false);
            btnPause.gameObject.SetActive(true);
        }

        // API

        /// <summary>
        /// 显示分数
        /// </summary>
        public void ShowScore(int score) {
            txtScore.text = score.ToString();
        }

    }
}
