using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    private Vector2[] prevTouchPos = new Vector2[2];
    public const float CameraSizeChangeCoefficient = 0.2f;
    private void Update()
    {
        if(Input.touchCount >= 2)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                prevTouchPos[0] = Input.GetTouch(0).position;
                prevTouchPos[1] = Input.GetTouch(1).position;
                return;
            }
            else
            {
                float curTouchDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                float prevTouchDistance = Vector2.Distance(prevTouchPos[0], prevTouchPos[1]);
                float distanceDelta = curTouchDistance - prevTouchDistance;
                float distanceChangeProportion = distanceDelta / prevTouchDistance;
                float cameraSizeChange = Camera.main.orthographicSize * distanceChangeProportion * CameraSizeChangeCoefficient;
                if (distanceDelta > 0)
                {
                    Camera.main.orthographicSize -= cameraSizeChange;
                }
                else if (distanceDelta < 0)
                {
                    Camera.main.orthographicSize += cameraSizeChange;
                }
                prevTouchPos[0] = Input.GetTouch(0).position;
                prevTouchPos[1] = Input.GetTouch(1).position;
            }
        }
    }
}
