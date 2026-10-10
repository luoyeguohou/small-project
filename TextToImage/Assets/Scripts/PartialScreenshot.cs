using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class PartialScreenshot : MonoBehaviour
{
    private Rect screenshotArea = new Rect(0, 0, 744, 1038); // 截图区域（左下角 x, y, 宽, 高）
    public Text text;
    public Text title;
    public List<Image> image;
    public bool useImage;
    public void Trans()
    {
        StartCoroutine(DoTheTrans());
    }

    public void TransTogather()
    {
        StartCoroutine(DoTheTransTogather());
    }

    IEnumerator DoTheTrans()
    {
        List<string> allTextPath = GetAllTextPath();
        foreach (string path in allTextPath)
        {
            string cont = LoadTextFromFile(path);
            string[] words = cont.Split('\n');
            for (int i = 0; i < words.Length; i++)
            {
                string newFilePath = Path.Combine(Application.streamingAssetsPath, "Outputs", GetFileNameByPath(path));
                if (!Directory.Exists(newFilePath))
                {
                    // 创建文件夹
                    Directory.CreateDirectory(newFilePath);
                }
                yield return StartCoroutine(CaptureScreenshot(words[i], newFilePath + "\\" + i + ".png"));
            }
        }
    }

    IEnumerator DoTheTransTogather()
    {
        List<string> allTextPath = GetAllTextPath();
        foreach (string path in allTextPath)
        {
            string cont = LoadTextFromFile(path);
            string[] words = cont.Split('\n');

            Debug.Log(words.Length);
            int length = (int)Mathf.Ceil(Mathf.Sqrt(words.Length));
            Debug.Log(length);
            string newFilePath = Path.Combine(Application.streamingAssetsPath, "Outputs", GetFileNameByPath(path));
            if (!Directory.Exists(newFilePath))
            {
                // 创建文件夹
                Directory.CreateDirectory(newFilePath);
            }


            int pageIndex = 17;
            Texture2D tex = new Texture2D(2481, 3507, TextureFormat.RGB24, false);
            for (int i = 1; i < words.Length; i+=2)
            {
                string titleText = words[i-1];
                string word = words[i];
                //yield return StartCoroutine(CaptureScreenshot(words[i], ));
                Debug.Log(titleText);
                //if (titleText.Contains ("Reference")) { 
                //Debug.Log("???????????");

                //    text.fontSize = 32;
                //}
                    // 创建纹理并读取屏幕像素
                    text.text = word.Replace("@", "\n");
                title.text = titleText;
                if(useImage)
                    image[i / 2].gameObject.SetActive(true);
                yield return new WaitForEndOfFrame();
                int index = (i%18) / 2;
                tex.ReadPixels(screenshotArea, index % 3 * (int)screenshotArea.width, (int)screenshotArea.height * (3 - index / 3 - 1));
                //i%length * (int)screenshotArea.width, (int)screenshotArea.height*(length- i/length)); // 从屏幕上读取指定区域
                Debug.Log(i + " " + words.Length);
                if (i == pageIndex || i == words.Length-1)
                {
                    // save 
                    Debug.Log("save");
                    SaveTex(tex, newFilePath, path, i / 18);
                    tex = new Texture2D(2481, 3507, TextureFormat.RGB24, false);
                    pageIndex += 18;
                }
            }

        }
    }

    void SaveTex(Texture2D tex, string newFilePath, string path,int num) {
        tex.Apply();
        // 编码为PNG
        byte[] bytes = tex.EncodeToPNG();
        string fileName = newFilePath + "\\" + GetFileNameByPath(path)+num.ToString() + ".png";
        string path1 = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(path1, bytes);
        // 释放纹理
        Destroy(tex);
    }

    IEnumerator CaptureScreenshot(string word,string fileName)
    {
        // 等待渲染结束
        yield return new WaitForEndOfFrame();

        // 创建纹理并读取屏幕像素
        text.text = word;
        Texture2D tex = new Texture2D((int)screenshotArea.width, (int)screenshotArea.height, TextureFormat.RGB24, false);
        tex.ReadPixels(screenshotArea, 0, 0); // 从屏幕上读取指定区域
        tex.Apply();
        // 编码为PNG
        byte[] bytes = tex.EncodeToPNG();
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(path, bytes);
        // 释放纹理
        Destroy(tex);
    }

    string LoadTextFromFile(string path)
    {
        if (File.Exists(path))
        {
            string cont = File.ReadAllText(path);
            return cont;
        }
        else
        {
            Debug.LogError("File does not exist at path: " + path);
            return null;
        }
    }

    public string GetFileNameByPath(string path) {
        string[] paths = path.Split("\\");
        string name = paths[paths.Length - 1];
        return name.Split('.')[0];
    }

    public List<string> GetAllTextPath()
    {
        List<string> ret = new List<string>();
        string path = Path.Combine(Application.streamingAssetsPath, "Inputs");

        // 确保路径存在
        if (Directory.Exists(path))
        {
            // 获取目录下的所有文件（不包括子文件夹）
            string[] files = Directory.GetFiles(path);

            // 输出所有文件的路径
            foreach (string file in files)
            {
                if (IsTxtFile(file))
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

    public static bool IsTxtFile(string filePath)
    {
        // 获取文件扩展名，并转换为小写以忽略大小写差异
        string extension = Path.GetExtension(filePath).ToLower();

        // 判断扩展名是否是 .txt
        return extension == ".txt";
    }
}
