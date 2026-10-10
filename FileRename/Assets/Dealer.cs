using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    public string mainPath;
    // Start is called before the first frame update
    void Start()
    {
        Deal();
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
    public void Deal() {
        List<string> allFolders = GetAllFolderPath(mainPath);
        foreach (string folder in allFolders)
        {
           
            List<string> files = GetAllJarPath(mainPath + "/" + folder);
            foreach (string file in files)
            {
                MoveAFile(file, mainPath);
            }
        }
        /*
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Exports");
        foreach (var filePath in allImages)
        {
            Debug.Log(filePath);
            Texture2D texture = LoadImageFromFile(filePath);
            string result = filePath.Replace("120px-", "Exports\\");
            result = result.Replace("180px-", "Exports\\");
            result = result.Replace("100px-", "Exports\\");
            result = result.Replace("400px-", "Exports\\");
            result = result.Replace("240px-", "Exports\\");
            SaveAsPNG(texture, result);
        }
        */
    }


    public void MoveAFile(string sourceFile, string destinationFile) {
        try
        {
            // 检查源文件是否存在
            if (File.Exists(sourceFile))
            {
                // 如果目标目录不存在，则创建它
                string destinationDirectory = Path.GetDirectoryName(destinationFile);
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                // 移动文件
                File.Move(sourceFile, destinationFile);
                Debug.Log("文件移动成功！");
            }
            else
            {
                Debug.Log("源文件不存在！"+ sourceFile);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("移动文件时发生错误: " + ex.Message);
        }
    }
    public List<string> GetAllFolderPath(string path)
    {
        List<string> ret = new List<string>();
        try
        {
            // 获取指定路径下所有子目录的名称
            string[] subDirectories = Directory.GetDirectories(path)
                                               .Select(Path.GetFileName)
                                               .ToArray();

            foreach (string dir in subDirectories)
            {
                Debug.Log("子文件夹名称：" + dir);
                ret.Add(dir);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("发生错误: " + ex.Message);
        }
        return ret;
    }
    public List<string> GetAllJarPath(string path)
    {
        List<string> ret = new List<string>();
        // 确保路径存在
        if (Directory.Exists(path))
        {
            // 获取目录下的所有文件（不包括子文件夹）
            string[] files = Directory.GetFiles(path);

            // 输出所有文件的路径
            foreach (string file in files)
            {
                if (IsJarFile(file))
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
    public List<string> GetAllImagePath(string path)
    {
        List<string> ret = new List<string>();
        // 确保路径存在
        if (Directory.Exists(path))
        {
            // 获取目录下的所有文件（不包括子文件夹）
            string[] files = Directory.GetFiles(path);

            // 输出所有文件的路径
            foreach (string file in files)
            {
                if (IsPngFile(file) || IsJpgFile(file))
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

    public static bool IsJarFile(string filePath)
    {
        // 获取文件扩展名，并转换为小写以忽略大小写差异
        string extension = Path.GetExtension(filePath).ToLower();

        // 判断扩展名是否是 .png
        return extension == ".jar";
    }

    public static bool IsJpgFile(string filePath)
    {
        // 获取文件扩展名，并转换为小写以忽略大小写差异
        string extension = Path.GetExtension(filePath).ToLower();

        // 判断扩展名是否是 .png
        return extension == ".jpg";
    }
}
