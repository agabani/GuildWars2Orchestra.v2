using System;

namespace music
{
    public class Fraction
    {
        public static Fraction Full = new Fraction(1, 1);
        public static Fraction Half = new Fraction(1, 2);
        public static Fraction Quater = new Fraction(1, 4);
        public static Fraction Eighth = new Fraction(1, 8);
        public static Fraction Sixteenth = new Fraction(1, 16);
        public static Fraction Thirtyseconth = new Fraction(1, 32);

        public Fraction(double numerator, double denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public double Numerator { get; }
        public double Denominator { get; }

        public static implicit operator double(Fraction fraction)
        {
            return fraction.Numerator/fraction.Denominator;
        }

        protected bool Equals(Fraction other)
        {
            return Math.Abs(this - other) < double.Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Fraction) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Numerator.GetHashCode()*397) ^ Denominator.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{(double)this}";
        }
    }
}