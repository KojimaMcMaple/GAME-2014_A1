using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  The Source file name: GameManager.cs
///  Author's name: Trung Le (Kyle Hunter)
///  Student Number: 101264698
///  Program description: Global game manager script
///  Date last Modified: See GitHub
///  Revision History: See GitHub
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider ui_hp_bar_;
    [SerializeField] private Text ui_score_;
    private int score_ = 0;

    // LAB1
    private Rect screen_;
    private Rect safe_area_;
    private RectTransform back_btn_rect_transform_;

    void Awake()
    {
        SetUIScoreValue(score_);
    }

    void Update()
    {
        
        //// LAB1
        //screen_ = new Rect(0f, 0f, Screen.width, Screen.height);
        //safe_area_ = Screen.safeArea;
        //CheckOrientation();
    }

    public void SetUIHPBarValue(float value)
    {
        ui_hp_bar_.value = value;
    }

    public void IncrementScore(int value)
    {
        score_ += value;
        SetUIScoreValue(score_);
    }

    public void SetUIScoreValue(int value)
    {
        ui_score_.text = ("Score " + value).ToString();
    }

    /// <summary>
    /// LAB 1
    /// </summary>
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
