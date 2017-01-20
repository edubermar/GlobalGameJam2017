using System;

public struct Tribool : IEquatable<Tribool>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Enumeraciones
    // ---- ---- ---- ---- ---- ---- ---- ----
    private enum TriboolState { Unknown = 0, True = 1, False = -1 }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private readonly TriboolState state;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public static Tribool True { get { return new Tribool(true); } }
    public static Tribool False { get { return new Tribool(false); } }
    public static Tribool Unknown { get { return new Tribool(); } }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructores
    // ---- ---- ---- ---- ---- ---- ---- ----
    public Tribool(bool state)
    {
        this.state = state ? TriboolState.True : TriboolState.False;
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Operadores sobrecargados
    public static bool operator true(Tribool value)
    {
        return value.state == TriboolState.True;
    }
    
    public static bool operator false(Tribool value)
    {
        return value.state == TriboolState.False;
    }
    
    public static bool operator ==(Tribool x, Tribool y)
    {
        return x.state == y.state;
    }
    
    public static bool operator !=(Tribool x, Tribool y)
    {
        return x.state != y.state;
    }
    
    // Métodos reemplazados
    public override string ToString()
    {
        return state.ToString();
    }
    
    public override bool Equals(object obj)
    {
        return (obj != null && obj is Tribool) ? Equals((Tribool)obj) : false;
    }
    
    public bool Equals(Tribool value)
    {
        return this.state == value.state;
    }
    
    public override int GetHashCode()
    {
        return state.GetHashCode();
    }
    
    // Conversiones
    public static implicit operator Tribool(bool value)
    {
        return new Tribool(value);
    }
    
    public static explicit operator bool(Tribool value)
    {
        switch (value.state)
        {
            case TriboolState.True: return true;
            case TriboolState.False: return false;
            default: throw new InvalidCastException();
        }
    }
    
}