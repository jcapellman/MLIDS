using System;

namespace jcIDS.lib.CommonObjects
{
    public class ReturnSet<T>
    {
        public T ObjectValue { get; }

        public Exception ObjectException { get; }

        public string ObjectExceptionInformation { get; }

        public bool HasObjectError => ObjectException != null;

        public ReturnSet(T objectValue)
        {
            ObjectValue = objectValue;
        }

        public ReturnSet(Exception exception, string additionalInformation)
        {
            ObjectException = exception;
            ObjectExceptionInformation = additionalInformation;
        }
    }
}