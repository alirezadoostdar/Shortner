using System.Diagnostics.Metrics;

namespace ShortnerUrl.Observebility
{
    public sealed class ShortenDiagnotics
    {
        public const string MeterName = "Shorten";
        public const string GenerateCode = "GenerateCode";
        public const string Rediect = "Rediect";
        private readonly Counter<long> _redirectCounter;
        private readonly Counter<long> _shortenCounter;

        public ShortenDiagnotics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MeterName);
            _redirectCounter = meter.CreateCounter<long>(Rediect);
            _shortenCounter = meter.CreateCounter<long>(GenerateCode);
        }

        public void AddRedirection() => _redirectCounter.Add(1);
        public void AddShorten() => _shortenCounter.Add(1);
    }
}
