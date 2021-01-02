using UnityEngine;

public class TextBillboarding : MonoBehaviour {

    private void OnWillRenderObject() {
        transform.LookAt(Camera.current.transform);
        transform.Rotate(0, 180f, 0);
    }
}