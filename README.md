# LexicalAnalyser
Lexical Analyser is a program that determines whether a `source code` is accepted by a given `DFSA`. 

## Usage

```
merzak-x@PR3C1S10N:~$ /usr/share/dotnet/dotnet /home/merzak-x/EMSI/C#/Projects/LexicalAnalyzer/bin/Debug/netcoreapp3.1/LexicalAnalyzer.dll "/home/merzak-x/EMSI/C#/Projects/DFA-Analyzer/lib/examples/Test Automaton.txt" "/home/merzak-x/EMSI/C#/Projects/DFA-Analyzer/lib/examples/SourceCode.txt"

Automaton [Test Automaton] : E = {1, 0, 2, 3, 4, 5, 6, 7} ; A = {a, b, c, d, e} ; Transitions: { [ σ(0, a) = 1 ]  [ σ(0, b) = 2 ]  [ σ(1, a) = 2 ]  [ σ(1, b) = 3 ]  [ σ(2, e) = 4 ]  [ σ(3, a) = 4 ]  [ σ(3, c) = 3 ]  [ σ(4, b) = 5 ]  [ σ(5, d) = 6 ]  [ σ(5, e) = 7 ] } ; q₀ = 0 ; F = {5, 6, 7}

<>

Process finished with exit code 0.


```