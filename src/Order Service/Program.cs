using Polly;
using Polly.Retry;

var retryStrategy = new RetryStrategyOptions
{
    MaxRetryAttempts = 5,
    Delay = TimeSpan.FromSeconds(5)
};
var pipeline = new ResiliencePipelineBuilder().AddRetry(retryStrategy).Build();

var act = () =>
{
    var httpclient = new HttpClient();
    httpclient.BaseAddress = new Uri("http://localhost:5017");
    var data = httpclient.GetStringAsync("shorten?long_url=https://MehrAccounting.com").GetAwaiter().GetResult();
    Console.WriteLine(data);
};

try
{
    pipeline.Execute(act);
}
catch (Exception)
{

	throw;
}

