using System.Diagnostics.Metrics;

namespace ContosoPizza
{
    public class InstrumentationService : IDisposable
    {

        internal const string MeterName = "Examples.AspNetCore";
        private readonly Meter meter;
        internal const string RequestCountName = "pizza_requests_total";
        
        // public static readonly Histogram<double> RequestDuration = Meter.CreateHistogram<double>("pizza_request_duration_seconds");
        internal const string RequestDurationName = "pizza_request_duration_seconds";
        internal const string ErrorCountName = "pizza_errors_total";
        private bool _disposed = false;

        public InstrumentationService()
        {
            string? version = typeof(InstrumentationService).Assembly.GetName().Version?.ToString();
            this.meter = new Meter(MeterName, version);

            this.RequestCount = this.meter.CreateCounter<long>(RequestCountName, description: "The number of requsets to the pizza service");

            this.RequestDuration = this.meter.CreateHistogram<double>(RequestDurationName, description: "The duration of requests to the pizza service");
            this.ErrorCount = this.meter.CreateCounter<long>(ErrorCountName, description: "The number of errors from the pizza service");   
        }

        public Counter<long> RequestCount { get; }

        public Histogram<double> RequestDuration { get; }

        public Counter<long> ErrorCount { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    this.meter.Dispose();
                }

                // Dispose unmanaged resources if any

                _disposed = true;
            }
        }

        ~InstrumentationService()
        {
            Dispose(false);
        }
    }
}