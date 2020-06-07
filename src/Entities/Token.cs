namespace LexicalAnalyzer.Entities
{
    internal enum Token
    {
        ID      = 1,
        KEYWORD = 12,
        ARTH_OP = 11,
        REL_OP  = 10,
        STRING  = 13,
        INT     = 4,
        REAL    = 8,
        COMMENT = 14
    }
}