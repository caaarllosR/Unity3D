using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class UIReceivedGoldEvents : MonoBehaviour
{
    private Text _goldReceivedText;

    // Use this for initialization
    private void Awake()
    {
        _goldReceivedText = GetComponent<Text>();
    }

    private void Start()
    {
        MessageManagerEvents<ReceivedGoldMessageEvents>.Instance.AddListener(OnReceivedGold);
    }

    private void OnDestroy()
    {
        MessageManagerEvents<ReceivedGoldMessageEvents>.Instance.RemoveListener(OnReceivedGold);
    }

    private void OnReceivedGold(ReceivedGoldMessageEvents message)
    {
        _goldReceivedText.text = message.GoldReceivedAmount.ToString();
    }
}
