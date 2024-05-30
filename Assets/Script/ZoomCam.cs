using System.Collections;
using UnityEngine;

public class ZoomCam : MonoBehaviour
{
    public float zoomPower = 13f; // Taille orthographique cible lorsqu'on zoome
    public float zoomOutPower = 30f; // Taille orthographique lorsqu'on dézoome
    public float zoomSpeed = 2f; // Vitesse du zoom

    public GameObject planetEnter;
    public Transform bob;
    public Vector3 scalezoomIN = new Vector3(0.65f, 0.65f, 0.65f); // Échelle cible pour Bob en zoom
    public Vector3 scalezoomOut = new Vector3(1f, 1f, 1f); // Échelle cible pour Bob en normal

    private Camera mainCamera;
    private bool isZooming = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void ZoomIn()
    {
        StartCoroutine(ZoomRoutine(zoomPower, scalezoomIN));
        planetEnter.SetActive(true);
    }

    public void ZoomOut()
    {
        StartCoroutine(ZoomRoutine(zoomOutPower, scalezoomOut));
        planetEnter.SetActive(false);
    }

    IEnumerator ZoomRoutine(float targetZoom, Vector3 targetScale)
    {
        isZooming = true;
        float originalZoom = mainCamera.orthographicSize;
        Vector3 originalScale = bob.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Lerp(originalZoom, targetZoom, t);
            bob.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        isZooming = false;
    }
}
