using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DebugComboText : MonoBehaviour
{
    ComboSystem comboSystem;
    TextMeshProUGUI comboTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        comboSystem = ComboSystem.GetInstance();
        comboTxt = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventManager.OnComboValueChanged += OnComboValueChanged;
    }

    private void OnDisable()
    {
        EventManager.OnComboValueChanged -= OnComboValueChanged;
    }

    public void OnComboValueChanged(uint combo, string letterRank)
    {
        if (combo == 0) comboTxt.text = " ";

        comboTxt.text = "Combo: " + combo.ToString() + " || " + letterRank;
    }
}
