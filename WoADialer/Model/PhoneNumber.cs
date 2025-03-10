﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoADialer.Model
{
    public struct PhoneNumber
    {
        private static readonly char[] AllowedChars = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '*', '#' };
        private static readonly char[] Separators = new[] { ' ', '-', '(', ')' };

        public static PhoneNumber Parse(string text)
        {
            PhoneNumber result = new PhoneNumber
            {
                Numbers = text.Where(x => AllowedChars.Contains(x)).ToList()
            };
            return result;
        }

        private List<char> Numbers;

        public void RemoveLastChar()
        {
            if (Numbers != null && Numbers.Count > 0)
            {
                Numbers.RemoveAt(Numbers.Count - 1);
            }
        }

        public void AddLastChar(char @char)
        {
            if (Numbers == null)
            {
                Numbers = new List<char>();
            }
            if (AllowedChars.Contains(@char))
            {
                Numbers.Add(@char);
            }
        }

        public string ToString(string format)
        {
            if (Numbers == null)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder result = new StringBuilder();
                switch (format)
                {
                    case "None":
                        return ToString();
                    case "Italian":
                        if(Numbers.Count > 3)
                        {
                            result.Append(Numbers.Take(3).ToArray());
                            result.Append(' ');
                            result.Append(Numbers.TakeLast(Numbers.Count - 3).ToArray());
                        } else
                        {
                            result.Append(Numbers.Take(Numbers.Count).ToArray());
                        }
                        return result.ToString();
                    case "Russian":
                        switch (Numbers.Count)
                        {
                            case 4:
                                result.Append(Numbers.Take(2).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(2).ToArray());
                                break;
                            case 5:
                                result.Append(Numbers.Take(2).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(3).ToArray());
                                break;
                            case 6:
                                result.Append(Numbers.Take(3).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(3).ToArray());
                                break;
                            case 7:
                                result.Append(Numbers.Take(1).ToArray());
                                result.Append(' ');
                                result.Append(Numbers.Skip(1).Take(3).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(3).ToArray());
                                break;
                            case 8:
                                result.Append(Numbers.Take(2).ToArray());
                                result.Append(' ');
                                result.Append(Numbers.Skip(2).Take(3).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(3).ToArray());
                                break;
                            case 9:
                                result.Append(Numbers.Take(3).ToArray());
                                result.Append(' ');
                                result.Append(Numbers.Skip(3).Take(3).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(3).ToArray());
                                break;
                            case 10:
                                result.Append(Numbers.Take(3).ToArray());
                                result.Append(' ');
                                result.Append(Numbers.Skip(3).Take(3).ToArray());
                                result.Append('-');
                                result.Append(Numbers.Skip(6).Take(2).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(2).ToArray());
                                break;
                            case 11:
                                result.Append(Numbers.Take(1).ToArray());
                                result.Append(" (");
                                result.Append(Numbers.Skip(1).Take(3).ToArray());
                                result.Append(") ");
                                result.Append(Numbers.Skip(4).Take(3).ToArray());
                                result.Append('-');
                                result.Append(Numbers.Skip(7).Take(2).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(2).ToArray());
                                break;
                            case 12:
                                result.Append(Numbers.Take(2).ToArray());
                                result.Append(" (");
                                result.Append(Numbers.Skip(2).Take(3).ToArray());
                                result.Append(") ");
                                result.Append(Numbers.Skip(5).Take(3).ToArray());
                                result.Append('-');
                                result.Append(Numbers.Skip(8).Take(2).ToArray());
                                result.Append('-');
                                result.Append(Numbers.TakeLast(2).ToArray());
                                break;
                            default:
                                return ToString();
                        }
                        return result.ToString();
                    default:
                        return ToString();
                }
            }
        }

        public override string ToString()
        {
            return Numbers == null ? string.Empty : new string(Numbers.ToArray());
        }
    }
}
