using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public abstract class ShopButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] protected int cost;

    private void Awake()
    {
        button = GetComponent<Button>();
        SetText();
        SetInteractability(true);
    }

    private void SetText()
    {
        if(textMesh == null) { return; }
        textMesh.text = "Cost: " + cost.ToString();
    }
    public abstract void OnPress();

    public void SetInteractability(bool isInteractable)
    {
        if (button != null)
            button.interactable = isInteractable;
    }
}
