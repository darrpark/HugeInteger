using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeIntegers
{
    class HugeInteger
    {
        // Attributes of a Huge Integer.
        public string number;               // the actual huge integer number
        public int[] digits;                // array to store the string values as separate integer digits
        public int significant_digits;      // number of significant digits (does not count leading 0s)
        public bool is_negative;            // determines if the huge integer is negative

        // Different creations of HugeInteger.
        public HugeInteger()                // Default Huge Integer with nothing inside
        {
            number = "";
            digits = new int[0];
            significant_digits = 0;
            is_negative = false;
        }

        public HugeInteger(bool x)          // Huge Integer with a set negative boolean
        {
            number = "";
            digits = new int[0];
            significant_digits = 0;
            is_negative = x;
        }

        public HugeInteger(string x)        // Huge Integer with a number value.
        {
            number = x;
            digits = new int[0];
            significant_digits = 0;
            is_negative = false;
        }

        public HugeInteger(string x, bool y)// Huge Integer with a number and negative value
        {
            number = x;
            digits = new int[0];
            significant_digits = 0;
            is_negative = y;
        }

        // Method to replace the Huge Integer's number
        public HugeInteger FillHugeIntArray(string input)
        {
            this.number = input;
            this.significant_digits = this.numberOfDigits(input);
            this.digits = new int[significant_digits];
            for (int i = 0; i < this.digits.Length; i++)
            {
                int result;
                if (Int32.TryParse(input[i].ToString(), out result))
                {
                    this.digits[i] = result;
                }
            }

            return this;
        }
        // Part A ends here

        // Part B begins here (Addition)
        public HugeInteger Add(HugeInteger x, HugeInteger y)
        {
            HugeInteger Sum = new HugeInteger();
            x.significant_digits = x.numberOfDigits();
            y.significant_digits = y.numberOfDigits();

            // IF BOTH ARE NEGATIVE NUMBERS, SIMPLY CHANGE NEGATIVE BOOLEAN TO TRUE
            if (x.is_negative && y.is_negative)
            {
                Sum.is_negative = true;
            }

            // IF EITHER ARE NEGATIVE NUMBERS, RUN "SUBTRACTION" INSTEAD OF ADDITION
            else if (x.is_negative && !y.is_negative)
            {
                x.is_negative = false;
                if (x.isGreaterThan(y))
                {
                    Sum = Sum.Subtract(x, y);
                    Sum.is_negative = true;
                }
                else
                {
                    Sum = Sum.Subtract(y, x);
                }
                x.is_negative = true;
                return Sum;
            }
            else if (y.is_negative && !x.is_negative)
            {
                y.is_negative = false;
                if (y.isGreaterThan(x))
                {
                    Sum = Sum.Subtract(y, x);
                    Sum.is_negative = true;
                }
                else
                {
                    Sum = Sum.Subtract(x, y);
                }
                y.is_negative = true;
                return Sum;
            }

            // BOTH POSITIVE NUMBERS OR BOTH NEGATIVE NUMBERS, SIMPLY ADD
            if (x.isGreaterThanOrEqualTo(y))
            {
                int[] new_y_digits = new int[x.significant_digits];              // Create a new array to line up y indices with x indices
                for (int j = x.significant_digits - y.significant_digits; j < x.significant_digits; j++)
                    new_y_digits[j] = y.digits[j - (x.significant_digits - y.significant_digits)];

                Sum.digits = new int[x.significant_digits + 1];                  // Updating number of digits needed to calculate the sum, plus 1 in case of final carry value.
                int xpos = x.digits.Length - 1;                                  // Initial index to start the addition from

                for (int pos = xpos; pos > 0; pos--)                             // Going through each index
                {
                    if (x.digits[pos] + new_y_digits[pos] > 9)                   // Check if carry is needed
                    {                                                            // If so...
                        x.digits[pos] -= 10;                                     // Current index value - 10            
                        x.digits[pos - 1]++;                                     // Next index value + 1
                    }
                    Sum.digits[pos + 1] = x.digits[pos] + new_y_digits[pos];     // Do the addition (index + 1 because Sum has 1 more index than x and y)
                }
                if (x.digits[0] + new_y_digits[0] > 9)                           // Final check for the very last index of Sum.
                {
                    x.digits[0] -= 10;
                    Sum.digits[1] += x.digits[0] + new_y_digits[0];
                    Sum.digits[0] += 1;
                }
                else
                    Sum.digits[1] += x.digits[0] + new_y_digits[0];
            }
            else   // Same as above except when y >= x
            {
                int[] new_x_digits = new int[y.significant_digits];           
                for (int j = y.significant_digits - x.significant_digits; j < y.significant_digits; j++)
                    new_x_digits[j] = x.digits[j - (y.significant_digits - x.significant_digits)];

                Sum.digits = new int[y.significant_digits + 1];                 
                int ypos = y.digits.Length - 1;                            

                for (int pos = ypos; pos > 0; pos--)                        
                {
                    if (y.digits[pos] + new_x_digits[pos] > 9)           
                    {                                                       
                        y.digits[pos] -= 10;                                          
                        y.digits[pos - 1]++;                                     
                    }
                    Sum.digits[pos + 1] = y.digits[pos] + new_x_digits[pos];     
                }
                if (y.digits[0] + new_x_digits[0] > 9)                           
                {
                    y.digits[0] -= 10;
                    Sum.digits[1] += y.digits[0] + new_x_digits[0];
                    Sum.digits[0] += 1;
                }
                else
                    Sum.digits[1] += y.digits[0] + new_x_digits[0];
            }

            Sum.number = Convert.ToString(Sum.digits[0]);                             // Get the full number in string form (including below for loop)
            for (int k = 1; k < Sum.digits.Length; k++)
                Sum.number += Convert.ToString(Sum.digits[k]);
            Sum.significant_digits = Sum.numberOfDigits();                            // Update Sum attributes
            return Sum;
        }
        // Part B Ends here

        public HugeInteger Subtract(HugeInteger x, HugeInteger y)
        {
            HugeInteger Difference = new HugeInteger();
            x.significant_digits = x.numberOfDigits();
            y.significant_digits = y.numberOfDigits();

            // IF BOTH ARE NEGATIVE, SIMPLY MAKE THE DIFFERENCE NEGATIVE AS WELL
            if (x.is_negative && y.is_negative)
            {
                Difference.is_negative = true;
            }

            // IF EITHER ARE NEGATIVE, DO "ADDITION"
            else if (x.is_negative && !y.is_negative)
            {
                x.is_negative = false;                      // Need to set this to negative to see which integer is "bigger"
                if (x.isGreaterThan(y))                     // If the negative value is larger...
                {
                    Difference = Difference.Add(x, y);
                    Difference.is_negative = true;          // Then the result should be negative.
                }
                else
                {
                    Difference = Difference.Add(y, x);      // Else result should remain positive.
                }
                x.is_negative = true;                       // Resetting to original attribute
                return Difference;
            }
            else if (y.is_negative && !x.is_negative)
            {
                y.is_negative = false;
                if (y.isGreaterThan(x))
                {
                    Difference = Difference.Add(y, x);
                    Difference.is_negative = true;
                }
                else
                {
                    Difference = Difference.Add(x, y);
                }
                y.is_negative = true;
                return Difference;
            }

            // BOTH POSITIVE NUMBERS OR BOTH NEGATIVE NUMBERS
            if (x.isGreaterThanOrEqualTo(y))
            {
                int[] new_y_digits = new int[x.significant_digits];                 // Create a new array to line up y indices with x indices
                for (int j = x.significant_digits - y.significant_digits; j < x.significant_digits; j++)
                    new_y_digits[j] = y.digits[j - (x.significant_digits - y.significant_digits)];

                Difference.digits = new int[x.significant_digits];                  // Updating number of digits needed to calculate the difference
                int xpos = x.digits.Length - 1;

                for (int pos = xpos; pos >= 0; pos--)                               // Going through each index
                {
                    if (x.digits[pos] < new_y_digits[pos])                          // Check if carry is needed
                    {
                        x.digits[pos] += 10;                                        // Current index value + 10            
                        x.digits[pos - 1]--;                                        // Next index value - 1
                    }
                    Difference.digits[pos] = x.digits[pos] - new_y_digits[pos];     // Do the subtraction
                }

                // Updating Attributes of HugeInteger Difference
                for (int k = 0; k < Difference.digits.Length; k++)
                    Difference.number += Convert.ToString(Difference.digits[k]);
                Difference.significant_digits = Difference.numberOfDigits();
                return Difference;
            }
            else // This part is the same as above, just with x and y switched.
            {
                int[] new_x_digits = new int[y.digits.Length]; 
                for (int j = y.digits.Length - x.digits.Length; j < y.digits.Length; j++)
                    new_x_digits[j] = x.digits[j - (y.digits.Length - x.digits.Length)];

                Difference.digits = new int[y.significant_digits];
                int ypos = y.digits.Length - 1;

                for (int pos = ypos; pos >= 0; pos--)
                {
                    if (y.digits[pos] < new_x_digits[pos])
                    {
                        y.digits[pos] += 10;
                        y.digits[pos - 1]--;
                    }
                    Difference.digits[pos] = y.digits[pos] - new_x_digits[pos];
                }
                Difference.is_negative = true;

                // Updating Attributes of HugeInteger Difference
                Difference.number = Convert.ToString(Difference.digits[0]);
                for (int k = 1; k < Difference.digits.Length; k++)
                    Difference.number += Convert.ToString(Difference.digits[k]);
                Difference.significant_digits = Difference.numberOfDigits();
                return Difference;
            }
        }
        // Part C Ends Here

        public HugeInteger Multiply(HugeInteger a, HugeInteger b)
        {
            HugeInteger Product = new HugeInteger();
            a.significant_digits = a.numberOfDigits();
            b.significant_digits = b.numberOfDigits();
            Product.digits = new int[a.significant_digits + b.significant_digits];

            // If A or B are 0, then the output should be 0.
            if (a.number == "0" || b.number == "0")
            {
                Product.number = "0";
                Product.significant_digits = Product.numberOfDigits();
                return Product;
            }

            // IF EITHER A OR B ARE NEGATIVE, PRODUCT SHOULD ALSO BE NEGATIVE, BUT POSITIVE IF BOTH ARE NEGATIVE OR BOTH ARE POSITIVE
            if (a.is_negative != b.is_negative)
            {
                Product.is_negative = true;
            }
            else
            {
                Product.is_negative = false;
            }

            // Calculating each digit
            int apos = a.digits.Length - 1;
            int bpos = b.digits.Length - 1;
            int temp = Product.digits.Length - 1;                       // Temp and count used here to properly select index of b value being multiplied
            int count = 0;
            while (bpos >= b.digits.Length - b.significant_digits)
            {
                while (apos >= a.digits.Length - a.significant_digits)
                {
                    int avalue = a.digits[apos];
                    int bvalue = b.digits[bpos];
                    int pvalue = Product.digits[temp];
                    Product.digits[temp] = avalue * bvalue + pvalue;
                    apos--;
                    temp--;
                }
                bpos--;
                apos = a.digits.Length - 1;
                count++;
                temp = Product.digits.Length - 1 - count;
            }

            for (int k = Product.digits.Length - 1; k > 0; k--)         // Some indices will have integers > 10, but no larger than 81. We fix that here
            {                                                           // by subtracting index number by 10 and adding a carry to the next index until it's smaller than 9
                while (Product.digits[k] > 9)
                    {
                        Product.digits[k - 1] += 1;
                        Product.digits[k] -= 10;
                    }
            }

            // Updating Product attributes
            for (int h = 0; h < Product.digits.Length; h++)
                Product.number += Convert.ToString(Product.digits[h]);
            Product.significant_digits = Product.numberOfDigits();
            return Product;
        }
        // Part D Ends here

        public HugeInteger GetQuotient(HugeInteger dividend, HugeInteger divisor)
        {
            HugeInteger Quotient = new HugeInteger();
            HugeInteger Remainder = new HugeInteger();
            dividend.significant_digits = dividend.numberOfDigits();
            divisor.significant_digits = divisor.numberOfDigits();

            // Since we call subtraction here, best work with two huge integers that are "positive"
            bool dividendneg = dividend.is_negative;
            bool divisorneg = divisor.is_negative;
            dividend.is_negative = false;
            divisor.is_negative = false;

            // Divisor cannot be 0. I'll just returnt he dividend.
            if (divisor.number == "0")
            {
                Console.WriteLine("Divisor cannot be 0. Will output the dividend.");
                return dividend;
            }

            // Dividend must be greater than divisor, else our quotient is 0 and our remainder is the divisor.
            if (dividend.isGreaterThanOrEqualTo(divisor))
            {
                Quotient.digits = new int[dividend.significant_digits];
                Remainder.digits = new int[dividend.significant_digits];

                int quopos = Quotient.digits.Length - 1;                    // Quotient index position
                Remainder = Remainder.Subtract(dividend, divisor);          // Our first run of subtraction.
                int counter = 0;                                            // Counter to see how many times we do subtraction
                                                                            // (how many times divisor fits in dividend)
                while (!Remainder.is_negative)                              // We can count this by running until the remainder hits a negative number
                {
                    counter++;
                    Remainder = Remainder.Subtract(Remainder, divisor);
                }
                Remainder = Remainder.Add(Remainder,divisor);               // Undo one subtraction to update to the proper remainder
                if (dividend == divisor)
                    Console.WriteLine("The remainder is: " + "0" + " with " + "1" + " digits.");
                else
                    Console.WriteLine("The remainder is: " + Remainder.number + " with " + Remainder.numberOfDigits() + " digits.");
                                                                            // Output remainder value
                // Reset the original negative values of the dividend and divisor.
                dividend.is_negative = dividendneg;
                divisor.is_negative = divisorneg;

                Quotient.digits[quopos] = counter;                          // Set quotient at max index as counter value
                for (int k = Quotient.digits.Length - 1; k > 0; k--)
                {
                    while (Quotient.digits[k] > 9)
                    {
                        Quotient.digits[k - 1] += 1;
                        Quotient.digits[k] -= 10;
                    }
                }
                
                // Update Quotient attributes
                for (int h = 0; h < Quotient.digits.Length; h++)
                    Quotient.number += Convert.ToString(Quotient.digits[h]);
                Quotient.significant_digits = Quotient.numberOfDigits();
                return Quotient;
            }
            else // Since our divisor was greater than our dividend, division cannot occur. Output 0 as quotient and divisor as remainder.
            {
                Console.WriteLine("The remainder is: " + divisor.number + " with " + divisor.numberOfDigits() + " digits.");
                dividend.is_negative = dividendneg;
                divisor.is_negative = divisorneg;
                Quotient.number = "0";
                Quotient.significant_digits = Quotient.numberOfDigits();
                return Quotient;
            }
        }
        // Part E ends here.

        // Finding number of digits, one for updating Huge Integer, one for checking general strings
        // The updater will also update the "number" string associated with it
        public int numberOfDigits()
        {
            char[] individual_nums = new char[this.number.Length];  // Create a char array to check each char of the string
            for (int i = 0; i < this.number.Length; i++)
            {
                individual_nums[i] = this.number[i];
            }
            if (this.number.Length > 1)                             // this way, if our integer is 0, then it won't remove 0
            {
                int index = 0;
                for (int j = 0; j < individual_nums.Length; j++)    // This removes any leading 0s in our huge integer.
                {
                    if (!individual_nums[j].Equals('0'))
                    {
                        index = j;
                        break;
                    }
                }
                this.number = this.number.Remove(0, index);
            }
            this.digits = new int[this.number.Length];              // Update the number of digits
            this.FillHugeIntArray(this.number);                     // Fill all indices of the digits attribute
            return this.number.Length;
        }

        public int numberOfDigits(string x)
        {
            char[] individual_nums = new char[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                individual_nums[i] = x[i];
            }
            if (x.Length > 1)
            {
                int index = 0;
                for (int j = 0; j < individual_nums.Length; j++)
                {
                    if (!individual_nums[j].Equals('0'))
                    {
                        index = j;
                        break;
                    }
                }
                x = x.Remove(0, index);
            }
            return x.Length;
        }
        // Part F ends here

        // All boolean check functions
        public bool isEqualTo(HugeInteger x)
        {
            bool equal = false;
            if (this.significant_digits == x.significant_digits)
            {
                if (x.is_negative == this.is_negative)
                {
                    for (int i = 0; i < x.digits.Length; i++)
                    {
                        if (x.digits[i] == this.digits[i])
                            equal = true;
                        else
                        {
                            equal = false;
                            break;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return equal;
        }

        public bool isNotEqualTo(HugeInteger x)
        {
            return !this.isEqualTo(x);
        }

        public bool isGreaterThan(HugeInteger x)
        {
            if (this.isEqualTo(x))
                return false;
            else
            {
                if (this.is_negative && !x.is_negative)
                {
                    return false;
                }
                if (!this.is_negative && x.is_negative)
                {
                    return true;
                }
                if (!this.is_negative && !x.is_negative)
                {
                    if (this.significant_digits > x.significant_digits)
                        return true;
                    else if (this.significant_digits < x.significant_digits)
                        return false;
                    else
                    {
                        for (int i = 0; i < this.digits.Length; i++)
                        {
                            if (this.digits[i] < x.digits[i])
                                return false;
                            else if (this.digits[i] > x.digits[i])
                                return true;
                        }
                        return false;
                    }
                }
                else
                {
                    if (this.significant_digits < x.significant_digits)
                        return false;
                    else if (this.significant_digits > x.significant_digits)
                        return true;
                    else
                    {
                        for (int i = 0; i < this.digits.Length; i++)
                        {
                            if (this.digits[i] > x.digits[i])
                                return true;
                            else if (this.digits[i] < x.digits[i])
                                return false;
                        }
                        return false;
                    }
                }
            }
        }

        public bool isLessThan(HugeInteger x)
        {
            if (!this.isGreaterThan(x) && this.isNotEqualTo(x))
                return true;
            else
                return false;
        }

        public bool isGreaterThanOrEqualTo(HugeInteger x)
        {
            if (this.isGreaterThan(x) || this.isEqualTo(x))
                return true;
            else
                return false;
        }

        public bool isLessThanOrEqualTo(HugeInteger x)
        {
            if (this.isLessThan(x) || this.isEqualTo(x))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            if (this.is_negative == false)
                return "The positive number is: " + this.number + " with " + this.numberOfDigits() + " digits.";
            else
                return "The negative number is: " + this.number + " with " + this.numberOfDigits() + " digits.";
        }
    }
}