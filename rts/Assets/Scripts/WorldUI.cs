using UnityEngine;
using System.Collections;

public class WorldUI : MonoBehaviour {

    [HideInInspector] public GameObject UICamera;

    private Resolution ScreenResolution;
    public LayerMask UICameraLayerMask;
   // public SpriteManager spriteManager;

    public static WorldUI Instance;

    void Start()
    {
        Instance = this;

        //resolution
        Resolution currentResolution = Screen.currentResolution;

        if (Application.isEditor)
            ScreenResolution = Screen.resolutions[0];

        else
            ScreenResolution = currentResolution;

        //Set the resolution
        Screen.SetResolution(ScreenResolution.width, ScreenResolution.height, true);


        //UI Camera setup
        UICamera = new GameObject("UICamera");
      //  UICamera.AddComponent("Camera");

       // Camera uiCamera = UICamera.camera;

        //uiCamera.cullingMask = UICameraLayerMask;
        //uiCamera.name = "UICamera";
        //uiCamera.orthographicSize = ScreenResolution.height / 2;
        //uiCamera.orthographic = true;
        //uiCamera.nearClipPlane = 0.3f;
        //uiCamera.farClipPlane = 40f;
        //uiCamera.clearFlags = CameraClearFlags.Depth;
        //uiCamera.depth = 1;
        //uiCamera.rect = new Rect(0, 0, 1, 1);
        //uiCamera.renderingPath = RenderingPath.UsePlayerSettings;
        //uiCamera.targetTexture = null; //used in UNity Pro
        //uiCamera.hdr = false;
    }
	
	
}
