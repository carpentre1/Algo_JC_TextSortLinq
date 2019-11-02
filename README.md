# Algo_JC_TextSortLinq
Using LINQ to sort a large list and return certain words

For the app to run, you'll need to make a Dictionary folder on the C drive and place the provided words_alpha.txt inside it.
I tried using relative path so that this wouldn't be necessary, but I couldn't figure out how to access the words_alpha file in the relative path.

When you run the app, it will reverse alphabetize the list, then create 3 new files in the Dictionary folder:

words that start with "z",

words that start with "he",

and words that have "e" at the second position.

It will also report the time it took to find all of those words. On my machine it took less than 100 ms per operation.
