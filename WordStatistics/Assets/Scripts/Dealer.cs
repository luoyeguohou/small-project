using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using OfficeOpenXml;
using System.Text.RegularExpressions;

public class Dealer : MonoBehaviour
{
    // 读取文件内容
    void Start()
    {
        List<string> allTxts = GetAllTxtPath();
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Exports");
        foreach (string fileName in allTxts)
        {
            string fileContent = File.ReadAllText(fileName);
            Debug.Log(fileContent);  // 输出文件内容
            var wordCount =  Statistics(fileContent);
            string newFileName = fileName.Replace(Application.streamingAssetsPath, Application.streamingAssetsPath + "/Exports");
            newFileName = newFileName.Replace(".txt",".xlsx");
            using (var package = new ExcelPackage(new FileInfo(newFileName)))
            {
                // 创建一个新的工作表
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // 将数据写入工作表，写入标题
                worksheet.Cells[1, 1].Value = "Word";
                worksheet.Cells[1, 2].Value = "Frequency";

                // 从第二行开始填充数据
                int row = 2;
                foreach (var entry in wordCount)
                {
                    worksheet.Cells[row, 1].Value = entry.Key;  // 单词
                    worksheet.Cells[row, 2].Value = entry.Value;  // 出现频率
                    row++;
                }

                // 保存 Excel 文件
                package.Save();
            }

        }
    }

    public List<string> GetAllTxtPath()
    {
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

        // 判断扩展名是否是 .png
        return extension == ".txt";
    }

    public List<KeyValuePair<string, int>> Statistics(string fileContent) {
        Dictionary<string, int> ret = new Dictionary<string, int>();

        fileContent = Regex.Replace(fileContent, @"-\s*\n\s*", "");
        fileContent = Regex.Replace(fileContent, "-\n", "");
        string[] words = fileContent.ToLower().Split(new[] { ' ', '.', '!', ',', '?', ';', ':', '\n', '\r','"','(',')' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            if (ret.ContainsKey(word))
            {
                ret[word]++;
            }
            else
            {
                ret.Add(word, 1);
            }
        }

        // 3. 使用 LINQ 对单词按频率降序排序
        var sortedWordCount = ret.OrderByDescending(entry => entry.Value).ToList();

        Debug.Log("word number:"+ sortedWordCount.Count);
        foreach (var item in sortedWordCount)
        {
            Debug.Log("words [" +item.Key+ "] appear times: "+item.Value);
        }

        return sortedWordCount;
    }
}

