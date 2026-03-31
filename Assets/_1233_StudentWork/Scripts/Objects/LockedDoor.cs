using UnityEngine;

public class LockedDoor : MonoBehaviour {
    public void Open(bool _) {
        gameObject.SetActive(false);
    }
}
