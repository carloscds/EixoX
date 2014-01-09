﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class StringHelper
    {
        public const string HexAlphabet = "0123456789abcdef";

        public static string DigitsOnly(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            StringBuilder builder = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
                if (Char.IsDigit(value, i))
                    builder.Append(value[i]);
            return builder.ToString();
        }

        public static string Right(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            else if (length >= input.Length)
                return input;
            else
                return input.Substring(input.Length - length, length);
        }

        public static string Left(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            else if (length >= input.Length)
                return input;
            else
                return input.Substring(0, length);
        }

        public static IEnumerable<object> Split(string input, Type conversionType, params char[] separator)
        {
            return Split(input, conversionType, System.Globalization.CultureInfo.CurrentUICulture, separator);
        }

        public static IEnumerable<object> Split(string input, Type conversionType, IFormatProvider formatProvider, params char[] separator)
        {
            string[] spl = input.Split(separator);
            for (int i = 0; i < spl.Length; i++)
                yield return Convert.ChangeType(spl[i], conversionType, formatProvider);
        }

        public static int NextNonWhiteSpaceIndex(string input, int start, int maxlength)
        {
            while (char.IsWhiteSpace(input, start) && start < maxlength)
                start++;
            return start;
        }

        public static int NextIndex(string input, int start, int maxlength, char marker, char escapeChar)
        {
            while (input[start] != marker && start < maxlength && (start > 0 && input[start - 1] != escapeChar))
                start++;
            return start;
        }

        public static string SqlSafeString(string input)
        {
            return input.Replace("'", "''").Replace("\\", "\\\\");
        }

        public static string HexEncode(byte[] input, int offset, int length)
        {
            char[] chars = new char[length * 2];
            for (int i = 0; i < length; i++)
            {
                chars[(i * 2)] = HexAlphabet[input[i + offset] / 16];
                chars[(i * 2) + 1] = HexAlphabet[input[i + offset] % 16];
            }
            return new string(chars);
        }

        public static string HexEncode(byte[] input)
        {
            return HexEncode(input, 0, input.Length);
        }

        public static byte[] HexDecode(string input, int offset, int length)
        {
            byte[] arr = new byte[length / 2];
            for (int i = 0; i < length; i++)
            {
                arr[i] =
                    (byte)(HexAlphabet.IndexOf(input[((offset + i) * 2)]) * 16);

                arr[i] +=
                    (byte)(HexAlphabet.IndexOf(input[((offset + i) * 2) + 1]));
            }
            return arr;
        }

        public static byte[] HexDecode(string input)
        {
            return HexDecode(input, 0, input.Length);
        }

        public static string StringMaxLength(string input, int maxLenght)
        {

            string output = "";
            if (input.Length > maxLenght)
            {
                output = input.Substring(0, (maxLenght - 1)) + "...";
            }
            else
            {
                output = input;
            }

            return output;
        }

        public static string Join<T>(string separator, IList<T> objects)
        {
            List<string> aux = new List<string>();
            foreach (object obj in objects)
                aux.Add(obj.ToString());

            return String.Join(separator, aux.ToArray());
        }

        public static IEnumerable<int> ParseInt(params string[] strings)
        {
            if (strings != null)
                for (int i = 0; i < strings.Length; i++)
                {
                    int value;
                    if (int.TryParse(strings[i], out value) && value != 0)
                        yield return value;
                }
        }



        public static string Join(string separator, System.Collections.IEnumerable items)
        {
            if (items != null)
            {
                StringBuilder builder = new StringBuilder();
                bool prependComma = false;
                foreach (Object o in items)
                {
                    if (o != null)
                    {
                        if (prependComma)
                            builder.Append(separator);
                        else
                            prependComma = true;

                        builder.Append(o.ToString());
                    }
                }

                return builder.ToString();
            }
            else
                return null;

            


        }
    }
}