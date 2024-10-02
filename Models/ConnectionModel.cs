public class ConnectionModel
{
    public string From { get; set; }
    public string To { get; set; }

    public ConnectionModel(string from, string to)
    {
        From = from;
        To = to;
    }
}