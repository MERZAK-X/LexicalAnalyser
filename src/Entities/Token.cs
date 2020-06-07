namespace LexicalAnalyzer.Entities
{
    internal enum Token
    {
        ID      = 1,
        KEYWORD = 12,
        ARTH_OP = 11, // & 2
        REL_OP  = 10, // & 5 & 9
        STRING  = 13,
        INT     = 4,
        REAL    = 8, // & 3
        COMMENT = 14
    }
}