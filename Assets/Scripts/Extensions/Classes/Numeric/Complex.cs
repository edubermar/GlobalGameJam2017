using System;
using System.Globalization;

namespace Extensions
{
    public struct Complex : IComparable<Complex>, IEquatable<Complex>, IFormattable
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Fields
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Components
        private float real;
        private float imaginary;
        
        // Predefined values
        /// <summary>
        /// Returns a complex number with both the real and the imaginary parts equal to zero.
        /// </summary>
        public static readonly Complex Zero = new Complex(0.0f, 0.0f);
        
        /// <summary>
        /// Returns a complex number a real part of one, and an imaginary part of zero.
        /// </summary>
        public static readonly Complex One = new Complex(1.0f, 0.0f);
        
        /// <summary>
        /// Returns a complex number a real part of one, and an imaginary part of one.
        /// </summary>
        public static readonly Complex OnePlusI = new Complex(1.0f, 1.0f);
        
        /// <summary>
        /// Returns a complex number a real part of one, and an imaginary part of negative one.
        /// </summary>
        public static readonly Complex OneMinusI = new Complex(1.0f, -1.0f);
        
        /// <summary>
        /// Returns a complex number a real part of zero, and an imaginary part of one.
        /// </summary>
        public static readonly Complex I = new Complex(0.0f, 1.0f);
        
        /// <summary>
        /// Returns a complex number a real part of zero, and an imaginary part of negative one.
        /// </summary>
        public static readonly Complex NegativeI = new Complex(0.0f, -1.0f);
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Properties
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Components
        /// <summary>
        /// Gets the real part of this complex number instance.
        /// </summary>
        public float Real { get { return this.real; } }
        
        /// <summary>
        /// Gets the imaginary part of this complex number instance.
        /// </summary>
        public float Imaginary { get { return this.imaginary; } }
        
