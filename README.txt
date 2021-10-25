1. I would test from the bottom up. I would see who has the smallest word count according to 
the program and see if their word count really matches up. And then, I would continue this line
of logic until I was somewhat confident with my programs output.

2. Multithreading in this case did not help, measuring the time elapsed between
 single-threading and multi-threading, I see that single-threading time elapsed is 130ms and 
multi-threading time elapsed is 143ms. It actually seems like multi-threading was more 
detrimental both to runtime (albeit quite small) and difficulty in coding.

Multi-threading could be helpful if we had a lot more txt files (or bigger files) to search
through. If there was more data then more threads would have been easier but because we had a
relatively small dataset single-threading was more effective.

3. I would probably append the name of the file I am referring to as part of each character's
key in the dictionary. For example, instead of "king", the key would be 
"king - shakespeare_hamlet". That way, that key is localized purely to that txt file. 