using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Boundary : MonoBehaviour {



    public float xMin {
        get {
            return m_boxCollider2D.bounds.min.x;
        }
    }

    public float xMax {
        get {
            return m_boxCollider2D.bounds.max.x;
        }
    }

    public float yMin {
        get {
            return m_boxCollider2D.bounds.min.y;
        }
    }

    public float yMax {
        get {
            return m_boxCollider2D.bounds.max.y;
        }
    }

    private BoxCollider2D m_boxCollider2D;

    private void Awake() {
        m_boxCollider2D = GetComponent<BoxCollider2D>();
        m_boxCollider2D.isTrigger = true;

    }





    /// <summary>
    /// 消除子弹
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log(other.name);
        if (other.CompareTag("Bullet")) {
            Destroy(other.gameObject);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos() {
        var col = GetComponent<BoxCollider2D>();
        Vector2 center = col.offset*0.5f;
        Vector2 size = col.size*0.5f;
        Vector3 p00 = new Vector3(center.x - size.x, center.y - size.y);
        Vector3 p10 = new Vector3(center.x + size.x, center.y - size.y);
        Vector3 p01 = new Vector3(center.x - size.x, center.y + size.y);
        Vector3 p11 = new Vector3(center.x + size.x, center.y + size.y);



        Gizmos.color = Color.red;


        Gizmos.DrawLine(p00, p10); 
        Gizmos.DrawLine(p10, p11); 
        Gizmos.DrawLine(p11, p01); 
        Gizmos.DrawLine(p01, p00);

    }


#endif
}
