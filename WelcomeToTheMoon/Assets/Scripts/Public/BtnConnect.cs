using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnConnect : MonoBehaviour
{
    public TMP_InputField inputField;
    public void Connect() { 
        string s = inputField.text;
        NetManager.Connect(s);
        SceneManager.LoadScene(1);
    }
}
