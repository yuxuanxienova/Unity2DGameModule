using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortSiteButton_DeployAitcraft : MonoBehaviour,IUIButton
{
    public AirPort airPort;

    public void ButtonClicked()
    {
        Debug.Log("[Info]PortSiteButton_DeployAitcraft ButtonClicked");
        airPort.DeployAircraft();

    }

}
