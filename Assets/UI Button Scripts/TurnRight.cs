using System.Collections;
using UnityEngine;

public class TurnRight : MonoBehaviour
{
    public CameraMovement CamMove;

    public void OnClick()
    {
        CamMove.RightTurn();
    }

}