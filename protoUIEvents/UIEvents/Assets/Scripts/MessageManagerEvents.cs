using UnityEngine.Events;

public class MessageManagerEvents<T> : UnityEvent<T>
{
    private static MessageManagerEvents<T> _instance;

    public static MessageManagerEvents<T> Instance
    {
        get { return _instance ?? (_instance = new MessageManagerEvents<T>()); }
    }

    private MessageManagerEvents()
    {

    }
}
