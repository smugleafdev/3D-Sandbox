using UnityEngine;

public class TextBillboarding : MonoBehaviour {

    void OnWillRenderObject() {
        transform.LookAt(Camera.current.transform);
        transform.Rotate(0, 180f, 0);
    }
}