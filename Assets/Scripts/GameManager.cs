using UnityEngine;
using System.Collections;

namespace BigPlane {

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
        [SerializeField]private float m_smallEnermyProp;

        [SerializeField]private Enermy m_bigEnermy;
        [SerializeField]private float m_bigEnermyProp;

        [SerializeField]private Enermy m_largeEnermy;
        [SerializeField]private float m_largeEnermyProp;

        [SerializeField] private Transform m_spwanPoint;

        [SerializeField]private float m_autoShootProp;
        [SerializeField]private float m_changeDirProp;


        [Header("道具")]

        [Header("其他")]


        /// <summary>
        /// 出兵间隔
        /// </summary>
        [SerializeField] private float m_defaultSpawnInterval = 2f;

        private float m_spawnInterval { get { return m_defaultSpawnInterval-m_level*0.1f; } }
        private float m_lastSpawnTime;
        /// <summary>
        /// 等级
        /// </summary>
        private int m_level;

        private int m_score;


        private Camera m_camera;



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


        private void Awake() {

            m_camera = Camera.main;

            CreateRef<Background>(ref m_background, m_backgroundPrefab);
            CreateRef<Boundary>(ref m_boundary, m_boundaryPrefab);
            CreateRef<Player>(ref m_player, m_playerPrefab);

            //m_player.Init(m_background, m_boundary);

        }


        private void Update() {
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
    
                ins.canAutoShoot = Random.value <= m_autoShootProp;

                ins.canChangeMoveDir = Random.value <= m_changeDirProp;

                ins.transform.localPosition = m_spwanPoint.localPosition + Vector3.right*Random.Range(-2f,2f) ;
            }
        }


    }
}