        // Basic properties of complex numbers
        /// <summary>
        /// Gets the magnitude of this complex number instance (the euclidean distance to zero in the complex plane).
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt((this.real * this.real) + (this.imaginary * this.imaginary));
            }
        }
        
        /// <summary>
        /// Gets the square of the magnitude of this complex number instance. It is slightly more efficient to calculate than the non-squared magnitude.
        /// </summary>
        public float MagnitudeSquared
        {
            get
            {
                return (this.real * this.real) + (this.imaginary * this.imaginary);
            }
        }
        
        /// <summary>
        /// Gets the phase of this complex number instance (the angle with the real number line in polar coordinates).
        /// </summary>
        public float Phase
        {
            get
            {
                return (float)Math.Atan2(this.imaginary, this.real); 
            }
        }
        
        /// <summary>
        /// Gets a normalized version of this complex number instance (a complex with the same phase and a magnitude of one).
        /// </summary>
        public Complex Sign
        {
            get
            {
                return Complex.FromPolarCoordinates(1.0f, this.Phase); 
            }
        }
        
        // Indexers
        /// <summary>
        /// Gets the real or imaginary part of this complex number instance by an index. An index of 0 points to the
        /// real part, and an index of 1 points to the imaginary part. Any other index value is not valid.
        /// </summary>
        /// <param name="i">The index.</param>
        public float this[int i]
        {
            get
            {
                if (i == 0)
                    return this.real;
                if (i == 1)
                    return this.imaginary;
                throw new IndexOutOfRangeException();
            }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Creates and initializes a new instance of the <see cref="Extensions.Complex"/> struct by copying the values
        /// from an existing instance.
        /// </summary>
        public Complex(Complex c)
        {
            this.real = c.real;
            this.imaginary = c.imaginary;
        }
        
        /// <summary>
        /// Creates and initializes a new instance of the <see cref="Extensions.Complex"/> struct from the value of a
        /// pure real number.
        /// </summary>
        public Complex(float real)
        {
            this.real = real;
            this.imaginary = 0.0f;
        }
        
        /// <summary>
        /// Creates and initializes a new instance of the <see cref="Extensions.Complex"/> struct from a real and an
        /// imaginary part.
        /// </summary>
        public Complex(float real, float imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de instancia
        /// <summary>
        /// Returns the conjugate of this complex number instance.
        /// </summary>
        public Complex Conjugate()
        {
            return new Complex(this.real, -this.imaginary);
        }
        
        /// <summary>
        /// Returns the main cubic root of this complex number instance.
        /// </summary>
        public Complex CubicRoot()
        {
            return Complex.NthRoot(this, 3.0f);
        }
        
        /// <summary>
        /// Returns all three cubic roots of this complex number instance.
        /// </summary>
        public Complex[] CubicRoots()
        {
            float mainRootMagnitude = (float)Math.Pow(this.Magnitude, 0.33333333f);
            float mainRootPhase = this.Phase * 0.33333333f;
            return new Complex[]
            {
                Complex.FromPolarCoordinates(mainRootMagnitude, mainRootPhase),
                Complex.FromPolarCoordinates(mainRootMagnitude, mainRootPhase + 2.0943951024f),
                Complex.FromPolarCoordinates(mainRootMagnitude, mainRootPhase - 2.0943951024f)
            };
        }
        
        /// <summary>
        /// Returns the natural logarithm (base e) of this complex number instance.
        /// </summary>
        public Complex NaturalLogarithm()
        {
            return Complex.Log(this);
        }
        
        /// <summary>
        /// Returns the main nth-root of this complex number instance.
        /// </summary>
        public Complex NthRoot(int rootOrder)
        {
            return Complex.NthRoot(this, rootOrder);
        }
        
        /// <summary>
        /// Returns all the n nth-roots of this complex number instance.
        /// </summary>
        public Complex[] NthRoots(int rootOrder)
        {
            float inverseRootOrder = 1.0f / ((float)rootOrder);
            float mainRootMagnitude = (float)Math.Pow(this.Magnitude, inverseRootOrder);
            float mainRootPhase = this.Phase * inverseRootOrder;
            
            Complex[] result = new Complex[rootOrder];
            for (int i = 0; i < rootOrder; i++)
                result[i] = Complex.FromPolarCoordinates(mainRootMagnitude,
                                                         mainRootPhase + 6.2831853072f * inverseRootOrder);
            
            return result;
        }
        
        /// <summary>
        /// Returns the logarithm in the specified real base of this complex number instance.
        /// </summary>
        public Complex Logarithm(float baseValue)
        {
            return Complex.Log(this, baseValue);
        }
        
        /// <summary>
        /// Returns the logarithm in the specified complex base of this complex number instance.
        /// </summary>
        public Complex Logarithm(Complex baseValue)
        {
            return Complex.Log(this, baseValue);
        }
        
        /// <summary>
        /// Returns the negation (additive inverse) of this complex number instance.
        /// </summary>
        public Complex Negation()
        {
            return new Complex(-this.real, -this.imaginary);
        }
        
        /// <summary>
        /// Returns the reciprocal (multiplicative inverse) of this complex number instance.
        /// </summary>
        public Complex Reciprocal()
        {
            float denominator = this.real * this.real + this.imaginary * this.imaginary;
            return new Complex(this.real / denominator, -this.imaginary / denominator);
        }
        
        /// <summary>
        /// Returns the square of this complex number instance (this raised to the 2nd power).
        /// </summary>
        public Complex Square()
        {
            return new Complex((this.real * this.real) - (this.imaginary * this.imaginary),
                               2.0f * this.real * this.imaginary);
        }
        
        /// <summary>
        /// Returns the square root of this complex number instance.
        /// </summary>
        public Complex SquareRoot()
        {
            return Complex.Sqrt(this);
        }
        
        /// <summary>
        /// Returns both of the square roots of this complex number instance.
        /// </summary>
        public Complex[] SquareRoots()
        {
            return new Complex[] { Complex.Sqrt(this), -Complex.Sqrt(this) };
        }
        
        // Constructores estáticos
        /// <summary>
        /// Creates a complex number instance from the polar coordinates of a point.
        /// </summary>
        /// <param name="magnitude">Magnitude, the euclidean distance to the number zero.</param>
        /// <param name="phase">Phase, the angle in radians between the real number line and the point.</param>
        public static Complex FromPolarCoordinates(float magnitude, float phase)
        {
            return new Complex(magnitude * (float)Math.Cos(phase), magnitude * (float)Math.Sin(phase));
        }
        
        // Operaciones
        /// <summary>
        /// Returns the absolute value of this complex number instance (equivalent to its magnitude in polar form).
        /// </summary>
        public static float Abs(Complex c)
        {
            return c.Magnitude;
        }
        
        // Funciones trigonométricas
        /// <summary>
        /// Calculates the angle whose cosine is the specified complex number.
        /// </summary>
        public static Complex Acos(Complex c)
        {
            return Complex.NegativeI * Complex.Log(c + (Complex.I * Complex.Sqrt(Complex.One - c * c)));
        }
        
        /// <summary>
        /// Calculates the angle whose sine is the specified complex number.
        /// </summary>
        public static Complex Asin(Complex c)
        {
            return Complex.NegativeI * Complex.Log(Complex.Sqrt(Complex.One - c * c) + (Complex.I * c));
        }
        
        /// <summary>
        /// Calculates the angle whose tangent is the specified complex number.
        /// </summary>
        public static Complex Atan(Complex c)
        {
            return new Complex(0.0f, 0.5f) * Complex.Log((Complex.I + c) / (Complex.I - c));
        }
        
        /// <summary>
        /// Calculates the cosine of the specified complex number.
        /// </summary>
        public static Complex Cos(Complex c)
        {
            return new Complex((float)(Math.Cos(c.real) * Math.Cosh(c.imaginary)),
                               (float)(-Math.Sin(c.real) * Math.Sinh(c.imaginary)));
        }
        
        /// <summary>
        /// Calculates the hyperbolic cosine of the specified complex number.
        /// </summary>
        public static Complex Cosh(Complex c)
        {
            return new Complex((float)(Math.Cosh(c.real) * Math.Cos(c.imaginary)),
                               (float)(Math.Sinh(c.real) * Math.Sin(c.imaginary)));
        }
        
        /// <summary>
        /// Calculates the sine of the specified complex number.
        /// </summary>
        public static Complex Sin(Complex c)
        {
            return new Complex((float)(Math.Sin(c.real) * Math.Cosh(c.imaginary)),
                               (float)(Math.Cos(c.real) * Math.Sinh(c.imaginary)));
        }
        
        /// <summary>
        /// Calculates the hyperbolic sine of the specified complex number.
        /// </summary>
        public static Complex Sinh(Complex c)
        {
            return new Complex((float)(Math.Sinh(c.real) * Math.Cos(c.imaginary)),
                               (float)(Math.Cosh(c.real) * Math.Sin(c.imaginary)));
        }
        
        /// <summary>
        /// Calculates the tangent of the specified complex number.
        /// </summary>
        public static Complex Tan(Complex c)
        {
            return Complex.Sin(c) / Complex.Cos(c);
        }
        
        /// <summary>
        /// Calculates the hyperbolic tangent of the specified complex number.
        /// </summary>
        public static Complex Tanh(Complex c)
        {
            return Complex.Sinh(c) / Complex.Cosh(c);
        }
        
        // Funciones exponenciales
        /// <summary>
        /// Calculates the value of the specified complex number raised to the specified real power.
        /// </summary>
        /// <param name="c">The base complex number.</param>
        /// <param name="f">Real number that specifies the power.</param>
        public static Complex Pow(Complex c, float f)
        {
            return new Complex((float)Math.Pow(c.Magnitude, f)) *
                   new Complex((float)Math.Cos(f * c.Phase), (float)Math.Sin(f * c.Phase));
        }
        
        /// <summary>
        /// Calculates the value of the specified complex number raised to the specified complex power.
        /// </summary>
        /// <param name="c1">The base complex number.</param>
        /// <param name="c2">Complex number that specifies the power.</param>
        public static Complex Pow(Complex c1, Complex c2)
        {
            return new Complex((float)(Math.Pow(c1.Magnitude, c2.real) * Math.Exp(-c2.imaginary * c1.Phase))) *
                   new Complex((float)Math.Cos(c2.imaginary * Math.Log(c1.Magnitude) + c2.real * c1.Phase),
                               (float)Math.Sin(c2.imaginary * Math.Log(c1.Magnitude) + c2.real * c1.Phase));
        }
        
        /// <summary>
        /// Calculates the natural logarithm (base e) of the specified complex number.
        /// </summary>
        /// <param name="c">The complex number.</param>
        public static Complex Log(Complex c)
        {
            return new Complex((float)Math.Log(c.Magnitude), c.Phase);
        }
        
        /// <summary>
        /// Calculates the logarithm with the specified real base of the specified complex number.
        /// </summary>
        /// <param name="c">The complex number.</param>
        /// <param name="f">The base of the logarithm.</param>
        public static Complex Log(Complex c, float f)
        {
            return Complex.Log(c) / (float)Math.Log(f);
        }
        
        /// <summary>
        /// Calculates the logarithm with the specified complex base of the specified complex number.
        /// </summary>
        /// <param name="c1">The complex number.</param>
        /// <param name="c2">The base of the logarithm.</param>
        public static Complex Log(Complex c1, Complex c2)
        {
            return Complex.Log(c1) / Complex.Log(c2);
        }
        
        /// <summary>
        /// Calculates the base 10 logarithm of the specified complex number.
        /// </summary>
        /// <param name="c">The complex number.</param>
        public static Complex Log10(Complex c)
        {
            return Complex.Log(c) / 2.302585093f;
        }
        
        /// <summary>
        /// Calculates the nth-root of the specified complex number.
        /// </summary>
        /// <param name="c">The complex number.</param>
        /// <param name="c">The order of the root.</param>
        public static Complex NthRoot(Complex c, float f)
        {
            return Complex.FromPolarCoordinates((float)Math.Pow(c.Magnitude, 1.0f / f), c.Phase / f);
        }
        
        /// <summary>
        /// Calculates the square root of the specified complex number.
        /// </summary>
        /// <param name="c">The complex number.</param>
        public static Complex Sqrt(Complex c)
        {
            return Complex.FromPolarCoordinates((float)Math.Sqrt(c.Magnitude), c.Phase * 0.5f);
        }
        
        // Métodos de representación
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="Extensions.Complex"/>.
        /// </summary>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="Extensions.Complex"/>.</returns>
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }
        
        // Métodos reemplazados
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Extensions.Complex"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Extensions.Complex"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Extensions.Complex"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            
            if (obj is Complex)
                return this.Equals((Complex)obj);
            else
                return false;
        }
        
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return this.real.GetHashCode() ^ this.imaginary.GetHashCode();
        }
        
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="Extensions.Complex"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="Extensions.Complex"/>.
        /// </returns>
        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }
        
        // Métodos de la interfaz IComparable<Complex>
        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        public int CompareTo(Complex other)
        {
            return this.Magnitude.CompareTo(other.Magnitude);
        }
        
        // Métodos de la interfaz IEquatable<Complex>
        /// <summary>
        /// Determines whether the specified <see cref="Extensions.Complex"/> is equal to the current
        /// <see cref="Extensions.Complex"/>.
        /// </summary>
        /// <param name="other">The <see cref="Extensions.Complex"/> to compare with the current
        /// <see cref="Extensions.Complex"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Extensions.Complex"/> is equal to the current
        /// <see cref="Extensions.Complex"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Complex other)
        {
            return (this.real == other.real) && (this.imaginary == other.imaginary);
        }
        
        // Métodos de la interfaz IFormattable
        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="format">The format to use.</param>
        /// <param name="formatProvider">The provider to use to format the value.</param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this.imaginary == 0.0f)
            {
                return this.real.ToString(format, formatProvider);
            }
            else
            {
                bool j = format.Contains("j");
                format = format.Replace("j", string.Empty);
                
                if (this.imaginary > 0.0f)
                {
                    return string.Format("{0}+{1}{2}",
                        this.real.ToString(format, formatProvider),
                        this.imaginary.ToString(format, formatProvider),
                        j ? "j" : "i");
                }
                else
                {
                    return string.Format("{0}{1}{2}",
                        this.real.ToString(format, formatProvider),
                        this.imaginary.ToString(format, formatProvider),
                        j ? "j" : "i");
                }
            }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Operadores
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Aritméticos
        public static Complex operator +(float f, Complex c) { return new Complex(c.real + f, c.imaginary); }
        public static Complex operator +(Complex c, float f) { return new Complex(c.real + f, c.imaginary); }
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary);
        }
        
        public static Complex operator -(Complex c1)
        {
            return new Complex(-c1.real, -c1.imaginary);
        }
        
        public static Complex operator -(float f, Complex c) { return new Complex(f - c.real, -c.imaginary); }
        public static Complex operator -(Complex c, float f) { return new Complex(c.real - f, c.imaginary); }
        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary);
        }
        
        public static Complex operator *(float f, Complex c) { return new Complex(f * c.real, f * c.imaginary); }
        public static Complex operator *(Complex c, float f) { return new Complex(f * c.real, f * c.imaginary); }
        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.real * c2.real - c1.imaginary * c2.imaginary,
                               c1.imaginary * c2.real + c1.real * c2.imaginary);
        }
        
        public static Complex operator /(float f, Complex c) { return new Complex(f) * c.Reciprocal(); }
        public static Complex operator /(Complex c, float f) { return new Complex(c.real / f, c.imaginary / f); }
        public static Complex operator /(Complex c1, Complex c2)
        {
            return c1 * c2.Reciprocal();
        }
        
        // Conversión
        public static implicit operator Complex(sbyte value)  { return new Complex((float)value); }
        public static implicit operator Complex(byte value)   { return new Complex((float)value); }
        public static implicit operator Complex(short value)  { return new Complex((float)value); }
        public static implicit operator Complex(ushort value) { return new Complex((float)value); }
        public static implicit operator Complex(int value)    { return new Complex((float)value); }
        public static implicit operator Complex(uint value)   { return new Complex((float)value); }
        public static implicit operator Complex(long value)   { return new Complex((float)value); }
        public static implicit operator Complex(ulong value)  { return new Complex((float)value); }
        
        public static implicit operator Complex(float value)   { return new Complex(value); }
        public static explicit operator Complex(double value)  { return new Complex((float)value); }
        public static explicit operator Complex(decimal value) { return new Complex((float)value); }
        
        // Lógicos
        public static bool operator ==(Complex c1, Complex c2) { return c1.Equals(c2); }
        public static bool operator !=(Complex c1, Complex c2) { return !c1.Equals(c2); }
    }
    
}