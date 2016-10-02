using System;

namespace jcIDS.Library.Common {
    public class JCIDSException : Exception {
        private readonly DateTimeOffset _timeStamp;
        private readonly string _exceptionMessage;

        public JCIDSException() { }

        public JCIDSException(string exceptionMessage) {
            _exceptionMessage = exceptionMessage;
            _timeStamp = DateTimeOffset.Now;
        }

        public override string ToString() => $"{_timeStamp} - {_exceptionMessage}";
    }
}