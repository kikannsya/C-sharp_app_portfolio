using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ComplexD = InoueLab.Complex<System.Double>;
using ComplexS = InoueLab.Complex<System.Single>;
//プロジェクトのビルド設定でunsafe codeを許可

#pragma warning disable CA1034
#pragma warning disable CA1043
#pragma warning disable CA1062
#pragma warning disable CA1303
#pragma warning disable CA1305
#pragma warning disable CA1720
#pragma warning disable CA1814
#pragma warning disable CA1815

#pragma warning disable CA1000
#pragma warning disable CA1051
#pragma warning disable CA1707
#pragma warning disable CA1715
#pragma warning disable CA2225

namespace System.Linq
{
    using InoueLab;

    #region competing System.Linq.Enumerable
    //System.Linq.Enumerable と競合
    public static class EnumerableEx
    {
        public static T Sum<T>(this IEnumerable<T> source) where T : unmanaged => Mt.SumFwrd<T>(source);
        public static T Avg<T>(this IEnumerable<T> source) where T : unmanaged => Mt.AvgFwrd<T>(source);
        public static T Average<T>(this IEnumerable<T> source) where T : unmanaged => Mt.AvgFwrd<T>(source);
        public static T Sum<TS, T>(this IEnumerable<TS> source, Func<TS, T> selector) where T : unmanaged => source.Select(selector).Sum();
        public static T Avg<TS, T>(this IEnumerable<TS> source, Func<TS, T> selector) where T : unmanaged => source.Select(selector).Avg();
        public static T Average<TS, T>(this IEnumerable<TS> source, Func<TS, T> selector) where T : unmanaged => source.Select(selector).Avg();

        public static T[] Sum<T>(this IEnumerable<T[]> source) where T : unmanaged => Mt.SumFwrd(source);
        public static T[,] Sum<T>(this IEnumerable<T[,]> source) where T : unmanaged => Mt.SumFwrd(source);
        public static T[,,] Sum<T>(this IEnumerable<T[,,]> source) where T : unmanaged => Mt.SumFwrd(source);
        public static T[] Avg<T>(this IEnumerable<T[]> source) where T : unmanaged => Mt.AvgFwrd(source);
        public static T[,] Avg<T>(this IEnumerable<T[,]> source) where T : unmanaged => Mt.AvgFwrd(source);
        public static T[,,] Avg<T>(this IEnumerable<T[,,]> source) where T : unmanaged => Mt.AvgFwrd(source);
        public static T[] Average<T>(this IEnumerable<T[]> source) where T : unmanaged => Mt.AvgFwrd(source);
        public static T[,] Average<T>(this IEnumerable<T[,]> source) where T : unmanaged => Mt.AvgFwrd(source);
        public static T[,,] Average<T>(this IEnumerable<T[,,]> source) where T : unmanaged => Mt.AvgFwrd(source);
        public static T[] Sum<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => source.Select(selector).Sum();
        public static T[,] Sum<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => source.Select(selector).Sum();
        public static T[,,] Sum<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => source.Select(selector).Sum();
        public static T[] Avg<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => source.Select(selector).Avg();
        public static T[,] Avg<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => source.Select(selector).Avg();
        public static T[,,] Avg<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => source.Select(selector).Avg();
        public static T[] Average<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => source.Select(selector).Avg();
        public static T[,] Average<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => source.Select(selector).Avg();
        public static T[,,] Average<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => source.Select(selector).Avg();
    }
    #endregion
}

namespace InoueLab
{
    #region Error class
    public static class ThrowException
    {
        internal static string ToString(string message, string file, int line, string member) => $"{Path.GetFileName(file)}:{line}: {member}: {message}";
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void NotImplemented([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new NotImplementedException(ToString("", file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T NotImplemented<T>([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new NotImplementedException(ToString("", file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Argument(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new ArgumentException(ToString(message, file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T Argument<T>(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new ArgumentException(ToString(message, file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ArgumentNull(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new ArgumentNullException(ToString(message, file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T ArgumentNull<T>(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new ArgumentNullException(ToString(message, file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ArgumentOutOfRange(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new ArgumentOutOfRangeException(ToString(message, file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T ArgumentOutOfRange<T>(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new ArgumentOutOfRangeException(ToString(message, file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void InvalidOperation(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            throw new InvalidOperationException(ToString(message, file, line, member));
        }
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SizeMismatch([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "") => InvalidOperation("size mismatch", file, line, member);
    }

    public static class Warning
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Singular([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "") => WriteLine("singular", file, line, member);
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void TooManyIterations([CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "") => WriteLine("too many iterations", file, line, member);
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void WriteLine(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            Console.Error.WriteLine(ThrowException.ToString(message, file, line, member));
        }
    }

    public static class Debug
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Assert(bool condition, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            if (!condition) Console.Error.WriteLine(ThrowException.ToString(message, file, line, member));
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void AssertIndex(int index, int count, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, [CallerMemberName] string member = "")
        {
            if ((uint)index >= (uint)count) ThrowException.ArgumentOutOfRange($"index = {index}, count = {count}", file, line, member);
        }
    }
    #endregion

    #region Complex<T>
    [Serializable]
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Complex<T> : IEquatable<Complex<T>> where T : unmanaged
    {
        public T Re;
        public T Im;
        private static T LOG_10_INV => Op<T>.From(0.43429448190325);
        public T Real => Re;
        public T Imaginary => Im;
        public T Magnitude => Op.BAbs(this);
        public T Phase => Op.Atan2(Im, Re);
        public static Complex<T> Zero => default;
        public static Complex<T> One => new Complex<T>(1, 0);
        public static Complex<T> ImaginaryOne => new Complex<T>(0, 1);
        public static Complex<T> I => new Complex<T>(0, 1);
        public static Complex<T> NegI => new Complex<T>(0, -1);
        public static Complex<T> Two => new Complex<T>(2, 0);
        public static Complex<T> NaN => new Complex<T>(Op<T>.NaN, Op<T>.NaN);
        public static Complex<T> PositiveInfinity => new Complex<T>(Op<T>.PositiveInfinity, Op<T>.PositiveInfinity);
        public static Complex<T> NegativeInfinity => new Complex<T>(Op<T>.NegativeInfinity, Op<T>.NegativeInfinity);
        private Complex(int real, int imaginary) { Op.LetCast(out Re, real); Op.LetCast(out Im, imaginary); }
        public Complex(T real) { Re = real; Im = default; }
        public Complex(T real, T imaginary) { Re = real; Im = imaginary; }

        public static implicit operator Complex<T>(Int32 value) { Op.LetCast(out Complex<T> z, value); return z; }
        public static implicit operator Complex<T>(Int64 value) { Op.LetCast(out Complex<T> z, value); return z; }
        public static implicit operator Complex<T>(Single value) { Op.LetCast(out Complex<T> z, value); return z; }
        public static implicit operator Complex<T>(Double value) { Op.LetCast(out Complex<T> z, value); return z; }
        public static explicit operator Complex<T>(Decimal value) { Op.LetCast(out Complex<T> z, value); return z; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static explicit operator Complex<T>(Complex<float> value) { Op.LetCast(out Complex<T> z, value); return z; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static explicit operator Complex<T>(Complex<double> value) { Op.LetCast(out Complex<T> z, value); return z; }

        public static Complex<T> FromPolarCoordinates(T magnitude, T phase)
        {
            return new Complex<T>(Op.Mul(magnitude, Op.Cos(phase)), Op.Mul(magnitude, Op.Sin(phase)));
        }

        public static Complex<T> Negate(Complex<T> x) => -x;
        public static Complex<T> Add(Complex<T> x, Complex<T> y) => x + y;
        public static Complex<T> Subtract(Complex<T> x, Complex<T> y) => x - y;
        public static Complex<T> Multiply(Complex<T> x, Complex<T> y) => x * y;
        public static Complex<T> Divide(Complex<T> x, Complex<T> y) => x / y;

        public static Complex<T> operator -(Complex<T> value) { Op.Neg(out var a, value); return a; }
        public static Complex<T> operator +(Complex<T> x, Complex<T> y) { Op.Add(out var a, x, y); return a; }
        public static Complex<T> operator -(Complex<T> x, Complex<T> y) { Op.Sub(out var a, x, y); return a; }
        public static Complex<T> operator *(Complex<T> x, Complex<T> y) { Op.Mul(out var a, x, y); return a; }
        public static Complex<T> operator /(Complex<T> x, Complex<T> y) { Op.Div(out var a, x, y); return a; }
        public static Complex<T> operator *(Complex<T> x, T y) { Op.MulB(out var a, x, y); return a; }
        public static Complex<T> operator /(Complex<T> x, T y) { Op.DivB(out var a, x, y); return a; }
        public static Complex<T> operator %(Complex<T> x, T y) => new Complex<T>(Op.Mod(x.Re, y), Op.Mod(x.Im, y));

        public static T Abs(Complex<T> x) => Op.BAbs(x);
        public static Complex<T> Conjugate(Complex<T> x) { Op.Cnj(out var a, x); return a; }
        public static Complex<T> Reciprocal(Complex<T> x)
        {
            if (Op.Equ(x.Re, 0) && Op.Equ(x.Im, 0)) return NaN;
            return Op<T>.One / x;
        }

        public static bool operator ==(Complex<T> x, Complex<T> y) { Op.Equ(out bool a, x, y); return a; }
        public static bool operator !=(Complex<T> x, Complex<T> y) { Op.Neq(out bool a, x, y); return a; }
        public static bool operator ==(Complex<T> x, T y) { Op.EquB(out bool a, x, y); return a; }
        public static bool operator !=(Complex<T> x, T y) { Op.NeqB(out bool a, x, y); return a; }

        public override bool Equals(object? obj) => obj is Complex<T> value && this == value;
        public bool Equals(Complex<T> value) => ((this.Re.Equals(value.Re)) && (this.Im.Equals(value.Im)));

        public override string ToString() => string.Format(CultureInfo.CurrentCulture, "({0}, {1})", Re, Im);
        public string ToString(string format) => string.Format(CultureInfo.CurrentCulture, $"({{0:{format}}}, {{1:{format}}})", Re, Im);
        public string ToString(IFormatProvider provider) => string.Format(provider, "({0}, {1})", Re, Im);
        public string ToString(string format, IFormatProvider provider) => string.Format(provider, $"({{0:{format}}}, {{1:{format}}})", Re, Im);
        public override Int32 GetHashCode()
        {
            return (Re.GetHashCode() % 99999997) ^ Im.GetHashCode();
        }

        public static Complex<T> Sin(Complex<T> x) => new Complex<T>(Op.Mul(Op.Sin(x.Re), Op.Cosh(x.Im)), Op.Mul(Op.Cos(x.Re), Op.Sinh(x.Im)));
        public static Complex<T> Sinh(Complex<T> x) => new Complex<T>(Op.Mul(Op.Sinh(x.Re), Op.Cos(x.Im)), Op.Mul(Op.Cosh(x.Re), Op.Sin(x.Im)));
        public static Complex<T> Asin(Complex<T> x) => (-I) * Log(I * x + Sqrt(One - x * x));
        public static Complex<T> Cos(Complex<T> x) => new Complex<T>(Op.Mul(Op.Cos(x.Re), Op.Cosh(x.Im)), Op.Neg(Op.Mul(Op.Sin(x.Re), Op.Sinh(x.Im))));
        public static Complex<T> Cosh(Complex<T> x) => new Complex<T>(Op.Mul(Op.Cosh(x.Re), Op.Cos(x.Im)), Op.Mul(Op.Sinh(x.Re), Op.Sin(x.Im)));
        public static Complex<T> Acos(Complex<T> x) => (-I) * Log(x + I * Sqrt(One - (x * x)));
        public static Complex<T> Tan(Complex<T> x) => Sin(x) / Cos(x);
        public static Complex<T> Tanh(Complex<T> x) => Sinh(x) / Cosh(x);
        public static Complex<T> Atan(Complex<T> x) => (I / Two) * (Log(One - I * x) - Log(One + I * x));
        public static Complex<T> Log(Complex<T> x) => new Complex<T>((Op.Log(Abs(x))), (Op.Atan2(x.Im, x.Re)));
        public static Complex<T> Log(Complex<T> x, T baseValue) => Log(x) / Op.Log(baseValue);
        public static Complex<T> Log10(Complex<T> x) => Log(x) * LOG_10_INV;
        public static Complex<T> Exp(Complex<T> x) => FromPolarCoordinates(Op.Exp(x.Re), x.Im);
        public static Complex<T> Sqrt(Complex<T> x) => FromPolarCoordinates(Op.Sqrt(x.Magnitude), Op.Div(x.Phase, Op<T>.Two));
        public static Complex<T> Pow(Complex<T> x, Complex<T> y)
        {
            if (Op.Equ(y, 0)) return One;
            if (Op.Equ(x, 0)) return Zero;
            T a = x.Re;
            T b = x.Im;
            T c = y.Re;
            T d = y.Im;
            T rho = Abs(x);
            T theta = Op.Atan2(b, a);
            T newRho = Op.Add(Op.Mul(c, theta), Op.Mul(d, Op.Log(rho)));
            T t = Op.Mul(Op.Pow(rho, c), Op.Exp(Op.Mul(Op.Neg(d), theta)));
            return new Complex<T>(Op.Mul(t, Op.Cos(newRho)), Op.Mul(t, Op.Sin(newRho)));
        }
        public static Complex<T> Pow(Complex<T> x, T y) => Pow(x, new Complex<T>(y));

        //additional functions
        public static Complex<T> operator ~(Complex<T> x) { Op.Cnj(out var a, x); return a; }
        public static Complex<T> operator +(Complex<T> x, T y) { Op.AddB(out var a, x, y); return a; }
        public static Complex<T> operator -(Complex<T> x, T y) { Op.SubB(out var a, x, y); return a; }
        public static Complex<T> operator +(T x, Complex<T> y) => y + x;
        public static Complex<T> operator -(T x, Complex<T> y) { Op.SubrB(out var a, y, x); return a; }
        public static Complex<T> operator *(T x, Complex<T> y) => y * x;
        public static Complex<T> operator /(T x, Complex<T> y) { Op.DivrB(out var a, y, x); return a; }
        public Complex<T> Cnj() { Op.Cnj(out var a, this); return a; }
        public T Abs() => Op.BAbs(this);
        public T AbsSq() => Op.BAbsSq(this);
        public Double DAbs() => Op.DAbs(this);
        public Double DAbsSq() => Op.DAbsSq(this);
        public Complex<T> Sign() { var r = Abs(); return Op.Equ(r, 0) ? Zero : this / r; }
        public static Complex<T> Cul(Complex<T> x, Complex<T> y) { Op.Cul(out var a, x, y); return a; }
    }
    #endregion

    #region Int2, Int3, Float2, Float3, Double2, Double3
    #region int
    [Serializable]
    public partial struct Int2 : IEquatable<Int2>, IComparable<Int2>, IFormattable
    {
        public int v0, v1;
        public int X => v0;
        public int Y => v1;
        public int this[int index]
        {
            get { Debug.AssertIndex(index, 2); return (index == 0 ? v0 : v1); }
            set { Debug.AssertIndex(index, 2); (index == 0 ? ref v0 : ref v1) = value; }
        }
        public Int2(int v0, int v1) { this.v0 = v0; this.v1 = v1; }
        public void Deconstruct(out int v0, out int v1) { v0 = this.v0; v1 = this.v1; }

        public static explicit operator Int3(Int2 x) => new Int3(x.v0, x.v1, 0);
        public static explicit operator Double2(Int2 x) => new Double2(x.v0, x.v1);
        public static explicit operator Double3(Int2 x) => new Double3(x.v0, x.v1, 0);
        public static bool operator ==(Int2 x, Int2 y) => ((x.v0 - y.v0) | (x.v1 - y.v1)) == 0;
        public static bool operator !=(Int2 x, Int2 y) => ((x.v0 - y.v0) | (x.v1 - y.v1)) != 0;
        public static bool operator <=(Int2 x, Int2 y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Int2 x, Int2 y) => x.CompareTo(y) >= 0;
        public static bool operator <(Int2 x, Int2 y) => x.CompareTo(y) < 0;
        public static bool operator >(Int2 x, Int2 y) => x.CompareTo(y) > 0;
        public static Int2 operator ~(Int2 x) => new Int2(~x.v0, ~x.v1);
        public static Int2 operator +(Int2 x) => new Int2(+x.v0, +x.v1);
        public static Int2 operator -(Int2 x) => new Int2(-x.v0, -x.v1);
        public static Int2 operator +(Int2 x, Int2 y) => new Int2(x.v0 + y.v0, x.v1 + y.v1);
        public static Int2 operator -(Int2 x, Int2 y) => new Int2(x.v0 - y.v0, x.v1 - y.v1);
        public static Int2 operator *(Int2 x, Int2 y) => new Int2(x.v0 * y.v0, x.v1 * y.v1);
        public static Int2 operator /(Int2 x, Int2 y) => new Int2(x.v0 / y.v0, x.v1 / y.v1);
        public static Int2 operator %(Int2 x, Int2 y) => new Int2(x.v0 % y.v0, x.v1 % y.v1);
        public static Int2 operator |(Int2 x, Int2 y) => new Int2(x.v0 | y.v0, x.v1 | y.v1);
        public static Int2 operator &(Int2 x, Int2 y) => new Int2(x.v0 & y.v0, x.v1 & y.v1);
        public static Int2 operator ^(Int2 x, Int2 y) => new Int2(x.v0 ^ y.v0, x.v1 ^ y.v1);
        public static Int2 operator |(Int2 x, int y) => new Int2(x.v0 | y, x.v1 | y);
        public static Int2 operator &(Int2 x, int y) => new Int2(x.v0 & y, x.v1 & y);
        public static Int2 operator ^(Int2 x, int y) => new Int2(x.v0 ^ y, x.v1 ^ y);
        public static Int2 operator +(Int2 x, int y) => new Int2(x.v0 + y, x.v1 + y);
        public static Int2 operator -(Int2 x, int y) => new Int2(x.v0 - y, x.v1 - y);
        public static Int2 operator *(Int2 x, int y) => new Int2(x.v0 * y, x.v1 * y);
        public static Int2 operator /(Int2 x, int y) => new Int2(x.v0 / y, x.v1 / y);
        public static Int2 operator %(Int2 x, int y) => new Int2(x.v0 % y, x.v1 % y);
        public int MinIndex() => v1 < v0 ? 1 : 0;
        public int MaxIndex() => v0 < v1 ? 1 : 0;
        public override bool Equals(object? obj) => (obj is Int2 o) && (this == o);
        public override int GetHashCode() => Ex.CombineHashCodes(v0.GetHashCode(), v1.GetHashCode());
        public override string ToString() => $"{v0}, {v1}";
        public string ToString(string format) => $"{v0.ToString(format)}, {v1.ToString(format)}";
        public string ToString(IFormatProvider provider) => string.Format(provider, "({0}, {1})", v0, v1);
        public string ToString(string? format, IFormatProvider? formatProvider) => string.Format(formatProvider, "({0}, {1})", v0.ToString(format, formatProvider), v1.ToString(format, formatProvider));
        public bool Equals(Int2 other) => ((v0 - other.v0) | (v1 - other.v1)) == 0;
        public int CompareTo(Int2 other) => v1 != other.v1 ? v1 - other.v1 : v0 - other.v0;
    }

    [Serializable]
    public partial struct Int3 : IEquatable<Int3>, IComparable<Int3>, IFormattable
    {
        public int v0, v1, v2;
        public int X => v0;
        public int Y => v1;
        public int Z => v2;
        public int this[int index]
        {
            get { Debug.AssertIndex(index, 3); return (index == 0 ? v0 : index == 1 ? v1 : v2); }
            set { Debug.AssertIndex(index, 3); (index == 0 ? ref v0 : ref (index == 1 ? ref v1 : ref v2)) = value; }
        }
        public Int3(int v0, int v1, int v2) { this.v0 = v0; this.v1 = v1; this.v2 = v2; }
        public void Deconstruct(out int v0, out int v1, out int v2) { v0 = this.v0; v1 = this.v1; v2 = this.v2; }

        public static explicit operator Int2(Int3 x) => new Int2(x.v0, x.v1);
        public static explicit operator Double2(Int3 x) => new Double2(x.v0, x.v1);
        public static explicit operator Double3(Int3 x) => new Double3(x.v0, x.v1, x.v2);
        public static bool operator ==(Int3 x, Int3 y) => ((x.v0 - y.v0) | (x.v1 - y.v1) | (x.v2 - y.v2)) == 0;
        public static bool operator !=(Int3 x, Int3 y) => ((x.v0 - y.v0) | (x.v1 - y.v1) | (x.v2 - y.v2)) != 0;
        public static bool operator <=(Int3 x, Int3 y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Int3 x, Int3 y) => x.CompareTo(y) >= 0;
        public static bool operator <(Int3 x, Int3 y) => x.CompareTo(y) < 0;
        public static bool operator >(Int3 x, Int3 y) => x.CompareTo(y) > 0;
        public static Int3 operator ~(Int3 x) => new Int3(~x.v0, ~x.v1, ~x.v2);
        public static Int3 operator +(Int3 x) => new Int3(+x.v0, +x.v1, +x.v2);
        public static Int3 operator -(Int3 x) => new Int3(-x.v0, -x.v1, -x.v2);
        public static Int3 operator +(Int3 x, Int3 y) => new Int3(x.v0 + y.v0, x.v1 + y.v1, x.v2 + y.v2);
        public static Int3 operator -(Int3 x, Int3 y) => new Int3(x.v0 - y.v0, x.v1 - y.v1, x.v2 - y.v2);
        public static Int3 operator *(Int3 x, Int3 y) => new Int3(x.v0 * y.v0, x.v1 * y.v1, x.v2 * y.v2);
        public static Int3 operator /(Int3 x, Int3 y) => new Int3(x.v0 / y.v0, x.v1 / y.v1, x.v2 / y.v2);
        public static Int3 operator %(Int3 x, Int3 y) => new Int3(x.v0 % y.v0, x.v1 % y.v1, x.v2 % y.v2);
        public static Int3 operator |(Int3 x, Int3 y) => new Int3(x.v0 | y.v0, x.v1 | y.v1, x.v2 | y.v2);
        public static Int3 operator &(Int3 x, Int3 y) => new Int3(x.v0 & y.v0, x.v1 & y.v1, x.v2 & y.v2);
        public static Int3 operator ^(Int3 x, Int3 y) => new Int3(x.v0 ^ y.v0, x.v1 ^ y.v1, x.v2 ^ y.v2);
        public static Int3 operator |(Int3 x, int y) => new Int3(x.v0 | y, x.v1 | y, x.v2 | y);
        public static Int3 operator &(Int3 x, int y) => new Int3(x.v0 & y, x.v1 & y, x.v2 & y);
        public static Int3 operator ^(Int3 x, int y) => new Int3(x.v0 ^ y, x.v1 ^ y, x.v2 ^ y);
        public static Int3 operator +(Int3 x, int y) => new Int3(x.v0 + y, x.v1 + y, x.v2 + y);
        public static Int3 operator -(Int3 x, int y) => new Int3(x.v0 - y, x.v1 - y, x.v2 - y);
        public static Int3 operator *(Int3 x, int y) => new Int3(x.v0 * y, x.v1 * y, x.v2 * y);
        public static Int3 operator /(Int3 x, int y) => new Int3(x.v0 / y, x.v1 / y, x.v2 / y);
        public static Int3 operator %(Int3 x, int y) => new Int3(x.v0 % y, x.v1 % y, x.v2 % y);
        public int MinIndex() => v1 < v0 ? (v2 < v1 ? 2 : 1) : (v2 < v0 ? 2 : 0);
        public int MaxIndex() => v0 < v1 ? (v1 < v2 ? 2 : 1) : (v0 < v2 ? 2 : 0);
        public override bool Equals(object? obj) => (obj is Int3 o) && (this == o);
        public override int GetHashCode() => Ex.CombineHashCodes(v0.GetHashCode(), v1.GetHashCode(), v2.GetHashCode());
        public override string ToString() => $"{v0}, {v1}, {v2}";
        public string ToString(string format) => $"{v0.ToString(format)}, {v1.ToString(format)}, {v2.ToString(format)}";
        public string ToString(IFormatProvider provider) => string.Format(provider, "({0}, {1}, {2})", v0, v1, v2);
        public string ToString(string? format, IFormatProvider? formatProvider) => string.Format(formatProvider, "({0}, {1}, {2})", v0.ToString(format, formatProvider), v1.ToString(format, formatProvider), v2.ToString(format, formatProvider));
        public bool Equals(Int3 other) => ((v0 - other.v0) | (v1 - other.v1) | (v2 - other.v2)) == 0;
        public int CompareTo(Int3 other) => v2 != other.v2 ? v2 - other.v2 : (v1 != other.v1 ? v1 - other.v1 : v0 - other.v0);
    }
    #endregion

    #region float
    [Serializable]
    public partial struct Single2 : IEquatable<Single2>, IComparable<Single2>, IFormattable
    {
        public static Single2 Zero => default;
        public static readonly Single2 One = new Single2(1);
        public static readonly Single2 PositiveInfinity = new Single2(float.PositiveInfinity);
        public static readonly Single2 NegativeInfinity = new Single2(float.NegativeInfinity);
        public static readonly Single2 NaN = new Single2(float.NaN);
        public float v0, v1;
        public float X => v0;
        public float Y => v1;
        public float this[int index]
        {
            get { Debug.AssertIndex(index, 2); return (index == 0 ? v0 : v1); }
            set { Debug.AssertIndex(index, 2); (index == 0 ? ref v0 : ref v1) = value; }
        }
        private Single2(float v) { v0 = v1 = v; }
        public Single2(float v0, float v1) { this.v0 = v0; this.v1 = v1; }
        public void Deconstruct(out float v0, out float v1) { v0 = this.v0; v1 = this.v1; }

        public static explicit operator Int2(Single2 x) => new Int2((int)x.v0, (int)x.v1);
        public static explicit operator Int3(Single2 x) => new Int3((int)x.v0, (int)x.v1, 0);
        public static explicit operator Single3(Single2 x) => new Single3(x.v0, x.v1, 0);
        public static explicit operator Double2(Single2 x) => new Double2(x.v0, x.v1);
        public static explicit operator Double3(Single2 x) => new Double3(x.v0, x.v1, 0);
        public static bool operator ==(Single2 x, Single2 y) => (x.v0 == y.v0) && (x.v1 == y.v1);
        public static bool operator !=(Single2 x, Single2 y) => (x.v0 != y.v0) || (x.v1 != y.v1);
        public static bool operator <=(Single2 x, Single2 y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Single2 x, Single2 y) => x.CompareTo(y) >= 0;
        public static bool operator <(Single2 x, Single2 y) => x.CompareTo(y) < 0;
        public static bool operator >(Single2 x, Single2 y) => x.CompareTo(y) > 0;
        public static Single2 operator +(Single2 x) => new Single2(+x.v0, +x.v1);
        public static Single2 operator -(Single2 x) => new Single2(-x.v0, -x.v1);
        public static Single2 operator +(Single2 x, Single2 y) => new Single2(x.v0 + y.v0, x.v1 + y.v1);
        public static Single2 operator -(Single2 x, Single2 y) => new Single2(x.v0 - y.v0, x.v1 - y.v1);
        public static Single2 operator *(Single2 x, Single2 y) => new Single2(x.v0 * y.v0, x.v1 * y.v1);
        public static Single2 operator /(Single2 x, Single2 y) => new Single2(x.v0 / y.v0, x.v1 / y.v1);
        public static Single2 operator +(Single2 x, Int2 y) => new Single2(x.v0 + y.v0, x.v1 + y.v1);
        public static Single2 operator -(Single2 x, Int2 y) => new Single2(x.v0 - y.v0, x.v1 - y.v1);
        public static Single2 operator *(Single2 x, Int2 y) => new Single2(x.v0 * y.v0, x.v1 * y.v1);
        public static Single2 operator /(Single2 x, Int2 y) => new Single2(x.v0 / y.v0, x.v1 / y.v1);
        public static Single2 operator +(Single2 x, float y) => new Single2(x.v0 + y, x.v1 + y);
        public static Single2 operator -(Single2 x, float y) => new Single2(x.v0 - y, x.v1 - y);
        public static Single2 operator *(Single2 x, float y) => new Single2(x.v0 * y, x.v1 * y);
        public static Single2 operator /(Single2 x, float y) => new Single2(x.v0 / y, x.v1 / y);
        public void LetNeg() { v0 = -v0; v1 = -v1; }
        public void LetAdd(float y) { v0 += y; v1 += y; }
        public void LetSub(float y) { v0 -= y; v1 -= y; }
        public void LetMul(float y) { v0 *= y; v1 *= y; }
        public void LetDiv(float y) { v0 /= y; v1 /= y; }
        public void LetMod(float y) { v0 %= y; v1 %= y; }
        public void LetSubr(float y) { v0 = y - v0; v1 = y - v1; }
        public void LetDivr(float y) { v0 = y / v0; v1 = y / v1; }
        public void LetModr(float y) { v0 = y % v0; v1 = y % v1; }
        public void LetAdd(Single2 y) { v0 += y.v0; v1 += y.v1; }
        public void LetSub(Single2 y) { v0 -= y.v0; v1 -= y.v1; }
        public void LetMul(Single2 y) { v0 *= y.v0; v1 *= y.v1; }
        public void LetDiv(Single2 y) { v0 /= y.v0; v1 /= y.v1; }
        public void LetMod(Single2 y) { v0 %= y.v0; v1 %= y.v1; }
        public void LetSubr(Single2 y) { v0 = y.v0 - v0; v1 = y.v1 - v1; }
        public void LetDivr(Single2 y) { v0 = y.v0 / v0; v1 = y.v1 / v1; }
        public void LetModr(Single2 y) { v0 = y.v0 % v0; v1 = y.v1 % v1; }
        public int MinIndex() => v1 < v0 ? 1 : 0;
        public int MaxIndex() => v0 < v1 ? 1 : 0;
        public override bool Equals(object? obj) => (obj is Single2 o) && (this == o);
        public override int GetHashCode() => Ex.CombineHashCodes(v0.GetHashCode(), v1.GetHashCode());
        public override string ToString() => $"{v0}, {v1}";
        public string ToString(string format) => $"{v0.ToString(format)}, {v1.ToString(format)}";
        public string ToString(IFormatProvider provider) => string.Format(provider, "({0}, {1})", v0, v1);
        public string ToString(string? format, IFormatProvider? formatProvider) => string.Format(formatProvider, "({0}, {1})", v0.ToString(format, formatProvider), v1.ToString(format, formatProvider));
        public bool Equals(Single2 other) => (v0 == other.v0) && (v1 == other.v1);
        public int CompareTo(Single2 other) => v1 != other.v1 ? (v1 < other.v1 ? -1 : 1) : (v0 == other.v0 ? 0 : v0 < other.v0 ? -1 : 1);
        public static bool IsNaN(Single2 x) => float.IsNaN(x.v0) || float.IsNaN(x.v1);
    }

    [Serializable]
    public partial struct Single3 : IEquatable<Single3>, IComparable<Single3>, IFormattable
    {
        public static Single3 Zero => default;
        public static readonly Single3 One = new Single3(1);
        public static readonly Single3 PositiveInfinity = new Single3(float.PositiveInfinity);
        public static readonly Single3 NegativeInfinity = new Single3(float.NegativeInfinity);
        public static readonly Single3 NaN = new Single3(float.NaN);
        public float v0, v1, v2;
        public float X => v0;
        public float Y => v1;
        public float Z => v2;
        public float this[int index]
        {
            get { Debug.AssertIndex(index, 3); return (index == 0 ? v0 : index == 1 ? v1 : v2); }
            set { Debug.AssertIndex(index, 3); (index == 0 ? ref v0 : ref (index == 1 ? ref v1 : ref v2)) = value; }
        }
        private Single3(float v) { v0 = v1 = v2 = v; }
        public Single3(float v0, float v1, float v2) { this.v0 = v0; this.v1 = v1; this.v2 = v2; }
        public void Deconstruct(out float v0, out float v1, out float v2) { v0 = this.v0; v1 = this.v1; v2 = this.v2; }

        public static explicit operator Int2(Single3 x) => new Int2((int)x.v0, (int)x.v1);
        public static explicit operator Int3(Single3 x) => new Int3((int)x.v0, (int)x.v1, (int)x.v2);
        public static explicit operator Single2(Single3 x) => new Single2(x.v0, x.v1);
        public static explicit operator Double2(Single3 x) => new Double2(x.v0, x.v1);
        public static explicit operator Double3(Single3 x) => new Double3(x.v0, x.v1, x.v2);
        public static bool operator ==(Single3 x, Single3 y) => (x.v0 == y.v0) && (x.v1 == y.v1) && (x.v2 == y.v2);
        public static bool operator !=(Single3 x, Single3 y) => (x.v0 != y.v0) || (x.v1 != y.v1) || (x.v2 != y.v2);
        public static bool operator <=(Single3 x, Single3 y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Single3 x, Single3 y) => x.CompareTo(y) >= 0;
        public static bool operator <(Single3 x, Single3 y) => x.CompareTo(y) < 0;
        public static bool operator >(Single3 x, Single3 y) => x.CompareTo(y) > 0;
        public static Single3 operator +(Single3 x) => new Single3(+x.v0, +x.v1, +x.v2);
        public static Single3 operator -(Single3 x) => new Single3(-x.v0, -x.v1, -x.v2);
        public static Single3 operator +(Single3 x, Single3 y) => new Single3(x.v0 + y.v0, x.v1 + y.v1, x.v2 + y.v2);
        public static Single3 operator -(Single3 x, Single3 y) => new Single3(x.v0 - y.v0, x.v1 - y.v1, x.v2 - y.v2);
        public static Single3 operator *(Single3 x, Single3 y) => new Single3(x.v0 * y.v0, x.v1 * y.v1, x.v2 * y.v2);
        public static Single3 operator /(Single3 x, Single3 y) => new Single3(x.v0 / y.v0, x.v1 / y.v1, x.v2 / y.v2);
        public static Single3 operator +(Single3 x, Int3 y) => new Single3(x.v0 + y.v0, x.v1 + y.v1, x.v2 + y.v2);
        public static Single3 operator -(Single3 x, Int3 y) => new Single3(x.v0 - y.v0, x.v1 - y.v1, x.v2 - y.v2);
        public static Single3 operator *(Single3 x, Int3 y) => new Single3(x.v0 * y.v0, x.v1 * y.v1, x.v2 * y.v2);
        public static Single3 operator /(Single3 x, Int3 y) => new Single3(x.v0 / y.v0, x.v1 / y.v1, x.v2 / y.v2);
        public static Single3 operator +(Single3 x, float y) => new Single3(x.v0 + y, x.v1 + y, x.v2 + y);
        public static Single3 operator -(Single3 x, float y) => new Single3(x.v0 - y, x.v1 - y, x.v2 - y);
        public static Single3 operator *(Single3 x, float y) => new Single3(x.v0 * y, x.v1 * y, x.v2 * y);
        public static Single3 operator /(Single3 x, float y) => new Single3(x.v0 / y, x.v1 / y, x.v2 / y);
        public void LetNeg() { v0 = -v0; v1 = -v1; v2 = -v2; }
        public void LetAdd(float y) { v0 += y; v1 += y; v2 += y; }
        public void LetSub(float y) { v0 -= y; v1 -= y; v2 /= y; }
        public void LetMul(float y) { v0 *= y; v1 *= y; v2 *= y; }
        public void LetDiv(float y) { v0 /= y; v1 /= y; v2 /= y; }
        public void LetMod(float y) { v0 %= y; v1 %= y; v2 %= y; }
        public void LetSubr(float y) { v0 = y - v0; v1 = y - v1; v2 = y - v1; }
        public void LetDivr(float y) { v0 = y / v0; v1 = y / v1; v2 = y / v1; }
        public void LetModr(float y) { v0 = y % v0; v1 = y % v1; v2 = y % v1; }
        public void LetAdd(Single3 y) { v0 += y.v0; v1 += y.v1; v2 += y.v2; }
        public void LetSub(Single3 y) { v0 -= y.v0; v1 -= y.v1; v2 += y.v2; }
        public void LetMul(Single3 y) { v0 *= y.v0; v1 *= y.v1; v2 += y.v2; }
        public void LetDiv(Single3 y) { v0 /= y.v0; v1 /= y.v1; v2 += y.v2; }
        public void LetMod(Single3 y) { v0 %= y.v0; v1 %= y.v1; v2 += y.v2; }
        public void LetSubr(Single3 y) { v0 = y.v0 - v0; v1 = y.v1 - v1; v2 = y.v2 - v2; }
        public void LetDivr(Single3 y) { v0 = y.v0 / v0; v1 = y.v1 / v1; v2 = y.v2 / v2; }
        public void LetModr(Single3 y) { v0 = y.v0 % v0; v1 = y.v1 % v1; v2 = y.v2 % v2; }
        public int MinIndex() => v1 < v0 ? (v2 < v1 ? 2 : 1) : (v2 < v0 ? 2 : 0);
        public int MaxIndex() => v0 < v1 ? (v1 < v2 ? 2 : 1) : (v0 < v2 ? 2 : 0);
        public override bool Equals(object? obj) => (obj is Single3 o) && (this == o);
        public override int GetHashCode() => Ex.CombineHashCodes(v0.GetHashCode(), v1.GetHashCode(), v2.GetHashCode());
        public override string ToString() => $"{v0}, {v1}, {v2}";
        public string ToString(string format) => $"{v0.ToString(format)}, {v1.ToString(format)}, {v2.ToString(format)}";
        public string ToString(IFormatProvider provider) => string.Format(provider, "({0}, {1}, {2})", v0, v1, v2);
        public string ToString(string? format, IFormatProvider? formatProvider) => string.Format(formatProvider, "({0}, {1}, {2})", v0.ToString(format, formatProvider), v1.ToString(format, formatProvider), v2.ToString(format, formatProvider));
        public bool Equals(Single3 other) => (v0 == other.v0) && (v1 == other.v1) && (v2 == other.v2);
        public int CompareTo(Single3 other) => v2 != other.v2 ? (v2 < other.v1 ? -1 : 1) : (v1 != other.v1 ? (v1 < other.v1 ? -1 : 1) : (v0 == other.v0 ? 0 : v0 < other.v0 ? -1 : 1));
        public static bool IsNaN(Single3 x) => float.IsNaN(x.v0) || float.IsNaN(x.v1) || float.IsNaN(x.v2);
    }
    #endregion

    #region double
    [Serializable]
    public partial struct Double2 : IEquatable<Double2>, IComparable<Double2>, IFormattable
    {
        public static Double2 Zero => default;
        public static readonly Double2 One = new Double2(1);
        public static readonly Double2 PositiveInfinity = new Double2(double.PositiveInfinity);
        public static readonly Double2 NegativeInfinity = new Double2(double.NegativeInfinity);
        public static readonly Double2 NaN = new Double2(double.NaN);
        public double v0, v1;
        public double X => v0;
        public double Y => v1;
        public double this[int index]
        {
            get { Debug.AssertIndex(index, 2); return (index == 0 ? v0 : v1); }
            set { Debug.AssertIndex(index, 2); (index == 0 ? ref v0 : ref v1) = value; }
        }
        private Double2(double v) { v0 = v1 = v; }
        public Double2(double v0, double v1) { this.v0 = v0; this.v1 = v1; }
        public void Deconstruct(out double v0, out double v1) { v0 = this.v0; v1 = this.v1; }

        public static explicit operator Int2(Double2 x) => new Int2((int)x.v0, (int)x.v1);
        public static explicit operator Int3(Double2 x) => new Int3((int)x.v0, (int)x.v1, 0);
        public static explicit operator Double3(Double2 x) => new Double3(x.v0, x.v1, 0);
        public static explicit operator Single2(Double2 x) => new Single2((float)x.v0, (float)x.v1);
        public static explicit operator Single3(Double2 x) => new Single3((float)x.v0, (float)x.v1, 0);
        public static bool operator ==(Double2 x, Double2 y) => (x.v0 == y.v0) && (x.v1 == y.v1);
        public static bool operator !=(Double2 x, Double2 y) => (x.v0 != y.v0) || (x.v1 != y.v1);
        public static bool operator <=(Double2 x, Double2 y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Double2 x, Double2 y) => x.CompareTo(y) >= 0;
        public static bool operator <(Double2 x, Double2 y) => x.CompareTo(y) < 0;
        public static bool operator >(Double2 x, Double2 y) => x.CompareTo(y) > 0;
        public static Double2 operator +(Double2 x) => new Double2(+x.v0, +x.v1);
        public static Double2 operator -(Double2 x) => new Double2(-x.v0, -x.v1);
        public static Double2 operator +(Double2 x, Double2 y) => new Double2(x.v0 + y.v0, x.v1 + y.v1);
        public static Double2 operator -(Double2 x, Double2 y) => new Double2(x.v0 - y.v0, x.v1 - y.v1);
        public static Double2 operator *(Double2 x, Double2 y) => new Double2(x.v0 * y.v0, x.v1 * y.v1);
        public static Double2 operator /(Double2 x, Double2 y) => new Double2(x.v0 / y.v0, x.v1 / y.v1);
        public static Double2 operator +(Double2 x, Int2 y) => new Double2(x.v0 + y.v0, x.v1 + y.v1);
        public static Double2 operator -(Double2 x, Int2 y) => new Double2(x.v0 - y.v0, x.v1 - y.v1);
        public static Double2 operator *(Double2 x, Int2 y) => new Double2(x.v0 * y.v0, x.v1 * y.v1);
        public static Double2 operator /(Double2 x, Int2 y) => new Double2(x.v0 / y.v0, x.v1 / y.v1);
        public static Double2 operator +(Double2 x, double y) => new Double2(x.v0 + y, x.v1 + y);
        public static Double2 operator -(Double2 x, double y) => new Double2(x.v0 - y, x.v1 - y);
        public static Double2 operator *(Double2 x, double y) => new Double2(x.v0 * y, x.v1 * y);
        public static Double2 operator /(Double2 x, double y) => new Double2(x.v0 / y, x.v1 / y);
        public void LetNeg() { v0 = -v0; v1 = -v1; }
        public void LetAdd(double y) { v0 += y; v1 += y; }
        public void LetSub(double y) { v0 -= y; v1 -= y; }
        public void LetMul(double y) { v0 *= y; v1 *= y; }
        public void LetDiv(double y) { v0 /= y; v1 /= y; }
        public void LetMod(double y) { v0 %= y; v1 %= y; }
        public void LetSubr(double y) { v0 = y - v0; v1 = y - v1; }
        public void LetDivr(double y) { v0 = y / v0; v1 = y / v1; }
        public void LetModr(double y) { v0 = y % v0; v1 = y % v1; }
        public void LetAdd(Double2 y) { v0 += y.v0; v1 += y.v1; }
        public void LetSub(Double2 y) { v0 -= y.v0; v1 -= y.v1; }
        public void LetMul(Double2 y) { v0 *= y.v0; v1 *= y.v1; }
        public void LetDiv(Double2 y) { v0 /= y.v0; v1 /= y.v1; }
        public void LetMod(Double2 y) { v0 %= y.v0; v1 %= y.v1; }
        public void LetSubr(Double2 y) { v0 = y.v0 - v0; v1 = y.v1 - v1; }
        public void LetDivr(Double2 y) { v0 = y.v0 / v0; v1 = y.v1 / v1; }
        public void LetModr(Double2 y) { v0 = y.v0 % v0; v1 = y.v1 % v1; }
        public int MinIndex() => v1 < v0 ? 1 : 0;
        public int MaxIndex() => v0 < v1 ? 1 : 0;
        public override bool Equals(object? obj) => (obj is Double2 o) && (this == o);
        public override int GetHashCode() => Ex.CombineHashCodes(v0.GetHashCode(), v1.GetHashCode());
        public override string ToString() => $"{v0}, {v1}";
        public string ToString(string format) => $"{v0.ToString(format)}, {v1.ToString(format)}";
        public string ToString(IFormatProvider provider) => string.Format(provider, "({0}, {1})", v0, v1);
        public string ToString(string? format, IFormatProvider? formatProvider) => string.Format(formatProvider, "({0}, {1})", v0.ToString(format, formatProvider), v1.ToString(format, formatProvider));
        public bool Equals(Double2 other) => (v0 == other.v0) && (v1 == other.v1);
        public int CompareTo(Double2 other) => v1 != other.v1 ? (v1 < other.v1 ? -1 : 1) : (v0 == other.v0 ? 0 : v0 < other.v0 ? -1 : 1);
        public static bool IsNaN(Double2 x) => double.IsNaN(x.v0) || double.IsNaN(x.v1);
    }

    [Serializable]
    public partial struct Double3 : IEquatable<Double3>, IComparable<Double3>, IFormattable
    {
        public static Double3 Zero => default;
        public static readonly Double3 One = new Double3(1);
        public static readonly Double3 PositiveInfinity = new Double3(double.PositiveInfinity);
        public static readonly Double3 NegativeInfinity = new Double3(double.NegativeInfinity);
        public static readonly Double3 NaN = new Double3(double.NaN);
        public double v0, v1, v2;
        public double X => v0;
        public double Y => v1;
        public double Z => v2;
        public double this[int index]
        {
            get { Debug.AssertIndex(index, 3); return (index == 0 ? v0 : index == 1 ? v1 : v2); }
            set { Debug.AssertIndex(index, 3); (index == 0 ? ref v0 : ref (index == 1 ? ref v1 : ref v2)) = value; }
        }
        private Double3(double v) { v0 = v1 = v2 = v; }
        public Double3(double v0, double v1, double v2) { this.v0 = v0; this.v1 = v1; this.v2 = v2; }
        public void Deconstruct(out double v0, out double v1, out double v2) { v0 = this.v0; v1 = this.v1; v2 = this.v2; }

        public static explicit operator Int2(Double3 x) => new Int2((int)x.v0, (int)x.v1);
        public static explicit operator Int3(Double3 x) => new Int3((int)x.v0, (int)x.v1, (int)x.v2);
        public static explicit operator Double2(Double3 x) => new Double2(x.v0, x.v1);
        public static explicit operator Single2(Double3 x) => new Single2((float)x.v0, (float)x.v1);
        public static explicit operator Single3(Double3 x) => new Single3((float)x.v0, (float)x.v1, (float)x.v2);
        public static bool operator ==(Double3 x, Double3 y) => (x.v0 == y.v0) && (x.v1 == y.v1) && (x.v2 == y.v2);
        public static bool operator !=(Double3 x, Double3 y) => (x.v0 != y.v0) || (x.v1 != y.v1) || (x.v2 != y.v2);
        public static bool operator <=(Double3 x, Double3 y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Double3 x, Double3 y) => x.CompareTo(y) >= 0;
        public static bool operator <(Double3 x, Double3 y) => x.CompareTo(y) < 0;
        public static bool operator >(Double3 x, Double3 y) => x.CompareTo(y) > 0;
        public static Double3 operator +(Double3 x) => new Double3(+x.v0, +x.v1, +x.v2);
        public static Double3 operator -(Double3 x) => new Double3(-x.v0, -x.v1, -x.v2);
        public static Double3 operator +(Double3 x, Double3 y) => new Double3(x.v0 + y.v0, x.v1 + y.v1, x.v2 + y.v2);
        public static Double3 operator -(Double3 x, Double3 y) => new Double3(x.v0 - y.v0, x.v1 - y.v1, x.v2 - y.v2);
        public static Double3 operator *(Double3 x, Double3 y) => new Double3(x.v0 * y.v0, x.v1 * y.v1, x.v2 * y.v2);
        public static Double3 operator /(Double3 x, Double3 y) => new Double3(x.v0 / y.v0, x.v1 / y.v1, x.v2 / y.v2);
        public static Double3 operator +(Double3 x, Int3 y) => new Double3(x.v0 + y.v0, x.v1 + y.v1, x.v2 + y.v2);
        public static Double3 operator -(Double3 x, Int3 y) => new Double3(x.v0 - y.v0, x.v1 - y.v1, x.v2 - y.v2);
        public static Double3 operator *(Double3 x, Int3 y) => new Double3(x.v0 * y.v0, x.v1 * y.v1, x.v2 * y.v2);
        public static Double3 operator /(Double3 x, Int3 y) => new Double3(x.v0 / y.v0, x.v1 / y.v1, x.v2 / y.v2);
        public static Double3 operator +(Double3 x, double y) => new Double3(x.v0 + y, x.v1 + y, x.v2 + y);
        public static Double3 operator -(Double3 x, double y) => new Double3(x.v0 - y, x.v1 - y, x.v2 - y);
        public static Double3 operator *(Double3 x, double y) => new Double3(x.v0 * y, x.v1 * y, x.v2 * y);
        public static Double3 operator /(Double3 x, double y) => new Double3(x.v0 / y, x.v1 / y, x.v2 / y);
        public void LetNeg() { v0 = -v0; v1 = -v1; v2 = -v2; }
        public void LetAdd(double y) { v0 += y; v1 += y; v2 += y; }
        public void LetSub(double y) { v0 -= y; v1 -= y; v2 /= y; }
        public void LetMul(double y) { v0 *= y; v1 *= y; v2 *= y; }
        public void LetDiv(double y) { v0 /= y; v1 /= y; v2 /= y; }
        public void LetMod(double y) { v0 %= y; v1 %= y; v2 %= y; }
        public void LetSubr(double y) { v0 = y - v0; v1 = y - v1; v2 = y - v1; }
        public void LetDivr(double y) { v0 = y / v0; v1 = y / v1; v2 = y / v1; }
        public void LetModr(double y) { v0 = y % v0; v1 = y % v1; v2 = y % v1; }
        public void LetAdd(Double3 y) { v0 += y.v0; v1 += y.v1; v2 += y.v2; }
        public void LetSub(Double3 y) { v0 -= y.v0; v1 -= y.v1; v2 += y.v2; }
        public void LetMul(Double3 y) { v0 *= y.v0; v1 *= y.v1; v2 += y.v2; }
        public void LetDiv(Double3 y) { v0 /= y.v0; v1 /= y.v1; v2 += y.v2; }
        public void LetMod(Double3 y) { v0 %= y.v0; v1 %= y.v1; v2 += y.v2; }
        public void LetSubr(Double3 y) { v0 = y.v0 - v0; v1 = y.v1 - v1; v2 = y.v2 - v2; }
        public void LetDivr(Double3 y) { v0 = y.v0 / v0; v1 = y.v1 / v1; v2 = y.v2 / v2; }
        public void LetModr(Double3 y) { v0 = y.v0 % v0; v1 = y.v1 % v1; v2 = y.v2 % v2; }
        public int MinIndex() => v1 < v0 ? (v2 < v1 ? 2 : 1) : (v2 < v0 ? 2 : 0);
        public int MaxIndex() => v0 < v1 ? (v1 < v2 ? 2 : 1) : (v0 < v2 ? 2 : 0);
        public override bool Equals(object? obj) => (obj is Double3 o) && (this == o);
        public override int GetHashCode() => Ex.CombineHashCodes(v0.GetHashCode(), v1.GetHashCode(), v2.GetHashCode());
        public override string ToString() => $"{v0}, {v1}, {v2}";
        public string ToString(string format) => $"{v0.ToString(format)}, {v1.ToString(format)}, {v2.ToString(format)}";
        public string ToString(IFormatProvider provider) => string.Format(provider, "({0}, {1}, {2})", v0, v1, v2);
        public string ToString(string? format, IFormatProvider? formatProvider) => string.Format(formatProvider, "({0}, {1}, {2})", v0.ToString(format, formatProvider), v1.ToString(format, formatProvider), v2.ToString(format, formatProvider));
        public bool Equals(Double3 other) => (v0 == other.v0) && (v1 == other.v1) && (v2 == other.v2);
        public int CompareTo(Double3 other) => v2 != other.v2 ? (v2 < other.v1 ? -1 : 1) : (v1 != other.v1 ? (v1 < other.v1 ? -1 : 1) : (v0 == other.v0 ? 0 : v0 < other.v0 ? -1 : 1));
        public static bool IsNaN(Double3 x) => double.IsNaN(x.v0) || double.IsNaN(x.v1) || double.IsNaN(x.v2);
    }
    #endregion
    #endregion

    #region Ints class
    public class Ints : IComparable, IEquatable<Ints>, IComparable<Ints>, IList<int>
    {
        readonly int[] Items;
        public Ints(params int[] items)
        {
            Items = items.CloneX();
        }
        public Ints(IEnumerable<int> collection)
        {
            Items = collection.ToArray();
        }
        public int Length => Items.Length;
        public static bool operator ==(Ints x, Ints y) => x.Equals(y);
        public static bool operator !=(Ints x, Ints y) => !x.Equals(y);
        public static bool operator <=(Ints x, Ints y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Ints x, Ints y) => x.CompareTo(y) >= 0;
        public static bool operator <(Ints x, Ints y) => x.CompareTo(y) < 0;
        public static bool operator >(Ints x, Ints y) => x.CompareTo(y) > 0;
        public static Ints operator +(Ints x, Ints y) => new Ints(x.Items.Concat(y.Items));
        public override bool Equals(object? obj) => (obj is Ints o) && Equals(o);
        public override int GetHashCode()
        {
            int h = 0;
            for (int i = 0; i < Items.Length; i++)
                h = Ex.CombineHashCodes(h, Items[i]);
            return h;
        }
        public override string ToString() => Items.ToStringFormat("", ", ");

        #region IComparable メンバー
        public int CompareTo(object? obj) => (obj is Ints o) ? CompareTo(o) : 1;
        #endregion
        #region IEquatable<Ints> メンバ
        public bool Equals(Ints? other)
        {
            if (this is null) return other is null;
            if (other is null) return false;
            if (Items.Length != other.Items.Length) return false;
            for (int i = 0; i < Items.Length; i++)
                if (Items[i] != other.Items[i]) return false;
            return true;
        }
        #endregion
        #region IComparable<Ints> メンバ
        public int CompareTo(Ints? other)
        {
            if (this is null) return (other is null) ? 0 : -1;
            if (other is null) return 1;
            int l = Math.Min(Items.Length, other.Items.Length);
            for (int i = 0; i < l; i++)
                if (Items[i] != other.Items[i]) return Items[i] - other.Items[i];
            return Items.Length - other.Items.Length;
        }
        #endregion
        #region IList<int> メンバー
        public int IndexOf(int item) => Items.IndexOf(item);
        [DoesNotReturn] public void Insert(int index, int item) => ThrowException.NotImplemented();
        [DoesNotReturn] public void RemoveAt(int index) => ThrowException.NotImplemented();
        public int this[int index]
        {
            get => Items[index];
            [DoesNotReturn] set => ThrowException.NotImplemented();
        }
        #endregion
        #region ICollection<int> メンバー
        [DoesNotReturn] public void Add(int item) => ThrowException.NotImplemented();
        [DoesNotReturn] public void Clear() => ThrowException.NotImplemented();
        public bool Contains(int item) => Items.Contains(item);
        public void CopyTo(int[] array, int arrayIndex) { Items.CopyTo(array, arrayIndex); }
        public int Count => Items.Length;
        public bool IsReadOnly => true;
        [DoesNotReturn] public bool Remove(int item) => ThrowException.NotImplemented<bool>();
        #endregion
        #region IEnumerable<int> メンバー
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < Items.Length; i++)
                yield return Items[i];
        }
        #endregion
        #region IEnumerable メンバー
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
    #endregion

    #region Array<T>
    public struct Array<T> : ICloneable
    {
        public readonly Array A;
        internal Array(Array array) { A = array; }
        public Array(Array<T> array) { A = array.A; }
        public Array(int[] lengths) { A = Array.CreateInstance(typeof(T), lengths); }
        public Array(T[] array) { A = array; }
        public Array(T[,] array) { A = array; }
        public Array(T[,,] array) { A = array; }
        public Array(T[,,,] array) { A = array; }
        public int Rank => A.Rank;
#pragma warning disable CA1721
        public int Length => A.Length;
#pragma warning restore CA1721
        public int GetLength(int d) => A.GetLength(d);
        public int[] GetLengths() => A.GetLengths();
        public static implicit operator Array<T>(T[] array) => new Array<T>(array);
        public static implicit operator Array<T>(T[,] array) => new Array<T>(array);
        public static implicit operator Array<T>(T[,,] array) => new Array<T>(array);
        public static implicit operator Array<T>(T[,,,] array) => new Array<T>(array);
        public static explicit operator T[](Array<T> array) => (T[])array.A;
        public static explicit operator T[,](Array<T> array) => (T[,])array.A;
        public static explicit operator T[,,](Array<T> array) => (T[,,])array.A;
        public static explicit operator T[,,,](Array<T> array) => (T[,,,])array.A;
        public Array<T> CloneX() => new Array<T>(A.CloneX());
        public object Clone() => CloneX();
        public Array<T> To0() => new Array<T>(GetLengths());
        public Array<U> To0<U>() => new Array<U>(GetLengths());
        public Span<T> AsSpan() => A.AsSpan<T>();
        public Span<T> AsSpan(int start) => A.AsSpan<T>(start);
        public Span<T> AsSpan(int start, int length) => A.AsSpan<T>(start, length);
        public Span<T> AsSpan(Index_ i) => A.AsSpan<T>(i);
        public Array<R> As<R>() => new Array<R>(A);  //sizeof(T)!=sizeof(R)の時Lengthが異なるので注意
        public Enumerator this[Range_ r0] { get => new Enumerator(this, stackalloc[] { r0 }); }
        public Enumerator this[Range_ r0, Range_ r1] { get => new Enumerator(this, stackalloc[] { r0, r1 }); }
        public Enumerator this[Range_ r0, Range_ r1, Range_ r2] { get => new Enumerator(this, stackalloc[] { r0, r1, r2 }); }
        public Enumerator this[Range_ r0, Range_ r1, Range_ r2, Range_ r3] { get => new Enumerator(this, stackalloc[] { r0, r1, r2, r3 }); }

        public struct Enumerator : IEnumerable<T>
        {
            readonly Array<T> A;
            readonly IEnumerator<int> E;
            public Enumerator(Array<T> A, Span<Range_> ranges)
            {
                if (A.Rank != ranges.Length) ThrowException.SizeMismatch();
                this.A = A;
                var L = new (int n, int s, int c)[ranges.Length];
                for (int d = L.Length; --d >= 0;) { var n = A.GetLength(d); var (s, c) = ranges[d].GetOffsetAndLength(n); L[d] = (n, s, c); }
                this.E = Ex.ForEach(L).GetEnumerator();
            }
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new EnumeratorItem(A, E);
            IEnumerator IEnumerable.GetEnumerator() => new EnumeratorItem(A, E);
            public EnumeratorRef GetEnumerator() => new EnumeratorRef(A, E);
            public struct EnumeratorItem : IEnumerator<T>
            {
                readonly Fix<T> F;
                readonly IEnumerator<int> E;
                public EnumeratorItem(Array<T> A, IEnumerator<int> E)
                {
                    this.F = New.Fix(A);
                    this.E = E;
                }
                public void Reset() { }
                public void Dispose() { F.Dispose(); }
                public bool MoveNext() => E.MoveNext();
                public T Current { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => F[E.Current]; }
                object? IEnumerator.Current => Current;
            }
            public struct EnumeratorRef: IDisposable
            {
                readonly Fix<T> F;
                readonly IEnumerator<int> E;
                public EnumeratorRef(Array<T> A, IEnumerator<int> E)
                {
                    this.F = New.Fix(A);
                    this.E = E;
                }
                public void Dispose() { F.Dispose(); }
                public bool MoveNext() => E.MoveNext();
                public ref T Current { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ref F[E.Current]; }
            }
        }
    }

    public unsafe struct Fix<T> : IDisposable
    {
        private readonly IntPtr P;
        public readonly int Length;
        private readonly GCHandle H;
        public ref T this[int i] { [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining )] get { Debug.AssertIndex(i, Length); return ref Unsafe.Add(ref Unsafe.AsRef<T>((void*)P), i); } }
        public static implicit operator IntPtr(Fix<T> p) => p.P;
        public Fix(Array<T> A) : this(A.A) { }
        public Fix(T[] A) : this((Array)A) { }
        public Fix(T[,] A) : this((Array)A) { }
        public Fix(T[,,] A) : this((Array)A) { }
        public Fix(T[,,,] A) : this((Array)A) { }
        internal Fix(Array A)
        {
            H = GCHandle.Alloc(A, GCHandleType.Pinned);
            P = Marshal.UnsafeAddrOfPinnedArrayElement(A, 0);
            Length = A.Length;
        }
        public void Dispose()
        {
            H.Free();
        }
        public Span<T> AsSpan() => MemoryMarshal.CreateSpan(ref Unsafe.AsRef<T>((void*)P), Length);
    }
    #endregion

    #region Index_, Range_
    public struct Index_
    {
        public int v;
        public Index_(int i) { v = i; }
        public Index_(int i, bool fromEnd) { v = fromEnd ? ~i : i; }
        public Index_(Index i) { v = i.IsFromEnd ? ~i.Value : i.Value; }
        public int GetOffset(int length) => v < 0 ? length - ~v : v;
        public bool IsFromEnd => v < 0;
        public int Value => v < 0 ? ~v : v;
        public static implicit operator Index_(int i) => new Index_(i);
        public static implicit operator Index_(Index i) => new Index_(i);
        public static implicit operator Index(Index_ i) => new Index(i.Value, i.IsFromEnd);
        public static Index_ operator +(Index_ i, int j) => new Index_(i.v + j);
        public static Index_ operator -(Index_ i, int j) => new Index_(i.v - j);
    }

    public struct Range_
    {
        public Index_ Start { get; set; }
        public Index_ End { get; set; }
        public Range_(Index_ i0, Index_ i1) { Start = i0; End = i1; }
        public Range_(Range r) { Start = r.Start; End = r.End; }
        public Range_(Index_ i) { Start = i; End = i + 1; }
        public static implicit operator Range_(Range r) => new Range_(r);
        public static implicit operator Range_(Index_ i) => new Range_(i);
        public static implicit operator Range_(Index i) => new Range_(new Index_(i));
        public static implicit operator Range_(int i) => new Range_(new Index_(i));
        public static implicit operator Range(Range_ r) => new Range(r.Start, r.End);
        public (int Start, int End) GetOffsets(int length) => (Start.GetOffset(length), End.GetOffset(length));
        public (int Offset, int Length) GetOffsetAndLength(int length) { var o = GetOffsets(length); return (o.Start, o.End - o.Start); }
    }
    #endregion

    #region MultiDimensionalIndexer
    public unsafe struct MultiDimensionalIndexer
    {
        public readonly int dim;
        public readonly int n;
        public readonly int[] ii;
        public readonly int[] nn;
        public readonly int[] oo;
        public MultiDimensionalIndexer(int[] lengths)
        {
            dim = lengths.Length;
            ii = new int[dim];
            nn = lengths;
            oo = new int[dim];
            n = 1;
            for (int i = dim; --i >= 0;) { oo[i] = n; n *= nn[i]; }
        }
        public bool Inc()
        {
            for (int i = dim; --i >= 0;)
                if (++ii[i] == nn[i]) ii[i] = 0; else return true;
            return false;
        }
        public bool Dec()
        {
            for (int i = dim; --i >= 0;)
                if (--ii[i] < 0) ii[i] = nn[i] - 1; else return true;
            return false;
        }
        public void Inc_()
        {
            for (int i = dim; --i >= 0;)
                if (++ii[i] == nn[i]) ii[i] = 0; else return;
        }
        public void Dec_()
        {
            for (int i = dim; --i >= 0;)
                if (--ii[i] < 0) ii[i] = nn[i] - 1; else return;
        }
    }
    #endregion

    #region StopWatch
    public class StopWatch
    {
        DateTime RegisteredTime;
        TimeSpan Duration;
        bool Running;
        public TimeSpan Elapsed => Running ? Duration + (DateTime.Now - RegisteredTime) : Duration;
        public bool IsRunning => Running;
        public StopWatch() { Restart(); }
        public void Restart() { Reset(); Start(); }
        public void Reset() { Running = false; Duration = TimeSpan.Zero; }
        public void Start() { RegisteredTime = DateTime.Now; Running = true; }
        public TimeSpan Stop() { Running = false; return Duration += DateTime.Now - RegisteredTime; }
        public override string ToString() => Elapsed.ToString();
    }
    #endregion

    #region RandomMT
    // MersenneTwister.dSFMT2.1
    [Serializable]
    public class RandomMT
    {
        const int N = 624, M = 397;
        const uint UPPER_MASK = 0x80000000u;
        const uint LOWER_MASK = 0x7fffffffu;
        const uint MATRIX_A = 0x9908b0dfu;

        uint BufferIndex;
        readonly uint[] Buffer = new uint[N];

        public RandomMT() { Init((uint)DateTime.Now.ToBinary()); }
        public RandomMT(uint seed) { Init(seed); }
        public RandomMT(int seed) { Init((uint)seed); }

        public void Init(uint seed)
        {
            Buffer[0] = seed;
            for (int i = 1; i < N; i++)
                Buffer[i] = 1812433253u * (Buffer[i - 1] ^ (Buffer[i - 1] >> 30)) + (uint)i;
            BufferIndex = N;
        }
        public void Init(uint[] key)
        {
            Init(19650218u);
            uint i = 1, j = 0;
            for (int k = Math.Max(N, key.Length); k > 0; k--)
            {
                Buffer[i] = (Buffer[i] ^ ((Buffer[i - 1] ^ (Buffer[i - 1] >> 30)) * 1664525u)) + key[j] + j; // non linear
                if (++i >= N) { i = 1; Buffer[0] = Buffer[N - 1]; }
                if (++j >= key.Length) j = 0;
            }
            for (int k = N - 1; k > 0; k--)
            {
                Buffer[i] = (Buffer[i] ^ ((Buffer[i - 1] ^ (Buffer[i - 1] >> 30)) * 1566083941u)) - i; // non linear
                if (++i >= N) { i = 1; Buffer[0] = Buffer[N - 1]; }
            }
            Buffer[0] = 0x80000000u; // MSB is 1; assuring non-zero initial array
        }

        // 32-bit uint [0, 0xffffffff]
        public uint UInt32()
        {
            if (BufferIndex >= N) { BufferIndex = 0; UInt32_(); } // generate N words at one time            
            uint y = Buffer[BufferIndex++];
            // tempering
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680u;
            y ^= (y << 15) & 0xefc60000u;
            y ^= (y >> 18);
            return y;
        }
        void UInt32_()
        {
            for (int kk = 0, k1 = 1, kM = M; kk < N; kk++, k1++, kM++)
            {
                if (k1 == N) k1 = 0;
                if (kM == N) kM = 0;
                uint y = (Buffer[kk] & UPPER_MASK) | (Buffer[k1] & LOWER_MASK);
                Buffer[kk] = Buffer[kM] ^ (y >> 1) ^ ((y & 1u) * MATRIX_A);
            }
        }
        // 31-bit int [-0x80000000, 0x7fffffff]
        public int Int32() => (int)UInt32();
        // 31-bit int [0, 0x7fffffff]
        public int Int31() => (int)(UInt32() >> 1);

        public uint UInt(uint value) => UInt32() % value;
        public int Int(int value) => (int)UInt((uint)value);

        public uint UIntExact(uint value)
        {
            while (true)
            {
                uint y = UInt32();
                if (y < (1LU << 32) / value * value) return y % value;
            }
        }
        public int IntExact(int value) => (int)UIntExact((uint)value);

        public float Single() => Single32CO();
        // 32-bit float [0, 1]
        public float Single32CC() => UInt32() * (1.0f / ((1L << 32) - 1));
        // 32-bit float [0, 1)
        public float Single32CO() => UInt32() * (1.0f / (1L << 32));
        // 32-bit float (0, 1]
        public float Single32OC() => (UInt32() + 1.0f) * (1.0f / (1L << 32));
        // 32-bit float (0, 1)
        public float Single32OO() => (UInt32() + 0.5f) * (1.0f / (1L << 32));

        public double Double() => Double32CO();
        // 32-bit double [0, 1]
        public double Double32CC() => UInt32() * (1.0 / ((1L << 32) - 1));
        // 32-bit double [0, 1)
        public double Double32CO() => UInt32() * (1.0 / (1L << 32));
        // 32-bit double (0, 1]
        public double Double32OC() => (UInt32() + 1.0) * (1.0 / (1L << 32));
        // 32-bit double (0, 1)
        public double Double32OO() => (UInt32() + 0.5) * (1.0 / (1L << 32));
        // 53-bit double [0, 1]
        public double Double53CC() => ((UInt32() >> 5) * (double)(1 << 26) + (UInt32() >> 6)) * (1.0 / ((1L << 53) - 1));
        // 53-bit double [0, 1)
        public double Double53CO() => ((UInt32() >> 5) * (double)(1 << 26) + (UInt32() >> 6)) * (1.0 / (1L << 53));
        // 53-bit double (0, 1]
        public double Double53OC() => ((UInt32() >> 5) * (double)(1 << 26) + (UInt32() >> 6) + 1.0) * (1.0 / (1L << 53));
        // 52-bit double (0, 1)
        public double Double52OO() => ((UInt32() >> 6) * (double)(1 << 26) + (UInt32() >> 6) + 0.5) * (1.0 / (1L << 52));

        public ComplexS ComplexS() => new ComplexS(Single(), Single());
        public ComplexD ComplexD() => new ComplexD(Double(), Double());

        public int[] Order(int n)
        {
            var a = new int[n];
            for (int i = n; --i >= 0;) a[i] = i;
            Randomize(a.AsSpan());
            return a;
        }
        public void Randomize<T>(Span<T> span)
        {
            for (int i = span.Length; --i > 0;) span.Swap(i, Int(i + 1));
        }
        public void Randomize<T>(T[] array) => Randomize(array.AsSpan());
        public void Randomize<T>(IList<T> list)
        {
            for (int i = list.Count; --i > 0;) list.Swap(i, Int(i + 1));
        }
        public int Categorical(double[] distribution)
        {
            var r = Double();
            var c = 0.0;
            for (int i = 0; i < distribution.Length - 1; i++)
            {
                c += distribution[i];
                if (r < c) return i;
            }
            return distribution.Length - 1;
        }

        public double Gaussian() => Gaussian32();
        double BufferGaussian32 = double.PositiveInfinity;
        public double Gaussian32()
        {
            if (BufferGaussian32 != double.PositiveInfinity) { double x = BufferGaussian32; BufferGaussian32 = double.PositiveInfinity; return x; }
            double v1, v2, r;
            do
            {
                v1 = Double32OO() - 0.5;
                v2 = Double32OO() - 0.5;
                r = v1 * v1 + v2 * v2;
            } while (r >= 0.25 || r == 0);
            double f = Math.Sqrt(-2 * Math.Log(r * 4) / r);
            BufferGaussian32 = v2 * f;
            return v1 * f;
        }
        double BufferGaussian52 = double.PositiveInfinity;
        public double Gaussian52()
        {
            if (BufferGaussian52 != double.PositiveInfinity) { double x = BufferGaussian52; BufferGaussian52 = double.PositiveInfinity; return x; }
            double v1, v2, r;
            do
            {
                v1 = Double52OO() - 0.5;
                v2 = Double52OO() - 0.5;
                r = v1 * v1 + v2 * v2;
            } while (r >= 0.25 || r == 0);
            double f = Math.Sqrt(-2 * Math.Log(r * 4) / r);
            BufferGaussian52 = v2 * f;
            return v1 * f;
        }
        public double ExponentialDistribution(double tau)
        {
            return -tau * Math.Log(1 - Double52OO());
        }
    }
    #endregion

    #region Comparer classes
    public class EqualityComparerArray<T> : IEqualityComparer<T[]>
        where T : IComparable<T>
    {
        public EqualityComparerArray() { }
        public bool Equals(T[]? x, T[]? y) => Ex.Compare(x, y) == 0;
        public int GetHashCode(T[] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
    }
    public class EqualityComparerArray2<T> : IEqualityComparer<T[][]>
        where T : IComparable<T>
    {
        public EqualityComparerArray2() { }
        public bool Equals(T[][]? x, T[][]? y) => Ex.Compare(x, y) == 0;
        public int GetHashCode(T[][] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
    }
    public class EqualityComparerArray3<T> : IEqualityComparer<T[][][]>
        where T : IComparable<T>
    {
        public EqualityComparerArray3() { }
        public bool Equals(T[][][]? x, T[][][]? y) => Ex.Compare(x, y) == 0;
        public int GetHashCode(T[][][] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
    }
    public class EqualityComparerArray4<T> : IEqualityComparer<T[][][][]>
        where T : IComparable<T>
    {
        public EqualityComparerArray4() { }
        public bool Equals(T[][][][]? x, T[][][][]? y) => Ex.Compare(x, y) == 0;
        public int GetHashCode(T[][][][] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
    }
    public class EqualityComparerIList<T> : IEqualityComparer<IList<T>>
        where T : IComparable<T>
    {
        public EqualityComparerIList() { }
        public bool Equals(IList<T>? x, IList<T>? y) => Ex.Compare(x, y) == 0;
        public int GetHashCode(IList<T> obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
    }

    public class ComparerArray<T> : IComparer<T[]>
        where T : IComparable<T>
    {
        public ComparerArray() { }
        public int Compare(T[]? x, T[]? y) => Ex.Compare(x, y);
    }
    public class ComparerArray2<T> : IComparer<T[][]>
        where T : IComparable<T>
    {
        public ComparerArray2() { }
        public int Compare(T[][]? x, T[][]? y) => Ex.Compare(x, y);
    }
    public class ComparerArray3<T> : IComparer<T[][][]>
        where T : IComparable<T>
    {
        public ComparerArray3() { }
        public int Compare(T[][][]? x, T[][][]? y) => Ex.Compare(x, y);
    }
    public class ComparerArray4<T> : IComparer<T[][][][]>
        where T : IComparable<T>
    {
        public ComparerArray4() { }
        public int Compare(T[][][][]? x, T[][][][]? y) => Ex.Compare(x, y);
    }
    public class ComparerIList<T> : IComparer<IList<T>>
        where T : IComparable<T>
    {
        public ComparerIList() { }
        public int Compare(IList<T>? x, IList<T>? y) => Ex.Compare(x, y);
    }
    public class ComparerComparison<T> : IComparer<T>
    {
        readonly Comparison<T> comparer;
        public ComparerComparison(Comparison<T> comparison) { comparer = comparison; }
        public int Compare([AllowNull] T x, [AllowNull] T y) => comparer(x!, y!);
    }
    public class ComparerReverse<T> : IComparer<T>
    {
        readonly IComparer<T> comparer;
        public ComparerReverse() { comparer = Comparer<T>.Default; }
        public ComparerReverse(IComparer<T> comparer) { this.comparer = comparer; }
        public ComparerReverse(Comparison<T> comparer) { this.comparer = new ComparerComparison<T>(comparer); }
        public int Compare([AllowNull] T x, [AllowNull] T y) => comparer.Compare(y, x);
    }
    #endregion

    #region generic classes
    public class ArrayInf2<T>
    {
        public T[,] A;
        Int2 L;
        public ArrayInf2(T[,] A) { this.A = A; this.L = A.Lengths(); }
        public bool IsIn(int i0, int i1) => (uint)i0 < (uint)L.v0 && (uint)i1 < (uint)L.v1;
        public T this[int i0, int i1]
        {
            get => IsIn(i0, i1) ? A[i0, i1] : default!;
            set { if (IsIn(i0, i1)) A[i0, i1] = value; }
        }
        public T this[Int2 i]
        {
            get => this[i.v0, i.v1];
            set => this[i.v0, i.v1] = value;
        }
    }
    public class ArrayInfOff2<T> : ArrayInf2<T>
    {
        public Int2 O;
        public ArrayInfOff2(T[,] A, Int2 o) : base(A) { this.O = o; }
        public ArrayInfOff2(T[,] A, int o0, int o1) : base(A) { this.O = new Int2(o0, o1); }
        public new bool IsIn(int i0, int i1) => base.IsIn(i0 + O.v0, i1 + O.v1);
        public new T this[int i0, int i1]
        {
            get => base[i0 + O.v0, i1 + O.v1];
            set => base[i0 + O.v0, i1 + O.v1] = value;
        }
        public new T this[Int2 i]
        {
            get => this[i.v0, i.v1];
            set => this[i.v0, i.v1] = value;
        }
    }

    [Serializable]
    public class PriorityQueue<TKey, TValue> where TKey : notnull
    {
        TKey[] keys;
        TValue[] values;
        readonly Comparison<TKey> compare;

        public PriorityQueue() : this(0) { }
        public PriorityQueue(int capacity) : this((Comparison<TKey>?)null, capacity) { }
        public PriorityQueue(IComparer<TKey> comparer) : this(comparer, 0) { }
        public PriorityQueue(Comparison<TKey>? compare) : this(compare, 0) { }
        public PriorityQueue(IComparer<TKey> comparer, int capacity) : this(comparer.Compare, capacity) { }
        public PriorityQueue(Comparison<TKey>? compare, int capacity)
        {
            this.compare = compare ?? Comparer<TKey>.Default.Compare;
            keys = new TKey[capacity];
            values = new TValue[capacity];
        }

        void Resize(int size)
        {
            Array.Resize(ref keys, size);
            Array.Resize(ref values, size);
        }
        public PriorityQueue(IEnumerable<KeyValuePair<TKey, TValue>> collection) : this()
        {
            foreach (var item in collection) Enqueue(item);
        }

        public int Count { get; protected set; }
        public Comparison<TKey> Compare => compare;

        public void Clear()
        {
            Array.Clear(keys, 0, Count);
            Array.Clear(values, 0, Count);
            Count = 0;
        }
        public KeyValuePair<TKey, TValue> Dequeue()
        {
            var result = new KeyValuePair<TKey, TValue>(keys[0], values[0]);
            var key = keys[Count - 1]; keys[Count - 1] = default!;
            var value = values[Count - 1]; values[Count - 1] = default!;
            int pos = 0;
            while (true)
            {
                int child = pos * 2 + 1;
                if (child >= Count) break;
                if (child + 1 < Count && compare(keys[child], keys[child + 1]) < 0) child++;
                if (compare(key, keys[child]) >= 0) break;
                keys[pos] = keys[child];
                values[pos] = values[child];
                pos = child;
            }
            keys[pos] = key;
            values[pos] = value;
            Count--;
            return result;
        }
        public void Add(KeyValuePair<TKey, TValue> item) => Enqueue(item.Key, item.Value);
        public void Add(TKey key, TValue value) => Enqueue(key, value);
        public void Enqueue(KeyValuePair<TKey, TValue> item) => Enqueue(item.Key, item.Value);
        public void Enqueue(TKey key, TValue value)
        {
            if (Count == keys.Length) Resize(Math.Max(4, keys.Length * 2));
            int pos = Count;
            while (pos > 0)
            {
                int parent = (pos - 1) / 2;
                if (compare(key, keys[parent]) <= 0) break;
                keys[pos] = keys[parent];
                values[pos] = values[parent];
                pos = parent;
            }
            keys[pos] = key;
            values[pos] = value;
            Count++;
        }
        public KeyValuePair<TKey, TValue> Peek() => new KeyValuePair<TKey, TValue>(keys[0], values[0]);
        public void TrimExcess() { Resize(Count); }
        public override string ToString() => $"Count = {Count}";
    }

    [Serializable]
    public class SortedList<T> : List<T>
    {
        protected IComparer<T> comparer;

        public SortedList() : this(null, 0) { }
        public SortedList(int capacity) : this(null, capacity) { }
        public SortedList(IComparer<T>? comparer) : this(comparer, 0) { }
        public SortedList(IComparer<T>? comparer, int capacity) : base(capacity)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public IComparer<T> Comparer => comparer;

        public bool AddOrDiscard(T item)
        {
            int index = IndexOf(item);
            if (index >= 0) return false;
            else { Insert(~index, item); return true; }
        }
        public bool AddOrOverwrite(T item)
        {
            int index = IndexOf(item);
            if (index >= 0) { this[index] = item; return false; }
            else { Insert(~index, item); return true; }
        }
        public T Pop()
        {
            T item = this[Count - 1];
            RemoveAt(Count - 1);
            return item;
        }

        public new int IndexOf(T item) => BinarySearch(item);
        public new void Add(T item)
        {
            int index = BinarySearch(item);
            if (index >= 0) ThrowException.Argument(nameof(item));
            Insert(~index, item);
        }
        public new bool Contains(T item) => BinarySearch(item) >= 0;
    }

    public class ListedList<T> : IList<T>
    {
        const int FixedSize = 1024, FixedHalfSize = FixedSize / 2;
        List<List<T>> listedlist = new List<List<T>>();

        public ListedList() { }
        protected T this[Int2 index]
        {
            get => listedlist[index.v0][index.v1];
            set => listedlist[index.v0][index.v1] = value;
        }
        public ListedList(IEnumerable<T> collection)
        {
            foreach (var item in collection) Add(item);
        }

        protected Int2 DecomposeIndex(int index)
        {
            int j = index;
            for (int i = 0; i < listedlist.Count; i++)
            {
                if (j < listedlist[i].Count) return new Int2(i, j);
                j -= listedlist[i].Count;
            }
            return ThrowException.ArgumentOutOfRange<Int2>(nameof(index));
        }
        protected int ComposeIndex(Int2 indexes)
        {
            int index = indexes.v1;
            for (int i = indexes.v0; --i >= 0;)
                index += listedlist[i].Count;
            return index;
        }
        protected Int2 IndexOf_(T item)
        {
            for (int i = 0; i < listedlist.Count; ++i)
            {
                int j = listedlist[i].IndexOf(item);
                if (j >= 0) return new Int2(i, j);
            }
            return ~new Int2(0, 0);
        }
        protected Int2 BinarySearch_(T item) => BinarySearch_(item, Comparer<T>.Default);
        protected Int2 BinarySearch_(T item, Comparer<T> comparer)
        {
            int i0 = 0, i1 = listedlist.Count;
            while (i0 < i1)
            {
                int i = (i0 + i1) / 2;
                int c = comparer.Compare(item, listedlist[i][0]);
                if (c == 0) return new Int2(i, 0);
                if (c < 0) i1 = i; else i0 = i + 1;
            }
            if (i0 == 0) return ~new Int2(0, 0);
            int ii = i0 - 1;
            var list = listedlist[ii];
            int j0 = 0, j1 = list.Count;
            while (j0 < j1)
            {
                int j = (j0 + j1) / 2;
                int c = comparer.Compare(item, list[j]);
                if (c == 0) return new Int2(ii, j);
                if (c < 0) j1 = j; else j0 = j + 1;
            }
            return ~new Int2(ii, j0);
        }

        protected void Insert_(Int2 indexes, T item)
        {
            if (indexes.v0 == listedlist.Count) listedlist.Add(new List<T>(FixedSize));
            if (listedlist[indexes.v0].Count == FixedSize)
            {
                listedlist.Insert(indexes.v0 + 1, new List<T>(FixedSize));
                listedlist[indexes.v0 + 1].AddRange(listedlist[indexes.v0].GetRange(FixedHalfSize, FixedHalfSize));
                listedlist[indexes.v0].RemoveRange(FixedHalfSize, FixedHalfSize);
                if (indexes.v1 >= FixedHalfSize) indexes -= new Int2(-1, FixedHalfSize);
            }
            listedlist[indexes.v0].Insert(indexes.v1, item);
            Count++;
        }
        protected void RemoveAt_(Int2 indexes)
        {
            Count--;
            listedlist[indexes.v0].RemoveAt(indexes.v1);
            if (listedlist[indexes.v0].Count == 0) listedlist.RemoveAt(indexes.v0);
        }

        public int RemoveAll(Predicate<T> match)
        {
            int count = 0;
            listedlist.RemoveAll(items =>
            {
                count += items.RemoveAll(match);
                return items.Count == 0;
            });
            Count -= count;
            return count;
        }
        public void CopyTo(T[] array)
        {
            int index = 0;
            foreach (var items in listedlist) { items.CopyTo(array, index); index += items.Count; }
        }
        public IEnumerable<T> Reverse()
        {
            for (int i = listedlist.Count; --i >= 0;)
            {
                var list = listedlist[i];
                for (int j = list.Count; --j >= 0;)
                    yield return list[j];
            }
        }
        public override string ToString() => $"Count = {Count}";

        public T Pop()
        {
            Count--;
            var list = listedlist[^1];
            T item = list[^1];
            list.RemoveAt(list.Count - 1);
            if (list.Count == 0) listedlist.RemoveAt(listedlist.Count - 1);
            return item;
        }

        public void Sort(Comparer<T> comparer)
        {
            var newlist = new ListedList<T>();
            foreach (var list in listedlist) { list.Sort(); list.Reverse(); }
            var head = listedlist.Select(list => list.Count - 1).ToList();
            var index = Enumerable.Range(0, listedlist.Count).ToList();
            int compare(int y, int x) => comparer.Compare(listedlist[x][head[x]], listedlist[y][head[y]]);
            index.Sort(compare);
            while (listedlist.Count > 0)
            {
                int most = index.Pop();
                newlist.Add(listedlist[most][head[most]]);
                if (--head[most] >= 0)
                {
                    int insert = ~index.BinarySearch(most, compare);
                    if (insert < 0) insert = ~insert;
                    index.Insert(insert, most);
                }
                else
                {
                    for (int i = index.Count; --i >= 0;)
                        if (index[i] > most) index[i]--;
                    head.RemoveAt(most);
                    listedlist.RemoveAt(most);
                }
            }
            listedlist = newlist.listedlist;
        }

        public int LetDistinct()
        {
            var comparer = EqualityComparer<T>.Default;
            int overlap = 0;
            for (int i = listedlist.Count; --i >= 0;)
            {
                var list = listedlist[i];
                for (int j = list.Count; --j > 0;)
                {
                    if (comparer.Equals(list[j], list[j - 1])) { list.RemoveAt(j); overlap++; }
                }
                if (i > 0)
                {
                    if (comparer.Equals(list[0], listedlist[i - 1].Last())) { list.RemoveAt(0); overlap++; }
                }
                if (list.Count == 0) listedlist.RemoveAt(i);
            }
            Count -= overlap;
            return overlap;
        }

        #region IList<T> メンバ
        public int IndexOf(T item)
        {
            Int2 indices = IndexOf_(item);
            if (indices.v0 >= 0) return ComposeIndex(indices);
            else return ~ComposeIndex(~indices);
        }
        public virtual void Insert(int index, T item) => Insert_(DecomposeIndex(index), item);
        public void RemoveAt(int index) => RemoveAt_(DecomposeIndex(index));
        public T this[int index]
        {
            get => this[DecomposeIndex(index)];
            set => this[DecomposeIndex(index)] = value;
        }
        #endregion
        #region ICollection<T> メンバ
        public void Add(T item)
        {
            if (listedlist.Count == 0 || listedlist[^1].Count == FixedSize) listedlist.Add(new List<T>(FixedSize));
            listedlist[^1].Add(item);
            Count++;
        }
        public void Clear()
        {
            Count = 0;
            listedlist.Clear();
        }
        public bool Contains(T item) => IndexOf_(item).v0 >= 0;
        public void CopyTo(T[] array, int arrayIndex)
        {
            int index = arrayIndex;
            foreach (var items in listedlist)
            {
                items.CopyTo(array, index);
                index += items.Count;
            }
        }
        public int Count { get; protected set; }
        public bool IsReadOnly => false;
        public bool Remove(T item)
        {
            Int2 indices = IndexOf_(item);
            if (indices.v0 >= 0) { RemoveAt_(indices); return true; }
            else { return false; }
        }
        #endregion
        #region IEnumerable<T> メンバ
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var items in listedlist)
                foreach (var item in items)
                    yield return item;
        }
        #endregion
        #region IEnumerable メンバ
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }

    [Serializable]
    public class SortedListedList<T> : ListedList<T>
    {
        protected IComparer<T> comparer;
        public SortedListedList() : this((IComparer<T>?)null) { }
        public SortedListedList(IComparer<T>? comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }
        public SortedListedList(IEnumerable<T> collection) : this(null, collection) { }
        public SortedListedList(IComparer<T>? comparer, IEnumerable<T> collection) : this(comparer)
        {
            foreach (var item in collection) Add(item);
        }

        public int BinarySearch(T item) => ComposeIndex(BinarySearch_(item));
        public new bool Add(T item)
        {
            Int2 indices = BinarySearch_(item);
            if (indices.v0 >= 0) return false;
            else { Insert_(~indices, item); return true; }
        }
        [DoesNotReturn] public override void Insert(int index, T item) => ThrowException.NotImplemented();
        public bool AddOrDiscard(T item)
        {
            Int2 indices = BinarySearch_(item);
            if (indices.v0 >= 0) return false;
            else { Insert_(~indices, item); return true; }
        }
        public bool AddOrOverwrite(T item)
        {
            Int2 indices = BinarySearch_(item);
            if (indices.v0 >= 0) { this[indices] = item; return false; }
            else { Insert_(~indices, item); return true; }
        }
        public T FindOrDefault(T item)
        {
            Int2 index = BinarySearch_(item);
            return index.v0 >= 0 ? this[index] : default!;
        }
        public new bool Contains(T item) => BinarySearch_(item).v0 >= 0;
    }
    #endregion

    #region BitStream
    public class BitStreamReader
    {
        public Stream BaseStream { get; private set; }
        int buffer;
        int count;
        public BitStreamReader(Stream stream) { BaseStream = stream; }
        public int ReadBit()
        {
            if (count == 0)
            {
                buffer = BaseStream.ReadByte();
                if (buffer == -1) return -1;
                count = 8;
            }
            return (buffer >> (7 - (--count))) & 1;
        }
        public bool EndOfStream() => count == 0 && BaseStream.EndOfStream();
    }
    public class BitStreamWriter : IDisposable
    {
        public Stream BaseStream { get; private set; }
        int buffer;
        int count;
        public BitStreamWriter(Stream stream) { BaseStream = stream; }
        public void WriteBit(int x)
        {
            buffer |= (x & 1) << count++;
            if (count == 8) Flush();
        }
        public void Flush()
        {
            BaseStream.WriteByte((byte)buffer);
            buffer = 0;
            count = 0;
        }
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;
            if (disposing) { Flush(); }
            disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    #endregion

    // Extended constructors
#pragma warning disable CA1716
    public static partial class New
#pragma warning restore CA1716
    {
        #region miscs
        public static DateTime DateTime(double sec) => new DateTime((long)(sec * 10000000 + 0.5));
        public static TimeSpan TimeSpan(double sec) => new TimeSpan((long)(sec * 10000000 + 0.5));
        public static KeyValuePair<TKey, TValue> KeyValuePair<TKey, TValue>(TKey key, TValue value) => new KeyValuePair<TKey, TValue>(key, value);
        public static Fix<T> Fix<T>(int n) => new Fix<T>(new T[n]);
        public static Fix<T> Fix<T>(T[] a) => new Fix<T>(a);
        public static Fix<T> Fix<T>(T[,] a) => new Fix<T>(a);
        public static Fix<T> Fix<T>(T[,,] a) => new Fix<T>(a);
        public static Fix<T> Fix<T>(Array<T> a) => new Fix<T>(a.A);
        public static Fix<T> Fix<T>(Array a) => new Fix<T>(a);
        public static ArrayInf2<T> ArrayInf2<T>(T[,] a) => new ArrayInf2<T>(a);
        public static ArrayInfOff2<T> ArrayInfOff2<T>(T[,] a, Int2 o) => new ArrayInfOff2<T>(a, o);
        public static ParallelOptions ParallelOptions(int degree) => new ParallelOptions { MaxDegreeOfParallelism = degree };

        public static Func<R> Cache<R>(this Func<R> create)
        {
            var l = new object();
            R cache = default!;
            var created = false;
            return () =>
            {
                lock (l)
                {
                    if (!created) { cache = create(); created = true; }
                }
                return cache;
            };
        }
        public static Func<T, R> Cache<T, R>(this Func<T, R> create) where T : notnull
        {
            var dictionary = new Dictionary<T, R>();
            return (arg) =>
            {
                lock (dictionary)
                {
                    if (!dictionary.ContainsKey(arg)) dictionary.Add(arg, create(arg));
                    return dictionary[arg];
                }
            };
        }
        public static Func<T0, T1, R> Cache<T0, T1, R>(this Func<T0, T1, R> create) where T0 : notnull where T1 : notnull
        {
            var dictionary = new Dictionary<(T0, T1), R>();
            return (arg0, arg1) =>
            {
                lock (dictionary)
                {
                    if (!dictionary.ContainsKey((arg0, arg1))) dictionary.Add((arg0, arg1), create(arg0, arg1));
                    return dictionary[(arg0, arg1)];
                }
            };
        }
        #endregion

        #region Array
        public static T[] Array<T>(int n, Func<int, T> f)
        {
            var r = new T[n];
            for (int i = 0; i < r.Length; i++)
                r[i] = f(i);
            return r;
        }
        public static T[,] Array<T>(int n0, int n1, Func<int, int, T> f)
        {
            var r = new T[n0, n1];
            Int2 i;
            for (i.v0 = 0; i.v0 < n0; i.v0++)
                for (i.v1 = 0; i.v1 < n1; i.v1++)
                    r[i.v0, i.v1] = f(i.v0, i.v1);
            return r;
        }
        public static T[,,] Array<T>(int n0, int n1, int n2, Func<int, int, int, T> f)
        {
            var r = new T[n0, n1, n2];
            Int3 i;
            for (i.v0 = 0; i.v0 < n0; i.v0++)
                for (i.v1 = 0; i.v1 < n1; i.v1++)
                    for (i.v2 = 0; i.v2 < n2; i.v2++)
                        r[i.v0, i.v1, i.v2] = f(i.v0, i.v1, i.v2);
            return r;
        }
        public static T[,] Array<T>(Int2 n, Func<Int2, T> f)
        {
            var r = new T[n.v0, n.v1];
            Int2 i;
            for (i.v0 = 0; i.v0 < n.v0; i.v0++)
                for (i.v1 = 0; i.v1 < n.v1; i.v1++)
                    r[i.v0, i.v1] = f(i);
            return r;
        }
        public static T[,,] Array<T>(Int3 n, Func<Int3, T> f)
        {
            var r = new T[n.v0, n.v1, n.v2];
            Int3 i;
            for (i.v0 = 0; i.v0 < n.v0; i.v0++)
                for (i.v1 = 0; i.v1 < n.v1; i.v1++)
                    for (i.v2 = 0; i.v2 < n.v2; i.v2++)
                        r[i.v0, i.v1, i.v2] = f(i);
            return r;
        }

        public static T[] ParallelArray<T>(int n, int degree, Func<int, T> f) { var r = new T[n]; Ex.ParallelFor(n, degree, i => r[i] = f(i)); return r; }
        public static T[,] ParallelArray<T>(Int2 n, int degree, Func<Int2, T> f) { var r = New.Array<T>(n); Ex.ParallelForEach(Ex.Range(n), degree, i => r.Item(i) = f(i)); return r; }
        public static T[,,] ParallelArray<T>(Int3 n, int degree, Func<Int3, T> f) { var r = New.Array<T>(n); Ex.ParallelForEach(Ex.Range(n), degree, i => r.Item(i) = f(i)); return r; }
        public static T[,] ParallelArray<T>(int n0, int n1, int degree, Func<int, int, T> f) { var n = new Int2(n0, n1); var r = New.Array<T>(n); Ex.ParallelForEach(Ex.Range(n), degree, i => r.Item(i) = f(i.v0, i.v1)); return r; }
        public static T[,,] ParallelArray<T>(int n0, int n1, int n2, int degree, Func<int, int, int, T> f) { var n = new Int3(n0, n1, n2); var r = New.Array<T>(n); Ex.ParallelForEach(Ex.Range(n), degree, i => r.Item(i) = f(i.v0, i.v1, i.v2)); return r; }
        public static T[] ParallelArray<T>(int n, Func<int, T> f) => ParallelArray(n, 0, f);
        public static T[,] ParallelArray<T>(Int2 n, Func<Int2, T> f) => ParallelArray(n, 0, f);
        public static T[,,] ParallelArray<T>(Int3 n, Func<Int3, T> f) => ParallelArray(n, 0, f);
        public static T[,] ParallelArray<T>(int n0, int n1, Func<int, int, T> f) => ParallelArray(n0, n1, 0, f);
        public static T[,,] ParallelArray<T>(int n0, int n1, int n2, Func<int, int, int, T> f) => ParallelArray(n0, n1, n2, 0, f);

        public static List<T> List<T>(int n, Func<int, T> f)
        {
            var r = new List<T>(n);
            for (int i = 0; i < n; i++)
                r.Add(f(i));
            return r;
        }

        public static T[] Array<T>(int n, T item)
        {
            var r = new T[n];
            for (int i = 0; i < r.Length; i++)
                r[i] = item;
            return r;
        }
        public static T[,] Array<T>(Int2 n, T item)
        {
            var r = new T[n.v0, n.v1];
            Int2 i;
            for (i.v0 = 0; i.v0 < n.v0; i.v0++)
                for (i.v1 = 0; i.v1 < n.v1; i.v1++)
                    r[i.v0, i.v1] = item;
            return r;
        }
        public static T[,,] Array<T>(Int3 n, T item)
        {
            var r = new T[n.v0, n.v1, n.v2];
            Int3 i;
            for (i.v0 = 0; i.v0 < n.v0; i.v0++)
                for (i.v1 = 0; i.v1 < n.v1; i.v1++)
                    for (i.v2 = 0; i.v2 < n.v2; i.v2++)
                        r[i.v0, i.v1, i.v2] = item;
            return r;
        }
        public static T[,] Array<T>(int n0, int n1, T item) => Array(new Int2(n0, n1), item);
        public static T[,,] Array<T>(int n0, int n1, int n2, T item) => Array(new Int3(n0, n1, n2), item);
        public static List<T> List<T>(int n, T item)
        {
            var r = new List<T>(n);
            for (int i = 0; i < n; i++)
                r.Add(item);
            return r;
        }

        public static Array Array<T>(int[] ns) => System.Array.CreateInstance(typeof(T), ns);
        public static T[] Array<T>(int n) => new T[n];
        public static T[,] Array<T>(int n0, int n1) => new T[n0, n1];
        public static T[,,] Array<T>(int n0, int n1, int n2) => new T[n0, n1, n2];
        public static T[,] Array<T>(Int2 n) => new T[n.v0, n.v1];
        public static T[,,] Array<T>(Int3 n) => new T[n.v0, n.v1, n.v2];
        #endregion

        #region IEnumerable
        public static IEnumerable<T> IEnumerable<T>(int count, Func<int, T> f)
        {
            for (int i = 0; i < count; i++) yield return f(i);
        }
        #endregion

        #region culture-based
        public static StreamReader? StreamReader(string path)
        {
            var encoding = Ex.DetectEncoding(path);
            if (encoding is null) return null;
            return new StreamReader(path, encoding);
        }
        #endregion
    }

    // Utility and extension methods
    public static partial class Ex
    {
        #region unsafe
        public unsafe struct Struct64Byte { public fixed byte _[64]; }
        public unsafe struct Struct32Byte { public fixed byte _[32]; }
        public unsafe struct Struct16Byte { public fixed byte _[16]; }

        //public static ref T I<T>(this ref T a, int i) where T : unmanaged => ref Unsafe.Add(ref a, i);
        public static ref T Ref<T>(this T[] a) where T : unmanaged => ref a[0];
        public static ref T Ref<T>(this T[,] a) where T : unmanaged => ref a[0, 0];
        public static ref T Ref<T>(this T[,,] a) where T : unmanaged => ref a[0, 0, 0];

        public static unsafe T* Cnew<T>(int size) where T : unmanaged
        {
            var p = (T*)Marshal.AllocHGlobal(size * sizeof(T));
            for (int i = size; --i >= 0;) p[i] = default;
            return p;
        }
        public static unsafe void Cdelete<T>(T* pointer) where T : unmanaged
        {
            Marshal.FreeHGlobal((IntPtr)pointer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)] public static bool IsSameType<T0, T1>() => typeof(T0) == typeof(T1);
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)] public static bool IsClass<T>() => default(T) is null;
        #endregion

        #region non-extension methods
        public static readonly int ProcessorCount = Environment.ProcessorCount;
        public static int ParallelDegree(int degree = 0) => (degree > 0) ? degree.Min(ProcessorCount) : (ProcessorCount + degree).Max(1);
        public static int SizeOf<T>() => Unsafe.SizeOf<T>();  // sizeof(T); Marshal.SizeOf<T>();
        public static void Swap<T>(ref T x, ref T y) { T z = x; x = y; y = z; }
        public static UInt16 ReverseEndian(UInt16 x)
        {
            return (UInt16)((x >> 8) | (x << 8));
        }
        public static UInt32 ReverseEndian(UInt32 x)
        {
            var y = (x >> 16) | (x << 16);
            return ((y & 0xff00ff00) >> 8) | ((y & 0x00ff00ff) << 8);
        }
        public static UInt64 ReverseEndian(UInt64 x)
        {
            var y = (x >> 32) | (x << 32);
            var z = ((y & 0xffff0000ffff0000) >> 16) | ((y & 0x00ffff0000ffff) << 16);
            return ((z & 0xff00ff00ff00ff00) >> 8) | ((z & 0x00ff00ff00ff00ff) << 8);
        }
        public static Int16 ReverseEndian(Int16 x) => (Int16)ReverseEndian((UInt16)x);
        public static Int32 ReverseEndian(Int32 x) => (Int32)ReverseEndian((UInt32)x);
        public static Int64 ReverseEndian(Int64 x) => (Int64)ReverseEndian((UInt64)x);
        public static unsafe Single ReverseEndian(Single x) { var r = ReverseEndian(*(UInt32*)&x); return *(Single*)&r; }
        public static unsafe Double ReverseEndian(Double x) { var r = ReverseEndian(*(UInt64*)&x); return *(Double*)&r; }
        public static unsafe T ReverseEndian<T>(T x) where T : unmanaged
        {
            T z = default;
            for (int i = sizeof(T); --i >= 0;)
                ((byte*)&z)[i] = ((byte*)&x)[sizeof(T) - 1 - i];
            return z;
        }

        public static unsafe bool MemoryEquals<T>(in T x, in T y) where T : unmanaged
        {
            fixed (T* p = &x, q = &y) return MemoryEquals((IntPtr)p, (IntPtr)q, sizeof(T));
        }
        public static unsafe int MemoryCompare<T>(in T x, in T y) where T : unmanaged
        {
            fixed (T* p = &x, q = &y) return MemoryCompare((IntPtr)p, (IntPtr)q, sizeof(T));
        }
        public static unsafe void MemoryCopy<T>(in T x, out T y) where T : unmanaged
        {
            fixed (T* p = &x, q = &y) MemoryCopy((IntPtr)p, (IntPtr)q, sizeof(T));
        }

        public static unsafe bool MemoryEquals(IntPtr p, IntPtr q, int count)
        {
            if (p == q) return true;
            for (var i = count / 8; --i >= 0; p += 8, q += 8)
                if (*(long*)q != *(long*)p) return false;
            for (var i = (int)(count & 7); --i >= 0; p += 1, q += 1)
                if (*(byte*)p != *(byte*)q) return false;
            return true;
        }
        public static unsafe int MemoryCompare(IntPtr p, IntPtr q, int count)
        {
            if (p == q) return 0;
            var n = (int)(count & 7);
            for (var i = count / 8; --i >= 0; p += 8, q += 8)
                if (*(long*)q != *(long*)p) { n = 8; break; }
            for (var i = n; --i >= 0; p += 1, q += 1)
            {
                var d = *(byte*)p - *(byte*)q;
                if (d != 0) return d > 0 ? +1 : -1;
            }
            return 0;
        }
        public static void MemoryZero<T>(Span<T> p)
        {
            for (int i = p.Length; --i >= 0;) p[i] = default!;
        }
        public static void MemoryCopy<T>(Span<T> p, Span<T> q)
        {
            if (p.Length != q.Length) ThrowException.SizeMismatch();
            for (int i = q.Length; --i >= 0;) q[i] = p[i];
        }
        public static unsafe void MemoryCopy<T>(T* p, T* q, int count) where T : unmanaged => MemoryCopy((IntPtr)p, (IntPtr)q, count * sizeof(T));
        public static unsafe void MemoryCopy(IntPtr p, IntPtr q, int count) => Buffer.MemoryCopy((void*)p, (void*)q, count, count);
        public static unsafe void MemoryCopy_Code1(IntPtr p, IntPtr q, int count)
        {
            if (p == q) return;
            if ((void*)p < (void*)q)
            {
                p = (IntPtr)((byte*)p + count);
                q = (IntPtr)((byte*)q + count);
                for (var i = count & 7; --i >= 0;) *(byte*)(q -= 1) = *(byte*)(p -= 1);
                for (var i = count / 8; --i >= 0;) *(long*)(q -= 8) = *(long*)(p -= 8);
            }
            else
            {
                for (var i = count / 8; --i >= 0; p += 8, q += 8) *(long*)q = *(long*)p;
                for (var i = count & 7; --i >= 0; p += 1, q += 1) *(byte*)q = *(byte*)p;
            }
        }
        public static unsafe void MemorySwap(IntPtr p, IntPtr q, int count)
        {
            if (p == q) return;
            for (var i = count / 32; --i >= 0; p += 32, q += 32)
                Ex.Swap(ref *(Struct32Byte*)q, ref *(Struct32Byte*)p);
            for (var i = (int)(count & 31) / 8; --i >= 0; p += 8, q += 8)
                Ex.Swap(ref *(long*)q, ref *(long*)p);
            for (var i = (int)(count & 7); --i >= 0; p += 1, q += 1)
                Ex.Swap(ref *(byte*)q, ref *(byte*)p);
        }
        public static unsafe void MemoryShift(IntPtr p, int count, int shift)
        {
            if (count <= 0) return;
            if (shift < 0 || shift >= count) shift = Mt.Mod_(shift, count);
            if (shift == 0) return;
            var c = Ex.DefaultBufferSize;
            var u = count - shift;
            var v = shift;
            while (true)
            {
                if (u == v) { MemorySwap(p, p + u, u); break; }
                if (u > c && v > c)
                {
                    if (u < v) { MemorySwap(p, p + u, u); p += u; v -= u; }
                    else { MemorySwap(p + (u - v), p + u, v); u -= v; }
                    continue;
                }
                //var buf = new byte[c];
                var buf = Ex.TakeBuffer(c);
                fixed (byte* b = buf)
                {
                    if (u < v) { MemoryCopy(p, (IntPtr)b, u); MemoryCopy(p + u, p, v); MemoryCopy((IntPtr)b, p + v, u); }
                    else { MemoryCopy(p + u, (IntPtr)b, v); MemoryCopy(p, p + v, u); MemoryCopy((IntPtr)b, p, v); }
                }
                Ex.ReturnBuffer(buf);
                break;
            }
        }
        public static void MemoryShift_Code1(IntPtr p, int count, int shift)
        {
            if (count <= 0) return;
            if (shift < 0 || shift >= count) shift = Mt.Mod_(shift, count);
            if (shift == 0) return;
            var u = count - shift;
            var v = shift;
            if (u % 8 == 0 && v % 8 == 0) main<long>(); else main<byte>();
            void main<T>() where T : unmanaged
            {
                MemoryReverse<T>(p, u);
                MemoryReverse<T>(p + u, v);
                MemoryReverse<T>(p, u + v);
            }
        }
        public static unsafe void MemoryReverse<T>(IntPtr p, int count) where T : unmanaged
        {
            if (count <= 0) return;
            if (count % sizeof(T) != 0) ThrowException.SizeMismatch();
            var q = p + count - sizeof(T);
            for (var i = (int)(count / sizeof(T)) / 2; --i >= 0; p += sizeof(T), q -= sizeof(T))
                Ex.Swap(ref *(T*)p, ref *(T*)q);
        }

        public const int DefaultBufferSize = 0x8000;  //32KiB
        public static byte[] TakeBuffer() => new byte[DefaultBufferSize];
        public static byte[] TakeBuffer(int size) => new byte[size];
        public static void ReturnBuffer(byte[] _) { }

        // System.Web.Util.HashCodeCombiner, System.Tuple
        public static int CombineHashCodes(int h1, int h2) => (((h1 << 5) + h1) ^ h2);
        public static int CombineHashCodes(int h1, int h2, int h3) => CombineHashCodes(CombineHashCodes(h1, h2), h3);
        public static int CombineHashCodes(int h1, int h2, int h3, int h4) => CombineHashCodes(CombineHashCodes(h1, h2), CombineHashCodes(h3, h4));
        public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5) => CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), h5);
        public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6) => CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6));
        public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7) => CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7));
        public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8) => CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7, h8));
        #endregion

        #region object
        public static T[] CloneX<T>(this Span<T> a) where T : ICloneable => a.ToArray();
        public static T CloneX<T>(this T obj) where T : ICloneable => (T)obj.Clone();
        public static void With<T>(this T obj, Action<T> method) => method(obj);
        public static R With<T, R>(this T obj, Func<T, R> method) => method(obj);
        #endregion

        #region Int2, Int3, Double2, Double3, Float2, Float3
        public static ref int I(ref this Int2 x, int index) => ref (index == 0 ? ref x.v0 : ref x.v1);
        public static ref int I(ref this Int3 x, int index) => ref (index == 0 ? ref x.v0 : ref (index == 1 ? ref x.v1 : ref x.v2));
        public static ref float I(ref this Single2 x, int index) => ref (index == 0 ? ref x.v0 : ref x.v1);
        public static ref float I(ref this Single3 x, int index) => ref (index == 0 ? ref x.v0 : ref (index == 1 ? ref x.v1 : ref x.v2));
        public static ref double I(ref this Double2 x, int index) => ref (index == 0 ? ref x.v0 : ref x.v1);
        public static ref double I(ref this Double3 x, int index) => ref (index == 0 ? ref x.v0 : ref (index == 1 ? ref x.v1 : ref x.v2));
        #endregion

        #region Span<T>, Array<T>
        public static unsafe T* P<T>(this Fix<T> f) where T : unmanaged => (T*)(IntPtr)f;
        public static unsafe T* P<T>(this Fix<T> f, int offset) where T : unmanaged => (T*)(IntPtr)f + offset;

        public static Span<T> AsSpan<T>(this T[,] a) => MemoryMarshal.CreateSpan(ref a[0, 0], a.Length);
        public static Span<T> AsSpan<T>(this T[,] a, int start) => a.AsSpan().Slice(start);
        public static Span<T> AsSpan<T>(this T[,] a, int start, int length) => a.AsSpan().Slice(start, length);
        public static Span<T> AsSpan<T>(this T[,,] a) => MemoryMarshal.CreateSpan(ref a[0, 0, 0], a.Length);
        public static Span<T> AsSpan<T>(this T[,,] a, int start) => a.AsSpan().Slice(start);
        public static Span<T> AsSpan<T>(this T[,,] a, int start, int length) => a.AsSpan().Slice(start, length);
        public static Span<T> AsSpan<T>(this T[,,,] a) => MemoryMarshal.CreateSpan(ref a[0, 0, 0, 0], a.Length);
        public static Span<T> AsSpan<T>(this T[,,,] a, int start) => a.AsSpan().Slice(start);
        public static Span<T> AsSpan<T>(this T[,,,] a, int start, int length) => a.AsSpan().Slice(start, length);
        public static Span<T> AsSpan<T>(this Array a) => a.Rank switch { 1 => ((T[])a).AsSpan(), 2 => ((T[,])a).AsSpan(), 3 => ((T[,,])a).AsSpan(), 4 => ((T[,,,])a).AsSpan(), _ => a.AsSpan_<T>() };
        public static unsafe Span<T> AsSpan_<T>(this Array a)
        {
            var H = GCHandle.Alloc(a, GCHandleType.Pinned);
            var P = Marshal.UnsafeAddrOfPinnedArrayElement(a, 0);
            var s = MemoryMarshal.CreateSpan(ref Unsafe.AsRef<T>((void*)P), a.Length);
            H.Free();
            return s;
            //using var f = New.Fix<T>(a); return f.AsSpan();
        }
        public static Span<T> AsSpan<T>(this Array a, int start) => a.AsSpan<T>().Slice(start);
        public static Span<T> AsSpan<T>(this Array a, int start, int length) => a.AsSpan<T>().Slice(start, length);
        public static Span<T> AsSpan<T>(ref T value) where T : struct => MemoryMarshal.CreateSpan(ref value, 1);
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(ref T value) where T : struct => MemoryMarshal.CreateReadOnlySpan(ref value, 1);  //inにしたいがライブラリがrefしか受け取らない
        public static Span<R> Cast<T, R>(this Span<T> span) where T : struct where R : struct => MemoryMarshal.Cast<T, R>(span);
        public static ReadOnlySpan<R> Cast<T, R>(this ReadOnlySpan<T> span) where T : struct where R : struct => MemoryMarshal.Cast<T, R>(span);

        public static Span<byte> AsBytes<T>(this Span<T> span) where T : struct => MemoryMarshal.AsBytes(span);
        public static ReadOnlySpan<byte> AsBytes<T>(this ReadOnlySpan<T> span) where T : struct => MemoryMarshal.AsBytes(span);
        public static Span<T> As<T>(this Span<byte> span) where T : struct => span.Cast<byte, T>();
        public static ReadOnlySpan<T> As<T>(this ReadOnlySpan<byte> span) where T : struct => span.Cast<byte, T>();

        public static Span<Array<T>> AsArray<T>(this Span<T[]> span) => MemoryMarshal.CreateSpan(ref Unsafe.As<T[], Array<T>>(ref span[0]), span.Length);
        public static Span<Array<T>> AsArray<T>(this Span<T[,]> span) => MemoryMarshal.CreateSpan(ref Unsafe.As<T[,], Array<T>>(ref span[0]), span.Length);
        public static Span<Array<T>> AsArray<T>(this Span<T[,,]> span) => MemoryMarshal.CreateSpan(ref Unsafe.As<T[,,], Array<T>>(ref span[0]), span.Length);
        public static Span<Array<T>> AsArray<T>(this Span<T[,,,]> span) => MemoryMarshal.CreateSpan(ref Unsafe.As<T[,,,], Array<T>>(ref span[0]), span.Length);

        public static Span<T> Slice<T>(this Span<T> a, Index_ r) => a.Slice(r.GetOffset(a.Length));
        public static Span<T> Slice<T>(this Span<T> a, Range_ r) { var (o, n) = r.GetOffsetAndLength(a.Length); return a.Slice(o, n); }
        public static Span<T> AsSpan<T>(this T[] a, Index_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this T[] a, Range_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this T[,] a, Index_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this T[,] a, Range_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this T[,,] a, Index_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this T[,,] a, Range_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this T[,,,] a, Index_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this T[,,,] a, Range_ r) => a.AsSpan().Slice(r);
        public static Span<T> AsSpan<T>(this Array a, Index_ r) => a.AsSpan<T>().Slice(r);
        public static Span<T> AsSpan<T>(this Array a, Range_ r) => a.AsSpan<T>().Slice(r);

        public static T FirstOrDefault<T>(this Span<T> a) => a.Length > 0 ? a[0] : default!;
        public static T LastOrDefault<T>(this Span<T> a) => a.Length > 0 ? a[^1] : default!;

        public static void Swap<T>(this Span<T> a, int i, int j) { T t = a[i]; a[i] = a[j]; a[j] = t; }
        public static bool IsSymmetric<T>(this Span<T> a) where T : IEquatable<T>
        {
            for (int i = a.Length / 2; --i >= 0;)
                if (!a[i].Equals(a[^(i + 1)])) return false;
            return true;
        }

        public static Array<T> As<T>(this Array a) => new Array<T>(a);
        public static Array<T> As<T>(this T[] a) => a;
        public static Array<T> As<T>(this T[,] a) => a;
        public static Array<T> As<T>(this T[,,] a) => a;
        public static Array<T> As<T>(this T[,,,] a) => a;
        public static IEnumerable<Array<T>> AsArray<T>(this IEnumerable<T[]> a) => a.Select(As);
        public static IEnumerable<Array<T>> AsArray<T>(this IEnumerable<T[,]> a) => a.Select(As);
        public static IEnumerable<Array<T>> AsArray<T>(this IEnumerable<T[,,]> a) => a.Select(As);
        public static IEnumerable<Array<T>> AsArray<T>(this IEnumerable<T[,,,]> a) => a.Select(As);

        public static IEnumerable<int> ForEach((int n, int s, int c)[] L)
        {
            var I = new (int o, int i)[L.Length];
            for (int d = 0; ; d++)
            {
                I[d].o += L[d].s;
                if (d == L.Length - 1)
                {
                    int c = L[d].c;
                    int o = I[d].o;
                    for (int i = 0; i < c; i++) yield return o + i;
                    do { if (d-- == 0) yield break; }
                    while (++I[d].i == L[d].c);
                }
                I[d + 1] = ((I[d].o + I[d].i) * L[d + 1].n, 0);
            }
        }
        public static IEnumerable<int> ForEach((int n, int s, int c)[] L, int d, int offset)
        {
            offset += L[d].s;
            var c = L[d].c;
            if (d + 1 == L.Length)
                for (int i = 0; i < c; i++) yield return offset + i;
            else
                for (int i = 0; i < c; i++)
                {
                    var next = ForEach(L, d + 1, (offset + i) * L[d + 1].n);
                    foreach (var e in next) yield return e;
                }
        }

        public static void ForEach(Span<int> lengths, Span<Range_> ranges, Action<int> f)
        {
            if (lengths.Length != ranges.Length) ThrowException.SizeMismatch();
            var lon = new (int l, int o, int n)[lengths.Length];
            for (int d = lon.Length; --d >=0;) { var l = lengths[d]; var (o, n) = ranges[d].GetOffsetAndLength(l); lon[d] = (l, o, n); }
            nest(0, 0);
            void nest(int d, int offset)
            {
                var (l, o, n) = lon[d];
                o += offset * l;
                if (d + 1 == lon.Length)
                    for (int i = 0; i < n; i++) f(o + i);
                else
                    for (int i = 0; i < n; i++) nest(d + 1, o + i);
            }
        }
        public delegate void ActionRef<T>(ref T v);
        public static void ForEach<T>(Array<T> a, Span<Range_> r, ActionRef<T> f)
        {
            using var a_ = New.Fix(a);
            Span<int> l = stackalloc int[a.Rank];
            for (int i = l.Length; --i >= 0;) l[i] = a.GetLength(i);
            ForEach(l, r, i => f(ref a_[i]));
        }
        public static void ForEach<T>(this (Array<T> a, Range_ r) _, ActionRef<T> f) => ForEach(_.a, stackalloc[] { _.r }, f);
        public static void ForEach<T>(this (Array<T> a, Range_ r0, Range_ r1) _, ActionRef<T> f) => ForEach(_.a, stackalloc[] { _.r0, _.r1 }, f);
        public static void ForEach<T>(this (Array<T> a, Range_ r0, Range_ r1, Range_ r2) _, ActionRef<T> f) => ForEach(_.a, stackalloc[] { _.r0, _.r1, _.r2 }, f);
        #endregion

        #region Array
        public static ref T Item<T>(this T[] a, int i) => ref a[i];
        public static ref T Item<T>(this T[,] a, Int2 i) => ref a[i.v0, i.v1];
        public static ref T Item<T>(this T[,,] a, Int3 i) => ref a[i.v0, i.v1, i.v2];
        public static (T0, T1) Item<T0, T1>(this (T0[], T1[]) a, int i) => (a.Item1[i], a.Item2[i]);
        public static (T0, T1) Item<T0, T1>(this (T0[,], T1[,]) a, Int2 i) => (a.Item1.Item(i), a.Item2.Item(i));
        public static (T0, T1) Item<T0, T1>(this (T0[,,], T1[,,]) a, Int3 i) => (a.Item1.Item(i), a.Item2.Item(i));
        public static (T0, T1, T2) Item<T0, T1, T2>(this (T0[], T1[], T2[]) a, int i) => (a.Item1[i], a.Item2[i], a.Item3[i]);
        public static (T0, T1, T2) Item<T0, T1, T2>(this (T0[,], T1[,], T2[,]) a, Int2 i) => (a.Item1.Item(i), a.Item2.Item(i), a.Item3.Item(i));
        public static (T0, T1, T2) Item<T0, T1, T2>(this (T0[,,], T1[,,], T2[,,]) a, Int3 i) => (a.Item1.Item(i), a.Item2.Item(i), a.Item3.Item(i));

        public static int[] GetLengths(this Array a) => New.Array(a.Rank, i => a.GetLength(i));
        public static int Lengths<T>(this T[] a) => a.Length;
        public static Int2 Lengths<T>(this T[,] a) => new Int2(a.GetLength(0), a.GetLength(1));
        public static Int3 Lengths<T>(this T[,,] a) => new Int3(a.GetLength(0), a.GetLength(1), a.GetLength(2));

        public static int SameTotalLength(Array a0, Array a1)
        {
            var n = a0.Length;
            if (n != a1.Length) ThrowException.SizeMismatch();
            return n;
        }

        public static void SizeCheck<T0, T1>(Span<T0> a0, Span<T1> a1)
        {
            if (a0.Length != a1.Length) ThrowException.SizeMismatch();
        }
        public static void SizeCheck<T0, T1, T2>(Span<T0> a0, Span<T1> a1, Span<T2> a2)
        {
            if (a0.Length != a1.Length || a0.Length != a2.Length) ThrowException.SizeMismatch();
        }
        public static int SameLength<T0, T1>(Span<T0> a0, Span<T1> a1) { SizeCheck(a0, a1); return a0.Length; }
        public static int SameLength<T0, T1, T2>(Span<T0> a0, Span<T1> a1, Span<T2> a2) { SizeCheck(a0, a1, a2); return a0.Length; }

        public static void SizeCheck(Array a0, Array a1)
        {
            if (a0.Rank != a1.Rank) ThrowException.SizeMismatch();
            for (int d = a0.Rank; --d >= 0;)
                if (a0.GetLength(d) != a1.GetLength(d)) ThrowException.SizeMismatch();
        }
        public static void SizeCheck(Array a0, Array a1, Array a2) { SizeCheck(a0, a1); SizeCheck(a0, a2); }
        public static int SameLength(Array a0, Array a1) { SizeCheck(a0, a1); return a0.Length; }
        public static int SameLength(Array a0, Array a1, Array a2) { SizeCheck(a0, a1, a2); return a0.Length; }
        public static int SameLengths<T0, T1>(Span<T0> a0, Span<T1> a1) { SizeCheck(a0, a1); return a0.Length; }
        public static int SameLengths<T0, T1>(T0[] a0, T1[] a1) { SizeCheck(a0, a1); return a0.Length; }
        public static Int2 SameLengths<T0, T1>(T0[,] a0, T1[,] a1) { SizeCheck(a0, a1); return a0.Lengths(); }
        public static Int3 SameLengths<T0, T1>(T0[,,] a0, T1[,,] a1) { SizeCheck(a0, a1); return a0.Lengths(); }
        public static int SameLengths<T0, T1, T2>(Span<T0> a0, Span<T1> a1, Span<T2> a2) { SizeCheck(a0, a1, a2); return a0.Length; }
        public static int SameLengths<T0, T1, T2>(T0[] a0, T1[] a1, T2[] a2) { SizeCheck(a0, a1, a2); return a0.Length; }
        public static Int2 SameLengths<T0, T1, T2>(T0[,] a0, T1[,] a1, T2[,] a2) { SizeCheck(a0, a1, a2); return a0.Lengths(); }
        public static Int3 SameLengths<T0, T1, T2>(T0[,,] a0, T1[,,] a1, T2[,,] a2) { SizeCheck(a0, a1, a2); return a0.Lengths(); }

        public static int FindIndex<T>(this T[] a, Predicate<T> match) => Array.FindIndex(a, match);
        public static int FindLastIndex<T>(this T[] a, Predicate<T> match) => Array.FindLastIndex(a, match);
        public static int IndexOf<T>(this T[] a, T value) => Array.IndexOf(a, value);
        public static void Clear(this Array a) => Array.Clear(a, 0, a.Length);
        public static void CopyTo(this Array src, Array dst) { src.CopyTo(dst, 0); }
        public static void CopyTo(this Array src, int srcIndex, Array dst, int dstIndex, int length) { Array.Copy(src, srcIndex, dst, dstIndex, length); }
        public static T[][] DeepClone<T>(this T[][] a) => New.Array(a.Length, i => a[i].CloneX());
        public static T[][][] DeepClone<T>(this T[][][] a) => New.Array(a.Length, i => a[i].DeepClone());

        public static IEnumerable<T> ToEnumerable<T>(this T[,] array)
        {
            foreach (var e in array) yield return e;
        }
        public static IEnumerable<T> ToEnumerable<T>(this T[,,] array)
        {
            foreach (var e in array) yield return e;
        }
        public static T[] ToArray<T>(this T[,] array)
        {
            var s = array.AsSpan();
            var a = new T[s.Length];
            MemoryCopy(s, a);
            return a;
        }
        public static T[] ToArray<T>(this T[,,] array)
        {
            var s = array.AsSpan();
            var a = new T[s.Length];
            MemoryCopy(s, a);
            return a;
        }
        public static T[,] ToArray<T>(this IEnumerable<T> source, Int2 length)
        {
            var a = New.Array<T>(length);
            var s = a.AsSpan();
            int i = 0;
            foreach (var e in source) s[i++] = e;
            if (i != s.Length) ThrowException.SizeMismatch();
            return a;
        }
        public static Array ToArray<T>(this IEnumerable<T> source, int[] lengths)
        {
            var a = Array.CreateInstance(typeof(T), lengths);
            var s = a.AsSpan<T>();
            int i = 0;
            foreach (var e in source) s[i++] = e;
            if (i != s.Length) ThrowException.SizeMismatch();
            return a;
        }

        public static T[][] ToJaggedArray<T>(this T[,] array)
        {
            var L = array.Lengths();
            return New.Array(L.v0, i0 => New.Array(L.v1, i1 => array[i0, i1]));
        }
        public static T[][][] ToJaggedArray<T>(this T[,,] array)
        {
            var L = array.Lengths();
            return New.Array(L.v0, i0 => New.Array(L.v1, i1 => New.Array(L.v2, i2 => array[i0, i1, i2])));
        }

        public static Array<T> ReplaceDimensions<T>(Array<T> x, params int[] id)
        {
            var nn = x.GetLengths();
            var dim = nn.Length;
            var ii = new int[dim];
            var oo = new int[dim];
            var nna = Ex.OrderByIndex(nn, id);
            for (int n = 1, i = dim; --i >= 0;) { oo[id[i]] = n; n *= nna[i]; }

            var a = Array.CreateInstance(typeof(T), nna).As<T>();
            var a_ = a.AsSpan();
            var x_ = x.AsSpan();
            for (int j = 0, i = 0; i < x_.Length; i++)
            {
                a_[j] = x_[i];
                for (int d = dim; --d >= 0;)
                {
                    j += oo[d];
                    if (++ii[d] < nn[d]) break;
                    ii[d] = 0; j -= oo[d] * nn[d];
                }
            }
            return a;
        }
        public static T[,] ReplaceDimensions<T>(T[,] x, params int[] id) => (T[,])ReplaceDimensions((Array<T>)x, id);
        public static T[,,] ReplaceDimensions<T>(T[,,] x, params int[] id) => (T[,,])ReplaceDimensions((Array<T>)x, id);
        #endregion

        #region List
        public static bool AddOrDiscard<T>(this List<T> list, T item)
        {
            if (list.Contains(item)) return false;
            list.Add(item); return true;
        }
        public static bool AddOrOverwrite<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index != -1) { list[index] = item; return false; }
            list.Add(item); return true;
        }
        public static R[] To<T, R>(this List<T> list, Func<T, R> selector) => list.Select(selector).ToArray();
        #endregion


        #region Zip, Unzip
        public static void Unzip<T0, T1>(this (T0, T1) a, Action<T0, T1> f) => f(a.Item1, a.Item2);
        public static void Unzip<T0, T1, T2>(this (T0, T1, T2) a, Action<T0, T1, T2> f) => f(a.Item1, a.Item2, a.Item3);
        public static TR Unzip<T0, T1, TR>(this (T0, T1) a, Func<T0, T1, TR> f) => f(a.Item1, a.Item2);
        public static TR Unzip<T0, T1, T2, TR>(this (T0, T1, T2) a, Func<T0, T1, T2, TR> f) => f(a.Item1, a.Item2, a.Item3);

        public static Action<(T0, T1)> Zip<T0, T1>(this Action<T0, T1> f) => a => f(a.Item1, a.Item2);
        public static Action<(T0, T1, T2)> Zip<T0, T1, T2>(this Action<T0, T1, T2> f) => a => f(a.Item1, a.Item2, a.Item3);
        public static Func<(T0, T1), TR> Zip<T0, T1, TR>(this Func<T0, T1, TR> f) => a => f(a.Item1, a.Item2);
        public static Func<(T0, T1, T2), TR> Zip<T0, T1, T2, TR>(this Func<T0, T1, T2, TR> f) => a => f(a.Item1, a.Item2, a.Item3);

        public static IEnumerable<(T0, T1)> Zip<T0, T1>(this (IEnumerable<T0>, IEnumerable<T1>) a)
        {
            using var e1 = a.Item1.GetEnumerator();
            using var e2 = a.Item2.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
                yield return (e1.Current, e2.Current);
        }
        public static IEnumerable<(T0, T1, T2)> Zip<T0, T1, T2>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a)
        {
            using var e1 = a.Item1.GetEnumerator();
            using var e2 = a.Item2.GetEnumerator();
            using var e3 = a.Item3.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                yield return (e1.Current, e2.Current, e3.Current);
        }
        public static IEnumerable<(T0, T1)> Zip<T0, T1>(this (T0[], T1[]) a)
        {
            var l = a.Unzip(SameLengths);
            for (int i = 0; i < l; i++)
                yield return (a.Item1[i], a.Item2[i]);
        }
        public static IEnumerable<(T0, T1)> Zip<T0, T1>(this (T0[,], T1[,]) a)
        {
            var l = a.Unzip(SameLengths);
            Int2 i;
            for (i.v0 = 0; i.v0 < l.v0; i.v0++)
                for (i.v1 = 0; i.v1 < l.v1; i.v1++)
                    yield return (a.Item1.Item(i), a.Item2.Item(i));
        }
        public static IEnumerable<(T0, T1)> Zip<T0, T1>(this (T0[,,], T1[,,]) a)
        {
            var l = a.Unzip(SameLengths);
            Int3 i;
            for (i.v0 = 0; i.v0 < l.v0; i.v0++)
                for (i.v1 = 0; i.v1 < l.v1; i.v1++)
                    for (i.v2 = 0; i.v2 < l.v2; i.v2++)
                        yield return (a.Item1.Item(i), a.Item2.Item(i));
        }
        public static IEnumerable<(T0, T1, T2)> Zip<T0, T1, T2>(this (T0[], T1[], T2[]) a)
        {
            var l = a.Unzip(SameLengths);
            for (int i = 0; i < l; i++)
                yield return (a.Item1[i], a.Item2[i], a.Item3[i]);
        }
        public static IEnumerable<(T0, T1, T2)> Zip<T0, T1, T2>(this (T0[,], T1[,], T2[,]) a)
        {
            var l = a.Unzip(SameLengths);
            Int2 i;
            for (i.v0 = 0; i.v0 < l.v0; i.v0++)
                for (i.v1 = 0; i.v1 < l.v1; i.v1++)
                    yield return (a.Item1.Item(i), a.Item2.Item(i), a.Item3.Item(i));
        }
        public static IEnumerable<(T0, T1, T2)> Zip<T0, T1, T2>(this (T0[,,], T1[,,], T2[,,]) a)
        {
            var l = a.Unzip(SameLengths);
            Int3 i;
            for (i.v0 = 0; i.v0 < l.v0; i.v0++)
                for (i.v1 = 0; i.v1 < l.v1; i.v1++)
                    for (i.v2 = 0; i.v2 < l.v2; i.v2++)
                        yield return (a.Item1.Item(i), a.Item2.Item(i), a.Item3.Item(i));
        }
        #endregion

        #region Range
        public static IEnumerable<(T e, int i)> Indexed<T>(this IEnumerable<T> source)
        {
            var i = 0;
            return source.Select(e => (e, i++));
        }

        public static IEnumerable<long> Range(long count)
        {
            for (long i = 0; i < count; i++) yield return i;
        }
        public static IEnumerable<int> Range(int count)
        {
            for (int i = 0; i < count; i++) yield return i;
        }
        public static IEnumerable<Int2> Range(Int2 counts)
        {
            Int2 i;
            for (i.v0 = 0; i.v0 < counts.v0; i.v0++)
                for (i.v1 = 0; i.v1 < counts.v1; i.v1++)
                    yield return i;
        }
        public static IEnumerable<Int3> Range(Int3 counts)
        {
            Int3 i;
            for (i.v0 = 0; i.v0 < counts.v0; i.v0++)
                for (i.v1 = 0; i.v1 < counts.v1; i.v1++)
                    for (i.v2 = 0; i.v2 < counts.v2; i.v2++)
                        yield return i;
        }

        public static (int n, int[] ii, int[] oo) GetNIndexerOffset(this int[] nn)
        {
            var oo = nn.To0();
            int n = 1; for (int k = nn.Length; --k >= 0; n *= nn[k]) oo[k] = n;
            return (n, nn.To0(), oo);
        }
        public static (int n, int[] ii) GetNIndexer(this int[] nn)
        {
            int n = 1; for (int k = nn.Length; --k >= 0; n *= nn[k]) ;
            return (n, nn.To0());
        }
        public static void Decrement(this int[] nn, int[] ii)
        {
            for (int k = ii.Length; --k >= 0;)
                if (--ii[k] < 0) ii[k] += nn[k]; else return;
        }
        public static void Increment(this int[] nn, int[] ii)
        {
            for (int k = ii.Length; --k >= 0;)
                if (++ii[k] == nn[k]) ii[k] = 0; else return;
        }

        public static IEnumerable<int[]> Range(this int[] counts)
        {
            var (n, ii) = counts.GetNIndexer();
            for (int i = n; --i >= 0;)
            {
                yield return ii;
                counts.Increment(ii);
            }
        }

        public static IEnumerable<int> FromTo(int start, int end)
        {
            for (int i = start; i < end; i++) yield return i;
        }
        public static IEnumerable<int> LeaveUntilDown(int start, int end)
        {
            for (int i = start; --i >= end;) yield return i;
        }
        public static IEnumerable<int> FromUntil(int start, int end)
        {
            for (int i = start; i <= end; i++) yield return i;
        }
        public static IEnumerable<int> FromUntilDown(int start, int end)
        {
            for (int i = start; i >= end; i--) yield return i;
        }
        public static IEnumerable<int> FromToStep(int start, int end, int step)
        {
            if (step > 0)
                for (int i = start; i < end; i += step) yield return i;
            else
                for (int i = start; i > end; i += step) yield return i;
        }
        public static IEnumerable<int> FromUntilStep(int start, int end, int step)
        {
            if (step > 0)
                for (int i = start; i <= end; i += step) yield return i;
            else
                for (int i = start; i >= end; i += step) yield return i;
        }
        #endregion

        #region For, ForEach
        public static void For(int n, Action<int> f)
        {
            for (int i = 0; i < n; i++)
                f(i);
        }
        public static void For(Int2 n, Action<Int2> f)
        {
            for (Int2 i = default; i.v0 < n.v0; i.v0++)
                for (i.v1 = 0; i.v1 < n.v1; i.v1++)
                    f(i);
        }
        public static void For(Int3 n, Action<Int3> f)
        {
            for (Int3 i = default; i.v0 < n.v0; i.v0++)
                for (i.v1 = 0; i.v1 < n.v1; i.v1++)
                    for (i.v2 = 0; i.v2 < n.v2; i.v2++)
                        f(i);
        }

        public static void ForEach<T>(this IEnumerable<T> a, Action<T> f)
        {
            foreach (var e in a) f(e);
        }
        public static void ForEach<T0, T1>(this IEnumerable<(T0, T1)> a, Action<T0, T1> f)
        {
            foreach (var e in a) e.Unzip(f);
        }
        public static void ForEach<T0, T1, T2>(this IEnumerable<(T0, T1, T2)> a, Action<T0, T1, T2> f)
        {
            foreach (var e in a) e.Unzip(f);
        }
        public static void ForEach<T>(this Span<T> a, Action<T> f)
        {
            foreach (var e in a) f(e);
        }
        public static void ForEach<T0, T1>(this Span<(T0, T1)> a, Action<T0, T1> f)
        {
            foreach (var e in a) e.Unzip(f);
        }
        public static void ForEach<T0, T1, T2>(this Span<(T0, T1, T2)> a, Action<T0, T1, T2> f)
        {
            foreach (var e in a) e.Unzip(f);
        }

        public static void ForEach<T>(this IEnumerable<T> a, Action<T, int> f)
        {
            var c = 0;
            foreach (var e in a) f(e, c++);
        }
        public static void ForEach<T0, T1>(this IEnumerable<(T0, T1)> a, Action<T0, T1, int> f)
        {
            var c = 0;
            foreach (var e in a) f(e.Item1, e.Item2, c++);
        }
        public static void ForEach<T0, T1, T2>(this IEnumerable<(T0, T1, T2)> a, Action<T0, T1, T2, int> f)
        {
            var c = 0;
            foreach (var e in a) f(e.Item1, e.Item2, e.Item3, c++);
        }
        public static void ForEach<T>(this Span<T> a, Action<T, int> f)
        {
            var c = 0;
            foreach (var e in a) f(e, c++);
        }
        public static void ForEach<T0, T1>(this Span<(T0, T1)> a, Action<T0, T1, int> f)
        {
            var c = 0;
            foreach (var e in a) f(e.Item1, e.Item2, c++);
        }
        public static void ForEach<T0, T1, T2>(this Span<(T0, T1, T2)> a, Action<T0, T1, T2, int> f)
        {
            var c = 0;
            foreach (var e in a) f(e.Item1, e.Item2, e.Item3, c++);
        }

        public static void ForEach<T0, T1>(this (IEnumerable<T0>, IEnumerable<T1>) a, Action<T0, T1> f) => a.Zip().ForEach(e => e.Unzip(f));
        public static void ForEach<T0, T1>(this (T0[], T1[]) a, Action<T0, T1> f) => For(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static void ForEach<T0, T1>(this (T0[,], T1[,]) a, Action<T0, T1> f) => For(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static void ForEach<T0, T1>(this (T0[,,], T1[,,]) a, Action<T0, T1> f) => For(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));

        public static void ForEach<T0, T1, T2>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, Action<T0, T1, T2> f) => a.Zip().ForEach(e => e.Unzip(f));
        public static void ForEach<T0, T1, T2>(this (T0[], T1[], T2[]) a, Action<T0, T1, T2> f) => For(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static void ForEach<T0, T1, T2>(this (T0[,], T1[,], T2[,]) a, Action<T0, T1, T2> f) => For(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static void ForEach<T0, T1, T2>(this (T0[,,], T1[,,], T2[,,]) a, Action<T0, T1, T2> f) => For(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));

        public static void ParallelFor_(int n, int degree, Action<int> f)
        {
            if (degree == 1) { For(n, f); return; }
            Parallel.For(0, n, new ParallelOptions { MaxDegreeOfParallelism = degree }, f);
        }
        public static void ParallelForEach_<T>(this IEnumerable<T> a, int degree, Action<T> f)
        {
            if (degree == 1) { a.ForEach(f); return; }
            Parallel.ForEach(a, new ParallelOptions { MaxDegreeOfParallelism = degree }, f);
        }

        //public static void ParallelForHeavy(int n, int degree, Action<int, int> f) // no lock, many Task.Run
        //{
        //    degree = ParallelDegree(degree).Min(n);
        //    var tasks = new Task[degree];
        //    var t = -degree;
        //    for (int i = 0; i < n; i++)
        //    {
        //        var eid = i;
        //        var tid = t >= 0 ? t : -++t;
        //        tasks[pid] = Task.Run(() => f(eid, tid));
        //        if (t >= 0) t = Task.WaitAny(tasks);
        //    }
        //    Task.WaitAll(tasks);
        //}
        //public static void ParallelForEachHeavy<T>(this IEnumerable<T> a, int degree, Action<T, int, int> f) // no lock, many Task.Run
        //{
        //    degree = ParallelDegree(degree);
        //    var tasks = new Task[degree];
        //    var t = -degree;
        //    var c = 0;
        //    foreach (var e in a)
        //    {
        //        var eid = c++;
        //        var tid = t >= 0 ? t : -++t;
        //        tasks[pid] = Task.Run(() => f(e, eid, tid));
        //        if (t >= 0) t = Task.WaitAny(tasks);
        //    }
        //    Task.WaitAll(tasks);
        //}
        public static void ParallelFor(int n, int degree, Action<int, int> f) // use lock, afew Task.Run
        {
            degree = ParallelDegree(degree).Min(n);
            if (degree <= 1) { for (int i = 0; i < n; i++) f(i, 0); return; }
            var l = new object();
            var c = 0;
            var tasks = New.Array(degree, tid => Task.Run(() =>
            {
                while (true)
                {
                    int eid;
                    lock (l)  //OK．軽い
                    {
                        if (c == n) break;
                        eid = c++;
                    }
                    f(eid, tid);
                }
            }));
            Task.WaitAll(tasks);
        }
        public static void ParallelForEach<T>(this IEnumerable<T> a, int degree, Action<T, int, int> f) // use lock, afew Task.Run
        {
            degree = ParallelDegree(degree);
            var c = 0;
            if (degree == 1) { foreach (var e in a) f(e, c++, 0); return; }
            var itr = a.GetEnumerator();
            var tasks = New.Array(degree, tid => Task.Run(() =>
            {
                while (true)
                {
                    T e; int eid;
                    lock (itr)  //MoveNext, Current で待たされる可能性あり
                    {
                        if (!itr.MoveNext()) break;
                        e = itr.Current; eid = c++;
                    }
                    f(e, eid, tid);
                }
            }));
            Task.WaitAll(tasks);
        }
        public static void ParallelFor(int n, int degree, Action<int> f) => ParallelFor(n, degree, (eid, tid) => f(eid));  //ParallelFor_(n, ParallelDegree(degree), f);
        public static void ParallelFor(int n, Action<int, int> f) => ParallelFor(n, 0, f);
        public static void ParallelFor(int n, Action<int> f) => ParallelFor(n, 0, f);
        public static void ParallelForEach<T>(this IEnumerable<T> a, int degree, Action<T> f) => ParallelForEach(a, degree, (e, eid, tid) => f(e));  //ParallelForEach_(a, ParallelDegree(degree), f);
        public static void ParallelForEach<T>(this IEnumerable<T> a, Action<T, int, int> f) => ParallelForEach(a, 0, f);
        public static void ParallelForEach<T>(this IEnumerable<T> a, Action<T> f) => ParallelForEach(a, 0, f);

        public static void ParallelForEvenBlock(int n, int degree, Action<int, int> f)
        {
            degree = ParallelDegree(degree).Min(n);
            var m = Mt.DivCeil(n, degree);
            ParallelFor(degree, degree, tid => { var o = m * tid; var k = (o + m).Min(n); for (var i = o; i < k; i++) f(i, tid); });
        }
        public static void ParallelForEvenStep(int n, int degree, Action<int, int> f)
        {
            degree = ParallelDegree(degree).Min(n);
            ParallelFor(degree, degree, tid => { for (int i = tid; i < n; i += degree) f(i, tid); });
        }

        public static void ParallelIndexer(int n, int degree, Action<int, int> f)
        {
            degree = ParallelDegree(degree).Min(n);
            var m = Mt.DivCeil(n, degree);
            ParallelFor(degree, degree, i => { var o = m * i; f(o, m.Min(n - o)); });
        }
        public static R[] ParallelIndexer<R>(int n, int degree, Func<int, int, R> f)
        {
            degree = ParallelDegree(degree).Min(n);
            var m = Mt.DivCeil(n, degree);
            return New.ParallelArray(degree, degree, i => { var o = m * i; return f(o, m.Min(n - o)); });
        }

        public static void ParallelFor(Int2 n, int degree, Action<Int2> f) => Range(n).ParallelForEach(degree, i => f(i));
        public static void ParallelFor(Int3 n, int degree, Action<Int3> f) => Range(n).ParallelForEach(degree, i => f(i));
        public static void ParallelFor(Int2 n, Action<Int2> f) => ParallelFor(n, 0, f);
        public static void ParallelFor(Int3 n, Action<Int3> f) => ParallelFor(n, 0, f);

        public static void ParallelForEach<T0, T1>(this IEnumerable<(T0, T1)> a, int degree, Action<T0, T1> f) => a.ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1, T2>(this IEnumerable<(T0, T1, T2)> a, int degree, Action<T0, T1, T2> f) => a.ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1>(this IEnumerable<(T0, T1)> a, Action<T0, T1> f) => a.ParallelForEach(0, f);
        public static void ParallelForEach<T0, T1, T2>(this IEnumerable<(T0, T1, T2)> a, Action<T0, T1, T2> f) => a.ParallelForEach(0, f);

        public static void ParallelForEach<T0, T1>(this (IEnumerable<T0>, IEnumerable<T1>) a, int degree, Action<T0, T1> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1>(this (T0[], T1[]) a, int degree, Action<T0, T1> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1>(this (T0[,], T1[,]) a, int degree, Action<T0, T1> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1>(this (T0[,,], T1[,,]) a, int degree, Action<T0, T1> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1>(this (IEnumerable<T0>, IEnumerable<T1>) a, Action<T0, T1> f) => a.ParallelForEach(0, f);
        public static void ParallelForEach<T0, T1>(this (T0[], T1[]) a, Action<T0, T1> f) => a.ParallelForEach(0, f);
        public static void ParallelForEach<T0, T1>(this (T0[,], T1[,]) a, Action<T0, T1> f) => a.ParallelForEach(0, f);
        public static void ParallelForEach<T0, T1>(this (T0[,,], T1[,,]) a, Action<T0, T1> f) => a.ParallelForEach(0, f);

        public static void ParallelForEach<T0, T1, T2>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, int degree, Action<T0, T1, T2> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1, T2>(this (T0[], T1[], T2[]) a, int degree, Action<T0, T1, T2> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1, T2>(this (T0[,], T1[,], T2[,]) a, int degree, Action<T0, T1, T2> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1, T2>(this (T0[,,], T1[,,], T2[,,]) a, int degree, Action<T0, T1, T2> f) => a.Zip().ParallelForEach(degree, e => e.Unzip(f));
        public static void ParallelForEach<T0, T1, T2>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, Action<T0, T1, T2> f) => a.ParallelForEach(0, f);
        public static void ParallelForEach<T0, T1, T2>(this (T0[], T1[], T2[]) a, Action<T0, T1, T2> f) => a.ParallelForEach(0, f);
        public static void ParallelForEach<T0, T1, T2>(this (T0[,], T1[,], T2[,]) a, Action<T0, T1, T2> f) => a.ParallelForEach(0, f);
        public static void ParallelForEach<T0, T1, T2>(this (T0[,,], T1[,,], T2[,,]) a, Action<T0, T1, T2> f) => a.ParallelForEach(0, f);
        #endregion

        #region Select
        public static IEnumerable<TR> Select<T0, T1, TR>(this IEnumerable<(T0, T1)> a, Func<T0, T1, TR> f) => a.Select(e => e.Unzip(f));
        public static IEnumerable<TR> Select<T0, T1, T2, TR>(this IEnumerable<(T0, T1, T2)> a, Func<T0, T1, T2, TR> f) => a.Select(e => e.Unzip(f));
        public static IEnumerable<TR> Select<T0, T1, TR>(this (IEnumerable<T0>, IEnumerable<T1>) a, Func<T0, T1, TR> f) => a.Zip().Select(e => e.Unzip(f));
        public static IEnumerable<TR> Select<T0, T1, T2, TR>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, Func<T0, T1, T2, TR> f) => a.Zip().Select(e => e.Unzip(f));

        public static IEnumerable<R> ParallelSelect<T, R>(this IEnumerable<T> a, int degree, Func<T, R> f) // no lock, many Task.Run
        {
            degree = ParallelDegree(degree);
            var tasks = new Queue<Task<R>>(degree);
            foreach (var e in a)
            {
                tasks.Enqueue(Task.Run(() => f(e)));
                if (tasks.Count == degree) yield return wait();
            }
            while (tasks.Count > 0) yield return wait();
            R wait()
            {
                using var task = tasks.Dequeue();
                task.Wait();
                return task.Result;
            }
        }
        public static IEnumerable<R> ParallelSelect<T, R>(this IEnumerable<T> a, Func<T, R> f) => a.ParallelSelect(0, f);
        public static IEnumerable<TR> ParallelSelect<T0, T1, TR>(this (IEnumerable<T0>, IEnumerable<T1>) a, int degree, Func<T0, T1, TR> f) => a.Zip().ParallelSelect(degree, e => e.Unzip(f));
        public static IEnumerable<TR> ParallelSelect<T0, T1, T2, TR>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, int degree, Func<T0, T1, T2, TR> f) => a.Zip().ParallelSelect(degree, e => e.Unzip(f));
        public static IEnumerable<TR> ParallelSelect<T0, T1, TR>(this (IEnumerable<T0>, IEnumerable<T1>) a, Func<T0, T1, TR> f) => a.ParallelSelect(0, f);
        public static IEnumerable<TR> ParallelSelect<T0, T1, T2, TR>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, Func<T0, T1, T2, TR> f) => a.ParallelSelect(0, f);

        public static IEnumerable<R> ParallelSelectUnordered<T, R>(this IEnumerable<T> a, int degree, Func<T, R> f) // no lock, many Task.Run
        {
            degree = ParallelDegree(degree);
            var tasks = new List<Task<R>>(degree);
            foreach (var e in a)
            {
                tasks.Add(Task.Run(() => f(e)));
                if (tasks.Count == degree) yield return wait();
            }
            while (tasks.Count > 0) yield return wait();
            R wait()
            {
                var i = Task.WaitAny(tasks.ToArray());
                using var task = tasks.Pull(i);
                return task.Result;
            }
        }
        public static IEnumerable<R> ParallelSelectUnordered<T, R>(this IEnumerable<T> a, Func<T, R> f) => a.ParallelSelectUnordered(0, f);
        public static IEnumerable<TR> ParallelSelectUnordered<T0, T1, TR>(this (IEnumerable<T0>, IEnumerable<T1>) a, int degree, Func<T0, T1, TR> f) => a.Zip().ParallelSelectUnordered(degree, e => e.Unzip(f));
        public static IEnumerable<TR> ParallelSelectUnordered<T0, T1, T2, TR>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, int degree, Func<T0, T1, T2, TR> f) => a.Zip().ParallelSelectUnordered(degree, e => e.Unzip(f));
        public static IEnumerable<TR> ParallelSelectUnordered<T0, T1, TR>(this (IEnumerable<T0>, IEnumerable<T1>) a, Func<T0, T1, TR> f) => a.ParallelSelectUnordered(0, f);
        public static IEnumerable<TR> ParallelSelectUnordered<T0, T1, T2, TR>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, Func<T0, T1, T2, TR> f) => a.ParallelSelectUnordered(0, f);

        public static IEnumerable<(R, int)> ParallelSelectIndexedUnordered<T, R>(this IEnumerable<T> a, int degree, Func<T, R> f) // no lock, many Task.Run
        {
            degree = ParallelDegree(degree);
            var tasks = new List<(Task<R>, int)>(degree);
            int c = 0;
            foreach (var e in a)
            {
                tasks.Add((Task.Run(() => f(e)), c++));
                if (tasks.Count == degree) yield return wait();
            }
            while (tasks.Count > 0) yield return wait();
            (R, int) wait()
            {
                var i = Task.WaitAny(tasks.To(t => t.Item1));
                var (task, idx) = tasks.Pull(i);
                using var u = task;
                return (u.Result, idx);
            }
        }
        public static IEnumerable<(R, int)> ParallelSelectIndexedUnordered<T, R>(this IEnumerable<T> a, Func<T, R> f) => a.ParallelSelectIndexedUnordered(0, f);
        public static IEnumerable<(TR, int)> ParallelSelectIndexedUnordered<T0, T1, TR>(this (IEnumerable<T0>, IEnumerable<T1>) a, int degree, Func<T0, T1, TR> f) => a.Zip().ParallelSelectIndexedUnordered(degree, e => e.Unzip(f));
        public static IEnumerable<(TR, int)> ParallelSelectIndexedUnordered<T0, T1, T2, TR>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, int degree, Func<T0, T1, T2, TR> f) => a.Zip().ParallelSelectIndexedUnordered(degree, e => e.Unzip(f));
        public static IEnumerable<(TR, int)> ParallelSelectIndexedUnordered<T0, T1, TR>(this (IEnumerable<T0>, IEnumerable<T1>) a, Func<T0, T1, TR> f) => a.ParallelSelectIndexedUnordered(0, f);
        public static IEnumerable<(TR, int)> ParallelSelectIndexedUnordered<T0, T1, T2, TR>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, Func<T0, T1, T2, TR> f) => a.ParallelSelectIndexedUnordered(0, f);
        #endregion

        #region Let
        public static Span<T> Let<T>(this Span<T> a, Func<T, T> f)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = f(a[i]);
            return a;
        }
        public static Span<T> Let<T>(this Span<T> a, Func<T, int, T> f)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = f(a[i], i);
            return a;
        }
        public static T[] Let<T>(this T[] a, Func<T, T> f) { Let(a.AsSpan(), f); return a; }
        public static T[,] Let<T>(this T[,] a, Func<T, T> f) { Let(a.AsSpan(), f); return a; }
        public static T[,,] Let<T>(this T[,,] a, Func<T, T> f) { Let(a.AsSpan(), f); return a; }
        public static T[] Let<T>(this T[] a, Func<T, int, T> f) { Let(a.AsSpan(), f); return a; }
        public static T[,] Let<T>(this T[,] a, Func<T, Int2, T> f)
        {
            var a_ = a.AsSpan();
            var l = a.Lengths();
            Int2 i;
            int j = 0;
            for (i.v0 = 0; i.v0 < l.v0; i.v0++)
                for (i.v1 = 0; i.v1 < l.v1; i.v1++, j++)
                    a_[j] = f(a_[j], i);
            return a;
        }
        public static T[,,] Let<T>(this T[,,] a, Func<T, Int3, T> f)
        {
            var a_ = a.AsSpan();
            var l = a.Lengths();
            Int3 i;
            int j = 0;
            for (i.v0 = 0; i.v0 < l.v0; i.v0++)
                for (i.v1 = 0; i.v1 < l.v1; i.v1++)
                    for (i.v2 = 0; i.v2 < l.v2; i.v2++, j++)
                        a_[j] = f(a_[j], i);
            return a;
        }

        public static unsafe Span<T> UnsafeParallelLet<T>(this Span<T> a, Func<T, T> f) where T : unmanaged
        {
            fixed (T* p = a) main(p, a.Length);
            void main(T* p, int n)
            {
                ParallelFor(n, i => p[i] = f(p[i]));
            }
            return a;
        }
        public static T[] UnsafeParallelLet<T>(this T[] a, Func<T, T> f) where T : unmanaged { UnsafeParallelLet(a.AsSpan(), f); return a; }
        public static T[,] UnsafeParallelLet<T>(this T[,] a, Func<T, T> f) where T : unmanaged { UnsafeParallelLet(a.AsSpan(), f); return a; }
        public static T[,,] UnsafeParallelLet<T>(this T[,,] a, Func<T, T> f) where T : unmanaged { UnsafeParallelLet(a.AsSpan(), f); return a; }

        static void LetItem<T>(this T[] a, Func<T, T> f, int i) => a[i] = f(a[i]);
        static void LetItem<T>(this T[,] a, Func<T, T> f, Int2 i) => a.Item(i) = f(a.Item(i));
        static void LetItem<T>(this T[,,] a, Func<T, T> f, Int3 i) => a.Item(i) = f(a.Item(i));
        static void LetItem<T>(this T[] a, Func<T, int, T> f, int i) => a[i] = f(a[i], i);
        static void LetItem<T>(this T[,] a, Func<T, Int2, T> f, Int2 i) => a.Item(i) = f(a.Item(i), i);
        static void LetItem<T>(this T[,,] a, Func<T, Int3, T> f, Int3 i) => a.Item(i) = f(a.Item(i), i);
        public static T[] ParallelLet<T>(this T[] a, int degree, Func<T, T> f) { ParallelFor(a.Lengths(), degree, i => LetItem(a, f, i)); return a; }
        public static T[,] ParallelLet<T>(this T[,] a, int degree, Func<T, T> f) { ParallelFor(a.Lengths(), degree, i => LetItem(a, f, i)); return a; }
        public static T[,,] ParallelLet<T>(this T[,,] a, int degree, Func<T, T> f) { ParallelFor(a.Lengths(), degree, i => LetItem(a, f, i)); return a; }
        public static T[] ParallelLet<T>(this T[] a, int degree, Func<T, int, T> f) { ParallelFor(a.Lengths(), degree, i => LetItem(a, f, i)); return a; }
        public static T[,] ParallelLet<T>(this T[,] a, int degree, Func<T, Int2, T> f) { ParallelFor(a.Lengths(), degree, i => LetItem(a, f, i)); return a; }
        public static T[,,] ParallelLet<T>(this T[,,] a, int degree, Func<T, Int3, T> f) { ParallelFor(a.Lengths(), degree, i => LetItem(a, f, i)); return a; }
        public static T[] ParallelLet<T>(this T[] a, Func<T, T> f) => a.ParallelLet(0, f);
        public static T[,] ParallelLet<T>(this T[,] a, Func<T, T> f) => a.ParallelLet(0, f);
        public static T[,,] ParallelLet<T>(this T[,,] a, Func<T, T> f) => a.ParallelLet(0, f);
        public static T[] ParallelLet<T>(this T[] a, Func<T, int, T> f) => a.ParallelLet(0, f);
        public static T[,] ParallelLet<T>(this T[,] a, Func<T, Int2, T> f) => a.ParallelLet(0, f);
        public static T[,,] ParallelLet<T>(this T[,,] a, Func<T, Int3, T> f) => a.ParallelLet(0, f);
        #endregion

        #region ToArray
        public static R[] To0<T, R>(this Span<T> a) => new R[a.Length];
        public static Array To0<T>(this Array a) => Array.CreateInstance(typeof(T), a.GetLengths());
        public static R[] To0<T, R>(this T[] a) => new R[a.Length];
        public static R[,] To0<T, R>(this T[,] a) => new R[a.GetLength(0), a.GetLength(1)];
        public static R[,,] To0<T, R>(this T[,,] a) => new R[a.GetLength(0), a.GetLength(1), a.GetLength(2)];
        public static T[] To0<T>(this T[] a) => a.To0<T, T>();
        public static T[,] To0<T>(this T[,] a) => a.To0<T, T>();
        public static T[,,] To0<T>(this T[,,] a) => a.To0<T, T>();

        public static R[] To<T, R>(this Span<T> a, Func<T, R> f)
        {
            var r = new R[a.Length];
            for (int i = 0; i < a.Length; i++) r[i] = f(a[i]);
            return r;
        }
        public static R[] To<T, R>(this IEnumerable<T> a, Func<T, R> f) => a.Select(f).ToArray();
        public static R[] To<T, R>(this T[] a, Func<T, R> f) => New.Array(a.Lengths(), i => f(a[i]));
        public static R[,] To<T, R>(this T[,] a, Func<T, R> f) => New.Array(a.Lengths(), i => f(a.Item(i)));
        public static R[,,] To<T, R>(this T[,,] a, Func<T, R> f) => New.Array(a.Lengths(), i => f(a.Item(i)));
        public static R[] ParallelTo<T, R>(this T[] a, int degree, Func<T, R> f) => New.ParallelArray(a.Lengths(), degree, i => f(a[i]));
        public static R[,] ParallelTo<T, R>(this T[,] a, int degree, Func<T, R> f) => New.ParallelArray(a.Lengths(), degree, i => f(a.Item(i)));
        public static R[,,] ParallelTo<T, R>(this T[,,] a, int degree, Func<T, R> f) => New.ParallelArray(a.Lengths(), degree, i => f(a.Item(i)));
        public static R[] ParallelTo<T, R>(this T[] a, Func<T, R> f) => a.ParallelTo(0, f);
        public static R[,] ParallelTo<T, R>(this T[,] a, Func<T, R> f) => a.ParallelTo(0, f);
        public static R[,,] ParallelTo<T, R>(this T[,,] a, Func<T, R> f) => a.ParallelTo(0, f);

        public static R[] To<T, R>(this Span<T> a, Func<T, int, R> f)
        {
            var r = new R[a.Length];
            for (int i = 0; i < a.Length; i++) r[i] = f(a[i], i);
            return r;
        }
        public static R[] To<T, R>(this IEnumerable<T> a, Func<T, int, R> f) => a.Select(f).ToArray();
        public static R[] To<T, R>(this T[] a, Func<T, int, R> f) => New.Array(a.Lengths(), i => f(a[i], i));
        public static R[,] To<T, R>(this T[,] a, Func<T, Int2, R> f) => New.Array(a.Lengths(), i => f(a.Item(i), i));
        public static R[,,] To<T, R>(this T[,,] a, Func<T, Int3, R> f) => New.Array(a.Lengths(), i => f(a.Item(i), i));
        public static R[] ParallelTo<T, R>(this T[] a, int degree, Func<T, int, R> f) => New.ParallelArray(a.Lengths(), degree, i => f(a[i], i));
        public static R[,] ParallelTo<T, R>(this T[,] a, int degree, Func<T, Int2, R> f) => New.ParallelArray(a.Lengths(), degree, i => f(a.Item(i), i));
        public static R[,,] ParallelTo<T, R>(this T[,,] a, int degree, Func<T, Int3, R> f) => New.ParallelArray(a.Lengths(), degree, i => f(a.Item(i), i));
        public static R[] ParallelTo<T, R>(this T[] a, Func<T, int, R> f) => a.ParallelTo(0, f);
        public static R[,] ParallelTo<T, R>(this T[,] a, Func<T, Int2, R> f) => a.ParallelTo(0, f);
        public static R[,,] ParallelTo<T, R>(this T[,,] a, Func<T, Int3, R> f) => a.ParallelTo(0, f);

        public static R[] To<T0, T1, R>(this (IEnumerable<T0>, IEnumerable<T1>) a, Func<T0, T1, R> f) => a.Select(f).ToArray();
        public static R[] To<T0, T1, R>(this (T0[], T1[]) a, Func<T0, T1, R> f) => New.Array(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static R[,] To<T0, T1, R>(this (T0[,], T1[,]) a, Func<T0, T1, R> f) => New.Array(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static R[,,] To<T0, T1, R>(this (T0[,,], T1[,,]) a, Func<T0, T1, R> f) => New.Array(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static R[] ParallelTo<T0, T1, R>(this (T0[], T1[]) a, int degree, Func<T0, T1, R> f) => New.ParallelArray(a.Unzip(SameLengths), degree, i => a.Item(i).Unzip(f));
        public static R[,] ParallelTo<T0, T1, R>(this (T0[,], T1[,]) a, int degree, Func<T0, T1, R> f) => New.ParallelArray(a.Unzip(SameLengths), degree, i => a.Item(i).Unzip(f));
        public static R[,,] ParallelTo<T0, T1, R>(this (T0[,,], T1[,,]) a, int degree, Func<T0, T1, R> f) => New.ParallelArray(a.Unzip(SameLengths), degree, i => a.Item(i).Unzip(f));
        public static R[] ParallelTo<T0, T1, R>(this (T0[], T1[]) a, Func<T0, T1, R> f) => a.ParallelTo(0, f);
        public static R[,] ParallelTo<T0, T1, R>(this (T0[,], T1[,]) a, Func<T0, T1, R> f) => a.ParallelTo(0, f);
        public static R[,,] ParallelTo<T0, T1, R>(this (T0[,,], T1[,,]) a, Func<T0, T1, R> f) => a.ParallelTo(0, f);

        public static R[] To<T0, T1, T2, R>(this (IEnumerable<T0>, IEnumerable<T1>, IEnumerable<T2>) a, Func<T0, T1, T2, R> f) => a.Select(f).ToArray();
        public static R[] To<T0, T1, T2, R>(this (T0[], T1[], T2[]) a, Func<T0, T1, T2, R> f) => New.Array(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static R[,] To<T0, T1, T2, R>(this (T0[,], T1[,], T2[,]) a, Func<T0, T1, T2, R> f) => New.Array(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static R[,,] To<T0, T1, T2, R>(this (T0[,,], T1[,,], T2[,,]) a, Func<T0, T1, T2, R> f) => New.Array(a.Unzip(SameLengths), i => a.Item(i).Unzip(f));
        public static R[] ParallelTo<T0, T1, T2, R>(this (T0[], T1[], T2[]) a, int degree, Func<T0, T1, T2, R> f) => New.ParallelArray(a.Unzip(SameLengths), degree, i => a.Item(i).Unzip(f));
        public static R[,] ParallelTo<T0, T1, T2, R>(this (T0[,], T1[,], T2[,]) a, int degree, Func<T0, T1, T2, R> f) => New.ParallelArray(a.Unzip(SameLengths), degree, i => a.Item(i).Unzip(f));
        public static R[,,] ParallelTo<T0, T1, T2, R>(this (T0[,,], T1[,,], T2[,,]) a, int degree, Func<T0, T1, T2, R> f) => New.ParallelArray(a.Unzip(SameLengths), degree, i => a.Item(i).Unzip(f));
        public static R[] ParallelTo<T0, T1, T2, R>(this (T0[], T1[], T2[]) a, Func<T0, T1, T2, R> f) => a.ParallelTo(0, f);
        public static R[,] ParallelTo<T0, T1, T2, R>(this (T0[,], T1[,], T2[,]) a, Func<T0, T1, T2, R> f) => a.ParallelTo(0, f);
        public static R[,,] ParallelTo<T0, T1, T2, R>(this (T0[,,], T1[,,], T2[,,]) a, Func<T0, T1, T2, R> f) => a.ParallelTo(0, f);
        #endregion

        #region IEnumerable
        public static T[] TakeTo<T>(this IEnumerable<T> a, int count) => a.Take(count).ToArray();
        public static T[] SkipTo<T>(this IEnumerable<T> a, int start) => a.Skip(start).ToArray();
        public static T[] SkipTakeTo<T>(this IEnumerable<T> a, int start, int count) => a.Skip(start).Take(count).ToArray();
        public static T[] SkipTo<T>(this T[] a, int start) { var r = new T[a.Length - start]; Array.Copy(a, start, r, 0, r.Length); return r; }
        public static T[] SkipTakeTo<T>(this T[] a, int start, int count) { var r = new T[count]; Array.Copy(a, start, r, 0, count); return r; }
        public static T[] SkipTo<T>(this IList<T> a, int start) => New.Array(a.Count - start, i => a[i + start]);
        public static T[] SkipTakeTo<T>(this IList<T> a, int start, int count) => New.Array(count, i => a[i + start]);

        public static IEnumerable<T> Row<T>(this T[,] matrix, int row) => Enumerable.Range(0, matrix.GetLength(1)).Select(j => matrix[row, j]);
        public static IEnumerable<T> Col<T>(this T[,] matrix, int col) => Enumerable.Range(0, matrix.GetLength(0)).Select(i => matrix[i, col]);

        public static T[] ConcatTo<T>(this IEnumerable<T> source0, IEnumerable<T> source1) => source0.Concat(source1).ToArray();
        public static T[] ConcatTo<T>(this IEnumerable<IEnumerable<T>> blocks)
        {
            var r = new List<T>();
            foreach (var b in blocks) r.AddRange(b);
            return r.ToArray();
        }
        public static T[,] ConcatTo<T>(this T[,][,] blocks)
        {
            var ll = blocks.Lengths();
            var l0 = new int[ll.v0];
            var l1 = new int[ll.v1];
            for (int i0 = 0; i0 < ll.v0; i0++)
            {
                for (int i1 = 0; i1 < ll.v1; i1++)
                {
                    var b = blocks[i0, i1];
                    if (b is null) continue;
                    var l = b.Lengths();
                    if (l0[i0] != 0 && l0[i0] != l.v0 || l1[i1] != 0 && l1[i1] != l.v1) ThrowException.SizeMismatch();
                    l0[i0] = l.v0;
                    l1[i1] = l.v1;
                }
            }
            var r = new T[l0.Sum(), l1.Sum()];
            for (int o0 = 0, i0 = 0; i0 < ll.v0; o0 += l0[i0], i0++)
            {
                for (int o1 = 0, i1 = 0; i1 < ll.v1; o1 += l1[i1], i1++)
                {
                    var b = blocks[i0, i1];
                    if (b is null) continue;
                    Ex.For(l0[i0], j0 => Ex.For(l1[i1], j1 => r[j0 + o0, j1 + o1] = b[j0, j1]));
                }
            }
            return r;
        }

        public static int FindIndex<T>(this IEnumerable<T> a, Predicate<T> match)
        {
            var i = 0;
            foreach (var e in a) { if (match(e)) return i; i++; }
            return -1;
        }
        public static T FirstOrDefault<T>(this IEnumerable<T> a, T defaultvalue)
        {
            foreach (var e in a) return e;
            return defaultvalue;
        }
        public static T FirstOrDefault<T>(this IEnumerable<T> a, Func<T, bool> predicate, T defaultvalue)
        {
            foreach (var e in a) if (predicate(e)) return e;
            return defaultvalue;
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> a) => a.OrderBy(x => x);
        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> a) => a.OrderByDescending(x => x);

        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> pair) where TKey : notnull => dictionary.Add(pair.Key, pair.Value);
        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, (TKey, TValue) pair) where TKey : notnull => dictionary.Add(pair.Item1, pair.Item2);
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> a) where TKey : notnull
        {
            var d = new Dictionary<TKey, TValue>();
            foreach (var e in a) d.Add(e);
            return d;
        }
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> a) where TKey : notnull
        {
            var d = new Dictionary<TKey, TValue>();
            foreach (var e in a) d.Add(e);
            return d;
        }

        public static Dictionary<TKey, int> ToHistogram<TKey>(this IEnumerable<TKey> a, IEqualityComparer<TKey>? comparer = null) where TKey : notnull
        {
            var d = comparer is null ? new Dictionary<TKey, int>() : new Dictionary<TKey, int>(comparer);
            foreach (var e in a) if (d.ContainsKey(e)) d[e]++; else d.Add(e, 1);
            return d;
        }
        #endregion

        #region min-max
        static ((int index, T item) min, (int index, T item) max) MinMaxIndexItem_<T>(this IEnumerable<T> source, bool minSw, bool maxSw)
            where T : IComparable<T>
        {
            ((int index, T item) min, (int index, T item) max) r = default;
            int index = 0;
            foreach (var item in source)
            {
                if (minSw && (index == 0 || r.min.item!.CompareTo(item) > 0)) { r.min.index = index; r.min.item = item; }
                if (maxSw && (index == 0 || r.max.item!.CompareTo(item) < 0)) { r.max.index = index; r.max.item = item; }
                index++;
            }
            if (index == 0) ThrowException.Argument(nameof(source));
            return r;
        }
        public static (int index, T item) MinIndexItem<T>(this IEnumerable<T> source) where T : IComparable<T> => source.MinMaxIndexItem_(true, false).min;
        public static (int index, T item) MaxIndexItem<T>(this IEnumerable<T> source) where T : IComparable<T> => source.MinMaxIndexItem_(false, true).max;
        public static ((int index, T item) min, (int index, T item) max) MinMaxIndexItem<T>(this IEnumerable<T> source) where T : IComparable<T> => source.MinMaxIndexItem_(true, true);
        public static int MinIndex<T>(this IEnumerable<T> source) where T : IComparable<T> => source.MinIndexItem().index;
        public static int MaxIndex<T>(this IEnumerable<T> source) where T : IComparable<T> => source.MaxIndexItem().index;
        public static (int minIndex, int maxIndex) MinMaxIndex<T>(this IEnumerable<T> source) where T : IComparable<T> { var (min, max) = source.MinMaxIndexItem(); return (min.index, max.index); }
        public static T MinItem<T>(this IEnumerable<T> source) where T : IComparable<T> => source.MinIndexItem().item;
        public static T MaxItem<T>(this IEnumerable<T> source) where T : IComparable<T> => source.MaxIndexItem().item;
        public static (T minItem, T maxItem) MinMaxItem<T>(this IEnumerable<T> source) where T : IComparable<T> { var (min, max) = source.MinMaxIndexItem(); return (min.item, max.item); }

        static ((int index, T item, C value) min, (int index, T item, C value) max) MinMaxIndexItemValue_<T, C>(this IEnumerable<T> source, bool minSw, bool maxSw, Func<T, C> selector)
            where C : IComparable<C>
        {
            ((int index, T item, C value) min, (int index, T item, C value) max) r = default;
            int index = 0;
            foreach (var item in source)
            {
                var value = selector(item);
                if (minSw && (index == 0 || r.min.value!.CompareTo(value) > 0)) { r.min.index = index; r.min.item = item; r.min.value = value; }
                if (maxSw && (index == 0 || r.max.value!.CompareTo(value) < 0)) { r.max.index = index; r.max.item = item; r.max.value = value; }
                index++;
            }
            if (index == 0) ThrowException.Argument(nameof(source));
            return r;
        }
        public static (int index, T item, C value) MinIndexItemValue<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MinMaxIndexItemValue_(true, false, selector).min;
        public static (int index, T item, C value) MaxIndexItemValue<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MinMaxIndexItemValue_(false, true, selector).max;
        public static ((int index, T item, C value) min, (int index, T item, C value) max) MinMaxIndexItemValue<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MinMaxIndexItemValue_(true, true, selector);
        public static int MinIndex<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MinIndexItemValue(selector).index;
        public static int MaxIndex<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MaxIndexItemValue(selector).index;
        public static (int minIndex, int maxIndex) MinMaxIndex<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> { var (min, max) = source.MinMaxIndexItemValue(selector); return (min.index, max.index); }
        public static T MinItem<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MinIndexItemValue(selector).item;
        public static T MaxItem<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MaxIndexItemValue(selector).item;
        public static (T minItem, T maxItem) MinMaxItem<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> { var (min, max) = source.MinMaxIndexItemValue(selector); return (min.item, max.item); }
        public static C MinValue<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MinIndexItemValue(selector).value;
        public static C MaxValue<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> => source.MaxIndexItemValue(selector).value;
        public static (C minValue, C maxValue) MinMaxValue<T, C>(this IEnumerable<T> source, Func<T, C> selector) where C : IComparable<C> { var (min, max) = source.MinMaxIndexItemValue(selector); return (min.value, max.value); }

        public static T Min<T>(int count, Func<int, T> selector) where T : IComparable<T>
        {
            if (count <= 0) ThrowException.ArgumentOutOfRange($"{nameof(count)} <= 0");
            T a = selector(0);
            for (int i = 1; i < count; i++) { var e = selector(i); if (a.CompareTo(e) > 0) a = e; }
            return a;
        }
        public static T Max<T>(int count, Func<int, T> selector) where T : IComparable<T>
        {
            if (count <= 0) ThrowException.ArgumentOutOfRange($"{nameof(count)} <= 0");
            T a = selector(0);
            for (int i = 1; i < count; i++) { var e = selector(i); if (a.CompareTo(e) < 0) a = e; }
            return a;
        }
        #endregion

        #region IList
        public static void Swap<T>(this IList<T> list, int i, int j) { T a = list[i]; list[i] = list[j]; list[j] = a; }
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> source)
        {
            if (list is List<T> l) { l.AddRange(source); return; }
            foreach (var e in source) list.Add(e);
        }
        public static int FindIndexLast<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = list.Count; --i >= 0;) if (match(list[i])) return i;
            return -1;
        }
        public static int[] FindAllIndex<T>(this IList<T> list, Predicate<T> match)
        {
            var result = new List<int>();
            for (int i = 0; i < list.Count; i++) if (match(list[i])) result.Add(i);
            return result.ToArray();
        }
        public static bool AllBackward<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = list.Count; --i >= 0;) if (!match(list[i])) return false;
            return true;
        }
        public static bool AnyBackward<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = list.Count; --i >= 0;) if (match(list[i])) return false;
            return true;
        }

        public static int BinarySearch<T>(this IList<T> list, T item, Comparison<T>? compare = null)
        {
            compare ??= Comparer<T>.Default.Compare;
            int i0 = 0;
            int i1 = list.Count;
            while (true)
            {
                if (i0 == i1) return ~i0;
                int ii = (i0 + i1) / 2;
                int c = compare(item, list[ii]);
                if (c == 0) return ii;
                if (c < 0) i1 = ii; else i0 = ii + 1;
            }
        }
        public static int BinarySearch<T, U>(this IList<T> list, Func<T, U> selector, U item, Comparison<U>? compare = null)
        {
            compare ??= Comparer<U>.Default.Compare;
            int i0 = 0;
            int i1 = list.Count;
            while (true)
            {
                if (i0 == i1) return ~i0;
                int ii = (i0 + i1) / 2;
                int c = compare(item, selector(list[ii]));
                if (c == 0) return ii;
                if (c < 0) i1 = ii; else i0 = ii + 1;
            }
        }

        public static ref T First<T>(this T[] array) => ref array[0];
        public static ref T Last<T>(this T[] array, int index = -1) => ref array[array.Length + index];
        public static T First<T>(this IList<T> list) => list[0];
        public static T Last<T>(this IList<T> list, int index = -1) => list[list.Count + index];
        public static T Pop<T>(this IList<T> list) => list.Pull(list.Count - 1);
        public static T Pull<T>(this IList<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static int LetDistinctSorted<T>(this IList<T> list, Func<T, T, bool>? equals = null)
        {
            equals ??= EqualityComparer<T>.Default.Equals;
            int i = 0;
            for (int j = 1; j < list.Count; j++)
            {
                if (!equals(list[i], list[j]))
                {
                    i++;
                    if (i != j) list[i] = list[j];
                }
            }
            i++;
            int overlap = list.Count - i;
            for (int j = list.Count; --j >= i;) list.RemoveAt(j);
            return overlap;
        }

        public static void Let<T>(this IList<T> list, Func<T, T> selector)
        {
            for (int i = 0; i < list.Count; i++) list[i] = selector(list[i]);
        }
        public static void Let<T>(this IList<T> list, Func<T, int, T> selector)
        {
            for (int i = 0; i < list.Count; i++) list[i] = selector(list[i], i);
        }
        #endregion

        #region ICollection, SortedSet, IDictionary, ISet, Queue
        // ICollection
        public static void AddNew<T>(this ICollection<T> source) where T : new()
        {
            source.Add(new T());
        }
        public static void AddNew<T>(this ICollection<T> source, int count) where T : new()
        {
            for (int i = count; --i >= 0;) source.Add(new T());
        }

        // LinkedList
        public static void Push<T>(this LinkedList<T> list, T value) => list.AddLast(value);
        public static T Pop<T>(this LinkedList<T> list) { var r = list.Last!.Value; list.RemoveLast(); return r; }
        public static void Enqueue<T>(this LinkedList<T> list, T value) => list.AddLast(value);
        public static T Dequeue<T>(this LinkedList<T> list) { var r = list.First!.Value; list.RemoveFirst(); return r; }

        // SortedSet
        public static T First<T>(this SortedSet<T> set) => set.Min!;
        public static T Last<T>(this SortedSet<T> set) => set.Max!;
        public static T Pop<T>(this SortedSet<T> set) { var r = set.Max!; set.Remove(r); return r; }

        // IDictionary
        public static KeyValuePair<TKey, TValue> Pop<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull { var r = dictionary.Last(); dictionary.Remove(r.Key); return r; }
        public static bool AddOrDiscard<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, value); return true;
        }
        public static bool AddOrOverwrite<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
        {
            if (dictionary.ContainsKey(key)) { dictionary[key] = value; return false; }
            dictionary.Add(key, value); return true;
        }
        public static TValue GetItemOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultvalue) where TKey : notnull
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultvalue;
        }
        public static TValue GetItemOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull
        {
            return dictionary.TryGetValue(key, out var value) ? value : default!;
        }
        public static TValue GetItemOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull where TValue : new()
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                value = new TValue();
                dictionary.Add(key, value);
            }
            return value;
        }

        // ISet
        public static bool AddOrDiscard<T>(this ISet<T> set, T item)
        {
            return set.Add(item);
        }
        public static bool AddOrOverwrite<T>(this ISet<T> set, T item)
        {
            if (set.Contains(item)) { set.Remove(item); set.Add(item); return false; }
            else { set.Add(item); return true; }
        }

        public static void Enqueue<T>(this Queue<T> queue, params T[] items)
        {
            foreach (var e in items) queue.Enqueue(e);
        }
        // Dictionary
        // SortedDictionary
        // SortedList
        #endregion

        #region sorting
        public static T[] LetReverse<T>(this T[] array) { Array.Reverse(array); return array; }
        public static List<T> LetReverse<T>(this List<T> list) { list.Reverse(); return list; }

        public static T[] LetSort<T>(this T[] array) where T : IComparable<T> { Array.Sort(array); return array; }
        public static T[] LetSort<T>(this T[] array, Comparison<T> compare) { Array.Sort(array, compare); return array; }
        public static T[] LetSort<T, C>(this T[] array, Func<T, C> selector) where C : IComparable<C> => array.LetSort((x, y) => selector(x).CompareTo(selector(y)));
        public static List<T> LetSort<T>(this List<T> list) where T : IComparable<T> { list.Sort(); return list; }
        public static List<T> LetSort<T>(this List<T> list, Comparison<T> compare) { list.Sort(compare); return list; }
        public static List<T> LetSort<T, C>(this List<T> list, Func<T, C> selector) where C : IComparable<C> => list.LetSort((x, y) => selector(x).CompareTo(selector(y)));

        public static int[] SortIndex<T>(this IList<T> list, Comparison<T> comparison)
        {
            int[] index = New.Array(list.Count, i => i);
            Array.Sort(index, (x, y) => comparison(list[x], list[y]));
            return index;
        }
        public static int[] SortIndex<T>(this IList<T> list) where T : IComparable<T> => SortIndex(list, (x, y) => x.CompareTo(y));
        public static int[] SortIndex<T, C>(this IList<T> list, Func<T, C> selector) where C : IComparable<C> => SortIndex(list, (x, y) => selector(x).CompareTo(selector(y)));

        public static void Sort<T>(Span<T> list, Comparison<T> comparison)
        {
            for (int k = list.Length, l = k / 2; k > 1;)
            {
                T item;
                if (l > 0) item = list[--l];
                else
                {
                    item = list[--k];
                    list[k] = list[0];  //一番大きいものを最後から格納していく
                }
                int i = l;  //空いている場所
                while (true)
                {
                    int j = i * 2 + 1;  // j=部下1
                    if (j >= k) break;
                    if (j + 1 < k && comparison(list[j], list[j + 1]) < 0) ++j;  // 部下2が存在して大ならj=部下2
                    if (comparison(item, list[j]) >= 0) break;
                    list[i] = list[j];
                    i = j;
                }
                list[i] = item;
            }
        }
        public static int[] SortIndex<T>(this Span<T> list, Comparison<T> comparison)
        {
            int[] index = New.Array(list.Length, i => i);
            for (int k = list.Length, l = k / 2; k > 1;)
            {
                int item;
                if (l > 0) item = index[--l];
                else
                {
                    item = index[--k];
                    index[k] = index[0];  //一番大きいものを最後から格納していく
                }
                int i = l;  //空いている場所
                while (true)
                {
                    int j = i * 2 + 1;  // j=部下1
                    if (j >= k) break;
                    if (j + 1 < k && comparison(list[index[j]], list[index[j + 1]]) < 0) ++j;  // 部下2が存在して大ならj=部下2
                    if (comparison(list[item], list[index[j]]) >= 0) break;
                    index[i] = index[j];
                    i = j;
                }
                index[i] = item;
            }
            return index;
        }
        public static int[] SortIndex<T>(this Span<T> list) where T : IComparable<T> => SortIndex(list, (x, y) => x.CompareTo(y));
        public static int[] SortIndex<T, C>(this Span<T> list, Func<T, C> selector) where C : IComparable<C> => SortIndex(list, (x, y) => selector(x).CompareTo(selector(y)));

        public static int[] IndexToRank(IList<int> index)
        {
            int[] Rank = new int[index.Count];
            for (int i = index.Count; --i >= 0;)
                Rank[index[i]] = i;
            return Rank;
        }

        public static T[] LetSortAsIndex<T>(this T[] list, IList<int> index) => (T[])((IList<T>)list).LetSortAsIndex(index);
        public static List<T> LetSortAsIndex<T>(this List<T> list, IList<int> index) => (List<T>)((IList<T>)list).LetSortAsIndex(index);
        public static IList<T> LetSortAsIndex<T>(this IList<T> list, IList<int> index)
        {
            for (int i = index.Count; --i >= 0;)
            {
                if (index[i] < 0) continue;
                T item = list[i];
                for (int j = i; ;)  // listの中の空いている添字番号
                {
                    int jj = index[j]; index[j] = ~index[j];
                    if (jj == i) { list[j] = item; break; }
                    list[j] = list[jj];
                    j = jj;
                }
            }
            for (int i = index.Count; --i >= 0;)
                index[i] = ~index[i];
            return list;
        }
        public static T[] OrderByIndex<T>(this IList<T> list, IEnumerable<int> index)
        {
            return index.To(i => list[i]);
        }

        public static T[] LetSortAsRank<T>(this T[] list, IList<int> rank) => (T[])((IList<T>)list).LetSortAsRank(rank);
        public static List<T> LetSortAsRank<T>(this List<T> list, IList<int> rank) => (List<T>)((IList<T>)list).LetSortAsRank(rank);
        public static IList<T> LetSortAsRank<T>(this IList<T> list, IList<int> rank)
        {
            for (int i = rank.Count; --i >= 0;)
            {
                if (rank[i] < 0) continue;
                T item = list[i];
                for (int j = i; ;)  // listの中の玉突きの玉の添字番号
                {
                    int jj = rank[j]; rank[j] = ~rank[j];
                    if (jj == i) { list[jj] = item; break; }
                    { T temp = list[jj]; list[jj] = item; item = temp; }
                    j = jj;
                }
            }
            for (int i = rank.Count; --i >= 0;)
                rank[i] = ~rank[i];
            return list;
        }
        public static T[] OrderByRank<T>(this IList<T> list, IList<int> rank)
        {
            var result = new T[list.Count];
            for (int i = result.Length; --i >= 0;)
                result[rank[i]] = list[i];
            return result;
        }

        public static int Compare<T>(IList<T>? x, IList<T>? y) where T : IComparable<T>
        {
            if (x is null) if (y is null) return 0; else return -1; else if (y is null) return +1;
            if (x.Count != y.Count) return x.Count - y.Count;
            for (int i = 0; i < x.Count; i++)
            {
                int c = x[i].CompareTo(y[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[]? x, T[]? y) where T : IComparable<T>
        {
            if (x is null) if (y is null) return 0; else return -1; else if (y is null) return +1;
            if (x.Length != y.Length) return x.Length - y.Length;
            for (int i = 0; i < x.Length; i++)
            {
                int c = x[i].CompareTo(y[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[][]? x, T[][]? y) where T : IComparable<T>
        {
            if (x is null) if (y is null) return 0; else return -1; else if (y is null) return +1;
            if (x.Length != y.Length) return x.Length - y.Length;
            for (int i = 0; i < x.Length; i++)
            {
                int c = Compare(x[i], y[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[][][]? x, T[][][]? y) where T : IComparable<T>
        {
            if (x is null) if (y is null) return 0; else return -1; else if (y is null) return +1;
            if (x.Length != y.Length) return x.Length - y.Length;
            for (int i = 0; i < x.Length; i++)
            {
                int c = Compare(x[i], y[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[][][][]? x, T[][][][]? y) where T : IComparable<T>
        {
            if (x is null) if (y is null) return 0; else return -1; else if (y is null) return +1;
            if (x.Length != y.Length) return x.Length - y.Length;
            for (int i = 0; i < x.Length; i++)
            {
                int c = Compare(x[i], y[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        #endregion

        #region TimeSpan
        public static TimeSpan Multiply(this TimeSpan x, long y) => new TimeSpan(x.Ticks * y);
        public static TimeSpan Multiply(this TimeSpan x, double y) => new TimeSpan((long)(x.Ticks * y));
        public static TimeSpan Divide(this TimeSpan x, long y) => new TimeSpan(x.Ticks / y);
        public static TimeSpan Divide(this TimeSpan x, double y) => new TimeSpan((long)(x.Ticks / y));
        public static TimeSpan Remainder(this TimeSpan x, long y) => new TimeSpan(x.Ticks % y);
        public static TimeSpan Remainder(this TimeSpan x, double y) => new TimeSpan((long)(x.Ticks % y));
        #endregion

        #region string
        public static string Trim(this string str, int trim)
        {
            return trim >= 0 ? str.Substring(trim) : str.Substring(0, str.Length + trim);
        }

        public static bool TryParse(this string str, string format, out DateTime date)
        {
            return DateTime.TryParseExact(str, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date);
        }
        public static uint ParseHex32(this string str) => uint.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
        public static ulong ParseHex64(this string str) => ulong.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
        public static DateTime ParseDateTime(this string str, string format)
        {
            if (!str.TryParse(format, out DateTime date))
                ThrowException.Argument($"unknown DateTime format: {str} for {format}");
            return date;
        }
        public static unsafe DateTime ParseDateTime(sbyte* str, string format) => new string(str, 0, format.Length).ParseDateTime(format);
        public static unsafe DateTime ParseDateTime(char* str, string format) => new string(str, 0, format.Length).ParseDateTime(format);
        public static int TryParse(this string str, int defaultvalue) => int.TryParse(str, out var value) ? value : defaultvalue;
        public static uint TryParse(this string str, uint defaultvalue) => uint.TryParse(str, out var value) ? value : defaultvalue;
        public static float TryParse(this string str, float defaultvalue) => float.TryParse(str, out var value) ? value : defaultvalue;
        public static double TryParse(this string str, double defaultvalue) => double.TryParse(str, out var value) ? value : defaultvalue;
        public static DateTime TryParse(this string str, DateTime defaultvalue) => DateTime.TryParse(str, out var value) ? value : defaultvalue;
        public static DateTime TryParse(this string str, string format, DateTime defaultvalue) => TryParse(str, format, out var value) ? value : defaultvalue;
        public static T Parse<T>(this string str) where T : Enum => (T)Enum.Parse(typeof(T), str);
        public static IEnumerable<string> SelectString<T>(this IEnumerable<T> source) => source.Select(e => e!.ToString()!);
        public static IEnumerable<string> SelectString<T>(this IEnumerable<T> source, string format)
        {
            var formatstr = $"{{0:{format}}}";
            return source.Select(e => string.Format(formatstr, e));
        }

        public static string Join(this IEnumerable<string> source, string separator, string terminator = "")
        {
            var buffer = new StringBuilder();
            foreach (var e in source)
            {
                buffer.Append(e);
                buffer.Append(separator);
            }
            if (buffer.Length > 0) buffer.Remove(buffer.Length - separator.Length, separator.Length);
            buffer.Append(terminator);
            return buffer.ToString();
        }
        public static string JoinDirectory(this IEnumerable<string> source) => source.Join(Path.DirectorySeparatorChar.ToString());
        public static string JoinTab(this IEnumerable<string> source) => source.Join("\t");
        public static string JoinSpace(this IEnumerable<string> source) => source.Join(" ");
        public static string JoinComma(this IEnumerable<string> source) => source.Join(",");
        public static string JoinCommaSpace(this IEnumerable<string> source) => source.Join(", ");
        public static string JoinLines(this IEnumerable<string> source) => source.Join("\n", "\n");
        public static string ToStringFormat<T>(this IEnumerable<T> source, string format = "", string separator = "\t", string terminator = "")
        {
            var formatstr = $"{{0:{format}}}{separator}";
            var buffer = new StringBuilder();
            foreach (var e in source)
                buffer.AppendFormat(formatstr, e);
            if (buffer.Length > 0) buffer.Remove(buffer.Length - separator.Length, separator.Length);
            buffer.Append(terminator);
            return buffer.ToString();
        }
        public static string ToStringFormat<T>(this T[,] array, string format = "", string separator = "\t", string lineseparator = "\n", string terminator = "")
        {
            var formatstr = $"{{0:{format}}}{separator}";
            var buffer = new StringBuilder();
            for (int i0 = 0; i0 < array.GetLength(0); i0++)
            {
                for (int i1 = 0; i1 < array.GetLength(1); i1++)
                    buffer.AppendFormat(formatstr, array[i0, i1]);
                buffer.Remove(buffer.Length - separator.Length, separator.Length);
                buffer.Append(lineseparator);
            }
            buffer.Append(terminator);
            return buffer.ToString();
        }
        public static string ToStringFormat<T>(this T[,,] array, string format = "", string separator = "\t", string lineseparator = "\n", string terminator = "")
        {
            var formatstr = $"{{0:{format}}}{separator}";
            var buffer = new StringBuilder();
            for (int i0 = 0; i0 < array.GetLength(0); i0++)
            {
                for (int i1 = 0; i1 < array.GetLength(1); i1++)
                {
                    for (int i2 = 0; i2 < array.GetLength(1); i2++)
                        buffer.AppendFormat(formatstr, array[i0, i1, i2]);
                    buffer.Remove(buffer.Length - separator.Length, separator.Length);
                    buffer.Append(lineseparator);
                }
            }
            buffer.Append(terminator);
            return buffer.ToString();
        }

        public static string[] Split(this string str, string separator) => str.Split(new[] { separator }, StringSplitOptions.None);
        public static string[] SplitLine(this string str) => str.Split(Ex.NewLineCodes, StringSplitOptions.None);
        public static string[] SplitTab(this string line) => line.Split('\t');
        public static string[] SplitSpace(this string line) => line.Split(' ');
        public static string[] SplitCsv(this string line)
        {
            var converted = new List<char>();
            var quotation = false;
            var escape = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = i < line.Length ? line[i] : ',';
                if (escape)
                {
                    if (c != '\"') { quotation = false; escape = false; }
                }
                if (!quotation)
                {
                    if (c == '\"') { quotation = true; continue; }
                    if (c == ',') c = '\t';
                }
                else
                {
                    if (c == '\"')
                    {
                        if (!escape) { escape = true; continue; }  // 1個目
                        else { escape = false; }                   // 2個目
                    }
                }
                converted.Add(c);
            }
            return new string(converted.ToArray()).SplitTab();
        }
        public static string Split(this string str, char separator, int index) => Split(str, separator.ToString(), index);
        public static string Split(this string str, string separator, int index)
        {
            var start = 0;
            var next = 0;
            for (var i = index + 1; --i >= 0;)
            {
                if (next == str.Length) ThrowException.ArgumentOutOfRange($"{nameof(index)} is out of range");
                if (next != 0) start = next + separator.Length;
                next = str.IndexOf(separator, start, StringComparison.Ordinal); if (next == -1) next = str.Length;
            }
            return str[start..next];
        }

        public static ReadOnlySpan<char> LineItem(this string line, int index, char delimiter)
        {
            if (index < 0) ThrowException.ArgumentOutOfRange(nameof(index));
            var i = 0;
            for (int c = index; --c >= 0;)
            {
                i = line.IndexOf(delimiter, i) + 1;
                if (i == 0) ThrowException.ArgumentOutOfRange(nameof(index));
            }
            var j = line.IndexOf(delimiter, i);
            if (j == -1) j = line.Length;
            return line.AsSpan()[i..j];
        }
        public static ReadOnlySpan<char> ItemTab(this string line, int index) => LineItem(line, index, '\t');
        public static ReadOnlySpan<char> ItemCsv(this string line, int index) => LineItem(line, index, ',');

        public static string Replace(this string str, IEnumerable<(string oldValue, string newValue)> replacelist)
        {
            foreach (var (o, n) in replacelist)
                str = str.Replace(o, n, StringComparison.Ordinal);
            return str;
        }
        public static string Replace(this string str, IEnumerable<(char oldValue, char newValue)> replacelist)
        {
            foreach (var (o, n) in replacelist)
                str = str.Replace(o, n);
            return str;
        }
        public static string Replace(this string str, IEnumerable<string> oldValues, string newValue) => str.Replace(oldValues.Select(o => (o, newValue)));
        public static string Replace(this string str, IEnumerable<char> oldValues, char newValue) => str.Replace(oldValues.Select(o => (o, newValue)));

        public static void Write(this StringBuilder sb, string value) { sb.Append(value); }
        public static void Write(this StringBuilder sb, ReadOnlySpan<char> buffer) { sb.Append(buffer); }
        public static void Write<T>(this StringBuilder sb, T value) where T : struct { sb.Append(value.ToString()); }
        public static void WriteLine(this StringBuilder sb) { sb.AppendLine(); }
        public static void WriteLine(this StringBuilder sb, string value) { sb.AppendLine(value); }
        public static void WriteLine(this StringBuilder sb, ReadOnlySpan<char> buffer) { sb.Append(buffer); sb.AppendLine(); }
        public static void WriteLine<T>(this StringBuilder sb, T value) where T : notnull { sb.AppendLine(value.ToString()); }
        #endregion

        #region IO
        public static FileStream FileCreate(string path) => File.Create(path);
        public static FileStream FileOpenReadShare(string path) => File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        public static string PathNotGz(string path) => IsPathGz(path) ? path[..^3] : path;
        public static string PathGz(string path) => path + ".gz";
        public static bool IsPathGz(string path) => path.EndsWith(".gz", StringComparison.OrdinalIgnoreCase);
        public static Stream FileOpenReadShareGz(string path)
        {
            if (File.Exists(path)) return FileOpenReadShare(path);
            var pathgz = PathGz(path);
            if (File.Exists(pathgz)) return new GZipStream(FileOpenReadShare(pathgz), CompressionMode.Decompress);
            return ThrowException.Argument<Stream>($"file not exists: {path} or +.gz");
        }
        public static bool FileExistsGz(string path)
        {
            return File.Exists(path) || File.Exists(PathGz(path));
        }
        public static Stream FileCreateGz(string path)
        {
            var pathgz = PathGz(path);
            return new GZipStream(FileCreate(pathgz), CompressionMode.Compress);
        }

        public readonly static char[] DirectorySeparators = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
        //dir does not include the last '\\'
        public static (string dir, string file) GetDirectoryFileName(string path)
        {
            if (Directory.Exists(path)) return (Path.TrimEndingDirectorySeparator(path), "");
            return (Path.GetDirectoryName(path) ?? string.Empty, Path.GetFileName(path) ?? string.Empty);
        }
        //dir does not include the last '\\'. ext includes '.'
        public static (string dir, string file, string ext) GetDirectoryFileExtensionName(string path)
        {
            if (Directory.Exists(path)) return (Path.TrimEndingDirectorySeparator(path), "", "");
            return (Path.GetDirectoryName(path) ?? string.Empty, Path.GetFileNameWithoutExtension(path) ?? string.Empty, Path.GetExtension(path) ?? string.Empty);
        }
        public static string GetParentDirectory(string path)
        {
            var dir = GetDirectoryFileName(path).dir!;
            var i = dir.LastIndexOfAny(DirectorySeparators);
            if (i == -1) return ThrowException.Argument<string>($"no parent directory: {path}");
            return dir[..i];
        }
        public static string[] GetFiles(string path, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            var dir = Path.GetDirectoryName(path);
            var file = Path.GetFileName(path);
            if (!Directory.Exists(dir)) ThrowException.Argument($"directory not exist: {dir}");
            return Directory.GetFiles(dir, file, option).LetSort();
        }
        public static string[] GetFilesGz(string path, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            var nc = GetFiles(path, option);
            var gz = GetFiles(PathGz(path), option).To(PathNotGz);
            return nc.ConcatTo(gz).LetSort();
        }
        public static string GetFullPath(this string path)
        {
            var pp = Path.GetFullPath(path);
            var isNetwork = pp.StartsWith(@"\\", StringComparison.Ordinal) || pp.StartsWith(@"//", StringComparison.Ordinal);
            var items = (isNetwork ? path[2..] : path).Split(DirectorySeparators);
            if (isNetwork) items[0] = $@"{pp[0..2]}{items[0]}";

            for (int i = 0; i < items.Length; i++)
            {
                var p = items.Take(i + 1).JoinDirectory();
                if (Directory.Exists(p) || File.Exists(p)) continue;
                p = $@"{p}.lnk";
                var target = ParseShortcutFile(p);
                if (target is null) break;
                items[i] = target;
                items = items.Skip(i).ToArray();
                return GetFullPath(items.JoinDirectory());
            }
            return items.JoinDirectory();
        }
#pragma warning disable IDE0059
        public static string? ParseShortcutFile(string path)
        {
            if (!File.Exists(path)) return null;
            using var reader = new BinaryReader(FileOpenReadShare(path));
            if (reader.ReadUInt32() != 0x4C) return null;
            reader.Skip(0x10);
            var flagsHeader = reader.ReadUInt16();
            reader.Skip(0x36);
            if ((flagsHeader & 1) != 0)
            {
                var len = reader.ReadUInt16();
                reader.Skip(len);
            }
            if ((flagsHeader & 2) != 0)  //file location info
            {
                var start = reader.BaseStream.Position;
                var lengthFileLocationInfo = reader.ReadUInt32();
                if (reader.ReadUInt32() != 0x1C) return null;
                var flagsVolume = reader.ReadUInt32();
                var offsetLocalInfo = reader.ReadUInt32();
                var offsetLocalPath = reader.ReadUInt32();
                var offsetNetworkInfo = reader.ReadUInt32();
                var offsetNetworkPath = reader.ReadUInt32();
                if ((flagsVolume & 1) != 0)  //local volume
                {
                    reader.BaseStream.Position = start + offsetLocalPath;
                    return reader.ReadStringNullTerminated();
                }
                if ((flagsVolume & 2) != 0)  //network volume
                {
                    reader.BaseStream.Position = start + offsetNetworkInfo + 20;
                    var vol = reader.ReadStringNullTerminated();
                    var dir = reader.ReadStringNullTerminated();  // start + offsetNetworkPath
                    return Path.Join(vol, dir);
                }
            }
            ThrowException.InvalidOperation($"cannot resolve shortcut: {path}");
            return null;
        }
#pragma warning restore IDE0059

        public static byte[] FileReadAllBytes(string path)
        {
            using var file = FileOpenReadShare(path);
            var b = new byte[file.Length];
            file.Read(b);
            return b;
        }
        public static string[] FileReadLines(string path, int start, int count, Encoding? encoding = null)
        {
            var lines = new List<string>();
            {
                using var reader = new StreamReader(FileOpenReadShare(path), encoding ?? Encoding.UTF8);
                while (lines.Count < count)
                {
                    var line = reader.ReadLine();
                    if (line is null) break;
                    if (start > 0) start--; else lines.Add(line);
                }
            }
            return lines.ToArray();
        }
        public static string[] FileReadAllLines(string path, Encoding? encoding = null) => FileReadLines(path, 0, int.MaxValue, encoding);

        public static void FileWriteAllLines(string path, IEnumerable<string> lines, Encoding? encoding = null)
        {
            using var writer = new StreamWriter(path, false, encoding ?? Encoding.UTF8);
            foreach (var line in lines)
            {
                writer.Write(line);
                writer.Write('\n');
            }
        }

        #region Stream
        public static bool EndOfStream(this Stream stream) => stream.Position >= stream.Length;

        public static unsafe void Read(this Stream reader, IntPtr buffer, int count)
        {
            var buf = Ex.TakeBuffer();
            fixed (byte* b = buf)
                while (count > 0)
                {
                    var s = Math.Min(buf.Length, count);
                    var r = reader.Read(buf, 0, s);
                    MemoryCopy((IntPtr)b, buffer, r);
                    buffer += r; count -= r;
                    if (r < s) ThrowException.InvalidOperation("cannot read expected size");
                }
            Ex.ReturnBuffer(buf);
        }
        public static unsafe void Write(this Stream writer, IntPtr buffer, int count)
        {
            var buf = Ex.TakeBuffer();
            fixed (byte* b = buf)
                while (count > 0)
                {
                    var s = Math.Min(buf.Length, count);
                    MemoryCopy(buffer, (IntPtr)b, s);
                    writer.Write(buf, 0, s);
                    buffer += s; count -= s;
                }
            Ex.ReturnBuffer(buf);
        }

        public static void Read<T>(this Stream stream, out T value) where T : struct
        {
            value = default;
            stream.Read(Ex.AsSpan(ref value).AsBytes());
        }
        public static T Read<T>(this Stream stream) where T : struct
        {
            T value = default;
            stream.Read(Ex.AsSpan(ref value).AsBytes());
            return value;
        }
        public static void Write<T>(this Stream stream, ref T value) where T : struct
        {
            stream.Write(Ex.AsReadOnlySpan(ref value).AsBytes());
        }
        #endregion

        #region BinaryReader, BinaryWriter
        public static bool EndOfStream(this BinaryReader reader) => reader.BaseStream.EndOfStream();
        public static void Skip(this BinaryReader reader, long count)
        {
            if (count >= 64) { reader.BaseStream.Seek(count, SeekOrigin.Current); return; }
            while (count >= 8) { reader.ReadUInt64(); count -= 8; }
            if ((count & 4) > 0) reader.ReadUInt32();
            if ((count & 2) > 0) reader.ReadUInt16();
            if ((count & 1) > 0) reader.ReadByte();
        }
        public static void Skip<T>(this BinaryReader reader, long count) where T : struct => Skip(reader, count * Ex.SizeOf<T>());
        public static string ReadStringBytes(this BinaryReader reader, int bytes, Encoding encoding)
        {
            var b = reader.ReadBytes(bytes);
            unsafe { fixed (byte* p = b) return new string((sbyte*)p, 0, bytes, encoding); }
        }
        public static string ReadStringBytes(this BinaryReader reader, int bytes) => ReadStringBytes(reader, bytes, Encoding.UTF8);
        public static string ReadStringNullTerminated(this BinaryReader reader)
        {
            var sb = new StringBuilder();
            while (true)
            {
                var c = reader.ReadChars(1);  //サロゲートペア対策．イマイチ
                if (c.Length == 0 || c[0] == '\0') break;
                sb.Write(c);
            }
            return sb.ToString();
        }

        public static T Read<T>(this BinaryReader reader) where T : struct => reader.BaseStream.Read<T>();
        public static void Read<T>(this BinaryReader reader, out T value) where T : struct => reader.BaseStream.Read(out value);
        public static void Write<T>(this BinaryWriter writer, T value) where T : struct => writer.BaseStream.Write(ref value);

        public static void Read<T>(this BinaryReader reader, Span<T> array) where T : struct => reader.Read(array.AsBytes());
        public static void Read<T>(this BinaryReader reader, T[] array) where T : struct => Read(reader, array.AsSpan());
        public static void Read<T>(this BinaryReader reader, T[,] array) where T : struct => Read(reader, array.AsSpan());
        public static void Read<T>(this BinaryReader reader, T[,,] array) where T : struct => Read(reader, array.AsSpan());
        public static T[] ReadArray<T>(this BinaryReader reader, int length) where T : struct { var r = new T[length]; Read(reader, r); return r; }
        public static T[,] ReadArray<T>(this BinaryReader reader, int length0, int length1) where T : struct { var r = new T[length0, length1]; Read(reader, r); return r; }
        public static T[,,] ReadArray<T>(this BinaryReader reader, int length0, int length1, int length2) where T : struct { var r = new T[length0, length1, length2]; Read(reader, r); return r; }

        public static void Write<T>(this BinaryWriter writer, ReadOnlySpan<T> array) where T : struct => writer.Write(array.AsBytes());
        public static void Write<T>(this BinaryWriter writer, Span<T> array) where T : struct => writer.Write(array.AsBytes());
        public static void Write<T>(this BinaryWriter writer, T[] array) where T : struct => Write(writer, array.AsSpan());
        public static void Write<T>(this BinaryWriter writer, T[,] array) where T : struct => Write(writer, array.AsSpan());
        public static void Write<T>(this BinaryWriter writer, T[,,] array) where T : struct => Write(writer, array.AsSpan());
        public static void Write<T>(this BinaryWriter writer, IEnumerable<T> source) where T : struct { foreach (var e in source) writer.Write(e); }

        public static void Write<T0, T1>(this BinaryWriter writer, T0 v0, T1 v1) where T0 : struct where T1 : struct { writer.Write<T0>(v0); writer.Write<T1>(v1); }
        public static void Write<T0, T1, T2>(this BinaryWriter writer, T0 v0, T1 v1, T2 v2) where T0 : struct where T1 : struct where T2 : struct { writer.Write<T0>(v0); writer.Write<T1>(v1); writer.Write<T2>(v2); }
        public static void Write<T0, T1, T2, T3>(this BinaryWriter writer, T0 v0, T1 v1, T2 v2, T3 v3) where T0 : struct where T1 : struct where T2 : struct where T3 : struct { writer.Write<T0>(v0); writer.Write<T1>(v1); writer.Write<T2>(v2); writer.Write<T3>(v3); }

        public static (T0, T1) Read<T0, T1>(this BinaryReader reader) where T0 : struct where T1 : struct => (reader.Read<T0>(), reader.Read<T1>());
        public static (T0, T1, T2) Read<T0, T1, T2>(this BinaryReader reader) where T0 : struct where T1 : struct where T2 : struct => (reader.Read<T0>(), reader.Read<T1>(), reader.Read<T2>());
        public static (T0, T1, T2, T3) Read<T0, T1, T2, T3>(this BinaryReader reader) where T0 : struct where T1 : struct where T2 : struct where T3 : struct => (reader.Read<T0>(), reader.Read<T1>(), reader.Read<T2>(), reader.Read<T3>());

        public static void FileCompressGz(string pathwild)
        {
            var buf = Ex.TakeBuffer();
            foreach (var path in GetFiles(pathwild))
            {
                Console.Write($"compressing: {path}");
                var pathgz = PathGz(path);
                if (File.Exists(pathgz)) { Console.WriteLine(" canceled (gz file exists)"); return; }
                using var reader = FileOpenReadShare(path);
                using var writer = FileCreateGz(path);
                for (int count = 0; ; count++)
                {
                    var r = reader.Read(buf, 0, buf.Length);
                    if (r == 0) break;
                    writer.Write(buf, 0, r);
                    if (((count + 1) & 0x3fff) == 0) Console.Write(".");
                }
                Console.WriteLine("done");
            }
            Ex.ReturnBuffer(buf);
        }
        #endregion

        #region file-array
        public static void SaveArray<T>(Array<T> array, Stream stream) where T : struct
        {
            using var writer = new BinaryWriter(stream);
            var lengths = array.GetLengths();
            writer.Write(Ex.SizeOf<T>(), lengths.Length);
            writer.Write(lengths.AsSpan().AsBytes());
            writer.Write(array.AsSpan().AsBytes());
        }
        public static void SaveArray<T>(Array<T> array, string path) where T : unmanaged
        {
            using var stream = FileCreate(path);
            SaveArray(array, stream);
        }
        public static void SaveArray<T>(T[] array, string path) where T : unmanaged => SaveArray((Array<T>)array, path);
        public static void SaveArray<T>(T[,] array, string path) where T : unmanaged => SaveArray((Array<T>)array, path);
        public static void SaveArray<T>(T[,,] array, string path) where T : unmanaged => SaveArray((Array<T>)array, path);
        public static void SaveArrayGz<T>(Array<T> array, string path) where T : unmanaged
        {
            using var stream = FileCreateGz(path);
            SaveArray(array, stream);
        }
        public static void SaveArrayGz<T>(T[] array, string path) where T : unmanaged => SaveArrayGz((Array<T>)array, path);
        public static void SaveArrayGz<T>(T[,] array, string path) where T : unmanaged => SaveArrayGz((Array<T>)array, path);
        public static void SaveArrayGz<T>(T[,,] array, string path) where T : unmanaged => SaveArrayGz((Array<T>)array, path);

        public static Array<T> LoadArray<T>(Stream stream) where T : struct
        {
            using var reader = new BinaryReader(stream);
            var expected = Ex.SizeOf<T>();
            var eachsize = reader.ReadInt32();
            if (eachsize != expected) ThrowException.Argument($"sizeof(T)={expected} != recorded size={eachsize}");
            var rank = reader.ReadInt32();
            var length = new int[rank];
            reader.Read(length.AsSpan().AsBytes());
            var array = Array.CreateInstance(typeof(T), length);
            reader.Read(array.AsSpan<T>().AsBytes());
            return array.As<T>();
        }
        public static Array<T> LoadArray<T>(string path) where T : unmanaged
        {
            using var stream = FileOpenReadShare(path);
            return LoadArray<T>(stream);
        }
        public static T[] LoadArray1D<T>(string path) where T : unmanaged => (T[])LoadArray<T>(path);
        public static T[,] LoadArray2D<T>(string path) where T : unmanaged => (T[,])LoadArray<T>(path);
        public static T[,,] LoadArray3D<T>(string path) where T : unmanaged => (T[,,])LoadArray<T>(path);
        public static Array<T> LoadArrayGz<T>(string path) where T : unmanaged
        {
            using var stream = FileOpenReadShareGz(path);
            return LoadArray<T>(stream);
        }
        public static T[] LoadArrayGz1D<T>(string path) where T : unmanaged => (T[])LoadArrayGz<T>(path);
        public static T[,] LoadArrayGz2D<T>(string path) where T : unmanaged => (T[,])LoadArrayGz<T>(path);
        public static T[,,] LoadArrayGz3D<T>(string path) where T : unmanaged => (T[,,])LoadArrayGz<T>(path);

        public static Array<T> CacheFile<T>(string path, Func<Array<T>> create, bool verbose = false) where T : unmanaged
        {
            if (File.Exists(path))
            {
                if (verbose) Console.WriteLine($"loading cache: {path}");
                return Ex.LoadArray<T>(path);
            }
            if (verbose) Console.WriteLine($"creating cache: {path}");
            var t = create();
            if (verbose) Console.WriteLine($"saving cache: {path}");
            Ex.SaveArray<T>(t, path);
            return t;
        }
        public static T[] CacheFile<T>(string path, Func<T[]> create, bool verbose = false) where T : unmanaged => (T[])CacheFile(path, () => (Array<T>)create(), verbose);
        public static T[,] CacheFile<T>(string path, Func<T[,]> create, bool verbose = false) where T : unmanaged => (T[,])CacheFile(path, () => (Array<T>)create(), verbose);
        public static T[,,] CacheFile<T>(string path, Func<T[,,]> create, bool verbose = false) where T : unmanaged => (T[,,])CacheFile(path, () => (Array<T>)create(), verbose);
        #endregion
        #endregion

        #region Encoding
        // here standard code is utf-8 & \n
        public const string NewLine = "\n";
        public static readonly string[] NewLineCodes = { "\r\n", "\n", "\r" };
        public static readonly Encoding EncodingUTF8 = Encoding.UTF8;
#if (NETCOREAPP || NETSTANDARD)
        public static readonly Encoding EncodingSJIS = CodePagesEncodingProvider.Instance.GetEncoding("shift_jis")!;
        public static readonly Encoding EncodingEUC = CodePagesEncodingProvider.Instance.GetEncoding("euc-jp")!;
        public static readonly Encoding EncodingJIS = CodePagesEncodingProvider.Instance.GetEncoding("iso-2022-jp")!;
#else
        public static readonly Encoding EncodingSJIS = Encoding.GetEncoding("shift_jis");
        public static readonly Encoding EncodingEUC = Encoding.GetEncoding("euc-jp");
        public static readonly Encoding EncodingJIS = Encoding.GetEncoding("iso-2022-jp");
#endif
        public static readonly Encoding[] Encodings = new[] { EncodingUTF8, EncodingSJIS, EncodingEUC, EncodingJIS };

        public static Encoding? DetectEncoding(string path)
        {
            static bool IsInvalidChar(char c) => (c <= 0x1f && c != '\0' && c != '\t' && c != '\n' && c != '\r') || (c == 0x7f) || (c >= 0x80 && c <= 0x9f) || (c >= '\uE000' && c <= '\uF8FF') || (c == '\uFFFD');
            var buffer = new char[4096];
            foreach (var encoding in Encodings)
            {
                using var file = new StreamReader(path, encoding);
                while (true)
                {
                    if (file.EndOfStream) return encoding;
                    var count = file.Read(buffer, 0, buffer.Length);
                    if (buffer.Take(count).Any(c => IsInvalidChar(c))) break;
                }
            }
            return null;
        }
        #endregion

        #region words expression
        public static T ConvertTo<T>(this string str)
        {
            return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(str);
        }

        public static int[] ExtractNumbers(string str)
        {
            var list = new List<int>();
            string[] items = str.Split(',');
            foreach (string s in items)
            {
                if (!s.Contains('-', StringComparison.Ordinal)) { list.Add(int.Parse(s)); continue; }
                string[] t = s.Split('-');
                for (int i = int.Parse(t[0]), j = int.Parse(t[1]); i <= j; ++i)
                    list.Add(i);
            }
            list.Sort();
            return list.ToArray();
        }
        #endregion

        #region sound file functions
        static readonly char[] wavefileChunkRiff = { 'R', 'I', 'F', 'F' };
        static readonly char[] wavefileChunkType = { 'W', 'A', 'V', 'E' };
        static readonly char[] wavefileChunkFrmt = { 'f', 'm', 't', ' ' };
        static readonly char[] wavefileChunkData = { 'd', 'a', 't', 'a' };
        public static void WaveFileSave(string path, double[][] data, int samplesPerSecond, int bitsPerSample = 16) => WaveFileSave(path, data.To(c => c.To(x => (float)x)), samplesPerSecond, bitsPerSample);
        public static void WaveFileSave(string path, float[][] data, int samplesPerSecond, int bitsPerSample = 16)
        {
            try
            {
                using var writer = new BinaryWriter(File.Create(path));
                int channels = data.Length;
                int dataLength = data[0].Length;

                int padding = 1;                             // File padding
                int bytesPerSample = (bitsPerSample / 8) * channels;  // Bytes per sample.
                int averageBytesPerSecond = bytesPerSample * samplesPerSecond;
                int chunkDataLength = (bitsPerSample / 8) * dataLength * channels;
                int chunkFrmtLength = 16;                    // Format chunk length.
                int chunkRiffLength = chunkDataLength + 36;  // File length, minus first 8 bytes of RIFF description.

                writer.Write(wavefileChunkRiff);             //4 bytes
                writer.Write(chunkRiffLength);               //4 bytes
                {
                    writer.Write(wavefileChunkType);         //4 bytes
                    writer.Write(wavefileChunkFrmt);         //4 bytes
                    writer.Write(chunkFrmtLength);           //4 bytes
                    {
                        writer.Write((short)padding);        //2 bytes
                        writer.Write((short)channels);       //2 bytes
                        writer.Write(samplesPerSecond);      //4 bytes
                        writer.Write(averageBytesPerSecond); //4 bytes
                        writer.Write((short)bytesPerSample); //2 bytes
                        writer.Write((short)bitsPerSample);  //2 bytes
                    }
                    writer.Write(wavefileChunkData);         //4 bytes
                    writer.Write(chunkDataLength);           //4 bytes
                    switch (bitsPerSample)
                    {
                        case +8: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) writer.Write((Byte)Mt.MinMax(data[j][i] * 0x80 + 0x80, 0, 0xff)); break;
                        case 16: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) writer.Write((Int16)Mt.MinMax(data[j][i] * 0x8000, -0x8000, 0x7fff)); break;
                        case 32: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) writer.Write((Int32)Mt.MinMax(data[j][i] * 0x80000000L, -0x80000000L, 0x7fffffffL)); break;
                        default: ThrowException.InvalidOperation($"unknown BitsPerSample {bitsPerSample}"); break;
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message); throw; }
        }
        public static (float[][]? data, int samplesPerSecond) WaveFileLoad(string path)
        {
            try
            {
                using var reader = new BinaryReader(Ex.FileOpenReadShare(path));
                if (Ex.Compare(reader.ReadChars(4), wavefileChunkRiff) != 0) ThrowException.InvalidOperation("not RIFF chunk");
                reader.ReadInt32();  //chunkRiffLength
                if (Ex.Compare(reader.ReadChars(4), wavefileChunkType) != 0) ThrowException.InvalidOperation("not WAVE chunk");

                int bitsPerSample = 0;
                int samplesPerSecond = 0;
                int channels = 0;
                while (true)
                {
                    char[] chunkName = reader.ReadChars(4);
                    int chunkLength = reader.ReadInt32();
                    if (Ex.Compare(chunkName, wavefileChunkFrmt) == 0)
                    {
                        reader.ReadInt16();  //shPad                 //2 bytes
                        channels = reader.ReadInt16();               //2 bytes
                        samplesPerSecond = reader.ReadInt32();       //4 bytes
                        reader.ReadInt32();  //averageBytesPerSecond //4 bytes
                        reader.ReadInt16();  //shBytesPerSample      //2 bytes
                        bitsPerSample = reader.ReadInt16();          //2 bytes
                        if (chunkLength > 16) reader.ReadBytes(chunkLength - 16);  //unknown data
                        continue;
                    }
                    if (Ex.Compare(chunkName, wavefileChunkData) == 0)
                    {
                        var data = ReadWaveData(reader.ReadBytes(chunkLength), bitsPerSample, channels);
                        return (data, samplesPerSecond);
                    }
                    reader.Skip(chunkLength);  //unknown chunk
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e.Message); throw; }
        }
        public static unsafe float[][] ReadWaveData(byte[] file, int bitsPerSample, int channels)
        {
            int length = file.Length / channels / (bitsPerSample / 8);
            var data = New.Array(channels, i => new float[length]);
            fixed (byte* p = file)
                switch (bitsPerSample)
                {
                    case +8: { var q = p; /*****/ for (int i = 0; i < length; ++i) for (int j = 0; j < data.Length; ++j) data[j][i] = (*q++ - 0x80) * (1.0f / 0x80); } break;
                    case 16: { var q = (Int16*)p; for (int i = 0; i < length; ++i) for (int j = 0; j < data.Length; ++j) data[j][i] = *q++ * (1.0f / 0x8000); } break;
                    case 32: { var q = (Int32*)p; for (int i = 0; i < length; ++i) for (int j = 0; j < data.Length; ++j) data[j][i] = *q++ * (1.0f / 0x80000000L); } break;
                    default: ThrowException.InvalidOperation($"unknown BitsPerSample {bitsPerSample}"); break;
                }
            return data;
        }
        #endregion

        #region color functions
        public static byte GetA(int x) => (byte)(x >> 24);
        public static byte GetR(int x) => (byte)(x >> 16);
        public static byte GetG(int x) => (byte)(x >> 8);
        public static byte GetB(int x) => (byte)(x >> 0);
        public static byte ToByteGray(ushort x) => (byte)(x >> 8);
        public static byte ToByteGray(float x) => (byte)(x <= 0 ? 0 : x >= 1 ? 0xff : (int)(0xff * x));
        public static byte ToByteGray(double x) => (byte)(x <= 0 ? 0 : x >= 1 ? 0xff : (int)(0xff * x));
        public static int ToIntColor(byte a, byte r, byte g, byte b) => (a << 24) | (r << 16) | (g << 8) | b;
        public static int ToIntColor(byte r, byte g, byte b) => ToIntColor((byte)0xff, r, g, b);
        public static int ToIntColor(int a, int r, int g, int b) => ToIntColor((byte)(a < 0 ? 0 : a > 0xff ? 0xff : a), (byte)(r < 0 ? 0 : r > 0xff ? 0xff : r), (byte)(g < 0 ? 0 : g > 0xff ? 0xff : g), (byte)(b < 0 ? 0 : b > 0xff ? 0xff : b));
        public static int ToIntColor(int r, int g, int b) => ToIntColor(0xff, r, g, b);

        public static int ToIntColor(bool x) => (int)(x ? 0xffffffff : 0xff000000);
        public static int ToIntColor(byte x) => (int)(x * 0x10101u | 0xff000000);
        public static int ToIntColor(ushort x) => ToIntColor(ToByteGray(x));
        public static int ToIntColor(float x) => ToIntColor(ToByteGray(x));
        public static int ToIntColor(double x) => ToIntColor(ToByteGray(x));
        public static int ToIntColorHeat(double x)
        {
            var y = x * 4.6 + 2.3;
            return ToIntColor(
                (int)(0x80 * (1 + Math.Cos(y))),
                (int)(0x80 * (1 + Math.Cos(y + Math.PI * 0.5))),
                (int)(0x80 * (1 + Math.Cos(y + Math.PI * 1.0))));
        }
        public static int ToIntColorHSL(double hue, double saturation, double lightness)
        {
            saturation.LetMinMax(0, 1);
            lightness.LetMinMax(0, 1);
            var a = 0x80 * saturation * (1 - Math.Abs(lightness * 2 - 1));
            var b = 0x100 * lightness;
            return ToIntColor(
                (int)(b + a * Math.Cos(hue)),
                (int)(b + a * Math.Cos(hue - Mt.PI2 / 3)),
                (int)(b + a * Math.Cos(hue + Mt.PI2 / 3)));
        }
        public static int ToIntColorHSV(double hue, double saturation, double value)
        {
            saturation.LetMinMax(0, 1);
            value.LetMinMax(0, 1);
            var a = 0x80 * value * saturation;
            var b = 0x80 * value * (2 - saturation);
            return ToIntColor(
                (int)(b + a * Math.Cos(hue)),
                (int)(b + a * Math.Cos(hue - Mt.PI2 / 3)),
                (int)(b + a * Math.Cos(hue + Mt.PI2 / 3)));
        }
        public static int ToIntColor(ComplexS x) => ToIntColorHSV(x.Phase, 1, x.Magnitude);
        public static int ToIntColor(ComplexD x) => ToIntColorHSV(x.Phase, 1, x.Magnitude);

        public static float ToBrightness(byte r, byte g, byte b)
        {
            return (Mt.Min(r, g, b) + Mt.Max(r, g, b)) / (0xff * 2f);
        }
        public static float ToBrightness(int c) => ToBrightness((byte)(c >> 16), (byte)(c >> 8), (byte)(c));

        public static double EncodeSrgb(double v) => (v <= 0.00304) ? v * 12.92 : Math.Pow(v, 1 / 2.4) * 1.055 - 0.055;
        public static double DecodeSrgb(double v) => (v / 12.92 <= 0.00304) ? v / 12.92 : Math.Pow((v + 0.055) / 1.055, 2.4);
        public static double EncodeSrgb255(double v) => EncodeSrgb(v) * 0xff;
        public static double DecodeSrgb255(double v) => DecodeSrgb(v / 0xff);
        public static float EncodeSrgb(float v) => (float)EncodeSrgb((double)v);
        public static float DecodeSrgb(float v) => (float)DecodeSrgb((double)v);
        public static float EncodeSrgb255(float v) => EncodeSrgb(v) * 0xff;
        public static float DecodeSrgb255(float v) => DecodeSrgb(v / 0xff);

        public static byte ToByteGraySrgb(ushort x) => (byte)(0xff * EncodeSrgb(x * (1.0 / 0x10000)));
        public static byte ToByteGraySrgb(float x) => (byte)(x <= 0 ? 0 : x >= 1 ? 0xff : (int)(0xff * EncodeSrgb(x)));
        public static byte ToByteGraySrgb(double x) => (byte)(x <= 0 ? 0 : x >= 1 ? 0xff : (int)(0xff * EncodeSrgb(x)));
        public static int ToIntColorSrgb(ushort x) => ToIntColor(ToByteGraySrgb(x));
        public static int ToIntColorSrgb(float x) => ToIntColor(ToByteGraySrgb(x));
        public static int ToIntColorSrgb(double x) => ToIntColor(ToByteGraySrgb(x));
        public static int ToIntColorHSVSrgb(double hue, double saturation, double value) => ToIntColorHSV(hue, saturation, value <= 0 ? 0 : value >= 1 ? 1 : EncodeSrgb(value));
        public static int ToIntColorSrgb(ComplexS x) => ToIntColorHSVSrgb(x.Phase, 1, x.Magnitude);
        public static int ToIntColorSrgb(ComplexD x) => ToIntColorHSVSrgb(x.Phase, 1, x.Magnitude);
        #endregion

        #region image functions
        public static float[,] EncodeGamma(this float[,] image) => image.Pow(1 / 2.4);
        public static float[,] DecodeGamma(this float[,] image) => image.Pow(2.4);
        public static float[,] EncodeSrgb(this float[,] image) => image.To(EncodeSrgb);
        public static float[,] DecodeSrgb(this float[,] image) => image.To(DecodeSrgb);
        public static float[,] EncodeSrgb255(this float[,] image) => image.To(EncodeSrgb255);
        public static float[,] DecodeSrgb255(this float[,] image) => image.To(DecodeSrgb255);
        #endregion
    }

    #region Op<T>
    public static partial class Op<T> where T : unmanaged
    {
        static T NA { [DoesNotReturn] get { ThrowException.NotImplemented(); return default; } }
        public static T MaxValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (typeof(T) == typeof(short)) return (T)(object)short.MaxValue;
                if (typeof(T) == typeof(int)) return (T)(object)int.MaxValue;
                if (typeof(T) == typeof(long)) return (T)(object)long.MaxValue;
                if (typeof(T) == typeof(float)) return (T)(object)float.MaxValue;
                if (typeof(T) == typeof(double)) return (T)(object)double.MaxValue;
                if (typeof(T) == typeof(decimal)) return (T)(object)decimal.MaxValue;
                return NA;
            }
        }
        public static T MinValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (typeof(T) == typeof(short)) return (T)(object)short.MinValue;
                if (typeof(T) == typeof(int)) return (T)(object)int.MinValue;
                if (typeof(T) == typeof(long)) return (T)(object)long.MinValue;
                if (typeof(T) == typeof(float)) return (T)(object)float.MinValue;
                if (typeof(T) == typeof(double)) return (T)(object)double.MinValue;
                if (typeof(T) == typeof(decimal)) return (T)(object)decimal.MinValue;
                return NA;
            }
        }
        public static T PositiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (typeof(T) == typeof(float)) return (T)(object)float.PositiveInfinity;
                if (typeof(T) == typeof(double)) return (T)(object)double.PositiveInfinity;
                return MaxValue;
            }
        }
        public static T NegativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (typeof(T) == typeof(float)) return (T)(object)float.NegativeInfinity;
                if (typeof(T) == typeof(double)) return (T)(object)double.NegativeInfinity;
                return MinValue;
            }
        }
        public static T NaN
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (typeof(T) == typeof(float)) return (T)(object)float.NaN;
                if (typeof(T) == typeof(double)) return (T)(object)double.NaN;
                if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.NaN;
                if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.NaN;
                return NA;
            }
        }
        public static int Fold
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (typeof(T) == typeof(ComplexD)) return 2;
                if (typeof(T) == typeof(ComplexS)) return 2;
                return 1;
            }
        }
        public static T Zero => default;
        public static T One => From(1);
        public static T NegOne => From(-1);
        public static T Two => From(2);
        public static T From(int x) { Op.LetCast(out T z, x); return z; }
        public static T From(long x) { Op.LetCast(out T z, x); return z; }
        public static T From(float x) { Op.LetCast(out T z, x); return z; }
        public static T From(double x) { Op.LetCast(out T z, x); return z; }
        public static T From(decimal x) { Op.LetCast(out T z, x); return z; }
        public static T From<T1>(T1 x) where T1 : unmanaged { Op.LetCast(out T z, x); return z; }
    }
    #endregion

    #region Op
    public static partial class Op
    {
        [DoesNotReturn] static void NA() => ThrowException.NotImplemented();
        static T NA<T>() where T : unmanaged { NA(); return default; }

        #region Cast
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out T a, int x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)(int)x; return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)(long)x; return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)(float)x; return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)(double)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)(decimal)x; return; }
            if (typeof(T) == typeof(ComplexS)) { LetB(out ComplexS z, (Single)x); a = (T)(object)z; return; }
            if (typeof(T) == typeof(ComplexD)) { LetB(out ComplexD z, (Double)x); a = (T)(object)z; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out T a, long x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)(int)x; return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)(long)x; return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)(float)x; return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)(double)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)(decimal)x; return; }
            if (typeof(T) == typeof(ComplexS)) { LetB(out ComplexS z, (Single)x); a = (T)(object)z; return; }
            if (typeof(T) == typeof(ComplexD)) { LetB(out ComplexD z, (Double)x); a = (T)(object)z; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out T a, float x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)(int)x; return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)(long)x; return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)(float)x; return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)(double)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)(decimal)x; return; }
            if (typeof(T) == typeof(ComplexS)) { LetB(out ComplexS z, (Single)x); a = (T)(object)z; return; }
            if (typeof(T) == typeof(ComplexD)) { LetB(out ComplexD z, (Double)x); a = (T)(object)z; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out T a, double x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)(int)x; return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)(long)x; return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)(float)x; return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)(double)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)(decimal)x; return; }
            if (typeof(T) == typeof(ComplexS)) { LetB(out ComplexS z, (Single)x); a = (T)(object)z; return; }
            if (typeof(T) == typeof(ComplexD)) { LetB(out ComplexD z, (Double)x); a = (T)(object)z; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out T a, decimal x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)(int)x; return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)(long)x; return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)(float)x; return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)(double)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)(decimal)x; return; }
            if (typeof(T) == typeof(ComplexS)) { LetB(out ComplexS z, (Single)x); a = (T)(object)z; return; }
            if (typeof(T) == typeof(ComplexD)) { LetB(out ComplexD z, (Double)x); a = (T)(object)z; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out T a, ComplexS x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexS)) { a = (T)(object)x; return; }
            if (typeof(T) == typeof(ComplexD)) { LetCast(out ComplexD z, x); a = (T)(object)z; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out T a, ComplexD x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexS)) { LetCast(out ComplexS z, x); a = (T)(object)z; return; }
            if (typeof(T) == typeof(ComplexD)) { a = (T)(object)x; return; }
            a = NA<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out int a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (int)(int)(object)x; return; }
            if (typeof(T) == typeof(long)) { a = (int)(long)(object)x; return; }
            if (typeof(T) == typeof(float)) { a = (int)(float)(object)x; return; }
            if (typeof(T) == typeof(double)) { a = (int)(double)(object)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (int)(decimal)(object)x; return; }
            a = NA<Int32>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out long a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (long)(int)(object)x; return; }
            if (typeof(T) == typeof(long)) { a = (long)(long)(object)x; return; }
            if (typeof(T) == typeof(float)) { a = (long)(float)(object)x; return; }
            if (typeof(T) == typeof(double)) { a = (long)(double)(object)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (long)(decimal)(object)x; return; }
            a = NA<Int64>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out float a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (float)(int)(object)x; return; }
            if (typeof(T) == typeof(long)) { a = (float)(long)(object)x; return; }
            if (typeof(T) == typeof(float)) { a = (float)(float)(object)x; return; }
            if (typeof(T) == typeof(double)) { a = (float)(double)(object)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (float)(decimal)(object)x; return; }
            a = NA<float>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out double a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (double)(int)(object)x; return; }
            if (typeof(T) == typeof(long)) { a = (double)(long)(object)x; return; }
            if (typeof(T) == typeof(float)) { a = (double)(float)(object)x; return; }
            if (typeof(T) == typeof(double)) { a = (double)(double)(object)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (double)(decimal)(object)x; return; }
            a = NA<double>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out decimal a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (decimal)(int)(object)x; return; }
            if (typeof(T) == typeof(long)) { a = (decimal)(long)(object)x; return; }
            if (typeof(T) == typeof(float)) { a = (decimal)(float)(object)x; return; }
            if (typeof(T) == typeof(double)) { a = (decimal)(double)(object)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(decimal)(object)x; return; }
            a = NA<decimal>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out ComplexS a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { LetB(out a, (Single)(int)(object)x); return; }
            if (typeof(T) == typeof(long)) { LetB(out a, (Single)(long)(object)x); return; }
            if (typeof(T) == typeof(float)) { LetB(out a, (Single)(float)(object)x); return; }
            if (typeof(T) == typeof(double)) { LetB(out a, (Single)(double)(object)x); return; }
            if (typeof(T) == typeof(decimal)) { LetB(out a, (Single)(decimal)(object)x); return; }
            if (typeof(T) == typeof(ComplexS)) { a = (ComplexS)(object)x; return; }
            if (typeof(T) == typeof(ComplexD)) { LetCast(out a, (ComplexD)(object)x); return; }
            a = NA<ComplexS>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T>(out ComplexD a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { LetB(out a, (Double)(int)(object)x); return; }
            if (typeof(T) == typeof(long)) { LetB(out a, (Double)(long)(object)x); return; }
            if (typeof(T) == typeof(float)) { LetB(out a, (Double)(float)(object)x); return; }
            if (typeof(T) == typeof(double)) { LetB(out a, (Double)(double)(object)x); return; }
            if (typeof(T) == typeof(decimal)) { LetB(out a, (Double)(decimal)(object)x); return; }
            if (typeof(T) == typeof(ComplexS)) { LetCast(out a, (ComplexS)(object)x); return; }
            if (typeof(T) == typeof(ComplexD)) { a = (ComplexD)(object)x; return; }
            a = NA<ComplexD>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetCast<T, T1>(out T a, T1 x) where T : unmanaged where T1 : unmanaged
        {
            if (typeof(T1) == typeof(T)) { a = (T)(object)x; return; }
            if (typeof(T1) == typeof(int)) { LetCast(out a, (int)(object)x); return; }
            if (typeof(T1) == typeof(long)) { LetCast(out a, (long)(object)x); return; }
            if (typeof(T1) == typeof(float)) { LetCast(out a, (float)(object)x); return; }
            if (typeof(T1) == typeof(double)) { LetCast(out a, (double)(object)x); return; }
            if (typeof(T1) == typeof(decimal)) { LetCast(out a, (decimal)(object)x); return; }
            if (typeof(T1) == typeof(ComplexS)) { LetCast(out a, (ComplexS)(object)x); return; }
            if (typeof(T1) == typeof(ComplexD)) { LetCast(out a, (ComplexD)(object)x); return; }
            a = NA<T>();
        }
        public static Int32 CastInt32<T>(T x) where T : unmanaged { LetCast(out Int32 z, x); return z; }
        public static Int64 CastInt64<T>(T x) where T : unmanaged { LetCast(out Int64 z, x); return z; }
        public static Single CastSingle<T>(T x) where T : unmanaged { LetCast(out Single z, x); return z; }
        public static Double CastDouble<T>(T x) where T : unmanaged { LetCast(out Double z, x); return z; }
        public static Decimal CastDecimal<T>(T x) where T : unmanaged { LetCast(out Decimal z, x); return z; }
        public static ComplexS CastComplexS<T>(T x) where T : unmanaged { LetCast(out ComplexS z, x); return z; }
        public static ComplexD CastComplexD<T>(T x) where T : unmanaged { LetCast(out ComplexD z, x); return z; }
        #endregion

        #region boolean operators
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveInfinity<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return float.IsPositiveInfinity((float)(object)x);
            if (typeof(T) == typeof(double)) return double.IsPositiveInfinity((double)(object)x);
            return NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeInfinity<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return float.IsNegativeInfinity((float)(object)x);
            if (typeof(T) == typeof(double)) return double.IsNegativeInfinity((double)(object)x);
            return NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInfinity<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return float.IsInfinity((float)(object)x);
            if (typeof(T) == typeof(double)) return double.IsInfinity((double)(object)x);
            return NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return float.IsNaN((float)(object)x);
            if (typeof(T) == typeof(double)) return double.IsNaN((double)(object)x);
            return NA<bool>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Equ<T>(out bool a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (int)(object)x == (int)(object)y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x == (long)(object)y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x == (float)(object)y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x == (double)(object)y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x == (decimal)(object)y; return; }
            if (typeof(T) == typeof(ComplexS)) { Equ(out a, (ComplexS)(object)x, (ComplexS)(object)y); return; }
            if (typeof(T) == typeof(ComplexD)) { Equ(out a, (ComplexD)(object)x, (ComplexD)(object)y); return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Equ<T>(out bool a, T x, int y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (int)(object)x == y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x == y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x == y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x == y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x == y; return; }
            if (typeof(T) == typeof(ComplexS)) { EquB(out a, (ComplexS)(object)x, (Single)y); return; }
            if (typeof(T) == typeof(ComplexD)) { EquB(out a, (ComplexD)(object)x, (Double)y); return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Equ0<T>(out bool a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (int)(object)x == 0; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x == 0; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x == 0; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x == 0; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x == 0; return; }
            if (typeof(T) == typeof(ComplexS)) { Equ0(out a, (ComplexS)(object)x); return; }
            if (typeof(T) == typeof(ComplexD)) { Equ0(out a, (ComplexD)(object)x); return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Neq0<T>(out bool a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (int)(object)x != 0; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x != 0; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x != 0; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x != 0; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x != 0; return; }
            if (typeof(T) == typeof(ComplexS)) { Neq0(out a, (ComplexS)(object)x); return; }
            if (typeof(T) == typeof(ComplexD)) { Neq0(out a, (ComplexD)(object)x); return; }
            a = NA<bool>();
        }
        public static void Neq<T>(out bool a, T x, T y) where T : unmanaged { Equ(out a, x, y); a = !a; }
        public static void Neq<T>(out bool a, T x, int y) where T : unmanaged { Equ(out a, x, y); a = !a; }
        public static bool Equ<T>(T x, T y) where T : unmanaged { Equ(out bool a, x, y); return a; }
        public static bool Neq<T>(T x, T y) where T : unmanaged { Neq(out bool a, x, y); return a; }
        public static bool Equ<T>(T x, int y) where T : unmanaged { Equ(out bool a, x, y); return a; }
        public static bool Neq<T>(T x, int y) where T : unmanaged { Neq(out bool a, x, y); return a; }
        public static bool Equ0<T>(T x) where T : unmanaged { Equ0(out bool a, x); return a; }
        public static bool Neq0<T>(T x) where T : unmanaged { Neq0(out bool a, x); return a; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LT<T>(out bool a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x < (short)(object)y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x < (int)(object)y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x < (long)(object)y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x < (float)(object)y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x < (double)(object)y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x < (decimal)(object)y; return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GT<T>(out bool a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x > (short)(object)y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x > (int)(object)y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x > (long)(object)y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x > (float)(object)y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x > (double)(object)y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x > (decimal)(object)y; return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LE<T>(out bool a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x <= (short)(object)y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x <= (int)(object)y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x <= (long)(object)y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x <= (float)(object)y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x <= (double)(object)y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x <= (decimal)(object)y; return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GE<T>(out bool a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x >= (short)(object)y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x >= (int)(object)y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x >= (long)(object)y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x >= (float)(object)y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x >= (double)(object)y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x >= (decimal)(object)y; return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LT<T>(out bool a, T x, int y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x < y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x < y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x < y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x < y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x < y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x < y; return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GT<T>(out bool a, T x, int y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x > y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x > y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x > y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x > y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x > y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x > y; return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LE<T>(out bool a, T x, int y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x <= y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x <= y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x <= y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x <= y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x <= y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x <= y; return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GE<T>(out bool a, T x, int y) where T : unmanaged
        {
            if (typeof(T) == typeof(short)) { a = (short)(object)x >= y; return; }
            if (typeof(T) == typeof(int)) { a = (int)(object)x >= y; return; }
            if (typeof(T) == typeof(long)) { a = (long)(object)x >= y; return; }
            if (typeof(T) == typeof(float)) { a = (float)(object)x >= y; return; }
            if (typeof(T) == typeof(double)) { a = (double)(object)x >= y; return; }
            if (typeof(T) == typeof(decimal)) { a = (decimal)(object)x >= y; return; }
            a = NA<bool>();
        }
        public static bool LT<T>(T x, T y) where T : unmanaged { LT(out bool a, x, y); return a; }
        public static bool GT<T>(T x, T y) where T : unmanaged { GT(out bool a, x, y); return a; }
        public static bool LE<T>(T x, T y) where T : unmanaged { LE(out bool a, x, y); return a; }
        public static bool GE<T>(T x, T y) where T : unmanaged { GE(out bool a, x, y); return a; }
        public static bool LT<T>(T x, int y) where T : unmanaged { LT(out bool a, x, y); return a; }
        public static bool GT<T>(T x, int y) where T : unmanaged { GT(out bool a, x, y); return a; }
        public static bool LE<T>(T x, int y) where T : unmanaged { LE(out bool a, x, y); return a; }
        public static bool GE<T>(T x, int y) where T : unmanaged { GE(out bool a, x, y); return a; }
        public static void LetMin<T>(ref T x, T y) where T : unmanaged { GT(out bool a, x, y); if (a) x = y; }
        public static void LetMax<T>(ref T x, T y) where T : unmanaged { LT(out bool a, x, y); if (a) x = y; }

        public static bool IsComplexReal<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexS)) return ((ComplexS)(object)x).Im == 0;
            if (typeof(T) == typeof(ComplexD)) return ((ComplexD)(object)x).Im == 0;
            return false;
        }
        #endregion

        #region basic operators
        public static void Pos<T>(out T a, T x) where T : unmanaged { a = x; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Neg<T>(out T a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)-(int)(object)x; return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)-(long)(object)x; return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)-(float)(object)x; return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)-(double)(object)x; return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)-(decimal)(object)x; return; }
            if (typeof(T) == typeof(TimeSpan)) { a = (T)(object)-(TimeSpan)(object)x; return; }
            if (typeof(T) == typeof(ComplexS)) { Neg(out ComplexS b, (ComplexS)(object)x); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Neg(out ComplexD b, (ComplexD)(object)x); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Tld<T>(out T a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(bool)) { a = (T)(object)!(bool)(object)x; return; }
            if (typeof(T) == typeof(int)) { a = (T)(object)~(int)(object)x; return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)~(long)(object)x; return; }
            if (typeof(T) == typeof(ComplexS)) { Cnj(out ComplexS b, (ComplexS)(object)x); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Cnj(out ComplexD b, (ComplexD)(object)x); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Inv<T>(out T a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) { a = (T)(object)(1 / (float)(object)x); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)(1 / (double)(object)x); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)(1 / (decimal)(object)x); return; }
            if (typeof(T) == typeof(ComplexS)) { Inv(out ComplexS b, (ComplexS)(object)x); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Inv(out ComplexD b, (ComplexD)(object)x); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cnj<T>(out T a, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexS)) { Cnj(out ComplexS b, (ComplexS)(object)x); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Cnj(out ComplexD b, (ComplexD)(object)x); a = (T)(object)b; return; }
            a = x;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(out T a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x + (int)(object)y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x + (long)(object)y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x + (float)(object)y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x + (double)(object)y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x + (decimal)(object)y); return; }
            if (typeof(T) == typeof(TimeSpan)) { a = (T)(object)((TimeSpan)(object)x + (TimeSpan)(object)y); return; }
            if (typeof(T) == typeof(ComplexS)) { Add(out ComplexS b, (ComplexS)(object)x, (ComplexS)(object)y); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Add(out ComplexD b, (ComplexD)(object)x, (ComplexD)(object)y); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sub<T>(out T a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x - (int)(object)y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x - (long)(object)y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x - (float)(object)y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x - (double)(object)y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x - (decimal)(object)y); return; }
            if (typeof(T) == typeof(TimeSpan)) { a = (T)(object)((TimeSpan)(object)x - (TimeSpan)(object)y); return; }
            if (typeof(T) == typeof(ComplexS)) { Sub(out ComplexS b, (ComplexS)(object)x, (ComplexS)(object)y); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Sub(out ComplexD b, (ComplexD)(object)x, (ComplexD)(object)y); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Mul<T>(out T a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x * (int)(object)y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x * (long)(object)y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x * (float)(object)y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x * (double)(object)y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x * (decimal)(object)y); return; }
            if (typeof(T) == typeof(ComplexS)) { Mul(out ComplexS b, (ComplexS)(object)x, (ComplexS)(object)y); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Mul(out ComplexD b, (ComplexD)(object)x, (ComplexD)(object)y); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cul<T>(out T a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x * (int)(object)y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x * (long)(object)y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x * (float)(object)y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x * (double)(object)y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x * (decimal)(object)y); return; }
            if (typeof(T) == typeof(ComplexS)) { Cul(out ComplexS b, (ComplexS)(object)x, (ComplexS)(object)y); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Cul(out ComplexD b, (ComplexD)(object)x, (ComplexD)(object)y); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Div<T>(out T a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x / (int)(object)y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x / (long)(object)y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x / (float)(object)y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x / (double)(object)y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x / (decimal)(object)y); return; }
            if (typeof(T) == typeof(ComplexS)) { Div(out ComplexS b, (ComplexS)(object)x, (ComplexS)(object)y); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Div(out ComplexD b, (ComplexD)(object)x, (ComplexD)(object)y); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Mod<T>(out T a, T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x % (int)(object)y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x % (long)(object)y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x % (float)(object)y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x % (double)(object)y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x % (decimal)(object)y); return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Div<T>(out T a, T x, long y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x / y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x / y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x / y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x / y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x / y); return; }
            if (typeof(T) == typeof(ComplexS)) { Div(out ComplexS b, (ComplexS)(object)x, y); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Div(out ComplexD b, (ComplexD)(object)x, y); a = (T)(object)b; return; }
            a = NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Div<T>(out T a, T x, double y) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) { a = (T)(object)((int)(object)x / y); return; }
            if (typeof(T) == typeof(long)) { a = (T)(object)((long)(object)x / y); return; }
            if (typeof(T) == typeof(float)) { a = (T)(object)((float)(object)x / y); return; }
            if (typeof(T) == typeof(double)) { a = (T)(object)((double)(object)x / y); return; }
            if (typeof(T) == typeof(decimal)) { a = (T)(object)((decimal)(object)x / (decimal)y); return; }
            if (typeof(T) == typeof(ComplexS)) { Div(out ComplexS b, (ComplexS)(object)x, y); a = (T)(object)b; return; }
            if (typeof(T) == typeof(ComplexD)) { Div(out ComplexD b, (ComplexD)(object)x, y); a = (T)(object)b; return; }
            a = NA<T>();
        }
        public static void MulAdd<T>(out T a, T x, T y, T z) where T : unmanaged { Mul(out T b, x, y); Add(out a, b, z); }
        public static void CulAdd<T>(out T a, T x, T y, T z) where T : unmanaged { Cul(out T b, x, y); Add(out a, b, z); }

        public static T Pos<T>(T x) where T : unmanaged => x;
        public static T Neg<T>(T x) where T : unmanaged { Neg(out T a, x); return a; }
        public static T Tld<T>(T x) where T : unmanaged { Tld(out T a, x); return a; }
        public static T Inv<T>(T x) where T : unmanaged { Inv(out T a, x); return a; }
        public static T Cnj<T>(T x) where T : unmanaged { Cnj(out T a, x); return a; }
        public static T Add<T>(T x, T y) where T : unmanaged { Add(out T a, x, y); return a; }
        public static T Sub<T>(T x, T y) where T : unmanaged { Sub(out T a, x, y); return a; }
        public static T Mul<T>(T x, T y) where T : unmanaged { Mul(out T a, x, y); return a; }
        public static T Div<T>(T x, T y) where T : unmanaged { Div(out T a, x, y); return a; }
        public static T Mod<T>(T x, T y) where T : unmanaged { Mod(out T a, x, y); return a; }
        public static T Cul<T>(T x, T y) where T : unmanaged { Cul(out T a, x, y); return a; }
        public static T Div<T>(T x, long y) where T : unmanaged { Div(out T a, x, y); return a; }

        public static void LetNeg<T>(ref T x) where T : unmanaged => Neg(out x, x);
        public static void LetTld<T>(ref T x) where T : unmanaged => Tld(out x, x);
        public static void LetInv<T>(ref T x) where T : unmanaged => Inv(out x, x);
        public static void LetCnj<T>(ref T x) where T : unmanaged => Cnj(out x, x);
        public static void LetAdd<T>(ref T x, T y) where T : unmanaged => Add(out x, x, y);
        public static void LetSub<T>(ref T x, T y) where T : unmanaged => Sub(out x, x, y);
        public static void LetMul<T>(ref T x, T y) where T : unmanaged => Mul(out x, x, y);
        public static void LetCul<T>(ref T x, T y) where T : unmanaged => Cul(out x, x, y);
        public static void LetDiv<T>(ref T x, T y) where T : unmanaged => Div(out x, x, y);
        public static void LetMod<T>(ref T x, T y) where T : unmanaged => Mod(out x, x, y);
        public static void LetSubr<T>(ref T x, T y) where T : unmanaged => Sub(out x, y, x);
        public static void LetDivr<T>(ref T x, T y) where T : unmanaged => Div(out x, y, x);
        public static void LetModr<T>(ref T x, T y) where T : unmanaged => Mod(out x, y, x);
        public static void LetDiv<T>(ref T x, long y) where T : unmanaged => Div(out x, x, y);
        public static void LetMulAdd<T>(ref T x, T y, T z) where T : unmanaged => MulAdd(out x, x, y, z);
        public static void LetAddMul<T>(ref T x, T y, T z) where T : unmanaged => MulAdd(out x, y, z, x);
        public static void LetCulAdd<T>(ref T x, T y, T z) where T : unmanaged => CulAdd(out x, x, y, z);
        public static void LetAddCul<T>(ref T x, T y, T z) where T : unmanaged => CulAdd(out x, y, z, x);
        #endregion

        #region mathematical functions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Abs<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) return (T)(object)Math.Abs((int)(object)x);
            if (typeof(T) == typeof(long)) return (T)(object)Math.Abs((long)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)Math.Abs((float)(object)x);
            if (typeof(T) == typeof(double)) return (T)(object)Math.Abs((double)(object)x);
            if (typeof(T) == typeof(decimal)) return (T)(object)Math.Abs((decimal)(object)x);
            if (typeof(T) == typeof(ComplexD)) { ComplexD a; a.Re = BAbs((ComplexD)(object)x); a.Im = default; return (T)(object)a; }
            if (typeof(T) == typeof(ComplexS)) { ComplexS a; a.Re = BAbs((ComplexS)(object)x); a.Im = default; return (T)(object)a; }
            return NA<T>();
        }
        public static T Sq<T>(T x) where T : unmanaged => Mul(x, x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AbsSq<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) { ComplexD a; a.Re = BAbsSq((ComplexD)(object)x); a.Im = default; return (T)(object)a; }
            if (typeof(T) == typeof(ComplexS)) { ComplexS a; a.Re = BAbsSq((ComplexS)(object)x); a.Im = default; return (T)(object)a; }
            return Sq(x);
        }
        public static T AbsSqSub<T>(T x, T y) where T : unmanaged { Sub(out T a, x, y); return AbsSq(a); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DAbs<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return DAbs((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return DAbs((ComplexS)(object)x);
            return Math.Abs(CastDouble(x));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DAbsSq<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return DAbsSq((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return DAbsSq((ComplexS)(object)x);
            return CastDouble(x).Sq();
        }
        public static Double DAbsSqSub<T>(T x, T y) where T : unmanaged { Sub(out T a, x, y); return DAbsSq(a); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sign<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(int)) return (T)(object)(int)Math.Sign((int)(object)x);
            if (typeof(T) == typeof(long)) return (T)(object)(long)Math.Sign((long)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)(float)Math.Sign((float)(object)x);
            if (typeof(T) == typeof(double)) return (T)(object)(double)Math.Sign((double)(object)x);
            if (typeof(T) == typeof(decimal)) return (T)(object)(decimal)Math.Sign((decimal)(object)x);
            if (typeof(T) == typeof(ComplexD)) return (T)(object)((ComplexD)(object)x).Sign();
            if (typeof(T) == typeof(ComplexS)) return (T)(object)((ComplexS)(object)x).Sign();
            return NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sqrt<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Sqrt((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Sqrt((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Sqrt((float)(object)x);
            return Op<T>.From(Math.Sqrt(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pow<T>(T x, double y) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Pow((ComplexD)(object)x, y);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Pow((ComplexS)(object)x, y);
            return Op<T>.From(Math.Pow(CastDouble(x), y));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pow<T>(T x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Pow((ComplexD)(object)x, (ComplexD)(object)y);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Pow((ComplexS)(object)x, (ComplexS)(object)y);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Pow((float)(object)x, (float)(object)y);
            return Op<T>.From(Math.Pow(CastDouble(x), CastDouble(y)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Exp<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Exp((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Exp((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Exp((float)(object)x);
            return Op<T>.From(Math.Exp(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Log<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Log((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Log((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Log((float)(object)x);
            return Op<T>.From(Math.Log(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Log<T>(T x, double y) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Log((ComplexD)(object)x, y);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Log((ComplexS)(object)x, (float)y);
            return Op<T>.From(Math.Log(CastDouble(x), y));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Cos<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Cos((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Cos((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Cos((float)(object)x);
            return Op<T>.From(Math.Cos((double)(object)x));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sin<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Sin((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Sin((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Sin((float)(object)x);
            return Op<T>.From(Math.Sin(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Tan<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Tan((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Tan((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Tan((float)(object)x);
            return Op<T>.From(Math.Tan(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Cosh<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Cosh((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Cosh((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Cosh((float)(object)x);
            return Op<T>.From(Math.Cosh(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sinh<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Sinh((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Sinh((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Sinh((float)(object)x);
            return Op<T>.From(Math.Sinh(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Tanh<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Tanh((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Tanh((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Tanh((float)(object)x);
            return Op<T>.From(Math.Tanh(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Acos<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Acos((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Acos((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Cosh((float)(object)x);
            return Op<T>.From(Math.Acos(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Asin<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Asin((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Asin((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Cosh((float)(object)x);
            return Op<T>.From(Math.Asin(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Atan<T>(T x) where T : unmanaged
        {
            if (typeof(T) == typeof(ComplexD)) return (T)(object)ComplexD.Atan((ComplexD)(object)x);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)ComplexS.Atan((ComplexS)(object)x);
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Atan((float)(object)x);
            return Op<T>.From(Math.Atan(CastDouble(x)));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Atan2<T>(T y, T x) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)MathF.Atan2((float)(object)x, (float)(object)y);
            return Op<T>.From(Math.Atan2(CastDouble(y), CastDouble(x)));
        }
        #endregion

        #region Complex<T>
        public static void Let<T>(out Complex<T> x, Complex<T> y) where T : unmanaged { x.Re = y.Re; x.Im = y.Im; }
        public static void LetB<T>(out Complex<T> z, T x) where T : unmanaged { z.Re = x; z.Im = default; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static void LetCast(out ComplexS x, ComplexD y) { x.Re = (float)y.Re; x.Im = (float)y.Im; }
        public static void LetCast(out ComplexD x, ComplexS y) { x.Re = y.Re; x.Im = y.Im; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EquB<T>(out bool a, Complex<T> x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) { EquB(out a, (ComplexS)(object)x, (Single)(object)y); return; }
            if (typeof(T) == typeof(Double)) { EquB(out a, (ComplexD)(object)x, (Double)(object)y); return; }
            a = NA<bool>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NeqB<T>(out bool a, Complex<T> x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) { NeqB(out a, (ComplexS)(object)x, (Single)(object)y); return; }
            if (typeof(T) == typeof(Double)) { NeqB(out a, (ComplexD)(object)x, (Double)(object)y); return; }
            a = NA<bool>();
        }
        public static void AddB<T>(out Complex<T> a, Complex<T> x, T y) where T : unmanaged { Add(out a.Re, x.Re, y); a.Im = x.Im; }
        public static void SubB<T>(out Complex<T> a, Complex<T> x, T y) where T : unmanaged { Sub(out a.Re, x.Re, y); a.Im = x.Im; }
        public static void MulB<T>(out Complex<T> a, Complex<T> x, T y) where T : unmanaged { Mul(out a.Re, x.Re, y); Mul(out a.Im, x.Im, y); }
        public static void CulB<T>(out Complex<T> a, Complex<T> x, T y) where T : unmanaged { Mul(out a.Re, x.Re, y); Neg(out var b, x.Im); Mul(out a.Im, b, y); }
        public static void DivB<T>(out Complex<T> a, Complex<T> x, T y) where T : unmanaged { Div(out a.Re, x.Re, y); Div(out a.Im, x.Im, y); }
        public static void SubrB<T>(out Complex<T> a, Complex<T> x, T y) where T : unmanaged { Sub(out a.Re, y, x.Re); Neg(out a.Im, x.Im); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DivrB<T>(out Complex<T> a, Complex<T> x, T y) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) { DivrB(out var b, (ComplexS)(object)x, (Single)(object)y); a = (Complex<T>)(object)b; return; }
            if (typeof(T) == typeof(Double)) { DivrB(out var b, (ComplexD)(object)x, (Double)(object)y); a = (Complex<T>)(object)b; return; }
            a = NA<Complex<T>>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MulBAdd<T>(out Complex<T> a, Complex<T> x, T y, Complex<T> z) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) { MulBAdd(out var b, (ComplexS)(object)x, (Single)(object)y, (ComplexS)(object)z); a = (Complex<T>)(object)b; return; }
            if (typeof(T) == typeof(Double)) { MulBAdd(out var b, (ComplexD)(object)x, (Double)(object)y, (ComplexD)(object)z); a = (Complex<T>)(object)b; return; }
            a = NA<Complex<T>>();
        }
        public static void LetMulBAdd<T>(ref Complex<T> x, T y, Complex<T> z) where T : unmanaged => MulBAdd(out x, x, y, z);
        public static void LetAddMulB<T>(ref Complex<T> x, Complex<T> y, T z) where T : unmanaged => MulBAdd(out x, y, z, x);

        public static bool EquB<T>(Complex<T> x, T y) where T : unmanaged { EquB(out bool a, x, y); return a; }
        public static bool NeqB<T>(Complex<T> x, T y) where T : unmanaged { NeqB(out bool a, x, y); return a; }
        public static void LetAddB<T>(ref Complex<T> x, T y) where T : unmanaged => AddB(out x, x, y);
        public static void LetSubB<T>(ref Complex<T> x, T y) where T : unmanaged => SubB(out x, x, y);
        public static void LetMulB<T>(ref Complex<T> x, T y) where T : unmanaged => MulB(out x, x, y);
        public static void LetDivB<T>(ref Complex<T> x, T y) where T : unmanaged => DivB(out x, x, y);
        public static void LetSubrB<T>(ref Complex<T> x, T y) where T : unmanaged => SubrB(out x, x, y);
        public static void LetDivrB<T>(ref Complex<T> x, T y) where T : unmanaged => DivrB(out x, x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BAbs<T>(Complex<T> x) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) return (T)(object)BAbs((ComplexS)(object)x);
            if (typeof(T) == typeof(Double)) return (T)(object)BAbs((ComplexD)(object)x);
            return NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BAbsSq<T>(Complex<T> x) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) return (T)(object)BAbsSq((ComplexS)(object)x);
            if (typeof(T) == typeof(Double)) return (T)(object)BAbsSq((ComplexD)(object)x);
            return NA<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DAbs<T>(Complex<T> x) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) return DAbs((ComplexS)(object)x);
            if (typeof(T) == typeof(Double)) return DAbs((ComplexD)(object)x);
            return NA<Double>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DAbsSq<T>(Complex<T> x) where T : unmanaged
        {
            if (typeof(T) == typeof(Single)) return DAbsSq((ComplexS)(object)x);
            if (typeof(T) == typeof(Double)) return DAbsSq((ComplexD)(object)x);
            return NA<Double>();
        }
        #endregion

        #region Complex<float>
        public static void Equ(out bool a, ComplexS x, ComplexS y) { a = x.Re == y.Re && x.Im == y.Im; }
        public static void Neq(out bool a, ComplexS x, ComplexS y) { a = x.Re != y.Re || x.Im != y.Im; }
        public static void EquB(out bool a, ComplexS x, float y) { a = x.Re == y && x.Im == 0; }
        public static void NeqB(out bool a, ComplexS x, float y) { a = x.Re != y || x.Im != 0; }
        public static void Equ0(out bool a, ComplexS x) { a = x.Re == 0 && x.Im == 0; }
        public static void Neq0(out bool a, ComplexS x) { a = x.Re != 0 || x.Im != 0; }

        public static void Neg(out ComplexS z, ComplexS x) { z.Re = -x.Re; z.Im = -x.Im; }
        public static void Cnj(out ComplexS z, ComplexS x) { z.Re = x.Re; z.Im = -x.Im; }
        public static void Add(out ComplexS z, ComplexS x, ComplexS y) { z.Re = x.Re + y.Re; z.Im = x.Im + y.Im; }
        public static void Sub(out ComplexS z, ComplexS x, ComplexS y) { z.Re = x.Re - y.Re; z.Im = x.Im - y.Im; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Mul(out ComplexS z, ComplexS x, ComplexS y)
        {
            var a = x.Re * y.Re - x.Im * y.Im;
            z.Im = x.Re * y.Im + x.Im * y.Re;
            z.Re = a;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cul(out ComplexS z, ComplexS x, ComplexS y)
        {
            var a = x.Re * y.Re + x.Im * y.Im;
            z.Im = x.Re * y.Im - x.Im * y.Re;
            z.Re = a;
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void Mul1(out ComplexS z, ComplexS x, ComplexS y)
        //{
        //    var a = (x.Re + x.Im) * y.Re;
        //    z.Im = a - x.Re * (y.Re - y.Im);
        //    z.Re = a - x.Im * (y.Re + y.Im);
        //}
        public static void Div(out ComplexS z, ComplexS x, ComplexS y)
        {
            var xRe = x.Re;
            var xIm = x.Im;
            var yRe = y.Re;
            var yIm = y.Im;
            if (yIm == 0) { DivB(out z, x, yRe); return; }
            if (xIm == 0) { DivrB(out z, y, xRe); return; }
            if (Math.Abs(yRe) > Math.Abs(yIm))
            {
                var a = yIm / yRe;
                var b = yRe + yIm * a;
                z.Re = (xRe + xIm * a) / b;
                z.Im = (xIm - xRe * a) / b;
            }
            else
            {
                var a = yRe / yIm;
                var b = yIm + yRe * a;
                z.Re = (xRe * a + xIm) / b;
                z.Im = (xIm * a - xRe) / b;
            }
        }
        public static void Div(out ComplexS z, ComplexS x, long y) { z.Re = x.Re / y; z.Im = x.Im / y; }
        public static void Div(out ComplexS z, ComplexS x, double y) { z.Re = (float)((double)x.Re / y); z.Im = (float)((double)x.Im / y); }
        public static void MulB(out ComplexS z, ComplexS x, float y) { z.Re = x.Re * y; z.Im = x.Im * y; }
        public static void DivB(out ComplexS z, ComplexS x, float y) { z.Re = x.Re / y; z.Im = x.Im / y; }
        public static void DivrB(out ComplexS z, ComplexS y, float x)
        {
            var yRe = y.Re;
            var yIm = y.Im;
            if (yIm == 0) { DivB(out z, x, yRe); return; }
            if (Math.Abs(yRe) > Math.Abs(yIm))
            {
                var a = yIm / yRe;
                var b = yRe + yIm * a;
                z.Re = x / b;
                z.Im = -x * a / b;
            }
            else
            {
                var a = yRe / yIm;
                var b = yIm + yRe * a;
                z.Re = x * a / b;
                z.Im = -x / b;
            }
        }
        public static void MulBAdd(out ComplexS a, ComplexS x, float y, ComplexS z) { a.Re = x.Re * y + z.Re; a.Im = x.Im * y + z.Im; }
        public static void MulAdd(out float a, float x, float y, float z) { a = x * y + z; }
        public static float BAbs(ComplexS x) => Mt.Norm2_(x.Re, x.Im);
        public static float BAbsSq(ComplexS x) => x.Re * x.Re + x.Im * x.Im;
        public static Double DAbs(ComplexS x) => Mt.Norm2_((Double)x.Re, (Double)x.Im);
        public static Double DAbsSq(ComplexS x) => (Double)x.Re * x.Re + (Double)x.Im * x.Im;

        public static void LetAdd(ref ComplexS x, ComplexS y) => Add(out x, x, y);
        public static void LetSub(ref ComplexS x, ComplexS y) => Sub(out x, x, y);
        public static void LetMul(ref ComplexS x, ComplexS y) => Mul(out x, x, y);
        public static void LetDiv(ref ComplexS x, ComplexS y) => Div(out x, x, y);
        public static void LetB(out ComplexS x, float y) { x.Re = y; x.Im = 0; }
        public static void LetMulB(ref ComplexS x, float y) { x.Re *= y; x.Im *= y; }
        public static void LetDivB(ref ComplexS x, float y) { x.Re /= y; x.Im /= y; }
        #endregion

        #region Complex<double>
        public static void Equ(out bool a, ComplexD x, ComplexD y) { a = x.Re == y.Re && x.Im == y.Im; }
        public static void Neq(out bool a, ComplexD x, ComplexD y) { a = x.Re != y.Re || x.Im != y.Im; }
        public static void EquB(out bool a, ComplexD x, double y) { a = x.Re == y && x.Im == 0; }
        public static void NeqB(out bool a, ComplexD x, double y) { a = x.Re != y || x.Im != 0; }
        public static void Equ0(out bool a, ComplexD x) { a = x.Re == 0 && x.Im == 0; }
        public static void Neq0(out bool a, ComplexD x) { a = x.Re != 0 || x.Im != 0; }

        public static void Neg(out ComplexD z, ComplexD x) { z.Re = -x.Re; z.Im = -x.Im; }
        public static void Cnj(out ComplexD z, ComplexD x) { z.Re = x.Re; z.Im = -x.Im; }
        public static void Add(out ComplexD z, ComplexD x, ComplexD y) { z.Re = x.Re + y.Re; z.Im = x.Im + y.Im; }
        public static void Sub(out ComplexD z, ComplexD x, ComplexD y) { z.Re = x.Re - y.Re; z.Im = x.Im - y.Im; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Mul(out ComplexD z, ComplexD x, ComplexD y)
        {
            var a = x.Re * y.Re - x.Im * y.Im;
            z.Im = x.Re * y.Im + x.Im * y.Re;
            z.Re = a;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cul(out ComplexD z, ComplexD x, ComplexD y)
        {
            var a = x.Re * y.Re + x.Im * y.Im;
            z.Im = x.Re * y.Im - x.Im * y.Re;
            z.Re = a;
        }
        public static void Div(out ComplexD z, ComplexD x, ComplexD y)
        {
            var xRe = x.Re;
            var xIm = x.Im;
            var yRe = y.Re;
            var yIm = y.Im;
            if (yIm == 0) { DivB(out z, x, yRe); return; }
            if (xIm == 0) { DivrB(out z, y, xRe); return; }
            if (Math.Abs(yRe) > Math.Abs(yIm))
            {
                var a = yIm / yRe;
                var b = yRe + yIm * a;
                z.Re = (xRe + xIm * a) / b;
                z.Im = (xIm - xRe * a) / b;
            }
            else
            {
                var a = yRe / yIm;
                var b = yIm + yRe * a;
                z.Re = (xRe * a + xIm) / b;
                z.Im = (xIm * a - xRe) / b;
            }
        }
        public static void Div(out ComplexD z, ComplexD x, long y) { z.Re = x.Re / y; z.Im = x.Im / y; }
        public static void Div(out ComplexD z, ComplexD x, double y) { z.Re = x.Re / y; z.Im = x.Im / y; }
        public static void MulB(out ComplexD z, ComplexD x, double y) { z.Re = x.Re * y; z.Im = x.Im * y; }
        public static void DivB(out ComplexD z, ComplexD x, double y) { z.Re = x.Re / y; z.Im = x.Im / y; }
        public static void DivrB(out ComplexD z, ComplexD y, double x)
        {
            var yRe = y.Re;
            var yIm = y.Im;
            if (yIm == 0) { DivB(out z, x, yRe); return; }
            if (Math.Abs(yRe) > Math.Abs(yIm))
            {
                var a = yIm / yRe;
                var b = yRe + yIm * a;
                z.Re = x / b;
                z.Im = -x * a / b;
            }
            else
            {
                var a = yRe / yIm;
                var b = yIm + yRe * a;
                z.Re = x * a / b;
                z.Im = -x / b;
            }
        }
        public static void MulBAdd(out ComplexD a, ComplexD x, double y, ComplexD z) { a.Re = x.Re * y + z.Re; a.Im = x.Im * y + z.Im; }
        public static void MulAdd(out double a, double x, double y, double z) { a = x * y + z; }
        public static double BAbs(ComplexD x) => Mt.Norm2_(x.Re, x.Im);
        public static double BAbsSq(ComplexD x) => x.Re * x.Re + x.Im * x.Im;
        public static Double DAbs(ComplexD x) => Mt.Norm2_((Double)x.Re, (Double)x.Im);
        public static Double DAbsSq(ComplexD x) => (Double)x.Re * x.Re + (Double)x.Im * x.Im;

        public static void LetAdd(ref ComplexD x, ComplexD y) => Add(out x, x, y);
        public static void LetSub(ref ComplexD x, ComplexD y) => Sub(out x, x, y);
        public static void LetMul(ref ComplexD x, ComplexD y) => Mul(out x, x, y);
        public static void LetDiv(ref ComplexD x, ComplexD y) => Div(out x, x, y);
        public static void LetB(out ComplexD x, double y) { x.Re = y; x.Im = 0; }
        public static void LetMulB(ref ComplexD x, double y) { x.Re *= y; x.Im *= y; }
        public static void LetDivB(ref ComplexD x, double y) { x.Re /= y; x.Im /= y; }
        #endregion

        #region Complex<T>
#pragma warning disable IDE1006
        public static void _Mul<T>(out Complex<T> a, Complex<T> x, Complex<T> y) where T : unmanaged
        {
            a.Re = Sub(Mul(x.Re, y.Re), Mul(x.Im, y.Im));
            a.Im = Add(Mul(x.Im, y.Re), Mul(x.Re, y.Im));
        }
        public static void _DivrB<T>(out Complex<T> a, Complex<T> z, T y) where T : unmanaged
        {
            T c = z.Re;
            T d = z.Im;
            if (LT(Abs(d), Abs(c)))
            {
                T doc = Div(d, c);
                a.Re = Div(y, Add(c, Mul(d, doc)));
                a.Im = Div(Neg(Mul(y, doc)), Add(c, Mul(d, doc)));
            }
            else
            {
                T cod = Div(c, d);
                a.Re = Div(Mul(y, cod), Add(d, Mul(c, cod)));
                a.Im = Div(Neg(y), Add(d, Mul(c, cod)));
            }
        }
        public static T _BAbs<T>(Complex<T> x) where T : unmanaged
        {
            if (IsInfinity(x.Re) || IsInfinity(x.Im)) return Op<T>.PositiveInfinity;
            T c = Abs(x.Re);
            T d = Abs(x.Im);
            if (GT(c, d))
            {
                T r = Div(d, c);
                return Mul(c, Sqrt(Add(Op<T>.One, Mul(r, r))));
            }
            if (Equ(d, 0)) return c;  // c is either 0.0 or NaN
            {
                T r = Div(c, d);
                return Mul(d, Sqrt(Add(Op<T>.One, Mul(r, r))));
            }
        }
        public static Double _DAbs<T>(Complex<T> x) where T : unmanaged
        {
            if (IsInfinity(x.Re) || IsInfinity(x.Im)) return Double.PositiveInfinity;
            return Mt.Norm2(CastDouble(x.Re), CastDouble(x.Im));
        }
        public static void _Sign<T>(out Complex<T> a, Complex<T> x) where T : unmanaged
        {
            var r = BAbs(x);
            if (Equ(r, 0)) a = default; else DivB(out a, x, r);
        }
#pragma warning restore IDE1006
        #endregion

        #region SumPair8
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SumPair4<T>(out T a, ref T data) where T : unmanaged
        {
            var d3 = Unsafe.Add(ref data, 3);
            var d2 = Unsafe.Add(ref data, 2);
            Add(out var a1, d2, d3);
            var d1 = Unsafe.Add(ref data, 1);
            var d0 = Unsafe.Add(ref data, 0);
            Add(out var a0, d0, d1);
            Add(out a, a0, a1);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SumPair8<T>(out T a, ref T data) where T : unmanaged
        {
            var d7 = Unsafe.Add(ref data, 7);
            var d6 = Unsafe.Add(ref data, 6);
            Add(out var a3, d6, d7);
            var d5 = Unsafe.Add(ref data, 5);
            var d4 = Unsafe.Add(ref data, 4);
            Add(out var a2, d4, d5);
            Add(out var b1, a2, a3);
            var d3 = Unsafe.Add(ref data, 3);
            var d2 = Unsafe.Add(ref data, 2);
            Add(out var a1, d2, d3);
            var d1 = Unsafe.Add(ref data, 1);
            var d0 = Unsafe.Add(ref data, 0);
            Add(out var a0, d0, d1);
            Add(out var b0, a0, a1);
            Add(out a, b0, b1);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SumPair8<T>(out T a, T* data) where T : unmanaged
        {
            var d7 = data[7];
            var d6 = data[6];
            Add(out var a3, d6, d7);
            var d5 = data[5];
            var d4 = data[4];
            Add(out var a2, d4, d5);
            Add(out var b1, a2, a3);
            var d3 = data[3];
            var d2 = data[2];
            Add(out var a1, d2, d3);
            var d1 = data[1];
            var d0 = data[0];
            Add(out var a0, d0, d1);
            Add(out var b0, a0, a1);
            Add(out a, b0, b1);
        }
        #endregion
    }
    #endregion

    // Unsafe array functions
    public static unsafe partial class Us
    {
        #region common
        public static void Clear<T>(T* r, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = default; }
        public static void Let<T>(T* r, T* p, int n) where T : unmanaged { if (r != p) for (int i = n; --i >= 0;) r[i] = p[i]; }
        public static void Let<T>(T* r, T p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = p; }
        public static void LetRev<T>(T* r, int n) where T : unmanaged { for (int i = n / 2; --i >= 0;) { var a = r[n - 1 - i]; r[n - 1 - i] = r[i]; r[i] = a; } }
        public static void LetMask<T>(T* r, bool* q, int n) where T : unmanaged => Mask(r, r, q, n);
        public static void LetMaskNot<T>(T* r, bool* q, int n) where T : unmanaged => MaskNot(r, r, q, n);
        public static void Rev<T>(T* r, T* p, int n) where T : unmanaged { if (r == p) LetRev(r, n); else for (int i = n; --i >= 0;) r[i] = p[n - 1 - i]; }
        public static void Mask<T>(T* r, T* p, bool* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = q[i] ? p[i] : default; }
        public static void MaskNot<T>(T* r, T* p, bool* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = !q[i] ? p[i] : default; }
        public static void Cast<T, T1>(T* r, T1* p, int n) where T : unmanaged where T1 : unmanaged { for (int i = n; --i >= 0;) Op.LetCast(out r[i], p[i]); }
        #endregion

        #region aggregated to double
        public static double DMaxAbs<T>(T* p, int n) where T : unmanaged => DMaxAbsSq(p, n).Sqrt();
        public static double DMaxAbsSq<T>(T* p, int n) where T : unmanaged { var a = 0.0; for (int i = 0; i < n; i++) a.LetMax(Op.DAbsSq(p[i])); return a; }
        public static double DMaxAbsSub<T>(T* p, T* q, int n) where T : unmanaged => DMaxAbsSqSub(p, q, n).Sqrt();
        public static double DMaxAbsSqSub<T>(T* p, T* q, int n) where T : unmanaged { if (p == q) return 0; var a = 0.0; for (int i = 0; i < n; i++) a.LetMax(Op.DAbsSqSub(p[i], q[i])); return a; }
        public static double DNorm<T>(T* p, double nu, int n) where T : unmanaged { nu /= 2; var a = new SumPair<double>(); for (int i = 0; i < n; i++) a.Add(Op.DAbsSq(p[i]).Pow(nu)); return a.Sum().Pow(0.5 / nu); }
        public static double DNorm1<T>(T* p, int n) where T : unmanaged { var a = new SumPair<double>(); for (int i = 0; i < n; i++) a.Add(Op.DAbs(p[i])); return a.Sum(); }
        public static double DNorm2<T>(T* p, int n) where T : unmanaged => DNorm2Sq(p, n).Sqrt();
        public static double DNorm2Sq<T>(T* p, int n) where T : unmanaged { var a = new SumPair<double>(); for (int i = 0; i < n; i++) a.Add(Op.DAbsSq(p[i])); return a.Sum(); }
        public static double DNormSub<T>(T* p, T* q, double nu, int n) where T : unmanaged { if (p == q) return 0; nu /= 2; var a = new SumPair<double>(); for (int i = 0; i < n; i++) a.Add(Op.DAbsSqSub(p[i], q[i]).Pow(nu)); return a.Sum().Pow(0.5 / nu); }
        public static double DNorm1Sub<T>(T* p, T* q, int n) where T : unmanaged { if (p == q) return 0; var a = new SumPair<double>(); for (int i = 0; i < n; i++) a.Add(Op.DAbs(Op.Sub(p[i], q[i]))); return a.Sum(); }
        public static double DNorm2Sub<T>(T* p, T* q, int n) where T : unmanaged => DNorm2SqSub(p, q, n).Sqrt();
        public static double DNorm2SqSub<T>(T* p, T* q, int n) where T : unmanaged { if (p == q) return 0; var a = new SumPair<double>(); for (int i = 0; i < n; i++) a.Add(Op.DAbsSqSub(p[i], q[i])); return a.Sum(); }
        public static double DRelativeError<T>(T* p, T* q, int n) where T : unmanaged { if (p == q) return 0; SumPair<double> a = new SumPair<double>(), b = new SumPair<double>(), c = new SumPair<double>(); for (int i = 0; i < n; i++) { a.Add(Op.DAbsSqSub(p[i], q[i])); b.Add(Op.DAbsSq(p[i])); c.Add(Op.DAbsSq(q[i])); } return 2 * a.Sum().Sqrt() / (b.Sum().Sqrt() + c.Sum().Sqrt() + Mt.DoubleEps); }
        #endregion

        #region type-generic
        public static T Min<T>(T* p, int n) where T : unmanaged { var a = Op<T>.PositiveInfinity; for (int i = 0; i < n; i++) Op.LetMin(ref a, p[i]); return a; }
        public static T Max<T>(T* p, int n) where T : unmanaged { var a = Op<T>.NegativeInfinity; for (int i = 0; i < n; i++) Op.LetMax(ref a, p[i]); return a; }
        public static T MaxAbs<T>(T* p, int n) where T : unmanaged { var a = Op<T>.Zero; for (int i = 0; i < n; i++) Op.LetMax(ref a, Op.Abs(p[i])); return a; }
        public static T MaxAbsSq<T>(T* p, int n) where T : unmanaged { var a = Op<T>.Zero; for (int i = 0; i < n; i++) Op.LetMax(ref a, Op.AbsSq(p[i])); return a; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static R SumHalf<T, R>(T* p, int n) where T : unmanaged where R : unmanaged
        {
            if (n > 4)
            {
                var a0 = SumHalf<T, R>(p, n / 2);
                var a1 = SumHalf<T, R>(p + n / 2, n - n / 2);
                Op.LetAdd(ref a0, a1);
                return a0;
            }
            if (n <= 0) return default;
            var d0 = Op<R>.From(p[0]);
            if (n == 1) return d0;
            { var d1 = Op<R>.From(p[1]); Op.LetAdd(ref d0, d1); }
            if (n == 2) return d0;
            var d2 = Op<R>.From(p[2]);
            if (n == 4) { var d3 = Op<R>.From(p[3]); Op.LetAdd(ref d2, d3); }
            return Op.Add(d0, d2);
        }
        public static T SumPairOverwrite<T>(T* p, int n) where T : unmanaged
        {
            if (n <= 0) return default;
            while (true)
            {
                if (n == 1) return p[0];
                if (n < 16 && n != 8) return SumPairOverwrite2(p, n);
                var m = n / 8; if ((n & 7) != 0) m--;
                for (int i = 0; i < m; i++) Op.SumPair8(out p[i], &p[i * 8]);
                if ((n & 7) != 0) p[m] = SumPairOverwrite2(&p[m * 8], n - m * 8);
                n /= 8;
            }
        }
        internal static T SumPairOverwrite2<T>(T* p, int n) where T : unmanaged
        {
            while (true)
            {
                if (n == 1) return p[0];
                var m = n / 2;
                for (int i = 0; i < m; i++) Op.Add(out p[i], p[i * 2], p[i * 2 + 1]);
                if ((n & 1) != 0) Op.LetAdd(ref p[m - 1], p[n - 1]);
                n /= 2;
            }
        }
        public static R SumFwrd<T, R>(T* p, int n) where T : unmanaged where R : unmanaged { var a = new SumFwrd<R>(); for (int i = 0; i < n; i++) a.Add(p[i]); return a.Sum(); }
        public static R AvgFwrd<T, R>(T* p, int n) where T : unmanaged where R : unmanaged => Op.Div(SumFwrd<T, R>(p, n), n);
        public static R SumPair<T, R>(T* p, int n) where T : unmanaged where R : unmanaged { var a = new SumPair<R>(); for (int i = 0; i < n; i++) a.Add(p[i]); return a.Sum(); }
        public static R AvgPair<T, R>(T* p, int n) where T : unmanaged where R : unmanaged => Op.Div(SumPair<T, R>(p, n), n);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        internal static T TypeConv<T>(T* p, int n, Mt.UFunc<T, double> fD, Mt.UFunc<T, ComplexD> fCD, Mt.UFunc<T, T> f) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)(float)fD(p, n);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)(ComplexS)fCD(p, n);
            return f(p, n);
        }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)] public static T SumFwrd<T>(T* p, int n) where T : unmanaged => TypeConv(p, n, SumFwrd<T, double>, SumFwrd<T, ComplexD>, SumFwrd<T, T>);
        [MethodImpl(MethodImplOptions.AggressiveOptimization)] public static T AvgFwrd<T>(T* p, int n) where T : unmanaged => TypeConv(p, n, AvgFwrd<T, double>, AvgFwrd<T, ComplexD>, AvgFwrd<T, T>);
        [MethodImpl(MethodImplOptions.AggressiveOptimization)] public static T SumPair<T>(T* p, int n) where T : unmanaged => TypeConv(p, n, SumPair<T, double>, SumPair<T, ComplexD>, SumPair<T, T>);
        [MethodImpl(MethodImplOptions.AggressiveOptimization)] public static T AvgPair<T>(T* p, int n) where T : unmanaged => TypeConv(p, n, AvgPair<T, double>, AvgPair<T, ComplexD>, AvgPair<T, T>);

        public static void LetPos<T>(T* r, int n) where T : unmanaged => Pos(r, r, n);
        public static void LetNeg<T>(T* r, int n) where T : unmanaged => Neg(r, r, n);
        public static void LetTld<T>(T* r, int n) where T : unmanaged => Tld(r, r, n);
        public static void LetInv<T>(T* r, int n) where T : unmanaged => Inv(r, r, n);
        public static void LetCnj<T>(T* r, int n) where T : unmanaged => Cnj(r, r, n);
        public static void LetSq<T>(T* r, int n) where T : unmanaged => Sq(r, r, n);
        public static void LetAbsSq<T>(T* r, int n) where T : unmanaged => AbsSq(r, r, n);
        public static void LetAbs<T>(T* r, int n) where T : unmanaged => Abs(r, r, n);
        public static void LetSqrt<T>(T* r, int n) where T : unmanaged => Sqrt(r, r, n);
        public static void LetSign<T>(T* r, int n) where T : unmanaged => Sign(r, r, n);
        public static void LetPow<T>(T* r, double nu, int n) where T : unmanaged => Pow(r, r, nu, n);

        public static void LetAdd<T>(T* r, T* q, int n) where T : unmanaged => Add(r, r, q, n);
        public static void LetSub<T>(T* r, T* q, int n) where T : unmanaged => Sub(r, r, q, n);
        public static void LetMul<T>(T* r, T* q, int n) where T : unmanaged => Mul(r, r, q, n);
        public static void LetDiv<T>(T* r, T* q, int n) where T : unmanaged => Div(r, r, q, n);
        public static void LetMod<T>(T* r, T* q, int n) where T : unmanaged => Mod(r, r, q, n);
        public static void LetCul<T>(T* r, T* q, int n) where T : unmanaged => Cul(r, r, q, n);
        public static void LetSubr<T>(T* r, T* q, int n) where T : unmanaged => Subr(r, r, q, n);
        public static void LetDivr<T>(T* r, T* q, int n) where T : unmanaged => Divr(r, r, q, n);
        public static void LetModr<T>(T* r, T* q, int n) where T : unmanaged => Modr(r, r, q, n);
        public static void LetCulr<T>(T* r, T* q, int n) where T : unmanaged => Culr(r, r, q, n);

        public static void LetAdd<T>(T* r, T q, int n) where T : unmanaged => Add(r, r, q, n);
        public static void LetSub<T>(T* r, T q, int n) where T : unmanaged => Sub(r, r, q, n);
        public static void LetMul<T>(T* r, T q, int n) where T : unmanaged => Mul(r, r, q, n);
        public static void LetDiv<T>(T* r, T q, int n) where T : unmanaged => Div(r, r, q, n);
        public static void LetMod<T>(T* r, T q, int n) where T : unmanaged => Mod(r, r, q, n);
        public static void LetCul<T>(T* r, T q, int n) where T : unmanaged => Cul(r, r, q, n);
        public static void LetSubr<T>(T* r, T q, int n) where T : unmanaged => Subr(r, r, q, n);
        public static void LetDivr<T>(T* r, T q, int n) where T : unmanaged => Divr(r, r, q, n);
        public static void LetModr<T>(T* r, T q, int n) where T : unmanaged => Modr(r, r, q, n);
        public static void LetCulr<T>(T* r, T q, int n) where T : unmanaged => Culr(r, r, q, n);
        public static void LetDiv<T>(T* r, long q, int n) where T : unmanaged => Div(r, r, q, n);
        public static void LetDiv<T>(T* r, double q, int n) where T : unmanaged => Div(r, r, q, n);

        public static void Pos<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Pos(out r[i], p[i]); }
        public static void Neg<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Neg(out r[i], p[i]); }
        public static void Tld<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Tld(out r[i], p[i]); }
        public static void Inv<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Inv(out r[i], p[i]); }
        public static void Cnj<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Cnj(out r[i], p[i]); }
        public static void Sq<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = Op.Sq(p[i]); }
        public static void AbsSq<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = Op.AbsSq(p[i]); }
        public static void Abs<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = Op.Abs(p[i]); }
        public static void Sqrt<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = Op.Sqrt(p[i]); }
        public static void Sign<T>(T* r, T* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = Op.Sign(p[i]); }
        public static void Pow<T>(T* r, T* p, double nu, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = Op.Pow(p[i], nu); }

        [MethodImpl(MethodImplOptions.NoInlining)] public static void LetAddType<T, S>(T* r, S* q, int n) where T : unmanaged where S : unmanaged { for (int i = n; --i >= 0;) Op.LetAdd(ref r[i], Op<T>.From(q[i])); }
        [MethodImpl(MethodImplOptions.NoInlining)] public static void Add<T>(T* r, T* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Add(out r[i], p[i], q[i]); }
        [MethodImpl(MethodImplOptions.NoInlining)] public static void Sub<T>(T* r, T* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Sub(out r[i], p[i], q[i]); }
        [MethodImpl(MethodImplOptions.NoInlining)] public static void Mul<T>(T* r, T* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Mul(out r[i], p[i], q[i]); }
        [MethodImpl(MethodImplOptions.NoInlining)] public static void Div<T>(T* r, T* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Div(out r[i], p[i], q[i]); }
        [MethodImpl(MethodImplOptions.NoInlining)] public static void Mod<T>(T* r, T* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Mod(out r[i], p[i], q[i]); }
        [MethodImpl(MethodImplOptions.NoInlining)] public static void Cul<T>(T* r, T* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Cul(out r[i], p[i], q[i]); }
        public static void Subr<T>(T* r, T* p, T* q, int n) where T : unmanaged => Sub(r, q, p, n);
        public static void Divr<T>(T* r, T* p, T* q, int n) where T : unmanaged => Div(r, q, p, n);
        public static void Modr<T>(T* r, T* p, T* q, int n) where T : unmanaged => Mod(r, q, p, n);
        public static void Culr<T>(T* r, T* p, T* q, int n) where T : unmanaged => Cul(r, q, p, n);

        public static void Add<T>(T* r, T* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Let(r, p, n); else for (int i = n; --i >= 0;) Op.Add(out r[i], p[i], q); }
        public static void Sub<T>(T* r, T* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Let(r, p, n); else for (int i = n; --i >= 0;) Op.Sub(out r[i], p[i], q); }
        public static void Mul<T>(T* r, T* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Clear(r, n); else if (Op.Equ(q, 1)) Let(r, p, n); else if (Op.Equ(q, -1)) Neg(r, p, n); else for (int i = n; --i >= 0;) Op.Mul(out r[i], p[i], q); }
        public static void Div<T>(T* r, T* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 1)) Let(r, p, n); else if (Op.Equ(q, -1)) Neg(r, p, n); else for (int i = n; --i >= 0;) Op.Div(out r[i], p[i], q); }
        public static void Mod<T>(T* r, T* p, T q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Mod(out r[i], p[i], q); }
        public static void Cul<T>(T* r, T* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Clear(r, n); else if (Op.Equ(q, 1)) Cnj(r, p, n); else for (int i = n; --i >= 0;) Op.Cul(out r[i], p[i], q); }
        public static void Subr<T>(T* r, T* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Neg(r, p, n); else for (int i = n; --i >= 0;) Op.Sub(out r[i], q, p[i]); }
        public static void Divr<T>(T* r, T* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 1)) Inv(r, p, n); else for (int i = n; --i >= 0;) Op.Div(out r[i], q, p[i]); }
        public static void Modr<T>(T* r, T* p, T q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.Mod(out r[i], q, p[i]); }
        public static void Culr<T>(T* r, T* p, T q, int n) where T : unmanaged => Mul(r, p, Op.Cnj(q), n);
        public static void Div<T>(T* r, T* p, long q, int n) where T : unmanaged { if (q == 1) Let(r, p, n); else if (q == -1) Neg(r, p, n); else for (int i = n; --i >= 0;) Op.Div(out r[i], p[i], q); }
        public static void Div<T>(T* r, T* p, double q, int n) where T : unmanaged { if (q== 1) Let(r, p, n); else if (q== -1) Neg(r, p, n); else for (int i = n; --i >= 0;) Op.Div(out r[i], p[i], q); }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void LetMulAdd<T>(T* r, T p, T* q, int n) where T : unmanaged
        {
            if (Op.Equ(p, 0)) { Let(r, q, n); return; }
            if (Op.Equ(p, 1)) { LetAdd(r, q, n); return; }
            if (Op.Equ(p, -1)) { LetSubr(r, q, n); return; }
            for (int i = n; --i >= 0;) Op.LetMulAdd(ref r[i], p, q[i]);
        }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void LetAddMul<T>(T* r, T* p, T q, int n) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) { LetAddMul((float*)r, (float*)p, (float)(object)q, n); return; }
            if (typeof(T) == typeof(double)) { LetAddMul((double*)r, (double*)p, (double)(object)q, n); return; }
            for (int i = n; --i >= 0;) Op.LetAddMul(ref r[i], p[i], q);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T SumCul<T>(T* p, T* q, int n) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)(float)SumCul<T, double>(p, q, n);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)(ComplexS)SumCul<T, ComplexD>(p, q, n);
            return SumCul<T, T>(p, q, n);
        }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T SumMul<T>(T* p, T* q, int n) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)(float)SumMul<T, double>(p, q, n);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)(ComplexS)SumMul<T, ComplexD>(p, q, n);
            return SumMul<T, T>(p, q, n);
        }
        public static R SumCul<T, R>(T* p, T* q, int n) where T : unmanaged where R : unmanaged { var a = new SumPair<R>(); for (int i = 0; i < n; i++) a.Add(Op.Cul(Op<R>.From(p[i]), Op<R>.From(q[i]))); return a.Sum(); }
        public static R SumMul<T, R>(T* p, T* q, int n) where T : unmanaged where R : unmanaged { var a = new SumPair<R>(); for (int i = 0; i < n; i++) a.Add(Op.Mul(Op<R>.From(p[i]), Op<R>.From(q[i]))); return a.Sum(); }
        public static R SumMul<T, R>(SumPairX<R> a, T* p, T* q, int n) where T : unmanaged where R : unmanaged
        {
            a.Reset();
            for (int i = 0; i < n; i++)
                a.Add(Op.Mul(Op<R>.From(p[i]), Op<R>.From(q[i])));
            return a.Sum();
        }
        #endregion

        #region float
        public static double DSum(float* p, int n) => SumPair<float, double>(p, n);
        public static double DAvg(float* p, int n) => AvgPair<float, double>(p, n);
        public static double DSumCul(float* p, float* q, int n) => SumCul<float, double>(p, q, n);
        public static double DSumMul(float* p, float* q, int n) => SumMul<float, double>(p, q, n);
        public static ComplexD DSum(ComplexS* p, int n) => SumPair<ComplexS, ComplexD>(p, n);
        public static ComplexD DAvg(ComplexS* p, int n) => AvgPair<ComplexS, ComplexD>(p, n);
        public static ComplexD DSumCul(ComplexS* p, ComplexS* q, int n) => SumCul<ComplexS, ComplexD>(p, q, n);
        public static ComplexD DSumMul(ComplexS* p, ComplexS* q, int n) => SumMul<ComplexS, ComplexD>(p, q, n);
        public static void LetAddMul(float* r, float* p, float q, int n)
        {
            int m = n & ~7;
            for (int i = n; --i >= m;)
                r[i] += p[i] * q;
            for (int i = m; (i -= 8) >= 0;)
            {
                *(r + i + 7) += *(p + i + 7) * q;
                *(r + i + 6) += *(p + i + 6) * q;
                *(r + i + 5) += *(p + i + 5) * q;
                *(r + i + 4) += *(p + i + 4) * q;
                *(r + i + 3) += *(p + i + 3) * q;
                *(r + i + 2) += *(p + i + 2) * q;
                *(r + i + 1) += *(p + i + 1) * q;
                *(r + i + 0) += *(p + i + 0) * q;
            }
        }
        #endregion

        #region double
        public static void LetAddMul(double* r, double* p, double q, int n)
        {
            int m = n & ~3;
            for (int i = n; --i >= m;)
                r[i] += p[i] * q;
            for (int i = m; (i -= 4) >= 0;)
            {
                *(r + i + 3) += *(p + i + 3) * q;
                *(r + i + 2) += *(p + i + 2) * q;
                *(r + i + 1) += *(p + i + 1) * q;
                *(r + i + 0) += *(p + i + 0) * q;
            }
        }
        #endregion

        #region Complex<T>
        public static T MaxAbs<T>(Complex<T>* p, int n) where T : unmanaged => Op.Sqrt(MaxAbsSq(p, n));
        public static T MaxAbsSq<T>(Complex<T>* p, int n) where T : unmanaged { var a = Op<T>.Zero; for (int i = n; --i >= 0;) Op.LetMax(ref a, p[i].AbsSq()); return a; }
        public static double DNorm1<T>(Complex<T>* p, int n) where T : unmanaged => DNorm1((T*)p, n * 2);
        public static double DNorm2<T>(Complex<T>* p, int n) where T : unmanaged => DNorm2Sq(p, n).Sqrt();
        public static double DNorm2Sq<T>(Complex<T>* p, int n) where T : unmanaged => DNorm2Sq((T*)p, n * 2);
        public static double DNorm1Sub<T>(Complex<T>* p, Complex<T>* q, int n) where T : unmanaged => DNorm1Sub((T*)p, (T*)q, n * 2);
        public static double DNorm2Sub<T>(Complex<T>* p, Complex<T>* q, int n) where T : unmanaged => DNorm2SqSub(p, q, n).Sqrt();
        public static double DNorm2SqSub<T>(Complex<T>* p, Complex<T>* q, int n) where T : unmanaged => DNorm2SqSub((T*)p, (T*)q, n * 2);
        public static double DRelativeError<T>(Complex<T>* p, Complex<T>* q, int n) where T : unmanaged => DRelativeError((T*)p, (T*)q, n * 2);
        public static Complex<T> SumMul<T>(Complex<T>* p, T* q, int n) where T : unmanaged { var a = new SumPair<ComplexD>(); for (int i = 0; i < n; i++) a.Add(Op.CastComplexD(p[i]) * Op.CastComplexD(q[i])); return Op<Complex<T>>.From(a.Sum()); }
        public static Complex<T> SumCul<T>(Complex<T>* p, T* q, int n) where T : unmanaged => ~SumMul(p, q, n);

        public static void Let<T>(Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) { p[i].Im = default; p[i].Re = q[i]; } }
        public static void Real<T>(T* r, Complex<T>* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = p[i].Re; }
        public static void Imag<T>(T* r, Complex<T>* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = p[i].Im; }
        public static void AbsSq<T>(T* r, Complex<T>* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = p[i].AbsSq(); }
        public static void Abs<T>(T* r, Complex<T>* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = p[i].Abs(); }
        public static void Arg<T>(T* r, Complex<T>* p, int n) where T : unmanaged { for (int i = n; --i >= 0;) r[i] = Op.Atan2(p[i].Im, p[i].Re); }
        public static void ToComplex<T>(Complex<T>* r, T* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) { r[i].Im = q[i]; r[i].Re = p[i]; } }

        public static void AddB<T>(Complex<T>* r, Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.AddB(out r[i], p[i], q[i]); }
        public static void SubB<T>(Complex<T>* r, Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.SubB(out r[i], p[i], q[i]); }
        public static void MulB<T>(Complex<T>* r, Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.MulB(out r[i], p[i], q[i]); }
        public static void DivB<T>(Complex<T>* r, Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.DivB(out r[i], p[i], q[i]); }
        public static void CulB<T>(Complex<T>* r, Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.CulB(out r[i], p[i], q[i]); }
        public static void SubrB<T>(Complex<T>* r, Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.SubrB(out r[i], p[i], q[i]); }
        public static void DivrB<T>(Complex<T>* r, Complex<T>* p, T* q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.DivrB(out r[i], p[i], q[i]); }
        public static void AddB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Let(r, p, n); else for (int i = n; --i >= 0;) Op.AddB(out r[i], p[i], q); }
        public static void SubB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Let(r, p, n); else for (int i = n; --i >= 0;) Op.SubB(out r[i], p[i], q); }
        public static void MulB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged => Mul((T*)r, (T*)p, q, n * 2);
        public static void DivB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged => Mul((T*)r, (T*)p, Op.Inv(q), n * 2);
        public static void CulB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 1)) Cnj(r, p, n); else for (int i = n; --i >= 0;) Op.CulB(out r[i], p[i], q); }
        public static void SubrB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 0)) Neg(r, p, n); else for (int i = n; --i >= 0;) Op.SubrB(out r[i], p[i], q); }
        public static void DivrB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged { if (Op.Equ(q, 1)) Inv(r, p, n); else for (int i = n; --i >= 0;) Op.DivrB(out r[i], p[i], q); }

        public static void LetAddMulB<T>(Complex<T>* r, Complex<T>* p, T q, int n) where T : unmanaged => LetAddMul((T*)r, (T*)p, q, n * 2);
        public static void LetAddMulB<T>(Complex<T>* r, T* p, Complex<T> q, int n) where T : unmanaged { for (int i = n; --i >= 0;) Op.LetAddMulB(ref r[i], q, p[i]); }
        #endregion
    }

    public static unsafe partial class Mt
    {
        #region utility, common
        internal delegate TR UFunc<T0, TR>(T0* p, int n) where T0 : unmanaged;
        delegate TR UFunc<T0, T1, TR>(T0* p, T1* q, int n) where T0 : unmanaged where T1 : unmanaged;
        delegate TR UFunc_<T0, T1, TR>(T0* p, T1 q, int n) where T0 : unmanaged where T1 : unmanaged;
        delegate TR UFunc<T0, T1, T2, TR>(T0* p, T1* q, T2* r, int n) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged;
        delegate TR UFunc_<T0, T1, T2, TR>(T0* p, T1* q, T2 r, int n) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged;
        delegate void UAction<T>(T* p, int n) where T : unmanaged;
        delegate void UAction<T0, T1>(T0* p, T1* q, int n) where T0 : unmanaged where T1 : unmanaged;
        delegate void UAction_<T0, T1>(T0* p, T1 q, int n) where T0 : unmanaged where T1 : unmanaged;
        delegate void UAction<T0, T1, T2>(T0* p, T1* q, T2* r, int n) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged;
        delegate void UAction_<T0, T1, T2>(T0* p, T1* q, T2 r, int n) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged;
        #endregion

        #region Span<T>
        static R FT1<T, R>(Span<T> x, UFunc<T, R> f) where T : unmanaged { fixed (T* p = x) return f(p, x.Length); }
        static R FT2<T, T1, R>(Span<T> x, Span<T1> y, UFunc<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, Ex.SameLength(x, y)); }
        static R FT2<T, T1, R>(Span<T> x, T1 y, UFunc_<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) return f(p, y, x.Length); }
        //static R FT3<T, T1, T2, R>(Span<T> x, Span<T1> y, Span<T2> z, UFunc<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) return f(p, q, r, Ex.SameLength(x, y, z)); }
        static R FT3<T, T1, T2, R>(Span<T> x, Span<T1> y, T2 z, UFunc_<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, z, Ex.SameLength(x, y)); }
        static Span<T> FL1<T>(Span<T> x, UAction<T> f) where T : unmanaged { fixed (T* p = x) f(p, x.Length); return x; }
        static Span<T> FL2<T, T1>(Span<T> x, Span<T1> y, UAction<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, Ex.SameLength(x, y)); return x; }
        static Span<T> FL2<T, T1>(Span<T> x, Span<T1> y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, p, q, Ex.SameLength(x, y)); return x; }
        static Span<T> FL2<T, T1>(Span<T> x, T1 y, UAction_<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, y, x.Length); return x; }
        static Span<T> FL2<T, T1>(Span<T> x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, p, y, x.Length); return x; }
        //static Span<T> FL3<T, T1, T2>(Span<T> x, Span<T1> y, Span<T2> z, UAction<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) f(p, q, r, Ex.SameLength(x, y, z)); return x; }
        static Span<T> FL3<T, T1, T2>(Span<T> x, Span<T1> y, T2 z, UAction_<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, z, Ex.SameLength(x, y)); return x; }
        static R[] FB1<T, R>(Span<T> x, UAction<R, T> f) where T : unmanaged where R : unmanaged { var a = x.To0<T, R>(); fixed (R* p = a) fixed (T* q = x) f(p, q, a.Length); return a; }//=> FL2(x.To0<T, R>(), x, f);
        static R[] FB2<T, T1, R>(Span<T> x, Span<T1> y, UAction<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged { var a = x.To0<T, R>(); fixed (R* p = a) fixed (T* q = x) fixed (T1* r = y) f(p, q, r, Ex.SameLength(x, y)); return a; }//=> FL3(x.To0<T, R>(), x, y, f);
        static R[] FB2<T, T1, R>(Span<T> x, T1 y, UAction_<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged { var a = x.To0<T, R>(); fixed (R* p = a) fixed (T* q = x) f(p, q, y, a.Length); return a; } //=> FL3(x.To0<T, R>(), x, y, f);
        static T[] FA1<T>(Span<T> x, UAction<T, T> f) where T : unmanaged => FB1(x, f);
        static T[] FA2<T, T1>(Span<T> x, Span<T1> y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);
        static T[] FA2<T, T1>(Span<T> x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);

        public static Span<T> Clear<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.Clear);
        public static Span<T> Let<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.Let);
        public static Span<T> Let<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.Let);
        public static Span<T> LetRev<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetRev);
        public static Span<T> LetMask<T>(this Span<T> x, Span<bool> y) where T : unmanaged => FL2(x, y, Us.LetMask);
        public static Span<T> LetMaskNot<T>(this Span<T> x, Span<bool> y) where T : unmanaged => FL2(x, y, Us.LetMaskNot);
        public static Span<T> Rev<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Rev);
        public static Span<T> Mask<T>(this Span<T> x, Span<bool> y) where T : unmanaged => FA2(x, y, Us.Mask);
        public static Span<T> MaskNot<T>(this Span<T> x, Span<bool> y) where T : unmanaged => FA2(x, y, Us.MaskNot);

        public static T Min<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.Min);
        public static T Max<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.Max);
        public static T MaxAbs<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this Span<T> x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(Span<T> x, Span<T> y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(Span<T> x, Span<T> y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(Span<T> x, Span<T> y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(Span<T> x, Span<T> y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this Span<T> x, Span<T> y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static double DInner<T>(this Span<T> x, Span<T> y) where T : unmanaged => FT2(x, y, Us.SumCul<T, double>);
        public static R Sum<T, R>(this Span<T> x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumFwrd<T, R>);
        public static R Avg<T, R>(this Span<T> x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgFwrd<T, R>);
        public static R SumPair<T, R>(this Span<T> x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumPair<T, R>);
        public static R AvgPair<T, R>(this Span<T> x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgPair<T, R>);
        public static R Inner<T, R>(this Span<T> x, Span<T> y) where T : unmanaged where R : unmanaged => FT2(x, y, Us.SumCul<T, R>);
        public static T Sum<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.SumFwrd);
        public static T Avg<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.AvgFwrd);
        public static T SumPair<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.SumPair);
        public static T AvgPair<T>(this Span<T> x) where T : unmanaged => FT1(x, Us.AvgPair);
        public static T Inner<T>(this Span<T> x, Span<T> y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static Span<T> LetNeg<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetNeg);
        public static Span<T> LetTld<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetTld);
        public static Span<T> LetInv<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetInv);
        public static Span<T> LetCnj<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetCnj);
        public static Span<T> LetSq<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetSq);
        public static Span<T> LetAbsSq<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetAbsSq);
        public static Span<T> LetAbs<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetAbs);
        public static Span<T> LetSqrt<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetSqrt);
        public static Span<T> LetSign<T>(this Span<T> x) where T : unmanaged => FL1(x, Us.LetSign);
        public static Span<T> LetPow<T>(this Span<T> x, double nu) where T : unmanaged => FL2(x, nu, Us.LetPow);

        public static Span<T> LetAdd<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static Span<T> LetSub<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static Span<T> LetMul<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static Span<T> LetDiv<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static Span<T> LetMod<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static Span<T> LetCul<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static Span<T> LetSubr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static Span<T> LetDivr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static Span<T> LetModr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static Span<T> LetCulr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FL2(x, y, Us.LetCulr);

        public static Span<T> LetAdd<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static Span<T> LetSub<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static Span<T> LetMul<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static Span<T> LetDiv<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static Span<T> LetMod<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static Span<T> LetCul<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static Span<T> LetSubr<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static Span<T> LetDivr<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static Span<T> LetModr<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static Span<T> LetCulr<T>(this Span<T> x, T y) where T : unmanaged => FL2(x, y, Us.LetCulr);
        public static Span<T> LetDiv<T>(this Span<T> x, long y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static Span<T> LetDiv<T>(this Span<T> x, double y) where T : unmanaged => FL2(x, y, Us.LetDiv);

        public static Span<T> Pos<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Pos);
        public static Span<T> Neg<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Neg);
        public static Span<T> Tld<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Tld);
        public static Span<T> Inv<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Inv);
        public static Span<T> Cnj<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Cnj);
        public static Span<T> Sq<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Sq);
        public static Span<T> AbsSq<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.AbsSq);
        public static Span<T> Abs<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Abs);
        public static Span<T> Sqrt<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Sqrt);
        public static Span<T> Sign<T>(this Span<T> x) where T : unmanaged => FA1(x, Us.Sign);
        public static Span<T> Pow<T>(this Span<T> x, double nu) where T : unmanaged => FA2(x, nu, Us.Pow);

        public static Span<T> Add<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Add);
        public static Span<T> Sub<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static Span<T> Mul<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static Span<T> Div<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Div);
        public static Span<T> Mod<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static Span<T> Cul<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static Span<T> Subr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static Span<T> Divr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static Span<T> Modr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static Span<T> Culr<T>(this Span<T> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static Span<T> Add<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Add);
        public static Span<T> Sub<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static Span<T> Mul<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static Span<T> Div<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Div);
        public static Span<T> Mod<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static Span<T> Cul<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static Span<T> Subr<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static Span<T> Divr<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static Span<T> Modr<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static Span<T> Culr<T>(this Span<T> x, T y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static Span<T> LetAddMul<T>(this Span<T> x, Span<T> y, T z) where T : unmanaged => FL3(x, y, z, Us.LetAddMul);
        public static T[] AddMul<T>(this Span<T> x, Span<T> y, T z) where T : unmanaged { var a = x.ToArray(); LetAddMul(a, y, z); return a; }//=> FL3(x.ToArray(), y, z, Us.LetAddMul);
        public static Span<T> LetNormalizeSum<T>(this Span<T> x) where T : unmanaged => x.LetDiv(x.Sum());
        public static Span<T> LetNormalizeNorm1<T>(this Span<T> x) where T : unmanaged => x.LetDiv(x.DNorm1());
        public static Span<T> LetNormalizeNorm2<T>(this Span<T> x) where T : unmanaged => x.LetDiv(x.DNorm2());
        public static Span<T> LetNormalizeMax<T>(this Span<T> x) where T : unmanaged => x.LetDiv(x.Max());
        #endregion

        #region type-generic[]
        static R FT1<T, R>(T[] x, UFunc<T, R> f) where T : unmanaged { fixed (T* p = x) return f(p, x.Length); }
        static R FT2<T, T1, R>(T[] x, T1[] y, UFunc<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, Ex.SameLength(x, y)); }
        static R FT2<T, T1, R>(T[] x, T1 y, UFunc_<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) return f(p, y, x.Length); }
        //static R FT3<T, T1, T2, R>(T[] x, T1[] y, T2[] z, UFunc<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) return f(p, q, r, Ex.SameLength(x, y, z)); }
        static R FT3<T, T1, T2, R>(T[] x, T1[] y, T2 z, UFunc_<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, z, Ex.SameLength(x, y)); }
        static T[] FL1<T>(T[] x, UAction<T> f) where T : unmanaged { fixed (T* p = x) f(p, x.Length); return x; }
        static T[] FL2<T, T1>(T[] x, T1[] y, UAction<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, Ex.SameLength(x, y)); return x; }
        static T[] FL2<T, T1>(T[] x, T1[] y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, p, q, Ex.SameLength(x, y)); return x; }
        static T[] FL2<T, T1>(T[] x, T1 y, UAction_<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, y, x.Length); return x; }
        static T[] FL2<T, T1>(T[] x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, p, y, x.Length); return x; }
        static T[] FL3<T, T1, T2>(T[] x, T1[] y, T2[] z, UAction<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) f(p, q, r, Ex.SameLength(x, y, z)); return x; }
        static T[] FL3<T, T1, T2>(T[] x, T1[] y, T2 z, UAction_<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, z, Ex.SameLength(x, y)); return x; }
        static R[] FB1<T, R>(T[] x, UAction<R, T> f) where T : unmanaged where R : unmanaged => FL2(x.To0<T, R>(), x, f);
        static R[] FB2<T, T1, R>(T[] x, T1[] y, UAction<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged => FL3(x.To0<T, R>(), x, y, f);
        static R[] FB2<T, T1, R>(T[] x, T1 y, UAction_<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged => FL3(x.To0<T, R>(), x, y, f);
        static T[] FA1<T>(T[] x, UAction<T, T> f) where T : unmanaged => FB1(x, f);
        static T[] FA2<T, T1>(T[] x, T1[] y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);
        static T[] FA2<T, T1>(T[] x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);

        public static T[] Clear<T>(this T[] x) where T : unmanaged => FL1(x, Us.Clear);
        public static T[] Let<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.Let);
        public static T[] Let<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.Let);
        public static T[] LetRev<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetRev);
        public static T[] LetMask<T>(this T[] x, bool[] y) where T : unmanaged => FL2(x, y, Us.LetMask);
        public static T[] LetMaskNot<T>(this T[] x, bool[] y) where T : unmanaged => FL2(x, y, Us.LetMaskNot);
        public static T[] LetCast<T, T1>(this T[] x, T1[] y) where T : unmanaged where T1 : unmanaged => FL2(x, y, Us.Cast);
        public static T[] Rev<T>(this T[] x) where T : unmanaged => FA1(x, Us.Rev);
        public static T[] Mask<T>(this T[] x, bool[] y) where T : unmanaged => FA2(x, y, Us.Mask);
        public static T[] MaskNot<T>(this T[] x, bool[] y) where T : unmanaged => FA2(x, y, Us.MaskNot);
        public static R[] Cast<T, R>(this T[] x) where T : unmanaged where R : unmanaged => FB1<T, R>(x, Us.Cast);

        public static T Min<T>(this T[] x) where T : unmanaged => FT1(x, Us.Min);
        public static T Max<T>(this T[] x) where T : unmanaged => FT1(x, Us.Max);
        public static T MaxAbs<T>(this T[] x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this T[] x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this T[] x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this T[] x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this T[] x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this T[] x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(T[] x, T[] y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(T[] x, T[] y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(T[] x, T[] y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(T[] x, T[] y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this T[] x, T[] y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static double DInner<T>(this T[] x, T[] y) where T : unmanaged => FT2(x, y, Us.SumCul<T, double>);
        public static R Sum<T, R>(this T[] x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumFwrd<T, R>);
        public static R Avg<T, R>(this T[] x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgFwrd<T, R>);
        public static R SumPair<T, R>(this T[] x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumPair<T, R>);
        public static R AvgPair<T, R>(this T[] x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgPair<T, R>);
        public static R Inner<T, R>(this T[] x, T[] y) where T : unmanaged where R : unmanaged => FT2(x, y, Us.SumCul<T, R>);
        public static T Sum<T>(this T[] x) where T : unmanaged => FT1(x, Us.SumFwrd);
        public static T Avg<T>(this T[] x) where T : unmanaged => FT1(x, Us.AvgFwrd);
        public static T SumPair<T>(this T[] x) where T : unmanaged => FT1(x, Us.SumPair);
        public static T AvgPair<T>(this T[] x) where T : unmanaged => FT1(x, Us.AvgPair);
        public static T Inner<T>(this T[] x, T[] y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static T[] LetNeg<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetNeg);
        public static T[] LetTld<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetTld);
        public static T[] LetInv<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetInv);
        public static T[] LetCnj<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetCnj);
        public static T[] LetSq<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetSq);
        public static T[] LetAbsSq<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetAbsSq);
        public static T[] LetAbs<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetAbs);
        public static T[] LetSqrt<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetSqrt);
        public static T[] LetSign<T>(this T[] x) where T : unmanaged => FL1(x, Us.LetSign);
        public static T[] LetPow<T>(this T[] x, double nu) where T : unmanaged => FL2(x, nu, Us.LetPow);

        public static T[] LetAdd<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static T[] LetSub<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static T[] LetMul<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static T[] LetDiv<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[] LetMod<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static T[] LetCul<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static T[] LetSubr<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static T[] LetDivr<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static T[] LetModr<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static T[] LetCulr<T>(this T[] x, T[] y) where T : unmanaged => FL2(x, y, Us.LetCulr);

        public static T[] LetAdd<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static T[] LetSub<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static T[] LetMul<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static T[] LetDiv<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[] LetMod<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static T[] LetCul<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static T[] LetSubr<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static T[] LetDivr<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static T[] LetModr<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static T[] LetCulr<T>(this T[] x, T y) where T : unmanaged => FL2(x, y, Us.LetCulr);
        public static T[] LetDiv<T>(this T[] x, long y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[] LetDiv<T>(this T[] x, double y) where T : unmanaged => FL2(x, y, Us.LetDiv);

        public static T[] Pos<T>(this T[] x) where T : unmanaged => FA1(x, Us.Pos);
        public static T[] Neg<T>(this T[] x) where T : unmanaged => FA1(x, Us.Neg);
        public static T[] Tld<T>(this T[] x) where T : unmanaged => FA1(x, Us.Tld);
        public static T[] Inv<T>(this T[] x) where T : unmanaged => FA1(x, Us.Inv);
        public static T[] Cnj<T>(this T[] x) where T : unmanaged => FA1(x, Us.Cnj);
        public static T[] Sq<T>(this T[] x) where T : unmanaged => FA1(x, Us.Sq);
        public static T[] AbsSq<T>(this T[] x) where T : unmanaged => FA1(x, Us.AbsSq);
        public static T[] Abs<T>(this T[] x) where T : unmanaged => FA1(x, Us.Abs);
        public static T[] Sqrt<T>(this T[] x) where T : unmanaged => FA1(x, Us.Sqrt);
        public static T[] Sign<T>(this T[] x) where T : unmanaged => FA1(x, Us.Sign);
        public static T[] Pow<T>(this T[] x, double nu) where T : unmanaged => FA2(x, nu, Us.Pow);

        public static T[] Add<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Add);
        public static T[] Sub<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static T[] Mul<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static T[] Div<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Div);
        public static T[] Mod<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static T[] Cul<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static T[] Subr<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static T[] Divr<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static T[] Modr<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static T[] Culr<T>(this T[] x, T[] y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static T[] Add<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Add);
        public static T[] Sub<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static T[] Mul<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static T[] Div<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Div);
        public static T[] Mod<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static T[] Cul<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static T[] Subr<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static T[] Divr<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static T[] Modr<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static T[] Culr<T>(this T[] x, T y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static T[] LetAddMul<T>(this T[] x, T[] y, T z) where T : unmanaged => FL3(x, y, z, Us.LetAddMul);
        public static T[] AddMul<T>(this T[] x, T[] y, T z) where T : unmanaged => FL3(x.CloneX(), y, z, Us.LetAddMul);
        public static T[] LetNormalizeSum<T>(this T[] x) where T : unmanaged => x.LetDiv(x.Sum());
        public static T[] LetNormalizeNorm1<T>(this T[] x) where T : unmanaged => x.LetDiv(x.DNorm1());
        public static T[] LetNormalizeNorm2<T>(this T[] x) where T : unmanaged => x.LetDiv(x.DNorm2());
        public static T[] LetNormalizeMax<T>(this T[] x) where T : unmanaged => x.LetDiv(x.Max());
        #endregion

        #region type-generic[,]
        static R FT1<T, R>(T[,] x, UFunc<T, R> f) where T : unmanaged { fixed (T* p = x) return f(p, x.Length); }
        static R FT2<T, T1, R>(T[,] x, T1[,] y, UFunc<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, Ex.SameLength(x, y)); }
        static R FT2<T, T1, R>(T[,] x, T1 y, UFunc_<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) return f(p, y, x.Length); }
        //static R FT3<T, T1, T2, R>(T[,] x, T1[,] y, T2[,] z, UFunc<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) return f(p, q, r, Ex.SameLength(x, y, z)); }
        static R FT3<T, T1, T2, R>(T[,] x, T1[,] y, T2 z, UFunc_<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, z, Ex.SameLength(x, y)); }
        static T[,] FL1<T>(T[,] x, UAction<T> f) where T : unmanaged { fixed (T* p = x) f(p, x.Length); return x; }
        static T[,] FL2<T, T1>(T[,] x, T1[,] y, UAction<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, Ex.SameLength(x, y)); return x; }
        static T[,] FL2<T, T1>(T[,] x, T1[,] y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, p, q, Ex.SameLength(x, y)); return x; }
        static T[,] FL2<T, T1>(T[,] x, T1 y, UAction_<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, y, x.Length); return x; }
        static T[,] FL2<T, T1>(T[,] x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, p, y, x.Length); return x; }
        static T[,] FL3<T, T1, T2>(T[,] x, T1[,] y, T2[,] z, UAction<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) f(p, q, r, Ex.SameLength(x, y, z)); return x; }
        static T[,] FL3<T, T1, T2>(T[,] x, T1[,] y, T2 z, UAction_<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, z, Ex.SameLength(x, y)); return x; }
        static R[,] FB1<T, R>(T[,] x, UAction<R, T> f) where T : unmanaged where R : unmanaged => FL2(x.To0<T, R>(), x, f);
        static R[,] FB2<T, T1, R>(T[,] x, T1[,] y, UAction<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged => FL3(x.To0<T, R>(), x, y, f);
        static R[,] FB2<T, T1, R>(T[,] x, T1 y, UAction_<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged => FL3(x.To0<T, R>(), x, y, f);
        static T[,] FA1<T>(T[,] x, UAction<T, T> f) where T : unmanaged => FB1(x, f);
        static T[,] FA2<T, T1>(T[,] x, T1[,] y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);
        static T[,] FA2<T, T1>(T[,] x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);

        public static T[,] Clear<T>(this T[,] x) where T : unmanaged => FL1(x, Us.Clear);
        public static T[,] Let<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.Let);
        public static T[,] Let<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.Let);
        public static T[,] LetRev<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetRev);
        public static T[,] LetMask<T>(this T[,] x, bool[,] y) where T : unmanaged => FL2(x, y, Us.LetMask);
        public static T[,] LetMaskNot<T>(this T[,] x, bool[,] y) where T : unmanaged => FL2(x, y, Us.LetMaskNot);
        public static T[,] LetCast<T, T1>(this T[,] x, T1[,] y) where T : unmanaged where T1 : unmanaged => FL2(x, y, Us.Cast);
        public static T[,] Rev<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Rev);
        public static T[,] Mask<T>(this T[,] x, bool[,] y) where T : unmanaged => FA2(x, y, Us.Mask);
        public static T[,] MaskNot<T>(this T[,] x, bool[,] y) where T : unmanaged => FA2(x, y, Us.MaskNot);
        public static R[,] Cast<T, R>(this T[,] x) where T : unmanaged where R : unmanaged => FB1<T, R>(x, Us.Cast);

        public static T Min<T>(this T[,] x) where T : unmanaged => FT1(x, Us.Min);
        public static T Max<T>(this T[,] x) where T : unmanaged => FT1(x, Us.Max);
        public static T MaxAbs<T>(this T[,] x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this T[,] x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this T[,] x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this T[,] x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this T[,] x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this T[,] x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(T[,] x, T[,] y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(T[,] x, T[,] y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(T[,] x, T[,] y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(T[,] x, T[,] y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this T[,] x, T[,] y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static double DInner<T>(this T[,] x, T[,] y) where T : unmanaged => FT2(x, y, Us.SumCul<T, double>);
        public static R Sum<T, R>(this T[,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumFwrd<T, R>);
        public static R Avg<T, R>(this T[,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgFwrd<T, R>);
        public static R SumPair<T, R>(this T[,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumPair<T, R>);
        public static R AvgPair<T, R>(this T[,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgPair<T, R>);
        public static R Inner<T, R>(this T[,] x, T[,] y) where T : unmanaged where R : unmanaged => FT2(x, y, Us.SumCul<T, R>);
        public static T Sum<T>(this T[,] x) where T : unmanaged => FT1(x, Us.SumFwrd);
        public static T Avg<T>(this T[,] x) where T : unmanaged => FT1(x, Us.AvgFwrd);
        public static T SumPair<T>(this T[,] x) where T : unmanaged => FT1(x, Us.SumPair);
        public static T AvgPair<T>(this T[,] x) where T : unmanaged => FT1(x, Us.AvgPair);
        public static T Inner<T>(this T[,] x, T[,] y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static T[,] LetNeg<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetNeg);
        public static T[,] LetTld<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetTld);
        public static T[,] LetInv<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetInv);
        public static T[,] LetCnj<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetCnj);
        public static T[,] LetSq<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetSq);
        public static T[,] LetAbsSq<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetAbsSq);
        public static T[,] LetAbs<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetAbs);
        public static T[,] LetSqrt<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetSqrt);
        public static T[,] LetSign<T>(this T[,] x) where T : unmanaged => FL1(x, Us.LetSign);
        public static T[,] LetPow<T>(this T[,] x, double nu) where T : unmanaged => FL2(x, nu, Us.LetPow);

        public static T[,] LetAdd<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static T[,] LetSub<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static T[,] LetMul<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static T[,] LetDiv<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[,] LetMod<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static T[,] LetCul<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static T[,] LetSubr<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static T[,] LetDivr<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static T[,] LetModr<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static T[,] LetCulr<T>(this T[,] x, T[,] y) where T : unmanaged => FL2(x, y, Us.LetCulr);

        public static T[,] LetAdd<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static T[,] LetSub<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static T[,] LetMul<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static T[,] LetDiv<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[,] LetMod<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static T[,] LetCul<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static T[,] LetSubr<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static T[,] LetDivr<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static T[,] LetModr<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static T[,] LetCulr<T>(this T[,] x, T y) where T : unmanaged => FL2(x, y, Us.LetCulr);
        public static T[,] LetDiv<T>(this T[,] x, long y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[,] LetDiv<T>(this T[,] x, double y) where T : unmanaged => FL2(x, y, Us.LetDiv);

        public static T[,] Pos<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Pos);
        public static T[,] Neg<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Neg);
        public static T[,] Tld<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Tld);
        public static T[,] Inv<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Inv);
        public static T[,] Cnj<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Cnj);
        public static T[,] Sq<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Sq);
        public static T[,] AbsSq<T>(this T[,] x) where T : unmanaged => FA1(x, Us.AbsSq);
        public static T[,] Abs<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Abs);
        public static T[,] Sqrt<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Sqrt);
        public static T[,] Sign<T>(this T[,] x) where T : unmanaged => FA1(x, Us.Sign);
        public static T[,] Pow<T>(this T[,] x, double nu) where T : unmanaged => FA2(x, nu, Us.Pow);

        public static T[,] Add<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Add);
        public static T[,] Sub<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static T[,] Mul<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static T[,] Div<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Div);
        public static T[,] Mod<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static T[,] Cul<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static T[,] Subr<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static T[,] Divr<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static T[,] Modr<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static T[,] Culr<T>(this T[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static T[,] Add<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Add);
        public static T[,] Sub<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static T[,] Mul<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static T[,] Div<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Div);
        public static T[,] Mod<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static T[,] Cul<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static T[,] Subr<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static T[,] Divr<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static T[,] Modr<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static T[,] Culr<T>(this T[,] x, T y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static T[,] LetAddMul<T>(this T[,] x, T[,] y, T z) where T : unmanaged => FL3(x, y, z, Us.LetAddMul);
        public static T[,] AddMul<T>(this T[,] x, T[,] y, T z) where T : unmanaged => FL3(x.CloneX(), y, z, Us.LetAddMul);
        public static T[,] LetNormalizeSum<T>(this T[,] x) where T : unmanaged => x.LetDiv(x.Sum());
        public static T[,] LetNormalizeNorm1<T>(this T[,] x) where T : unmanaged => x.LetDiv(x.DNorm1());
        public static T[,] LetNormalizeNorm2<T>(this T[,] x) where T : unmanaged => x.LetDiv(x.DNorm2());
        public static T[,] LetNormalizeMax<T>(this T[,] x) where T : unmanaged => x.LetDiv(x.Max());
        #endregion

        #region type-generic[,,]
        static R FT1<T, R>(T[,,] x, UFunc<T, R> f) where T : unmanaged { fixed (T* p = x) return f(p, x.Length); }
        static R FT2<T, T1, R>(T[,,] x, T1[,,] y, UFunc<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, Ex.SameLength(x, y)); }
        static R FT2<T, T1, R>(T[,,] x, T1 y, UFunc_<T, T1, R> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) return f(p, y, x.Length); }
        //static R FT3<T, T1, T2, R>(T[,,] x, T1[,,] y, T2[,,] z, UFunc<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) return f(p, q, r, Ex.SameLength(x, y, z)); }
        static R FT3<T, T1, T2, R>(T[,,] x, T1[,,] y, T2 z, UFunc_<T, T1, T2, R> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) return f(p, q, z, Ex.SameLength(x, y)); }
        static T[,,] FL1<T>(T[,,] x, UAction<T> f) where T : unmanaged { fixed (T* p = x) f(p, x.Length); return x; }
        static T[,,] FL2<T, T1>(T[,,] x, T1[,,] y, UAction<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, Ex.SameLength(x, y)); return x; }
        static T[,,] FL2<T, T1>(T[,,] x, T1[,,] y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, p, q, Ex.SameLength(x, y)); return x; }
        static T[,,] FL2<T, T1>(T[,,] x, T1 y, UAction_<T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, y, x.Length); return x; }
        static T[,,] FL2<T, T1>(T[,,] x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged { fixed (T* p = x) f(p, p, y, x.Length); return x; }
        static T[,,] FL3<T, T1, T2>(T[,,] x, T1[,,] y, T2[,,] z, UAction<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) fixed (T2* r = z) f(p, q, r, Ex.SameLength(x, y, z)); return x; }
        static T[,,] FL3<T, T1, T2>(T[,,] x, T1[,,] y, T2 z, UAction_<T, T1, T2> f) where T : unmanaged where T1 : unmanaged where T2 : unmanaged { fixed (T* p = x) fixed (T1* q = y) f(p, q, z, Ex.SameLength(x, y)); return x; }
        static R[,,] FB1<T, R>(T[,,] x, UAction<R, T> f) where T : unmanaged where R : unmanaged => FL2(x.To0<T, R>(), x, f);
        static R[,,] FB2<T, T1, R>(T[,,] x, T1[,,] y, UAction<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged => FL3(x.To0<T, R>(), x, y, f);
        static R[,,] FB2<T, T1, R>(T[,,] x, T1 y, UAction_<R, T, T1> f) where T : unmanaged where T1 : unmanaged where R : unmanaged => FL3(x.To0<T, R>(), x, y, f);
        static T[,,] FA1<T>(T[,,] x, UAction<T, T> f) where T : unmanaged => FB1(x, f);
        static T[,,] FA2<T, T1>(T[,,] x, T1[,,] y, UAction<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);
        static T[,,] FA2<T, T1>(T[,,] x, T1 y, UAction_<T, T, T1> f) where T : unmanaged where T1 : unmanaged => FB2(x, y, f);

        public static T[,,] Clear<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.Clear);
        public static T[,,] Let<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.Let);
        public static T[,,] Let<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.Let);
        public static T[,,] LetRev<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetRev);
        public static T[,,] LetMask<T>(this T[,,] x, bool[,,] y) where T : unmanaged => FL2(x, y, Us.LetMask);
        public static T[,,] LetMaskNot<T>(this T[,,] x, bool[,,] y) where T : unmanaged => FL2(x, y, Us.LetMaskNot);
        public static T[,,] LetCast<T, T1>(this T[,,] x, T1[,,] y) where T : unmanaged where T1 : unmanaged => FL2(x, y, Us.Cast);
        public static T[,,] Rev<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Rev);
        public static T[,,] Mask<T>(this T[,,] x, bool[,,] y) where T : unmanaged => FA2(x, y, Us.Mask);
        public static T[,,] MaskNot<T>(this T[,,] x, bool[,,] y) where T : unmanaged => FA2(x, y, Us.MaskNot);
        public static R[,,] Cast<T, R>(this T[,,] x) where T : unmanaged where R : unmanaged => FB1<T, R>(x, Us.Cast);

        public static T Min<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.Min);
        public static T Max<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.Max);
        public static T MaxAbs<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this T[,,] x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(T[,,] x, T[,,] y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(T[,,] x, T[,,] y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(T[,,] x, T[,,] y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(T[,,] x, T[,,] y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this T[,,] x, T[,,] y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static double DInner<T>(this T[,,] x, T[,,] y) where T : unmanaged => FT2(x, y, Us.SumCul<T, double>);
        public static R Sum<T, R>(this T[,,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumFwrd<T, R>);
        public static R Avg<T, R>(this T[,,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgFwrd<T, R>);
        public static R SumPair<T, R>(this T[,,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.SumPair<T, R>);
        public static R AvgPair<T, R>(this T[,,] x) where T : unmanaged where R : unmanaged => FT1(x, Us.AvgPair<T, R>);
        public static R Inner<T, R>(this T[,,] x, T[,,] y) where T : unmanaged where R : unmanaged => FT2(x, y, Us.SumCul<T, R>);
        public static T Sum<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.SumFwrd);
        public static T Avg<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.AvgFwrd);
        public static T SumPair<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.SumPair);
        public static T AvgPair<T>(this T[,,] x) where T : unmanaged => FT1(x, Us.AvgPair);
        public static T Inner<T>(this T[,,] x, T[,,] y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static T[,,] LetNeg<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetNeg);
        public static T[,,] LetTld<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetTld);
        public static T[,,] LetInv<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetInv);
        public static T[,,] LetCnj<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetCnj);
        public static T[,,] LetSq<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetSq);
        public static T[,,] LetAbsSq<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetAbsSq);
        public static T[,,] LetAbs<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetAbs);
        public static T[,,] LetSqrt<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetSqrt);
        public static T[,,] LetSign<T>(this T[,,] x) where T : unmanaged => FL1(x, Us.LetSign);
        public static T[,,] LetPow<T>(this T[,,] x, double nu) where T : unmanaged => FL2(x, nu, Us.LetPow);

        public static T[,,] LetAdd<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static T[,,] LetSub<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static T[,,] LetMul<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static T[,,] LetDiv<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[,,] LetMod<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static T[,,] LetCul<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static T[,,] LetSubr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static T[,,] LetDivr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static T[,,] LetModr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static T[,,] LetCulr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FL2(x, y, Us.LetCulr);

        public static T[,,] LetAdd<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetAdd);
        public static T[,,] LetSub<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetSub);
        public static T[,,] LetMul<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetMul);
        public static T[,,] LetDiv<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[,,] LetMod<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetMod);
        public static T[,,] LetCul<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetCul);
        public static T[,,] LetSubr<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetSubr);
        public static T[,,] LetDivr<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetDivr);
        public static T[,,] LetModr<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetModr);
        public static T[,,] LetCulr<T>(this T[,,] x, T y) where T : unmanaged => FL2(x, y, Us.LetCulr);
        public static T[,,] LetDiv<T>(this T[,,] x, long y) where T : unmanaged => FL2(x, y, Us.LetDiv);
        public static T[,,] LetDiv<T>(this T[,,] x, double y) where T : unmanaged => FL2(x, y, Us.LetDiv);

        public static T[,,] Pos<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Pos);
        public static T[,,] Neg<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Neg);
        public static T[,,] Tld<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Tld);
        public static T[,,] Inv<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Inv);
        public static T[,,] Cnj<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Cnj);
        public static T[,,] Sq<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Sq);
        public static T[,,] AbsSq<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.AbsSq);
        public static T[,,] Abs<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Abs);
        public static T[,,] Sqrt<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Sqrt);
        public static T[,,] Sign<T>(this T[,,] x) where T : unmanaged => FA1(x, Us.Sign);
        public static T[,,] Pow<T>(this T[,,] x, double nu) where T : unmanaged => FA2(x, nu, Us.Pow);

        public static T[,,] Add<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Add);
        public static T[,,] Sub<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static T[,,] Mul<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static T[,,] Div<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Div);
        public static T[,,] Mod<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static T[,,] Cul<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static T[,,] Subr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static T[,,] Divr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static T[,,] Modr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static T[,,] Culr<T>(this T[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static T[,,] Add<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Add);
        public static T[,,] Sub<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Sub);
        public static T[,,] Mul<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Mul);
        public static T[,,] Div<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Div);
        public static T[,,] Mod<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Mod);
        public static T[,,] Cul<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Cul);
        public static T[,,] Subr<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Subr);
        public static T[,,] Divr<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Divr);
        public static T[,,] Modr<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Modr);
        public static T[,,] Culr<T>(this T[,,] x, T y) where T : unmanaged => FA2(x, y, Us.Culr);

        public static T[,,] LetAddMul<T>(this T[,,] x, T[,,] y, T z) where T : unmanaged => FL3(x, y, z, Us.LetAddMul);
        public static T[,,] AddMul<T>(this T[,,] x, T[,,] y, T z) where T : unmanaged => FL3(x.CloneX(), y, z, Us.LetAddMul);
        public static T[,,] LetNormalizeSum<T>(this T[,,] x) where T : unmanaged => x.LetDiv(x.Sum());
        public static T[,,] LetNormalizeNorm1<T>(this T[,,] x) where T : unmanaged => x.LetDiv(x.DNorm1());
        public static T[,,] LetNormalizeNorm2<T>(this T[,,] x) where T : unmanaged => x.LetDiv(x.DNorm2());
        public static T[,,] LetNormalizeMax<T>(this T[,,] x) where T : unmanaged => x.LetDiv(x.Max());
        #endregion

        #region Span<Complex<T>>
        public static T MaxAbs<T>(this Span<Complex<T>> x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this Span<Complex<T>> x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this Span<Complex<T>> x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this Span<Complex<T>> x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this Span<Complex<T>> x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this Span<Complex<T>> x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(Span<Complex<T>> x, Span<Complex<T>> y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(Span<Complex<T>> x, Span<Complex<T>> y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(Span<Complex<T>> x, Span<Complex<T>> y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(Span<Complex<T>> x, Span<Complex<T>> y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this Span<Complex<T>> x, Span<Complex<T>> y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static Complex<T> Inner<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static Span<Complex<T>> Let<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.Let);
        public static Span<Complex<T>> ToComplex<T>(Span<T> x, Span<T> y) where T : unmanaged => FB2<T, T, Complex<T>>(x, y, Us.ToComplex<T>);
        public static Span<Complex<T>> ToComplex<T>(this Span<T> x) where T : unmanaged => FB1<T, Complex<T>>(x, Us.Let);
        public static Span<T> Real<T>(this Span<Complex<T>> x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Real);
        public static Span<T> Imag<T>(this Span<Complex<T>> x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Imag);
        public static Span<T> AbsSq<T>(this Span<Complex<T>> x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.AbsSq);
        public static Span<T> Abs<T>(this Span<Complex<T>> x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Abs);
        public static Span<T> Arg<T>(this Span<Complex<T>> x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Arg);

        public static Span<Complex<T>> LetAddB<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Span<Complex<T>> LetSubB<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Span<Complex<T>> LetMulB<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Span<Complex<T>> LetDivB<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Span<Complex<T>> LetCulB<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Span<Complex<T>> LetSubrB<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.SubrB);
        public static Span<Complex<T>> LetDivrB<T>(this Span<Complex<T>> a, Span<T> y) where T : unmanaged => FL2(a, y, Us.DivrB);
        public static Span<Complex<T>> LetAddB<T>(this Span<Complex<T>> a, T y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Span<Complex<T>> LetSubB<T>(this Span<Complex<T>> a, T y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Span<Complex<T>> LetMulB<T>(this Span<Complex<T>> a, T y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Span<Complex<T>> LetDivB<T>(this Span<Complex<T>> a, T y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Span<Complex<T>> LetCulB<T>(this Span<Complex<T>> a, T y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Span<Complex<T>> AddB<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Span<Complex<T>> SubB<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Span<Complex<T>> MulB<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Span<Complex<T>> DivB<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Span<Complex<T>> CulB<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Span<Complex<T>> SubrB<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Span<Complex<T>> DivrB<T>(this Span<Complex<T>> x, Span<T> y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Span<Complex<T>> AddB<T>(this Span<Complex<T>> x, T y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Span<Complex<T>> SubB<T>(this Span<Complex<T>> x, T y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Span<Complex<T>> MulB<T>(this Span<Complex<T>> x, T y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Span<Complex<T>> DivB<T>(this Span<Complex<T>> x, T y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Span<Complex<T>> CulB<T>(this Span<Complex<T>> x, T y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Span<Complex<T>> SubrB<T>(this Span<Complex<T>> x, T y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Span<Complex<T>> DivrB<T>(this Span<Complex<T>> x, T y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Span<Complex<T>> AddB<T>(this Span<T> x, Span<Complex<T>> y) where T : unmanaged => y.AddB(x);
        public static Span<Complex<T>> SubB<T>(this Span<T> x, Span<Complex<T>> y) where T : unmanaged => y.SubrB(x);
        public static Span<Complex<T>> MulB<T>(this Span<T> x, Span<Complex<T>> y) where T : unmanaged => y.MulB(x);
        public static Span<Complex<T>> DivB<T>(this Span<T> x, Span<Complex<T>> y) where T : unmanaged => y.DivrB(x);
        public static Span<Complex<T>> CulrB<T>(this Span<T> x, Span<Complex<T>> y) where T : unmanaged => y.CulB(x);

        public static Span<Complex<T>> LetAddMulB<T>(this Span<Complex<T>> a, Span<Complex<T>> y, T z) where T : unmanaged => FL3(a, y, z, Us.LetAddMulB);
        public static Complex<T>[] AddMulB<T>(this Span<Complex<T>> x, Span<Complex<T>> y, T z) where T : unmanaged { var a = x.ToArray(); a.AsSpan().LetAddMulB(y, z); return a; }
        #endregion

        #region Complex<T>[]
        public static T MaxAbs<T>(this Complex<T>[] x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this Complex<T>[] x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this Complex<T>[] x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this Complex<T>[] x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this Complex<T>[] x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this Complex<T>[] x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(Complex<T>[] x, Complex<T>[] y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(Complex<T>[] x, Complex<T>[] y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(Complex<T>[] x, Complex<T>[] y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(Complex<T>[] x, Complex<T>[] y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this Complex<T>[] x, Complex<T>[] y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static Complex<T> Inner<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static Complex<T>[] Let<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.Let);
        public static Complex<T>[] ToComplex<T>(T[] x, T[] y) where T : unmanaged => FB2<T, T, Complex<T>>(x, y, Us.ToComplex<T>);
        public static Complex<T>[] ToComplex<T>(this T[] x) where T : unmanaged => FB1<T, Complex<T>>(x, Us.Let);
        public static T[] Real<T>(this Complex<T>[] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Real);
        public static T[] Imag<T>(this Complex<T>[] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Imag);
        public static T[] AbsSq<T>(this Complex<T>[] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.AbsSq);
        public static T[] Abs<T>(this Complex<T>[] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Abs);
        public static T[] Arg<T>(this Complex<T>[] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Arg);

        public static Complex<T>[] LetAddB<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Complex<T>[] LetSubB<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Complex<T>[] LetMulB<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Complex<T>[] LetDivB<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Complex<T>[] LetCulB<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Complex<T>[] LetSubrB<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.SubrB);
        public static Complex<T>[] LetDivrB<T>(this Complex<T>[] a, T[] y) where T : unmanaged => FL2(a, y, Us.DivrB);
        public static Complex<T>[] LetAddB<T>(this Complex<T>[] a, T y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Complex<T>[] LetSubB<T>(this Complex<T>[] a, T y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Complex<T>[] LetMulB<T>(this Complex<T>[] a, T y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Complex<T>[] LetDivB<T>(this Complex<T>[] a, T y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Complex<T>[] LetCulB<T>(this Complex<T>[] a, T y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Complex<T>[] AddB<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Complex<T>[] SubB<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Complex<T>[] MulB<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Complex<T>[] DivB<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Complex<T>[] CulB<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Complex<T>[] SubrB<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Complex<T>[] DivrB<T>(this Complex<T>[] x, T[] y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Complex<T>[] AddB<T>(this Complex<T>[] x, T y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Complex<T>[] SubB<T>(this Complex<T>[] x, T y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Complex<T>[] MulB<T>(this Complex<T>[] x, T y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Complex<T>[] DivB<T>(this Complex<T>[] x, T y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Complex<T>[] CulB<T>(this Complex<T>[] x, T y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Complex<T>[] SubrB<T>(this Complex<T>[] x, T y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Complex<T>[] DivrB<T>(this Complex<T>[] x, T y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Complex<T>[] AddB<T>(this T[] x, Complex<T>[] y) where T : unmanaged => y.AddB(x);
        public static Complex<T>[] SubB<T>(this T[] x, Complex<T>[] y) where T : unmanaged => y.SubrB(x);
        public static Complex<T>[] MulB<T>(this T[] x, Complex<T>[] y) where T : unmanaged => y.MulB(x);
        public static Complex<T>[] DivB<T>(this T[] x, Complex<T>[] y) where T : unmanaged => y.DivrB(x);
        public static Complex<T>[] CulrB<T>(this T[] x, Complex<T>[] y) where T : unmanaged => y.CulB(x);

        public static Complex<T>[] LetAddMulB<T>(this Complex<T>[] a, Complex<T>[] y, T z) where T : unmanaged => FL3(a, y, z, Us.LetAddMulB);
        public static Complex<T>[] AddMulB<T>(this Complex<T>[] x, Complex<T>[] y, T z) where T : unmanaged => x.CloneX().LetAddMulB(y, z);
        #endregion

        #region Complex<T>[,]
        public static T MaxAbs<T>(this Complex<T>[,] x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this Complex<T>[,] x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this Complex<T>[,] x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this Complex<T>[,] x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this Complex<T>[,] x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this Complex<T>[,] x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(Complex<T>[,] x, Complex<T>[,] y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(Complex<T>[,] x, Complex<T>[,] y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(Complex<T>[,] x, Complex<T>[,] y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(Complex<T>[,] x, Complex<T>[,] y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this Complex<T>[,] x, Complex<T>[,] y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static Complex<T> Inner<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static Complex<T>[,] Let<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.Let);
        public static Complex<T>[,] ToComplex<T>(T[,] x, T[,] y) where T : unmanaged => FB2<T, T, Complex<T>>(x, y, Us.ToComplex<T>);
        public static Complex<T>[,] ToComplex<T>(this T[,] x) where T : unmanaged => FB1<T, Complex<T>>(x, Us.Let);
        public static T[,] Real<T>(this Complex<T>[,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Real);
        public static T[,] Imag<T>(this Complex<T>[,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Imag);
        public static T[,] AbsSq<T>(this Complex<T>[,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.AbsSq);
        public static T[,] Abs<T>(this Complex<T>[,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Abs);
        public static T[,] Arg<T>(this Complex<T>[,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Arg);

        public static Complex<T>[,] LetAddB<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Complex<T>[,] LetSubB<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Complex<T>[,] LetMulB<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Complex<T>[,] LetDivB<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Complex<T>[,] LetCulB<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Complex<T>[,] LetSubrB<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.SubrB);
        public static Complex<T>[,] LetDivrB<T>(this Complex<T>[,] a, T[,] y) where T : unmanaged => FL2(a, y, Us.DivrB);
        public static Complex<T>[,] LetAddB<T>(this Complex<T>[,] a, T y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Complex<T>[,] LetSubB<T>(this Complex<T>[,] a, T y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Complex<T>[,] LetMulB<T>(this Complex<T>[,] a, T y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Complex<T>[,] LetDivB<T>(this Complex<T>[,] a, T y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Complex<T>[,] LetCulB<T>(this Complex<T>[,] a, T y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Complex<T>[,] AddB<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Complex<T>[,] SubB<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Complex<T>[,] MulB<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Complex<T>[,] DivB<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Complex<T>[,] CulB<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Complex<T>[,] SubrB<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Complex<T>[,] DivrB<T>(this Complex<T>[,] x, T[,] y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Complex<T>[,] AddB<T>(this Complex<T>[,] x, T y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Complex<T>[,] SubB<T>(this Complex<T>[,] x, T y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Complex<T>[,] MulB<T>(this Complex<T>[,] x, T y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Complex<T>[,] DivB<T>(this Complex<T>[,] x, T y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Complex<T>[,] CulB<T>(this Complex<T>[,] x, T y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Complex<T>[,] SubrB<T>(this Complex<T>[,] x, T y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Complex<T>[,] DivrB<T>(this Complex<T>[,] x, T y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Complex<T>[,] AddB<T>(this T[,] x, Complex<T>[,] y) where T : unmanaged => y.AddB(x);
        public static Complex<T>[,] SubB<T>(this T[,] x, Complex<T>[,] y) where T : unmanaged => y.SubrB(x);
        public static Complex<T>[,] MulB<T>(this T[,] x, Complex<T>[,] y) where T : unmanaged => y.MulB(x);
        public static Complex<T>[,] DivB<T>(this T[,] x, Complex<T>[,] y) where T : unmanaged => y.DivrB(x);
        public static Complex<T>[,] CulrB<T>(this T[,] x, Complex<T>[,] y) where T : unmanaged => y.CulB(x);

        public static Complex<T>[,] LetAddMulB<T>(this Complex<T>[,] a, Complex<T>[,] y, T z) where T : unmanaged => FL3(a, y, z, Us.LetAddMulB);
        public static Complex<T>[,] AddMulB<T>(this Complex<T>[,] x, Complex<T>[,] y, T z) where T : unmanaged => x.CloneX().LetAddMulB(y, z);
        #endregion

        #region Complex<T>[,,]
        public static T MaxAbs<T>(this Complex<T>[,,] x) where T : unmanaged => FT1(x, Us.MaxAbs);
        public static T MaxAbsSq<T>(this Complex<T>[,,] x) where T : unmanaged => FT1(x, Us.MaxAbsSq);
        public static double DNorm<T>(this Complex<T>[,,] x, double nu) where T : unmanaged => FT2(x, nu, Us.DNorm);
        public static double DNorm1<T>(this Complex<T>[,,] x) where T : unmanaged => FT1(x, Us.DNorm1);
        public static double DNorm2<T>(this Complex<T>[,,] x) where T : unmanaged => FT1(x, Us.DNorm2);
        public static double DNorm2Sq<T>(this Complex<T>[,,] x) where T : unmanaged => FT1(x, Us.DNorm2Sq);
        public static double DNormSub<T>(Complex<T>[,,] x, Complex<T>[,,] y, double nu) where T : unmanaged => FT3(x, y, nu, Us.DNormSub);
        public static double DNorm1Sub<T>(Complex<T>[,,] x, Complex<T>[,,] y) where T : unmanaged => FT2(x, y, Us.DNorm1Sub);
        public static double DNorm2Sub<T>(Complex<T>[,,] x, Complex<T>[,,] y) where T : unmanaged => FT2(x, y, Us.DNorm2Sub);
        public static double DNorm2SqSub<T>(Complex<T>[,,] x, Complex<T>[,,] y) where T : unmanaged => FT2(x, y, Us.DNorm2SqSub);
        public static double DRelativeError<T>(this Complex<T>[,,] x, Complex<T>[,,] y) where T : unmanaged => FT2(x, y, Us.DRelativeError);
        public static Complex<T> Inner<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FT2(x, y, Us.SumCul);

        public static Complex<T>[,,] Let<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.Let);
        public static Complex<T>[,,] ToComplex<T>(T[,,] x, T[,,] y) where T : unmanaged => FB2<T, T, Complex<T>>(x, y, Us.ToComplex<T>);
        public static Complex<T>[,,] ToComplex<T>(this T[,,] x) where T : unmanaged => FB1<T, Complex<T>>(x, Us.Let);
        public static T[,,] Real<T>(this Complex<T>[,,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Real);
        public static T[,,] Imag<T>(this Complex<T>[,,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Imag);
        public static T[,,] AbsSq<T>(this Complex<T>[,,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.AbsSq);
        public static T[,,] Abs<T>(this Complex<T>[,,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Abs);
        public static T[,,] Arg<T>(this Complex<T>[,,] x) where T : unmanaged => FB1<Complex<T>, T>(x, Us.Arg);

        public static Complex<T>[,,] LetAddB<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Complex<T>[,,] LetSubB<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Complex<T>[,,] LetMulB<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Complex<T>[,,] LetDivB<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Complex<T>[,,] LetCulB<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Complex<T>[,,] LetSubrB<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.SubrB);
        public static Complex<T>[,,] LetDivrB<T>(this Complex<T>[,,] a, T[,,] y) where T : unmanaged => FL2(a, y, Us.DivrB);
        public static Complex<T>[,,] LetAddB<T>(this Complex<T>[,,] a, T y) where T : unmanaged => FL2(a, y, Us.AddB);
        public static Complex<T>[,,] LetSubB<T>(this Complex<T>[,,] a, T y) where T : unmanaged => FL2(a, y, Us.SubB);
        public static Complex<T>[,,] LetMulB<T>(this Complex<T>[,,] a, T y) where T : unmanaged => FL2(a, y, Us.MulB);
        public static Complex<T>[,,] LetDivB<T>(this Complex<T>[,,] a, T y) where T : unmanaged => FL2(a, y, Us.DivB);
        public static Complex<T>[,,] LetCulB<T>(this Complex<T>[,,] a, T y) where T : unmanaged => FL2(a, y, Us.CulB);
        public static Complex<T>[,,] AddB<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Complex<T>[,,] SubB<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Complex<T>[,,] MulB<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Complex<T>[,,] DivB<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Complex<T>[,,] CulB<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Complex<T>[,,] SubrB<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Complex<T>[,,] DivrB<T>(this Complex<T>[,,] x, T[,,] y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Complex<T>[,,] AddB<T>(this Complex<T>[,,] x, T y) where T : unmanaged => FA2(x, y, Us.AddB);
        public static Complex<T>[,,] SubB<T>(this Complex<T>[,,] x, T y) where T : unmanaged => FA2(x, y, Us.SubB);
        public static Complex<T>[,,] MulB<T>(this Complex<T>[,,] x, T y) where T : unmanaged => FA2(x, y, Us.MulB);
        public static Complex<T>[,,] DivB<T>(this Complex<T>[,,] x, T y) where T : unmanaged => FA2(x, y, Us.DivB);
        public static Complex<T>[,,] CulB<T>(this Complex<T>[,,] x, T y) where T : unmanaged => FA2(x, y, Us.CulB);
        public static Complex<T>[,,] SubrB<T>(this Complex<T>[,,] x, T y) where T : unmanaged => FA2(x, y, Us.SubrB);
        public static Complex<T>[,,] DivrB<T>(this Complex<T>[,,] x, T y) where T : unmanaged => FA2(x, y, Us.DivrB);
        public static Complex<T>[,,] AddB<T>(this T[,,] x, Complex<T>[,,] y) where T : unmanaged => y.AddB(x);
        public static Complex<T>[,,] SubB<T>(this T[,,] x, Complex<T>[,,] y) where T : unmanaged => y.SubrB(x);
        public static Complex<T>[,,] MulB<T>(this T[,,] x, Complex<T>[,,] y) where T : unmanaged => y.MulB(x);
        public static Complex<T>[,,] DivB<T>(this T[,,] x, Complex<T>[,,] y) where T : unmanaged => y.DivrB(x);
        public static Complex<T>[,,] CulrB<T>(this T[,,] x, Complex<T>[,,] y) where T : unmanaged => y.CulB(x);

        public static Complex<T>[,,] LetAddMulB<T>(this Complex<T>[,,] a, Complex<T>[,,] y, T z) where T : unmanaged => FL3(a, y, z, Us.LetAddMulB);
        public static Complex<T>[,,] AddMulB<T>(this Complex<T>[,,] x, Complex<T>[,,] y, T z) where T : unmanaged => x.CloneX().LetAddMulB(y, z);
        #endregion

        #region fixed size
        public static float Min(this Single2 x) => Math.Max(x.v0, x.v1);
        public static float Min(this Single3 x) => Math.Max(Math.Max(x.v0, x.v1), x.v2);
        public static double Min(this Double2 x) => Math.Max(x.v0, x.v1);
        public static double Min(this Double3 x) => Math.Max(Math.Max(x.v0, x.v1), x.v2);
        public static float Max(this Single2 x) => Math.Max(x.v0, x.v1);
        public static float Max(this Single3 x) => Math.Max(Math.Max(x.v0, x.v1), x.v2);
        public static double Max(this Double2 x) => Math.Max(x.v0, x.v1);
        public static double Max(this Double3 x) => Math.Max(Math.Max(x.v0, x.v1), x.v2);
        public static int Sum(this Int2 x) => x.v0 + x.v1;
        public static int Sum(this Int3 x) => x.v0 + x.v1 + x.v2;
        public static float Sum(this Single2 x) => x.v0 + x.v1;
        public static float Sum(this Single3 x) => x.v0 + x.v1 + x.v2;
        public static double Sum(this Double2 x) => x.v0 + x.v1;
        public static double Sum(this Double3 x) => x.v0 + x.v1 + x.v2;
        public static int Avg(this Int2 x) => x.Sum() / 2;
        public static int Avg(this Int3 x) => x.Sum() / 3;
        public static float Avg(this Single2 x) => x.Sum() / 2;
        public static float Avg(this Single3 x) => x.Sum() / 3;
        public static double Avg(this Double2 x) => x.Sum() / 2;
        public static double Avg(this Double3 x) => x.Sum() / 3;
        public static int Product(this Int2 x) => x.v0 * x.v1;
        public static int Product(this Int3 x) => x.v0 * x.v1 * x.v2;
        public static float Product(this Single2 x) => x.v0 * x.v1;
        public static float Product(this Single3 x) => x.v0 * x.v1 * x.v2;
        public static double Product(this Double2 x) => x.v0 * x.v1;
        public static double Product(this Double3 x) => x.v0 * x.v1 * x.v2;
        public static float Norm1(this Single2 x) => x.v0.Abs() + x.v1.Abs();
        public static float Norm1(this Single3 x) => x.v0.Abs() + x.v1.Abs() + x.v2.Abs();
        public static double Norm1(this Double2 x) => x.v0.Abs() + x.v1.Abs();
        public static double Norm1(this Double3 x) => x.v0.Abs() + x.v1.Abs() + x.v2.Abs();
        public static float Norm2Sq(this Single2 x) => x.v0.Sq() + x.v1.Sq();
        public static float Norm2Sq(this Single3 x) => x.v0.Sq() + x.v1.Sq() + x.v2.Sq();
        public static double Norm2Sq(this Double2 x) => x.v0.Sq() + x.v1.Sq();
        public static double Norm2Sq(this Double3 x) => x.v0.Sq() + x.v1.Sq() + x.v2.Sq();
        public static float Norm2(this Single2 x) => x.Norm2Sq().Sqrt();
        public static float Norm2(this Single3 x) => x.Norm2Sq().Sqrt();
        public static double Norm2(this Double2 x) => x.Norm2Sq().Sqrt();
        public static double Norm2(this Double3 x) => x.Norm2Sq().Sqrt();
        public static int Inner(this Int2 x, Int2 y) => x.v0 * y.v0 + x.v1 * y.v1;
        public static int Inner(this Int3 x, Int3 y) => x.v0 * y.v0 + x.v1 * y.v1 + x.v2 * y.v2;
        public static float Inner(this Single2 x, Single2 y) => x.v0 * y.v0 + x.v1 * y.v1;
        public static float Inner(this Single3 x, Single3 y) => x.v0 * y.v0 + x.v1 * y.v1 + x.v2 * y.v2;
        public static double Inner(this Double2 x, Double2 y) => x.v0 * y.v0 + x.v1 * y.v1;
        public static double Inner(this Double3 x, Double3 y) => x.v0 * y.v0 + x.v1 * y.v1 + x.v2 * y.v2;
        public static Single3 Outer(Single3 x, Single3 y)
        {
            return new Single3(
                x.v1 * y.v2 - x.v2 * y.v1,
                x.v2 * y.v0 - x.v0 * y.v2,
                x.v0 * y.v1 - x.v1 * y.v0
            );
        }
        public static Double3 Outer(Double3 x, Double3 y)
        {
            return new Double3(
                x.v1 * y.v2 - x.v2 * y.v1,
                x.v2 * y.v0 - x.v0 * y.v2,
                x.v0 * y.v1 - x.v1 * y.v0
            );
        }
        public static Single2 NormalizeMax(this Single2 x) => x / x.Max();
        public static Single3 NormalizeMax(this Single3 x) => x / x.Max();
        public static Double2 NormalizeMax(this Double2 x) => x / x.Max();
        public static Double3 NormalizeMax(this Double3 x) => x / x.Max();
        public static Single2 NormalizeSum(this Single2 x) => x / x.Sum();
        public static Single3 NormalizeSum(this Single3 x) => x / x.Sum();
        public static Double2 NormalizeSum(this Double2 x) => x / x.Sum();
        public static Double3 NormalizeSum(this Double3 x) => x / x.Sum();
        public static Single2 NormalizeNorm1(this Single2 x) => x / Norm1(x);
        public static Single3 NormalizeNorm1(this Single3 x) => x / Norm1(x);
        public static Double2 NormalizeNorm1(this Double2 x) => x / Norm1(x);
        public static Double3 NormalizeNorm1(this Double3 x) => x / Norm1(x);
        public static Single2 NormalizeNorm2(Single2 x) => x / Norm2(x);
        public static Single3 NormalizeNorm2(Single3 x) => x / Norm2(x);
        public static Double2 NormalizeNorm2(this Double2 x) => x / Norm2(x);
        public static Double3 NormalizeNorm2(this Double3 x) => x / Norm2(x);

        public static void LetNormalizeMax(this Single2 x) => x.LetDiv(x.Max());
        public static void LetNormalizeMax(this Single3 x) => x.LetDiv(x.Max());
        public static void LetNormalizeMax(this Double2 x) => x.LetDiv(x.Max());
        public static void LetNormalizeMax(this Double3 x) => x.LetDiv(x.Max());
        public static void LetNormalizeSum(this Single2 x) => x.LetDiv(x.Sum());
        public static void LetNormalizeSum(this Single3 x) => x.LetDiv(x.Sum());
        public static void LetNormalizeSum(this Double2 x) => x.LetDiv(x.Sum());
        public static void LetNormalizeSum(this Double3 x) => x.LetDiv(x.Sum());
        public static void LetNormalizeNorm1(this Single2 x) => x.LetDiv(Norm1(x));
        public static void LetNormalizeNorm1(this Single3 x) => x.LetDiv(Norm1(x));
        public static void LetNormalizeNorm1(this Double2 x) => x.LetDiv(Norm1(x));
        public static void LetNormalizeNorm1(this Double3 x) => x.LetDiv(Norm1(x));
        public static void LetNormalizeNorm2(Single2 x) => x.LetDiv(Norm2(x));
        public static void LetNormalizeNorm2(Single3 x) => x.LetDiv(Norm2(x));
        public static void LetNormalizeNorm2(this Double2 x) => x.LetDiv(Norm2(x));
        public static void LetNormalizeNorm2(this Double3 x) => x.LetDiv(Norm2(x));
        #endregion
    }

    #region Sum
    public struct SumFwrd<T> where T : unmanaged
    {
        T a;
        public long Count { get; private set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add<S>(S value) where S : unmanaged => Add(Op<T>.From(value));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add(T value) { Op.LetAdd(ref a, value); Count++; }
        public T Sum() => a;
        public T Avg() => Op.Div(a, Count);
    }

    public struct SumPairX<T> where T : unmanaged
    {
        const int m = 8, mask1 = m - 1, mask2 = m * 2 - 1;
#pragma warning disable CS0169
#pragma warning disable CA1823
#pragma warning disable IDE0044
#pragma warning disable IDE0051
        T Data, d1, d2, d3, d4, d5, d6, d7, d8, d9, dA, dB, dC, dD, dE, dF;
#pragma warning restore IDE0051
#pragma warning restore IDE0044
#pragma warning restore CA1823
#pragma warning restore CS0169
        public long Count { get; private set; }
        SumPairX<T>[]? Next;
        [MethodImpl(MethodImplOptions.NoInlining)] public void Reset() { Count = 0; if (!(Next is null)) Next[0].Reset(); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add<S>(S value) where S : unmanaged => Add(Op<T>.From(value));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add(T value) { Unsafe.Add(ref Data, (int)Count++ & mask2) = value; if (((int)Count & mask1) == 0 && Count != m) Proc(); }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        void Proc()
        {
            Op.SumPair8(out var a, ref Unsafe.Add(ref Data, (int)Count & mask2));
            Next ??= new SumPairX<T>[1];
            Next[0].Add(a);
        }
        public T Sum()
        {
            if (Count < m * 2) return Mt.SumPairOverwrite(MemoryMarshal.CreateSpan(ref Data, (int)Count));
            if (((int)Count & mask2) < m) for (int i = m; --i >= 0;) Ex.Swap(ref Unsafe.Add(ref Data, i), ref Unsafe.Add(ref Data, i + m));
            var n = ((int)Count & mask1) + m;
            var a = Mt.SumPairOverwrite(MemoryMarshal.CreateSpan(ref Data, n));
            Next![0].Add(a);
            return Next![0].Sum();
        }
        public T Avg() => Op.Div(Sum(), Count);
    }

    //public struct SumPairA<T> where T : unmanaged
    //{
    //    readonly T[] Data;
    //    public long Count;
    //    [MethodImpl(MethodImplOptions.NoInlining)] public SumPairA(int length) { Data = new T[length]; Count = 0; }
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add<S>(S value) where S : unmanaged => Add(Op<T>.From(value));
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add(T value) { Data[(int)Count++] = value; }
    //    public T Sum()
    //    {
    //        return Mt.SumPairOverwrite(Data.AsSpan());
    //    }
    //    public T Avg() => Op.Div(Sum(), Count);
    //}

    public class SumPair<T> where T : unmanaged
    {
        const int m = 8, mask1 = m - 1, mask2 = m * 2 - 1;
        readonly T[] Data = new T[m * 2];
        public long Count { get; private set; }
        SumPair<T>? Next;
        [MethodImpl(MethodImplOptions.NoInlining)] public SumPair() { }
        [MethodImpl(MethodImplOptions.NoInlining)] public void Reset() { Count = 0; Next?.Reset(); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add<S>(S value) where S : unmanaged => Add(Op<T>.From(value));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Add(T value) { Data[(int)Count++ & mask2] = value; if (((int)Count & mask1) == 0 && Count != m) Proc(); }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        void Proc()
        {
            Op.SumPair8(out var a, ref Data[(int)Count & mask2]);
            Next ??= new SumPair<T>();
            Next.Add(a);
        }
        public T Sum()
        {
            if (Count < m * 2) return Mt.SumPairOverwrite(Data.AsSpan(0, (int)Count));
            if (((int)Count & mask2) < m) for (int i = m; --i >= 0;) Ex.Swap(ref Data[i], ref Data[i + m]);
            var n = ((int)Count & mask1) + m;
            var a = Mt.SumPairOverwrite(Data.AsSpan(0, n));
            Next!.Add(a);
            return Next!.Sum();
        }
        public T Avg() => Op.Div(Sum(), Count);
    }

    public struct SumFwrdArray<T> where T : unmanaged
    {
        Array<T> Result;
        public long Count { get; private set; }
        public SumFwrdArray(Array<T> init = default) { Result = init; Count = 0; }
        public void Add<S>(Array<S> value) where S : unmanaged
        {
            if (Result.A is null) Result = value.Cast<S, T>();
            else Result.LetAddType(value);
        }
        public Array<T> Sum() => Result;
        public Array<T> Avg() => Sum().LetDiv(Count);
    }

    public class SumPairArray<T> where T : unmanaged
    {
        const int m = 2, mask1 = m - 1;
        Array<T> DataT;
        Array<T> DataR;
        public long Count { get; private set; }
        SumPairArray<T>? Next;
        readonly bool Overwrite;
        public SumPairArray(bool overwrite = false) { this.Overwrite = overwrite; }
        public void Add<S>(S[] value) where S : unmanaged => Add((Array<S>)value);
        public void Add<S>(S[,] value) where S : unmanaged => Add((Array<S>)value);
        public void Add<S>(S[,,] value) where S : unmanaged => Add((Array<S>)value);
        public void Add<S>(Array<S> value) where S : unmanaged
        {
            if (((int)Count++ & mask1) == 0) { DataT = Cast<S>(value.A); return; }
            DataT.LetAddType(value);
            if (Count != m)
            {
                Next ??= new SumPairArray<T>(true);
                Next.Add(DataR);
            }
            DataR = DataT; DataT = default;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Array<T> Cast<S>(Array x) where S : unmanaged
        {
            if (typeof(T) == typeof(S) && Overwrite) return x.As<T>();
            return x.Cast<S, T>();
        }
        public Array<T> Sum()
        {
            var n = Count < m * 2 ? (int)Count : ((int)Count & mask1) + m;
            var a = n switch { 1 => DataT, 2 => DataR, 3 => DataR.LetAddType(DataT), _ => default };
            if (Next is null) return a;
            if (n != 0) Next.Add(a);
            return Next.Sum();
        }
        public Array<T> Avg() => Sum().LetDiv(Count);
    }

    public class SumFwrdArrayIndexed<T, R> where T : unmanaged where R : unmanaged
    {
        readonly Dictionary<long, Array<T>> Data = new Dictionary<long, Array<T>>();
        Array<R> Result;
        readonly bool Overwrite;
        public long Count { get; private set; }
        public SumFwrdArrayIndexed(bool overwrite = false)
        {
            Overwrite = overwrite;
        }
        public void Add(Array<T> item, int k)
        {
            if (k == 0) { Result = Cast(item); Count++; }
            else Data[k] = item;
            while (Data.TryGetValue(Count, out var value))
            {
                Result.LetAddType(value);
                Data.Remove(Count++);
            }
        }
        Array<R> Cast(Array<T> x)
        {
            if (typeof(R) == typeof(T) && Overwrite) return x.As<R>();
            return x.Cast<T, R>();
        }
        public Array<R> Sum() => Result;
        public Array<R> Avg() => Sum().LetDiv(Count);
    }
    public class SumPairArrayIndexed<T, R> where T : unmanaged where R : unmanaged
    {
        readonly Array?[][] Data;
        readonly int Length;
        readonly bool Overwrite;
        public SumPairArrayIndexed(int length, bool overwrite = false)
        {
            Length = length;
            Overwrite = overwrite;
            var list = new List<Array?[]>();
            for (int i = length; i > 0; i /= 2) list.Add(new Array?[(i + 1) / 2]);
            Data = list.ToArray();
        }
        public void Add(Array<T> item, int k)
        {
            Array e = item.A;
            for (int i = 0, m = Length; ; i++, m /= 2, k /= 2)
            {
                var q = Data[i];
                var j = k / 2;
                if (q[j] is null) { q[j] = e; break; }
                if (i == 0) { e = Cast(e).A; }
                LetAdd(e, q[j]!); q[j] = null;
                if ((m & 1) != 0)
                {
                    if (k == m - 1) { k--; continue; }
                    if (k >= m - 3)
                    {
                        if (q[j + 1] is null) { q[j + 1] = e; break; }
                        LetAdd(e, q[j + 1]!); q[j + 1] = null;
                    }
                }
            }
        }
        Array<R> Cast(Array x)
        {
            if (typeof(R) == typeof(T) && Overwrite) return x.As<R>();
            return x.Cast<T, R>();
        }
        static void LetAdd(Array x, Array y)
        {
            if (x.GetType().GetElementType() != y.GetType().GetElementType()) x.As<R>().LetAddType(y.As<T>()); else x.As<R>().LetAddType(y.As<R>());
        }
        public Array<R> Sum()
        {
            var e = Data[^1][0]!;
            return Length == 1 ? Cast(e) : e.As<R>();
        }
        public Array<R> Avg() => Sum().LetDiv(Length);
    }
    #endregion

    // Mathematical functions
    public static partial class Mt
    {
        #region miscellaneous functions
        public const double SingleEpsilon = 1.1754943508222875079687365372222e-38;  // 2^-126
        public const double SingleEps = 1.1920928955078125e-7;                      // 2^-23
        public const double SingleFpMin = 9.8607613152626475676466070660348e-32;    // 2^-126 / 2^-23 = 2^-103
        public const double DoubleEpsilon = 2.2250738585072013830902327173324e-308; // 2^-1022
        public const double DoubleEps = 2.2204460492503130808472633361816e-16;      // 2^-52
        public const double DoubleFpMin = 1.0020841800044863889980540256751e-292;   // 2^-1022 / 2^-52 = 2^-970
        public const double PI2 = Math.PI * 2;
        public const double PI_2 = Math.PI / 2;
        public const double Ln2 = 0.69314718055994530941723212145818; // Math.Log(2);
        public const double Sqrt2 = 1.4142135623730950488016887242097; // Math.Sqrt(2);
        public const double Sqrt3 = 1.7320508075688772935274463415059; // Math.Sqrt(3);
        public const double Sqrt2inv = 0.70710678118654752440084436210485;  // 1 / Math.Sqrt(2);
        public const double Sqrt2PIinv = 0.39894228040143267793994605993438;  // 1 / Math.Sqrt(2 * Math.PI);

        public static bool IsNaN(this float x) => float.IsNaN(x);
        public static bool IsNaN(this double x) => double.IsNaN(x);
        public static bool IsInf(this float x) => float.IsInfinity(x);
        public static bool IsInf(this double x) => double.IsInfinity(x);
        public static bool IsPosInf(this float x) => float.IsPositiveInfinity(x);
        public static bool IsNegInf(this float x) => float.IsNegativeInfinity(x);
        public static bool IsPosInf(this double x) => double.IsPositiveInfinity(x);
        public static bool IsNegInf(this double x) => double.IsNegativeInfinity(x);
        public static bool IsNanOrInf(this float x) => float.IsNaN(x) || float.IsInfinity(x);
        public static bool IsNanOrInf(this double x) => double.IsNaN(x) || double.IsInfinity(x);
        public static bool ContainsNanOrInf(this IEnumerable<float> source) => source.Any(v => v.IsNanOrInf());
        public static bool ContainsNanOrInf(this IEnumerable<double> source) => source.Any(v => v.IsNanOrInf());
        public static float IfNan(this float x, float y) => float.IsNaN(x) ? y : x;
        public static float IfInf(this float x, float y) => float.IsInfinity(x) ? y : x;
        public static float IfNanOrInf(this float x, float y) => IsNanOrInf(x) ? y : x;
        public static double IfNan(this double x, double y) => double.IsNaN(x) ? y : x;
        public static double IfInf(this double x, double y) => double.IsInfinity(x) ? y : x;
        public static double IfNanOrInf(this double x, double y) => IsNanOrInf(x) ? y : x;
        public static bool IsTooSmall(this float x, float y) => x + y == y;
        public static bool IsTooSmall(this double x, double y) => x + y == y;
        public static double SlightlyInferior(double x) => x - Math.Max(1e-296, Math.Abs(x) * 1e-12);
        public static double SlightlySuperior(double x) => x + Math.Max(1e-296, Math.Abs(x) * 1e-12);
        public static double RelativeError(double x, double y, double eps = DoubleEps) => 2 * Math.Abs(x - y) / (Math.Abs(x) + Math.Abs(y) + eps);

        public static T Min<T>(this T v1, T v2) where T : IComparable<T> => v1.CompareTo(v2) < 0 ? v1 : v2;
        public static T Max<T>(this T v1, T v2) where T : IComparable<T> => v1.CompareTo(v2) > 0 ? v1 : v2;
        public static T Min<T>(T v1, T v2, T v3) where T : IComparable<T> => Min(Min(v1, v2), v3);
        public static T Max<T>(T v1, T v2, T v3) where T : IComparable<T> => Max(Max(v1, v2), v3);
        public static T Min<T>(T v1, T v2, T v3, T v4) where T : IComparable<T> => Min(Min(v1, v2), Min(v3, v4));
        public static T Max<T>(T v1, T v2, T v3, T v4) where T : IComparable<T> => Max(Max(v1, v2), Max(v3, v4));
        public static T MinMax<T>(this T x, T min, T max) where T : IComparable<T> => x.CompareTo(min) < 0 ? min : (x.CompareTo(max) > 0 ? max : x);
        public static void LetMin<T>(this ref T x, T min) where T : struct, IComparable<T> { if (x.CompareTo(min) > 0) x = min; }
        public static void LetMax<T>(this ref T x, T max) where T : struct, IComparable<T> { if (x.CompareTo(max) < 0) x = max; }
        public static void LetMinMax<T>(this ref T x, T min, T max) where T : struct, IComparable<T> { if (x.CompareTo(min) < 0) x = min; else if (x.CompareTo(max) > 0) x = max; }
        public static bool IfLetMin<T>(this ref T x, T min) where T : struct, IComparable<T> { if (x.CompareTo(min) > 0) { x = min; return true; } return false; }
        public static bool IfLetMax<T>(this ref T x, T max) where T : struct, IComparable<T> { if (x.CompareTo(max) < 0) { x = max; return true; } return false; }
        public static void LetOrder<T>(ref T x, ref T y) where T : IComparable<T> { if (x.CompareTo(y) > 0) Ex.Swap(ref x, ref y); }
        public static bool IsInside<T>(this T x, T a, T b) where T : IComparable<T> => a.CompareTo(x) <= 0 && x.CompareTo(b) <= 0;
        public static bool IsInsideC<T>(this T x, T a, T b) where T : IComparable<T> => a.CompareTo(x) <= 0 && x.CompareTo(b) < 0;

        public static int MinMaxC(this int x, int min, int max) => x < min ? min : x >= max ? max - 1 : x;
        public static void LetMinMaxC(this ref int x, int min, int max) { if (x < min) x = min; else if (x >= max) x = max - 1; }
        public static (int div, int rem) DivRem(int x, int y) => (Math.DivRem(x, y, out var rem), rem);
        public static (long div, long rem) DivRem(long x, long y) => (Math.DivRem(x, y, out var rem), rem);
        public static int AlignUp(int x, int size) => (x + (size - 1)) - (x + (size - 1)) % size;
        public static int AlignDown(int x, int size) => x - x % size;
        public static int AlignUpX(int x, int size) => (x + (size - 1)) & (~(size - 1));
        public static int AlignDownX(int x, int size) => x & (~(size - 1));
        public static int Round(this float x) => (int)Math.Round(x);
        public static int Round(this double x) => (int)Math.Round(x);
        public static Int2 Round(this Single2 x) => new Int2(x.v0.Round(), x.v1.Round());
        public static Int3 Round(this Single3 x) => new Int3(x.v0.Round(), x.v1.Round(), x.v2.Round());
        public static Int2 Round(this Double2 x) => new Int2(x.v0.Round(), x.v1.Round());
        public static Int3 Round(this Double3 x) => new Int3(x.v0.Round(), x.v1.Round(), x.v2.Round());

        public static int Div_(int x, int y) => (x - Mod_(x, y)) / y;                                                                          //y>0:下方向商   y<0:上方向商
        public static int Mod_(int x, int y) { var a = x % y; return ((x ^ y) < 0 && a != 0) ? a + y : a; }                                    //y>0:下方向剰余 y<0:上方向剰余
        public static double Div_(double x, double y) => (x - Mod_(x, y)) / y;                                                                 //y>0:下方向商   y<0:上方向商
        public static unsafe double Mod_(double x, double y) { var a = x % y; return ((*(long*)&x ^ *(long*)&y) < 0 && a != 0) ? a + y : a; }  //y>0:下方向剰余 y<0:上方向剰余
        //public static int Div_(int x, int y) => (x - Mod_(x, y)) / y;           //y>0:下方向商   y<0:上方向商
        //public static int Mod_(int x, int y) => ((x % y) + y) % y;              //y>0:下方向剰余 y<0:上方向剰余        
        //public static double Div_(double x, double y) => (x - Mod_(x, y)) / y;  //y>0:下方向商   y<0:上方向商
        //public static double Mod_(double x, double y) => ((x % y) + y) % y;     //y>0:下方向剰余 y<0:上方向剰余        
        //  5/ 3= 1	Div_( 5, 3)= 1
        // -5/ 3=-1	Div_(-5, 3)=-2
        //  5/-3=-1	Div_( 5,-3)=-2
        // -5/-3= 1	Div_(-5,-3)= 1
        //  5% 3= 2	Mod_( 5, 3)= 2
        // -5% 3=-2	Mod_(-5, 3)= 1
        //  5%-3= 2	Mod_( 5,-3)=-1
        // -5%-3=-2	Mod_(-5,-3)=-2
        public static double ModPmHalf(double x)
        {
            x %= 1.0;
            return x <= -0.5 ? x + 1.0 : x > +0.5 ? x - 1.0 : x;
        }
        public static double ModPmPi(double x)
        {
            x %= PI2;
            return x <= -Math.PI ? x + PI2 : x > Math.PI ? x - PI2 : x;
        }

        public static double Log(this double x) => Math.Log(x);
        public static double Exp(this double x) => Math.Exp(x);
        public static double Sqrt(this double x) => Math.Sqrt(x);
        public static double Pow(this double x, double y) => Math.Pow(x, y);
        public static float Log(this float x) => MathF.Log(x);
        public static float Exp(this float x) => MathF.Exp(x);
        public static float Sqrt(this float x) => MathF.Sqrt(x);
        public static float Pow(this float x, float y) => MathF.Pow(x, y);
        public static int Sq(this int x) => x * x;
        public static long Sq(this long x) => x * x;
        public static float Sq(this float x) => x * x;
        public static double Sq(this double x) => x * x;
        public static int Cube(this int x) => x * x * x;
        public static long Cube(this long x) => x * x * x;
        public static float Cube(this float x) => x * x * x;
        public static double Cube(this double x) => x * x * x;
        public static int Abs(this int x) => x < 0 ? -x : x;
        public static long Abs(this long x) => x < 0 ? -x : x;
        public static float Abs(this float x) => x < 0 ? -x : x;
        public static double Abs(this double x) => x < 0 ? -x : x;
        public static BigInteger Abs(this BigInteger x) => BigInteger.Abs(x);
        public static int LetAbs(ref int x) { if (x < 0) x = -x; return x; }
        public static long LetAbs(ref long x) { if (x < 0) x = -x; return x; }
        public static float LetAbs(ref float x) { if (x < 0) x = -x; return x; }
        public static double LetAbs(ref double x) { if (x < 0) x = -x; return x; }
        public static BigInteger LetAbs(ref BigInteger x) { if (x < 0) x = -x; return x; }

        public static int DivCeil(int x, int y) => (x + y - 1) / y;
        public static long DivCeil(long x, int y) => (x + y - 1) / y;
        public static int DivRound(int x, int y) => (x + (x >= 0 ? y / 2 : y / -2)) / y;
        public static long DivRound(long x, int y) => (x + (x >= 0 ? y / 2 : y / -2)) / y;
        public static int Log2Floor(long x) => Log2Floor(x < 0 ? 0 : (ulong)x);
        public static int Log2Floor(ulong x)
        {
            if (x == 0) ThrowException.ArgumentOutOfRange(nameof(x));
            int i = 32;
            for (int j = i; (j >>= 1) > 0;)
                if (x < (1LU << i)) i -= j; else i += j;
            return x < (1LU << i) ? i - 1 : i;
        }
        public static int Log2Ceiling(long x) => Log2Ceiling(x < 0 ? 0 : (ulong)x);
        public static int Log2Ceiling(ulong x)
        {
            if (x == 0) ThrowException.ArgumentOutOfRange(nameof(x));
            if (x == 1) return 0;
            int i = 32;
            for (int j = i; (j >>= 1) > 0;)
                if (x <= (1LU << i)) i -= j; else i += j;
            return x > (1LU << i) ? i + 1 : i;
        }
        public static long AlignPowerOf2Ceiling(long x) => 1L << Log2Ceiling(x);
        public static ulong AlignPowerOf2Ceiling(ulong x) => 1LU << Log2Ceiling(x);
        public static int PowOfNeg1(int x) => (x & 1) == 0 ? +1 : -1;

        public static double XLog(double x) => x == 0 ? 0 : x * Math.Log(x);
        public static double WLog(double weight, double x) => (x == 0 && weight == 0) ? 0 : weight * Math.Log(x);
        public static double Logistic(double x) => 1 / (1 + Math.Exp(-x));
        public static double Sinc(double x) => Math.Abs(x) < DoubleEps ? 1 - x * x / 6 : Math.Sin(x) / x;
        public static double Atanh(double x) => (Math.Abs(x) >= 1 ? (x > 0 ? double.PositiveInfinity : double.NegativeInfinity) : 0.5 * Math.Log((1 + x) / (1 - x)));
        public static double Atanh_(double x) => (Math.Abs(x) >= 1 ? (x > 0 ? 18.7149738751185 : -18.7149738751185) : 0.5 * Math.Log((1 + x) / (1 - x)));
        public static double Tanh(double x)
        {
            if (x == double.PositiveInfinity) return +1;
            if (x == double.NegativeInfinity) return -1;
            double y = Math.Exp(x * 2);
            return (y - 1) / (y + 1);
        }
        public static double Tanh_(double x)
        {
            if (Math.Abs(x) >= 18.7149738751185) return x > 0 ? +1 - 1e-16 : -1 + 1e-16;
            double y = Math.Exp(x + x);
            return (y - 1) / (y + 1);
        }
        public static double Acos_(double x) => x <= -1 ? Math.PI : x >= 1 ? 0.0 : Math.Acos(x);
        public static double Asin_(double x) => x <= -1 ? -0.5 * Math.PI : x >= 1 ? 0.5 * Math.PI : Math.Asin(x);
        public static double Atan1(double y, double x)
        {
            double theta = Math.Atan2(y, x);
            if (theta <= -0.5 * Math.PI) return theta + Math.PI;
            if (theta > 0.5 * Math.PI) return theta - Math.PI;
            return theta;
        }

        public static float Norm2Sq(float x, float y) => x * x + y * y;
        public static double Norm2Sq(double x, double y) => x * x + y * y;
        public static float Norm2_(float x, float y) => (float)Math.Sqrt((double)x * x + (double)y * y);
        public static double Norm2_(double x, double y) => Math.Sqrt(x * x + y * y);
        //Sqrt(x*x + y*y) をoverflow, underflowしないよう計算
        public static float Norm2(float x, float y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            return
                x > y ?
                MathF.Sqrt(1 + (y / x).Sq()) * x :
                x == y ? y * (float)Sqrt2 :
                MathF.Sqrt(1 + (x / y).Sq()) * y;
        }
        public static double Norm2(double x, double y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            return
                x > y ?
                Math.Sqrt(1 + (y / x).Sq()) * x :
                x == y ? y * Sqrt2 :
                Math.Sqrt(1 + (x / y).Sq()) * y;
        }
        public static void Norm2Test()
        {
            var X = new[] { 0, 1e-300, 1.5e308, double.PositiveInfinity, double.NaN };
            foreach (var x in X)
                foreach (var y in X)
                    Console.WriteLine($"{x}\t{y}\t{Mt.Norm2(x, y):e2}\t{Mt.Norm2_(x, y):e2}");
        }

        public static ComplexS Phase(float phase) => new ComplexS(MathF.Cos(phase), MathF.Sin(phase));
        public static ComplexD Phase(double phase) => new ComplexD(Math.Cos(phase), Math.Sin(phase));
        public static ComplexD Phase(int numerator, int denominator) => Phase(PI2 * numerator / denominator);
        #endregion

        #region sum, average
        internal static unsafe T SumPairOverwrite<T>(Span<T> data) where T : unmanaged
        {
            fixed (T* d = data) return Us.SumPairOverwrite(d, data.Length);
        }

        public static Array<R> Cast<T, R>(this Array x) where T : unmanaged where R : unmanaged => Cast<T, R>(x.As<T>());
        public static Array<R> Cast<T, R>(this Array<T> x) where T : unmanaged where R : unmanaged
        {
            var a = x.To0<R>();
            using var a_ = New.Fix(a);
            using var x_ = New.Fix(x);
            for (int i = x_.Length; --i >= 0;) Op.LetCast(out a_[i], x_[i]);
            return a;
        }
        public static Array<T> LetAddType<T, T1>(this Array<T> x, Array<T1> y) where T : unmanaged where T1 : unmanaged
        {
            Ex.SizeCheck(x.A, y.A);
            using var x_ = New.Fix(x);
            using var y_ = New.Fix(y);
            for (int i = x_.Length; --i >= 0;) Op.LetAdd(ref x_[i], Op<T>.From(y_[i]));
            return x;
        }
        public static Array<T> LetDiv<T>(this Array<T> x, long y) where T : unmanaged
        {
            using var x_ = New.Fix(x);
            for (int i = x_.Length; --i >= 0;) Op.LetDiv(ref x_[i], y);
            return x;
        }

        #region alias
        public static T Sum<T>(int count, Func<int, T> selector) where T : unmanaged => SumFwrd(count, selector);
        public static T Avg<T>(int count, Func<int, T> selector) where T : unmanaged => AvgFwrd(count, selector);
        public static T[] Sum<T>(int count, Func<int, T[]> selector) where T : unmanaged => SumFwrd(count, selector);
        public static T[,] Sum<T>(int count, Func<int, T[,]> selector) where T : unmanaged => SumFwrd(count, selector);
        public static T[,,] Sum<T>(int count, Func<int, T[,,]> selector) where T : unmanaged => SumFwrd(count, selector);
        public static T[] Avg<T>(int count, Func<int, T[]> selector) where T : unmanaged => AvgFwrd(count, selector);
        public static T[,] Avg<T>(int count, Func<int, T[,]> selector) where T : unmanaged => AvgFwrd(count, selector);
        public static T[,,] Avg<T>(int count, Func<int, T[,,]> selector) where T : unmanaged => AvgFwrd(count, selector);
        public static T Sum<S, T>(this Span<S> x, Func<S, T> selector) where T : unmanaged => SumFwrd(x, selector);
        public static T Avg<S, T>(this Span<S> x, Func<S, T> selector) where T : unmanaged => AvgFwrd(x, selector);
        public static T Sum<S, S1, T>(Span<S> x, Span<S1> y, Func<S, S1, T> selector) where T : unmanaged => SumFwrd(x, y, selector);
        public static T Avg<S, S1, T>(Span<S> x, Span<S1> y, Func<S, S1, T> selector) where T : unmanaged => AvgFwrd(x, y, selector);
        public static T Sum<S, S1, S2, T>(Span<S> x, Span<S1> y, Span<S2> z, Func<S, S1, S2, T> selector) where T : unmanaged => SumFwrd(x, y, z, selector);
        public static T Avg<S, S1, S2, T>(Span<S> x, Span<S1> y, Span<S2> z, Func<S, S1, S2, T> selector) where T : unmanaged => AvgFwrd(x, y, z, selector);
        #endregion

        #region Sum Func
        public static T SumFwrd<T>(int count, Func<int, T> selector) where T : unmanaged { var a = new SumFwrd<T>(); for (int i = 0; i < count; i++) a.Add(selector(i)); return a.Sum(); }
        public static T AvgFwrd<T>(int count, Func<int, T> selector) where T : unmanaged => Op.Div(SumFwrd(count, selector), count);
        public static T SumPair<T>(int count, Func<int, T> selector) where T : unmanaged { var a = new SumPair<T>(); for (int i = 0; i < count; i++) a.Add(selector(i)); return a.Sum(); }
        public static T AvgPair<T>(int count, Func<int, T> selector) where T : unmanaged => Op.Div(SumPair(count, selector), count);

        internal static Array<R> SumFwrdType<T, R>(int count, Func<int, Array> selector) where T : unmanaged where R : unmanaged
        {
            if (count <= 0) return default;
            var a = new SumFwrdArray<R>();
            for (int i = 0; i < count; i++) a.Add(selector(i).As<T>());
            return a.Sum();
        }
        internal static Array<R> AvgFwrdType<T, R>(int count, Func<int, Array> selector) where T : unmanaged where R : unmanaged => SumFwrdType<T, R>(count, selector).LetDiv(count);
        public static R[] SumFwrdType<T, R>(int count, Func<int, T[]> selector) where T : unmanaged where R : unmanaged => (R[])SumFwrdType<T, R>(count, (Func<int, Array>)selector);
        public static R[,] SumFwrdType<T, R>(int count, Func<int, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])SumFwrdType<T, R>(count, (Func<int, Array>)selector);
        public static R[,,] SumFwrdType<T, R>(int count, Func<int, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])SumFwrdType<T, R>(count, (Func<int, Array>)selector);
        public static R[] AvgFwrdType<T, R>(int count, Func<int, T[]> selector) where T : unmanaged where R : unmanaged => (R[])AvgFwrdType<T, R>(count, (Func<int, Array>)selector);
        public static R[,] AvgFwrdType<T, R>(int count, Func<int, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])AvgFwrdType<T, R>(count, (Func<int, Array>)selector);
        public static R[,,] AvgFwrdType<T, R>(int count, Func<int, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])AvgFwrdType<T, R>(count, (Func<int, Array>)selector);
        internal static Array<T> SumFwrd<T>(int count, Func<int, Array> selector) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return SumFwrdType<T, double>(count, selector).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return SumFwrdType<T, ComplexD>(count, selector).Cast<ComplexD, T>();
            return SumFwrdType<T, T>(count, selector);
        }
        internal static Array<T> AvgFwrd<T>(int count, Func<int, Array> selector) where T : unmanaged => SumFwrd<T>(count, selector).LetDiv(count);
        public static T[] SumFwrd<T>(int count, Func<int, T[]> selector) where T : unmanaged => (T[])SumFwrd<T>(count, (Func<int, Array>)selector);
        public static T[,] SumFwrd<T>(int count, Func<int, T[,]> selector) where T : unmanaged => (T[,])SumFwrd<T>(count, (Func<int, Array>)selector);
        public static T[,,] SumFwrd<T>(int count, Func<int, T[,,]> selector) where T : unmanaged => (T[,,])SumFwrd<T>(count, (Func<int, Array>)selector);
        public static T[] AvgFwrd<T>(int count, Func<int, T[]> selector) where T : unmanaged => (T[])AvgFwrd<T>(count, (Func<int, Array>)selector);
        public static T[,] AvgFwrd<T>(int count, Func<int, T[,]> selector) where T : unmanaged => (T[,])AvgFwrd<T>(count, (Func<int, Array>)selector);
        public static T[,,] AvgFwrd<T>(int count, Func<int, T[,,]> selector) where T : unmanaged => (T[,,])AvgFwrd<T>(count, (Func<int, Array>)selector);

        internal static Array<R> SumPairType<T, R>(int count, Func<int, Array> selector) where T : unmanaged where R : unmanaged
        {
            if (count <= 0) return default;
            var a = new SumPairArray<R>();
            for (int i = 0; i < count; i++) a.Add(selector(i).As<T>());
            return a.Sum();
        }
        internal static Array<R> AvgPairType<T, R>(int count, Func<int, Array> selector) where T : unmanaged where R : unmanaged => SumPairType<T, R>(count, selector).LetDiv(count);
        public static R[] SumPairType<T, R>(int count, Func<int, T[]> selector) where T : unmanaged where R : unmanaged => (R[])SumPairType<T, R>(count, (Func<int, Array>)selector);
        public static R[,] SumPairType<T, R>(int count, Func<int, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])SumPairType<T, R>(count, (Func<int, Array>)selector);
        public static R[,,] SumPairType<T, R>(int count, Func<int, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])SumPairType<T, R>(count, (Func<int, Array>)selector);
        public static R[] AvgPairType<T, R>(int count, Func<int, T[]> selector) where T : unmanaged where R : unmanaged => (R[])AvgPairType<T, R>(count, (Func<int, Array>)selector);
        public static R[,] AvgPairType<T, R>(int count, Func<int, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])AvgPairType<T, R>(count, (Func<int, Array>)selector);
        public static R[,,] AvgPairType<T, R>(int count, Func<int, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])AvgPairType<T, R>(count, (Func<int, Array>)selector);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> SumPair<T>(int count, Func<int, Array> selector) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return SumPairType<T, double>(count, selector).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return SumPairType<T, ComplexD>(count, selector).Cast<ComplexD, T>();
            return SumPairType<T, T>(count, selector);
        }
        internal static Array<T> AvgPair<T>(int count, Func<int, Array> selector) where T : unmanaged => SumPair<T>(count, selector).LetDiv(count);
        public static T[] SumPair<T>(int count, Func<int, T[]> selector) where T : unmanaged => (T[])SumPair<T>(count, (Func<int, Array>)selector);
        public static T[,] SumPair<T>(int count, Func<int, T[,]> selector) where T : unmanaged => (T[,])SumPair<T>(count, (Func<int, Array>)selector);
        public static T[,,] SumPair<T>(int count, Func<int, T[,,]> selector) where T : unmanaged => (T[,,])SumPair<T>(count, (Func<int, Array>)selector);
        public static T[] AvgPair<T>(int count, Func<int, T[]> selector) where T : unmanaged => (T[])AvgPair<T>(count, (Func<int, Array>)selector);
        public static T[,] AvgPair<T>(int count, Func<int, T[,]> selector) where T : unmanaged => (T[,])AvgPair<T>(count, (Func<int, Array>)selector);
        public static T[,,] AvgPair<T>(int count, Func<int, T[,,]> selector) where T : unmanaged => (T[,,])AvgPair<T>(count, (Func<int, Array>)selector);
        #endregion

        #region Sum Span<T>
        public static T SumFwrd<S, T>(this Span<S> x, Func<S, T> selector) where T : unmanaged { var a = new SumFwrd<T>(); for (int i = 0; i < x.Length; i++) a.Add(selector(x[i])); return a.Sum(); }
        public static T AvgFwrd<S, T>(this Span<S> x, Func<S, T> selector) where T : unmanaged => Op.Div(SumFwrd(x, selector), x.Length);
        public static T SumFwrd<S, S1, T>(Span<S> x, Span<S1> y, Func<S, S1, T> selector) where T : unmanaged { Ex.SizeCheck(x, y); var a = new SumFwrd<T>(); for (int i = 0; i < x.Length; i++) a.Add(selector(x[i], y[i])); return a.Sum(); }
        public static T AvgFwrd<S, S1, T>(Span<S> x, Span<S1> y, Func<S, S1, T> selector) where T : unmanaged => Op.Div(SumFwrd(x, y, selector), x.Length);
        public static T SumFwrd<S, S1, S2, T>(Span<S> x, Span<S1> y, Span<S2> z, Func<S, S1, S2, T> selector) where T : unmanaged { Ex.SizeCheck(x, y, z); var a = default(T); for (int i = 0; i < x.Length; i++) Op.LetAdd(ref a, selector(x[i], y[i], z[i])); return a; }
        public static T AvgFwrd<S, S1, S2, T>(Span<S> x, Span<S1> y, Span<S2> z, Func<S, S1, S2, T> selector) where T : unmanaged => Op.Div(SumFwrd(x, y, z, selector), x.Length);
        public static T SumPair<S, T>(this Span<S> x, Func<S, T> selector) where T : unmanaged { var a = new SumPair<T>(); for (int i = 0; i < x.Length; i++) a.Add(selector(x[i])); return a.Sum(); }
        public static T AvgPair<S, T>(this Span<S> x, Func<S, T> selector) where T : unmanaged => Op.Div(SumPair(x, selector), x.Length);
        public static T SumPair<S, S1, T>(Span<S> x, Span<S1> y, Func<S, S1, T> selector) where T : unmanaged { Ex.SizeCheck(x, y); var a = new SumPair<T>(); for (int i = 0; i < x.Length; i++) a.Add(selector(x[i], y[i])); return a.Sum(); }
        public static T AvgPair<S, S1, T>(Span<S> x, Span<S1> y, Func<S, S1, T> selector) where T : unmanaged => Op.Div(SumPair(x, y, selector), x.Length);
        public static T SumPair<S, S1, S2, T>(Span<S> x, Span<S1> y, Span<S2> z, Func<S, S1, S2, T> selector) where T : unmanaged { Ex.SizeCheck(x, y, z); var a = new SumPair<T>(); for (int i = 0; i < x.Length; i++) a.Add(selector(x[i], y[i], z[i])); return a.Sum(); }
        public static T AvgPair<S, S1, S2, T>(Span<S> x, Span<S1> y, Span<S2> z, Func<S, S1, S2, T> selector) where T : unmanaged => Op.Div(SumPair(x, y, z, selector), x.Length);
        #endregion

        #region Sum IEnumerable<T>
        internal static (R sum, long count) SumFwrdType_<T, R>(IEnumerable<T> source) where T : unmanaged where R : unmanaged
        {
            var a = new SumFwrd<R>();
            foreach (var e in source) a.Add(e);
            return (a.Sum(), a.Count);
        }
        public static R SumFwrdType<T, R>(this IEnumerable<T> source) where T : unmanaged where R : unmanaged => SumFwrdType_<T, R>(source).sum;
        public static R AvgFwrdType<T, R>(this IEnumerable<T> source) where T : unmanaged where R : unmanaged { var (sum, count) = SumFwrdType_<T, R>(source); return Op.Div(sum, count); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SumFwrd<T>(this IEnumerable<T> source) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)(float)SumFwrdType<T, double>(source);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)(ComplexS)SumFwrdType<T, ComplexD>(source);
            return SumFwrdType<T, T>(source);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AvgFwrd<T>(this IEnumerable<T> source) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)(float)AvgFwrdType<T, double>(source);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)(ComplexS)AvgFwrdType<T, ComplexD>(source);
            return AvgFwrdType<T, T>(source);
        }

        internal static (R sum, long count) SumPairType_<T, R>(IEnumerable<T> source) where T : unmanaged where R : unmanaged
        {
            var a = new SumPair<R>();
            foreach (var e in source) a.Add(e);
            return (a.Sum(), a.Count);
        }
        public static R SumPairType<T, R>(this IEnumerable<T> source) where T : unmanaged where R : unmanaged => SumPairType_<T, R>(source).sum;
        public static R AvgPairType<T, R>(this IEnumerable<T> source) where T : unmanaged where R : unmanaged { var (sum, count) = SumPairType_<T, R>(source); return Op.Div(sum, count); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SumPair<T>(this IEnumerable<T> source) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)(float)SumPairType<T, double>(source);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)(ComplexS)SumPairType<T, ComplexD>(source);
            return SumPairType<T, T>(source);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AvgPair<T>(this IEnumerable<T> source) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return (T)(object)(float)AvgPairType<T, double>(source);
            if (typeof(T) == typeof(ComplexS)) return (T)(object)(ComplexS)AvgPairType<T, ComplexD>(source);
            return AvgPairType<T, T>(source);
        }
        #endregion

        #region Sum IEnumerable<Array>
        internal static (Array<R> sum, long count) SumFwrdType_<T, R>(IEnumerable<Array> source, Array<R> init = default) where T : unmanaged where R : unmanaged
        {
            var a = new SumFwrdArray<R>(init);
            foreach (var e in source) a.Add(e.As<T>());
            return (a.Sum(), a.Count);
        }
        internal static Array<R> SumFwrdType<T, R>(IEnumerable<Array> source, Array<R> init = default) where T : unmanaged where R : unmanaged => SumFwrdType_<T, R>(source, init).sum;
        internal static Array<R> AvgFwrdType<T, R>(IEnumerable<Array> source, Array<R> init = default) where T : unmanaged where R : unmanaged { var (sum, count) = SumFwrdType_<T, R>(source, init); return sum.LetDiv(count); }
        public static R[] SumFwrdType<T, R>(this IEnumerable<T[]> source) where T : unmanaged where R : unmanaged => (R[])SumFwrdType<T, R>(source.Cast<Array>());
        public static R[,] SumFwrdType<T, R>(this IEnumerable<T[,]> source) where T : unmanaged where R : unmanaged => (R[,])SumFwrdType<T, R>(source.Cast<Array>());
        public static R[,,] SumFwrdType<T, R>(this IEnumerable<T[,,]> source) where T : unmanaged where R : unmanaged => (R[,,])SumFwrdType<T, R>(source.Cast<Array>());
        public static R[] AvgFwrdType<T, R>(this IEnumerable<T[]> source) where T : unmanaged where R : unmanaged => (R[])AvgFwrdType<T, R>(source.Cast<Array>());
        public static R[,] AvgFwrdType<T, R>(this IEnumerable<T[,]> source) where T : unmanaged where R : unmanaged => (R[,])AvgFwrdType<T, R>(source.Cast<Array>());
        public static R[,,] AvgFwrdType<T, R>(this IEnumerable<T[,,]> source) where T : unmanaged where R : unmanaged => (R[,,])AvgFwrdType<T, R>(source.Cast<Array>());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> SumFwrd<T>(IEnumerable<Array> source) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return SumFwrdType<T, double>(source).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return SumFwrdType<T, ComplexD>(source).Cast<ComplexD, T>();
            return SumFwrdType<T, T>(source);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> AvgFwrd<T>(IEnumerable<Array> source) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return AvgFwrdType<T, double>(source).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return AvgFwrdType<T, ComplexD>(source).Cast<ComplexD, T>();
            return AvgFwrdType<T, T>(source);
        }
        public static T[] SumFwrd<T>(this IEnumerable<T[]> source) where T : unmanaged => (T[])SumFwrd<T>(source.Cast<Array>());
        public static T[,] SumFwrd<T>(this IEnumerable<T[,]> source) where T : unmanaged => (T[,])SumFwrd<T>(source.Cast<Array>());
        public static T[,,] SumFwrd<T>(this IEnumerable<T[,,]> source) where T : unmanaged => (T[,,])SumFwrd<T>(source.Cast<Array>());
        public static T[] AvgFwrd<T>(this IEnumerable<T[]> source) where T : unmanaged => (T[])AvgFwrd<T>(source.Cast<Array>());
        public static T[,] AvgFwrd<T>(this IEnumerable<T[,]> source) where T : unmanaged => (T[,])AvgFwrd<T>(source.Cast<Array>());
        public static T[,,] AvgFwrd<T>(this IEnumerable<T[,,]> source) where T : unmanaged => (T[,,])AvgFwrd<T>(source.Cast<Array>());
        public static T[] SumFwrd<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => source.Select(selector).SumFwrd();
        public static T[,] SumFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => source.Select(selector).SumFwrd();
        public static T[,,] SumFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => source.Select(selector).SumFwrd();
        public static T[] AvgFwrd<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => source.Select(selector).AvgFwrd();
        public static T[,] AvgFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => source.Select(selector).AvgFwrd();
        public static T[,,] AvgFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => source.Select(selector).AvgFwrd();

        internal static (Array<R> sum, long count) SumPairType_<T, R>(IEnumerable<Array> source, bool overwrite = false) where T : unmanaged where R : unmanaged
        {
            var a = new SumPairArray<R>(overwrite);
            foreach (var e in source) a.Add(e.As<T>());
            return (a.Sum(), a.Count);
        }
        internal static Array<R> SumPairType<T, R>(IEnumerable<Array> source, bool overwrite = false) where T : unmanaged where R : unmanaged => SumPairType_<T, R>(source, overwrite).sum;
        internal static Array<R> AvgPairType<T, R>(IEnumerable<Array> source, bool overwrite = false) where T : unmanaged where R : unmanaged { var (sum, count) = SumPairType_<T, R>(source, overwrite); return sum.LetDiv(count); }
        public static R[] SumPairType<T, R>(this IEnumerable<T[]> source) where T : unmanaged where R : unmanaged => (R[])SumPairType<T, R>(source.Cast<Array>());
        public static R[,] SumPairType<T, R>(this IEnumerable<T[,]> source) where T : unmanaged where R : unmanaged => (R[,])SumPairType<T, R>(source.Cast<Array>());
        public static R[,,] SumPairType<T, R>(this IEnumerable<T[,,]> source) where T : unmanaged where R : unmanaged => (R[,,])SumPairType<T, R>(source.Cast<Array>());
        public static R[] AvgPairType<T, R>(this IEnumerable<T[]> source) where T : unmanaged where R : unmanaged => (R[])AvgPairType<T, R>(source.Cast<Array>());
        public static R[,] AvgPairType<T, R>(this IEnumerable<T[,]> source) where T : unmanaged where R : unmanaged => (R[,])AvgPairType<T, R>(source.Cast<Array>());
        public static R[,,] AvgPairType<T, R>(this IEnumerable<T[,,]> source) where T : unmanaged where R : unmanaged => (R[,,])AvgPairType<T, R>(source.Cast<Array>());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> SumPair<T>(IEnumerable<Array> source, bool overwrite = false) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return SumPairType<T, double>(source, overwrite).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return SumPairType<T, ComplexD>(source, overwrite).Cast<ComplexD, T>();
            return SumPairType<T, T>(source, overwrite);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> AvgPair<T>(IEnumerable<Array> source, bool overwrite = false) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return AvgPairType<T, double>(source, overwrite).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return AvgPairType<T, ComplexD>(source, overwrite).Cast<ComplexD, T>();
            return AvgPairType<T, T>(source, overwrite);
        }
        public static T[] SumPair<T>(this IEnumerable<T[]> source) where T : unmanaged => (T[])SumPair<T>(source.Cast<Array>());
        public static T[,] SumPair<T>(this IEnumerable<T[,]> source) where T : unmanaged => (T[,])SumPair<T>(source.Cast<Array>());
        public static T[,,] SumPair<T>(this IEnumerable<T[,,]> source) where T : unmanaged => (T[,,])SumPair<T>(source.Cast<Array>());
        public static T[] AvgPair<T>(this IEnumerable<T[]> source) where T : unmanaged => (T[])AvgPair<T>(source.Cast<Array>());
        public static T[,] AvgPair<T>(this IEnumerable<T[,]> source) where T : unmanaged => (T[,])AvgPair<T>(source.Cast<Array>());
        public static T[,,] AvgPair<T>(this IEnumerable<T[,,]> source) where T : unmanaged => (T[,,])AvgPair<T>(source.Cast<Array>());
        public static T[] SumPair<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => source.Select(selector).SumPair();
        public static T[,] SumPair<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => source.Select(selector).SumPair();
        public static T[,,] SumPair<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => source.Select(selector).SumPair();
        public static T[] AvgPair<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => source.Select(selector).AvgPair();
        public static T[,] AvgPair<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => source.Select(selector).AvgPair();
        public static T[,,] AvgPair<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => source.Select(selector).AvgPair();
        #endregion

        #region Sum Parallel
        internal static (Array<R> sum, long count) ParallelSumFwrdType_<S, T, R>(IEnumerable<S> source, Func<S, Array> selector) where T : unmanaged where R : unmanaged
        {
            var a = new SumFwrdArrayIndexed<T, R>();
            source.ParallelForEach((se, eid, pid) => { var e = selector(se).As<T>(); lock (a) { a.Add(e, eid); } });
            return (a.Sum(), a.Count);
        }
        internal static Array<R> ParallelSumFwrdType<S, T, R>(IEnumerable<S> source, Func<S, Array> selector) where T : unmanaged where R : unmanaged => ParallelSumFwrdType_<S, T, R>(source, selector).sum;
        internal static Array<R> ParallelAvgFwrdType<S, T, R>(IEnumerable<S> source, Func<S, Array> selector) where T : unmanaged where R : unmanaged { var (sum, count) = ParallelSumFwrdType_<S, T, R>(source, selector); return sum.LetDiv(count); }
        public static R[] ParallelSumFwrdType<S, T, R>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged where R : unmanaged => (R[])ParallelSumFwrdType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,] ParallelSumFwrdType<S, T, R>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])ParallelSumFwrdType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,,] ParallelSumFwrdType<S, T, R>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])ParallelSumFwrdType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[] ParallelAvgFwrdType<S, T, R>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged where R : unmanaged => (R[])ParallelAvgFwrdType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,] ParallelAvgFwrdType<S, T, R>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])ParallelAvgFwrdType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,,] ParallelAvgFwrdType<S, T, R>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])ParallelAvgFwrdType<S, T, R>(source, (Func<S, Array>)selector);

        internal static (Array<R> sum, long count) ParallelSumPairType_<S, T, R>(IEnumerable<S> source, Func<S, Array> selector, bool overwrite = false) where T : unmanaged where R : unmanaged
        {
            var a = new SumPairArray<R>(overwrite);  //Indexedにしたい
            var o = new object();
            Ex.ParallelForEach(source, se => { var e = selector(se).As<T>(); lock (o) { a.Add(e); } });
            return (a.Sum(), a.Count);
        }
        internal static Array<R> ParallelSumPairType<S, T, R>(IEnumerable<S> source, Func<S, Array> selector, bool overwrite = false) where T : unmanaged where R : unmanaged => ParallelSumPairType_<S, T, R>(source, selector, overwrite).sum;
        internal static Array<R> ParallelAvgPairType<S, T, R>(IEnumerable<S> source, Func<S, Array> selector, bool overwrite = false) where T : unmanaged where R : unmanaged { var (sum, count) = ParallelSumPairType_<S, T, R>(source, selector, overwrite); return sum.LetDiv(count); }
        public static R[] ParallelSumPairType<S, T, R>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged where R : unmanaged => (R[])ParallelSumPairType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,] ParallelSumPairType<S, T, R>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])ParallelSumPairType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,,] ParallelSumPairType<S, T, R>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])ParallelSumPairType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[] ParallelAvgPairType<S, T, R>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged where R : unmanaged => (R[])ParallelAvgPairType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,] ParallelAvgPairType<S, T, R>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged where R : unmanaged => (R[,])ParallelAvgPairType<S, T, R>(source, (Func<S, Array>)selector);
        public static R[,,] ParallelAvgPairType<S, T, R>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged where R : unmanaged => (R[,,])ParallelAvgPairType<S, T, R>(source, (Func<S, Array>)selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> ParallelSumFwrd<S, T>(IEnumerable<S> source, Func<S, Array> selector) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return ParallelSumFwrdType<S, T, double>(source, selector).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return ParallelSumFwrdType<S, T, ComplexD>(source, selector).Cast<ComplexD, T>();
            return ParallelSumFwrdType<S, T, T>(source, selector);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> ParallelAvgFwrd<S, T>(IEnumerable<S> source, Func<S, Array> selector) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return ParallelAvgFwrdType<S, T, double>(source, selector).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return ParallelAvgFwrdType<S, T, ComplexD>(source, selector).Cast<ComplexD, T>();
            return ParallelAvgFwrdType<S, T, T>(source, selector);
        }
        public static T[] ParallelSumFwrd<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => (T[])ParallelSumFwrd<S, T>(source, (Func<S, Array>)selector);
        public static T[,] ParallelSumFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => (T[,])ParallelSumFwrd<S, T>(source, (Func<S, Array>)selector);
        public static T[,,] ParallelSumFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => (T[,,])ParallelSumFwrd<S, T>(source, (Func<S, Array>)selector);
        public static T[] ParallelAvgFwrd<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => (T[])ParallelAvgFwrd<S, T>(source, (Func<S, Array>)selector);
        public static T[,] ParallelAvgFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => (T[,])ParallelAvgFwrd<S, T>(source, (Func<S, Array>)selector);
        public static T[,,] ParallelAvgFwrd<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => (T[,,])ParallelAvgFwrd<S, T>(source, (Func<S, Array>)selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> ParallelSumPair<S, T>(IEnumerable<S> source, Func<S, Array> selector, bool overwrite = false) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return ParallelSumPairType<S, T, double>(source, selector, overwrite).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return ParallelSumPairType<S, T, ComplexD>(source, selector, overwrite).Cast<ComplexD, T>();
            return ParallelSumPairType<S, T, T>(source, selector, overwrite);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array<T> ParallelAvgPair<S, T>(IEnumerable<S> source, Func<S, Array> selector, bool overwrite = false) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) return ParallelAvgPairType<S, T, double>(source, selector, overwrite).Cast<double, T>();
            if (typeof(T) == typeof(ComplexS)) return ParallelAvgPairType<S, T, ComplexD>(source, selector, overwrite).Cast<ComplexD, T>();
            return ParallelAvgPairType<S, T, T>(source, selector, overwrite);
        }
        public static T[] ParallelSumPair<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => (T[])ParallelSumPair<S, T>(source, (Func<S, Array>)selector);
        public static T[,] ParallelSumPair<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => (T[,])ParallelSumPair<S, T>(source, (Func<S, Array>)selector);
        public static T[,,] ParallelSumPair<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => (T[,,])ParallelSumPair<S, T>(source, (Func<S, Array>)selector);
        public static T[] ParallelAvgPair<S, T>(this IEnumerable<S> source, Func<S, T[]> selector) where T : unmanaged => (T[])ParallelAvgPair<S, T>(source, (Func<S, Array>)selector);
        public static T[,] ParallelAvgPair<S, T>(this IEnumerable<S> source, Func<S, T[,]> selector) where T : unmanaged => (T[,])ParallelAvgPair<S, T>(source, (Func<S, Array>)selector);
        public static T[,,] ParallelAvgPair<S, T>(this IEnumerable<S> source, Func<S, T[,,]> selector) where T : unmanaged => (T[,,])ParallelAvgPair<S, T>(source, (Func<S, Array>)selector);
        #endregion

        #region Sum other value-type
        public static TimeSpan Avg(this IEnumerable<TimeSpan> source) => TimeSpan.FromMilliseconds(source.Select(e => e.TotalMilliseconds).Average());
        public static TimeSpan Avg<TS>(this IEnumerable<TS> source, Func<TS, TimeSpan> selector) => source.Select(selector).Average();

        static (BigInteger sum, long count) Sum_(IEnumerable<BigInteger> source)
        {
            (BigInteger sum, long count) r = default;
            foreach (var e in source) { checked { r.sum += e; } r.count++; }
            return r;
        }
        public static BigInteger Sum(this IEnumerable<BigInteger> source) => Sum_(source).sum;
        public static BigInteger Sum<TS>(this IEnumerable<TS> source, Func<TS, BigInteger> selector) => source.Select(selector).Sum();

        static (Int2 sum, long count) Sum_(IEnumerable<Int2> source)
        {
            (Int2 sum, long count) r = default;
            foreach (var e in source) { checked { r.sum += e; } r.count++; }
            return r;
        }
        public static Int2 Sum(this IEnumerable<Int2> source) => Sum_(source).sum;
        public static Int2 Sum<TS>(this IEnumerable<TS> source, Func<TS, Int2> selector) => source.Select(selector).Sum();
        public static Double2 Avg(this IEnumerable<Int2> source) { var (sum, count) = Sum_(source); return (Double2)sum / count; }
        public static Double2 Avg<TS>(this IEnumerable<TS> source, Func<TS, Int2> selector) => source.Select(selector).Avg();

        static (Int3 sum, long count) Sum_(IEnumerable<Int3> source)
        {
            (Int3 sum, long count) r = default;
            foreach (var e in source) { checked { r.sum += e; } r.count++; }
            return r;
        }
        public static Int3 Sum(this IEnumerable<Int3> source) => Sum_(source).sum;
        public static Int3 Sum<TS>(this IEnumerable<TS> source, Func<TS, Int3> selector) => source.Select(selector).Sum();
        public static Double3 Avg(this IEnumerable<Int3> source) { var (sum, count) = Sum_(source); return (Double3)sum / count; }
        public static Double3 Avg<TS>(this IEnumerable<TS> source, Func<TS, Int3> selector) => source.Select(selector).Avg();

        static (Double2 sum, long count) SumFwrd_(IEnumerable<Double2> source)
        {
            (Double2 sum, long count) r = default;
            foreach (var e in source) { checked { r.sum += e; } r.count++; }
            return r;
        }
        public static Double2 Sum(this IEnumerable<Double2> source) => SumFwrd_(source).sum;
        public static Double2 Avg(this IEnumerable<Double2> source) { var (sum, count) = SumFwrd_(source); return sum / count; }
        public static Double2 Sum<TS>(this IEnumerable<TS> source, Func<TS, Double2> selector) => source.Select(selector).Sum();
        public static Double2 Avg<TS>(this IEnumerable<TS> source, Func<TS, Double2> selector) => source.Select(selector).Avg();

        static (Double3 sum, long count) SumFwrd_(IEnumerable<Double3> source)
        {
            (Double3 sum, long count) r = default;
            foreach (var e in source) { checked { r.sum += e; } r.count++; }
            return r;
        }
        public static Double3 Sum(this IEnumerable<Double3> source) => SumFwrd_(source).sum;
        public static Double3 Avg(this IEnumerable<Double3> source) { var (sum, count) = SumFwrd_(source); return sum / count; }
        public static Double3 Sum<TS>(this IEnumerable<TS> source, Func<TS, Double3> selector) => source.Select(selector).Sum();
        public static Double3 Avg<TS>(this IEnumerable<TS> source, Func<TS, Double3> selector) => source.Select(selector).Avg();
        #endregion

        #region Sum class
        public static T SumFwrd<T>(int count, Func<int, T> selector, Func<T, T, T> letadd, Func<T, T> clone)
        {
            if (count <= 0) ThrowException.ArgumentOutOfRange($"{nameof(count)} <= 0");
            var a = clone(selector(0));
            for (int i = 1; i < count; i++) letadd(a, selector(i));
            return a;
        }
        public static T AvgFwrd<T>(int count, Func<int, T> selector, Func<T, T, T> letadd, Func<T, T> clone, Func<T, int, T> letdiv) => letdiv(SumFwrd(count, selector, letadd, clone), count);
        public static T SumFwrd<T>(int count, T init, Func<int, T> selector, Func<T, T, T> letadd)
        {
            var a = init;
            for (int i = 0; i < count; i++) letadd(a, selector(i));
            return a;
        }

        #endregion
        #endregion

        #region other basic calculations
        #region Count bool[..]
        public static int Count(this bool[,] array)
        {
            var c = 0;
            foreach (var e in array) if (e) c++;
            return c;
        }
        public static int Count(this bool[,,] array)
        {
            var c = 0;
            foreach (var e in array) if (e) c++;
            return c;
        }
        #endregion

        #region product
        public static T Product<T>(int count, Func<int, T> selector) where T : unmanaged { T a = Op<T>.One; for (int i = 0; i < count; i++) checked { Op.LetMul(ref a, selector(i)); } return a; }
        public static T GeometricalAverage<T>(int count, Func<int, T> selector) where T : unmanaged => Op.Pow(Product(count, selector), 1.0 / count);

        static (T product, long count) Product_<T>(IEnumerable<T> source) where T : unmanaged
        {
            (T product, long count) r = (Op<T>.One, 0);
            foreach (var e in source) { checked { Op.LetMul(ref r.product, e); } r.count++; }
            return r;
        }
        public static T Product<T>(this IEnumerable<T> source) where T : unmanaged => Product_(source).product;
        public static T Product<TS, T>(this IEnumerable<TS> source, Func<TS, T> selector) where T : unmanaged => source.Select(selector).Product();

        static (BigInteger product, long count) Product_(this IEnumerable<BigInteger> source)
        {
            (BigInteger product, long count) r = (1, 0);
            foreach (var e in source) { checked { r.product *= e; } r.count++; }
            return r;
        }
        public static BigInteger Product(this IEnumerable<BigInteger> source) => source.Product_().product;

        public static T GeometricalAverage<TS, T>(this IEnumerable<TS> source, Func<TS, T> selector) where T : unmanaged => source.Select(selector).GeometricalAverage();
        public static T GeometricalAverage<T>(this IEnumerable<T> source) where T : unmanaged { var (product, count) = Product_(source); return Op.Pow(product, 1.0 / count); }

        #region BigInteger
        public static BigInteger Product(int count, Func<int, BigInteger> selector) { BigInteger a = 1; for (int i = 0; i < count; i++) a *= selector(i); return a; }
        public static BigInteger Sum(int count, Func<int, BigInteger> selector) { BigInteger a = 0; for (int i = 0; i < count; i++) a += selector(i); return a; }
        #endregion
        #endregion

        #region Variance
        public static double StandardErrorMean(this IEnumerable<double> source)
        {
            var (varianceN, count) = source.VarianceN_();
            if (count < 2) ThrowException.Argument($"{nameof(source)}: count < 2");
            return (varianceN / (count - 1) / count).Sqrt();
        }
        public static double StandardDeviation(this IEnumerable<double> source) => (source.VarianceUnbiased()).Sqrt();
        public static double VarianceUnbiased(this IEnumerable<double> source)
        {
            var (varianceN, count) = source.VarianceN_();
            if (count < 2) ThrowException.Argument($"{nameof(source)}: count < 2");
            return varianceN / (count - 1);
        }
        public static double VariancePopulation(this IEnumerable<double> source)
        {
            var (varianceN, count) = source.VarianceN_();
            if (count < 1) ThrowException.Argument($"{nameof(source)}: count < 1");
            return varianceN / count;
        }
        internal static (double varianceN, long count) VarianceN_(this IEnumerable<double> source, double average = double.NaN)
        {
            if (average.IsNaN()) average = source.AvgPair();
            (double varianceN, long count) r = default;
            foreach (var e in source) { r.varianceN += (e - average).Sq(); r.count++; }
            return r;
        }

        public static float[,] CovariancePopulation(this IEnumerable<float[]> source, float[]? average = null)
        {
            average ??= source.AvgPair();
            var a = new float[average.Length, average.Length];
            long c = 0;
            foreach (var e in source) { a.LetAddMulVVS(e.Sub(average), 1); c++; }
            return a.LetDiv(c);
        }
        public static double[,] CovariancePopulation(this IEnumerable<double[]> source, double[]? average = null)
        {
            average ??= source.AvgPair();
            var a = new double[average.Length, average.Length];
            long c = 0;
            foreach (var e in source) { a.LetAddMulVVS(e.Sub(average), 1); c++; }
            return a.LetDiv(c);
        }
        #endregion

        #region Median
        public static (int i0, int i1, double ratio) QuantileIndexRatio(int count, double quantile)
        {
            if (count <= 0) ThrowException.Argument(nameof(count));
            if (quantile < 0 || quantile > 1) ThrowException.Argument(nameof(quantile));
            var r = (count - 1) * quantile;
            int i = (int)r;
            r -= i;
            return (i, (r == 0 ? i : i + 1), r);
        }
        public static T DivideInternally<T>(T x, T y, double ratio) where T : unmanaged
        {
            return Op.Add(Op.Mul(x, Op<T>.From(1 - ratio)), Op.Mul(y, Op<T>.From(ratio)));
        }

        public static T Quantile<T>(this IList<T> data, double quantile, int[]? order = null) where T : unmanaged, IComparable<T>
        {
            order ??= data.SortIndex();
            var (i0, i1, r) = QuantileIndexRatio(data.Count, quantile);
            return DivideInternally(data[order[i0]], data[order[i1]], r);
        }
        public static T QuantileSorted<T>(this IList<T> data, double quantile) where T : unmanaged
        {
            var (i0, i1, r) = QuantileIndexRatio(data.Count, quantile);
            return DivideInternally(data[i0], data[i1], r);
        }
        public static T Median<T>(this IList<T> data) where T : unmanaged, IComparable<T> => Quantile(data, 0.5);
        public static T MedianSorted<T>(this IList<T> data) where T : unmanaged => QuantileSorted(data, 0.5);

        public static T Quantile<T>(this Span<T> data, double quantile, int[]? order = null) where T : unmanaged, IComparable<T>
        {
            order ??= data.SortIndex();
            var (i0, i1, r) = QuantileIndexRatio(data.Length, quantile);
            return DivideInternally(data[order[i0]], data[order[i1]], r);
        }
        public static T QuantileSorted<T>(this Span<T> data, double quantile) where T : unmanaged
        {
            var (i0, i1, r) = QuantileIndexRatio(data.Length, quantile);
            return DivideInternally(data[i0], data[i1], r);
        }
        public static T Median<T>(this Span<T> data) where T : unmanaged, IComparable<T> => Quantile(data, 0.5);
        public static T MedianSorted<T>(this Span<T> data) where T : unmanaged => QuantileSorted(data, 0.5);
        #endregion

        #region Cumurative
        public static IEnumerable<int> Cumurative(this IEnumerable<int> source)
        {
            int a = default;
            return source.Select(e => checked(a += e));
        }
        public static IEnumerable<int> CumurativeZero(this IEnumerable<int> source)
        {
            int a = default;
            return source.Select(e => { var s = a; checked { a += e; } return s; });
        }
        public static IEnumerable<double> Cumurative(this IEnumerable<double> source)
        {
            double a = default;
            return source.Select(e => checked(a += e));
        }
        public static IEnumerable<double> CumurativeZero(this IEnumerable<double> source)
        {
            double a = default;
            return source.Select(e => { var s = a; checked { a += e; } return s; });
        }
        #endregion
        #endregion

        #region integer functions
        public static bool IsPrime(int x) => (x > 1) && IsPrime((uint)x);
        public static bool IsPrime(uint x)
        {
            if ((x & 1) == 0) return x == 2;
            var limit = (uint)Math.Sqrt(x);
            for (uint i = 3; i <= limit; i += 2)
                if (x % i == 0) return false;
            return x != 1;
        }
        public static bool IsPrime(long x) => (x > 1) && IsPrime((ulong)x);
        public static bool IsPrime(ulong x)
        {
            if ((x & 1) == 0) return x == 2;
            var limit = (ulong)Math.Sqrt(x);
            for (uint i = 3; i <= limit; i += 2)
                if (x % i == 0) return false;
            return x != 1;
        }

        static readonly List<uint> PrimeNumbersUInt = new List<uint>() { 2, 3 };
        static uint PrimeNumbersUIntExamined = 3;
        static bool FindNextPrimeNumberUInt()
        {
            var v = PrimeNumbersUIntExamined;
            while (v != uint.MaxValue)
            {
                v += 2;
                var limit = (uint)Math.Sqrt(v);
                for (int i = 1; ; i++)
                {
                    var prime = PrimeNumbersUInt[i];
                    if (prime <= limit)
                    {
                        if (v % prime != 0) continue;
                        break;
                    }
                    PrimeNumbersUInt.Add(v);
                    PrimeNumbersUIntExamined = v;
                    return true;
                }
            }
            PrimeNumbersUIntExamined = v;
            return false;
        }

        static (int prime, int count)[] FactorInteger_(int x)
        {
            var list = new List<(int prime, int count)>();
            if (x == 0) list.Add((0, 1));
            else
            {
                var v = (uint)x;
                if (x < 0) { list.Add((-1, 1)); v = (uint)-x; }
                lock (PrimeNumbersUInt)
                {
                    for (int i = 0; v != 1; i++)
                    {
                        if (PrimeNumbersUInt.Count <= i) FindNextPrimeNumberUInt();
                        var prime = PrimeNumbersUInt[i];
                        int count = 0;
                        while (v % prime == 0) { v /= prime; count++; }
                        if (count > 0) { list.Add(((int)prime, count)); continue; }
                        if (v < prime * prime) { list.Add(((int)v, 1)); break; }
                    }
                }
            }
            return list.ToArray();
        }
        public static readonly Func<int, (int prime, int count)[]> FactorInteger = New.Cache<int, (int prime, int count)[]>(FactorInteger_);
        static int[] FactorIntegerExpanded_(int x)
        {
            return FactorInteger(x).SelectMany(p => Enumerable.Repeat(p.prime, p.count)).ToArray();
        }
        public static readonly Func<int, int[]> FactorIntegerExpanded = New.Cache<int, int[]>(FactorIntegerExpanded_);

        public static BigInteger MultinomialInteger(IEnumerable<int> source)
        {
            int total = 0;
            BigInteger product = 1;
            foreach (int e in source)
            {
                if (e < 0) ThrowException.Argument($"{nameof(source)}: element < 0");
                total += e;
                product *= FactorialInteger(e);
            }
            return FactorialInteger(total) / product;
        }

        static readonly List<BigInteger> FactorialIntegerBuffer = new List<BigInteger>() { 1 };
        static BigInteger FactorialInteger_(int x)
        {
            BigInteger product = FactorialIntegerBuffer.Last();
            for (int i = FactorialIntegerBuffer.Count; i <= x; i++)
            {
                product *= i;
                FactorialIntegerBuffer.Add(product);
            }
            return product;
        }
        public static BigInteger FactorialInteger(int x)
        {
            if (x < 0) ThrowException.ArgumentOutOfRange(nameof(x));
            return x < FactorialIntegerBuffer.Count ? FactorialIntegerBuffer[x] : FactorialInteger_(x);
        }

        public static BigInteger GreatestCommonDivisor(BigInteger x0, BigInteger x1)
        {
            while (true)
            {
                if (x0 < x1) Ex.Swap(ref x0, ref x1);
                var z = x0 % x1;
                if (z == 0) return x1;
                x0 = z;
            }
        }
        #endregion

        #region special functions
        #region gamma functions
        static readonly double[] LogGammaCoefficients = { 57.1562356658629235, -59.5979603554754912, 14.1360979747417471, -0.491913816097620199, 0.339946499848118887e-4, 0.465236289270485756e-4, -0.983744753048795646e-4, 0.158088703224912494e-3, -0.210264441724104883e-3, 0.217439618115212643e-3, -0.164318106536763890e-3, 0.844182239838527433e-4, -0.261908384015814087e-4, 0.368991826595316234e-5 };
        public static double LogGamma(double x)
        {
            if (x <= 0) return double.NaN;
            double ser = 0.999999999999997092;
            for (int i = 0; i < LogGammaCoefficients.Length; i++) ser += LogGammaCoefficients[i] / (i + 1 + x);
            double tmp = x + 5.24218750000000000;
            return (x + 0.5) * Math.Log(tmp) - tmp + Math.Log(2.5066282746310005 * ser / x);
        }
        public static double Gamma(double x) => Math.Exp(LogGamma(x));
        public static double MultivariateLogGamma(double x, int dim)
        {
            if (dim <= 0) ThrowException.Argument(nameof(dim));
            if (x <= (dim - 1) * 0.5) ThrowException.Argument(nameof(x));
            return SumFwrd(dim, d => LogGamma(x - d * 0.5)) + (dim * (dim - 1) / 4.0) * Math.Log(Math.PI);
        }
        public static double Digamma(double x)
        {
            double eps = Math.Max(x, 1e-10) * 1e-10;
            return (LogGamma(x + eps) - LogGamma(x)) / eps;
        }
        public static double MultivariateDigamma(double x, int dim) => SumFwrd(dim, d => Digamma(x - d * 0.5));

        // 階乗
        // 22! まではdoubleで正確に表現可能
        // 170! まではdoubleで近似的に表現可能
        static double[]? FactorialBuffer;
        public static double Factorial(int x)
        {
            FactorialBuffer ??= Factorial_();
            if (x < 0 || x >= FactorialBuffer.Length) ThrowException.ArgumentOutOfRange(nameof(x));
            return FactorialBuffer[x];
            static double[] Factorial_()
            {
                var buffer = new double[171];
                double f = 1;
                buffer[0] = f;
                for (int i = 1; i < buffer.Length; i++)
                {
                    f *= i;
                    buffer[i] = f;
                }
                return buffer;
            }
        }

        static double[]? LogFactorialBuffer;
        public static double LogFactorial(int x)
        {
            if (x < 0) ThrowException.ArgumentOutOfRange(nameof(x));
            LogFactorialBuffer ??= LogFactorial_();
            if (x < LogFactorialBuffer.Length) return LogFactorialBuffer[x];
            return LogGamma(x + 1.0);
            static double[] LogFactorial_()
            {
                var buffer = new double[2000];
                double f = 0;
                for (int i = 2; i < buffer.Length; i++)
                {
                    f += Math.Log(i);
                    buffer[i] = i <= 22 ? Math.Log(Factorial(i)) : f;
                }
                return buffer;
            }
        }

        // same to Pochhammer function
        public static double RisingFactorial(int x, int count)
        {
            if (count == 0) return 1;
            if (count == 1) return x;
            var (s, v0, v1) = x > 0 ? (1, x + count - 1, x - 1) : (1 - 2 * (count & 1), -x, -x - count);
            if (v0 < 0) return double.NaN;
            if (v1 < 0) return 0;
            return s * (v1 < 171 && v0 < 171 ? Factorial(v0) / Factorial(v1) : Math.Exp(LogFactorial(v0) - LogFactorial(v1)));
        }
        // FactorialPowerと同じ
        public static double FallingFactorial(int x, int count)
        {
            if (count == 0) return 1;
            if (count == 1) return x;
            var (s, v0, v1) = x >= count ? (1, x, x - count) : (1 - 2 * (count & 1), -x + count - 1, -x - 1);
            if (v0 < 0) return double.NaN;
            if (v1 < 0) return 0;
            return s * (v1 < 171 && v0 < 171 ? Factorial(v0) / Factorial(v1) : Math.Exp(LogFactorial(v0) - LogFactorial(v1)));
        }

        public static double LogBinomial(int x, int y)
        {
            if (x < 0) ThrowException.ArgumentOutOfRange(nameof(x));
            int z = x - y;
            if (y < 0 || z < 0) return double.NegativeInfinity;
            if (y == 0 || z == 0) return 0.0;
            return LogFactorial(x) - (LogFactorial(y) + LogFactorial(z));
        }
        public static double Binomial(int x, int y)
        {
            if (x < 0) ThrowException.ArgumentOutOfRange(nameof(x));
            int z = x - y;
            if (y < 0 || z < 0) return 0.0;
            if (y == 0 || z == 0) return 1.0;
            return Math.Round(Math.Exp(LogFactorial(x) - (LogFactorial(y) + LogFactorial(z))), MidpointRounding.AwayFromZero);
        }
        public static double LogMultinomial(IEnumerable<int> source)
        {
            int total = 0;
            double sum = 0;
            foreach (int e in source)
            {
                if (e < 0) ThrowException.ArgumentOutOfRange($"{nameof(source)}: element < 0");
                total += e;
                sum += LogFactorial(e);
            }
            return LogFactorial(total) - sum;
        }
        public static double Multinomial(IEnumerable<int> table) => Math.Round(Math.Exp(LogMultinomial(table)), MidpointRounding.AwayFromZero);

        // beta function
        public static double LogBeta(double x, double y) => LogGamma(x) + LogGamma(y) - LogGamma(x + y);
        public static double Beta(double x, double y) => Math.Exp(LogBeta(x, y));
        #endregion

        #region incomplete gamma functions
        static readonly double[] Gauleg18y = new double[] {
            0.0021695375159141994, 0.011413521097787704, 0.027972308950302116, 0.051727015600492421,
            0.082502225484340941, 0.12007019910960293, 0.16415283300752470, 0.21442376986779355,
            0.27051082840644336, 0.33199876341447887, 0.39843234186401943, 0.46931971407375483,
            0.54413605556657973, 0.62232745288031077, 0.70331500465597174, 0.78649910768313447,
            0.87126389619061517, 0.95698180152629142
        };
        static readonly double[] Gauleg18w = new double[] {
            0.0055657196642445571, 0.012915947284065419, 0.020181515297735382, 0.027298621498568734,
            0.034213810770299537, 0.040875750923643261, 0.047235083490265582, 0.053244713977759692,
            0.058860144245324798, 0.064039797355015485, 0.068745323835736408, 0.072941885005653087,
            0.076598410645870640, 0.079687828912071670, 0.082187266704339706, 0.084078218979661945,
            0.085346685739338721, 0.085983275670394821
        };
        static double IncompleteGamma_(double x, double gamma, bool upper)
        {
            if (gamma <= 0) return double.NaN;
            if (!upper)
            {
                if (x == double.PositiveInfinity) return 1;
                if (x <= 0) return 0;
                if (gamma >= 100) return gammpapprox(x, gamma, true);
                return (x < gamma + 1) ? gser(x, gamma) : 1 - gcf(x, gamma);
            }
            else
            {
                if (x == double.PositiveInfinity) return 0;
                if (x <= 0) return 1;
                if (gamma >= 100) return gammpapprox(x, gamma, false);
                return (x < gamma + 1) ? 1 - gser(x, gamma) : gcf(x, gamma);
            }

            static double gammpapprox(double x_, double gamma_, bool psig)
            {
                double a1 = gamma_ - 1, lna1 = Math.Log(a1), sqrta1 = Math.Sqrt(a1);
                double xu = (x_ > a1) ?
                    Math.Max(a1 + 11.5 * sqrta1, x_ + 6 * sqrta1) :
                    Math.Max(0, Math.Min(a1 - 7.5 * sqrta1, x_ - 5 * sqrta1));
                double sum = 0;
                for (int j = 0; j < Gauleg18y.Length; j++)
                {
                    double t = x_ + (xu - x_) * Gauleg18y[j];
                    sum += Gauleg18w[j] * Math.Exp(a1 - t + a1 * (Math.Log(t) - lna1));
                }
                double ans = sum * (xu - x_) * Math.Exp(a1 * (lna1 - 1) - LogGamma(gamma_));
                return psig ? (ans > 0 ? 1 : 0) - ans : (ans >= 0 ? 0 : 1) + ans;
            }
            static double gser(double x_, double gamma_)
            {
                double sum = 1 / gamma_;
                double del = sum;
                for (int i = 1; ; i++)
                {
                    del *= x_ / (gamma_ + i);
                    sum += del;
                    if (Math.Abs(del) < Math.Abs(sum) * DoubleEps) break;
                }
                return sum * Math.Exp(gamma_ * Math.Log(x_) - x_ - LogGamma(gamma_));
            }
            static double gcf(double x_, double gamma_)
            {
                double c = double.PositiveInfinity;
                double d = 1 / (x_ - gamma_ + 1);
                double h = d;
                for (int n = 1; ; n++)
                {
                    double an = n * (gamma_ - n);
                    double bn = (x_ - gamma_ + 1) + n * 2;
                    c = bn + an / c; if (Math.Abs(c) < DoubleFpMin) c = DoubleFpMin;
                    d = bn + an * d; if (Math.Abs(d) < DoubleFpMin) d = DoubleFpMin;
                    if (c == d) break;
                    h *= c / d;
                    d = 1.0 / d;
                }
                return h * Math.Exp(gamma_ * Math.Log(x_) - x_ - LogGamma(gamma_));
            }
        }
        // integrate [0, x] t^(gamma-1) e^-t dt / Gamma(gamma)
        public static double IncompleteGammaLower(double x, double gamma) => IncompleteGamma_(x, gamma, false);
        // integrate [x, inf) t^(gamma-1) e^-t dt / Gamma(gamma)
        public static double IncompleteGammaUpper(double x, double gamma) => IncompleteGamma_(x, gamma, true);

        public static double InverseIncompleteGamma(double y, double gamma)
        {
            if (gamma <= 0) return double.NaN;
            if (y >= 1) return Math.Max(100, gamma + 100 * Math.Sqrt(gamma));
            if (y <= 0) return 0;

            const double eps = 1e-8;
            double x, t, lna1 = 0, afac = 0, a1 = gamma - 1;
            double gln = LogGamma(gamma);
            if (gamma > 1)
            {
                lna1 = Math.Log(a1);
                afac = Math.Exp(a1 * (lna1 - 1) - gln);
                t = Math.Sqrt(-2 * Math.Log((y < 0.5) ? y : 1 - y));
                x = (2.30753 + t * 0.27061) / (1 + t * (0.99229 + t * 0.04481)) - t;
                if (y < 0.5) x = -x;
                x = Math.Max(1.0e-3, gamma * Math.Pow(1 - 1 / (9 * gamma) - x / (3 * Math.Sqrt(gamma)), 3));
            }
            else
            {
                t = 1 - gamma * (0.253 + gamma * 0.12);
                x = (y < t) ?
                    Math.Pow(y / t, 1 / gamma) :
                    1 - Math.Log(1 - (y - t) / (1 - t));
            }
            for (int j = 0; j < 12; j++)
            {
                if (x <= 0) return 0;
                double err = IncompleteGammaLower(x, gamma) - y;
                t = (gamma > 1) ?
                    afac * Math.Exp(-(x - a1) + a1 * (Math.Log(x) - lna1)) :
                    Math.Exp(-x + a1 * Math.Log(x) - gln);
                double u = err / t;
                x -= (t = u / (1 - 0.5 * Math.Min(1, u * (a1 / x - 1))));
                if (x <= 0) x = 0.5 * (x + t);
                if (Math.Abs(t) < eps * x) break;
            }
            return x;
        }
        public static double Erf(double x) => IncompleteGammaLower(x * x, 0.5) * Math.Sign(x);
        // normal distribution
        public static double StandardNormalDistribution(double x) => Math.Exp(x * x * -0.5) * Sqrt2PIinv;
        public static double StandardNormalDistributionLower(double x) => (1 + Erf(x * Sqrt2inv)) / 2;
        public static double StandardNormalDistributionUpper(double x) => (1 - Erf(x * Sqrt2inv)) / 2;
        public static double NormalDistribution(double x, double mean, double variance)
        {
            if (variance <= 0) ThrowException.Argument($"{nameof(variance)} <= 0");
            return Math.Exp((x - mean).Sq() / variance * -0.5) / Math.Sqrt(PI2 * variance);
        }

        // chi-square distribution
        public static double ChiSquareDistribution(double x, double freedom)
        {
            if (freedom < 0) ThrowException.Argument(nameof(freedom));
            if (x < 0) return 0;
            if (x == 0)
            {
                if (freedom < 2) return double.PositiveInfinity;
                if (freedom == 2) return 0.5;
                return 0;
            }
            double f = freedom / 2;
            return Math.Exp(-0.5 * x + (f - 1) * Math.Log(x) - f * Mt.Ln2 - LogGamma(f));
        }
        public static double ChiSquareDistributionLower(double x, double freedom) => IncompleteGammaLower(x / 2, freedom / 2);
        public static double ChiSquareDistributionUpper(double x, double freedom) => IncompleteGammaUpper(x / 2, freedom / 2);
        #endregion

        #region incomplete beta functions

        public static double IncompleteBeta(double x, double param1, double param2)
        {
            if (x < 0 || x > 1) ThrowException.ArgumentOutOfRange(nameof(x));
            if (param1 <= 0) ThrowException.ArgumentOutOfRange(nameof(param1));
            if (param2 <= 0) ThrowException.ArgumentOutOfRange(nameof(param2));
            if (x == 0 || x == 1) return x;
            if (param1 > 3000 && param2 > 3000) return betaiapprox(param1, param2, x);
            double bt = Math.Exp(
                LogGamma(param1 + param2) - LogGamma(param1) - LogGamma(param2)
                + param1 * Math.Log(x) + param2 * Math.Log(1 - x));
            return (x < (param1 + 1) / (param1 + param2 + 2)) ?
                bt * betacf(param1, param2, x) / param1 :
                1 - bt * betacf(param2, param1, 1 - x) / param2;

            static double betaiapprox(double a, double b, double x_)
            {
                double xu;
                double mu = a / (a + b);
                double lnmu = Math.Log(mu);
                double lnmuc = Math.Log(1 - mu);
                double t = Math.Sqrt(a * b / ((a + b).Sq() * (a + b + 1)));
                if (x_ > a / (a + b))
                {
                    if (x_ >= 1) return 1;
                    xu = Math.Min(1.0, Math.Max(mu + 10 * t, x_ + 5 * t));
                }
                else
                {
                    if (x_ <= 0) return 0;
                    xu = Math.Max(0.0, Math.Min(mu - 10 * t, x_ - 5 * t));
                }
                double sum = 0;
                for (int j = 0; j < 18; j++)
                {
                    t = x_ + (xu - x_) * Gauleg18y[j];
                    sum += Gauleg18w[j] * Math.Exp((a - 1) * (Math.Log(t) - lnmu) + (b - 1) * (Math.Log(1 - t) - lnmuc));
                }
                double ans = sum * (xu - x_) * Math.Exp((a - 1) * lnmu - LogGamma(a) + (b - 1) * lnmuc - LogGamma(b) + LogGamma(a + b));
                return ans > 0 ? 1 - ans : -ans;
            }
            static double betacf(double a, double b, double x_)
            {
                static double absmaxFPMIN(double x__) => Math.Abs(x__) < DoubleFpMin ? DoubleFpMin : x__;
                double qab = a + b;
                double qap = a + 1;
                double qam = a - 1;
                double c = 1;
                double d = 1 / absmaxFPMIN(1 - qab * x_ / qap);
                double h = d;
                for (int m = 1; m < 10000; m++)
                {
                    int m2 = 2 * m;
                    double aa = m * (b - m) * x_ / ((qam + m2) * (a + m2));
                    d = 1 / absmaxFPMIN(1 + aa * d);
                    c = absmaxFPMIN(1 + aa / c);
                    h *= d * c;
                    aa = -(a + m) * (qab + m) * x_ / ((a + m2) * (qap + m2));
                    d = 1 / absmaxFPMIN(1 + aa * d);
                    c = absmaxFPMIN(1 + aa / c);
                    h *= d * c;
                    if (Math.Abs(d * c - 1) <= DoubleEps) break;
                }
                return h;
            }
        }

        // Student t distribution
        public static double StudentTDistribution(double x, double freedom)
        {
            double n2 = (freedom + 1) / 2;
            return Gamma(n2) / (Gamma(freedom / 2) * Math.Sqrt(Math.PI * freedom)) * Math.Pow(1 + x * x / freedom, -n2);
        }
        // ∫∞~-t and t~∞
        public static double StudentTDistributionBilateral(double x, double freedom)
        {
            return IncompleteBeta(freedom / (freedom + x * x), freedom / 2, 0.5);
        }
        // ∫t~∞
        public static double StudentTDistributionUpper(double x, double freedom)
        {
            double a = StudentTDistributionBilateral(x, freedom) / 2;
            return x < 0 ? 1 - a : a;
        }
        // ∫-∞~t
        public static double StudentTDistributionLower(double x, double freedom)
        {
            double a = StudentTDistributionBilateral(x, freedom) / 2;
            return x < 0 ? a : 1 - a;
        }

        // F distribution
        // ∫f~∞, Q(F|v1,v2) <0.01 で有意
        public static double FDistributionUpper(double x, double freedom1, double freedom2)
        {
            return IncompleteBeta(freedom2 / (freedom2 + freedom1 * x), freedom2 / 2, freedom1 / 2);
        }
        // ∫-∞~f = ∫0~f
        public static double FDistributionLower(double x, double freedom1, double freedom2)
        {
            return 1 - FDistributionUpper(x, freedom1, freedom2);
        }
        #endregion

        #region elliptic functions
        public static double EllipticTheta3(double phase, double radius)
        {
            if (radius < 0 || radius >= 1) ThrowException.ArgumentOutOfRange(nameof(radius));
            if (radius == 0) return 1;
            int digit = (int)Math.Ceiling(Math.Sqrt(Math.Log(DoubleEps) / Math.Log(radius)));
            double a = 0;
            for (int d = 1; d <= digit; d++)
                a += Math.Cos(2 * d * phase) * Math.Pow(radius, d * d);
            return 1 + 2 * a;
        }
        #endregion

        #region Kernel functions
        public static double LinearKernel(double[] x, double[] y) => Inner(x, y);
        public static double PowerKernel(double[] x, double[] y, double p) => Math.Pow(Inner(x, y), p);
        public static double PolynomialKernel(double[] x, double[] y, double a, double b, double p) => Math.Pow(Inner(x, y) * a + b, p);
        public static double LogisticKernel(double[] x, double[] y, double beta) => Logistic(Inner(x, y) * beta);
        public static double TanhKernel(double[] x, double[] y, double a, double b) => Math.Tanh(Inner(x, y) * a + b);
        public static double GaussianKernel(double[] x, double[] y, double sigma2) => Math.Exp(-0.5 * DNorm2SqSub(x, y) / sigma2);
        #endregion
        #endregion

        #region linear functions
        #region type-generic
        public static T[,] T<T>(this T[,] x)
        {
            var L = x.Lengths();
            var a = new T[L.v1, L.v0];
            var x_ = x.AsSpan();
            int j = x_.Length;
            for (int i0 = L.v0; --i0 >= 0;)
                for (int i1 = L.v1; --i1 >= 0;)
                    a[i1, i0] = x_[--j];
            return a;
        }
        public static T[,] H<T>(this T[,] x) where T : unmanaged
        {
            var L = x.Lengths();
            var a = new T[L.v1, L.v0];
            var x_ = x.AsSpan();
            int j = x_.Length;
            for (int i0 = L.v0; --i0 >= 0;)
                for (int i1 = L.v1; --i1 >= 0;)
                    a[i1, i0] = Op.Cnj(x_[--j]);
            return a;
        }

        public static T[,] UnitMatrix<T>(int n) where T : unmanaged => DiagonalMatrix(n, Op<T>.One);
        public static T[,] DiagonalMatrix<T>(int n, T x) where T : unmanaged
        {
            var a = new T[n, n];
            for (int i = n; --i >= 0;) a[i, i] = x;
            return a;
        }
        public static T[,] DiagonalMatrix<T>(this T[] x) where T : unmanaged
        {
            var a = new T[x.Length, x.Length];
            for (int i = x.Length; --i >= 0;) a[i, i] = x[i];
            return a;
        }

        public static T MultiplyQuadratic<T>(this T[,] x, T[] y) where T : unmanaged => MultiplyQuadratic(x, y, y);
        public static T MultiplyQuadratic<T>(this T[,] x, T[] y, T[] z) where T : unmanaged => y.Multiply(x).MultiplyTN(z);
        public static T[,] MultiplyNT<T>(this T[,] x) where T : unmanaged => MultiplyNT(x, x);
        public static T[,] MultiplyTN<T>(this T[,] x) where T : unmanaged => MultiplyTN(x, x);

        public static unsafe T MultiplyTN<T>(this T[] x, T[] y) where T : unmanaged { fixed (T* p = x, q = y) return Us.SumMul(p, q, Ex.SameLength(x, y)); }
        public static unsafe T[,] MultiplyNT<T>(this T[] x, T[] y) where T : unmanaged
        {
            int n = x.Length;
            int m = y.Length;
            var A = new T[n, m];
            fixed (T* r = A, p = x) fixed (T* q = y)
                for (int i = n; --i >= 0;)
                    Us.Mul(&r[m * i], q, p[i], m);
            return A;
        }
        public static unsafe T[] Multiply<T>(this T[] x, T[,] y) where T : unmanaged
        {
            var a = new T[y.GetLength(1)];
            fixed (T* r = a, p = x) fixed (T* q = y)
                MultiplyNN(1, x.Length, p, y.GetLength(0), y.GetLength(1), q, r);
            return a;
        }
        public static unsafe T[] Multiply<T>(this T[,] x, T[] y) where T : unmanaged
        {
            var a = new T[x.GetLength(0)];
            fixed (T* r = a, p = x, q = y)
                MultiplyNT(x.GetLength(0), x.GetLength(1), p, 1, y.Length, q, r);
            return a;
        }
        public static unsafe T[,] Multiply<T>(this T[,] x, T[,] y) where T : unmanaged
        {
            var A = new T[x.GetLength(0), y.GetLength(1)];
            fixed (T* r = A, p = x, q = y)
                MultiplyNN(x.GetLength(0), x.GetLength(1), p, y.GetLength(0), y.GetLength(1), q, r);
            return A;
        }
        public static unsafe T[,] MultiplyNT<T>(this T[,] x, T[,] y) where T : unmanaged
        {
            var A = new T[x.GetLength(0), y.GetLength(0)];
            fixed (T* r = A, p = x, q = y)
                MultiplyNT(x.GetLength(0), x.GetLength(1), p, y.GetLength(0), y.GetLength(1), q, r);
            return A;
        }
        public static unsafe T[,] MultiplyTN<T>(this T[,] x, T[,] y) where T : unmanaged
        {
            var A = new T[x.GetLength(1), y.GetLength(1)];
            fixed (T* r = A, p = x, q = y)
                MultiplyTN(x.GetLength(0), x.GetLength(1), p, y.GetLength(0), y.GetLength(1), q, r);
            return A;
        }

        static unsafe void MultiplyNN<T>(int n, int o, T* p, int oo, int m, T* q, T* r) where T : unmanaged
        {
            if (o != oo) ThrowException.SizeMismatch();
            for (int i = n; --i >= 0;)
            {
                var ri = &r[m * i];
                var pi = &p[o * i];
                for (int j = o; --j >= 0;)
                    Us.LetAddMul(ri, &q[m * j], pi[j], m);
            }
        }
        static unsafe void MultiplyTN<T>(int o, int n, T* p, int oo, int m, T* q, T* r) where T : unmanaged
        {
            if (o != oo) ThrowException.SizeMismatch();
            for (int i = n; --i >= 0;)
            {
                var ri = &r[m * i];
                for (int j = o; --j >= 0;)
                    Us.LetAddMul(ri, &q[m * j], p[o * j + i], m);
            }
        }
        static unsafe void MultiplyNT<T>(int n, int o, T* p, int m, int oo, T* q, T* r) where T : unmanaged
        {
            if (o != oo) ThrowException.SizeMismatch();
            fixed (T* a = new T[o])
            {
                for (int i = n; --i >= 0; p += o)
                {
                    var qj = q;
                    for (int j = m; --j >= 0; qj += o, r++)
                    {
                        //*r = Us.SumMul(p, qj, o);
                        Us.Mul(a, p, qj, o); *r = Us.SumPairOverwrite(a, o);
                        //Us.Mul(a, p, qj, o); *r = Us.SumFwrd(a, o);
                    }
                }
            }
        }
        public static T Tr<T>(this T[,] x) where T : unmanaged { T a = default; for (int i = CheckSquareMatrix(x); --i >= 0;) Op.LetAdd(ref a, x[i, i]); return a; }
        static int CheckSquareMatrix<T>(T[,] x) where T : unmanaged
        {
            int n = x.GetLength(0);
            if (n != x.GetLength(1)) ThrowException.SizeMismatch();
            return n;
        }
        public static T[,] LetAddDiag<T>(this T[,] a, T y) where T : unmanaged { for (int i = CheckSquareMatrix(a); --i >= 0;) Op.LetAdd(ref a[i, i], y); return a; }
        public static T[,] LetSubDiag<T>(this T[,] a, T y) where T : unmanaged { for (int i = CheckSquareMatrix(a); --i >= 0;) Op.LetSub(ref a[i, i], y); return a; }
        public static T[,] LetSubrDiag<T>(this T[,] a, T y) where T : unmanaged => a.LetNeg().LetAddDiag(y);
        public static T[,] AddDiag<T>(this T[,] x, T y) where T : unmanaged => x.CloneX().LetAddDiag(y);
        public static T[,] SubDiag<T>(this T[,] x, T y) where T : unmanaged => x.CloneX().LetSubDiag(y);
        public static T[,] SubrDiag<T>(this T[,] x, T y) where T : unmanaged => x.Neg().LetAddDiag(y);

        static int CheckSquareMatrix<T>(T[,] x, T[] y) where T : unmanaged
        {
            int n = x.GetLength(0);
            if (n != x.GetLength(1) || n != y.Length) ThrowException.SizeMismatch();
            return n;
        }
        public static T[,] LetAddDiag<T>(this T[,] a, T[] y) where T : unmanaged { for (int i = CheckSquareMatrix(a, y); --i >= 0;) Op.LetAdd(ref a[i, i], y[i]); return a; }
        public static T[,] LetSubDiag<T>(this T[,] a, T[] y) where T : unmanaged { for (int i = CheckSquareMatrix(a, y); --i >= 0;) Op.LetSub(ref a[i, i], y[i]); return a; }
        public static T[,] LetSubrDiag<T>(this T[,] a, T[] y) where T : unmanaged => a.LetNeg().LetAddDiag(y);
        public static T[,] AddDiag<T>(this T[,] x, T[] y) where T : unmanaged => x.CloneX().LetAddDiag(y);
        public static T[,] SubDiag<T>(this T[,] x, T[] y) where T : unmanaged => x.CloneX().LetSubDiag(y);
        public static T[,] DiagAdd<T>(this T[] y, T[,] x) where T : unmanaged => x.CloneX().LetAddDiag(y);
        public static T[,] DiagSub<T>(this T[] y, T[,] x) where T : unmanaged => x.Neg().LetAddDiag(y);

        public static unsafe T[,] LetMulDiag<T>(this T[,] a, T[] y) where T : unmanaged
        {
            int n = a.GetLength(1);
            if (n != y.Length) ThrowException.SizeMismatch();
            fixed (T* p = a, q = y)
                for (int i = a.GetLength(0); --i >= 0;) Us.LetMul(&p[n * i], q, n);
            return a;
        }
        public static unsafe T[,] LetDivDiag<T>(this T[,] a, T[] y) where T : unmanaged
        {
            int n = a.GetLength(1);
            if (n != y.Length) ThrowException.SizeMismatch();
            fixed (T* p = a, q = y)
                for (int i = a.GetLength(0); --i >= 0;) Us.LetDiv(&p[n * i], q, n);
            return a;
        }
        public static unsafe T[,] LetDivrDiag<T>(this T[,] a, T[] y) where T : unmanaged
        {
            int n = a.GetLength(1);
            if (n != y.Length) ThrowException.SizeMismatch();
            fixed (T* p = a, q = y)
                for (int i = a.GetLength(0); --i >= 0;) Us.LetDivr(&p[n * i], q, n);
            return a;
        }
        public static T[,] MulDiag<T>(this T[,] x, T[] y) where T : unmanaged => x.CloneX().LetMulDiag(y);
        public static T[,] DivDiag<T>(this T[,] x, T[] y) where T : unmanaged => x.CloneX().LetDivDiag(y);
        public static T[,] DivrDiag<T>(this T[,] x, T[] y) where T : unmanaged => x.CloneX().LetDivrDiag(y);

        public static unsafe T[,] LetDiagMul<T>(this T[,] a, T[] y) where T : unmanaged
        {
            int n = a.GetLength(1);
            if (a.GetLength(0) != y.Length) ThrowException.SizeMismatch();
            fixed (T* p = a)
                for (int i = y.Length; --i >= 0;) Us.LetMul(&p[n * i], y[i], n);
            return a;
        }
        public static unsafe T[,] LetDiagDiv<T>(this T[,] a, T[] y) where T : unmanaged
        {
            int n = a.GetLength(1);
            if (a.GetLength(0) != y.Length) ThrowException.SizeMismatch();
            fixed (T* p = a)
                for (int i = y.Length; --i >= 0;) Us.LetDiv(&p[n * i], y[i], n);
            return a;
        }
        public static unsafe T[,] LetDiagDivr<T>(this T[,] a, T[] y) where T : unmanaged
        {
            int n = a.GetLength(1);
            if (a.GetLength(0) != y.Length) ThrowException.SizeMismatch();
            fixed (T* p = a)
                for (int i = y.Length; --i >= 0;) Us.LetDivr(&p[n * i], y[i], n);
            return a;
        }
        public static T[,] DiagMul<T>(this T[] y, T[,] x) where T : unmanaged => x.CloneX().LetDiagMul(y);
        public static T[,] DiagDiv<T>(this T[] y, T[,] x) where T : unmanaged => x.CloneX().LetDiagDiv(y);
        public static T[,] DiagDivr<T>(this T[] y, T[,] x) where T : unmanaged => x.CloneX().LetDiagDivr(y);
        #endregion

        #region Complex<T> matrix
        public static unsafe Complex<T>[] Multiply<T>(this T[,] x, Complex<T>[] y) where T : unmanaged
        {
            int o = y.Length;
            if (x.GetLength(1) != o) ThrowException.SizeMismatch();
            var result = new Complex<T>[x.GetLength(0)];
            fixed (Complex<T>* r = result, q = y) fixed (T* p = x)
                for (int i = result.Length; --i >= 0;)
                    r[i] = Us.SumMul(q, &p[o * i], o);
            return result;
        }
        public static unsafe Complex<T>[] Multiply<T>(this Complex<T>[] y, T[,] x) where T : unmanaged
        {
            if (y.Length != x.GetLength(0)) ThrowException.SizeMismatch();
            int n = x.GetLength(1);
            var a = new Complex<T>[n];
            fixed (Complex<T>* r = a, q = y) fixed (T* p = x)
                for (int i = y.Length; --i >= 0;)
                    Us.LetAddMulB(r, &p[n * i], q[i], n);
            return a;
        }
        #endregion

        #region double matrix
        public static double TrMultiply(this double[,] x, double[,] y)
        {
            int n = x.GetLength(0);
            int o = x.GetLength(1);
            if (o != y.GetLength(0) || n != y.GetLength(1)) ThrowException.SizeMismatch();
            double a = 0;
            for (int i = n; --i >= 0;)
                for (int j = o; --j >= 0;)
                    a += x[i, j] * y[j, i];
            return a;
        }
        public static double TrMultiply(double[,] x, double[,] y, double[,] z)
        {
            if (x.GetLength(1) != y.GetLength(0) || y.GetLength(1) != z.GetLength(0) || x.GetLength(0) != z.GetLength(1)) ThrowException.SizeMismatch();
            double a = 0;
            for (int i = y.GetLength(0); --i >= 0;)
                for (int j = y.GetLength(1); --j >= 0;)
                {
                    double b = 0;
                    for (int k = x.GetLength(0); --k >= 0;)
                        b += x[k, i] * z[j, k];
                    a += b * y[i, j];
                }
            return a;
        }

        public static unsafe float[,] LetAddMulVVS(this float[,] x, float[] y, float z = 1)
        {
            int n = y.Length;
            if (x.GetLength(0) != n || x.GetLength(1) != n) ThrowException.SizeMismatch();
            fixed (float* r = x, p = y)
                for (int i = n; --i >= 0;)
                    Us.LetAddMul(&r[n * i], p, p[i] * z, n);
            return x;
        }
        public static unsafe double[,] LetAddMulVVS(this double[,] x, double[] y, double z = 1)
        {
            int n = y.Length;
            if (x.GetLength(0) != n || x.GetLength(1) != n) ThrowException.SizeMismatch();
            fixed (double* r = x, p = y)
                for (int i = n; --i >= 0;)
                    Us.LetAddMul(&r[n * i], p, p[i] * z, n);
            return x;
        }

        public static void LetSymmetrical(this double[,] x)
        {
            if (x.GetLength(0) != x.GetLength(1)) ThrowException.SizeMismatch();
            for (int i = x.GetLength(0); --i >= 0;)
                for (int j = i; --j >= 0;)
                {
                    var a = (x[i, j] + x[j, i]) / 2;
                    x[i, j] = a;
                    x[j, i] = a;
                }
        }

        // matrix inverse and determinant
        static double Det1by1(double[,] x)
        {
            return x[0, 0];
        }
        static double[,] Inverse1by1(double[,] x)
        {
            var det = x[0, 0];
            if (det == 0) Warning.Singular();
            return new double[1, 1]{
                {1.0 / det}
            };
        }
        static double Det2by2(double[,] x)
        {
            double a = x[0, 0], b = x[0, 1];
            double c = x[1, 0], d = x[1, 1];
            return a * d - b * c;
        }
        static double[,] Inverse2by2(double[,] x)
        {
            double a = x[0, 0], b = x[0, 1];
            double c = x[1, 0], d = x[1, 1];
            var det = a * d - b * c;
            if (det == 0) Warning.Singular();
            return new double[2, 2]{
                {d / det, -b / det},
                {-c / det, a / det}
            };
        }
        static double Det3by3(double[,] x)
        {
            double a = x[0, 0], b = x[0, 1], c = x[0, 2];
            double d = x[1, 0], e = x[1, 1], f = x[1, 2];
            double g = x[2, 0], h = x[2, 1], i = x[2, 2];
            return a * (e * i - f * h) + b * (f * g - d * i) + c * (d * h - e * g);
        }
        static double[,] Inverse3by3(double[,] x)
        {
            double a = x[0, 0], b = x[0, 1], c = x[0, 2];
            double d = x[1, 0], e = x[1, 1], f = x[1, 2];
            double g = x[2, 0], h = x[2, 1], i = x[2, 2];
            double eifh = e * i - f * h, fgdi = f * g - d * i, dheg = d * h - e * g;
            var det = a * eifh + b * fgdi + c * dheg;
            if (det == 0) Warning.Singular();
            return new double[3, 3]{
                { eifh / det, (c * h - b * i) / det, (b * f - c * e) / det},
                { fgdi / det, (a * i - c * g) / det, (c * d - a * f) / det},
                { dheg / det, (b * g - a * h) / det, (a * e - b * d) / det}
            };
        }
        static int[] LUDecomposition(double[][] matrix)
        {
            int n = matrix.GetLength(0);
            var pivot = new int[n];
            var vv = new double[n];
            for (int i = 0; i < n; i++)
            {
                //var max = Mt.Max(n, j => Math.Abs(matrix[i][j]));
                var max = matrix[i].Max(v => Math.Abs(v));
                if (max == 0) Warning.Singular();
                vv[i] = 1 / max;
            }

            bool warning = true;
            for (int k = 0; k < n; k++)
            {
                int p = k + Ex.FromTo(k, n).Select(i => vv[i] * Math.Abs(matrix[i][k])).MaxIndex();
                if (p != k)
                {
                    Ex.Swap(ref matrix[p], ref matrix[k]);
                    vv[p] = vv[k];
                }
                pivot[k] = p;
                var mk = matrix[k];
                if (mk[k] == 0) { mk[k] = 1e-40; if (warning) { warning = false; Warning.Singular(); } }
                for (int i = k; ++i < n;)  //順不同並列可
                {
                    var mi = matrix[i];
                    var temp = mi[k] /= mk[k];
                    for (int j = k; ++j < n;)
                        mi[j] -= temp * mk[j];
                }
            }
            return pivot;
        }
        public static (double[][], int[]) LUDecomposition(this double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1)) ThrowException.SizeMismatch();
            double[][] LU = New.Array(n, i => New.Array(n, j => matrix[i, j]));
            int[] pivot = LUDecomposition(LU);
            return (LU, pivot);
        }
        public static double[] Divide(this double[] vector, (double[][], int[]) LUinfo)
        {
            double[][] LU = LUinfo.Item1;
            int[] pivot = LUinfo.Item2;
            int n = vector.Length;
            if (n != pivot.Length) ThrowException.SizeMismatch();

            var r = vector.CloneX();
            for (int ii = 0, i = 0; i < n; i++)
            {
                var sum = r[pivot[i]];
                r[pivot[i]] = r[i];
                if (ii != 0)
                    for (int j = ii - 1; j < i; j++) sum -= LU[i][j] * r[j];
                else if (sum != 0)
                    ii = i + 1;
                r[i] = sum;
            }
            for (int i = n - 1; i >= 0; i--)
            {
                var sum = r[i];
                for (int j = i + 1; j < n; j++) sum -= LU[i][j] * r[j];
                r[i] = sum / LU[i][i];
            }
            return r;
        }

        public static double[] Divide(this double[] vector, double[,] matrix) => vector.Divide(LUDecomposition(matrix));
        public static ComplexD[] Divide(this ComplexD[] vector, double[,] matrix)
        {
            var LU = LUDecomposition(matrix);
            return Mt.ToComplex(vector.Real().Divide(LU), vector.Imag().Divide(LU));
        }
        public static double[] DivideGauss(this double[] vector, double[,] matrix)
        {
            var A = matrix.CloneX();
            var b = vector.CloneX();
            int N = b.Length;
            //前進消去
            for (int k = 0; k < N - 1; k++)
            {
                //部分ピボット選択
                int m = k;
                for (int i = k + 1; i < N; i++)
                    if (Math.Abs(A[i, k]) > Math.Abs(A[m, k])) m = i;
                if (m != k)
                {
                    for (int j = k; j < N; j++)
                        Ex.Swap(ref A[k, j], ref A[m, j]);
                    Ex.Swap(ref b[k], ref b[m]);
                }
                //第k列消去
                for (int i = k + 1; i < N; i++)
                {
                    double r = A[i, k] / A[k, k];
                    for (int j = k; j < N; j++)
                        A[i, j] -= A[k, j] * r;
                    b[i] -= b[k] * r;
                }
            }
            //後退代入
            for (int i = N - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < N; j++)
                    b[i] -= A[i, j] * b[j];
                b[i] /= A[i, i];
            }
            return b;
        }
        public static ComplexD[] DivideGauss(this ComplexD[] vector, double[,] matrix)
        {
            return Mt.ToComplex(vector.Real().DivideGauss(matrix), vector.Imag().DivideGauss(matrix));
        }

        public static double[,] Inverse(this (double[][], int[]) LUinfo)
        {
            double[][] LU = LUinfo.Item1;
            int[] pivot = LUinfo.Item2;
            int n = pivot.Length;
            int[] index = New.Array(n, i => i);
            for (int i = 0; i < n; i++)
                Ex.Swap(ref index[i], ref index[pivot[i]]);

            var result = new double[n, n];
            var vec = new double[n];
            for (int i = n; --i >= 0;)  //順不同並列可
            {
                vec.Clear();
                vec[i] = 1.0;
                for (int j = i; ++j < n;)
                {
                    double sum = 0;
                    for (int k = i; k < j; k++)
                        sum -= LU[j][k] * vec[k];
                    vec[j] = sum;
                }
                for (int j = n; --j >= 0;)
                {
                    double sum = vec[j];
                    for (int k = j; ++k < n;)
                        sum -= LU[j][k] * vec[k];
                    vec[j] = sum / LU[j][j];
                }
                int idx = index[i];
                for (int j = n; --j >= 0;)
                    result[idx, j] = vec[j];
            }
            return result;
        }
        public static double[,] Inverse(this double[,] x)
        {
            int n = CheckSquareMatrix(x);
            if (n == 1) return Inverse1by1(x);
            if (n == 2) return Inverse2by2(x);
            if (n == 3) return Inverse3by3(x);
            return LUDecomposition(x).Inverse();
        }

        public static double Det(this (double[][], int[]) LUinfo)
        {
            double[][] LU = LUinfo.Item1;
            int[] pivot = LUinfo.Item2;
            int n = pivot.Length;
            int swap = Ex.Range(n).Count(i => pivot[i] != i);
            return (swap % 2 == 0 ? 1 : -1) * Product(n, i => LU[i][i]);
        }

        public static double Det(this double[,] x)
        {
            int n = CheckSquareMatrix(x);
            if (n == 1) return Det1by1(x);
            if (n == 2) return Det2by2(x);
            if (n == 3) return Det3by3(x);
            return LUDecomposition(x).Det();
        }
        public static double LogDet(this (double[][], int[]) LUinfo)
        {
            double[][] LU = LUinfo.Item1;
            int[] pivot = LUinfo.Item2;
            int n = pivot.Length;
            int swap = Ex.Range(n).Count(i => pivot[i] != i);
            int negc = Ex.Range(n).Count(i => LU[i][i] < 0);
            double sign = (swap + negc) % 2 == 0 ? 1.0 : -1.0;
            if (sign == -1) ThrowException.Argument("determinant <= 0");
            return SumFwrd(n, i => Math.Log(Math.Abs(LU[i][i])));
        }
        public static double LogDet(this double[,] x)
        {
            int n = CheckSquareMatrix(x);
            if (n <= 3) return Math.Log(Det(x));
            return LUDecomposition(x).LogDet();
        }

        public static double[,] PseudoInverse(this double[,] x)
        {
            var xT = x.T();
            if (x.GetLength(0) >= x.GetLength(1))
                return xT.Multiply(x).Inverse().Multiply(xT);
            else
                return xT.Multiply(x.Multiply(xT).Inverse());
        }

        // Inverse of symmetric positive definite matrix
        static double[,] CholeskyDecomposition(this double[,] matrix)
        {
            int n = matrix.GetLength(0);
            var L = new double[n, n];
            //対角と下三角部分を計算
            for (int j = 0; j < n; j++)
            {
                for (int i = j; i < n; i++)
                {
                    double sum = matrix[j, i];
                    for (int k = j; --k >= 0;)
                        sum -= L[j, k] * L[i, k];
                    if (j == i && sum <= 0.0) Warning.WriteLine("not a positive definite matrix");
                    L[i, j] = (j == i) ? Math.Sqrt(sum) : sum / L[j, j];
                }
            }
            return L;
        }
        public static double[,] InverseSymmetricPositiveDefinite(this double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1)) ThrowException.SizeMismatch();
            double[,] L = CholeskyDecomposition(matrix);
            //対角と上三角部分を計算
            for (int j = 0; j < n; j++)
            {
                L[j, j] = 1 / L[j, j];
                for (int i = j; --i >= 0;)
                {
                    double sum = 0;
                    for (int k = j; --k >= i;)
                        sum -= L[j, k] * L[i, k];
                    L[i, j] = sum * L[j, j];
                }
            }
            for (int j = n; --j >= 0;)
            {
                for (int i = 0; i <= j; i++)
                {
                    double sum = L[i, j];
                    for (int k = j; ++k < n;)
                        sum -= L[k, j] * L[i, k];
                    L[i, j] = sum * L[j, j];
                }
            }
            //下三角部分を上三角部分からコピー
            for (int i = n; --i >= 0;)
                for (int j = i; --j >= 0;)
                    L[i, j] = L[j, i];
            return L;
        }

        static void QR_(double[] vector0, double[] vector1, double c, double s)
        {
            for (int i = vector0.Length; --i >= 0;)
            {
                var x = vector0[i];
                var y = vector1[i];
                vector0[i] = x * c + y * s;
                vector1[i] = y * c - x * s;
            }
        }

        //M: symmetric matrix
        //Mを転置させた計算の方が速いため改変
        static double[] Householder(double[][] M, double[] D)
        {
            int n = M.Length;
            var E = new double[n];
            for (int i = n; --i >= 2;)
            {
                var Mi = new double[i];
                for (int l = i; --l >= 0;) Mi[l] = M[l][i];
                double scale = Mi.DNorm1();
                if (scale == 0) continue;
                for (int l = i; --l >= 0;) Mi[l] /= scale;

                double Di;
                {
                    var f = Mi.DNorm2Sq();
                    var g = Math.Sqrt(f);
                    if (Mi[i - 1] < 0) g *= -1;
                    E[i] = -g * scale;
                    D[i] = Di = f + g * Mi[i - 1];
                    Mi[i - 1] += g;
                }

                var hh = 0.0;
                for (int j = 0; j < i; j++)
                {
                    M[i][j] = Mi[j] / Di;
                    {
                        var a = 0.0;
                        for (int l = i; --l > j;) a += M[j][l] * Mi[l];
                        for (int l = j + 1; --l >= 0;) a += M[l][j] * Mi[l];
                        E[j] = a / Di;
                    }
                    hh += E[j] * Mi[j];
                }
                hh /= 2 * Di;
                for (int j = 0; j < i; j++)
                {
                    E[j] -= Mi[j] * hh;
                    for (int k = j + 1; --k >= 0;) M[k][j] -= Mi[j] * E[k] + Mi[k] * E[j];
                }
                for (int l = i; --l >= 0;) M[l][i] = Mi[l];
            }
            E[1] = M[0][1];

            for (int i = 0; i < n; i++)
            {
                if (D[i] != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        var a = 0.0;
                        for (int l = i; --l >= 0;) a += M[j][l] * M[l][i];
                        for (int l = i; --l >= 0;) M[j][l] -= M[i][l] * a;
                    }
                }
                D[i] = M[i][i];
                M[i][i] = 1;
                for (int l = i; --l >= 0;) M[i][l] = 0;
                for (int l = i; --l >= 0;) M[l][i] = 0;
            }
            return E;
        }
        static void QLMethod(double[][] M, double[] D, double[] E)
        {
            static double pythag1(double x) => Math.Abs(x) < 1 ? Math.Sqrt(1 + x * x) * (x < 0 ? -1 : 1) : Math.Sqrt(1 + (1 / x) * (1 / x)) * x;
            int N = M.Length;
            for (int l = 0; l < N; l++)
            {
                while (true)
                {
                    int m;
                    for (m = l; m < N - 1; m++)
                        if (IsTooSmall(E[m], Math.Abs(D[m]) + Math.Abs(D[m + 1])) || Math.Abs(E[m]) < DoubleEpsilon) break;
                    if (m == l) break;

                    var g = (D[l + 1] - D[l]) / E[l] * 0.5;
                    g += pythag1(g);
                    g = E[l] / g + D[m] - D[l];
                    double s = 1, c = 1, p = 0;
                    int i;
                    for (i = m; --i >= l;)
                    {
                        double b = E[i] * c;
                        {
                            var f = E[i] * s;
                            var py = Norm2(f, g);
                            E[i + 1] = py;
                            if (py == 0) break;
                            s = f / py;
                            c = g / py;
                        }
                        {
                            var q = D[i + 1] - p;
                            var r = (D[i] - q) * s + b * c * 2;
                            p = r * s;
                            D[i + 1] = q + p;
                            g = r * c - b;
                        }
                        QR_(M[i], M[i + 1], c, -s);
                    }
                    if (i < l) E[l] = g;
                    D[i + 1] -= p;
                    E[m] = 0;
                }
            }
        }
        //matrix: symmetric matrix
        //matrix => V * diag[D] * V^T
        //固有値の配列と固有ベクトルの配列を返す
        public static (double[] values, double[][] vectors) EigenValueDecomposition(this double[,] matrix)
        {
            int N = matrix.GetLength(0);
            if (N != matrix.GetLength(1)) ThrowException.SizeMismatch();
            var D = new double[N];
            var V = New.Array(N, i => New.Array(N, j => matrix[i, j]));

            double[] E = Householder(V, D);
            for (int i = 0; i < N - 1; i++) E[i] = E[i + 1];
            E[N - 1] = 0;
            QLMethod(V, D, E);

            int[] order = D.SortIndex().LetReverse();
            D.LetSortAsIndex(order);
            V.LetSortAsIndex(order);
            for (int i = N; --i >= 0;)
                if (V[i].Count(a => a < 0) > N / 2) V[i].LetNeg();
            return (D, V);
        }
        //matrix => V * diag[D] * V^T
        //V, Dを返す
        public static (double[,] matrix, double[] values) EigenValueDecompositionM(this double[,] matrix)
        {
            var (values, vectors) = EigenValueDecomposition(matrix);
            return (New.Array(vectors[0].Length, vectors.Length, (i, j) => vectors[j][i]), values);
        }

        //matrix -> U diag[W] V^T
        //U, V: 縦ベクトルの配列を返す
        public static (double[][] vectorsLt, double[] values, double[][] vectorsRt) SingularValueDecomposition(this double[,] matrix)
        {
            int XN = matrix.GetLength(1);
            int YN = matrix.GetLength(0);
            int ZN = Math.Min(YN, XN);
            var U = New.Array(XN, i => New.Array(YN, j => matrix[j, i]));
            var V = New.Array(XN, i => new double[XN]);
            var W = new double[XN];
            var R = new double[XN];
            double anorm = 0;
            for (int i = 0; i < XN; i++)
            {
                var I = Ex.FromTo(i, YN);
                if (i < YN)
                {
                    double scale = I.Sum(k => Math.Abs(U[i][k]));
                    if (scale != 0)
                    {
                        foreach (var k in I) U[i][k] /= scale;
                        var s = I.Sum(k => U[i][k].Sq());
                        var f = U[i][i];
                        var g = Math.Sqrt(s);
                        if (f > 0) g *= -1;
                        var h = f * g - s;
                        U[i][i] = f - g;
                        for (int j = i; ++j < XN;)
                        {
                            var coef = 0.0;
                            for (int k = i; k < YN; k++) coef += U[j][k] * U[i][k];
                            coef /= h;
                            for (int k = i; k < YN; k++) U[j][k] += coef * U[i][k];
                        }
                        foreach (var k in I) U[i][k] *= scale;
                        W[i] = scale * g;
                    }
                }
                int l = i + 1;
                var L = Ex.FromTo(l, XN);
                if (i < YN && i < XN - 1)
                {
                    double scale = L.Sum(k => Math.Abs(U[k][i]));
                    if (scale != 0.0)
                    {
                        foreach (var k in L) U[k][i] /= scale;
                        var s = L.Sum(k => U[k][i].Sq());
                        var f = U[l][i];
                        var g = -Math.Sign(f) * Math.Sqrt(s);
                        var h = f * g - s;
                        U[l][i] = f - g;
                        foreach (var k in L) R[k] = U[k][i] / h;
                        for (int j = l; j < YN; j++)
                        {
                            double coef = 0;
                            for (int k = l; k < XN; k++) coef += U[k][j] * U[k][i];
                            for (int k = l; k < XN; k++) U[k][j] += coef * R[k];
                        }
                        foreach (var k in L) U[k][i] *= scale;
                        R[l] = scale * g;
                    }
                }
                anorm.LetMax(Math.Abs(W[i]) + Math.Abs(R[i]));
                if (i > YN) R[i] = 0.0;
            }

            for (int i = XN; --i >= 0;)
            {
                if (i < XN - 1)
                {
                    int l = i + 1;
                    var L = Ex.FromTo(l, XN);
                    var g = R[l];
                    if (g != 0.0)
                    {
                        if (U[l][i] != 0.0)
                        {
                            var coef = 1 / U[l][i] / g;
                            foreach (var k in L) V[i][k] = U[k][i] * coef;
                        }
                        for (int j = l; j < XN; j++)
                        {
                            var coef = 0.0;
                            for (int k = l; k < XN; k++) coef += V[j][k] * U[k][i];
                            for (int k = l; k < XN; k++) V[j][k] += coef * V[i][k];
                        }
                    }
                    foreach (var k in L) V[i][k] = 0;
                    foreach (var k in L) V[k][i] = 0;
                }
                V[i][i] = 1.0;
            }

            for (int i = ZN; --i >= 0;)
            {
                int l = i + 1;
                var I = Ex.FromTo(i, YN);
                foreach (var k in Ex.FromTo(l, XN)) U[k][i] = 0;
                var g = W[i];
                if (g != 0.0)
                {
                    g = 1 / g;
                    for (int j = l; j < XN; j++)
                    {
                        var coef = 0.0;
                        for (int k = l; k < YN; k++) coef += U[j][k] * U[i][k];
                        coef = coef / U[i][i] * g;
                        for (int k = i; k < YN; k++) U[j][k] += coef * U[i][k];  //
                    }
                    foreach (var k in I) U[i][k] *= g;
                }
                else
                    foreach (var k in I) U[i][k] = 0;
                U[i][i] += 1;
            }

            for (int k = XN; --k >= 0;)
            {
                for (int iterations = 30; ; iterations--)
                {
                    int l;
                    for (l = k; l > 0; l--)
                    {  // R[0]==0;
                        if (IsTooSmall(R[l], anorm)) break;
                        if (IsTooSmall(W[l - 1], anorm))
                        {
                            double c = 0, s = 1;
                            for (int i = l; i <= k; i++)
                            {
                                var f = s * R[i];
                                R[i] *= c;
                                if (IsTooSmall(f, anorm)) break;
                                var g = W[i];
                                var py = Norm2(f, g);
                                W[i] = py;
                                c = g / py;
                                s = -f / py;
                                QR_(U[l - 1], U[i], c, s);
                            }
                            break;
                        }
                    }

                    if (l == k)
                    {
                        if (W[k] < 0) { W[k] *= -1; Mt.LetMul(V[k], -1); }
                        break;
                    }
                    if (iterations == 0) { Warning.TooManyIterations(); break; }

                    {
                        var f = 0.0;
                        var x = W[l];
                        {
                            var y = W[k - 1];
                            var z = W[k];
                            var g = R[k - 1];
                            var h = R[k];
                            f = ((y - z) * (y + z) + (g - h) * (g + h)) / (2 * h * y);
                            var py = f * (1 + Math.Sqrt(1 + 1 / (f * f)));
                            f = ((x - z) * (x + z) + h * (y / py - h)) / x;
                        }
                        double c = 1, s = 1;
                        for (int j = l; j < k; j++)
                        {
                            int i = j + 1;
                            var g = R[i] * c;
                            var h = R[i] * s;
                            {
                                var py = Norm2(f, h);
                                R[j] = py;
                                c = f / py;
                                s = h / py;
                            }
                            f = x * c + g * s;
                            g = g * c - x * s;

                            var y = W[i] * c;
                            h = W[i] * s;
                            QR_(V[j], V[i], c, s);
                            {
                                var py = Norm2(f, h);
                                W[j] = py;
                                if (py != 0)
                                {
                                    c = f / py;
                                    s = h / py;
                                }
                            }
                            f = g * c + y * s;
                            x = y * c - g * s;
                            QR_(U[j], U[i], c, s);
                        }
                        R[l] = 0;
                        R[k] = f;
                        W[k] = x;
                    }
                }
            }
            {
                var order = W.SortIndex().LetReverse();
                W.LetSortAsIndex(order);
                U.LetSortAsIndex(order);
                V.LetSortAsIndex(order);

                for (int k = 0; k < XN; k++)
                {
                    int s = U[k].Count(a => a < 0) + V[k].Count(a => a < 0);
                    if (s > (YN + XN) / 2)
                    {
                        U[k].LetNeg();
                        V[k].LetNeg();
                    }
                }
            }
            return (U, W, V);
        }
        //matrix -> U diag[W] V^T
        //U, W, Vを返す
        public static (double[,] matrixLt, double[] values, double[,] matrixRt) SingularValueDecompositionM(double[,] matrix)
        {
            var (vectorsLt, values, vectorsRt) = SingularValueDecomposition(matrix);
            return (New.Array(vectorsLt[0].Length, vectorsLt.Length, (i, j) => vectorsLt[j][i]), values, New.Array(vectorsRt.Length, vectorsRt.Length, (i, j) => vectorsRt[j][i]));
        }

        // Rotation Matrix
        public static Double2 Rotate(Double2 vector, double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            return new Double2(cos * vector.v0 - sin * vector.v1, sin * vector.v0 + cos * vector.v1);
        }
        // 右手座標系ならvectorをaxisを軸にして右ねじの回転方向に回転させる．axisの長さ=1
        public static Double3 Rotate(Double3 vector, Double3 axis, double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            var cos1 = 1 - cos;
            var x = axis.v0;
            var y = axis.v1;
            var z = axis.v2;
            return new Double3(
                (cos1 * x * x + cos) * vector.v0 + (cos1 * x * y - sin * z) * vector.v1 + (cos1 * z * x + sin * y) * vector.v2,
                (cos1 * x * y + sin * z) * vector.v0 + (cos1 * y * y + cos) * vector.v1 + (cos1 * y * z - sin * x) * vector.v2,
                (cos1 * z * x - sin * y) * vector.v0 + (cos1 * y * z + sin * x) * vector.v1 + (cos1 * z * z + cos) * vector.v2
            );
        }
        public static double[,] RotationMatrix(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            return new double[2, 2]{
                { cos, -sin },
                { sin, cos }
            };
        }
        public static double[,] RotationMatrix(Double3 axis, double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            var cos1 = 1 - cos;
            var x = axis.v0;
            var y = axis.v1;
            var z = axis.v2;
            return new double[3, 3] {
                { cos1 * x * x + cos, cos1 * x * y - sin * z, cos1 * z * x + sin * y },
                { cos1 * x * y + sin * z, cos1 * y * y + cos, cos1 * y * z - sin * x },
                { cos1 * z * x - sin * y, cos1 * y * z + sin * x, cos1 * z * z + cos },
            };
        }

        public static Single2 Rotate(Single2 vector, float angle)
        {
            var cos = MathF.Cos(angle);
            var sin = MathF.Sin(angle);
            return new Single2(cos * vector.v0 - sin * vector.v1, sin * vector.v0 + cos * vector.v1);
        }
        // 右手座標系ならvectorをaxisを軸にして右ねじの回転方向に回転させる．axisの長さ=1
        public static Single3 Rotate(Single3 vector, Single3 axis, float angle)
        {
            var cos = MathF.Cos(angle);
            var sin = MathF.Sin(angle);
            var cos1 = 1 - cos;
            var x = axis.v0;
            var y = axis.v1;
            var z = axis.v2;
            return new Single3(
                (cos1 * x * x + cos) * vector.v0 + (cos1 * x * y - sin * z) * vector.v1 + (cos1 * z * x + sin * y) * vector.v2,
                (cos1 * x * y + sin * z) * vector.v0 + (cos1 * y * y + cos) * vector.v1 + (cos1 * y * z - sin * x) * vector.v2,
                (cos1 * z * x - sin * y) * vector.v0 + (cos1 * y * z + sin * x) * vector.v1 + (cos1 * z * z + cos) * vector.v2
            );
        }
        public static float[,] RotationMatrix(float angle)
        {
            var cos = MathF.Cos(angle);
            var sin = MathF.Sin(angle);
            return new float[2, 2]{
                { cos, -sin },
                { sin, cos }
            };
        }
        public static float[,] RotationMatrix(Single3 axis, float angle)
        {
            var cos = MathF.Cos(angle);
            var sin = MathF.Sin(angle);
            var cos1 = 1 - cos;
            var x = axis.v0;
            var y = axis.v1;
            var z = axis.v2;
            return new float[3, 3] {
                { cos1 * x * x + cos, cos1 * x * y - sin * z, cos1 * z * x + sin * y },
                { cos1 * x * y + sin * z, cos1 * y * y + cos, cos1 * y * z - sin * x },
                { cos1 * z * x - sin * y, cos1 * y * z + sin * x, cos1 * z * z + cos },
            };
        }
        #endregion

        #region complex matrix
        public static void LetSymmetrical(this ComplexD[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1)) ThrowException.SizeMismatch();
            for (int i = matrix.GetLength(0); --i >= 0;)
                for (int j = i; --j >= 0;)
                {
                    var a = (matrix[i, j].Re + matrix[j, i].Re) * 0.5;
                    var b = (matrix[i, j].Im - matrix[j, i].Im) * 0.5;
                    matrix[i, j] = new ComplexD(a, +b);
                    matrix[j, i] = new ComplexD(a, -b);
                }
        }

        static int[] LUDecomposition(ComplexD[][] matrix)
        {
            var n = matrix.GetLength(0);
            var pivot = new int[n];
            var vv = new ComplexD[n];
            for (int i = 0; i < n; i++)
            {
                double max = Ex.Max(n, j => matrix[i][j].Abs());
                if (max == 0) Warning.Singular();
                vv[i] = (ComplexD)(1 / max);
            }

            for (int k = 0; k < n; k++)
            {
                int p = k + Ex.FromTo(k, n).Select(i => (vv[i] * matrix[i][k]).Abs()).MaxIndex();
                if (p != k)
                {
                    Ex.Swap(ref matrix[p], ref matrix[k]);
                    vv[p] = vv[k];
                }
                pivot[k] = p;
                var mk = matrix[k];
                if (mk[k] == 0.0) { Warning.Singular(); mk[k] = (ComplexD)1e-40; }
                for (int i = k; ++i < n;)  //順不同並列可
                {
                    var mi = matrix[i];
                    var temp = mi[k] /= mk[k];
                    for (int j = k; ++j < n;)
                        mi[j] -= temp * mk[j];
                }
            }
            return pivot;
        }
        public static ComplexD[,] Inverse(this ComplexD[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1)) ThrowException.SizeMismatch();
            //if (n == 1) return Inverse1by1(matrix);
            //if (n == 2) return Inverse2by2(matrix);
            //if (n == 3) return Inverse3by3(matrix);

            ComplexD[][] LU = New.Array(n, i => New.Array(n, j => matrix[j, i]));
            int[] pivot = LUDecomposition(LU);
            int[] index = New.Array(n, i => i);
            for (int i = 0; i < n; i++)
                Ex.Swap(ref index[i], ref index[pivot[i]]);

            var result = new ComplexD[n, n];
            var vec = new ComplexD[n];
            for (int i = n; --i >= 0;)  //順不同並列可
            {
                vec.Clear();
                vec[i] = ComplexD.One;
                for (int j = i; ++j < n;)
                {
                    var sum = ComplexD.Zero;
                    for (int k = i; k < j; k++)
                        sum -= LU[j][k] * vec[k];
                    vec[j] = sum;
                }
                for (int j = n; --j >= 0;)
                {
                    var sum = vec[j];
                    for (int k = j; ++k < n;)
                        sum -= LU[j][k] * vec[k];
                    vec[j] = sum / LU[j][j];
                }
                int idx = index[i];
                for (int j = n; --j >= 0;)
                    result[idx, j] = vec[j];
            }
            return result;
        }

        public static ComplexD[,] PseudoInverse(this ComplexD[,] matrix)
        {
            var transpose = matrix.H();
            if (matrix.GetLength(0) >= matrix.GetLength(1))
                return transpose.Multiply(matrix).Inverse().Multiply(transpose);
            else
                return transpose.Multiply(matrix.Multiply(transpose).Inverse());
        }
        #endregion

        #region linear approximation
        public static (double A, double B) LinearApproximationY(IEnumerable<double> X, IEnumerable<double> Y)
        {
            var (a, c) = AverageCovariance(X, Y);
            var A = c.v1 / c.v0;
            var B = a.Y - A * a.X;
            return (A, B);
        }
        public static (double A, Double2 C) LinearApproximationXY(IEnumerable<double> X, IEnumerable<double> Y)
        {
            var (a, c) = AverageCovariance(X, Y);
            return (Math.Atan2(c.v1 * 2, c.v0 - c.v2) / 2, a);
        }
        public static (Double2 average, Double3 covariance) AverageCovariance(IEnumerable<double> X, IEnumerable<double> Y)
        {
            var count = 0;
            var avg = new Double2(X.Average(), Y.Average());
            var cov = default(Double3);
            foreach (var d in X.Select(x => x - avg.X).Zip(Y.Select(y => y - avg.Y), (x, y) => new Double2(x, y)))
            {
                count++;
                cov.v0 += d.X.Sq();  //x^2
                cov.v1 += d.X * d.Y; //x*y
                cov.v2 += d.Y.Sq();  //y^2
            }
            return (avg, cov / count);
        }
        #endregion
        #endregion
    }

    // Numerical functions
    public static partial class Nm
    {
        #region spline
        public class Spline3
        {
            public readonly double[] X;
            public readonly double[] Y;
            readonly double[] Z;
            public Spline3(IEnumerable<double>? dataX, IEnumerable<double> dataY)
            {
                Y = dataY.ToArray();
                int N = Y.Length;
                if (N == 0) ThrowException.Argument($"size is 0: {nameof(dataY)}");
                if (N == 1) Y = new double[] { Y[0], Y[0], Y[0] };
                if (N == 2) Y = new double[] { Y[0], (Y[0] + Y[1]) / 2, Y[1] };

                if (dataX != null)
                {
                    X = dataX.ToArray();
                    if (X.Length == 1) X = new double[] { X[0], X[0], X[0] };
                    if (X.Length == 2) X = new double[] { X[0], (X[0] + X[1]) / 2, X[1] };
                }
                //if (dataX is null || X.Length == 0)
                else
                {
                    X = new double[N];
                    X[0] = 0;
                    for (int i = 1; i < N; i++) X[i] = X[i - 1] + Math.Max(Math.Abs(Y[i] - Y[i - 1]), 1e-10);
                    for (int i = 1; i < N; i++) X[i] /= X[N - 1];
                }
                if (X.Length != N) ThrowException.SizeMismatch();

                Z = new double[N];
                var h = new double[N];
                var d = new double[N];
                Z[0] = Z[N - 1] = 0;
                for (int i = 0; i < N - 1; i++)
                {
                    h[i] = X[i + 1] - X[i];
                    d[i + 1] = (Y[i + 1] - Y[i]) / h[i];
                }
                Z[1] = d[2] - d[1] - h[0] * Z[0];
                d[1] = 2 * (X[2] - X[0]);
                for (int i = 1; i < N - 2; i++)
                {
                    var t = h[i] / d[i];
                    Z[i + 1] = d[i + 2] - d[i + 1] - Z[i] * t;
                    d[i + 1] = 2 * (X[i + 2] - X[i]) - h[i] * t;
                }
                Z[N - 2] -= h[N - 2] * Z[N - 1];
                for (int i = N - 2; i > 0; i--)
                    Z[i] = (Z[i] - h[i] * Z[i + 1]) / d[i];
            }
            int FindSection(double x)
            {
                int i = 0;
                for (int j = X.Length - 1; i < j;)
                {
                    int k = (i + j) / 2;
                    if (X[k] < x) i = k + 1; else j = k;
                }
                if (i > 0) --i;
                return i;
            }
            public double Interpolate(double x)
            {
                int i = FindSection(x);
                var h = X[i + 1] - X[i];
                var d = x - X[i];
                return (((Z[i + 1] - Z[i]) * d / h + Z[i] * 3) * d + ((Y[i + 1] - Y[i]) / h - (Z[i] * 2 + Z[i + 1]) * h)) * d + Y[i];
            }
            public double CalcGrad(double x)
            {
                int i = FindSection(x);
                var h = X[i + 1] - X[i];
                var d = x - X[i];
                return ((Z[i + 1] - Z[i]) * d / h * 3 + Z[i] * 6) * d + ((Y[i + 1] - Y[i]) / h - (Z[i] * 2 + Z[i + 1]) * h);
            }
        }

        public class Spline1
        {
            public readonly double[] X;
            public readonly double[] Y;
            public Spline1(IEnumerable<double> dataX, IEnumerable<double> dataY)
            {
                X = dataX.ToArray();
                Y = dataY.ToArray();
                if (Y.Length == 0) ThrowException.Argument($"size is 0: {nameof(dataY)}");
                if (X.Length != Y.Length) ThrowException.SizeMismatch();
            }
            int FindSection(double x)
            {
                int i = 0;
                for (int j = X.Length - 1; i < j;)
                {
                    int k = (i + j) / 2;
                    if (X[k] < x) i = k + 1; else j = k;
                }
                if (i > 0) --i;
                return i;
            }
            public double Interpolate(double x)
            {
                int i = FindSection(x);
                return (Y[i + 1] - Y[i]) / (X[i + 1] - X[i]) * (x - X[i]) + Y[i];
            }
        }
        #endregion

        #region interporation
        // NumericalRecipes3.Base_Interp
        public abstract class InterporationBase
        {
            protected int n;
            protected int mm;
            protected int jsav;
            protected int cor;
            protected int dj;
            protected double[] xx;
            protected double[] yy;

            protected InterporationBase(double[] x, double[] y, int m)
            {
                n = x.Length;
                mm = m;
                xx = x;
                yy = y;
                dj = Math.Min(1, (int)Math.Pow(n, 0.25));
            }
            protected double Interp(double x)
            {
                int jlo = cor != 0 ? Hunt(x) : Locate(x);
                return RawInterp(jlo, x);
            }
            public int Locate(double x)
            {
                if (n < 2 || mm < 2 || mm > n) { ThrowException.Argument("size error"); return 0; }
                bool ascnd = (xx[n - 1] >= xx[0]);
                var jl = 0;
                var ju = n - 1;
                while (ju - jl > 1)
                {
                    var jm = (ju + jl) >> 1;
                    if (x >= xx[jm] == ascnd) jl = jm;
                    else ju = jm;
                }
                cor = Math.Abs(jl - jsav) > dj ? 0 : 1;
                jsav = jl;
                return Math.Max(0, Math.Min(n - mm, jl - ((mm - 2) >> 1)));
            }
            public int Hunt(double x)
            {
                int jl = jsav, jm, ju, inc = 1;
                if (n < 2 || mm < 2 || mm > n) { ThrowException.Argument("size error"); return 0; }
                bool ascnd = (xx[n - 1] >= xx[0]);
                if (jl < 0 || jl > n - 1) { jl = 0; ju = n - 1; }
                else
                {
                    if (x >= xx[jl] == ascnd)
                    {
                        for (; ; )
                        {
                            ju = jl + inc;
                            if (ju >= n - 1) { ju = n - 1; break; }
                            if (x < xx[ju] == ascnd) break;
                            jl = ju; inc += inc;
                        }
                    }
                    else
                    {
                        ju = jl;
                        for (; ; )
                        {
                            jl -= inc;
                            if (jl <= 0) { jl = 0; break; }
                            if (x >= xx[jl] == ascnd) break;
                            ju = jl; inc += inc;
                        }
                    }
                }
                while (ju - jl > 1)
                {
                    jm = (ju + jl) >> 1;
                    if (x >= xx[jm] == ascnd) jl = jm;
                    else ju = jm;
                }
                cor = Math.Abs(jl - jsav) > dj ? 0 : 1;
                jsav = jl;
                return Math.Max(0, Math.Min(n - mm, jl - ((mm - 2) >> 1)));
            }
            public abstract double RawInterp(int jlo, double x);
        }

        // NumericalRecipes3.Poly_Interp
        public static (double, double) InterporatePolynomial(IList<double> xx, IList<double> yy, int order, double x)
        {
            double dy = 0.0;
            var c = yy.Take(order).ToArray();
            var d = c.CloneX();
            var ns = xx.MinIndex(v => Math.Abs(v - x));
            var y = yy[ns--];
            for (int m = 1; m < order; m++)
            {
                for (int i = 0; i < order - m; i++)
                {
                    var ho = xx[i] - x;
                    var hp = xx[i + m] - x;
                    var w = c[i + 1] - d[i];
                    if (ho - hp == 0.0) { ThrowException.InvalidOperation("divide by 0"); return (double.NaN, double.NaN); }
                    var den = w / (ho - hp);
                    d[i] = hp * den;
                    c[i] = ho * den;
                }
                dy = 2 * (ns + 1) < (order - m) ? c[ns + 1] : d[ns--];
                y += dy;
            }
            return (y, dy);
        }

        #endregion

        #region quadrature
        // NumericalRecipes3.Quadrature
        // NumericalRecipes3.Trapzd
        static Func<double> QuadratureTrapezoid(Func<double, double> function, double start, double end)
        {
            int n = 0;
            double area = 0.0;
            double next()
            {
                n++;
                if (n == 1)
                {
                    area = 0.5 * (end - start) * (function(start) + function(end));
                }
                else
                {
                    var m = 1 << (n - 2);
                    var w = (end - start) / m;
                    var sum = 0.0;
                    for (int i = 0; i < m; i++)
                        sum += function(start + w * (i + 0.5));
                    area = (area + w * sum) * 0.5;
                }
                return area;
            }
            return next;
        }

        // NumericalRecipes3.qtrap
        public static double IntegrateTrapezoid(Func<double, double> function, double start, double end, double tolerance = 1e-10)
        {
            const int JMAX = 20;
            var quadrature = QuadratureTrapezoid(function, start, end);
            double area = 0.0;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { Warning.TooManyIterations(); break; }
                var old = area;
                area = quadrature();
                if (i > 5)
                    if (Math.Abs(area - old) < tolerance * Math.Abs(old) || (area == 0.0 && old == 0.0)) break;
            }
            return area;
        }

        // NumericalRecipes3.qsimp
        public static double IntegrateSimpson(Func<double, double> function, double start, double end, double tolerance = 1e-10)
        {
            const int JMAX = 20;
            var quadrature = QuadratureTrapezoid(function, start, end);
            double area = 0.0, orig = 0.0;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { Warning.TooManyIterations(); break; }
                var oldo = orig;
                var olda = area;
                orig = quadrature();
                area = (4.0 * orig - oldo) / 3.0;
                if (i > 5)
                    if (Math.Abs(area - olda) < tolerance * Math.Abs(olda) || (area == 0.0 && olda == 0.0)) break;
            }
            return area;
        }

        // NumericalRecipes3.Midpnt
        static Func<double> QuadratureMidpoint(Func<double, double> function, double start, double end)
        {
            int n = 0;
            double area = 0.0;
            double next()
            {
                n++;
                if (n == 1)
                {
                    area = (end - start) * function(0.5 * (start + end));
                }
                else
                {
                    int m = (int)Math.Pow(3, n - 2);
                    var w = (end - start) / m;
                    var sum = 0.0;
                    for (int i = 0; i < m; i++)
                    {
                        sum += function(start + w * (i + 1 / 6.0));
                        sum += function(start + w * (i + 5 / 6.0));
                    }
                    area = (area + w * sum) / 3.0;
                }
                return area;
            }
            return next;
        }
        static Func<double[]> QuadratureMidpoint(Func<double, double[]> function, double start, double end)
        {
            int n = 0;
            double[] area = null!;
            double[] next()
            {
                n++;
                if (n == 1)
                {
                    area = Mt.Mul(function(0.5 * (start + end)), end - start);
                }
                else
                {
                    int m = (int)Math.Pow(3, n - 2);
                    var w = (end - start) / m;
                    var sum = new double[area.Length];
                    for (int i = 0; i < m; i++)
                    {
                        sum.LetAdd(function(start + w * (i + 1 / 6.0)));
                        sum.LetAdd(function(start + w * (i + 5 / 6.0)));
                    }
                    area.LetAddMul(sum, w).LetDiv(3);
                }
                return area;
            }
            return next;
        }

        // NumericalRecipes3.Midinf
        public static Func<double> QuadratureMidinf(Func<double, double> function, double start, double end)
        {
            double func(double x) => function(1 / x) / (x * x);
            return QuadratureMidpoint(func, 1 / end, 1 / start);
        }
        // NumericalRecipes3.Midsql
        public static Func<double> QuadratureMidsql(Func<double, double> function, double start, double end)
        {
            double func(double x) => 2.0 * x * function(start + x * x);
            return QuadratureMidpoint(func, 0, Math.Sqrt(end - start));
        }
        // NumericalRecipes3.Midsqu
        public static Func<double> QuadratureMidsqu(Func<double, double> function, double start, double end)
        {
            double func(double x) => 2.0 * x * function(end - x * x);
            return QuadratureMidpoint(func, 0, Math.Sqrt(end - start));
        }
        // NumericalRecipes3.Midexp
        public static Func<double> QuadratureMidexp(Func<double, double> function, double start, double _)
        {
            double func(double x) => function(-Math.Log(x)) / x;
            return QuadratureMidpoint(func, 0, Math.Exp(-start));
        }

        // NumericalRecipes3.qromb
        // NumericalRecipes3.qromo
        static double IntegrateRomberg(Func<double> quadrature, double tolerance, int div, int JMAX)
        {
            const int Order = 5;
            var xxxx = new List<double>(JMAX);
            var area = new List<double>(JMAX);
            var estimated = 0.0;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { Warning.TooManyIterations(); break; }
                xxxx.Insert(0, 1 / Math.Pow(div, i));
                area.Insert(0, quadrature());
                if (i + 1 >= Order)
                {
                    var r = InterporatePolynomial(xxxx, area, Order, 0.0);
                    estimated = r.Item1;
                    var error = r.Item2;
                    if (Math.Abs(error) <= Math.Abs(estimated) * tolerance) break;
                }
            }
            return estimated;
        }
        public static double IntegrateRombergTrapezoid(Func<double, double> function, double start, double end, double tolerance = 1e-10)
        {
            var quadrature = QuadratureTrapezoid(function, start, end);
            return IntegrateRomberg(quadrature, tolerance, 4, 20);
        }
        public static double IntegrateRombergMidpoint(Func<double, double> function, double start, double end, double tolerance = 3e-9)
        {
            var quadrature = QuadratureMidpoint(function, start, end);
            return IntegrateRomberg(quadrature, tolerance, 9, 14);
        }

        // vector version
        static double[] IntegrateRomberg(Func<double[]> quadrature, double tolerance, int div, int JMAX)
        {
            const int Order = 5;
            var xxxx = new List<double>(JMAX);
            var area = new List<double[]>(JMAX);
            double[] estimated = null!;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { Warning.TooManyIterations(); break; }
                xxxx.Insert(0, 1 / Math.Pow(div, i));
                area.Insert(0, quadrature().CloneX());
                int D = area[0].Length;
                if (i + 1 >= Order)
                {
                    var r = New.Array(D, d => InterporatePolynomial(xxxx, area.Select(v => v[d]).ToArray(), Order, 0.0));
                    estimated = r.Select(v => v.Item1).ToArray();
                    var error = r.Select(v => v.Item2).ToArray();
                    if (Ex.Range(D).All(d => Math.Abs(error[d]) <= Math.Abs(estimated[d]) * tolerance)) break;
                }
            }
            return estimated;
        }
        public static double[] IntegrateRombergMidpoint(Func<double, double[]> function, double start, double end, double tolerance = 3e-9)
        {
            var quadrature = QuadratureMidpoint(function, start, end);
            return IntegrateRomberg(quadrature, tolerance, 9, 14);
        }
        #endregion

        #region signal processing functions

        #region windowing
        public enum DataWindowType { Box, Hanning, Hamming, Blackman, Parzen, Welch, NormalDistribution = -1 }
        public static double[] GetDataWindow(DataWindowType type, int size)
        {
            var Table = new double[size];

            double h = Mt.PI2 / (size - 1);
            switch (type)
            {
                case DataWindowType.Box:
                    for (int i = size; --i >= 0;) Table[i] = 1;
                    break;
                case DataWindowType.Hanning:
                    for (int i = size; --i >= 0;) Table[i] = 0.50 - 0.50 * Math.Cos(h * i);
                    break;
                case DataWindowType.Hamming:
                    for (int i = size; --i >= 0;) Table[i] = 0.54 - 0.46 * Math.Cos(h * i);
                    break;
                case DataWindowType.Blackman:
                    for (int i = size; --i >= 0;) Table[i] = 0.42 - 0.50 * Math.Cos(h * i) + 0.08 * Math.Cos(2 * h * i);
                    break;
                case DataWindowType.Parzen:
                    for (int i = size; --i >= 0;) Table[i] = 1.0 - Math.Abs((i * 2 - (size - 1)) / (double)(size + 1));
                    break;
                case DataWindowType.Welch:
                    for (int i = size; --i >= 0;) Table[i] = 1.0 - ((i * 2 - (size - 1)) / (double)(size + 1)).Sq();
                    break;
            }
            //double c = Math.Sqrt(n / Table.Sum(x => Sq(x)));
            //for (int i = n; --i >= 0; ) Table[i] *= c;
            return Table;
        }

        public static void Windowing(double[] data, DataWindowType type)
        {
            double[] window = GetDataWindow(type, data.Length);
            for (int i = data.Length; --i >= 0;) data[i] *= window[i];
        }
        #endregion

        #region utilities
        public static Array<T> ShiftArray<T>(Array<T> x, int[] nn, int[] ss)
        {
            var a = x.To0();
            var a_ = a.AsSpan();
            var x_ = x.AsSpan();
            var ii = ss.CloneX();
            var oo = ss.To0();
            var dim = ss.Length;
            int n = 1; for (int d = dim; --d >= 0;) { n *= nn[d]; oo[d] = n; }
            int j = ii[^1]; for (int d = dim - 1; --d >= 0;) j += ii[d] * oo[d + 1];
            var n0 = nn[^1];
            var m0 = ii[^1]; 
            var m1 = n0 - m0;
            for (int i = 0; i < n; )
            {
                Ex.MemoryCopy(x_.Slice(i, m1), a_.Slice(j, m1)); i += m1; j -= m0;
                Ex.MemoryCopy(x_.Slice(i, m0), a_.Slice(j, m0)); i += m0; j += m0 + n0;
                for (int d = dim - 1; --d >= 0;)
                {
                    if (++ii[d] == ss[d]) { j += oo[d]; continue; }
                    if (ii[d] == nn[d]) { ii[d] = 0; j -= oo[d]; }
                    break;
                }
            }
            return a;
        }
        public static Array<T> ShiftCentering<T>(Array<T> a) { var nn = a.GetLengths(); return ShiftArray(a, nn, nn.To(n => n / 2)); }
        public static Array<T> ShiftBeginning<T>(Array<T> a) { var nn = a.GetLengths(); return ShiftArray(a, nn, nn.To(n => (n + 1) / 2)); }
        public static T[] ShiftCentering<T>(this T[] a) => (T[])ShiftCentering((Array<T>)a);
        public static T[,] ShiftCentering<T>(this T[,] a) => (T[,])ShiftCentering((Array<T>)a);
        public static T[,,] ShiftCentering<T>(this T[,,] a) => (T[,,])ShiftCentering((Array<T>)a);
        public static T[] ShiftBeginning<T>(this T[] a) => (T[])ShiftBeginning((Array<T>)a);
        public static T[,] ShiftBeginning<T>(this T[,] a) => (T[,])ShiftBeginning((Array<T>)a);
        public static T[,,] ShiftBeginning<T>(this T[,,] a) => (T[,,])ShiftBeginning((Array<T>)a);

        static int[] FactorIntegerExpandedFFT_(int x)
        {
            var fc = Mt.FactorInteger(x);
            if (fc.Length == 1 && fc[0].prime == 2)
            {
                var c = fc[0].count;
                fc = (c & 1) == 0 ?
                    new[] { (4, c / 2) } :
                    new[] { (4, c / 4), (2, c & 3), (4, c / 4) };
            }
            return fc.SelectMany(p => Enumerable.Repeat(p.prime, p.count)).ToArray();
        }
        static readonly Func<int, int[]> FactorIntegerExpandedFFT = New.Cache<int, int[]>(FactorIntegerExpandedFFT_);

        public static unsafe void DimensionReversePowerOf2<T>(T* data, int n) where T : unmanaged
        {
            for (int j = 0, i = 0; i < n; i++)
            {
                if (i < j) Ex.Swap(ref data[i], ref data[j]);
                for (int m = n; j < m; j ^= (m >>= 1)) ;
            }
        }
        public static unsafe void DimensionReverse1<T>(T* data, int[] nn) where T : unmanaged
        {
            var dim = nn.Length;
            var ii = nn.CloneX();
            var oo = new int[dim + 1];
            int n = 1; for (int d = 0; d < dim; n *= nn[d++]) oo[d] = n; oo[dim] = n;
            var sym = nn.AsSpan().IsSymmetric();
            fixed (T* temp = sym ? null : new T[n])
            {
                for (int j = n - 1, i = n; --i >= 0;)
                {
                    if (!sym) temp[i] = data[j];
                    else if (i > j) Ex.Swap(ref data[i], ref data[j]);
                    int d = dim - 1;
                    do
                    {
                        j -= oo[d]; if (--ii[d] > 0) break;
                        j += oo[d + 1]; ii[d] = nn[d];
                    }
                    while (--d >= 0);
                }
                if (!sym) Us.Let(data, temp, n);
            }
        }
        public static unsafe void DimensionReverse<T>(T* data, int n) where T : unmanaged
        {
            (var seq, var sym) = DimensionReverseSequence(n);
            if (sym)
            {
                for (int i = n; --i >= 0;)
                {
                    var j = seq[i];
                    if (i > j) Ex.Swap(ref data[i], ref data[j]);
                }
            }
            else
            {
                var temp = new T[n];
                for (int i = n; --i >= 0;) temp[i] = data[seq[i]];
                for (int i = n; --i >= 0;) data[i] = temp[i];
            }
        }
        static readonly Func<int, (int[] sequence, bool symmetric)> DimensionReverseSequence = New.Cache<int, (int[], bool)>(DimensionReverseSequence_);
        public static (int[] sequence, bool symmetric) DimensionReverseSequence_(int n)
        {
            var nn = FactorIntegerExpandedFFT(n);
            var dim = nn.Length;
            var ii = nn.CloneX();
            var oo = new int[dim + 1];
            n = 1; for (int d = 0; d < dim; n *= nn[d++]) oo[d] = n; oo[dim] = n;
            var seq = new int[n];
            for (int j = n - 1, i = n; --i >= 0;)
            {
                seq[i] = j;
                int d = dim - 1;
                do
                {
                    j -= oo[d]; if (--ii[d] > 0) break;
                    j += oo[d + 1]; ii[d] = nn[d];
                }
                while (--d >= 0);
            }
            return (seq, nn.AsSpan().IsSymmetric());
        }

        public static int AxialSlice(int[] nn, int d, Action<int, int, int> action)
        {
            int nd = nn[d];
            if (nn.Length == 1) action(0, 1, nd);
            else
            {
                int n = 1; for (int i = nn.Length; --i >= 0;) n *= nn[i];
                int step = 1; for (int i = nn.Length; --i > d;) step *= nn[i];
                int ndstep = nd * step;
                for (int i0 = n; (i0 -= ndstep) >= 0;)  //着目次元より上位のループ
                    for (int i1 = step; --i1 >= 0;)     //着目次元より下位のループ
                        action(i0 + i1, step, nd);
            }
            return nd;
        }
        public static int AxialSlice(int[] nn, Action<int, int, int> action)
        {
            int n = 1; for (int i = nn.Length; --i >= 0;) n *= nn[i];
            if (nn.Length == 1) action(0, 1, n);
            else
            {
                for (int step = 1, d = nn.Length; --d >= 0;)
                {
                    int nd = nn[d];
                    int ndstep = nd * step;
                    for (int i0 = n; (i0 -= ndstep) >= 0;)  //着目次元より上位のループ
                        for (int i1 = step; --i1 >= 0;)     //着目次元より下位のループ
                            action(i0 + i1, step, nd);
                    step = ndstep;
                }
            }
            return n;
        }
        public static IEnumerable<(int start, int step, int nd)> AxialSlice(int[] nn)
        {
            int n = 1; for (int i = nn.Length; --i >= 0;) n *= nn[i];
            if (nn.Length == 1) yield return (0, 1, n);
            else
            {
                for (int step = 1, d = nn.Length; --d >= 0;)
                {
                    int nd = nn[d];
                    int ndstep = nd * step;
                    for (int i0 = n; (i0 -= ndstep) >= 0;)  //着目次元より上位のループ
                        for (int i1 = step; --i1 >= 0;)     //着目次元より下位のループ
                            yield return (i0 + i1, step, nd);
                    step = ndstep;
                }
            }
        }

        public static Array<Complex<T>> LetComplexEach<T>(Array<Complex<T>> data, Action<Array<T>> action) where T : unmanaged
        {
            var v0 = data.To0<T>();
            var v1 = data.To0<T>();
            using var p = New.Fix(data);
            using var r0 = New.Fix(v0);
            using var r1 = New.Fix(v1);
            for (int i = data.Length; --i >= 0;) { r1[i] = p[i].Im; r0[i] = p[i].Re; }
            action(v0);
            action(v1);
            for (int i = data.Length; --i >= 0;) { p[i].Im = r1[i]; p[i].Re = r0[i]; }
            return data;
        }

        [MethodImpl(MethodImplOptions.NoInlining)] public static unsafe void LetStartStep<T0, T1>(T0* r, T1* p, int start, int step, int n) where T0 : unmanaged where T1 : unmanaged { for (int i = 0, j = start; i < n; i++, j += step) Op.LetCast(out r[i], p[j]); }
        [MethodImpl(MethodImplOptions.NoInlining)] public static unsafe void LetStartStep<T0, T1>(T0* r, int start, int step, T1* p, int n) where T0 : unmanaged where T1 : unmanaged { for (int i = 0, j = start; i < n; i++, j += step) Op.LetCast(out r[j], p[i]); }
        #endregion

        #region discrete Fourier transform

        #region complex
        static unsafe void FftRadix2(ComplexD* data, int n, int isign, int step)
        {
            if (n <= 1024)
                FftRadix2_Code0(data, n, isign, step);
            else
                FftRadix2_Code1(data, n, isign, step);
        }
        static unsafe void FftRadix2_Code0(ComplexD* data, int n, int isign, int step)
        {
            var dw = Mt.Phase(isign, step * 2);
            var w = (ComplexD)1;
            for (int s = 0; s < step; s++)
            {
                for (int o = s; o < n; o += step * 2)  //stepping access, low calculation
                {
                    var d = &data[o];
                    var d0 = d[0 * step];
                    var d1 = d[1 * step] * w;  //w = Mt.Phase(isign * Math.PI / step * k);
                    d[0 * step] = d0 + d1;
                    d[1 * step] = d0 - d1;
                }
                w *= dw;
            }
        }
        static unsafe void FftRadix2_Code1(ComplexD* data, int n, int isign, int step)
        {
            var dw = Mt.Phase(isign, step * 2);
            for (int o = 0; o < n; o += step * 2)
            {
                var w = (ComplexD)1;
                for (int s = 0; s < step; s++)  //sequential access, high calculation
                {
                    var d = &data[o + s];
                    var d0 = d[0 * step];
                    var d1 = d[1 * step] * w;  //w = Mt.Phase(isign * Math.PI / step * k);
                    d[0 * step] = d0 + d1;
                    d[1 * step] = d0 - d1;
                    w *= dw;
                }
            }
        }

        //[MethodImpl(MethodImplOptions.AggressiveOptimization)]
        static unsafe void FftRadix4(ComplexD* data, int n, int isign, int step)
        {
            var ddx = Mt.Phase(isign, step * 4);
            var dx = (ComplexD)1;
            for (int s = 0; s < step; s++)
            {
                var dx2 = dx * dx;
                var dx3 = dx * dx * dx;
                for (int o = s; o < n; o += step * 4)
                {
                    var d = &data[o];
                    var d0 = d[0 * step];
                    var d1 = d[1 * step] * dx;
                    var d2 = d[2 * step] * dx2;
                    var d3 = d[3 * step] * dx3;
                    var d02a = d0 + d2;
                    var d02b = d0 - d2;
                    var d13a = d1 + d3;
                    var d13b = d1 - d3;
                    d[0 * step].Re = d02a.Re + d13a.Re;
                    d[0 * step].Im = d02a.Im + d13a.Im;
                    d[2 * step].Re = d02a.Re - d13a.Re;
                    d[2 * step].Im = d02a.Im - d13a.Im;
                    d[(2 - isign) * step].Re = d02b.Re - d13b.Im;
                    d[(2 - isign) * step].Im = d02b.Im + d13b.Re;
                    d[(2 + isign) * step].Re = d02b.Re + d13b.Im;
                    d[(2 + isign) * step].Im = d02b.Im - d13b.Re;
                }
                dx *= ddx;
            }
        }

        static unsafe void FftRadixN(ComplexD* data, int n, int isign, int step, int factor) => FftRadixN_Code0(data, n, isign, step, factor);
        static unsafe void FftRadixN_Code0(ComplexD* data, int n, int isign, int step, int factor)
        {
            if (step > 1)
            {
                var ddx = Mt.Phase(isign, step * factor);
                var dx = ddx;
                for (int f = 1; f < factor; f++)
                {
                    var x = dx;
                    for (int s = 1; s < step; s++)
                    {
                        for (int o = s + f * step; o < n; o += step * factor)  //low calculation
                            data[o] *= x;  //x = Mt.Phase(isign * Mt.PI2 / (step * factor) * (s * f))
                        x *= dx;
                    }
                    dx *= ddx;
                }
            }
            fixed (ComplexD* temp = new ComplexD[factor * 2])
            {
                var yy = temp + factor;
                {
                    var dy = Mt.Phase(isign, factor);
                    var y = (ComplexD)1;
                    for (int f = 0; f < factor; f++) { yy[f] = y; y *= dy; }  //low calculation
                }
                for (int o = 0; o < n; o += step * factor)
                {
                    for (int s = 0; s < step; s++)
                    {
                        var d = &data[o + s];
                        for (int f = 0; f < factor; f++) temp[f] = d[0];
                        for (int f = 1; f < factor; f++)
                        {
                            var y = yy[f];
                            var t = d[f * step];
                            temp[0] += t;
                            for (int ff = 1; ff < factor; ff++)
                                temp[ff] += (t *= y);
                        }
                        for (int f = 0; f < factor; f++) d[f * step] = temp[f];
                    }
                }
            }
        }
#pragma warning disable IDE0051
        static unsafe void FftRadixN_Code1(ComplexD* data, int n, int isign, int step, int factor)
        {
            fixed (ComplexD* temp = new ComplexD[factor])
            {
                var ddx = Mt.Phase(isign, step * factor);
                var dy = Mt.Phase(isign, factor);
                for (int o = 0; o < n; o += step * factor)
                {
                    var dx = (ComplexD)1;
                    for (int s = 0; s < step; s++)
                    {
                        var d = &data[o + s];
                        for (int f = 0; f < factor; f++) temp[f] = d[0];
                        var x = dx;
                        var y = dy;
                        for (int f = 1; f < factor; f++)
                        {
                            var t = d[f * step] * x;  //x = s * f
                            temp[0] += t;
                            for (int ff = 1; ff < factor; ff++)
                                temp[ff] += (t *= y);  //t = data[o + s + f * step] * Mt.Phase(isign * Mt.PI2 / (step * factor) * (s + step * ff) * f);
                            y *= dy;  //high calculation
                            x *= dx;  //high calculation
                        }
                        for (int f = 0; f < factor; f++) d[f * step] = temp[f];
                        dx *= ddx;
                    }
                }
            }
        }
#pragma warning restore IDE0051
        #endregion

        #region complexF
        static unsafe void FftRadix2(ComplexS* data, int n, int isign, int step) => FftRadix2_Code0(data, n, isign, step);
        static unsafe void FftRadix2_Code0(ComplexS* data, int n, int isign, int step)
        {
            var dw = Mt.Phase(isign, step * 2);
            var w = (ComplexD)1;
            for (int s = 0; s < step; s++)
            {
                Op.LetCast(out ComplexS v, w);
                for (int o = s; o < n; o += step * 2)  //stepping access, low calculation
                {
                    var d = &data[o];
                    Op.Mul(out ComplexS d1, d[1 * step], v);  //w = Mt.Phase(isign * Math.PI / step * k);
                    Op.Let(out ComplexS d0, d[0 * step]);
                    Op.Add(out d[0 * step], d0, d1);
                    Op.Sub(out d[1 * step], d0, d1);
                }
                w *= dw;
            }
        }
#pragma warning disable IDE0051
        static unsafe void FftRadix2_Code1(ComplexS* data, int n, int isign, int step)
        {
            var dw = Mt.Phase(isign, step * 2);
            for (int o = 0; o < n; o += step * 2)
            {
                var w = (ComplexD)1;
                for (int s = 0; s < step; s++)  //sequential access, high calculation
                {
                    var d = &data[o + s];
                    Op.LetCast(out ComplexS v, w);
                    Op.Mul(out ComplexS d1, d[1 * step], v);  //w = Mt.Phase(isign * Math.PI / step * k);
                    Op.Let(out ComplexS d0, d[0 * step]);
                    Op.Add(out d[0 * step], d0, d1);
                    Op.Sub(out d[1 * step], d0, d1);
                    w *= dw;
                }
            }
        }
#pragma warning restore IDE0051

        static unsafe void FftRadix4(ComplexS* data, int n, int isign, int step)
        {
            var ddx = Mt.Phase(isign, step * 4);
            var dx = (ComplexD)1;
            for (int s = 0; s < step; s++)
            {
                Op.LetCast(out ComplexS dx1, dx);
                Op.LetCast(out ComplexS dx2, dx * dx);
                Op.LetCast(out ComplexS dx3, dx * dx * dx);
                for (int o = s; o < n; o += step * 4)
                {
                    var d = &data[o];
                    Op.Mul(out ComplexS d1, d[1 * step], dx1);
                    Op.Mul(out ComplexS d2, d[2 * step], dx2);
                    Op.Mul(out ComplexS d3, d[3 * step], dx3);
                    Op.Let(out ComplexS d0, d[0 * step]);
                    Op.Add(out ComplexS d02a, d0, d2);
                    Op.Sub(out ComplexS d02b, d0, d2);
                    Op.Add(out ComplexS d13a, d1, d3);
                    Op.Sub(out ComplexS d13b, d1, d3);
                    Op.Add(out d[0 * step], d02a, d13a);
                    Op.Sub(out d[2 * step], d02a, d13a);
                    d[(2 - isign) * step].Re = d02b.Re - d13b.Im;
                    d[(2 - isign) * step].Im = d02b.Im + d13b.Re;
                    d[(2 + isign) * step].Re = d02b.Re + d13b.Im;
                    d[(2 + isign) * step].Im = d02b.Im - d13b.Re;
                }
                dx *= ddx;
            }
        }

        static unsafe void FftRadixN(ComplexS* data, int n, int isign, int step, int factor)
        {
            if (step > 1)
            {
                var ddx = Mt.Phase(isign, step * factor);
                var dx = ddx;
                for (int f = 1; f < factor; f++)
                {
                    var x = dx;
                    for (int s = 1; s < step; s++)
                    {
                        Op.LetCast(out ComplexS v, x);
                        for (int o = s + f * step; o < n; o += step * factor)  //low calculation
                            Op.LetMul(ref data[o], v);  //x = Mt.Phase(isign * Mt.PI2 / (step * factor) * (s * f))
                        x *= dx;
                    }
                    dx *= ddx;
                }
            }
            fixed (ComplexS* temp = new ComplexS[factor * 2])
            {
                var yy = temp + factor;
                {
                    var dy = Mt.Phase(isign, factor);
                    var y = (ComplexD)1;
                    for (int f = 0; f < factor; f++) { Op.LetCast(out yy[f], y); y *= dy; }  //low calculation
                }
                for (int o = 0; o < n; o += step * factor)
                {
                    for (int s = 0; s < step; s++)
                    {
                        var d = &data[o + s];
                        for (int f = 0; f < factor; f++) temp[f] = d[0];
                        for (int f = 1; f < factor; f++)
                        {
                            var y = yy[f];
                            var t = d[f * step];
                            Op.LetAdd(ref temp[0], t);
                            for (int ff = 1; ff < factor; ff++)
                            {
                                Op.LetMul(ref t, y);
                                Op.LetAdd(ref temp[ff], t);
                            }
                        }
                        for (int f = 0; f < factor; f++) d[f * step] = temp[f];
                    }
                }
            }
        }
        #endregion

        //one-dimensional FFT
        static unsafe void LetFFT<T>(Complex<T>* data, int n, bool inverse) where T : unmanaged
        {
            if (data == null) ThrowException.ArgumentNull(nameof(data));
            if (n < 0) ThrowException.ArgumentOutOfRange(nameof(n));
            var isign = inverse ? +1 : -1;  //NumericalRecipesはisignの意味が逆なので注意
            var factors = FactorIntegerExpandedFFT(n);
            DimensionReverse(data, n);
            for (int step = 1, d = factors.Length; --d >= 0;)
            {
                var factor = factors[d];
                if (typeof(T) == typeof(double))
                    switch (factor)
                    {
                        case +2: FftRadix2((ComplexD*)data, n, isign, step); break;
                        case +4: FftRadix4((ComplexD*)data, n, isign, step); break;
                        default: FftRadixN((ComplexD*)data, n, isign, step, factor); break;
                    }
                if (typeof(T) == typeof(float))
                    switch (factor)
                    {
                        case +2: FftRadix2((ComplexS*)data, n, isign, step); break;
                        case +4: FftRadix4((ComplexS*)data, n, isign, step); break;
                        default: FftRadixN((ComplexS*)data, n, isign, step, factor); break;
                    }
                step *= factor;
            }
        }
        //multi-dimensional FFT
        //static unsafe void LetFFT<T>(Complex<T>* data, int[] nn, bool inverse, int _ = 1) where T : unmanaged
        //{
        //    fixed (ComplexD* temp = new ComplexD[nn.Max()])
        //    {
        //        foreach (var (start, step, nd) in AxialSlice(nn))
        //        {
        //            if (step == 1 && Ex.IsSameType<T, double>())
        //                LetFFT(data + start, nd, inverse);
        //            else
        //            {
        //                LetStartStepD(temp, data, start, step, nd);
        //                LetFFT(temp, nd, inverse);
        //                LetStartStepD(data, start, step, temp, nd);
        //            }
        //        }
        //        var n = nn.Product();
        //        var c = 1 / Math.Sqrt(n);
        //        Us.MulB(data, data, Op<T>.From(c), n);  //unitary transform
        //    }
        //}
        static unsafe void LetFFT<T>(Complex<T>* data, int[] nn, bool inverse, int degree = 1) where T : unmanaged
        {
            degree = Ex.ParallelDegree(degree);
            var buffers = New.Array(degree, i => New.Fix<ComplexD>(nn.Max()));
            int n = nn.Product();
            for (int step = 1, d = nn.Length; --d >= 0;)
            {
                int nd = nn[d];
                int ndstep = nd * step;
                IEnumerable<int> main2()
                {
                    for (int i0 = n; (i0 -= ndstep) >= 0;)  //着目次元より上位のループ
                        for (int i1 = step; --i1 >= 0;)     //着目次元より下位のループ
                            yield return i0 + i1;//, step, nd);
                }
                main2().ParallelForEach(degree, (start, eid, pid) =>
                {
                    if (step == 1 && Ex.IsSameType<T, double>())
                        LetFFT(data + start, nd, inverse);
                    else
                    {
                        var temp = buffers[pid].P();
                        LetStartStep(temp, data, start, step, nd);
                        LetFFT(temp, nd, inverse);
                        LetStartStep(data, start, step, temp, nd);
                    }
                });
                step = ndstep;
            }
            var c = 1 / Math.Sqrt(n);
            Us.MulB(data, data, Op<T>.From(c), n);  //unitary transform
            buffers.ForEach(b => b.Dispose());
        }
        //static unsafe void LetFFT1D<T>(Complex<T>* data, int nd, bool inverse) where T : unmanaged
        //{
        //    if (typeof(T) == typeof(double))
        //        LetFFT(data, nd, inverse);
        //    else
        //    {
        //        fixed (ComplexD* temp = new ComplexD[nd])
        //        {
        //            Us.Cast(temp, data, nd);
        //            LetFFT(temp, nd, inverse);
        //            Us.Cast(data, temp, nd);
        //        }
        //    }
        //}
        //static unsafe void LetFFT<T>(Complex<T>* data, int[] nn, bool inverse, int degree = 1) where T : unmanaged
        //{
        //    if (nn.Length == 1) { LetFFT1D(data, nn[0], inverse); return; }
        //    degree = Ex.ParallelDegree(degree);
        //    int n = nn.Product();
        //    var buffers = New.Array(degree, i => New.Fix<ComplexD>(nn.Max()));
        //    using Fix<Complex<T>> dat_ = New.Fix<Complex<T>>(n);
        //    var d0 = data;
        //    var d1 = dat_.P;
        //    for (int d = nn.Length; --d >= 0;)
        //    {
        //        int nd = nn[d];
        //        int step = n / nd;
        //        Ex.ParallelForEvenBlock(step, degree, (start, pid) =>
        //        {
        //            var temp = buffers[pid].P;
        //            Us.Cast(temp, &d0[nd * start], nd);
        //            LetFFT(temp, nd, inverse);
        //            LetStartStep(d1, start, step, temp, nd);
        //        });
        //        var d_ = d0; d0 = d1; d1 = d_;
        //    }
        //    var c = 1 / Math.Sqrt(n);
        //    Us.MulB(data, d0, Op<T>.From(c), n);  //unitary transform
        //    buffers.ForEach(b => b.Dispose());
        //}

        static unsafe Array<Complex<T>> LetFFT<T>(Array<Complex<T>> data, bool inverse, int degree) where T : unmanaged { using var r = New.Fix(data); LetFFT<T>(r.P(), data.GetLengths(), inverse, degree); return data; }
        static Array<Complex<T>> FFT<T>(Array<Complex<T>> data, bool inverse, int degree) where T : unmanaged => LetFFT<T>(data.CloneX(), inverse, degree);
        public static Complex<T>[] LetDiscreteFourierTransform<T>(Complex<T>[] data, bool inverse, int degree = 1) where T : unmanaged => (Complex<T>[])LetFFT<T>(data, inverse, degree);
        public static Complex<T>[,] LetDiscreteFourierTransform<T>(Complex<T>[,] data, bool inverse, int degree = 1) where T : unmanaged => (Complex<T>[,])LetFFT<T>(data, inverse, degree);
        public static Complex<T>[,,] LetDiscreteFourierTransform<T>(Complex<T>[,,] data, bool inverse, int degree = 1) where T : unmanaged => (Complex<T>[,,])LetFFT<T>(data, inverse, degree);
        public static Complex<T>[] DiscreteFourierTransform<T>(Complex<T>[] data, bool inverse, int degree = 1) where T : unmanaged => (Complex<T>[])FFT<T>(data, inverse, degree);
        public static Complex<T>[,] DiscreteFourierTransform<T>(Complex<T>[,] data, bool inverse, int degree = 1) where T : unmanaged => (Complex<T>[,])FFT<T>(data, inverse, degree);
        public static Complex<T>[,,] DiscreteFourierTransform<T>(Complex<T>[,,] data, bool inverse, int degree = 1) where T : unmanaged => (Complex<T>[,,])FFT<T>(data, inverse, degree);

        public static Complex<T>[] LetDiscreteFourierTransformForward<T>(Complex<T>[] data, int degree = 1) where T : unmanaged => (Complex<T>[])LetFFT<T>(data, false, degree);
        public static Complex<T>[,] LetDiscreteFourierTransformForward<T>(Complex<T>[,] data, int degree = 1) where T : unmanaged => (Complex<T>[,])LetFFT<T>(data, false, degree);
        public static Complex<T>[,,] LetDiscreteFourierTransformForward<T>(Complex<T>[,,] data, int degree = 1) where T : unmanaged => (Complex<T>[,,])LetFFT<T>(data, false, degree);
        public static Complex<T>[] LetDiscreteFourierTransformInverse<T>(Complex<T>[] data, int degree = 1) where T : unmanaged => (Complex<T>[])LetFFT<T>(data, true, degree);
        public static Complex<T>[,] LetDiscreteFourierTransformInverse<T>(Complex<T>[,] data, int degree = 1) where T : unmanaged => (Complex<T>[,])LetFFT<T>(data, true, degree);
        public static Complex<T>[,,] LetDiscreteFourierTransformInverse<T>(Complex<T>[,,] data, int degree = 1) where T : unmanaged => (Complex<T>[,,])LetFFT<T>(data, true, degree);
        public static Complex<T>[] DiscreteFourierTransformForward<T>(Complex<T>[] data, int degree = 1) where T : unmanaged => (Complex<T>[])FFT<T>(data, false, degree);
        public static Complex<T>[,] DiscreteFourierTransformForward<T>(Complex<T>[,] data, int degree = 1) where T : unmanaged => (Complex<T>[,])FFT<T>(data, false, degree);
        public static Complex<T>[,,] DiscreteFourierTransformForward<T>(Complex<T>[,,] data, int degree = 1) where T : unmanaged => (Complex<T>[,,])FFT<T>(data, false, degree);
        public static Complex<T>[] DiscreteFourierTransformInverse<T>(Complex<T>[] data, int degree = 1) where T : unmanaged => (Complex<T>[])FFT<T>(data, true, degree);
        public static Complex<T>[,] DiscreteFourierTransformInverse<T>(Complex<T>[,] data, int degree = 1) where T : unmanaged => (Complex<T>[,])FFT<T>(data, true, degree);
        public static Complex<T>[,,] DiscreteFourierTransformInverse<T>(Complex<T>[,,] data, int degree = 1) where T : unmanaged => (Complex<T>[,,])FFT<T>(data, true, degree);

        public static ComplexD[] DiscreteFourierTransform_DefinitionCode(ComplexD[] data, bool inverse = false)
        {
            int n = data.Length;
            int isign = inverse ? +1 : -1;
            var s = 1 / Math.Sqrt(data.Length);
            return New.Array(n, i => Mt.SumFwrd(n, j => data[j] * Mt.Phase(isign * i * j % n, n)) * s);
        }
        public static ComplexS[] DiscreteFourierTransform_DefinitionCode(ComplexS[] data, bool inverse = false)
        {
            int n = data.Length;
            int isign = inverse ? +1 : -1;
            var s = (float)(1 / Math.Sqrt(data.Length));
            return New.Array(n, i => Mt.SumFwrd(n, j => data[j] * Op<ComplexS>.From(Mt.Phase(isign * i * j % n, n))) * s);
        }

        #region real FFT
        //NumericalRecipes3.realft
        //bug-fixed
        static unsafe void RealFFT(double* data, int n, bool inverse)
        {
            if (data == null) ThrowException.ArgumentNull(nameof(data));
            if (n < 0) ThrowException.ArgumentOutOfRange(nameof(n));
            if ((n & 1) != 0) ThrowException.Argument($"{nameof(n)} should be multiple of 2");
            var comp = (ComplexD*)data;
            int n2 = n >> 1;
            if (!inverse)
            {
                LetFFT(comp, n2, inverse);
            }
            var d = Mt.Phase(inverse ? +1 : -1, n);
            var w = new ComplexD(0, inverse ? -1 : +1) * 0.5;
            for (int i = 1; i <= (n >> 2); i++)
            {
                w *= d;
                int j = n2 - i;
                var h1 = new ComplexD(comp[i].Re + comp[j].Re, comp[i].Im - comp[j].Im) * 0.5;
                var h2 = new ComplexD(comp[i].Re - comp[j].Re, comp[i].Im + comp[j].Im) * w;
                comp[i] = new ComplexD(+h1.Re - h2.Re, +h1.Im - h2.Im);
                comp[j] = new ComplexD(+h1.Re + h2.Re, -h1.Im - h2.Im);
            }
            comp[0] = new ComplexD(comp[0].Re + comp[0].Im, comp[0].Re - comp[0].Im);
            if (inverse)
            {
                comp[0] *= 0.5;
                LetFFT(comp, n2, inverse);
            }
        }
        public static unsafe ComplexD[] RealDiscreteFourierTransform(double[] data)
        {
            int n = data.Length;
            var result = new ComplexD[n / 2 + 1];
            fixed (ComplexD* resultcomplex = result)
            fixed (double* d = data)
            {
                double* r = (double*)resultcomplex;
                Us.Mul(r, d, 1 / Math.Sqrt(n), n);  //unitary transform
                RealFFT(r, n, true);
                r[n] = r[1];
                r[1] = 0;
            }
            return result;
        }
        public static unsafe double[] RealInverseDiscreteFourierTransform(ComplexD[] data)
        {
            int n = (data.Length - 1) * 2;
            var result = new double[n];
            fixed (double* r = result)
            fixed (ComplexD* datacomplex = data)
            {
                double* d = (double*)datacomplex;
                double c = 2 / Math.Sqrt(n);  //unitary transform
                Us.Mul(r, d, c, n);
                r[1] = d[n] * c;
                RealFFT(r, n, false);
            }
            return result;
        }

        public static double[] PowerSpectrum(double[] data) => PowerSpectrum(data.ToComplex());
        public static double[] PowerSpectrum(ComplexD[] data)
        {
            var fft = DiscreteFourierTransformForward(data);
            var result = New.Array(fft.Length / 2 + 1, i => fft[i].AbsSq() * 2);
            result[0] /= 2;
            if ((fft.Length & 1) == 0) result[^1] /= 2;
            return result;
        }
        #endregion

        #endregion

        #region convolution
        public enum ConvolutionType { Full, Same, Valid };
        public static int ConvolutionSize(int length0, int length1, ConvolutionType type)
        {
            return type switch
            {
                ConvolutionType.Full => Math.Max(0, length0 + length1 - 1),
                ConvolutionType.Same => length0,
                ConvolutionType.Valid => Math.Max(0, length0 - Math.Max(0, length1 - 1)),
                _ => ThrowException.Argument<int>($"{nameof(ConvolutionType)} {nameof(type)} is unknown"),
            };
        }

        public static ComplexD[] Convolution_DefinitionCode(ComplexD[] data0, ComplexD[] data1, ConvolutionType type = ConvolutionType.Full)
        {
            var n0 = data0.Length;
            var n1 = data1.Length;
            var nR = ConvolutionSize(n0, n1, type);
            var dataR = new ComplexD[nR];
            for (int i0 = 0; i0 < n0; i0++)
                for (int i1 = 0; i1 < n1; i1++)
                {
                    var j = i0 + i1 - (n0 + n1 - nR) / 2;
                    if (j >= 0 && j < nR) dataR[j] += data0[i0] * data1[i1];
                }
            return dataR;
        }

        static IEnumerable<Int2> CopyToEnlargeIterator(int[] nn0, int[] nn1)
        {
            var i = default(Int2);
            var dim = nn0.Length - 1;
            if (dim == 0) { yield return i; yield break; }
            var indx = new int[dim];
            var skip = new int[dim];
            var nnn = 1;
            for (int d = dim; --d >= 0; nnn *= nn1[d]) skip[d] = nnn * (nn1[d] - nn0[d]);
            for (; i.v1 < nnn; i.v0++, i.v1++)
            {
                yield return i;
                for (int d = dim; --d >= 0 && ++indx[d] == nn0[d];) { indx[d] = 0; i.v1 += skip[d]; }
            }
        }
        static unsafe void CopyToEnlarge(ComplexD* data0, int[] nn0, ComplexD* data1, int[] nn1)
        {
            int n0 = nn0.Last();
            int n1 = nn1.Last();
            foreach (var i in CopyToEnlargeIterator(nn0, nn1))
                Us.Let(data1 + i.v1 * n1, data0 + i.v0 * n0, n0);
        }
        static IEnumerable<Int2> CopyToEnlargeShrinkIterator(int[] nn0, int[] nn1, int[] oo1)
        {
            var i = default(Int2);
            var dim = nn0.Length - 1;
            if (dim == 0) { yield return i; yield break; }
            var indx = new int[dim];
            var skip = new int[dim];
            var nnn = 1;
            for (int d = dim; --d >= 0; nnn *= nn0[d]) { i.v0 += nnn * oo1[d]; skip[d] = nnn * (nn0[d] - nn1[d] + oo1[d]); }
            for (; i.v0 < nnn; i.v0++, i.v1++)
            {
                yield return i;
                for (int d = dim; --d >= 0 && ++indx[d] == nn1[d];) { indx[d] = 0; i.v0 += skip[d]; }
            }
        }
        static unsafe void CopyToShrink(ComplexD* data0, int[] nn0, ComplexD* data1, int[] nn1, int[] oo1)
        {
            int n0 = nn0.Last();
            int n1 = nn1.Last();
            int o0 = oo1.Last();
            foreach (var i in CopyToEnlargeShrinkIterator(nn0, nn1, oo1))
                Us.Let(data1 + i.v1 * n1, data0 + (i.v0 * n0 + o0), n1);
        }

        static unsafe Array<ComplexD> CONV(Array<ComplexD> data0, Array<ComplexD> data1, ConvolutionType type)
        {
            if (data0.Length == 0 || data1.Length == 0) ThrowException.Argument($"size of {nameof(data0)} or {nameof(data1)} is 0");
            var nn0 = data0.GetLengths();
            var nn1 = data1.GetLengths();
            var nnR = (nn0, nn1).To((i0, i1) => ConvolutionSize(i0, i1, type));
            var dataR = new Array<ComplexD>(nnR);
            using var d0 = New.Fix(data0);
            using var d1 = New.Fix(data1);
            using var dR = New.Fix(dataR);
            {
                var nnT = (nn0, nn1).To((i0, i1) => 1 << Mt.Log2Ceiling(Math.Max(1, i0 + i1 - 1)));
                var ooR = (nn0, nn1, nnR).To((i0, i1, i2) => (i0 + i1 - i2) / 2);
                var nT = nnT.Product();

                fixed (ComplexD* t0 = new ComplexD[nT], t1 = new ComplexD[nT])
                {
                    CopyToEnlarge(d0.P(), nn0, t0, nnT); LetFFT(t0, nnT, true);
                    CopyToEnlarge(d1.P(), nn1, t1, nnT); LetFFT(t1, nnT, true);
                    Us.Mul(t0, t0, t1, nT);
                    Us.MulB(t0, t0, Math.Sqrt(nT), nT);
                    LetFFT(t0, nnT, false); CopyToShrink(t0, nnT, dR.P(), nnR, ooR);
                }
            }
            return dataR;
        }
        public static ComplexD[] Convolution(ComplexD[] data0, ComplexD[] data1, ConvolutionType type = ConvolutionType.Full) => (ComplexD[])CONV(data0, data1, type);
        public static ComplexD[,] Convolution(ComplexD[,] data0, ComplexD[,] data1, ConvolutionType type = ConvolutionType.Full) => (ComplexD[,])CONV(data0, data1, type);
        public static ComplexD[,,] Convolution(ComplexD[,,] data0, ComplexD[,,] data1, ConvolutionType type = ConvolutionType.Full) => (ComplexD[,,])CONV(data0, data1, type);
        public static ComplexD[] Convolution(double[] data0, double[] data1, ConvolutionType type = ConvolutionType.Full) => (ComplexD[])CONV(data0.ToComplex(), data1.ToComplex(), type);
        public static ComplexD[,] Convolution(double[,] data0, double[,] data1, ConvolutionType type = ConvolutionType.Full) => (ComplexD[,])CONV(data0.ToComplex(), data1.ToComplex(), type);
        public static ComplexD[,,] Convolution(double[,,] data0, double[,,] data1, ConvolutionType type = ConvolutionType.Full) => (ComplexD[,,])CONV(data0.ToComplex(), data1.ToComplex(), type);

        public static ComplexD[] Filter(ComplexD[] data, ComplexD[] filter, ConvolutionType type = ConvolutionType.Same) => (ComplexD[])CONV(data, filter.Rev(), type);
        public static ComplexD[,] Filter(ComplexD[,] data, ComplexD[,] filter, ConvolutionType type = ConvolutionType.Same) => (ComplexD[,])CONV(data, filter.Rev(), type);
        public static ComplexD[,,] Filter(ComplexD[,,] data, ComplexD[,,] filter, ConvolutionType type = ConvolutionType.Same) => (ComplexD[,,])CONV(data, filter.Rev(), type);
        public static ComplexD[] Filter(double[] data, double[] filter, ConvolutionType type = ConvolutionType.Same) => (ComplexD[])CONV(data.ToComplex(), filter.Rev().ToComplex(), type);
        public static ComplexD[,] Filter(double[,] data, double[,] filter, ConvolutionType type = ConvolutionType.Same) => (ComplexD[,])CONV(data.ToComplex(), filter.Rev().ToComplex(), type);
        public static ComplexD[,,] Filter(double[,,] data, double[,,] filter, ConvolutionType type = ConvolutionType.Same) => (ComplexD[,,])CONV(data.ToComplex(), filter.Rev().ToComplex(), type);
        #endregion

        #region discrete sine and cosine transform
        public static double[] DiscreteCosineTransformType1_DefinitionCode(double[] data, bool _ = true)
        {
            int n = data.Length;
            double c = Math.Sqrt(2.0 / (n - 1));
            return New.Array(n, k => Mt.SumFwrd(n, i => Math.Cos(Math.PI / (n - 1) * k * i) * data[i] * c * ((i == 0 || i == n - 1) ? 0.5 : 1.0)));
        }
        public static double[] DiscreteCosineTransformType2_DefinitionCode(double[] data, bool inverse = false)
        {
            int n = data.Length;
            if (inverse) return DiscreteCosineTransformType3_DefinitionCode(data);
            double c = Math.Sqrt(2.0 / n);
            return New.Array(n, k => Mt.SumFwrd(n, i => Math.Cos(Math.PI / n * k * (i + 0.5)) * data[i] * c));
        }
        public static double[] DiscreteCosineTransformType3_DefinitionCode(double[] data, bool inverse = false)
        {
            int n = data.Length;
            if (inverse) return DiscreteCosineTransformType2_DefinitionCode(data);
            double c = Math.Sqrt(2.0 / n);
            return New.Array(n, k => Mt.SumFwrd(n, i => Math.Cos(Math.PI / n * (k + 0.5) * i) * data[i] * (i == 0 ? 0.5 : 1) * c));
        }
        public static double[] DiscreteCosineTransformType4_DefinitionCode(double[] data, bool _ = false)
        {
            int n = data.Length;
            double c = Math.Sqrt(2.0 / n);
            return New.Array(n, k => Mt.SumFwrd(n, i => Math.Cos(Math.PI / n * (k + 0.5) * (i + 0.5)) * data[i] * c));
        }

        public static double[] DiscreteCosineTransform_DefinitionCode(double[] data, bool inverse = false)
        {
            int n = data.Length;
            double[] result = null!;
            if (!inverse)
            {
                result = New.Array(n, k => { var c = Math.PI / n * (k + 0.0); return Mt.SumFwrd(n, i => Math.Cos(c * (i + 0.5)) * data[i]); });
                result[0] /= Mt.Sqrt2;  //unitary transform
            }
            else
            {
                var b = data[0]; data[0] /= Mt.Sqrt2;  //unitary transform
                result = New.Array(n, k => { var c = Math.PI / n * (k + 0.5); return Mt.SumFwrd(n, i => Math.Cos(c * (i + 0.0)) * data[i]); });
                data[0] = b;
            }
            return result.LetMul(1 / Math.Sqrt(n / 2));  //unitary transform
        }

        public static double[] DCT_DefinitionCode(double[] data, bool inverse)
        {
            int n = data.Length;
            if (inverse) data[0] /= Mt.Sqrt2;
            var result = !inverse ?
                New.Array(n, k => { var c = Math.PI / n * (k + 0.0); return Mt.SumFwrd(n, i => Math.Cos(c * (i + 0.5)) * data[i]); }) :
                New.Array(n, k => { var c = Math.PI / n * (k + 0.5); return Mt.SumFwrd(n, i => Math.Cos(c * (i + 0.0)) * data[i]); });
            if (!inverse) result[0] /= Mt.Sqrt2;
            return result;  //定数倍をしていないのでunitaryではない
        }
        // NumericalRecipes3.dct2
        // modified to meet the definition of FFT/real FFT
        // modified data[0] divided/multiplied Mt.Sqrt2
        static unsafe void LetDCT(double* data, int n, bool inverse)
        {
            var comp = (ComplexD*)data;
            var n2 = n >> 1;
            var wp = Mt.Phase(1, n * 2);
            var wq = Mt.Phase(1, n * 4);
            var w = ComplexD.One;
            if (!inverse)
            {
                for (int i = 0; i < n2; i++)
                {
                    int j = n - 1 - i;
                    var y1 = (data[i] + data[j]) * 0.5;
                    var y2 = (data[i] - data[j]) * wq.Im;
                    data[i] = y1 + y2;
                    data[j] = y1 - y2;
                    wq *= wp;
                }
                RealFFT(data, n, inverse);  //複素共役
                for (int i = 1; i < n2; i++) Op.LetCul(ref comp[i], (w *= wp));
                var sum = data[1] * 0.5;
                for (int i = n - 1; i > 0; i -= 2)
                {
                    var sum1 = sum;
                    sum += data[i];
                    data[i] = sum1;
                }
                data[0] /= Mt.Sqrt2;
            }
            else
            {
                data[0] *= Mt.Sqrt2;
                var ytemp = data[n - 1];
                for (int i = n - 1; i > 2; i -= 2) data[i] = data[i - 2] - data[i];
                data[1] = ytemp * 2;
                for (int i = 1; i < n2; i++) Op.LetCul(ref comp[i], (w *= wp));
                RealFFT(data, n, inverse);  //複素共役
                for (int i = 0; i < n2; i++)
                {
                    int j = n - 1 - i;
                    var y1 = (data[i] + data[j]) * 0.5;
                    var y2 = (data[i] - data[j]) * 0.25 / wq.Im;
                    data[i] = y1 + y2;
                    data[j] = y1 - y2;
                    wq *= wp;
                }
            }
        }
        static unsafe void LetDCT<T>(T* data, int[] nn, bool inverse) where T : unmanaged
        {
            if (Op<T>.Fold == 1) main<T>();
            else if (typeof(T) == typeof(ComplexD)) main<double>();
            else if (typeof(T) == typeof(ComplexS)) main<float>();
            void main<B>() where B : unmanaged
            {
                var fold = Op<T>.Fold;
                fixed (double* temp = typeof(T) == typeof(double) && nn.Length == 1 ? null : new double[nn.Max()])
                {
                    foreach (var (start, step, nd) in AxialSlice(nn))
                    {
                        for (int o = fold; --o >= 0;)
                        {
                            if (typeof(T) == typeof(double) && step == 1)
                                LetDCT((double*)data + start, nd, inverse);
                            else
                            {
                                LetStartStep(temp, (B*)data, start * fold + o, step * fold, nd);
                                LetDCT(temp, nd, inverse);
                                LetStartStep((B*)data, start * fold + o, step * fold, temp, nd);
                            }
                        }
                    }
                    var n = nn.Product();
                    var c = Math.Sqrt((1 << nn.Length) / (double)n);
                    Us.LetMul(data, Op<T>.From(c), n);  //unitary transform
                }
            }
        }
        static unsafe Array<T> LetDCT<T>(Array<T> data, bool inverse) where T : unmanaged { using var r = New.Fix(data); LetDCT<T>(r.P(), data.GetLengths(), inverse); return data; }
        static Array<T> DCT<T>(Array<T> data, bool inverse) where T : unmanaged => LetDCT<T>(data.CloneX(), inverse);
        public static T[] LetDiscreteCosineTransform<T>(T[] data, bool inverse) where T : unmanaged => (T[])LetDCT<T>(data, inverse);
        public static T[,] LetDiscreteCosineTransform<T>(T[,] data, bool inverse) where T : unmanaged => (T[,])LetDCT<T>(data, inverse);
        public static T[,,] LetDiscreteCosineTransform<T>(T[,,] data, bool inverse) where T : unmanaged => (T[,,])LetDCT<T>(data, inverse);
        public static T[] DiscreteCosineTransform<T>(T[] data, bool inverse) where T : unmanaged => (T[])DCT<T>(data, inverse);
        public static T[,] DiscreteCosineTransform<T>(T[,] data, bool inverse) where T : unmanaged => (T[,])DCT<T>(data, inverse);
        public static T[,,] DiscreteCosineTransform<T>(T[,,] data, bool inverse) where T : unmanaged => (T[,,])DCT<T>(data, inverse);

        public static T[] LetDiscreteCosineTransformForward<T>(T[] data) where T : unmanaged => (T[])LetDCT<T>(data, false);
        public static T[,] LetDiscreteCosineTransformForward<T>(T[,] data) where T : unmanaged => (T[,])LetDCT<T>(data, false);
        public static T[,,] LetDiscreteCosineTransformForward<T>(T[,,] data) where T : unmanaged => (T[,,])LetDCT<T>(data, false);
        public static T[] LetDiscreteCosineTransformInverse<T>(T[] data) where T : unmanaged => (T[])LetDCT<T>(data, true);
        public static T[,] LetDiscreteCosineTransformInverse<T>(T[,] data) where T : unmanaged => (T[,])LetDCT<T>(data, true);
        public static T[,,] LetDiscreteCosineTransformInverse<T>(T[,,] data) where T : unmanaged => (T[,,])LetDCT<T>(data, true);
        public static T[] DiscreteCosineTransformForward<T>(T[] data) where T : unmanaged => (T[])DCT<T>(data, false);
        public static T[,] DiscreteCosineTransformForward<T>(T[,] data) where T : unmanaged => (T[,])DCT<T>(data, false);
        public static T[,,] DiscreteCosineTransformForward<T>(T[,,] data) where T : unmanaged => (T[,,])DCT<T>(data, false);
        public static T[] DiscreteCosineTransformInverse<T>(T[] data) where T : unmanaged => (T[])DCT<T>(data, true);
        public static T[,] DiscreteCosineTransformInverse<T>(T[,] data) where T : unmanaged => (T[,])DCT<T>(data, true);
        public static T[,,] DiscreteCosineTransformInverse<T>(T[,,] data) where T : unmanaged => (T[,,])DCT<T>(data, true);
        #endregion

        #region discrete wavelet transform
        public enum WaveletType
        {
            Haar,
            Daubechies1, Daubechies2, Daubechies3, Daubechies4, Daubechies5, Daubechies6, Daubechies7, Daubechies8, Daubechies9, Daubechies10,
            Symlet1, Symlet2, Symlet3, Symlet4, Symlet5, Symlet6, Symlet7, Symlet8, Symlet9, Symlet10,
            Coiflet1, Coiflet2, Coiflet3, Coiflet4, Coiflet5,
            CDF5_3, CDF9_7,
            ReverseCDF5_3, ReverseCDF9_7,
            BiorthogonalSpline1_3, BiorthogonalSpline1_5, BiorthogonalSpline2_2, BiorthogonalSpline2_4, BiorthogonalSpline2_6, BiorthogonalSpline2_8, BiorthogonalSpline3_1, BiorthogonalSpline3_3, BiorthogonalSpline3_5, BiorthogonalSpline3_7, BiorthogonalSpline3_9, BiorthogonalSpline4_4, BiorthogonalSpline5_5, BiorthogonalSpline6_8,
            ReverseBiorthogonalSpline1_3, ReverseBiorthogonalSpline1_5, ReverseBiorthogonalSpline2_2, ReverseBiorthogonalSpline2_4, ReverseBiorthogonalSpline2_6, ReverseBiorthogonalSpline2_8, ReverseBiorthogonalSpline3_1, ReverseBiorthogonalSpline3_3, ReverseBiorthogonalSpline3_5, ReverseBiorthogonalSpline3_7, ReverseBiorthogonalSpline3_9, ReverseBiorthogonalSpline4_4, ReverseBiorthogonalSpline5_5, ReverseBiorthogonalSpline6_8,
            bior1_1, bior1_3, bior1_5, bior2_2, bior2_4, bior2_6, bior2_8, bior3_1, bior3_3, bior3_5, bior3_7, bior3_9, bior4_4, bior5_5, bior6_8,
            rbio1_1, rbio1_3, rbio1_5, rbio2_2, rbio2_4, rbio2_6, rbio2_8, rbio3_1, rbio3_3, rbio3_5, rbio3_7, rbio3_9, rbio4_4, rbio5_5, rbio6_8,
            Count
        }
        static Dictionary<WaveletType, WaveletType> WaveletFilterMappingReverseToForward_()
        {
            var dict = new Dictionary<WaveletType, WaveletType>();
            for (int ww = 0; ww < (int)WaveletType.Count; ww++)
            {
                var type = (WaveletType)ww;
                var str = type.ToString();
                if (str.StartsWith("Reverse", StringComparison.Ordinal) || str.StartsWith("rbio", StringComparison.Ordinal))
                {
                    var typeF = str.Replace("Reverse", "", StringComparison.Ordinal).Replace("rbio", "bior", StringComparison.Ordinal).Parse<WaveletType>();
                    dict.Add(type, typeF);
                }
            }
            return dict;
        }
        public static readonly Dictionary<WaveletType, WaveletType> WaveletFilterMappingReverseToForward = WaveletFilterMappingReverseToForward_();

        public static readonly Dictionary<WaveletType, WaveletType> WaveletFilterMappingForwardToReverse = WaveletFilterMappingReverseToForward.Select(pair => New.KeyValuePair(pair.Value, pair.Key)).ToDictionary();
        public static bool IsBiorthogonal(this WaveletType type)
        {
            return WaveletFilterMappingReverseToForward.Keys.Contains(type)
                || WaveletFilterMappingForwardToReverse.Keys.Contains(type);
        }
        public static bool IsMatlab(this WaveletType type)
        {
            return type >= WaveletType.bior1_1 && type <= WaveletType.rbio6_8;
        }

        static double[] Div(this int[] x, double y) => x.To(v => v / y);
        static readonly Dictionary<WaveletType, (int[] center, double[] filterL, double[]? filterH)> WaveletFilterCoefficients = new Dictionary<WaveletType, (int[] center, double[] filterL, double[]? filterH)>()
        {
            { WaveletType.Haar, (new[] { 0, 0 }, new[] { 1, 1 }.Div(2d), null) },

            { WaveletType.Daubechies1, (new[] { 0, 0 }, new[] { 1, 1 }.Div(2d), null) },
            { WaveletType.Daubechies2, (new[] { 0, 2 }, new[] { 0.341506350946109662, 0.591506350946109662, 0.158493649053890338, -0.0915063509461096617 }, null) },
            { WaveletType.Daubechies3, (new[] { 0, 4 }, new[] { 0.235233603892081840, 0.570558457915721813, 0.325182500263116264, -0.0954672077841636808, -0.0604161041551981046, 0.0249087498684418679 }, null) },
            { WaveletType.Daubechies4, (new[] { 0, 6 }, new[] { 0.162901714025649174, 0.505472857545914431, 0.446100069123379812, -0.0197875131178223215, -0.132253583684519868, 0.0218081502370886263, 0.0232518005354908823, -0.00749349466518073622 }, null) },
            { WaveletType.Daubechies5, (new[] { 0, 8 }, new[] { 0.113209491291779179, 0.426971771352514166, 0.512163472129598537, 0.0978834806739046740, -0.171328357691467443, -0.0228005659417736487, 0.0548513293210668235, -0.00441340005417912727, -0.00889593505097709574, 0.00235871396953393576 }, null) },
            { WaveletType.Daubechies6, (new[] { 0, 10 }, new[] { 0.0788712160014507084, 0.349751907037617831, 0.531131879940868985, 0.222915661465017756, -0.159993299446061395, -0.0917590320301475761, 0.0689440464873722988, 0.0194616048541646641, -0.0223318741650945346, 0.000391625576148577888, 0.00337803118146393786, -0.000761766902801253228 }, null) },
            { WaveletType.Daubechies7, (new[] { 0, 12 }, new[] { 0.0550497153728118489, 0.280395641812762563, 0.515574245818098668, 0.332186241105539674, -0.101756911231346242, -0.158417505640332839, 0.0504232325046940869, 0.0570017225798715763, -0.0268912262948454388, -0.0117199707821032886, 0.00887489618968076392, 0.000303757497701069354, -0.00127395235909368659, 0.000250113426561245329 }, null) },
            { WaveletType.Daubechies8, (new[] { 0, 14 }, new[] { 0.0384778110540762366, 0.221233623576124920, 0.477743075213873696, 0.413908266211195893, -0.0111928676668802177, -0.200829316390489050, 0.000334097046220118780, 0.0910381784236577454, -0.0122819505228484093, -0.0311751033251394281, 0.00988607964835075898, 0.00618442240981592237, -0.00344385962844180908, -0.000277002274479389322, 0.000477614855649626155, -0.0000830686306866126906 }, null) },
            { WaveletType.Daubechies9, (new[] { 0, 16 }, new[] { 0.0269251747946628001, 0.172417151906977942, 0.427674532179707566, 0.464772857183147340, 0.0941847747531837791, -0.207375880900938492, -0.0684767745123830986, 0.105034171139506192, 0.0217263377306145436, -0.0478236320600970261, 0.000177446406616518914, 0.0158120829262558630, -0.00333981011313857748, -0.00302748028754506593, 0.00130648364024724570, 0.000162907335676092213, -0.000178164879510777668, 0.0000278227570171548578 }, null) },
            { WaveletType.Daubechies10, (new[] { 0, 18 }, new[] { 0.0188585787961206889, 0.133061091396920895, 0.372787535743233385, 0.486814055366819946, 0.198818870884508690, -0.176668100897056300, -0.138554939360483153, 0.0900637242666966663, 0.0658014935505350073, -0.0504832855983897171, -0.0208296240438008070, 0.0234849070486985608, 0.00255021848390723873, -0.00758950116792824939, 0.000986662682481602594, 0.00140884329509733811, -0.000484973919928205481, -0.0000823545030453889776, 0.0000661771834255533832, -9.37920781375020417e-6 }, null) },

            { WaveletType.Symlet1, (new[] { 0, 0 }, new[] { 1, 1 }.Div(2d), null) },
            { WaveletType.Symlet2, (new[] { 0, 2 }, new[] { 0.341506350946109662, 0.591506350946109662, 0.158493649053890338, -0.0915063509461096617 }, null) },
            { WaveletType.Symlet3, (new[] { 0, 4 }, new[] { 0.235233603892081840, 0.570558457915721813, 0.325182500263116264, -0.0954672077841636808, -0.0604161041551981046, 0.0249087498684418679 }, null) },
            { WaveletType.Symlet4, (new[] { 0, 6 }, new[] { -0.0535744507091029091, -0.0209554825625297638, 0.351869534328149944, 0.56832912170382036, 0.210617267101788543, -0.070158812089271724, -0.0089123507208355776, 0.0227851729479811287 }, null) },
            { WaveletType.Symlet5, (new[] { 0, 8 }, new[] { 0.0138160764789039104, -0.0149212499343306372, -0.123975681306471244, 0.0117394615680618916, 0.44829082418991607, 0.51152648344719579, 0.140995348426909606, -0.0276720930583110351, 0.0208734322107416525, 0.0193273979773839931 }, null) },
            { WaveletType.Symlet6, (new[] { 0, 10 }, new[] { 0.0108923501632923404, 0.00246830618592331792, -0.083431607706072971, -0.0341615607932860043, 0.347228986479217711, 0.55694639196286894, 0.238952185666434696, -0.051362484930839247, -0.0148918756492696984, 0.0316252813300217445, 0.00124996104639792265, -0.0055159337546887511 }, null) },
            { WaveletType.Symlet7, (new[] { 0, 12 }, new[] { 0.0072606973809772346, 0.00283567134287206067, -0.076231935947767382, -0.099028353403661296, 0.204091969862259157, 0.54289135490721086, 0.379081300981849780, 0.0123328297443058951, -0.0350391456110006535, 0.048007383967732907, 0.0215777262909796243, -0.0089352158255620531, -0.00074061295729776000, 0.00189632926710162320 }, null) },
            { WaveletType.Symlet8, (new[] { 0, 14 }, new[] { -0.00239172925574918242, -0.000383345448116262939, 0.0224118115218331187, 0.0053793058752816682, -0.101324327643146345, -0.043326807702927445, 0.340372673594868292, 0.54955331526837133, 0.257699335187136744, -0.0367312543805017983, -0.0192467606317033857, 0.0347452329556764726, 0.00269319437688271298, -0.0105728432641897714, -0.000214197150121953918, 0.00133639669640580508 }, null) },
            { WaveletType.Symlet9, (new[] { 0, 16 }, new[] { 0.00075624365468110336, -0.000334570754565580138, -0.0072577892764722866, 0.0062644481209288757, 0.043895625777140233, -0.0128932229647116819, -0.135446891752230114, 0.0249414154790615168, 0.43652420367474112, 0.50762989541675616, 0.168829461801127693, -0.0385860805487290861, 0.000412570464354699657, 0.0213722168012282998, -0.0081516756127939299, -0.0093846984182122765, 0.00043825126945147886, 0.00099059686824377197 }, null) },
            { WaveletType.Symlet10, (new[] { 0, 18 }, new[] { 0.000544585223622171551, 0.000067622509971109376, -0.0061103213170447729, -0.00103618196027313741, 0.0324754623014816445, 0.0082094347081706310, -0.112779486159978673, -0.050120107506458779, 0.333535669214578217, 0.54412576536872958, 0.271406505551398398, -0.0251282701703042731, -0.0226203861521083050, 0.0353517837811440680, 0.0040764083918891607, -0.0143931159719292173, -0.00056876765533680188, 0.00324786418934089525, 0.0000403306014989607765, -0.000324794948390880057 }, null) },

            { WaveletType.Coiflet1, (new[] { 2, 2 }, new[] { -0.0514297284707684560, 0.238929728470768456, 0.602859456941536912, 0.272140543058463088, -0.0514297284707684560, -0.0110702715292315440 }, null) },
            { WaveletType.Coiflet2, (new[] { 4, 6 }, new[] { 0.0115875967387168685, -0.0293201379834685650, -0.0476395903110081330, 0.273021046534766616, 0.574682393856863848, 0.294867193695619187, -0.0540856070917114290, -0.0420264804607716054, 0.0167444101632795053, 0.00396788361296201195, -0.00128920335614065920, -0.000509505399107644243 }, null)},
            { WaveletType.Coiflet3, (new[] { 6, 10 }, new[] { -0.00268241867092206927, 0.00550312670783138296, 0.0165835604791703551, -0.0465077644787269664, -0.0432207635602119521, 0.286503335273647449, 0.561285256870330136, 0.302983571772824158, -0.0507701407548889747, -0.0581962507615855011, 0.0244340943211670365, 0.0112292409620378310, -0.00636960101104885286, -0.00182045891556622424, 0.000790205100957598228, 0.000329665173793179054, -0.0000501927745532764655, -0.0000244657342553079588 }, null) },
            { WaveletType.Coiflet4, (new[] { 8, 14 }, new[] { 0.000630961211430991787, -0.00115222514377005423, -0.00519452516347062984, 0.0113624614832654496, 0.0188672385695638949, -0.0574642419019292915, -0.0396526529624501758, 0.293667405016104659, 0.553126455039549482, 0.307157309667881165, -0.0471127375238945609, -0.0680381146780174842, 0.0278136369584678459, 0.0177358314227020191, -0.0107563161550861612, -0.00400101084495052833, 0.00265266491353014143, 0.000895593927695410741, -0.000416500195094125322, -0.000183829616713661261, 0.0000440802266159736075, 0.0000220828469123108912, -2.30491916267664121e-6, -1.26217917999472470e-6 }, null) },
            { WaveletType.Coiflet5, (new[] { 10, 18 }, new[] { -0.000149964522834595033, 0.000253552752358033471, 0.00154028672599522236, -0.00294107816403569319, -0.00716411234941005329, 0.0165521833064928884, 0.0199190171979843206, -0.0649983782547232496, -0.0368025534744687353, 0.298095901458719180, 0.547508271354036715, 0.309700259078420353, -0.0438673148282361564, -0.0746444201328397124, 0.0291946927752807367, 0.0231045722770668419, -0.0139712926863820056, -0.00647674975150586184, 0.00478111679913065761, 0.00171938348438550402, -0.00117494793441353769, -0.000450822240069623631, 0.000213445797508629167, 0.0000992469113987353317, -0.0000291468438862213082, -0.0000150403179819768591, 2.61680966001311815e-6, 1.45750292135516307e-6, -1.14819964990297973e-7, -6.79106067732235551e-8 }, null) },

            { WaveletType.CDF5_3, (new[] { 1, 1 }, new[] { 1, 2, 1 }.Div(4d), new[] { -1, -2, 6, -2, -1 }.Div(8d)) },
            { WaveletType.CDF9_7, (new[] { 4, 2 }, new[] { 0.0267487574108100884, -0.0168641184428749544, -0.0782232665289902625, 0.266864118442874954, 0.602949018236360348, 0.266864118442874954, -0.0782232665289902625, -0.0168641184428749544, 0.0267487574108100884 }, new[] { 0.0456358815571250456, -0.0287717631142500911, -0.295635881557125046, 0.557543526228500182, -0.295635881557125046, -0.0287717631142500911, 0.0456358815571250456 }) },

            { WaveletType.BiorthogonalSpline1_3, (new[] { 2, 0 }, new[] { -1, 1, 8, 8, 1, -1 }.Div(16d), new[] { 1, -1 }.Div(2d)) },
            { WaveletType.BiorthogonalSpline1_5, (new[] { 4, 0 }, new[] { 3, -3, -22, 22, 128, 128, 22, -22, -3, 3 }.Div(256d), new[] { 1, -1 }.Div(2d)) },
            { WaveletType.BiorthogonalSpline2_2, (new[] { 2, 0 }, new[] { -1, 2, 6, 2, -1 }.Div(8d), new[] { -1, 2, -1 }.Div(4d)) },
            { WaveletType.BiorthogonalSpline2_4, (new[] { 4, 0 }, new[] { 3, -6, -16, 38, 90, 38, -16, -6, 3 }.Div(128d), new[] { -1, 2, -1 }.Div(4d)) },
            { WaveletType.BiorthogonalSpline2_6, (new[] { 6, 0 }, new[] { -5, 10, 34, -78, -123, 324, 700, 324, -123, -78, 34, 10, -5 }.Div(1024d), new[] { -1, 2, -1 }.Div(4d)) },
            { WaveletType.BiorthogonalSpline2_8, (new[] { 8, 0 }, new[] { 35, -70, -300, 670, 1228, -3126, -3796, 10718, 22050, 10718, -3796, -3126, 1228, 670, -300, -70, 35 }.Div(32768d), new[] { -1, 2, -1 }.Div(4d)) },
            { WaveletType.BiorthogonalSpline3_1, (new[] { 1, 1 }, new[] { -1, 3, 3, -1 }.Div(4d), new[] { -1, 3, -3, 1 }.Div(8d)) },
            { WaveletType.BiorthogonalSpline3_3, (new[] { 3, 1 }, new[] { 3, -9, -7, 45, 45, -7, -9, 3 }.Div(64d), new[] { -1, 3, -3, 1 }.Div(8d)) },
            { WaveletType.BiorthogonalSpline3_5, (new[] { 5, 1 }, new[] { -5, 15, 19, -97, -26, 350, 350, -26, -97, 19, 15, -5 }.Div(512d), new[] { -1, 3, -3, 1 }.Div(8d)) },
            { WaveletType.BiorthogonalSpline3_7, (new[] { 7, 1 }, new[] { 35, -105, -195, 865, 363, -3489, -307, 11025, 11025, -307, -3489, 363, 865, -195, -105, 35 }.Div(16384d), new[] { -1, 3, -3, 1 }.Div(8d)) },
            { WaveletType.BiorthogonalSpline3_9, (new[] { 9, 1 }, new[] { -63, 189, 469, -1911, -1308, 9188, 1140, -29676, 190, 87318, 87318, 190, -29676, 1140, 9188, -1308, -1911, 469, 189, -63 }.Div(131072d), new[] { -1, 3, -3, 1 }.Div(8d)) },
            { WaveletType.BiorthogonalSpline4_4, (new[] { 5, 1 }, new[] { -5, 20, -1, -96, 70, 280, 70, -96, -1, 20, -5 }.Div(256d), new[] { 1, -4, 6, -4, 1 }.Div(16d)) },
            { WaveletType.BiorthogonalSpline5_5, (new[] { 6, 2 }, new[] { 35, -175, 120, 800, -1357, -1575, 4200, 4200, -1575, -1357, 800, 120, -175, 35 }.Div(4096d), new[] { 1, -5, 10, -10, 5, -1 }.Div(32d)) },
            { WaveletType.BiorthogonalSpline6_8, (new[] { 10, 2 }, new[] { 231, -1386, 1302, 8358, -19397, -15624, 83400, -9896, -210210, 84084, 420420, 84084, -210210, -9896, 83400, -15624, -19397, 8358, 1302, -1386, 231 }.Div(262144d), new[] { -1, 6, -15, 20, -15, 6, -1 }.Div(64d)) },

            //lossless JPEG 2000: bior2_2
            //lossy    JPEG 2000: bior4_4
            { WaveletType.bior1_1, (new[] { 0, 0 }, new[] { +7.071067811865475e-1, +7.071067811865475e-1 }, new[] { -7.071067811865476e-1, +7.071067811865476e-1 }) },
            { WaveletType.bior1_3, (new[] { 2, 0 }, new[] { -8.838834764831843e-2, +8.838834764831843e-2, +7.071067811865475e-1, +7.071067811865475e-1, +8.838834764831843e-2, -8.838834764831843e-2 }, new[] { -7.071067811865476e-1, +7.071067811865476e-1 }) },
            { WaveletType.bior1_5, (new[] { 4, 0 }, new[] { +1.657281518405971e-2, -1.657281518405971e-2, -1.215339780164378e-1, +1.215339780164378e-1, +7.071067811865475e-1, +7.071067811865475e-1, +1.215339780164378e-1, -1.215339780164378e-1, -1.657281518405971e-2, +1.657281518405971e-2 }, new[] { -7.071067811865476e-1, +7.071067811865476e-1 }) },
            { WaveletType.bior2_2, (new[] { 2, 0 }, new[] { -1.767766952966369e-1, +3.535533905932737e-1, +1.060660171779821e-0, +3.535533905932737e-1, -1.767766952966369e-1 }, new[] { -3.535533905932738e-1, +7.071067811865476e-1, -3.535533905932738e-1 }) },
            { WaveletType.bior2_4, (new[] { 4, 0 }, new[] { +3.314563036811941e-2, -6.629126073623882e-2, -1.767766952966369e-1, +4.198446513295125e-1, +9.943689110435824e-1, +4.198446513295125e-1, -1.767766952966369e-1, -6.629126073623882e-2, +3.314563036811941e-2 }, new[] { -3.535533905932738e-1, +7.071067811865476e-1, -3.535533905932738e-1 }) },
            { WaveletType.bior2_6, (new[] { 6, 0 }, new[] { -6.905339660024878e-3, +1.381067932004976e-2, +4.695630968816917e-2, -1.077232986963881e-1, -1.698713556366120e-1, +4.474660099696121e-1, +9.667475524034829e-1, +4.474660099696121e-1, -1.698713556366120e-1, -1.077232986963881e-1, +4.695630968816917e-2, +1.381067932004976e-2, -6.905339660024878e-3 }, new[] { -3.535533905932738e-1, +7.071067811865476e-1, -3.535533905932738e-1 }) },
            { WaveletType.bior2_8, (new[] { 8, 0 }, new[] { +1.510543050630442e-3, -3.021086101260884e-3, -1.294751186254665e-2, +2.891610982635417e-2, +5.299848189069094e-2, -1.349130736077361e-1, -1.638291834340902e-1, +4.625714404759165e-1, +9.516421218971785e-1, +4.625714404759165e-1, -1.638291834340902e-1, -1.349130736077361e-1, +5.299848189069094e-2, +2.891610982635417e-2, -1.294751186254665e-2, -3.021086101260884e-3, +1.510543050630442e-3 }, new[] { -3.535533905932738e-1, +7.071067811865476e-1, -3.535533905932738e-1 }) },
            { WaveletType.bior3_1, (new[] { 1, 1 }, new[] { -3.535533905932737e-1, +1.060660171779821e-0, +1.060660171779821e-0, -3.535533905932737e-1 }, new[] { -1.767766952966369e-1, +5.303300858899107e-1, -5.303300858899107e-1, +1.767766952966369e-1 }) },
            { WaveletType.bior3_3, (new[] { 3, 1 }, new[] { +6.629126073623882e-2, -1.988737822087165e-1, -1.546796083845572e-1, +9.943689110435824e-1, +9.943689110435824e-1, -1.546796083845573e-1, -1.988737822087165e-1, +6.629126073623882e-2 }, new[] { -1.767766952966369e-1, +5.303300858899107e-1, -5.303300858899107e-1, +1.767766952966369e-1 }) },
            { WaveletType.bior3_5, (new[] { 5, 1 }, new[] { -1.381067932004976e-2, +4.143203796014926e-2, +5.248058141618906e-2, -2.679271788089653e-1, -7.181553246425870e-2, +9.667475524034829e-1, +9.667475524034828e-1, -7.181553246425877e-2, -2.679271788089653e-1, +5.248058141618906e-2, +4.143203796014926e-2, -1.381067932004976e-2 }, new[] { -1.767766952966369e-1, +5.303300858899107e-1, -5.303300858899107e-1, +1.767766952966369e-1 }) },
            { WaveletType.bior3_7, (new[] { 0, 6 }, new[] { +2.121320343559642e-0, -7.071067811865475e-1 }, new[] { -1.790273245191635e-3, +5.967577483972118e-4, +1.534519924449973e-2, -5.115066414833244e-3, -6.460328881934387e-2, +2.153442960644796e-2, -3.862898156482065e-1, +6.001677926737673e-1, -4.657267970705668e-1, +1.552422656901890e-1, -1.534519924449973e-2, +5.115066414833244e-3, +1.790273245191635e-3, -5.967577483972118e-4 }) },
            { WaveletType.bior3_9, (new[] { 0, 8 }, new[] { +2.121320343559642e-0, -7.071067811865475e-1 }, new[] { +4.028114801681179e-4, -1.342704933893726e-4, -4.207141509302952e-3, +1.402380503100984e-3, +2.098455996685338e-2, -6.994853322284461e-3, -7.024264344749608e-2, +2.341421448249869e-2, -3.862898156482065e-1, +6.001677926737673e-1, -4.600874424424146e-1, +1.533624808141382e-1, -2.098455996685338e-2, +6.994853322284461e-3, +4.207141509302952e-3, -1.402380503100984e-3, -4.028114801681179e-4, +1.342704933893726e-4 }) },
            { WaveletType.bior4_4, (new[] { 4, 2 }, new[] { +3.782845550750114e-2, -2.384946501955685e-2, -1.106244044092826e-1, +3.774028556128305e-1, +8.526986789091245e-1, +3.774028557909638e-1, -1.106244045129673e-1, -2.384946502829822e-2, +3.782845552136610e-2 }, new[] { +6.453888262876165e-2, -4.068941760920477e-2, -4.180922732220352e-1, +7.884856164063713e-1, -4.180922732220352e-1, -4.068941760920475e-2, +6.453888262876159e-2 }) },
            { WaveletType.bior5_5, (new[] { 4, 4 }, new[] { +3.968708835354199e-2, +7.948108637240277e-3, -5.446378851811667e-2, +3.456052819946451e-1, +7.366601815848807e-1, +3.456052816507071e-1, -5.446378830562263e-2, +7.948108668692961e-3, +3.968708830153168e-2 }, new[] { -1.345670945911869e-2, -2.694966880111492e-3, +1.367065846643291e-1, -9.350469740093889e-2, -4.768032657984836e-1, +8.995061097486485e-1, -4.768032657984851e-1, -9.350469740093885e-2, +1.367065846643292e-1, -2.694966880111505e-3, -1.345670945911870e-2 }) },
            { WaveletType.bior6_8, (new[] { 8, 4 }, new[] { +1.908831735831091e-3, -1.914286128426919e-3, -1.699063986420198e-2, +1.193456527806748e-2, +4.973290347287927e-2, -7.726317313318716e-2, -9.405920352008927e-2, +4.207962846175345e-1, +8.259229974570239e-1, +4.207962846098224e-1, -9.405920349528524e-2, -7.726317316720342e-2, +4.973290349080352e-2, +1.193456527972913e-2, -1.699063986761873e-2, -1.914286129088744e-3, +1.908831736491035e-3 }, new[] { +1.442628250562461e-2, -1.446750489679035e-2, -7.872200106262979e-2, +4.036797903034046e-2, +4.178491091502785e-1, -7.589077294536618e-1, +4.178491091502799e-1, +4.036797903034038e-2, -7.872200106262969e-2, -1.446750489679027e-2, +1.442628250562456e-2 }) },
        };

        public static (int center, double[] filter) WaveletFilterCoefficientEach(WaveletType type, int no)
        {
            if (!no.IsInsideC(0, 4)) ThrowException.ArgumentOutOfRange(nameof(no));
            var btype = type;
            var bno = no;
            if (WaveletFilterMappingReverseToForward.TryGetValue(type, out var typeF))
            {
                type = typeF;
                no ^= 2;  //biorthogonal の reverse version は forward version のdualとprimaryを入替
            }
            var (center, filterL, filterH) = WaveletFilterCoefficients[type];
            var orthogonal = filterH is null;
            if (orthogonal && no >= 2) no -= 2;  //orthogonal wavelet はprimaryとdualが同じ

            var fL = filterL;
            var fH = filterH ?? filterL;
            var f = (no == 0 || no == 3) ? fL : fH;
            var rep = no >= 2 || (no == 1 && orthogonal);  //流用
            f = rep ? f.To((v, i) => Mt.PowOfNeg1(1 + no + i) * v).LetRev() : f.CloneX();
            var c = no < 2 ? center[no] : center[3 - no] + Mt.PowOfNeg1(no) * (fL.Length & 1);

            if (type.IsMatlab())
            {
                if (btype >= WaveletType.rbio1_1 && btype <= WaveletType.rbio6_8)  //特殊
                {
                    if (btype >= WaveletType.rbio3_7 && btype <= WaveletType.rbio6_8) f.LetRev();
                    if ((bno == 1 || bno == 3) && !(btype == WaveletType.rbio3_7 || btype == WaveletType.rbio3_9)) f.LetNeg();
                }
                if (type == WaveletType.bior6_8 && no >= 2) f.LetNeg();  //特殊
            }
            else
            {
                if (rep && f.Length % 2 == 0) f.LetNeg();
                if (rep && !orthogonal && filterH!.Length.IsInside(4, 5)) f.LetNeg();  //特殊
                f.LetMul(Mt.Sqrt2);
            }
            return (c, f);
        }

        public static void TestWaveletFilters()
        {
            for (int ww = 0; ww < (int)WaveletType.Count; ww++)
            {
                var w = (WaveletType)ww;
                var ffs = New.Array(4, no => WaveletFilterCoefficientEach(w, no));
                int n = 32;
                int n2 = n / 2;
                var A = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    var (center, filter) = ffs[i / n2];
                    var o = i * 2 % n;
                    for (int j = 0; j < filter.Length; j++)
                        A[i, Mt.Mod_(o + j - center, n)] += filter[j];  //periodical
                }
                var B = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    var (center, filter) = ffs[(i / n2) + 2];
                    var o = i * 2 % n;
                    for (int j = 0; j < filter.Length; j++)
                        B[i, Mt.Mod_(o + j - center, n)] += filter[j];  //periodical
                }
                B = B.T();
                var R = B.Multiply(A);
                //if (w == WaveletType.bior5_5)
                //{
                //    Console.WriteLine(A.ToStringFormat("f4"));
                //    Console.WriteLine(B.ToStringFormat("f4"));
                //    Console.WriteLine(R.ToStringFormat("f4"));
                //}
                var I = Mt.UnitMatrix<double>(n);
                Console.WriteLine($"{w}\t{ffs[0].filter.Length},{ffs[1].filter.Length}\t{ffs[0].center},{ffs[1].center},{ffs[2].center},{ffs[3].center}\t{(float)A.Det()}\t{(float)B.Det()}\t{(float)R.Det()}\t{(float)Mt.DNorm2Sub(R, I)}");
                //Console.WriteLine(R.ToStringFormat("f2"));
            }
        }

        public static (Int2 center, ComplexD[] filter) WaveletFilterCoefficient_(WaveletType type, bool dual)
        {
            var f0 = WaveletFilterCoefficientEach(type, !dual ? 0 : 2);
            var f1 = WaveletFilterCoefficientEach(type, !dual ? 1 : 3);
            var filter = new ComplexD[Math.Max(f0.filter.Length, f1.filter.Length)];
            f0.filter.ForEach((x, i) => filter[i].Re = x);
            f1.filter.ForEach((x, i) => filter[i].Im = x);
            return (new Int2(f0.center, f1.center), filter);
        }
        public static readonly Func<WaveletType, bool, (Int2 center, ComplexD[] filter)> WaveletFilterCoefficient = New.Cache<WaveletType, bool, (Int2 center, ComplexD[] filter)>(WaveletFilterCoefficient_);
        public static (Int2 center, ComplexS[] filter) WaveletFilterCoefficientSingle_(WaveletType type, bool dual)
        {
            var (center, filter) = WaveletFilterCoefficient(type, dual);
            return (center, filter.To(f => new ComplexS((float)f.Re, (float)f.Im)));
        }
        public static readonly Func<WaveletType, bool, (Int2 center, ComplexS[] filter)> WaveletFilterCoefficientSingle = New.Cache<WaveletType, bool, (Int2 center, ComplexS[] filter)>(WaveletFilterCoefficientSingle_);


        static unsafe void SetBorderConditionPeriodical<T>(T* data, int n, int m, bool inverse) where T : unmanaged
        {
            if (!inverse)
            {
                for (int i = 0, j = 0; i < m; i++) { data[i + n] = data[j++]; if (j == n) j = 0; }
                for (int i = 0, j = n; --i >= -m;) { data[i + 0] = data[--j]; if (j == 0) j = n; }
            }
            else
            {
                for (int i = 0, j = 0; i < m; i++) { Op.LetAdd(ref data[j++], data[i + n]); if (j == n) j = 0; }
                for (int i = 0, j = n; --i >= -m;) { Op.LetAdd(ref data[--j], data[i + 0]); if (j == 0) j = n; }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void LetAddMulB<T>(ref T a, T x, double y) where T : unmanaged
        {
            if (typeof(T) == typeof(double)) { Unsafe.As<T, double>(ref a) += (double)(object)x * y; return; }
            if (typeof(T) == typeof(ComplexD)) { Unsafe.As<T, ComplexD>(ref a) += (ComplexD)(object)x * y; return; }
        }
        //[MethodImpl(MethodImplOptions.AggressiveOptimization)]
        static unsafe void DWT0<T>(T* result, T* data, int n, Int2 center, ComplexD* filter, int m, bool inverse) where T : unmanaged
        {
            var n2 = n / 2;
            if (!inverse)
            {
                SetBorderConditionPeriodical(data, n, m, inverse);
                for (int i = n2; --i >= 0;)
                {
                    var d0 = &data[i * 2 - center.v0];
                    var d1 = &data[i * 2 - center.v1];
                    T a0 = default;
                    T a1 = default;
                    for (int k = m; --k >= 0;)
                    {
                        LetAddMulB(ref a0, d0[k], filter[k].Re);
                        LetAddMulB(ref a1, d1[k], filter[k].Im);
                    }
                    result[i] = a0;
                    result[i + n2] = a1;
                }
            }
            else
            {
                Us.Clear(result - m, n + m * 2);
                for (int i = n2; --i >= 0;)
                {
                    var d0 = &result[i * 2 - center.v0];
                    var d1 = &result[i * 2 - center.v1];
                    var a0 = data[i];
                    var a1 = data[i + n2];
                    for (int k = m; --k >= 0;)
                    {
                        LetAddMulB(ref d0[k], a0, filter[k].Re);
                        LetAddMulB(ref d1[k], a1, filter[k].Im);
                    }
                }
                SetBorderConditionPeriodical(result, n, m, inverse);
            }
        }
        static unsafe void DWT(double* result, double* data, int n, Int2 center, ComplexD* filter, int m, bool inverse)
        {
            var n2 = n / 2;
            if (!inverse)
            {
                SetBorderConditionPeriodical(data, n, m, inverse);
                for (int i = n2; --i >= 0;)
                {
                    var d0 = &data[i * 2 - center.v0];
                    var d1 = &data[i * 2 - center.v1];
                    double a0 = default;
                    double a1 = default;
                    for (int k = m; --k >= 0;)
                    {
                        a0 += d0[k] * filter[k].Re;
                        a1 += d1[k] * filter[k].Im;
                    }
                    result[i] = a0;
                    result[i + n2] = a1;
                }
            }
            else
            {
                Us.Clear(result - m, n + m * 2);
                for (int i = n2; --i >= 0;)
                {
                    var d0 = &result[i * 2 - center.v0];
                    var d1 = &result[i * 2 - center.v1];
                    var a0 = data[i];
                    var a1 = data[i + n2];
                    for (int k = m; --k >= 0;)
                    {
                        d0[k] += a0 * filter[k].Re;
                        d1[k] += a1 * filter[k].Im;
                    }
                }
                SetBorderConditionPeriodical(result, n, m, inverse);
            }
        }
        static unsafe void DWT(ComplexD* result, ComplexD* data, int n, Int2 center, ComplexD* filter, int m, bool inverse)
        {
            var n2 = n / 2;
            if (!inverse)
            {
                SetBorderConditionPeriodical(data, n, m, inverse);
                for (int i = n2; --i >= 0;)
                {
                    var d0 = &data[i * 2 - center.v0];
                    var d1 = &data[i * 2 - center.v1];
                    ComplexD a0 = default;
                    ComplexD a1 = default;
                    for (int k = m; --k >= 0;)
                    {
                        a0 += d0[k] * filter[k].Re;
                        a1 += d1[k] * filter[k].Im;
                    }
                    result[i] = a0;
                    result[i + n2] = a1;
                }
            }
            else
            {
                Us.Clear(result - m, n + m * 2);
                for (int i = n2; --i >= 0;)
                {
                    var d0 = &result[i * 2 - center.v0];
                    var d1 = &result[i * 2 - center.v1];
                    var a0 = data[i];
                    var a1 = data[i + n2];
                    for (int k = m; --k >= 0;)
                    {
                        d0[k] += a0 * filter[k].Re;
                        d1[k] += a1 * filter[k].Im;
                    }
                }
                SetBorderConditionPeriodical(result, n, m, inverse);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe void DWT<T>(T* result, T* data, int n, Int2 center, ComplexD* filter, int m, bool inverse) where T : unmanaged
        {
            if (typeof(T) == typeof(double)) { DWT((double*)result, (double*)data, n, center, filter, m, inverse); return; }
            if (typeof(T) == typeof(ComplexD)) { DWT((ComplexD*)result, (ComplexD*)data, n, center, filter, m, inverse); return; }
            ThrowException.NotImplemented();
        }

        //static unsafe void LetDWT<T>(T* data, int[] nn, WaveletType type, bool inverse, bool transpose, int level) where T : unmanaged
        //{
        //    if (typeof(T) == typeof(Double) || typeof(T) == typeof(ComplexD)) { main<Double>(WaveletFilterCoefficient(type, inverse)); return; }
        //    if (typeof(T) == typeof(Single) || typeof(T) == typeof(ComplexS)) { main<Single>(WaveletFilterCoefficientSingle(type, inverse)); return; }
        //    ThrowException.NotImplemented();
        //    void main<B>((Int2 center, Complex<B>[] filter) cf) where B : unmanaged
        //    {
        //        var forwardx = inverse == transpose;
        //        var m = cf.filter.Length;
        //        var nmax = nn.Max();
        //        if (level < 0) level = int.MaxValue; level.LetMin(Mt.Log2Floor(nmax));
        //        fixed (Complex<B>* f_ = cf.filter)
        //        fixed (T* temp0 = &(new T[nmax + m * 2])[m])
        //        fixed (T* temp1 = &(new T[nmax + m * 2])[m])
        //        {
        //            var steps = nn.To0(); int n = 1; for (int i = nn.Length; --i >= 0; n *= nn[i]) steps[i] = n;
        //            var index = nn.To0();
        //            for (int l = 0; l < level; l++)
        //            {
        //                var sizes = nn.To(n => (n >> (forwardx ? l : level - 1 - l)) & ~1);
        //                for (int d = nn.Length; --d >= 0;)
        //                {
        //                    var size = sizes[d];
        //                    var step = steps[d];
        //                    if (size < 2) continue;
        //                    for (int offset = 0; ;)
        //                    {
        //                        LetStartStep(temp0, data, offset, step, size);
        //                        DWT(temp1, temp0, size, cf.center, f_, m, forwardx);
        //                        LetStartStep(data, offset, step, temp1, size);
        //                        for (int i = index.Length; --i >= 0;)
        //                        {
        //                            if (i == d || sizes[i] == 0) continue;
        //                            offset += steps[i]; if (++index[i] < sizes[i]) break;
        //                            offset -= steps[i] * sizes[i]; index[i] = 0;
        //                        }
        //                        if (offset == 0) break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        static unsafe void LetDWT<T>(T* data, int[] nn, WaveletType type, bool inverse, bool transpose, int level, int degree) where T : unmanaged
        {
            if (typeof(T) == typeof(Double)) { main<Double>(); return; }
            if (typeof(T) == typeof(Single)) { main<Double>(); return; }
            if (typeof(T) == typeof(ComplexD)) { main<ComplexD>(); return; }
            if (typeof(T) == typeof(ComplexS)) { main<ComplexD>(); return; }
            ThrowException.NotImplemented();
            void main<T1>() where T1 : unmanaged
            {
                degree = Ex.ParallelDegree(degree);
                (Int2 center, ComplexD[] filter) = WaveletFilterCoefficient(type, inverse);
                var forward = inverse == transpose;
                var m = filter.Length;
                var nmax = nn.Max();
                var bn = nmax + m * 2;
                var buffer = New.Array(degree, pid => New.Fix<T1>(2 * bn));
                using var filter_ = New.Fix(filter);
                {
                    if (level < 0) level = int.MaxValue; level.LetMin(Mt.Log2Floor(nmax));
                    var steps = nn.To0(); int n = 1; for (int i = nn.Length; --i >= 0; n *= nn[i]) steps[i] = n;
                    var index = nn.To0();
                    for (int l = level; --l >= 0;)
                    {
                        var lv = forward ? l : level - 1 - l;
                        var sizes = nn.To(n => (n >> (forward ? level - 1 - l : l)) & ~1);
                        for (int d = nn.Length; --d >= 0;)
                        {
                            var size = sizes[d];
                            var step = steps[d];
                            if (size < 2) continue;
                            IEnumerable<int> main3()
                            {
                                for (int offset = 0; ;)
                                {
                                    yield return offset;
                                    for (int i = index.Length; --i >= 0;)
                                    {
                                        if (i == d || sizes[i] == 0) continue;
                                        offset += steps[i]; if (++index[i] < sizes[i]) break;
                                        offset -= steps[i] * sizes[i]; index[i] = 0;
                                    }
                                    if (offset == 0) break;
                                }
                            }
                            main3().ParallelForEach(degree.Min((lv - 2).Max(1)), (offset, eid, pid) =>
                            {
                                var temp = buffer[pid].P();
                                var temp0 = &temp[0 * bn + m];
                                var temp1 = &temp[1 * bn + m];
                                LetStartStep(temp0, data, offset, step, size);
                                DWT(temp1, temp0, size, center, filter_.P(), m, !forward);
                                LetStartStep(data, offset, step, temp1, size);
                            });
                        }
                    }
                }
                buffer.ForEach(b => b.Dispose());
            }
        }
        static unsafe Array<T> LetDWT<T>(Array<T> data, WaveletType type, bool inverse, bool transpose, int level, int degree) where T : unmanaged { using var d = New.Fix(data); LetDWT<T>(d.P(), data.GetLengths(), type, inverse, transpose, level, degree); return data; }
        static Array<T> DWT<T>(Array<T> data, WaveletType type, bool inverse, bool transpose, int level, int degree) where T : unmanaged => LetDWT(data.CloneX(), type, inverse, transpose, level, degree);
        public static T[] LetDiscreteWaveletTransform<T>(T[] data, WaveletType type, bool inverse, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[])LetDWT<T>(data, type, inverse, transpose, level, degree);
        public static T[,] LetDiscreteWaveletTransform<T>(T[,] data, WaveletType type, bool inverse, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,])LetDWT<T>(data, type, inverse, transpose, level, degree);
        public static T[,,] LetDiscreteWaveletTransform<T>(T[,,] data, WaveletType type, bool inverse, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,,])LetDWT<T>(data, type, inverse, transpose, level, degree);
        public static T[] DiscreteWaveletTransform<T>(T[] data, WaveletType type, bool inverse, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[])DWT<T>(data, type, inverse, transpose, level, degree);
        public static T[,] DiscreteWaveletTransform<T>(T[,] data, WaveletType type, bool inverse, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,])DWT<T>(data, type, inverse, transpose, level, degree);
        public static T[,,] DiscreteWaveletTransform<T>(T[,,] data, WaveletType type, bool inverse, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,,])DWT<T>(data, type, inverse, transpose, level, degree);

        public static T[] LetDiscreteWaveletTransformForward<T>(T[] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[])LetDWT<T>(data, type, false, transpose, level, degree);
        public static T[,] LetDiscreteWaveletTransformForward<T>(T[,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,])LetDWT<T>(data, type, false, transpose, level, degree);
        public static T[,,] LetDiscreteWaveletTransformForward<T>(T[,,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,,])LetDWT<T>(data, type, false, transpose, level, degree);
        public static T[] LetDiscreteWaveletTransformInverse<T>(T[] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[])LetDWT<T>(data, type, true, transpose, level, degree);
        public static T[,] LetDiscreteWaveletTransformInverse<T>(T[,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,])LetDWT<T>(data, type, true, transpose, level, degree);
        public static T[,,] LetDiscreteWaveletTransformInverse<T>(T[,,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,,])LetDWT<T>(data, type, true, transpose, level, degree);
        public static T[] DiscreteWaveletTransformForward<T>(T[] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[])DWT<T>(data, type, false, transpose, level, degree);
        public static T[,] DiscreteWaveletTransformForward<T>(T[,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,])DWT<T>(data, type, false, transpose, level, degree);
        public static T[,,] DiscreteWaveletTransformForward<T>(T[,,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,,])DWT<T>(data, type, false, transpose, level, degree);
        public static T[] DiscreteWaveletTransformInverse<T>(T[] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[])DWT<T>(data, type, true, transpose, level, degree);
        public static T[,] DiscreteWaveletTransformInverse<T>(T[,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,])DWT<T>(data, type, true, transpose, level, degree);
        public static T[,,] DiscreteWaveletTransformInverse<T>(T[,,] data, WaveletType type, bool transpose = false, int level = -1, int degree = 1) where T : unmanaged => (T[,,])DWT<T>(data, type, true, transpose, level, degree);

        public static double[] DiscreteWaveletTransform_DefinitionCode(double[] data, Double2[] filter)
        {
            var n = data.Length;
            var m = filter.Length;
            var result0 = New.Array(n / 2, i => Mt.SumFwrd(m, k => data[(n + (2 - m / 2) + i * 2 + k) % n] * filter[k].v0));
            var result1 = New.Array(n / 2, i => Mt.SumFwrd(m, k => data[(n + (2 - m / 2) + i * 2 + k) % n] * filter[k].v1));
            return result0.ConcatTo(result1);
        }
        public static double[] InverseDiscreteWaveletTransform_DefinitionCode(double[] data, Double2[] filter)
        {
            var n = data.Length;
            var m = filter.Length;
            var result = new double[n];
            Ex.For(n / 2, i => Ex.For(m, k => result[(n + (2 - m / 2) + i * 2 + k) % n] += filter[k].v0 * data[i]));
            Ex.For(n / 2, i => Ex.For(m, k => result[(n + (2 - m / 2) + i * 2 + k) % n] += filter[k].v1 * data[i + n / 2]));
            return result;
        }
        #endregion

        #endregion

        #region association tests
        public static (BigInteger, BigInteger) FisherExactTestInteger(int[,] ctable)
        {
            int lengthY = ctable.GetLength(0);
            int lengthX = ctable.GetLength(1);
            int[] restY = New.Array(lengthY, y => Mt.SumFwrd(lengthX, x => ctable[y, x]));
            int[] restX = New.Array(lengthX, x => Mt.SumFwrd(lengthY, y => ctable[y, x]));

            BigInteger nCasesMultinomialX = Mt.MultinomialInteger(restX);
            BigInteger nCasesThis = Mt.Product(lengthY, i => Mt.MultinomialInteger(Ex.Range(lengthX).Select(x => ctable[i, x])));
            BigInteger nCasesLess = 0;
            {
                void function(int yy, int xx, BigInteger nCasesPrev)
                {
                    int backupY = restY[yy];
                    int backupX = restX[xx];
                    int min = Math.Max(0, backupY - Mt.SumFwrd(xx, x => restX[x]));
                    int max = Math.Min(backupY, backupX);
                    if (min > max) ThrowException.Argument(nameof(ctable));
                    for (int k = max; k >= min; k--)
                    {
                        restY[yy] = backupY - k;
                        restX[xx] = backupX - k;
                        BigInteger nCases = nCasesPrev / Mt.FactorialInteger(k);
                        if (xx > 0)
                        {
                            function(yy, xx - 1, nCases);
                            continue;
                        }
                        if (yy > 1)
                        {
                            function(yy - 1, lengthX - 1, nCases * Mt.FactorialInteger(restY[yy - 1]));
                            continue;
                        }
                        nCases *= Mt.MultinomialInteger(restX);
                        if (nCases <= nCasesThis) nCasesLess += nCases;
                    }
                    restY[yy] = backupY;
                    restX[xx] = backupX;
                }

                function(lengthY - 1, lengthX - 1, Mt.FactorialInteger(restY[lengthY - 1]));
            }
            var gcd = Mt.GreatestCommonDivisor(nCasesMultinomialX, nCasesLess);
            return (nCasesLess / gcd, nCasesMultinomialX / gcd);
        }

        public static double FisherExactTestDouble(int[,] ctable)
        {
            int lengthY = ctable.GetLength(0);
            int lengthX = ctable.GetLength(1);
            int[] restY = New.Array(lengthY, y => Mt.SumFwrd(lengthX, x => ctable[y, x]));
            int[] restX = New.Array(lengthX, x => Mt.SumFwrd(lengthY, y => ctable[y, x]));

            double nCasesTotal = restX.Sum(c => Mt.LogFactorial(c)) + restY.Sum(c => Mt.LogFactorial(c)) - Mt.LogFactorial(restY.Sum());
            double nCasesThis = ctable.ToEnumerable().Sum(c => Mt.LogFactorial(c)) * (1 - 16 * Mt.DoubleEps);
            double nCasesLess = 0;
            {
                void function(int yy, int xx, double nCasesPrev)
                {
                    int backupY = restY[yy];
                    int backupX = restX[xx];
                    int min = Math.Max(0, backupY - Mt.SumFwrd(xx, x => restX[x]));
                    int max = Math.Min(backupY, backupX);
                    if (min > max) ThrowException.Argument(nameof(ctable));
                    for (int k = max; k >= min; k--)
                    {
                        restY[yy] = backupY - k;
                        restX[xx] = backupX - k;
                        double nCases = nCasesPrev + Mt.LogFactorial(k);
                        if (xx > 0)
                        {
                            function(yy, xx - 1, nCases);
                            continue;
                        }
                        if (yy > 1)
                        {
                            function(yy - 1, lengthX - 1, nCases);
                            continue;
                        }
                        nCases += restX.Sum(c => Mt.LogFactorial(c));
                        if (nCases >= nCasesThis) nCasesLess += Math.Exp(nCasesTotal - nCases);
                    }
                    restY[yy] = backupY;
                    restX[xx] = backupX;
                }

                function(lengthY - 1, lengthX - 1, 0);
            }
            return nCasesLess;
        }

        public static double PearsonChiSquareTest(int[,] ctable)
        {
            int lengthY = ctable.GetLength(0);
            int lengthX = ctable.GetLength(1);
            int[] totalY = New.Array(lengthY, y => Mt.SumFwrd(lengthX, x => ctable[y, x]));
            int[] totalX = New.Array(lengthX, x => Mt.SumFwrd(lengthY, y => ctable[y, x]));
            int total = totalY.Sum();

            double sum = 0;
            for (int y = lengthY; --y >= 0;)
                for (int x = lengthX; --x >= 0;)
                    sum += (ctable[y, x] - totalY[y] * totalX[x] / (double)total).Sq() / (totalY[y] * totalX[x]);
            return Mt.ChiSquareDistributionUpper((lengthY - 1) * (lengthX - 1), sum * total);
        }

        //public static double YatesChiSquareTest(int[,] ctable);
        #endregion

        #region numerical optimization functions
        public static bool IsConverged(IList<double> values, int limit, double error)
        {
            var t = values.Count - 1;
            return limit >= 0 && t >= limit ||
                error >= 0 && t > 0 && values[t] <= values[0] && Mt.RelativeError(values[t], values[t - 1]) < error;
        }
        public static bool IsConverged(IList<double[]> parameters, int limit, double error)
        {
            var t = parameters.Count - 1;
            return limit >= 0 && t >= limit ||
                error >= 0 && t > 0 && Mt.DRelativeError(parameters[t], parameters[t - 1]) < error;
        }

        #region FISTA
        static unsafe double BoostFista<T>(int option, bool ascending, double bo, T* x, T* x_, int n) where T : unmanaged
        {
            var bt = Math.Sqrt(bo.Sq() + 0.25) + 0.5;
            if (option >= 1)
            {
                if (bo == 0) Us.Let(x_, x, n);
                else
                {
                    if (option == 2 && ascending)
                    {
                        var c = Op<T>.From(bo / bt);
                        for (int i = n; --i >= 0;) x[i] = Op.Add(x_[i], Op.Mul(c, Op.Sub(x[i], x_[i])));
                    }
                    else
                    {
                        //var c = Op<T>.From((bo - 1) / bt);
                        //for (int i = n; --i >= 0;) { var t = x[i]; x[i] = Op.Add(t, Op.Mul(c, Op.Sub(t, x_[i]))); x_[i] = t; }
                        var r = (1 - bo) / bt;
                        var c = Op<T>.From(1 - r);
                        var d = Op<T>.From(r);
                        for (int i = n; --i >= 0;) { var t = x[i]; x[i] = Op.Add(Op.Mul(t, c), Op.Mul(x_[i], d)); x_[i] = t; }
                    }
                }
            }
            return bt;
        }
        public static unsafe void ArgminAllFista<T>(int option, int N, T* X, Func<Array> gradf, Action<double>? argming, double LipschitzInv, Func<int, bool> converged) where T : unmanaged
        {
            var bt = 0.0;
            fixed (T* X_ = option == 0 ? null : new T[N])
                for (int step = 1; ; step++)
                {
                    bt = BoostFista(option, false, bt, X, X_, N);
                    var G = New.Fix<T>(gradf());
                    Us.LetAddMul(X, G.P(), Op<T>.From(-LipschitzInv), N);
                    G.Dispose();
                    argming?.Invoke(LipschitzInv);
                    if (converged(step)) return;
                }
        }
        public static unsafe void ArgminAllFista<T>(int option, int N, T* X, Func<Array> gradf, Action<double>? argming, Func<double> funcf, Func<double>? funcg, double LipschitzInv = 0, Func<IList<double>, bool>? converged = null) where T : unmanaged
        {
            static double DSumMulSub(T* g, T* x, T* y, int n) { var a = 0.0; for (int i = n; --i >= 0;) a += Op.CastDouble(Op.Mul(g[i], Op.Sub(x[i], y[i]))); return a; }
            static void MulAdd(T* x, T* g, T l, T* y, int n) { for (int i = n; --i >= 0;) x[i] = Op.Add(Op.Mul(g[i], l), y[i]); }
            funcg ??= () => 0.0;
            converged ??= list => Nm.IsConverged(list, 100, 1e-6);
            var lambda = LipschitzInv != 0 ? Math.Abs(LipschitzInv) : 1.0;
            var lambdaValid = 0.0;
            var fg = new List<double>() { funcf() + funcg() };
            if (converged(fg)) return;
            var bt = 0.0;
            var ascending = false;
            fixed (T* Y = new T[N], X_ = option == 0 ? null : new T[N])
                for (int step = 1; ; step++)
                {
                    bt = BoostFista(option, ascending, bt, X, X_, N);
                    var G = New.Fix<T>(gradf());
                    var fY = funcf();
                    Us.Let(Y, X, N);
                    double fX;
                    for (; ; lambda *= 0.8)
                    {
                        MulAdd(X, G.P(), Op<T>.From(-lambda), Y, N);
                        argming?.Invoke(lambda);
                        fX = funcf();
                        double fY_ = fY + DSumMulSub(G.P(), X, Y, N) + Us.DNorm2SqSub(X, Y, N) / (2 * lambda);
                        if (fX <= fY_ + fY_.Abs() * 1e-6) break;  //check descending
                        if (LipschitzInv > 0 || lambda < lambdaValid) { Warning.WriteLine($"FISTA {step}: LipschitzInv UNCORRECTABLE: {fX}>{fY_} at lambda={lambda}"); break; }
                    }
                    if (lambdaValid == 0) { lambdaValid = lambda * 1e-6; }
                    G.Dispose();
                    var fg_ = fX + funcg();
                    ascending = fg_ > fg[^1];
                    fg.Add((option == 2 && ascending) ? fg[^1] : fg_);
                    if (option == 0 && ascending) Warning.WriteLine($"ISTA {step}: ASCENDING, ratio: {Mt.RelativeError(fg[^1], fg[^2])}");
                    if (converged(fg)) return;
                }
        }
        public static unsafe void ArgminAllFista<T>(int option, Array<T> X, Func<Array> gradf, Action<double>? argming, double LipschitzInv, Func<int, bool> converged) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) { f<float>(); return; }
            if (typeof(T) == typeof(double)) { f<double>(); return; }
            if (typeof(T) == typeof(ComplexD)) { f<double>(); return; }
            if (typeof(T) == typeof(ComplexS)) { f<float>(); return; }
            ThrowException.NotImplemented();
            void f<B>() where B : unmanaged
            {
                using var x = New.Fix(X);
                ArgminAllFista<B>(option, X.Length * Op<T>.Fold, (B*)x.P(), gradf, argming, LipschitzInv, converged);
            }
        }
        public static unsafe void ArgminAllFista<T>(int option, Array<T> X, Func<Array> gradf, Action<double>? argming, Func<double> funcf, Func<double>? funcg, double LipschitzInv = 0, Func<IList<double>, bool>? converged = null) where T : unmanaged
        {
            if (typeof(T) == typeof(float)) { f<float>(); return; }
            if (typeof(T) == typeof(double)) { f<double>(); return; }
            if (typeof(T) == typeof(ComplexD)) { f<double>(); return; }
            if (typeof(T) == typeof(ComplexS)) { f<float>(); return; }
            ThrowException.NotImplemented();
            void f<B>() where B : unmanaged
            {
                using var x = New.Fix(X);
                ArgminAllFista<B>(option, X.Length * Op<T>.Fold, (B*)x.P(), gradf, argming, funcf, funcg, LipschitzInv, converged);
            }
        }

        public static T[] ArgminAllFista<T>(int option, T[] X, Func<T[], T[]> gradf, Func<T[], double, T[]>? argming, double LipschitzInv, Func<int, T[], bool> converged) where T : unmanaged
        {
            ArgminAllFista<T>(option, X, () => gradf(X), argming is null ? (Action<double>?)null : l => X.Let(argming!(X, l)), LipschitzInv, t => converged(t, X));
            return X;
        }
        public static T[] ArgminIsta<T>(T[] X, Func<T[], T[]> gradf, Func<T[], double, T[]>? argming, double LipschitzInv, Func<int, T[], bool> converged) where T : unmanaged => ArgminAllFista(0, X, gradf, argming, LipschitzInv, converged);
        public static T[] ArgminFista<T>(T[] X, Func<T[], T[]> gradf, Func<T[], double, T[]>? argming, double LipschitzInv, Func<int, T[], bool> converged) where T : unmanaged => ArgminAllFista(1, X, gradf, argming, LipschitzInv, converged);
        public static T[] ArgminAllFista<T>(int option, T[] X, Func<T[], T[]> gradf, Func<T[], double, T[]>? argming, Func<T[], double> funcf, Func<T[], double>? funcg, double LipschitzInv, Func<IList<double>, T[], bool> converged) where T : unmanaged
        {
            ArgminAllFista<T>(option, X, () => gradf(X), argming is null ? (Action<double>?)null : l => X.Let(argming!(X, l)), () => funcf(X), funcg is null ? (Func<double>?)null : () => funcg!(X), LipschitzInv, fg => converged(fg, X));
            return X;
        }
        public static T[] ArgminIsta<T>(T[] X, Func<T[], T[]> gradf, Func<T[], double, T[]>? argming, Func<T[], double> funcf, Func<T[], double> funcg, double LipschitzInv, Func<IList<double>, T[], bool> converged) where T : unmanaged => ArgminAllFista(0, X, gradf, argming, funcf, funcg, LipschitzInv, converged);
        public static T[] ArgminFista<T>(T[] X, Func<T[], T[]> gradf, Func<T[], double, T[]>? argming, Func<T[], double> funcf, Func<T[], double> funcg, double LipschitzInv, Func<IList<double>, T[], bool> converged) where T : unmanaged => ArgminAllFista(1, X, gradf, argming, funcf, funcg, LipschitzInv, converged);
        public static T[] ArgminMFista<T>(T[] X, Func<T[], T[]> gradf, Func<T[], double, T[]>? argming, Func<T[], double> funcf, Func<T[], double> funcg, double LipschitzInv, Func<IList<double>, T[], bool> converged) where T : unmanaged => ArgminAllFista(2, X, gradf, argming, funcf, funcg, LipschitzInv, converged);

        public static T[,] ArgminAllFista<T>(int option, T[,] X, Func<T[,], T[,]> gradf, Func<T[,], double, T[,]>? argming, double LipschitzInv, Func<int, T[,], bool> converged) where T : unmanaged
        {
            ArgminAllFista<T>(option, X, () => gradf(X), argming is null ? (Action<double>?)null : l => X.Let(argming!(X, l)), LipschitzInv, t => converged(t, X));
            return X;
        }
        public static T[,] ArgminIsta<T>(T[,] X, Func<T[,], T[,]> gradf, Func<T[,], double, T[,]>? argming, double LipschitzInv, Func<int, T[,], bool> converged) where T : unmanaged => ArgminAllFista(0, X, gradf, argming, LipschitzInv, converged);
        public static T[,] ArgminFista<T>(T[,] X, Func<T[,], T[,]> gradf, Func<T[,], double, T[,]>? argming, double LipschitzInv, Func<int, T[,], bool> converged) where T : unmanaged => ArgminAllFista(1, X, gradf, argming, LipschitzInv, converged);
        public static T[,] ArgminAllFista<T>(int option, T[,] X, Func<T[,], T[,]> gradf, Func<T[,], double, T[,]>? argming, Func<T[,], double> funcf, Func<T[,], double>? funcg, double LipschitzInv, Func<IList<double>, T[,], bool> converged) where T : unmanaged
        {
            ArgminAllFista<T>(option, X, () => gradf(X), argming is null ? (Action<double>?)null : l => X.Let(argming!(X, l)), () => funcf(X), funcg is null ? (Func<double>?)null : () => funcg!(X), LipschitzInv, fg => converged(fg, X));
            return X;
        }
        public static T[,] ArgminIsta<T>(T[,] X, Func<T[,], T[,]> gradf, Func<T[,], double, T[,]>? argming, Func<T[,], double> funcf, Func<T[,], double> funcg, double LipschitzInv, Func<IList<double>, T[,], bool> converged) where T : unmanaged => ArgminAllFista(0, X, gradf, argming, funcf, funcg, LipschitzInv, converged);
        public static T[,] ArgminFista<T>(T[,] X, Func<T[,], T[,]> gradf, Func<T[,], double, T[,]>? argming, Func<T[,], double> funcf, Func<T[,], double> funcg, double LipschitzInv, Func<IList<double>, T[,], bool> converged) where T : unmanaged => ArgminAllFista(1, X, gradf, argming, funcf, funcg, LipschitzInv, converged);
        public static T[,] ArgminMFista<T>(T[,] X, Func<T[,], T[,]> gradf, Func<T[,], double, T[,]>? argming, Func<T[,], double> funcf, Func<T[,], double> funcg, double LipschitzInv, Func<IList<double>, T[,], bool> converged) where T : unmanaged => ArgminAllFista(2, X, gradf, argming, funcf, funcg, LipschitzInv, converged);

        public static T[,,] ArgminAllFista<T>(int option, T[,,] X, Func<T[,,], T[,,]> gradf, Func<T[,,], double, T[,,]>? argming, double LipschitzInv, Func<int, T[,,], bool> converged) where T : unmanaged
        {
            ArgminAllFista<T>(option, X, () => gradf(X), argming is null ? (Action<double>?)null : l => X.Let(argming!(X, l)), LipschitzInv, t => converged(t, X));
            return X;
        }
        public static T[,,] ArgminIsta<T>(T[,,] X, Func<T[,,], T[,,]> gradf, Func<T[,,], double, T[,,]>? argming, double LipschitzInv, Func<int, T[,,], bool> converged) where T : unmanaged => ArgminAllFista(0, X, gradf, argming, LipschitzInv, converged);
        public static T[,,] ArgminFista<T>(T[,,] X, Func<T[,,], T[,,]> gradf, Func<T[,,], double, T[,,]>? argming, double LipschitzInv, Func<int, T[,,], bool> converged) where T : unmanaged => ArgminAllFista(1, X, gradf, argming, LipschitzInv, converged);
        public static T[,,] ArgminAllFista<T>(int option, T[,,] X, Func<T[,,], T[,,]> gradf, Func<T[,,], double, T[,,]>? argming, Func<T[,,], double> funcf, Func<T[,,], double>? funcg, double LipschitzInv, Func<IList<double>, T[,,], bool> converged) where T : unmanaged
        {
            ArgminAllFista<T>(option, X, () => gradf(X), argming is null ? (Action<double>?)null : l => X.Let(argming!(X, l)), () => funcf(X), funcg is null ? (Func<double>?)null : () => funcg!(X), LipschitzInv, fg => converged(fg, X));
            return X;
        }
        public static T[,,] ArgminIsta<T>(T[,,] X, Func<T[,,], T[,,]> gradf, Func<T[,,], double, T[,,]>? argming, Func<T[,,], double> funcf, Func<T[,,], double> funcg, double LipschitzInv, Func<IList<double>, T[,,], bool> converged) where T : unmanaged => ArgminAllFista(0, X, gradf, argming, funcf, funcg, LipschitzInv, converged);
        public static T[,,] ArgminFista<T>(T[,,] X, Func<T[,,], T[,,]> gradf, Func<T[,,], double, T[,,]>? argming, Func<T[,,], double> funcf, Func<T[,,], double> funcg, double LipschitzInv, Func<IList<double>, T[,,], bool> converged) where T : unmanaged => ArgminAllFista(1, X, gradf, argming, funcf, funcg, LipschitzInv, converged);
        public static T[,,] ArgminMFista<T>(T[,,] X, Func<T[,,], T[,,]> gradf, Func<T[,,], double, T[,,]>? argming, Func<T[,,], double> funcf, Func<T[,,], double> funcg, double LipschitzInv, Func<IList<double>, T[,,], bool> converged) where T : unmanaged => ArgminAllFista(2, X, gradf, argming, funcf, funcg, LipschitzInv, converged);
        #endregion

        #region Brent
        // NumericalRecipes3.rtbis
        public static double FindRootBisection(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 50;
            double dx, xmid, rtb;
            double f = func(x1);
            double fmid = func(x2);
            if (f * fmid >= 0) Warning.WriteLine("root must be bracketed");
            if (f < 0) { dx = x2 - x1; rtb = x1; }
            else { dx = x1 - x2; rtb = x2; }
            for (int j = 0; j < maxIteration; j++)
            {
                fmid = func(xmid = rtb + (dx *= 0.5));
                if (fmid <= 0) rtb = xmid;
                if (Math.Abs(dx) < xacc || fmid == 0) return rtb;
            }
            Warning.TooManyIterations();
            return double.NaN;
        }

        // NumericalRecipes3.rtflsp
        public static double FindRootFalsePosition(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 30;
            double xl, xh, del;
            double fl = func(x1);
            double fh = func(x2);
            if (fl * fh > 0) Warning.WriteLine("root must be bracketed");
            if (fl < 0) { xl = x1; xh = x2; }
            else { xl = x2; xh = x1; Ex.Swap(ref fl, ref fh); }
            double dx = xh - xl;
            for (int j = 0; j < maxIteration; j++)
            {
                double rtf = xl + dx * fl / (fl - fh);
                double f = func(rtf);
                if (f < 0.0) { del = xl - rtf; xl = rtf; fl = f; }
                else { del = xh - rtf; xh = rtf; fh = f; }
                dx = xh - xl;
                if (Math.Abs(del) < xacc || f == 0) return rtf;
            }
            Warning.TooManyIterations();
            return double.NaN;
        }

        // NumericalRecipes3.rtsec
        public static double FindRootSecant(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 30;
            double xl, rts;
            double fl = func(x1);
            double f = func(x2);
            if (Math.Abs(fl) < Math.Abs(f)) { rts = x1; xl = x2; Ex.Swap(ref fl, ref f); }
            else { xl = x1; rts = x2; }
            for (int j = 0; j < maxIteration; j++)
            {
                double dx = (xl - rts) * f / (f - fl);
                xl = rts;
                fl = f;
                rts += dx;
                f = func(rts);
                if (Math.Abs(dx) < xacc || f == 0) return rts;
            }
            Warning.TooManyIterations();
            return double.NaN;
        }

        // NumericalRecipes3.zriddr
        public static double FindRootRidders(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 60;
            double fl = func(x1);
            double fh = func(x2);
            if ((fl > 0 && fh < 0) || (fl < 0 && fh > 0))
            {
                double xl = x1;
                double xh = x2;
                double ans = -9.99e99;
                for (int j = 0; j < maxIteration; j++)
                {
                    double xm = 0.5 * (xl + xh);
                    double fm = func(xm);
                    double s = Math.Sqrt(fm * fm - fl * fh);
                    if (s == 0.0) return ans;
                    double xnew = xm + (xm - xl) * ((fl >= fh ? 1 : -1) * fm / s);
                    if (Math.Abs(xnew - ans) <= xacc) return ans;
                    ans = xnew;
                    double fnew = func(ans);
                    if (fnew == 0.0) return ans;
                    if (SameSign(fm, fnew) != fm) { xl = xm; fl = fm; xh = ans; fh = fnew; }
                    else if (SameSign(fl, fnew) != fl) { xh = ans; fh = fnew; }
                    else if (SameSign(fh, fnew) != fh) { xl = ans; fl = fnew; }
                    else ThrowException.InvalidOperation("never get here.");
                    if (Math.Abs(xh - xl) <= xacc) return ans;
                }
                Warning.TooManyIterations();
            }
            else
            {
                if (fl == 0) return x1;
                if (fh == 0) return x2;
                Warning.WriteLine("root must be bracketed");
            }
            return double.NaN;
        }

        // NumericalRecipes3.zbrent
        public static double FindRootBrent(Func<double, double> function, double start, double end, double tolerance)
        {
            const int maxIteration = 100;
            double a = start, b = end, c = end, d = 0, e = 0;
            double fa = function(a), fb = function(b), fc = fb;
            if ((fa > 0 && fb > 0) || (fa < 0 && fb < 0)) ThrowException.Argument("root must be bracketed");
            for (int i = 0; i < maxIteration; i++)
            {
                if ((fb > 0 && fc > 0) || (fb < 0 && fc < 0)) { c = a; fc = fa; e = d = b - a; }
                if (Math.Abs(fc) < Math.Abs(fb)) { a = b; b = c; c = a; fa = fb; fb = fc; fc = fa; }
                double tol = 2 * Mt.DoubleEps * Math.Abs(b) + tolerance / 2;
                double xm = (c - b) / 2;
                if (Math.Abs(xm) <= tol || fb == 0) return b;
                if (Math.Abs(e) >= tol && Math.Abs(fa) > Math.Abs(fb))
                {  // 逆二乗補間
                    double s = fb / fa, p, q;
                    if (a == c)
                    {
                        p = 2 * xm * s;
                        q = 1 - s;
                    }
                    else
                    {
                        double t = fa / fc;
                        double r = fb / fc;
                        p = s * (2 * xm * t * (t - r) - (b - a) * (r - 1));
                        q = (t - 1) * (r - 1) * (s - 1);
                    }
                    if (p > 0) q *= -1; else p *= -1;
                    if (2 * p < Math.Min(3 * xm * q - Math.Abs(tol * q), Math.Abs(e * q))) { e = d; d = p / q; }  // 成功
                    else { d = xm; e = d; }  // 二分法
                }
                else { d = xm; e = d; }  // 二分法
                a = b; fa = fb;
                b += (Math.Abs(d) > tol) ? d : (xm >= 0 ? tol : -tol);
                fb = function(b);
            }
            Warning.TooManyIterations();
            return double.NaN;
        }

        // NumericalRecipes3.rtnewt
        public static double FindRootNewtonRaphson(Func<double, double> funcd, Func<double, double> funcddf, double x1, double x2, double xacc)
        {
            const int JMAX = 20;
            double rtn = 0.5 * (x1 + x2);
            for (int j = 0; j < JMAX; j++)
            {
                double f = funcd(rtn);
                double df = funcddf(rtn);
                double dx = f / df;
                rtn -= dx;
                if ((x1 - rtn) * (rtn - x2) < 0) Warning.WriteLine("jumped out of brackets");
                if (Math.Abs(dx) < xacc) return rtn;
            }
            Warning.TooManyIterations();
            return double.NaN;
        }

        // NumericalRecipes3.rtsafe
        public static double FindRootNewtonRaphsonBisection(Func<double, double> funcd, Func<double, double> funcddf, double x1, double x2, double xacc)
        {
            const int maxIteration = 100;
            double xh, xl;
            double fl = funcd(x1);
            double fh = funcd(x2);
            if ((fl > 0 && fh > 0) || (fl < 0 && fh < 0)) Warning.WriteLine("root must be bracketed");
            if (fl == 0) return x1;
            if (fh == 0) return x2;
            if (fl < 0) { xl = x1; xh = x2; }
            else { xh = x1; xl = x2; }
            double rts = 0.5 * (x1 + x2);
            double dxold = Math.Abs(x2 - x1);
            double dx = dxold;
            double f = funcd(rts);
            double df = funcddf(rts);
            for (int j = 0; j < maxIteration; j++)
            {
                if ((((rts - xh) * df - f) * ((rts - xl) * df - f) > 0) || (Math.Abs(2 * f) > Math.Abs(dxold * df)))
                {
                    dxold = dx;
                    dx = 0.5 * (xh - xl);
                    rts = xl + dx;
                    if (xl == rts) return rts;
                }
                else
                {
                    dxold = dx;
                    dx = f / df;
                    double temp = rts;
                    rts -= dx;
                    if (temp == rts) return rts;
                }
                if (Math.Abs(dx) < xacc) return rts;
                f = funcd(rts);
                df = funcddf(rts);
                if (f < 0) xl = rts;
                else xh = rts;
            }
            Warning.TooManyIterations();
            return double.NaN;
        }

        static double SameSign(double a, double b) => b >= 0 ? (a >= 0 ? a : -a) : (a >= 0 ? -a : a);
        static double MulSign(double a, double b) => b >= 0 ? a : -a;

        // NumericalRecipes3.Bracketmethod
        static (double v0, double v1, double v2) CalcBracket(double a, double b, Func<double, double> func)
        {
            const double GOLD = 1.618034;
            const double GLIMIT = 100;
            const double TINY = 1e-20;
            var ax = a;
            var bx = b;
            var fa = func(ax);
            var fb = func(bx);
            if (fb > fa)
            {
                Ex.Swap(ref ax, ref bx);
                Ex.Swap(ref fb, ref fa);
            }
            var cx = bx + GOLD * (bx - ax);
            var fc = func(cx);
            while (fb > fc)
            {
                double r = (bx - ax) * (fb - fc);
                double q = (bx - cx) * (fb - fa);
                double u = bx - ((bx - cx) * q - (bx - ax) * r) / (2.0 * SameSign(Math.Max(Math.Abs(q - r), TINY), q - r));
                double ulim = bx + GLIMIT * (cx - bx);
                double fu;
                if ((bx - u) * (u - cx) > 0)
                {
                    fu = func(u);
                    if (fu < fc)
                    {
                        ax = bx; bx = u;
                        //fa = fb; fb = fu;
                        break;
                    }
                    else if (fu > fb)
                    {
                        cx = u;
                        //fc = fu;
                        break;
                    }
                    u = cx + GOLD * (cx - bx);
                    fu = func(u);
                }
                else if ((cx - u) * (u - ulim) > 0)
                {
                    fu = func(u);
                    if (fu < fc)
                    {
                        bx = cx; cx = u; u += GOLD * (u - cx);
                        fb = fc; fc = fu; fu = func(u);
                    }
                }
                else if ((u - ulim) * (ulim - cx) >= 0)
                {
                    u = ulim;
                    fu = func(u);
                }
                else
                {
                    u = cx + GOLD * (cx - bx);
                    fu = func(u);
                }
                ax = bx; bx = cx; cx = u;
                fa = fb; fb = fc; fc = fu;
            }
            return (ax, bx, cx);
        }

        // NumericalRecipes3.Golden
        public static (double v0, double v1) MinimizeGolden(Func<double, double> func, double ax, double bx, double cx)
        {
            const double tol = 3e-8;
            const double R = 0.61803399;
            const double C = 1 - R;

            double x1, x2;
            double x0 = ax;
            double x3 = cx;
            if (Math.Abs(cx - bx) > Math.Abs(bx - ax))
            {
                x1 = bx;
                x2 = bx + C * (cx - bx);
            }
            else
            {
                x2 = bx;
                x1 = bx - C * (bx - ax);
            }
            double f1 = func(x1);
            double f2 = func(x2);
            while (Math.Abs(x3 - x0) > tol * (Math.Abs(x1) + Math.Abs(x2)))
            {
                if (f2 < f1)
                {
                    x0 = x1; x1 = x2; x2 = R * x2 + C * x3;
                    f1 = f2; f2 = func(x2);
                }
                else
                {
                    x3 = x2; x2 = x1; x1 = R * x1 + C * x0;
                    f2 = f1; f1 = func(x1);
                }
            }
            return f1 < f2 ? (x1, f1) : (x2, f2);
        }

        // NumericalRecipes3.Brent
        static (double v0, double v1) MinimizeBrent(Func<double, double> func, (double v0, double v1, double v2) bracket)
        {
            //Console.Write("Brent:");
            const double tolerance = 3e-8;
            const int maxIteration = 100;
            const double goldSection = 0.3819660;
            const double ZEPS = Mt.DoubleEps * 1e-3;

            var a = bracket.v0;
            var b = bracket.v2;
            Mt.LetOrder(ref a, ref b);
            double v, w, x; v = w = x = bracket.v1;
            double fv, fw, fx; fv = fw = fx = func(x);
            double d = 0.0, e = 0.0;
            for (int iteration = 0; ; iteration++)
            {
                if (iteration == maxIteration) { Warning.TooManyIterations(); break; }
                var m = 0.5 * (a + b);
                var tol1 = tolerance * Math.Abs(x) + ZEPS;
                var tol2 = 2.0 * tol1;
                if (Math.Abs(x - m) <= (tol2 - 0.5 * (b - a))) break;

                double be = e, bd = d;
                e = (x >= m ? a : b) - x; d = goldSection * e;  //黄金分割
                if (Math.Abs(be) > tol1)
                {
                    var vw = (x - v) * (fx - fw);
                    var wv = (x - w) * (fx - fv);
                    var p = (x - v) * vw - (x - w) * wv;
                    var q = 2.0 * (vw - wv);
                    if (q > 0) p *= -1; else q *= -1;
                    if (Math.Abs(p) < Math.Abs(0.5 * q * be) && p > q * (a - x) && p < q * (b - x))
                    {
                        e = bd; d = p / q;  //放物線補間
                        if (x + d - a < tol2 || b - (x + d) < tol2) d = MulSign(tol1, m - x);
                    }
                }

                var u = x + (Math.Abs(d) >= tol1 ? d : MulSign(tol1, d));
                var fu = func(u);
                if (fu <= fx)
                {
                    if (u >= x) a = x; else b = x;
                    v = w; fv = fw;
                    w = x; fw = fx;
                    x = u; fx = fu;
                }
                else
                {
                    if (u < x) a = u; else b = u;
                    if (fu <= fw || w == x)
                    {
                        v = w; fv = fw;
                        w = u; fw = fu;
                    }
                    else if (fu <= fv || v == x || v == w)
                    {
                        v = u; fv = fu;
                    }
                }
            }
            return (x, fx);
        }

        // NumericalRecipes3.Dbrent
        public static (double v0, double v1) MinimizeDbrent(Func<double, double> funcd, Func<double, double> gradient, (double v0, double v1, double v2) bracket)
        {
            const double tol = 3e-8;
            const int ITMAX = 100;
            const double ZEPS = Mt.DoubleEps * 1e-3;

            var a = bracket.v0;
            var b = bracket.v2;
            Mt.LetOrder(ref a, ref b);
            double v, w, x; x = w = v = bracket.v1;
            double fv, fw, fx; fw = fv = fx = funcd(x);
            double dv, dw, dx; dw = dv = dx = gradient(x);
            double d = 0.0, e = 0.0;
            for (int iterations = 0; ; iterations++)
            {
                if (iterations == ITMAX) { Warning.TooManyIterations(); break; }
                var m = 0.5 * (a + b);
                var tol1 = tol * Math.Abs(x) + ZEPS;
                var tol2 = 2.0 * tol1;
                if (Math.Abs(x - m) <= (tol2 - 0.5 * (b - a))) break;
                if (Math.Abs(e) > tol1)
                {
                    var d1 = 2 * (b - a);
                    var d2 = d1;
                    if (dw != dx) d1 = (w - x) * dx / (dx - dw);
                    if (dv != dx) d2 = (v - x) * dx / (dx - dv);
                    var u1 = x + d1;
                    var u2 = x + d2;
                    var ok1 = (a - u1) * (u1 - b) > 0.0 && dx * d1 <= 0.0;
                    var ok2 = (a - u2) * (u2 - b) > 0.0 && dx * d2 <= 0.0;
                    var olde = e;
                    e = d;
                    if (ok1 || ok2)
                    {
                        if (ok1 && ok2)
                            d = (Math.Abs(d1) < Math.Abs(d2) ? d1 : d2);
                        else if (ok1)
                            d = d1;
                        else
                            d = d2;
                        if (Math.Abs(d) <= Math.Abs(0.5 * olde))
                        {
                            if (x + d - a < tol2 || b - (x + d) < tol2) d = SameSign(tol1, m - x);
                        }
                        else
                        {
                            e = (dx >= 0 ? a - x : b - x);
                            d = 0.5 * e;
                        }
                    }
                    else
                    {
                        e = (dx >= 0 ? a - x : b - x);
                        d = 0.5 * e;
                    }
                }
                else
                {
                    e = (dx >= 0 ? a - x : b - x);
                    d = 0.5 * e;
                }

                double u;
                double fu;
                if (Math.Abs(d) >= tol1)
                {
                    u = x + d;
                    fu = funcd(u);
                }
                else
                {
                    u = x + MulSign(tol1, d);
                    fu = funcd(u);
                    if (fu > fx) break;
                }
                var du = gradient(u);
                if (fu <= fx)
                {
                    if (u >= x) a = x; else b = x;
                    v = w; fv = fw; dv = dw;
                    w = x; fw = fx; dw = dx;
                    x = u; fx = fu; dx = du;
                }
                else
                {
                    if (u < x) a = u; else b = u;
                    if (fu <= fw || w == x)
                    {
                        v = w; fv = fw; dv = dw;
                        w = u; fw = fu; dw = du;
                    }
                    else if (fu < fv || v == x || v == w)
                    {
                        v = u; fv = fu; dv = du;
                    }
                }
            }
            return (fx, x);
        }
        #endregion

        #region Conjugate gradient discent
        // NumericalRecipes3.Linemethod
        static (double[] v0, double v1) ArgminAlongLine(double[] argument, double[] direction, Func<double[], double> function)
        {
            double newfunction(double x) => function(Mt.AddMul(argument, direction, x));
            var bracket = CalcBracket(0.0, 1.0, newfunction);
            var (v0, v1) = MinimizeBrent(newfunction, bracket);
            var arg = Mt.AddMul(argument, direction, v0);
            return (arg, v1);
        }

        // NumericalRecipes3.Frprmn
        public static (double[], double) ArgminConjugateGradient(double[] initial, Func<double[], double[]> gradient, Func<double[], double> function, int iterationsMax = 200)
        {
            const double EPS = 1e-18;
            const double toleranceFunction = 3e-8;
            const double toleranceGradient = 1e-8;

            int n = initial.Length;
            var arg = initial.CloneX();
            var func = function(arg);
            var grad = gradient(arg).LetNeg();
            var g = grad.CloneX();
            for (int iterations = 0; ; iterations++)
            {
                if (iterations == iterationsMax)
                {
                    Warning.TooManyIterations();
                    break;
                }
                var h = grad.CloneX();
                var backup = func;
                (arg, func) = ArgminAlongLine(arg, grad, function);
                if (2 * Math.Abs(func - backup) / (Math.Abs(func) + Math.Abs(backup) + EPS) <= toleranceFunction) break;
                grad = gradient(arg).LetNeg();
                if (Ex.Max(n, j => Math.Abs(grad[j]) * Math.Max(Math.Abs(arg[j]), 1)) / Math.Max(func, 1) < toleranceGradient) break;

                var gg = g.DNorm2Sq();
                if (gg == 0) break;
                double gam = (grad.DNorm2Sq() - Mt.Inner(grad, g)) / gg;
                g = grad.CloneX();
                grad = Mt.AddMul(g, h, gam);
            }
            return (arg, func);
        }
        #endregion

        #region Downhill simplex
        // NumericalRecipes3.Amoeba
        public static (double[], double) ArgminDownhillSimplex(Func<double[], double> function, double[] initialArgment, double delta, double tolerance, int maxFunctionCall = 5000)
        {
            return ArgminDownhillSimplex(function, initialArgment, New.Array(initialArgment.Length, delta), tolerance, maxFunctionCall);
        }
        public static (double[], double) ArgminDownhillSimplex(Func<double[], double> function, double[] initialArgment, double[] delta, double tolerance, int maxFunctionCall = 5000)
        {
            var points = New.Array(initialArgment.Length + 1, i =>
            {
                var p = initialArgment.CloneX();
                if (i > 0) p[i - 1] += delta[i - 1];
                return p;
            });
            return ArgminDownhillSimplex(function, points, tolerance, maxFunctionCall);
        }
        public static (double[], double) ArgminDownhillSimplex(Func<double[], double> function, double[][] initialArgs, double tolerance, int maxFunctionCall = 5000)
        {
            const double TINY = 1e-10;
            var args = initialArgs.Select(v => v.CloneX()).ToArray();
            var argSum = args.SumFwrd();
            var values = args.Select(function).ToArray();

            // NumericalRecipes3.Amoeba.amotry
            double tryfunc(int iMax, double fac)
            {
                double fac1 = (1.0 - fac) / argSum.Length;
                double fac2 = fac1 - fac;
                var argTry = Mt.Mul(argSum, fac1).LetAddMul(args[iMax], -fac2);
                double valueTry = function(argTry);
                if (valueTry < values[iMax])
                {
                    values[iMax] = valueTry;
                    argSum.LetAdd(argTry).LetSub(args[iMax]);
                    args[iMax] = argTry;
                }
                return valueTry;
            }

            int countFunctionCall = 0;
            int iMin;
            while (true)
            {
                iMin = 0;
                int iMax = values[0] > values[1] ? 0 : 1;
                int iMax2 = 1 - iMax;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] <= values[iMin]) iMin = i;
                    if (values[i] > values[iMax]) { iMax2 = iMax; iMax = i; }
                    else if (values[i] > values[iMax2] && i != iMax) iMax2 = i;
                }
                if (2.0 * Math.Abs(values[iMax] - values[iMin]) / (Math.Abs(values[iMax]) + Math.Abs(values[iMin]) + TINY) < tolerance) break;
                if (countFunctionCall >= maxFunctionCall)
                {
                    //Warning.TooManyIterations();
                    break;
                }

                countFunctionCall += 2;
                double valueTry = tryfunc(iMax, -1);
                if (valueTry <= values[iMin]) { tryfunc(iMax, 2); continue; }
                if (valueTry < values[iMax2]) { countFunctionCall--; continue; }

                double valueSave = values[iMax];
                valueTry = tryfunc(iMax, 0.5);
                if (valueTry >= valueSave)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (i == iMin) continue;
                        args[i].LetAdd(args[iMin]).LetMul(0.5);
                        values[i] = function(args[i]);
                    }
                    countFunctionCall += values.Length - 1;
                    argSum = args.SumFwrd();
                }
            }
            return (args[iMin].CloneX(), values[iMin]);
        }

        // NumericalRecipes1.Ameba
        public static double[] ArgminDownhillSimplex1(Func<double[], double> func, double[] initialArgument, double[] delta, double tolerance)
        {
            const int maxFunctionCall = 5000;
            int countFunctionCall = 0;
            int D = initialArgument.Length;

            double[][] args = New.Array(D + 1, j => New.Array(D, i => initialArgument[i] + (i == j ? delta[i] : 0)));
            double[] argSum = New.Array(D, i => Mt.SumFwrd(D + 1, j => args[j][i]));
            double[] argTry = new double[D];
            double[] values = args.Select(func).ToArray();

            double tryfunc(int i, double fac)
            {
                double[] arg = args[i];
                double fac1 = (1 - fac) / D;
                double fac2 = fac1 - fac;
                for (int j = D; --j >= 0;)
                    argTry[j] = argSum[j] * fac1 - arg[j] * fac2;

                double y = func(argTry);
                if (values[i] > y)
                {
                    values[i] = y;
                    for (int j = D; --j >= 0;)
                    {
                        argSum[j] += argTry[j] - arg[j];
                        arg[j] = argTry[j];
                    }
                }
                return y;
            }

            while (true)
            {
                int iMin = 0;
                int iMax = values[0] > values[1] ? 0 : 1;
                int iMax2 = 1 - iMax;
                for (int i = 0; i < D + 1; i++)
                {
                    double v = values[i];
                    if (v <= values[iMin]) iMin = i;
                    if (v > values[iMax]) { iMax2 = iMax; iMax = i; }
                    else if (v > values[iMax2] && i != iMax) iMax2 = i;
                }

                double d = 2.0 * Math.Abs(values[iMax] - values[iMin])
                    / (Math.Abs(values[iMax]) + Math.Abs(values[iMin]) + Mt.DoubleEps);
                if (d < tolerance) break;
                if (countFunctionCall >= maxFunctionCall)
                {
                    Warning.TooManyIterations();
                    break;
                }

                countFunctionCall += 2;
                double valueTry = tryfunc(iMax, -1);
                if (valueTry <= values[iMin]) { tryfunc(iMax, 2); continue; }
                if (valueTry < values[iMax2]) { countFunctionCall--; continue; }

                double valueSave = values[iMax];
                valueTry = tryfunc(iMax, 0.5);
                if (valueTry < valueSave) continue;

                countFunctionCall += D;
                for (int i = 0; i < D + 1; i++)
                {
                    if (i == iMin) continue;
                    for (int j = 0; j < D; j++)
                        args[i][j] = (args[i][j] + args[iMin][j]) / 2;
                    values[i] = func(args[i]);
                }
                for (int i = D; --i >= 0;)
                    argSum[i] = Mt.SumFwrd(D + 1, j => args[j][i]);
            }
            return New.Array(D, i => Mt.AvgFwrd(D + 1, j => args[j][i]));
        }
        #endregion

        #endregion

        #region TV and FGP
        static unsafe double NormTV<T>(int[] nn, T* p) where T : unmanaged
        {
            var I = new MultiDimensionalIndexer(nn);
            var a = new SumPair<double>();
            for (int i = 0; i < I.n; i++)
            {
                I.Dec_();
                var s = 0.0;
                for (int k = I.dim; --k >= 0;)
                    if (I.ii[k] > 0) s += Op.DAbsSqSub(p[i], p[i + I.oo[k]]);
                a.Add(s.Sqrt());
            }
            return a.Sum();
        }
        static unsafe double NormTV_<T>(Array<T> image) where T : unmanaged { using var p = New.Fix(image); return NormTV<T>(image.GetLengths(), p.P()); }
        public static double NormTV<T>(T[,] image) where T : unmanaged => NormTV_<T>(image);
        public static double NormTV<T>(T[,,] image) where T : unmanaged => NormTV_<T>(image);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T BAbsSq<C, T>(C x) where C : unmanaged where T : unmanaged
        {
            if (typeof(C) == typeof(T)) return (T)(object)Op.Sq(x);
            if (typeof(C) == typeof(ComplexS)) return (T)(object)Op.BAbsSq((ComplexS)(object)x);
            if (typeof(C) == typeof(ComplexD)) return (T)(object)Op.BAbsSq((ComplexD)(object)x);
            return ThrowException.NotImplemented<T>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LetDivB<C, T>(ref C x, T y) where C : unmanaged where T : unmanaged
        {
            if (typeof(C) == typeof(T)) { Op.Div(out x, x, (C)(object)y); return; }
            if (typeof(C) == typeof(ComplexS)) { Op.DivB(out ComplexS a, (ComplexS)(object)x, (Single)(object)y); x = (C)(object)a; return; }
            if (typeof(C) == typeof(ComplexD)) { Op.DivB(out ComplexD a, (ComplexD)(object)x, (Double)(object)y); x = (C)(object)a; return; }
            ThrowException.NotImplemented();
        }
        static unsafe void LetAddLetMul<T>(T* r, T* p, T q, int n) where T : unmanaged
        {
            //if (Op.Equ(q, 0)) { Us.Clear(r, n); return; }
            //if (Op.Equ(q, 1)) { Us.LetAdd(r, p, n); return; }
            for (int i = n; --i >= 0;) { Op.Add(out T a, r[i], p[i]); Op.Mul(out r[i], a, q); }
        }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void FastGradientProjection<C, T>(Array<C> image, double lambda, double eps, Func<int, bool> converged) where C : unmanaged where T : unmanaged
        {
            var nn = image.GetLengths();
            var I = new MultiDimensionalIndexer(nn);
            var n = I.n;
            var nf = n * Op<C>.Fold;
            var dim = I.dim;
            var ndim = n * dim;
            var ndimf = ndim * Op<C>.Fold;
            var LipschitzInv = 1 / (dim * 4 * lambda);

            using var _X = New.Fix(image);
            fixed (C* _A = new C[n])
            fixed (C* _Y = new C[ndim])
            fixed (C* _Y_ = new C[ndim])
            {
                C* X = _X.P();
                C* A = _A;
                C* Y = _Y;
                C* Y_ = _Y_;
                var XX = new C*[dim]; for (int k = dim; --k >= 0;) XX[k] = &X[I.oo[k]];
                var p = Op<T>.From(-lambda);
                var q = Op<T>.From(-lambda * -LipschitzInv);
                Us.Div((T*)A, (T*)X, p, nf);
                void YtoX(T scale)
                {
                    Us.Clear(X, n);
                    for (int i = 0, j = 0; i < n; i++, j += dim)
                    {
                        I.Dec_();
                        for (int k = dim; --k >= 0;)
                            if (I.ii[k] > 0)
                            {
                                var Yki = Y[j + k];
                                Op.LetAdd(ref X[i], Yki);
                                Op.LetSub(ref XX[k][i], Yki);
                            }
                    }
                    LetAddLetMul((T*)X, (T*)A, scale, nf);
                }
                double funcf()
                {
                    YtoX(p);
                    return Us.DNorm2Sq(X, n) / (2 * lambda);  //双対問題
                    //return Us.DNorm2SqSub(X, A * p, n) / (2 * lambda) + Nm.NormTV(nn, X);  //主問題
                }
                void do_gradf_argming()
                {
                    YtoX(q);
                    for (int i = 0, j = 0; i < n; i++, j += dim)
                    {
                        I.Dec();
                        C* Yj = &Y[j];
                        T s = default;
                        for (int k = dim; --k >= 0;)
                            if (I.ii[k] > 0)
                            {
                                Op.Sub(out var Gki, XX[k][i], X[i]);
                                Op.LetAdd(ref Gki, Yj[k]);
                                Op.LetAdd(ref s, BAbsSq<C, T>(Gki));
                                Yj[k] = Gki;
                            }
                        if (Op.GT(s, 1))
                        {
                            s = Op.Sqrt(s);
                            for (int k = dim; --k >= 0;)
                                if (I.ii[k] > 0)
                                    LetDivB(ref Yj[k], s);
                        }
                    }
                }

                var fg = new List<double>() { funcf() };
                var tt = 0;
                //var sw = new StopWatch();
                while (true)
                {
                    var restart = false;
                    var bt = 0.0;
                    for (int step = 1; ; step++)
                    {
                        bt = BoostFista(1, false, bt, (T*)Y, (T*)Y_, ndimf);
                        do_gradf_argming();
                        if (converged_(step)) break;
                    }
                    bool converged_(int t)
                    {
                        tt++;
                        if (Math.Sqrt(t) % 1 == 0)
                        {
                            fg.Add(funcf());
                            Console.WriteLine($"FGP {tt}:\t{fg[^1]:e8}\t{(fg[^1] - fg[^2]) / fg[^2]:f8}");
                            //if (t == 100) { Console.WriteLine($"{sw}"); return true; }
                            if (IsConverged(fg, -1, eps * (Math.Sqrt(t) * 2 - 1)) && converged(tt)) return true;
                            if (t == 10000) { restart = true; Console.WriteLine($"FGP {tt}: restart"); return true; }
                        }
                        return false;
                    }
                    if (!restart) break;
                }
                YtoX(p);
            }
        }

        public static Array<T> FastGradientProjection<T>(Array<T> image, double lambda, Func<int, Array<T>, bool> converged) where T : unmanaged
        {
            if (Op<T>.Fold == 1) main<T>();
            else if (typeof(T) == typeof(ComplexD)) main<double>();
            else if (typeof(T) == typeof(ComplexS)) main<float>();
            else ThrowException.NotImplemented();
            void main<B>() where B : unmanaged
            {
                FastGradientProjection<T, B>(image, lambda, 5e-10, t => converged(t, image));
            }
            return image;
        }
        public static T[,] ArgminTV<T>(T[,] image, double lambda, Func<int, T[,], bool> converged) where T : unmanaged => (T[,])FastGradientProjection<T>(image, lambda, (t, a) => converged(t, (T[,])a));
        public static T[,,] ArgminTV<T>(T[,,] image, double lambda, Func<int, T[,,], bool> converged) where T : unmanaged => (T[,,])FastGradientProjection<T>(image, lambda, (t, a) => converged(t, (T[,,])a));
        public static T[,] ArgminTV<T>(T[,] image, double lambda, int maxstep) where T : unmanaged => (T[,])FastGradientProjection<T>(image, lambda, (t, a) => t == maxstep);
        public static T[,,] ArgminTV<T>(T[,,] image, double lambda, int maxstep) where T : unmanaged => (T[,,])FastGradientProjection<T>(image, lambda, (t, a) => t == maxstep);
        #endregion

        #region SheppLoganPhantom
        public static float[,] SheppLoganPhantom(int size, int id = 1)
        {
            //数学的Y座標．計算機的Y座標ではないので描画時はY座標を反転させること
            //[-1,+1]^2 x [0,1] の範囲
            //CenterXY AxisXY Theta[deg] GrayLevel
            var Ellipses = new[] {
                //L.A.Shepp and B.F.Logan, The Fourier Reconstruction of a Head Section, IEEE Transactions on Nuclear Science, Vol.NS-21, June 1974.
                new[] {
                    new [] { +0.00, +0.0000, 0.6900, 0.920,   0, +2.00 },
                    new [] { +0.00, -0.0184, 0.6624, 0.874,   0, -0.98 },
                    new [] { +0.22, +0.0000, 0.1100, 0.310, -18, -0.02 },
                    new [] { -0.22, +0.0000, 0.1600, 0.410, +18, -0.02 },
                    new [] { +0.00, +0.3500, 0.2100, 0.250,   0, +0.01 },
                    new [] { +0.00, +0.1000, 0.0460, 0.046,   0, +0.01 },
                    new [] { +0.00, -0.1000, 0.0460, 0.046,   0, +0.01 },
                    new [] { -0.08, -0.6050, 0.0460, 0.023,   0, +0.01 },
                    new [] { +0.00, -0.6050, 0.0230, 0.023,   0, +0.01 },
                    new [] { +0.06, -0.6050, 0.0230, 0.046,   0, +0.01 },
                },
                new[] {
                    new [] { +0.00, +0.0000, 0.6900, 0.920,   0, +2.0 },
                    new [] { +0.00, -0.0184, 0.6624, 0.874,   0, -1.6 },
                    new [] { +0.22, +0.0000, 0.1100, 0.310, -18, -0.2 },
                    new [] { -0.22, +0.0000, 0.1600, 0.410, +18, -0.2 },
                    new [] { +0.00, +0.3500, 0.2100, 0.250,   0, +0.2 },
                    new [] { +0.00, +0.1000, 0.0460, 0.046,   0, +0.2 },
                    new [] { +0.00, -0.1000, 0.0460, 0.046,   0, +0.2 },
                    new [] { -0.08, -0.6050, 0.0460, 0.023,   0, +0.2 },
                    new [] { +0.00, -0.6050, 0.0230, 0.023,   0, +0.2 },
                    new [] { +0.06, -0.6050, 0.0230, 0.046,   0, +0.2 },
                },
            };
            var result = new float[size, size];
            var size2 = size / 2.0;
            int RealToLogical(double x) => Mt.Round((x + 1) * size2);
            double LogicalToReal(double x) => x / size2 - 1;
            foreach (var e in Ellipses[id])
            {
                var center = new Double2(e[0], e[1]);
                var axes = new Double2(e[2], e[3]);
                var maxaxis = Math.Max(axes.v0, axes.v1);
                var theta = e[4] * (Math.PI / 180);
                var level = (float)(e[5] / 2);

                var cos = Math.Cos(theta * 2);
                var sin = Math.Sin(theta * 2);
                var a = 1 / axes.X.Sq();
                var b = 1 / axes.Y.Sq();
                var f = a + b + (a - b) * cos;
                int y0 = RealToLogical(-maxaxis + center.Y);
                int y1 = RealToLogical(+maxaxis + center.Y);
                for (int iy = y0; iy <= y1; iy++)
                {
                    var y = LogicalToReal(iy) - center.Y;
                    var d = 2 * (f - 2 * a * b * y.Sq());
                    if (d < 0) continue;
                    d = Math.Sqrt(d);
                    int x0 = RealToLogical(((b - a) * sin * y - d) / f + center.X);
                    int x1 = RealToLogical(((b - a) * sin * y + d) / f + center.X);
                    for (int ix = x0; ix <= x1; ix++)
                        result[iy, ix] += level;
                }
            }
            return result;
        }
        public static float[,,] SheppLoganPhantom3D(int size, int id = 0)
        {
            //数学的Y座標．計算機的Y座標ではないので描画時はY座標を反転させること
            //[-1,+1]^3 x [0,1] の範囲
            //CenterXYZ AxisXYZ Theta[deg] GrayLevel
            var Ellipses = new[] {
                //C.G.Koay, J.E.Sarlls, E.O:zarslan, Three-Dimensional Analytical Magnetic Resonance Imaging Phantom in the Fourier Domain, Magnetic Resonance in Medicine 58:430-436(2007)
                new[] {
                    new [] {  0.00,  0.000,  0.000, 0.6900, 0.920, 0.900,   0, +2.0 },
                    new [] {  0.00,  0.000,  0.000, 0.6624, 0.874, 0.880,   0, -0.8 },
                    new [] { -0.22,  0.000, -0.250, 0.4100, 0.160, 0.210, -72, -0.2 },
                    new [] { +0.22,  0.000, -0.250, 0.3100, 0.110, 0.220, +72, -0.2 },
                    new [] {  0.00, +0.350, -0.250, 0.2100, 0.250, 0.500,   0, +0.2 },
                    new [] {  0.00, +0.100, -0.250, 0.0460, 0.046, 0.046,   0, +0.2 },
                    new [] { -0.08, -0.650, -0.250, 0.0460, 0.023, 0.020,   0, +0.1 },
                    new [] { +0.06, -0.650, -0.250, 0.0460, 0.023, 0.020, +90, +0.1 },
                    new [] { +0.06, -0.105, +0.625, 0.0560, 0.040, 0.100, +90, +0.2 },
                    new [] {  0.00, +0.100, +0.625, 0.0560, 0.056, 0.100,   0, -0.2 },
                },
            };
            var result = new float[size, size, size];
            var size2 = size / 2.0;
            int RealToLogical(double x) => Mt.Round((x + 1) * size2);
            double LogicalToReal(double x) => x / size2 - 1;
            foreach (var e in Ellipses[id])
            {
                var center = new Double3(e[0], e[1], e[2]);
                var axes = new Double3(e[3], e[4], e[5]);
                var maxaxis = Mt.Max(axes.v0, axes.v1, axes.v2);
                var theta = e[6] * (Math.PI / 180);
                var level = (float)(e[7] / 2);

                var cos = Math.Cos(theta * 2);
                var sin = Math.Sin(theta * 2);
                var a = 1 / axes.X.Sq();
                var b = 1 / axes.Y.Sq();
                var c = 1 / axes.Z.Sq();
                var f = a + b + (a - b) * cos;
                int z0 = RealToLogical(-maxaxis + center.Z);
                int z1 = RealToLogical(+maxaxis + center.Z);
                for (int iz = z0; iz <= z1; iz++)
                {
                    var z = LogicalToReal(iz) - center.Z;
                    int y0 = RealToLogical(-maxaxis + center.Y);
                    int y1 = RealToLogical(+maxaxis + center.Y);
                    for (int iy = y0; iy <= y1; iy++)
                    {
                        var y = LogicalToReal(iy) - center.Y;
                        var d = 2 * (f - 2 * a * b * y.Sq() - f * c * z.Sq());
                        if (d < 0) continue;
                        d = Math.Sqrt(d);
                        int x0 = RealToLogical(((b - a) * sin * y - d) / f + center.X);
                        int x1 = RealToLogical(((b - a) * sin * y + d) / f + center.X);
                        for (int ix = x0; ix <= x1; ix++)
                            result[iz, iy, ix] += level;
                    }
                }
            }
            return result;
        }
        #endregion

        #region image functions
        public static T[] MaskWhere<T>(Array<T> image, Array<bool> mask) where T : unmanaged
        {
            var n = Ex.SameLength(image.A, mask.A);
            using var p = New.Fix(image);
            using var q = New.Fix(mask);
            var m = 0; for (int i = n; --i >= 0;) if (q[i]) m++;
            var r = new T[m];
            for (int i = n, j = m; --i >= 0;) if (q[i]) r[--j] = p[i];
            return r;
        }
        public static Array<T> Mask<T>(this Array<T> image, Array<bool> mask) where T : unmanaged
        {
            var a = image.To0();
            a.AsSpan().LetMask(mask.AsSpan());
            return a;
        }
        public static double NormRelativeRootMeanSquareError(Array<float> x, Array<float> y, Array<bool> mask) => NormRelativeMeanSquareError(x, y, mask).Sqrt();
        public static double NormRelativeMeanSquareError(Array<float> x, Array<float> y, Array<bool> mask) => NormRelativeMeanSquareError(x.Mask(mask), y.Mask(mask));
        public static double NormRelativeRootMeanSquareErrorLinearlyAdjusted(Array<float> x, Array<float> y, Array<bool> mask) => NormRelativeMeanSquareErrorLinearlyAdjusted(x, y, mask).Sqrt();
        public static double NormRelativeMeanSquareErrorLinearlyAdjusted(Array<float> x, Array<float> y, Array<bool> mask) => DZSIM(x.Mask(mask), y.Mask(mask));
        public static double VarianceRelativeRootMeanSquareErrorLinearlyAdjusted(Array<float> x, Array<float> y, Array<bool> mask) => VarianceRelativeMeanSquareErrorLinearlyAdjusted(x, y, mask).Sqrt();
        public static double VarianceRelativeMeanSquareErrorLinearlyAdjusted(Array<float> x, Array<float> y, Array<bool> mask) => DLSIM(x.Mask(mask), y.Mask(mask));
        public static double NormRelativeRootMeanSquareError(Array<float> x, Array<float> y) => NormRelativeMeanSquareError(x, y).Sqrt();
        public static double NormRelativeMeanSquareError(Array<float> x, Array<float> y)
        {
            var x_ = x.AsSpan();
            var y_ = y.AsSpan();
            double dd = Mt.DNorm2SqSub(x_, y_);
            double yy = y_.DNorm2Sq();
            return dd / yy;
        }
        public static double DZSIM(Array<float> x, Array<float> y)
        {
            var x_ = x.AsSpan();
            var y_ = y.AsSpan();
            double xx = x_.DNorm2Sq();
            double yy = y_.DNorm2Sq();
            double xy = Mt.Inner<float, double>(x_, y_);
            return 1 - (xy / xx) * (xy / yy);
        }
        public static double DLSIM(Array<float> x, Array<float> y)
        {
            var x_ = x.AsSpan();
            var y_ = y.AsSpan();
            double mx = x_.AvgPair<float, double>();
            double my = y_.AvgPair<float, double>();
            double xx = x_.AvgPair(u => (u - mx).Sq());
            double yy = y_.AvgPair(v => (v - my).Sq());
            double xy = Mt.AvgPair(x_, y_, (u, v) => (u - mx) * (v - my));
            return 1 - (xy / xx) * (xy / yy);
        }
        public static double DSSIM(Array<float> x, Array<float> y, double range = 0, double param1 = 0.01, double param2 = 0.03)
        {
            var x_ = x.AsSpan();
            var y_ = y.AsSpan();
            double c1 = (param1 * range).Sq();
            double c2 = (param2 * range).Sq();
            double mx = x_.AvgPair<float, double>();
            double my = y_.AvgPair<float, double>();
            double xx = x_.AvgPair(u => (u - mx).Sq());
            double yy = y_.AvgPair(v => (v - my).Sq());
            double xy = Mt.AvgPair(x_, y_, (u, v) => (u - mx) * (v - my));
            return 1 - (2 * mx * my + c1) * (2 * xy + c2) / ((mx * mx + my * my + c1) * (xx + yy + c2));
        }
        #endregion
    }

    #region Matlab
    public static class Matlab
    {
        public class MatFileDataBlock
        {
            public int Type { get; private set; }
            public int Size { get; private set; }
            public byte[] Data;
            public MatFileDataBlock(BinaryReader reader)
            {
                Type = reader.ReadInt16();
                Size = reader.ReadInt16(); if (Size == 0) Size = reader.ReadInt32();
                Data = reader.ReadBytes(Size);
                if (reader.BaseStream.CanSeek && !reader.EndOfStream())
                {
                    int s = (int)(8 - reader.BaseStream.Position % 8) % 8;
                    reader.Skip(s);
                }
            }
            public unsafe double GetNumbers(int index)
            {
                fixed (byte* p = Data)
                    return Type switch
                    {
                        1 => ((sbyte*)p)[index],
                        2 => p[index],
                        3 => ((short*)p)[index],
                        4 => ((ushort*)p)[index],
                        5 => ((int*)p)[index],
                        6 => ((uint*)p)[index],
                        7 => ((float*)p)[index],
                        9 => ((double*)p)[index],
                        12 => ((long*)p)[index],
                        13 => ((ulong*)p)[index],
                        _ => double.NaN,
                    };
            }
            public int[] GetInts()
            {
                var temp = Data;
                return Type switch
                {
                    5 => New.Array(Size / sizeof(int), i => BitConverter.ToInt32(temp, i * sizeof(int))),
                    _ => ThrowException.NotImplemented<int[]>(nameof(Type)),
                };
            }
            public string GetString()
            {
                byte[] unicode;
                switch (Type)
                {
                    case 1: unicode = Encoding.Convert(Encoding.ASCII, Encoding.Unicode, Data); break;
                    case 16: unicode = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Data); break;
                    case 17: unicode = Data; break;
                    case 18: unicode = Encoding.Convert(Encoding.UTF32, Encoding.Unicode, Data); break;
                    default: return "";
                }
                return new string(Encoding.Unicode.GetChars(unicode));
            }
        }
        public static Dictionary<string, object> Load(BinaryReader filereader)
        {
            var variables = new Dictionary<string, object>();
            {
                var header = new byte[128];
                filereader.Read(header, 0, header.Length);

                while (!filereader.EndOfStream())
                {
                    var dataelement = new MatFileDataBlock(filereader);
                    if (dataelement.Type == 15)
                    {
                        using var reader = new BinaryReader(new GZipStream(new MemoryStream(dataelement.Data), CompressionMode.Decompress));
                        dataelement = new MatFileDataBlock(reader);
                    }
                    {
                        using var reader = new BinaryReader(new MemoryStream(dataelement.Data));

                        if (dataelement.Type != 14) Warning.WriteLine($"LoadMatFile: unknown type: {dataelement.Type}");
                        var _ = new MatFileDataBlock(reader);  //arrayflag
                        var dimensions = new MatFileDataBlock(reader);
                        int[] lengths = dimensions.GetInts();
                        lengths = lengths.Reverse().ToArray();

                        var arrayname = new MatFileDataBlock(reader);
                        string varname = arrayname.GetString();
                        var realpart = new MatFileDataBlock(reader);
                        var imagpart = default(MatFileDataBlock);
                        if (!reader.EndOfStream()) imagpart = new MatFileDataBlock(reader);
                        if (!reader.EndOfStream()) Warning.WriteLine("LoadMatFile: unknown data");

                        Array variable;
                        if (imagpart is null)
                        {
                            variable = Array.CreateInstance(typeof(double), lengths);
                            int i = 0;
                            foreach (var index in Ex.Range(lengths))
                            {
                                var v = realpart.GetNumbers(i); i++;
                                variable.SetValue(v, index);
                            }
                        }
                        else
                        {
                            variable = Array.CreateInstance(typeof(ComplexD), lengths);
                            int i = 0;
                            foreach (var index in Ex.Range(lengths))
                            {
                                var v = new ComplexD(realpart.GetNumbers(i), imagpart.GetNumbers(i)); i++;
                                variable.SetValue(v, index);
                            }
                        }
                        variables.Add(varname, variable);
                        //Console.WriteLine(varname + ": " + variable.GetType());
                    }
                }
            }
            return variables;
        }

        public static Dictionary<string, object> Load(string path)
        {
            using var file = new BinaryReader(Ex.FileOpenReadShare(path));
            return Load(file);
        }

        public const double eps = Mt.DoubleEps;  //Math.Pow(2, -52);
        //端の周波数0を真中に
        public static T[,] FftShift<T>(T[,] data) => Nm.ShiftCentering(data);
        //真中の周波数0を端に
        public static T[,] IFftShift<T>(T[,] data) => Nm.ShiftBeginning(data);

        public static double[] Hamming(int size)
        {
            return New.Array(size, i => 0.54 - 0.46 * Math.Cos(2 * Math.PI * (i + 1) / (size + 1)));
        }

        public static double[,] Filter2(double[,] filter, double[,] data, string type)
        {
            Nm.ConvolutionType ctype;
            switch (type)
            {
                case "full": ctype = Nm.ConvolutionType.Full; break;
                case "same": ctype = Nm.ConvolutionType.Same; break;
                case "valid": ctype = Nm.ConvolutionType.Valid; break;
                default: return ThrowException.Argument<double[,]>($"{nameof(type)} is unknown");
            }
            return Nm.Filter(data, filter, ctype).Abs();
        }

        public static T[,] Zpad<T>(T[,] matrix, Int2 length)
        {
            var l0 = matrix.GetLength(0);
            var l1 = matrix.GetLength(1);
            var o0 = (length.v0 - l0) / 2;
            var o1 = (length.v1 - l1) / 2;
            var result = New.Array<T>(length);
            for (int i0 = 0; i0 < l0; i0++)
                for (int i1 = 0; i1 < l1; i1++)
                    result[o0 + i0, o1 + i1] = matrix[i0, i1];
            return result;
        }

        public static T[,] Crop<T>(T[,] matrix, Int2 length)
        {
            var l0 = matrix.GetLength(0);
            var l1 = matrix.GetLength(1);
            var o0 = (l0 - length.v0) / 2;
            var o1 = (l1 - length.v1) / 2;
            var result = New.Array<T>(length);
            for (int i0 = 0; i0 < length.v0; i0++)
                for (int i1 = 0; i1 < length.v1; i1++)
                    result[i0, i1] = matrix[o0 + i0, o1 + i1];
            return result;
        }
    }
    #endregion
}
