using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]
public class PlayerPauseController : MonoBehaviour
{
    private InputData _inputData;
    public GameObject[] uiObjects; // Reference to the GameObject containing your Canvas
    public float distanceFromHeadset = 1.5f; // Distance from the headset
    private bool wasMenuButtonPressed = false;

    private VideoPlayerManager vpm;

    void Start()
    {
        _inputData = GetComponent<InputData>();
        SetUIObjectState(false); // Initially, hide the UI

        if (_inputData._leftController.isValid)
        {
            Debug.Log("Left controller is valid.");
        }
        else
        {
            Debug.Log("Left controller is not valid.");
        }

        vpm = GameObject.Find("VideoManager").GetComponent<VideoPlayerManager>();
    }

    void Update()
    {
        if (_inputData._leftController.isValid)
        {
            bool isMenuButtonPressed;
            if (_inputData._leftController.TryGetFeatureValue(CommonUsages.menuButton, out isMenuButtonPressed))
            {
                if (isMenuButtonPressed && !wasMenuButtonPressed)
                {
                    // Button was pressed in this frame
                    Debug.Log("Menu button pressed");
                    ToggleUIPosition();
                }

                if (!isMenuButtonPressed && wasMenuButtonPressed)
                {
                    // Button was released in this frame
                    // Optionally, you can add logic here for button release
                }

                wasMenuButtonPressed = isMenuButtonPressed;
            }
        }
    }

    public void ToggleUIPosition()
    {
        // Toggle the visibility of the UI GameObject
        SetUIObjectState(!IsUIObjectActive());

        if (IsUIObjectActive())
        {
            // Move the UI GameObject to be in front of the user's VR headset
            Vector3 headsetPosition = Camera.main.transform.position;
            Vector3 headsetForward = Camera.main.transform.forward;
            foreach(GameObject uiObject in uiObjects) {
                uiObject.transform.position = headsetPosition + distanceFromHeadset * headsetForward;
                uiObject.transform.position = new Vector3(uiObject.transform.position.x, uiObject.transform.position.y - 1.5f, uiObject.transform.position.z);            
                uiObject.transform.rotation = Quaternion.LookRotation(headsetForward);
            }
            vpm.Pause();
        }
        else {
            vpm.Play();
        }
    }

    bool IsUIObjectActive()
    {
        foreach(GameObject uiObject in uiObjects) {
            if(uiObject.activeSelf) {
                return true;
            }
        }
        return false;
    }

    void SetUIObjectState(bool state)
    {
        uiObjects[0].SetActive(state);
    }
}
