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

    // Update is called once per frame
    void Update()
    {
        StringBuilder stringBuilder = new StringBuilder("Combo: ");
        comboTxt.text = stringBuilder.Append(
            comboSystem.CollectedNotes.ToString() + " || " + comboSystem.LetterRank
            ).ToString();
    }
}
