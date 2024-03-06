using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
    public Animator splashAnim;
    public FishingMinigameManager miniMan;
    public float bigSplashDuration;

    private int splashPosition = 0;
    private float bigSplashTimer = 0;

    private int splashPosLastFrame = 0;
    private bool isPulling = false;
    // Left is -1, Middle is 0, Right is 1

    void Start()
    {
        splashAnim.Play("CenterSmall");
    }

    // Update is called once per frame
    void Update()
    {
        splashPosition = miniMan.getButtonToMashAsInt();
        
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && splashPosition == -1)
        {
            bigSplashTimer = bigSplashDuration;
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && splashPosition == 1)
        {
            bigSplashTimer = bigSplashDuration;
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && splashPosition == 0)
        {
            bigSplashTimer = bigSplashDuration;
        }

        bigSplashTimer -= Time.deltaTime;

        if (bigSplashTimer > 0)
            isPulling = true;
        else
            isPulling = false;

        if (splashPosition != splashPosLastFrame)
        {
            PositionUpdate();
        }
        if (isPulling != splashAnim.GetBool("IsPulling"))
        {
            SizeUpdate(isPulling);
        }

        splashPosLastFrame = splashPosition;
    }

    private void PositionUpdate()
    {
        splashAnim.SetInteger("LeftCenterRight", splashPosition);
        bigSplashTimer = 0;
        SizeUpdate(false);
    }

    private void SizeUpdate(bool newPull)
    {
        splashAnim.SetBool("IsPulling", newPull);
    }
}
