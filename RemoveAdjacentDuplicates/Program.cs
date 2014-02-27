using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveAdjacentDuplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = "azxxzy";
            string input = "xxxyyz";
            // string input = "axxxxa";
            //string input = "geeksforgeeg";
            //string input = "caaabbbaacdddd";
            // string input = "acaaabbbacdddd";
            Console.WriteLine("Test case 1: Output is {0}", RemoveAdjacentDuplicateCharacter(input));
            Console.WriteLine("Test case 2: Output is {0}", RemoveAdjacentDuplicateCharacters2(input));
        }

        // Time complexity - O(n^2)
        static string RemoveAdjacentDuplicateCharacter(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder(input);

            bool duplicatesfound = false;
            int start, end = 0;

            int current = 0;
            while (current < sb.Length)
            {
                int count = 0;

                start = current;
                end = current + 1;
                while ((end < sb.Length) && (sb[start] == sb[end]))
                {
                    count++;
                    end++;
                }

                if (count > 0)
                {
                    duplicatesfound = true;
                    sb.Remove(start, count + 1);

                    // Since start has been removed now reset i = start - 1
                    current = start - 1;
                }

                current++;
            }

            if (!duplicatesfound)
            {
                return sb.ToString();
            }

            return RemoveAdjacentDuplicateCharacter(sb.ToString());
        }

        static string RemoveAdjacentDuplicateCharacters2(string input)
        {
            char lastRemoved = '\0';
            return RemoveUtil(input, ref lastRemoved);
        }

        static string RemoveUtil(string str, ref char lastRemoved)
        {
            if (String.IsNullOrEmpty(str) || str.Length == 1)
            {
                return str;
            }

            StringBuilder sb = new StringBuilder(str);

            // Remove duplicates at left corner if any and recurse for the 
            // remaining string
            if (sb[0] == sb[1])
            {
                lastRemoved = sb[0];

                int start = 0;
                int end = 0;
                while (end < sb.Length && sb[start] == sb[end])
                {
                    end++;
                }

                sb.Remove(0, end);

                return RemoveUtil(sb.ToString(), ref lastRemoved);
            }

            // If no duplicate characters found, save first character
            char first = sb[0];

            string remString = RemoveUtil(sb.Remove(0, 1).ToString(), ref lastRemoved);

            StringBuilder temp = new StringBuilder(remString);

            // Check if first character of remString is same as 'first'
            // if so remove the first character in remString.
            // There wont be more than one match since all the others
            // would have been removed by the earlier check
            if (!String.IsNullOrEmpty(remString) && remString[0] == first)
            {
                return temp.Remove(0, 1).ToString();
            }
            else if (!String.IsNullOrEmpty(remString) && (remString[0] == lastRemoved))
            {
                return temp.ToString();
            }
            else
            {
                temp.Insert(0, first);
                return temp.ToString();
            }
        }
    }
}
