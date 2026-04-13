
namespace Malfunction.Result
{
    public static class IEnumerableExtentions
    {
        public static IResult<List<TPass>, List<TFail>> Traverse<T, TPass, TFail>(this List<T> enumerable, Func<T, IResult<TPass, TFail>> resultFunc) => Traverse(enumerable, resultFunc);
        public static IResult<TPass[], TFail[]> Traverse<T, TPass, TFail>(this T[] enumerable, Func<T, IResult<TPass, TFail>> resultFunc) => Traverse(enumerable, resultFunc);
        public static IResult<IEnumerable<TPass>, IEnumerable<TFail>> Traverse<T, TPass, TFail>(this IEnumerable<T> enumerable, Func<T, IResult<TPass, TFail>> resultFunc)
        {
            return enumerable.Aggregate(new IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Pass([]) as IResult<IEnumerable<TPass>, IEnumerable<TFail>>, (current, next) => (current, resultFunc(next)) switch
            {
                (IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Pass pass, IResult<TPass, TFail>.Pass newPass) =>
                    new IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Pass(pass.Value.Append(newPass.Value)),

                (IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Fail fail, IResult<TPass, TFail>.Pass) =>
                    fail,

                (IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Pass pass, IResult<TPass, TFail>.Fail newFail) =>
                    new IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Fail([newFail.Value]),

                (IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Fail fail, IResult<TPass, TFail>.Fail newFail) =>
                    new IResult<IEnumerable<TPass>, IEnumerable<TFail>>.Fail([.. fail.Value, newFail.Value]),
            });
        }

     }
}
