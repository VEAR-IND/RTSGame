using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PersonShowStats : MonoBehaviour
{

    public ArrayList CurrentlySelectedUnits;
    public GameObject g;
    private float fTextLabelHeight = 0;
    public Camera m_Camera;

    void Start()
    {
        m_Camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void TextUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        CurrentlySelectedUnits = GameObject.Find("MouseControllWorld").GetComponent<Mouse_Control>().GetSelectedPersons();
        CreatePersonStatsText();
    }
    private void CreatePersonStatsText()
    {
        if (CurrentlySelectedUnits.Count != 0)
        {
            ArrayList units = new ArrayList();
            foreach (GameObject unit in CurrentlySelectedUnits)
            {
                if (!unit.GetComponent<Person>().isStatsShown)
                {
                    units.Add(unit);
                    unit.GetComponent<Person>().isStatsShown = true;
                }
            }
            foreach (GameObject unit in units)
            {
                PlaceholderInventory pInv = unit.transform.FindChild("Inventory").gameObject.GetComponent<PlaceholderInventory>();
                Stats inventoryStats = pInv.stats;
                var unitClass = unit.GetComponent<Person>();
                unitClass.personStats.ShowStats();
                GameObject g = new GameObject();
                g.name = "TextLable";
                g.gameObject.tag = "TextLable";
                g.gameObject.transform.parent = unit.transform;
                g.transform.position = unit.transform.position;


                Canvas canvas = g.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.WorldSpace;

                CanvasScaler cs = g.AddComponent<CanvasScaler>();
                cs.scaleFactor = 10.0f;
                cs.dynamicPixelsPerUnit = 10f;
                GraphicRaycaster gr = g.AddComponent<GraphicRaycaster>();

                g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
                g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);

                GameObject g2 = new GameObject();
                g2.name = "Text";
                g2.transform.parent = g.transform;
                Text t = g2.AddComponent<Text>();
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
                    t.text = unitClass.personStats.GetStats(inventoryStats: inventoryStats);
                }
                else
                {
                    t.text = unitClass.personStats.GetStats();
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
        }
    }
    public static void DeletePersonStatsText(GameObject ArrayListUnit)
    {
        GameObject[] allLables = GameObject.FindGameObjectsWithTag("TextLable");

        foreach (GameObject g in allLables)
        {
            if (g.transform.IsChildOf(ArrayListUnit.transform))
            {
                ArrayListUnit.GetComponent<Person>().isStatsShown = false;
                g.transform.SetParent(null);
                Destroy(g);
            }
        }
    }

    public void SetText()
    {
        
    }
}
