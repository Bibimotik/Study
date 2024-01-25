using System;

public partial class OperationSet : Software
{
    private string Type;

    public OperationSet(string name, string type) : base(name, "")
    {
        Type = type;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing operation set: {Name}");
    }
}