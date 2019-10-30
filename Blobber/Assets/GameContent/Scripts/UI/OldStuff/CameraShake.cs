using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(GameObject camera, float duration, float magnitude)
    {
        Vector3 originalPos = camera.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camera.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        camera.transform.localPosition = originalPos;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.transform.position = Vector2.zero;
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        this.gameObject.transform.position = new Vector2(transform.position.x, 0);
    }
}
