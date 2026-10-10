using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIconButton : MonoBehaviour
{
    public Sprite[] icons;
    public int iconIndex { get; set;  }
    public Image icon;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        icon.sprite = icons[0];
        iconIndex = 0;
    }
    void OnClick()
    {
        iconIndex = (iconIndex + 1)%icons.Length;
        icon.sprite = icons[iconIndex];
    }
}
