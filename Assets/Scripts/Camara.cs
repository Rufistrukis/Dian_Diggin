using UnityEngine;

public class Camara : MonoBehaviour
{
public Transform objetivo;
public float velcam = 0.025f;

public Vector3 desplazamiento;

private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + desplazamiento;
        Vector3 posicionSuave = Vector3.Lerp(transform.position, posicionDeseada, velcam);
        transform.position = posicionSuave;
    }
}
