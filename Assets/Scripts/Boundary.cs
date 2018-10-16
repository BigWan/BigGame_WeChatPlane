using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Boundary : MonoBehaviour {

    private void Awake() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    /// <summary>
    /// 消除子弹
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other) {
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

        Debug.Log(p00);


        Gizmos.color = Color.red;


        Gizmos.DrawLine(p00, p10); 
        Gizmos.DrawLine(p10, p11); 
        Gizmos.DrawLine(p11, p01); 
        Gizmos.DrawLine(p01, p00);

    }
#endif
}
