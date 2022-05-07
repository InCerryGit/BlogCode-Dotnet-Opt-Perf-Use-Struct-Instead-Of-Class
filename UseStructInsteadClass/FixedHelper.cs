using System.Runtime.CompilerServices;

namespace UseStructInsteadClass;

public static class FixedHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void SetTo(this string str, char* dest)
    {
        fixed (char* ptr = str)
        {
            Unsafe.CopyBlock(dest, ptr, (uint)(Unsafe.SizeOf<char>() * str.Length));
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool SpanEquals(this string str, char* dest, int length)
    {
        return new Span<char>(dest, length).SequenceEqual(str.AsSpan());
    }
}