using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class UIGoldEvents : MonoBehaviour {

    private Text _goldText;

    // Use this for initialization
    private void Awake()
    {
        _goldText = GetComponent<Text>();

    }

    private void Start()
    {
        MessageManagerEvents<UpdateGoldMessageEvents>.Instance.AddListener(OnUpdateGold);
    }

    private void OnDestroy()
    {
        MessageManagerEvents<UpdateGoldMessageEvents>.Instance.RemoveListener(OnUpdateGold);
    }

    private void OnUpdateGold(UpdateGoldMessageEvents message)
    {
        _goldText.text = message.GoldAmount.ToString();  
    }
}
