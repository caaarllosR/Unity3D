
public class BaseMessageEvents
{

}


public class UpdateGoldMessageEvents : BaseMessageEvents
{
    public int GoldAmount;
}

public class ReceivedGoldMessageEvents : BaseMessageEvents
{
    public int GoldReceivedAmount;
}

