# LexicalAnalyser
_Lexical Analyser_ is a program that determines whether a `source code` is accepted by a given `DFSA` **Deterministic Finite State Automaton**. 

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

## Usage
[![Requirements](https://badgen.net/badge/Requirements/.NET%20Core%20Runtime/red)](https://aka.ms/dotnet-core-applaunch)
>  Please make sure that `.NET Core` runtime is installed before running **LexicalAnalyser**, if not visit : https://aka.ms/dotnet-core-applaunch

```
merzak-x@PR3C1S10N:~$ dotnet LexicalAnalyser.dll "/home/merzak-x/EMSI/C#/Projects/LexicalAnalyser/lib/examples/SimpleLanguageAutomaton.test" "/home/merzak-x/EMSI/C#/Projects/LexicalAnalyser/lib/examples/SourceCode.test" -v

Automaton [SimpleLanguageAutomaton] : 

E = {0, 1, 4, 2, 15, 3, 6, 7, 8, 5, 9, 10, 11} ; 

A = {a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, _, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, <, >, (, ), ", +, -, *, /, =} ; 

q₀ = 0 ; 

F = {1, 2, 3, 4, 5, 8, 9, 10, 11} ;

Source code : 

‎`‎`‎`‎
(* test comment *)
BEGIN
var = "some quite long string with ¢ħæræŧ€rß +°c §.-?";
IF 13.4 >= 77
THEN
    variable = 99 - 98;
ELSE
    var=17.54E^485512
    1-2;9=8;8/9;
    var*-1<=77/18;
    test=99+98+1;
END
‎`‎`‎`‎


✓ <KEYWORD,{BEGIN}>
✓ <ID,{var}>
✓ <REL_OP,{=}>
✓ <STRING,{some quite long string with ¢ħæræŧ€rß +°c §.-?}>
✓ <KEYWORD,{IF}>
✓ <REAL,{13.4}>
✓ <REL_OP,{>=}>
✓ <INT,{77}>
✓ <KEYWORD,{THEN}>
✓ <ID,{variable}>
✓ <REL_OP,{=}>
✓ <INT,{99}>
✓ <ARTH_OP,{-}>
✓ <INT,{98}>
✓ <KEYWORD,{ELSE}>
✓ <ID,{var}>
✓ <REL_OP,{=}>
✓ <REAL,{17.54E^485512}>
✓ <INT,{1}>
✓ <INT,{-2}>
✓ <INT,{9}>
✓ <REL_OP,{=}>
✓ <INT,{8}>
✓ <INT,{8}>
✓ <ARTH_OP,{/}>
✓ <INT,{9}>
✓ <ID,{var}>
✓ <ARTH_OP,{*}>
✓ <INT,{-1}>
✓ <REL_OP,{<=}>
✓ <INT,{77}>
✓ <ARTH_OP,{/}>
✓ <INT,{18}>
✓ <ID,{test}>
✓ <REL_OP,{=}>
✓ <INT,{99}>
✓ <ARTH_OP,{+}>
✓ <INT,{98}>
✓ <ARTH_OP,{+}>
✓ <INT,{1}>
✓ <KEYWORD,{END}>

✓ The source file `SourceCode.test` is accepted by the automaton's described language !


Process finished with exit code 0.

```

```
LexicalAnalyser v1.4: https://github.com/MERZAK-X/LexicalAnalyser

Usage: dotnet LexicalAnalyser.dll [[Automaton] [Sourcecode]] [-v|-vv|-q] [--help]

    Arguments:
        Automaton       Path to the Automaton's file
        Source          Path to the source code file to be analysed
    Options:
        -v, -vv         Verbose level, 1 or 2 respectively, if not set 0
        -q              Quiet (verbose level -1), only display results
        --help          Display this help and exit
        
Examples:
    ./LexicalAnalyser SimpleLanguageAutomaton.test SourceCode.test -v
    dotnet LexicalAnalyser.dll lib/examples/SimpleLanguageAutomaton.test lib/examples/SourceCode.test

Documentation: https://git.io/JfNf4

Copyright (C) 2020 "NUL-X"
```
#### Developers

[![MERZAK-X](https://badgen.net/badge/Developer/MERZAK-X/black?icon=github)](https://github.com/MERZAK-X)