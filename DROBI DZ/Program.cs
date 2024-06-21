using System;

public class Fraction
{
    public int Numerator { get; private set; }
    public int Denominator { get; private set; }

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Denominator cannot be zero.");

        Numerator = numerator;
        Denominator = denominator;
        Simplify();
    }

    private void Simplify()
    {
        int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
        Numerator /= gcd;
        Denominator /= gcd;
        if (Denominator < 0)
        {
            Numerator = -Numerator;
            Denominator = -Denominator;
        }
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        int numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
        int denominator = a.Denominator * b.Denominator;
        return new Fraction(numerator, denominator);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        int numerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
        int denominator = a.Denominator * b.Denominator;
        return new Fraction(numerator, denominator);
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        int numerator = a.Numerator * b.Numerator;
        int denominator = a.Denominator * b.Denominator;
        return new Fraction(numerator, denominator);
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.Numerator == 0)
            throw new DivideByZeroException("Cannot divide by zero fraction.");

        int numerator = a.Numerator * b.Denominator;
        int denominator = a.Denominator * b.Numerator;
        return new Fraction(numerator, denominator);
    }

    public static bool operator ==(Fraction a, Fraction b)
    {
        return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
    }

    public static bool operator !=(Fraction a, Fraction b)
    {
        return !(a == b);
    }

    public static implicit operator bool(Fraction f)
    {
        return f.Numerator < f.Denominator;
    }

    public static implicit operator Fraction(int value)
    {
        return new Fraction(value, 1);
    }

    public static implicit operator Fraction(double value)
    {
        double tolerance = 1.0E-6;
        int sign = Math.Sign(value);
        value = Math.Abs(value);

        int n = (int)value;
        double decimalPart = value - n;

        if (Math.Abs(decimalPart) < tolerance)
        {
            return new Fraction(sign * n, 1);
        }

        int numerator = (int)(decimalPart * 1000000);
        int denominator = 1000000;
        Fraction decimalFraction = new Fraction(numerator, denominator);

        Fraction integerFraction = new Fraction(n, 1);
        Fraction result = integerFraction + decimalFraction;
        result.Numerator *= sign;
        return result;
    }

    public override bool Equals(object obj)
    {
        if (obj is Fraction)
        {
            Fraction other = (Fraction)obj;
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Numerator.GetHashCode() ^ Denominator.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }

    public static void Main(string[] args)
    {
        Fraction f = new Fraction(3, 4);
        int a = 10;
        Fraction f1 = f * a;
        Fraction f2 = a * f;
        double d = 1.5;
        Fraction f3 = f + d;

        Console.WriteLine($"f: {f}");
        Console.WriteLine($"f1: {f1}");
        Console.WriteLine($"f2: {f2}");
        Console.WriteLine($"f3: {f3}");
    }
}
