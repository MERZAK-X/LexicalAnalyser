# LexicalAnalyser
_Lexical Analyser_ is a program that determines whether a `source code` is accepted by a given **Deterministic Finite State Automaton** or not, and outputs each `lexeme` with its corresponding `token type` or whether was it not accepted using the given **DFSA**. 

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

``` bash
merzak-x@PR3C1S10N:~$ ./LexicalAnalyser SimpleLanguageAutomaton.test SourceCode.test
```

<details>
  <summary><b>Output result</b></summary>

```
merzak-x@PR3C1S10N:~$ ./LexicalAnalyser SimpleLanguageAutomaton.test SourceCode.test

Automaton [SimpleLanguageAutomaton] : 

E = {0, 1, 4, 2, 15, 3, 6, 7, 8, 5, 9, 10, 11} ; 

A = {a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, _, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, <, >, (, ), ", +, -, *, /, =} ; 

q₀ = 0 ; 

F = {1, 2, 3, 4, 5, 8, 9, 10, 11} ;

                                        
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

</details>

<details>
  <summary><b>Output result with -q</b></summary>

```
merzak-x@PR3C1S10N:~$ dotnet LexicalAnalyser.dll "/home/merzak-x/EMSI/C#/Projects/LexicalAnalyser/lib/examples/SimpleLanguageAutomaton.test" "/home/merzak-x/EMSI/C#/Projects/LexicalAnalyser/lib/examples/SourceCode.test" -q


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

</details>

<details>
  <summary><b>Output result with -v</b></summary>

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

</details>

<details>
  <summary><b>Output result with -vv</b></summary>

```
merzak-x@PR3C1S10N:~$ dotnet LexicalAnalyser.dll "/home/merzak-x/EMSI/C#/Projects/LexicalAnalyser/lib/examples/SimpleLanguageAutomaton.test" "/home/merzak-x/EMSI/C#/Projects/LexicalAnalyser/lib/examples/SourceCode.test" -vv

Automaton [SimpleLanguageAutomaton] : 

E = {0, 1, 4, 2, 15, 3, 6, 7, 8, 5, 9, 10, 11} ; 

A = {a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, _, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, <, >, (, ), ", +, -, *, /, =} ; 

Transitions: {
        σ(0, a) = 1
        σ(0, b) = 1
        σ(0, c) = 1
        σ(0, d) = 1
        σ(0, e) = 1
        σ(0, f) = 1
        σ(0, g) = 1
        σ(0, h) = 1
        σ(0, i) = 1
        σ(0, j) = 1
        σ(0, k) = 1
        σ(0, l) = 1
        σ(0, m) = 1
        σ(0, n) = 1
        σ(0, o) = 1
        σ(0, p) = 1
        σ(0, q) = 1
        σ(0, r) = 1
        σ(0, s) = 1
        σ(0, t) = 1
        σ(0, u) = 1
        σ(0, v) = 1
        σ(0, w) = 1
        σ(0, x) = 1
        σ(0, y) = 1
        σ(0, z) = 1
        σ(0, _) = 1
        σ(1, 0) = 1
        σ(1, 1) = 1
        σ(1, 2) = 1
        σ(1, 3) = 1
        σ(1, 4) = 1
        σ(1, 5) = 1
        σ(1, 6) = 1
        σ(1, 7) = 1
        σ(1, 8) = 1
        σ(1, 9) = 1
        σ(1, a) = 1
        σ(1, b) = 1
        σ(1, c) = 1
        σ(1, d) = 1
        σ(1, e) = 1
        σ(1, f) = 1
        σ(1, g) = 1
        σ(1, h) = 1
        σ(1, i) = 1
        σ(1, j) = 1
        σ(1, k) = 1
        σ(1, l) = 1
        σ(1, m) = 1
        σ(1, n) = 1
        σ(1, o) = 1
        σ(1, p) = 1
        σ(1, q) = 1
        σ(1, r) = 1
        σ(1, s) = 1
        σ(1, t) = 1
        σ(1, u) = 1
        σ(1, v) = 1
        σ(1, w) = 1
        σ(1, x) = 1
        σ(1, y) = 1
        σ(1, z) = 1
        σ(1, _) = 1
        σ(0, 0) = 4
        σ(0, 1) = 4
        σ(0, 2) = 4
        σ(0, 3) = 4
        σ(0, 4) = 4
        σ(0, 5) = 4
        σ(0, 6) = 4
        σ(0, 7) = 4
        σ(0, 8) = 4
        σ(0, 9) = 4
        σ(0, -) = 2
        σ(2, 0) = 4
        σ(2, 1) = 4
        σ(2, 2) = 4
        σ(2, 3) = 4
        σ(2, 4) = 4
        σ(2, 5) = 4
        σ(2, 6) = 4
        σ(2, 7) = 4
        σ(2, 8) = 4
        σ(2, 9) = 4
        σ(4, 0) = 4
        σ(4, 1) = 4
        σ(4, 2) = 4
        σ(4, 3) = 4
        σ(4, 4) = 4
        σ(4, 5) = 4
        σ(4, 6) = 4
        σ(4, 7) = 4
        σ(4, 8) = 4
        σ(4, 9) = 4
        σ(4, .) = 15
        σ(15, 0) = 3
        σ(15, 1) = 3
        σ(15, 2) = 3
        σ(15, 3) = 3
        σ(15, 4) = 3
        σ(15, 5) = 3
        σ(15, 6) = 3
        σ(15, 7) = 3
        σ(15, 8) = 3
        σ(15, 9) = 3
        σ(3, 0) = 3
        σ(3, 1) = 3
        σ(3, 2) = 3
        σ(3, 3) = 3
        σ(3, 4) = 3
        σ(3, 5) = 3
        σ(3, 6) = 3
        σ(3, 7) = 3
        σ(3, 8) = 3
        σ(3, 9) = 3
        σ(3, e) = 6
        σ(6, ^) = 7
        σ(7, 0) = 8
        σ(7, 1) = 8
        σ(7, 2) = 8
        σ(7, 3) = 8
        σ(7, 4) = 8
        σ(7, 5) = 8
        σ(7, 6) = 8
        σ(7, 7) = 8
        σ(7, 8) = 8
        σ(7, 9) = 8
        σ(8, 0) = 8
        σ(8, 1) = 8
        σ(8, 2) = 8
        σ(8, 3) = 8
        σ(8, 4) = 8
        σ(8, 5) = 8
        σ(8, 6) = 8
        σ(8, 7) = 8
        σ(8, 8) = 8
        σ(8, 9) = 8
        σ(0, =) = 5
        σ(0, <) = 9
        σ(0, >) = 10
        σ(9, =) = 5
        σ(9, >) = 5
        σ(10, =) = 5
        σ(0, +) = 11
        σ(0, *) = 11
        σ(0, /) = 11
} ; 

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

</details>

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