using UnityEngine;
using System.Collections;

namespace BigPlane {

    /// <summary>
    /// 音效管理器
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour {


        #region "单例"
        private static SoundManager m_instance;

        public static SoundManager instance {
            get {
                if (m_instance == null)
                    m_instance = FindObjectOfType<SoundManager>() as SoundManager;
                if (m_instance == null)
                    throw new UnityException("场景中没有SoundManager");
                return m_instance;
            }
        }

        #endregion


        [Header("Music")]
        [SerializeField] private AudioClip acButtonClick;
        [SerializeField] private AudioClip acBgMusic;


        [Header("Sound Effect")]

        [SerializeField] private AudioClip em1;
        [SerializeField] private AudioClip em2;
        [SerializeField] private AudioClip em3;

        [SerializeField] private AudioClip shoot;

        [Header("播放器")]

        [SerializeField] private AudioSource musicAS;
        [SerializeField] private AudioSource effectAS;


        private void Awake() {
            
        }




    }
}