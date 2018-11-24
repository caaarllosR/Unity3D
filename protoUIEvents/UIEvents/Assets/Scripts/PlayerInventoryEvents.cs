using UnityEngine;

public class PlayerInventoryEvents : MonoBehaviour {

    private int _currentGold;
    private int _receivedGold;

    // Use this for initialization
    public void AddGold(int amount)
    {
        _currentGold += amount;
        _receivedGold += amount;
        UpdateGoldUI();
        UpdateReceivedGoldUI();
    }
    public void RemoveGold(int amount)
    {
        _currentGold -= amount;
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        MessageManagerEvents<UpdateGoldMessageEvents>.Instance.Invoke(new UpdateGoldMessageEvents
        {
            GoldAmount = _currentGold
        });
    }

    public void UpdateReceivedGoldUI()
    {
        MessageManagerEvents<ReceivedGoldMessageEvents>.Instance.Invoke(new ReceivedGoldMessageEvents
        {
            GoldReceivedAmount = _receivedGold
        });
    }
}
