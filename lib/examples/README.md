## Automaton

> Since **LexicalAnalyser** detects Tokens by their final states, the following final states rule must be respected for automaton's tokens !

**Token**|**FinalState**
:-----|:-----:
|ID|1|
|KEYWORD|12|
|ARTH OP|2, 11|
|REL OP|5, 9, 10|
|STRING|13|
|INT|4|
|REAL|3, 8|
|COMMENT|14|

<br>

![Simple Language Automaton](https://i.imgur.com/OxSj4LL.jpg)


## Automaton's file format
#### Template :
```
Number of states
Alphabets
Initial State
Final states separated by space
Language operators separated by space
Language keywords separated by space
Transitions {StartState Symbol EndState} [From 8th line to the end of the file]
Comments {# Comment} [Starting from line 8]
```
#### Examples :

- [Simple Language Automaton](https://github.com/MERZAK-X/LexicalAnalyser/blob/master/lib/examples/SimpleLanguageAutomaton.test) : Automaton file example

- [Source Code](https://github.com/MERZAK-X/LexicalAnalyser/blob/master/lib/examples/SourceCode.test) : Simple source code file example, that's accepted by the automaton above
