/*
///-----------------------------------------------------------------
///   Source file name:     BasicButtonController.cs
///   Author's name:        Kyle Hunter (Trung Le)
///   Student number:       101264698
///   Date created:         2021-10-03
///   Date last modified:   2021-10-03 (See GitHub)
///   Program description:  Load scene commands for basic UI button objects
///   Revision History:     
///   20211013 - initial submit
///-----------------------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicButtonController : MonoBehaviour
{
    public void DoLoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DoLoadNextScene()
    {
        StartCoroutine("Enlarge");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DoLoadPrevScene()
    {
        StartCoroutine("Shrink");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void DoQuitApp()
    {
        StartCoroutine("Shrink");
        Application.Quit();
    }

    IEnumerator Shrink()
    {
        for (float ft = 1f; ft <= 0.01f; ft -= 0.01f)
        {
            gameObject.transform.localScale = new Vector3(ft, ft, ft);
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator Enlarge()
    {
        for (float ft = 0.1f; ft <= 1f; ft += 0.05f)
        {
            gameObject.transform.localScale = new Vector3(ft, ft, ft);
            //yield return null;
            yield return new WaitForSeconds(2.0f);
        }
    }
}
