namespace FunctionBlock;

internal class Variable : IVariable
{
    private IDictionary<string, object> _dictionary;

    public Variable()
    {
        _dictionary = new Dictionary<string, object>();
    }

    public void SetInt(string key, int value)
    {
        _dictionary[key] = value;
    }

    public int GetInt(string key)
    {
        return (int)_dictionary[key];
    }
}