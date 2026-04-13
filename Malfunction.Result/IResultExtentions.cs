
namespace Malfunction.Result
{
    public static class IResultExtentions
    {
        public static IResult<TResult, TFail> Map<TPass, TFail, TResult>(this IResult<TPass, TFail> result, Func<TPass, TResult> mapFunc) => result switch
        {
            IResult<TPass, TFail>.Pass pass => new IResult<TResult, TFail>.Pass(mapFunc(pass.Value)),
            IResult<TPass, TFail>.Fail fail => new IResult<TResult, TFail>.Fail(fail.Value),
            _ => throw new Exception("Unexpected validation result")
        };

        public static IResult<TResult, TFail> Bind<TPass, TFail, TResult>(this IResult<TPass, TFail> result, Func<TPass, IResult<TResult, TFail>> bindFunc) => result switch
        {
            IResult<TPass, TFail>.Pass pass => bindFunc(pass.Value),
            IResult<TPass, TFail>.Fail fail => new IResult<TResult, TFail>.Fail(fail.Value),
            _ => throw new Exception("Unexpected result")
        };

        public static IResult<TPass, TResult> MapFail<TPass, TFail, TResult>(this IResult<TPass, TFail> result, Func<TFail, TResult> mapFunc) => result switch
        {
            IResult<TPass, TFail>.Pass pass => new IResult<TPass, TResult>.Pass(pass.Value),
            IResult<TPass, TFail>.Fail fail => new IResult<TPass, TResult>.Fail(mapFunc(fail.Value)),
            _ => throw new Exception("Unexpected validation result")
        };

        public static IResult<TPass, TResult> BindFail<TPass, TFail, TResult>(this IResult<TPass, TFail> result, Func<TFail, IResult<TPass, TResult>> bindFunc) => result switch
        {
            IResult<TPass, TFail>.Pass pass => new IResult<TPass, TResult>.Pass(pass.Value),
            IResult<TPass, TFail>.Fail fail => bindFunc(fail.Value),
            _ => throw new Exception("Unexpected validation result")
        };

        public static IResult<TPass, TAccumulate> AggregateFail<TPass, TFail, TAccumulate>(this IResult<TPass, List<TFail>> result, TAccumulate seed, Func<TAccumulate, TFail, TAccumulate> func) => AggregateFail(result, seed, func);
        public static IResult<TPass, TAccumulate> AggregateFail<TPass, TFail, TAccumulate>(this IResult<TPass, TFail[]> result, TAccumulate seed, Func<TAccumulate, TFail, TAccumulate> func) => AggregateFail(result, seed, func);
        public static IResult<TPass, TAccumulate> AggregateFail<TPass, TFail, TAccumulate>(this IResult<TPass, IEnumerable<TFail>> result, TAccumulate seed, Func<TAccumulate, TFail, TAccumulate> func) => MapFail(result, x => x.Aggregate(seed, func));
        
        public static IResult<TPass, TFail> AggregateFail<TPass, TFail>(this IResult<TPass, List<TFail>> result, Func<TFail, TFail, TFail> func) => AggregateFail(result, func);
        public static IResult<TPass, TFail> AggregateFail<TPass, TFail>(this IResult<TPass, TFail[]> result, Func<TFail, TFail, TFail> func) => AggregateFail(result, func);
        public static IResult<TPass, TFail> AggregateFail<TPass, TFail>(this IResult<TPass, IEnumerable<TFail>> result, Func<TFail, TFail, TFail> func) => MapFail(result, x => x.Aggregate(func));
      
    }
}
