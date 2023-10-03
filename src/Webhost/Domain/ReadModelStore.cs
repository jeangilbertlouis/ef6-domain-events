namespace Webhost.Domain;

public class ReadModelStore
{
    public const string PersonListKey = "PersonList";
    
    public ReadModelStore(string id, string value)
    {
        Id = id;
        Value = value;
    }

    private ReadModelStore()
    {
        
    }

    public string Id { get; private set; }
    public string Value { get; private set; }

    public void Update(string value)
    {
        Value =value;
    }
}