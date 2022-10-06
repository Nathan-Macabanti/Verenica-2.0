using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DebugHPText : MonoBehaviour
{
    [SerializeField] bool isEnemy;
    [SerializeField] Being being;
    TextMeshProUGUI HPText;
    // Start is called before the first frame update
    void Start()
    {
        HPText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (isEnemy)
        {
            EnemyGroup eGroup = EnemyGroup.GetInstance();
            string EnemyName = eGroup.CurrentEnemy.name;
            int enemyHP = eGroup.CurrentEnemy.CurrentHP;
            HPText.text = stringBuilder.Append(EnemyName + " HP: " + enemyHP).ToString();
        }

        if (being == null) return;
        int beingHP = being.CurrentHP;
        HPText.text = stringBuilder.Append(being.name + " HP: " + beingHP).ToString();
    }
}
