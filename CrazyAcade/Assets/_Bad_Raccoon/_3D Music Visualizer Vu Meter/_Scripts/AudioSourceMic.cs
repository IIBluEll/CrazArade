using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif


// Here is a small class to understand how use microphone
// It's a start base... You will need to fight with the
// Feedback effect :) :) I'm not enought good coder to do it!
// Thanks to : Yasha Jain for the help :) 
//
// Usefull informations :
// ----------------------
// https://en.wikipedia.org/wiki/Audio_feedback
// https://support.unity3d.com/hc/en-us/articles/206485253-How-do-I-get-Unity-to-playback-a-Microphone-input-in-real-time-
// https://www.mediacollege.com/audio/howto/feedback.html

public class AudioSourceMic : MonoBehaviour
{
    public AudioSource _audiosource;
    public AudioClip _audioClip;
    public bool useMicrophone;

    private string selectedDevice;

    GameObject dialog = null;

    void Start(){
        #if PLATFORM_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
                {
                    Permission.RequestUserPermission(Permission.Microphone);
                    dialog = new GameObject();
                }
        #endif
    }

    // Start is called before the first frame update
    void Awake(){

        // Mic input 
        if (useMicrophone)
        {
            if (Microphone.devices.Length > 0)
            {
                selectedDevice = Microphone.devices[0].ToString();
                _audiosource.clip = Microphone.Start(selectedDevice, true, 10, 44100);
                _audiosource.Play();
            }
        }

        if (!useMicrophone)
        {
            _audiosource.clip = _audioClip;
        }

    }


    void OnGUI(){
        #if PLATFORM_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
                {
                    // The user denied permission to use the microphone.
                    // Display a message explaining why you need it with Yes/No buttons.
                    // If the user says yes then present the request again
                    // Display a dialog here.
                    dialog.AddComponent<PermissionsRationaleDialog>();
                    return;
                }
                else if (dialog != null)
                {
                    Destroy(dialog);
                }
        #endif

        // Now you can do things with the microphone

     
    }

}
