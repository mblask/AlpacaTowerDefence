using System;

[Serializable]
public class Modifier<T> where T : struct
{
    public int Level;
    public T Value;

    public Modifier() { }

    public Modifier(Modifier<T> modifier)
    {
        this.Level = modifier.Level;
        this.Value = modifier.Value;
    }
}
