using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryControl : MonoBehaviour
{

    bool On = false;

    public GameObject mouseControl;
    public GameObject cameraControl;

    private Mouse_Control mouseControrScript;
    private WorldCamera worldControl;
    public GameObject invenoryPanel;

    void Start()
    {
        worldControl = cameraControl.GetComponent<WorldCamera>();
        mouseControrScript = mouseControl.GetComponent<Mouse_Control>();
        SetOf();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (On)
            {
                SetOf();
            }
            else
            {
                SetOn();
            }
            On = !On;
        }
    }
    void SetOn()
    {
        mouseControrScript.enabled = false;
        worldControl.enabled = false;
        invenoryPanel.SetActive(true);
    }
    void SetOf()
    {
        invenoryPanel.SetActive(false);
        mouseControrScript.enabled = true;
        worldControl.enabled = true;

    }
}