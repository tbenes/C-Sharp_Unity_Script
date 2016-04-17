using UnityEngine;
using System.Collections;
    
    /*Kód pro ovládání kamery pomocí myši nebo ovladače
     *pro ovladač musí být nastaveny vstupní proměné [HorizontalJoy] a [VerticalJoy]
     */

public class ThirdPersonCamera : MonoBehaviour {
    //Promněné pro maximální možné vertikální otočení kamery
    public const float Y_ANGLE_MIN = -30.0f;
    public const float Y_ANGLE_MAX = 50.0f;

    //Proměná typu objekt, která určuje, na jaký objekt se bude kamera soustředit
    public Transform lookAt;
    //Proměná typu objekt, do které se ukládá objekt KAMERY
    public Transform camTransform;

    private Camera cam;
    private float currentX = 0.0f;
    private float currentY = 2.0f;

    public float distance = 10.0f;
    public float GamepadSensitivity = 3.3f;

    private void Start() {
        camTransform = transform;
        cam = Camera.main;
    }

    //Tohle prostě nějak funguje, tak se v tom prosimtě nehrab
    private void Update() {
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");
		currentX += Input.GetAxis("HorizontalJoy")* GamepadSensitivity;
		currentY -= Input.GetAxis("VerticalJoy")* GamepadSensitivity;
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate(){
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        float y = lookAt.position.y;
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
