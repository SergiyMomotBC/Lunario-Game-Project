using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private const float SHAKE_AMOUNT = 0.1f;
    private Camera mainCamera;

    public void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void Shake()
    {
        InvokeRepeating("Shaking", 0.0f, 0.01f);
        Invoke("StopShaking", 0.3f);
    }

    private void Shaking()
    {
        if (SHAKE_AMOUNT > 0.0f)
        {
            var strength = Random.value * SHAKE_AMOUNT * 2 - SHAKE_AMOUNT;
            var camPos = mainCamera.transform.position;
            camPos.x += strength;
            mainCamera.transform.position = camPos;
        }
    }

    private void StopShaking()
    {
        CancelInvoke("Shaking");
    }
}