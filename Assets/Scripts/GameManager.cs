using UnityEngine;
using System.Collections;
using System;

using Random = UnityEngine.Random;

namespace BigPlane {

    public enum GameState {
        Ready,
        Playing,
        Pause,
    }


    public class GameManager : MonoBehaviour {

        #region "单例"
        private static GameManager m_instance;

        public static GameManager instance {
            get {
                if (m_instance == null)
                    m_instance = FindObjectOfType<GameManager>() as GameManager;
                if (m_instance == null)
                    throw new UnityException("场景中没有GameManager");
                return m_instance;
            }
        }



        #endregion

        // 预制物体引用

        [Header("Prefabs config")]

        [SerializeField] private Player m_playerPrefab;
        private Player m_player;

        [SerializeField] private Background m_backgroundPrefab;


        private Background m_background;

        [SerializeField]private Boundary m_boundaryPrefab;
        private Boundary m_boundary;

        
        [Header("敌机")]

        // 敌机
        [SerializeField]private Enermy m_smallEnermy;
        private float m_smallEnermyProp;

        [SerializeField]private Enermy m_bigEnermy;
        private float m_bigEnermyProp;

        [SerializeField]private Enermy m_largeEnermy;
        private float m_largeEnermyProp;


        // 刷怪点配置
        [SerializeField] private Transform m_spwanPoint;


        // 其他数据

        private float m_autoShootProp;
        private float m_changeDirProp;


        [SerializeField] private float m_defaultSpawnInterval = 2f;

        private float m_spawnInterval { get { return m_defaultSpawnInterval-m_level*0.1f; } }
        private float m_lastSpawnTime;

        /// <summary>
        /// 等级
        /// </summary>
        private int m_level { get { return (int)(m_score / 1000f); } }

        private int m_score;

        GameState m_gameState;
        private Camera m_camera;



        // 内部方法

        /// <summary>
        /// 创建实例和预制物体的引用
        /// 先查找场景中是否存在,
        /// 不存在则实例化一个
        /// 再不行就报错
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        /// <param name="prefab">预制物体</param>
        private void CreateRef<T>(ref T instance, T prefab)where T:MonoBehaviour {
            if (instance != null)
                return;

            instance = FindObjectOfType<T>() as T;
            if (instance != null)
                return;

            instance = Instantiate(prefab) as T;
            if (instance == null)
                throw new UnityException($"实例化[{typeof(T)}]对象失败");
        }

        
        /// <summary>
        /// 开始游戏
        /// </summary>
        internal void StartGame() {
            m_gameState = GameState.Playing;            
            //UIManager.instance.HideStartUI();
            m_player.autoShoot = true;
            m_player.SetShootSpeed();
        }


        private void Awake() {

            m_gameState = GameState.Ready;

            CreateRef<Background>(ref m_background, m_backgroundPrefab);
            CreateRef<Boundary>(ref m_boundary, m_boundaryPrefab);
            CreateRef<Player>(ref m_player, m_playerPrefab);

            m_player.transform.localPosition = new Vector3(0, -2.5f);

            m_player.autoShoot = false;
            //m_player.Init(m_background, m_boundary);
            ReadData();
        }


        private void Update() {

            if (m_gameState != GameState.Playing) return;

            m_lastSpawnTime += Time.deltaTime;
            if (m_lastSpawnTime >= m_spawnInterval) {
                SpawnEnermy(m_smallEnermy, m_smallEnermyProp);
                SpawnEnermy(m_bigEnermy, m_bigEnermyProp);
                SpawnEnermy(m_largeEnermy, m_largeEnermyProp);
                m_lastSpawnTime = 0;
            }

        }

        private void SpawnEnermy(Enermy prefab,float spawnProp) {
            if(Random.value < spawnProp) {
                Enermy ins = Instantiate<Enermy>(prefab);
    
                ins.autoShoot = Random.value <= m_autoShootProp;

                ins.canChangeMoveDir = Random.value <= m_changeDirProp;

                ins.SetMoveSpeed(Random.value*1.8f);
                ins.transform.localPosition = m_spwanPoint.localPosition + Vector3.right*Random.Range(-1.5f,1.5f) ;
            }
        }


        // API
        /// <summary>
        /// 重新读取数据
        /// </summary>
        public void ReadData() {
            m_smallEnermyProp = float.Parse(GameSetting.GetSetting("SmallPlaneProp"));
            m_bigEnermyProp = float.Parse(GameSetting.GetSetting("BigPlaneProp"));
            m_largeEnermyProp = float.Parse(GameSetting.GetSetting("LargePlaneProp"));
            m_autoShootProp = float.Parse(GameSetting.GetSetting("AutoShootProp"));
            m_changeDirProp = float.Parse(GameSetting.GetSetting("AutoChangeDirProp"));
        }

        /// <summary>
        /// 外部查询游戏状态
        /// </summary>
        /// <returns><+/returns>
        public bool IsPlaying() {
            return m_gameState == GameState.Playing;
        }

        /// <summary>
        /// 添加得分
        /// </summary>
        public void AddScore(int score) {
            this.m_score += score;
            
            UIManager.instance.playingUI.ShowScore(this.m_score);
        }

        /// <summary>
        /// 暂停游戏
        /// </summary>
        internal void Pause() {
            Time.timeScale = 0f;
            m_gameState = GameState.Pause;
        }

        internal void Resume() {
            Time.timeScale = 1f;
            m_gameState = GameState.Playing;
        }
        internal void ReStart() {
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }


    }
}
