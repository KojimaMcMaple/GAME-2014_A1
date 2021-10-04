/*
///-----------------------------------------------------------------
///   Source file name:     GameController.cs
///   Author's name:        Kyle Hunter (Trung Le)
///   Student number:       101264698
///   Date created:         2021-10-03
///   Date last modified:   2021-10-03 (See GitHub)
///   Program description:  Global game manager script
///   Revision History:     
///   20211013 - initial submit
///-----------------------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Rect screen_;
    private Rect safe_area_;

    private RectTransform back_btn_rect_transform_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screen_ = new Rect(0f, 0f, Screen.width, Screen.height);
        safe_area_ = Screen.safeArea;

        CheckOrientation();
    }
    private static void CheckOrientation()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Unknown:
                break;
            case ScreenOrientation.Portrait:
                break;
            case ScreenOrientation.PortraitUpsideDown:
                break;
            case ScreenOrientation.LandscapeLeft:
                break;
            case ScreenOrientation.LandscapeRight:
                break;
            case ScreenOrientation.AutoRotation:
                break;
            default:
                break;
        }
    }
}
