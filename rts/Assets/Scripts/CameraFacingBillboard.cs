using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera;
    private PlaceholderInventory pInv;
    public delegate void BilbordEventHandler(object sender, EventArgs e);
    public event BilbordEventHandler onCreate;
    public event BilbordEventHandler onEnable;
    public event BilbordEventHandler onDisable;
    GameObject g;
    Stats inventoryStats;
    Person person;
    Text t;
    private float fTextLabelHeight = 0;
    void Start()
    {
        m_Camera = GameObject.Find("Camera").GetComponent<Camera>();
        pInv = gameObject.transform.parent.FindChild("Inventory").GetComponent<PlaceholderInventory>();

        
        person = transform.parent.GetComponent<Person>();
        onCreate += TextCreate;
        onCreate += UpdateText;
        onCreate += (sender, e) => { pInv.onUpdate += UpdateText; };
        if (onCreate != null)
        {
            onCreate(this.gameObject, EventArgs.Empty);
        }
    }
    void Update()
    {
        g.transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }

    void TextCreate(object sender, EventArgs e)
    {
        g = new GameObject();
        g.name = "TextLable";
        g.gameObject.tag = "TextLable";

        g.gameObject.transform.parent = transform.transform;
        g.transform.position = transform.transform.position;
        
        Canvas canvas = g.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        CanvasScaler cs = g.AddComponent<CanvasScaler>();
        cs.scaleFactor = 1.0f;
        cs.dynamicPixelsPerUnit = 10f;
        GraphicRaycaster gr = g.AddComponent<GraphicRaycaster>();

        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);

        GameObject g2 = new GameObject();
        g2.name = "Text";
        g2.transform.parent = g.transform;
        t= g2.AddComponent<Text>();
        g2.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        g2.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
        t.alignment = TextAnchor.MiddleLeft;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        t.verticalOverflow = VerticalWrapMode.Overflow;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        t.font = ArialFont;
        t.fontSize = 4;
        if (inventoryStats != null)
        {
            t.text = person.personStats.GetStats(inventoryStats: inventoryStats);
        }
        else
        {
            t.text = person.personStats.GetStats();
        }
        t.enabled = true;
        t.color = Color.black;

        bool bWorldPosition = false;

        g.transform.localPosition = new Vector3(0f, fTextLabelHeight, 0f);
        g.transform.localScale = new Vector3(
                                                1.0f / this.transform.localScale.x * 0.1f,
                                                1.0f / this.transform.localScale.y * 0.1f,
                                                1.0f / this.transform.localScale.z * 0.1f); 
    }

    void UpdateText(object sender, EventArgs e)
    {
        if (pInv != null)
        {
            inventoryStats = pInv.stats;
        }
        if (inventoryStats != null)
        {
            t.text = person.personStats.GetStats(inventoryStats: inventoryStats);
        }
        else
        {
            t.text = person.personStats.GetStats();
        }
    }

    void OnEnable()
    {
        onEnable += (sender, e) => { if (g != null) { g.gameObject.SetActive(true); } };
        if (onEnable != null)
        {
            onEnable(this.gameObject, EventArgs.Empty);
        }
    }
    void OnDisable()
    {
        onDisable += (sender, e) => { if (g != null) { g.gameObject.SetActive(false); } };
        if (onDisable != null)
        {
            onDisable(this.gameObject, EventArgs.Empty);
        }
    }
}
