using System;

namespace HugeIntegers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing creation of huge integers
            Console.WriteLine("Creation of huge integers testing begins here.\n");

            HugeInteger bignumber = new HugeInteger("5");
            Console.WriteLine(bignumber.ToString());
            Console.WriteLine();

            HugeInteger bignumber2 = new HugeInteger("7");
            Console.WriteLine(bignumber2.ToString());
            Console.WriteLine();

            HugeInteger bignumber3 = new HugeInteger("");
            bignumber3.FillHugeIntArray("721");
            Console.WriteLine(bignumber3.ToString());
            Console.WriteLine();

            HugeInteger bignumber4 = new HugeInteger("826");
            HugeInteger bignumber5 = new HugeInteger("51", true);
            HugeInteger bignumber6 = new HugeInteger("37", true);
            HugeInteger bignumber7 = new HugeInteger("652");
            HugeInteger bignumber8 = new HugeInteger("567000", true);
            HugeInteger bignumber9 = new HugeInteger("4382715982137598");
            HugeInteger bignumber10 = new HugeInteger("38927659236598");
            HugeInteger addresult = new HugeInteger("");
            HugeInteger subresult = new HugeInteger("");
            HugeInteger multresult = new HugeInteger("1000");
            HugeInteger divresult = new HugeInteger("");

            // Testing addition
            Console.WriteLine("Addition testing begins here.\n");

            addresult = addresult.Add(bignumber, bignumber2);
            Console.WriteLine(addresult.ToString());
            Console.WriteLine();

            addresult = addresult.Add(bignumber3, bignumber4);
            Console.WriteLine(addresult.ToString());
            Console.WriteLine();

            addresult = addresult.Add(bignumber5, bignumber6);
            Console.WriteLine(addresult.ToString());
            Console.WriteLine();

            addresult = addresult.Add(bignumber7, bignumber8);
            Console.WriteLine(addresult.ToString());
            Console.WriteLine();

            addresult = addresult.Add(bignumber9, bignumber10);
            Console.WriteLine(addresult.ToString());
            Console.WriteLine();

            // Testing Subtraction
            Console.WriteLine("Subtraction testing begins here.\n");

            subresult = subresult.Subtract(bignumber, bignumber2);
            Console.WriteLine(subresult.ToString());
            Console.WriteLine();

            subresult = subresult.Subtract(bignumber3, bignumber4);
            Console.WriteLine(subresult.ToString());
            Console.WriteLine();

            subresult = subresult.Subtract(bignumber5, bignumber6);
            Console.WriteLine(subresult.ToString());
            Console.WriteLine();

            subresult = subresult.Subtract(bignumber7, bignumber8);
            Console.WriteLine(subresult.ToString());
            Console.WriteLine();

            subresult = subresult.Subtract(bignumber9, bignumber10);
            Console.WriteLine(subresult.ToString());
            Console.WriteLine();

            // Testing Multiplication
            Console.WriteLine("Multiplication testing begins here.\n");

            for (int i = 999; i > 0; i--)
            {
                multresult = multresult.Multiply(multresult, bignumber.FillHugeIntArray(Convert.ToString(i)));
            }
            //multresult = multresult.Multiply(bignumber, bignumber2);
            Console.WriteLine(multresult.ToString());
            Console.WriteLine();

            //multresult = multresult.Multiply(bignumber3, bignumber4);
            //Console.WriteLine(multresult.ToString());
            //Console.WriteLine();

            //multresult = multresult.Multiply(bignumber5, bignumber6);
            //Console.WriteLine(multresult.ToString());
            //Console.WriteLine();

            //multresult = multresult.Multiply(bignumber7, bignumber8);
            //Console.WriteLine(multresult.ToString());
            //Console.WriteLine();

            //multresult = multresult.Multiply(bignumber9, bignumber10);
            //Console.WriteLine(multresult.ToString());
            //Console.WriteLine();

            // Testing Division
            Console.WriteLine("Division testing begins here.\n");

            divresult = divresult.GetQuotient(bignumber, bignumber2);
            Console.WriteLine(divresult.ToString());
            Console.WriteLine();

            divresult = divresult.GetQuotient(bignumber3, bignumber4);
            Console.WriteLine(divresult.ToString());
            Console.WriteLine();

            divresult = divresult.GetQuotient(bignumber5, bignumber6);
            Console.WriteLine(divresult.ToString());
            Console.WriteLine();

            divresult = divresult.GetQuotient(bignumber7, bignumber8);
            Console.WriteLine(divresult.ToString());
            Console.WriteLine();

            divresult = divresult.GetQuotient(bignumber9, bignumber10);
            Console.WriteLine(divresult.ToString());
            Console.WriteLine();

            // Testing different cases, attributes, and boolean commands.

            //Console.WriteLine(bignumber10.number);
            //Console.WriteLine(bignumber10.numberOfDigits());
            //Console.WriteLine(bignumber9.number);
            //Console.WriteLine(bignumber9.numberOfDigits());

            //HugeInteger softEng = new HugeInteger("4318");
            //HugeInteger artiInt = new HugeInteger("4317");

            //addresult = addresult.Add(artiInt, softEng);
            //Console.WriteLine("Add: " + addresult.ToString());
            //subresult = subresult.Subtract(artiInt, softEng);
            //Console.WriteLine("Sub: " + subresult.ToString());
            //multresult = multresult.Multiply(artiInt, softEng);
            //Console.WriteLine("Mult: " + multresult.ToString());
            //divresult = divresult.GetQuotient(artiInt, softEng);
            //Console.WriteLine("Div: " + divresult.ToString());

            //Console.WriteLine("{0} {1}", bignumber3.isNegative, bignumber3.SignificantDigits);
            //Console.WriteLine("{0} {1}", bignumber.isNegative, bignumber.SignificantDigits);
            //Console.WriteLine(bignumber.isEqualTo(bignumber3));
            //Console.WriteLine(bignumber.isNotEqualTo(bignumber3));
            //Console.WriteLine("Is greater than: " + bignumber2.isGreaterThan(bignumber));
            //Console.WriteLine("Is greater than: " + bignumber.isGreaterThan(bignumber3));
            //Console.WriteLine("Is greater than: " + bignumber.isGreaterThan(bignumber2));
            //Console.WriteLine("Is less than: " + bignumber2.isLessThan(bignumber));
            //Console.WriteLine("Is less than: " + bignumber.isLessThan(bignumber3));
            //Console.WriteLine("Is less than: " + bignumber.isLessThan(bignumber2));
        }
    }
}
