
namespace Malfunction.Result
{
    internal static class IResultCollectionExtentions
    {
        public static IResult<TPass, TFail> Aggregate<TPass, TFail, TCollection>(this IResult<TCollection, TFail> result, Func<TPass, TPass, TPass> func) where TCollection : IEnumerable<TPass> => 
            result.Map(x => x.Aggregate(func));
        public static IResult<TAccumulate, TFail> Aggregate<TPass, TFail, TAccumulate, TCollection>(this IResult<TCollection, TFail> result, TAccumulate seed, Func<TAccumulate, TPass, TAccumulate> func) where TCollection : IEnumerable<TPass> => 
            result.Map(x => x.Aggregate(seed, func));
        public static IResult<TPass, TAccumulate> AggregateFail<TPass, TFail, TAccumulate, TCollection>(this IResult<TPass, TCollection> result, TAccumulate seed, Func<TAccumulate, TFail, TAccumulate> func) where TCollection : IEnumerable<TFail> => 
            result.MapFail(x => x.Aggregate(seed, func));
        public static IResult<TPass, TFail> AggregateFail<TPass, TFail, TCollection>(this IResult<TPass, TCollection> result, Func<TFail, TFail, TFail> func) where TCollection : IEnumerable<TFail> => 
            result.MapFail(x => x.Aggregate(func));
    }
}
