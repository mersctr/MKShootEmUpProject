using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}