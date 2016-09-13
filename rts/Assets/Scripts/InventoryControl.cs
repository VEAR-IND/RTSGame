using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryControl : MonoBehaviour
{

    bool isInventoryOn = false;
    bool isControlOn = true;
    bool isPlaceholderOn = false;

    public GameObject mouseControl;
    public GameObject cameraControl;

    private Mouse_Control mouseControrScript;
    private WorldCamera worldControl;
    public GameObject invenoryPanel;
    public GameObject placeholderPanel;

    void Start()
    {
        worldControl = cameraControl.GetComponent<WorldCamera>();
        mouseControrScript = mouseControl.GetComponent<Mouse_Control>();
        SetInventoryOff();
        SetPlaceholderOff();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInventoryOn)
            {
                SetInventoryOff();
            }
            else
            {
                SetInventoryOn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPlaceholderOn)
            {
                SetPlaceholderOff();
            }
            else
            {
                SetPlaceholderOn();
            }
        }
    }
    void SetInventoryOn()
    {
        isInventoryOn = true;
        SetControlOff();
        invenoryPanel.SetActive(true);
    }
    void SetInventoryOff()
    {
        isInventoryOn = false;
        invenoryPanel.SetActive(false);
        SetControlOn();
    }
    void SetPlaceholderOn()
    {
        isPlaceholderOn = true;
        SetControlOff();
        placeholderPanel.SetActive(true);
    }
    void SetPlaceholderOff()
    {
        isPlaceholderOn = false;
        placeholderPanel.SetActive(false);
        SetControlOn();
    }
    void SetControlOn()
    {
        if (!isInventoryOn && !isPlaceholderOn)
        {
            mouseControrScript.enabled = true;
            worldControl.enabled = true;
        }
    }
    void SetControlOff()
    {
        mouseControrScript.enabled = false;
        worldControl.enabled = false;
    }
}