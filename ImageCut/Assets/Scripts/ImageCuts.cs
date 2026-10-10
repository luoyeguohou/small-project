using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageCuts : MonoBehaviour
{
     int offsetX;
     int offsetY;
     int offsetXR;
     int offsetYR;
     int gridX;
     int gridY;
    public TMP_InputField offsetXIF;
    public TMP_InputField offsetYIF;
    public TMP_InputField offsetXRIF;
    public TMP_InputField offsetYRIF;
    public TMP_InputField gridXIF;
    public TMP_InputField gridYIF;
    public TMP_Text text;

    public void Execution() {
        StartCoroutine(dealFile());
    }
    public IEnumerator dealFile() {
        try
        {
            offsetX = offsetXIF.text == "" ? 0 : int.Parse(offsetXIF.text);
            offsetY = offsetYIF.text == "" ? 0 : int.Parse(offsetYIF.text);
            offsetXR = offsetXRIF.text == "" ? 0 : int.Parse(offsetXRIF.text);
            offsetYR = offsetYRIF.text == "" ? 0 : int.Parse(offsetYRIF.text);
            gridX = gridXIF.text == "" ? 0 : int.Parse(gridXIF.text);
            gridY = gridYIF.text == "" ? 0 : int.Parse(gridYIF.text);
        }
        catch (Exception e)
        {
            text.text = e.Message;
        }
        // 获取所有图片文件
        List<string> allImages = GetAllImagePath();
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Exports");
            int count = 0;
            int countMax = allImages.Count * gridX * gridY;
            // 切割
            foreach (var filePath in allImages)
            {
                Texture2D texture = LoadImageFromFile(filePath);


                int gridWidth = (texture.width - offsetX - offsetXR) / gridX;
                int gridHeight = (texture.height - offsetY - offsetYR) / gridY;
                for (int i = 0; i < gridX; i++)
                {
                    for (int j = 0; j < gridY; j++)
                    {
                        Rect rect = new Rect(new Vector2(offsetX + i * gridWidth, offsetY + j * gridHeight), new Vector2(gridWidth, gridHeight));
                        Texture2D tex = CropImage(texture, rect);
                        string result = filePath.Replace(".png", (i + j * gridX).ToString() + ".png");
                        Debug.Log(result);
                        Debug.Log(Application.streamingAssetsPath);
                        result = result.Replace(".jpg", (i + j * gridX).ToString() + ".png");
                        result = result.Replace(Application.streamingAssetsPath, Application.streamingAssetsPath + "/Exports");
                        Debug.Log(result);
                        SaveAsPNG(tex, result);
                        text.text = "progress: ("+count.ToString()+"/"+ countMax.ToString() + ")";
                        count++;
                        yield return null;
                    }
                }
            }

            text.text = "finish";

      
    }
    void SaveAsPNG(Texture2D texture, string path)
    {
        // 将Texture2D编码为PNG格式的字节数组
        byte[] pngData = texture.EncodeToPNG();

        // 判断编码是否成功
        if (pngData != null)
        {
            // 将PNG数据写入指定路径的文件
            File.WriteAllBytes(path, pngData);
            Debug.Log("Image saved as PNG at: " + path);
        }
        else
        {
            Debug.LogError("Failed to encode the texture to PNG.");
        }
    }
    // 裁剪图片
    Texture2D CropImage(Texture2D original, Rect cropRect)
    {
        // 获取裁切区域的宽度和高度
        int width = Mathf.FloorToInt(cropRect.width);
        int height = Mathf.FloorToInt(cropRect.height);

        // 获取裁剪区域的像素数据
        Color[] pixels = original.GetPixels(
            Mathf.FloorToInt(cropRect.x),
            Mathf.FloorToInt(cropRect.y),
            width,
            height
        );

        // 创建一个新的纹理，用于存储裁切后的图片
        Texture2D croppedTexture = new Texture2D(width, height);

        // 将获取到的像素数据设置到新的纹理
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();  // 更新纹理数据

        return croppedTexture;
    }

    Texture2D LoadImageFromFile(string path)
    {
        if (File.Exists(path))
        {
            // 读取文件并加载到字节数组
            byte[] fileData = File.ReadAllBytes(path);

            // 创建一个 Texture2D 对象并加载字节数组中的图像
            Texture2D texture = new Texture2D(2, 2);  // 创建一个空纹理（初始化大小可以为 2x2）
            if (texture.LoadImage(fileData))  // 加载图片
            {
                return texture;  // 返回加载的纹理
            }
            else
            {
                Debug.LogError("Failed to load image.");
                return null;
            }
        }
        else
        {
            Debug.LogError("File does not exist at path: " + path);
            return null;
        }
    }
    public List<string> GetAllImagePath() {
        List<string> ret = new List<string>();
        string path = Path.Combine(Application.streamingAssetsPath, "");

        // 确保路径存在
        if (Directory.Exists(path))
        {
            // 获取目录下的所有文件（不包括子文件夹）
            string[] files = Directory.GetFiles(path);

            // 输出所有文件的路径
            foreach (string file in files)
            {
                if (IsPngFile(file) || IsJpgFile(file) )
                {
                    Debug.Log("Found file: " + file);
                    ret.Add(file);
                }
            }
        }
        else
        {
            Debug.LogError("StreamingAssets folder not found at path: " + path);
        }
        return ret;
    }

    public static bool IsPngFile(string filePath)
    {
        // 获取文件扩展名，并转换为小写以忽略大小写差异
        string extension = Path.GetExtension(filePath).ToLower();

        // 判断扩展名是否是 .png
        return extension == ".png";
    }

    public static bool IsJpgFile(string filePath)
    {
        // 获取文件扩展名，并转换为小写以忽略大小写差异
        string extension = Path.GetExtension(filePath).ToLower();

        // 判断扩展名是否是 .png
        return extension == ".jpg";
    }
}
