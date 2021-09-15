using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        Debug.Log("Start button pressed");
        StartCoroutine("Enlarge");
    }

    IEnumerator Enlarge()
    {
        for (float ft = 0.1f; ft <= 1f; ft += 0.05f)
        {
            gameObject.transform.localScale = new Vector3(ft, ft, ft);
            yield return null;
        }
    }
}
