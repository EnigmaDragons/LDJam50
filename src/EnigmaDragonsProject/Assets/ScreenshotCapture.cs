using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
   [SerializeField] private string screenshotName;
   [SerializeField] private Camera captureCamera;
   
   
   private string ScreenshotParent => Application.persistentDataPath;
   private string ScreenshotFolder = "Screenshots";
   private string ScreenshotPath => Application.persistentDataPath + "/" + ScreenshotFolder;
   
    #if UNITY_EDITOR
          [Button]
          private void OpenScreenshotFolder(){
             EditorUtility.RevealInFinder(ScreenshotPath);
          }
    #endif
       
    [Button(ButtonStyle.FoldoutButton)]
    public string TakeScreenshot(int saveSlot = 0)
    {
       var Cam = captureCamera;

       var tempRT = new RenderTexture(1280, 720, 24, RenderTextureFormat.ARGB32)
       {
          antiAliasing = 4
       };

       Cam.targetTexture = tempRT;

       RenderTexture.active = tempRT;
       Cam.Render();

       var Image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height, TextureFormat.ARGB32, false, true);
       Image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
       Image.Apply();

       var Bytes = Image.EncodeToPNG();
       DestroyImmediate(Image);


       if (!Directory.Exists(ScreenshotFolder))
       {
          Directory.CreateDirectory(ScreenshotPath);
       }

       var path = ScreenshotPath + "/" + screenshotName + ".png";
       File.WriteAllBytes(path, Bytes);
       return path;
    }
 }
