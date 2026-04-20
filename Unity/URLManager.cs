using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLManager : MonoBehaviour
{
 public string LinkToOpen = "Url to Open";

    public void OpenURL()
     {
         Application.OpenURL(LinkToOpen);
         Debug.Log("OpenURL");
     }

}
