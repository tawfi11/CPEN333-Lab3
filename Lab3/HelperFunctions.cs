using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


namespace Lab3Q1
{
    public class HelperFunctions
    {
        /**
         * Counts number of words, separated by spaces, in a line.
         * @param line string in which to count words
         * @param start_idx starting index to search for words
         * @return number of words in the line
         */
        public static int WordCount(ref string line, int start_idx)
        {
            int count = 0;
            line = line.Substring(start_idx);
            string[] words = line.Split(' ');
            words = words.Where(w => w != "").ToArray();
            count = words.Length;

            return count;
        }


        /**
        * Reads a file to count the number of words each actor speaks.
        *
        * @param filename file to open
        * @param mutex mutex for protected access to the shared wcounts map
        * @param wcounts a shared map from character -> word count
        */
        public static void countcharacterwords(string filename,
                                 Mutex mutex,
                                 Dictionary<string, int> wcounts)
        {

            //===============================================
            //  implement this method including thread safety
            //===============================================

            string line;  // for storing each line read from the file
            string character = "";  // empty character to start
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            while ((line = file.ReadLine()) != null)
            {
                //=================================================
                // your job to add word count information to map
                //=================================================

                // is the line a dialogueline?
                //    if yes, get the index and the character name.
                //      if index > 0 and character not empty
                //        get the word counts
                //          if the key exists, update the word counts
                //          else add a new key-value to the dictionary
                //    reset the character
                int index = isdialogueline(line, ref character);
                if(index > 0 && character != "")
                {
                    int wordCount = WordCount(ref line, index);
                    mutex.WaitOne();
                    if (wcounts.ContainsKey(character))
                    {
                        wcounts[character] += wordCount;
                    }
                    else
                    {
                        wcounts.Add(character, wordCount);
                    }
                    mutex.ReleaseMutex();
                    character = "";
                }

            }
            // close the file
            file.Close();
        }



        ///**
         /* checks if the line specifies a character's dialogue, returning
         * the index of the start of the dialogue.  if the
         * line specifies a new character is speaking, then extracts the
         * character's name.
         *
         * assumptions: (doesn't have to be perfect)
         *     line that starts with exactly two spaces has
         *       character. <dialogue>
         *     line that starts with exactly four spaces
         * continues the dialogue of previous character
         *
         * @param line line to check
         * @param character extracted character name if new character,
         *        otherwise leaves character unmodified
         * @return index of start of dialogue if a dialogue line,
         *      -1 if not a dialogue line
         */
        static int isdialogueline(string line, ref string character)
        {

            // new character
            if (line.Length >= 3 && line[0] == ' '
                && line[1] == ' ' && line[2] != ' ')
            {
                // extract character name

                int start_idx = 2;
                int end_idx = 3;
                while (end_idx <= line.Length && line[end_idx - 1] != '.')
                {
                    ++end_idx;
                }

                // no name found
                if (end_idx >= line.Length)
                {
                    return 0;
                }

                // extract character's name
                character = line.Substring(start_idx, end_idx - start_idx - 1);
                return end_idx;
            }

            // previous character
            if (line.Length >= 5 && line[0] == ' '
                && line[1] == ' ' && line[2] == ' '
                && line[3] == ' ' && line[4] != ' ')
            {
                // continuation
                return 4;
            }

            return 0;
        }

        /**
         * sorts characters in descending order by word count
         *
         * @param wcounts a map of character -> word count
         * @return sorted vector of {character, word count} pairs
         */
        public static List<Tuple<int, string>> sortcharactersbywordcount(Dictionary<string, int> wordcount)
        {
            // implement sorting by word count here
            List<Tuple<int, string>> sortedbyvaluelist = new List<Tuple<int, string>>();

            foreach(KeyValuePair<string,int> kvp in wordcount.OrderByDescending(key => key.Value))
            {
                sortedbyvaluelist.Add(new Tuple<int, string>(kvp.Value, kvp.Key));
            }
            
           /*foreach(KeyValuePair<string, int> keyValuePair in wordcount)
            {
                Tuple<int, string> tuple = new Tuple<int, string>(keyValuePair.Value, keyValuePair.Key);
                if(sortedbyvaluelist.Count == 0)
                {
                    sortedbyvaluelist.Add(tuple);
                }
                else
                {
                    int i = 0;
                    foreach(Tuple<int, string> t in sortedbyvaluelist)
                    {
                        if(t.Item1 <= tuple.Item1)
                        {
                            sortedbyvaluelist.Insert(i, tuple);
                            break;
                        }
                        else if(i == sortedbyvaluelist.Count - 1)
                        {
                            sortedbyvaluelist.Add(tuple);
                        }
                    }
                }
            }*/
            //printlistoftuples(sortedbyvaluelist);
            return sortedbyvaluelist;

        }


        ///**
        // * prints the list of tuple<int, string>
        // *
        // * @param sortedlist
        // * @return nothing
        // */
        public static void printlistoftuples(List<Tuple<int, string>> sortedlist)
        {
            // implement printing here
            foreach(Tuple<int, string> t in sortedlist)
            {
                Console.WriteLine($"Word count: {t.Item1}\n\"{t.Item2}\"\n");
            }
        }
    }
}
