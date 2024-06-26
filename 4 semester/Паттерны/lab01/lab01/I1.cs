using System;
namespace lab01
{
    public interface I1
    {
        int I1_property { get; set; }
        void I1_method();
        event EventHandler I1_event;
        int this[int index] { get; set; }
    }
}