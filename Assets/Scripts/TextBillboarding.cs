using UnityEngine;

public class TextBillboarding : MonoBehaviour {

    void Update() {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0);
    }
}