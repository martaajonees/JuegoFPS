using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Text txtHp;
    public Text txtAmmo;
    public static CanvasController instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddTextHp(int vld)
    {
        txtHp.text = "Vida: " + vld.ToString();
    }

    public void AddTextAmmo(int vld)
    {
        txtAmmo.text = "Munición: " + vld.ToString();
    }
}
