using System;
using System.Collections.Generic;
namespace Lab3Q1
{
    public class WordCountTester
    {
        /*static int Main()
        {
            try {
                List<string> testList = new List<string>();
                testList.Add("   This has a ton of whitespace in the beginning");
                testList.Add("This has a ton of whitespace at the end     ");
                testList.Add("Thishasbasicallynowhitespace");
                testList.Add("This has a normal amount of whitespace");
                testList.Add("    This has a ton of whitespace at the beginning AND end    ");
                testList.Add("  I   randomly hit    the   space bar            as many    times  as    I             felt  like       it         ");
                testList.Add("");

                int[] testListCount = new int[] { 9, 6, 1, 5, 9, 7, 0 };
                int[] startIndex = new int[]    { 0, 3, 0, 2, 2, 7, 0 };

                int i = 0;
                foreach (string line in testList)
                {
                    WCTester(line, startIndex[i], testListCount[i]);
                    i++;
                }
                //WCTester(line, startIdx, expectedResults);

            } catch (UnitTestException e) {
                Console.WriteLine(e);
            }
            return 0;

        }


        /**
         * Tests word_count for the given line and starting index
         * @param line line in which to search for words
         * @param start_idx starting index in line to search for words
         * @param expected expected answer
         * @throws UnitTestException if the test fails
         */
          static void WCTester(string line, int start_idx, int expected) {

            //=================================================
            // Implement: comparison between the expected and
            // the actual word counter results
            //=================================================
            int result = HelperFunctions.WordCount(ref line, start_idx);

            if (result != expected) {
              throw new Lab3Q1.UnitTestException(ref line, start_idx, result, expected, String.Format("UnitTestFailed: result:{0} expected:{1}, line: {2} starting from index {3}", result, expected, line, start_idx));
            }

           }
    }
}
