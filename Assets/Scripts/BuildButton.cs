using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    private Button button;
    private Player player;
    public GameObject building;
    void Start()
    {
        button = GetComponent<Button>();
        player = Camera.main.GetComponent<Player>();
        button.onClick.AddListener(Click);
    }

    public void Click()
    {
        player.toBuild = building;
    }
}
