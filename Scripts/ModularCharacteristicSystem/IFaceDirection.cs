using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFaceDirection
{
    void SetFaceDirection(Vector3 directionVector);
    public Vector3 GetEulerAngles();

}
