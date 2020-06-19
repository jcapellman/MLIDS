using MLIDS.Scripter.lib.Enums;

namespace MLIDS.Scripter.lib
{
    public abstract class BaseVector
    {
        public abstract int NumRequiredArguments { get; }

        public abstract ScriptVectors ScriptType { get; }

        public string[] Arguments { get; set; }

        public abstract void Execute();
    }
}