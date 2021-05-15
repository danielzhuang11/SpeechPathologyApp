using UnityEngine;
using UnityEngine.Android;
using TMPro;

public class RequestPermissionScript : MonoBehaviour
{
    public TextMeshProUGUI logs;

    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user authorized use of the microphone.
            logs.text = "has authorized";
        }
        else
        {
            // We do not have permission to use the microphone.
            // Ask for permission or proceed without the functionality enabled.
            logs.text = "has not authorized";
            Permission.RequestUserPermission(Permission.Microphone);

        }
    }
}