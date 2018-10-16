using UnityEngine;
using System.Collections;

/// <summary>
/// Transform的各种快捷操作
/// </summary>
public static class TransformUtil { 


    public static void _SetLocalX(this Transform trans,float value) {
        trans.localPosition = new Vector3(value, trans.localPosition.y, trans.localPosition.z);
    }
    public static void _SetLocalY(this Transform trans, float value) {
        trans.localPosition = new Vector3(trans.localPosition.x, value, trans.localPosition.z);
    }
    public static void _SetLocalZ(this Transform trans, float value) {
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y,value);
    }
    public static void _SetX(this Transform trans,float value) {
        trans.position = new Vector3(value, trans.position.y, trans.position.z);
    }
    public static void _SetY(this Transform trans, float value) {
        trans.position = new Vector3(trans.position.x, value, trans.position.z);
    }
    public static void _SetZ(this Transform trans, float value) {
        trans.position = new Vector3(trans.position.x, trans.position.y, value);
    }

    public static void _SetLocalScaleX(this Transform trans, float value) {
    }



}
