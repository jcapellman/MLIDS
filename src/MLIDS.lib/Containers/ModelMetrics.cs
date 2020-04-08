using System;

using Microsoft.ML.Data;

namespace MLIDS.lib.Containers
{
    public class ModelMetrics
    {
        public TimeSpan Duration { get; set; }

        public AnomalyDetectionMetrics Metrics { get; set; }

        public int NumCleanRows { get; set; }

        public int NumMaliciousRows { get; set; }
    }
}