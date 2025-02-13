namespace BlazorApp1.Models;

/// <summary>
/// Neiter is wrong monad
/// </summary>
/// <typeparam name="T"></typeparam>
public class Neither<T>
{
    public T? _value { get; set; }
    private bool _isWrong;
    private Neither(T? value, bool isWrong = false)
    {
        _value = value;
        _isWrong = isWrong;
    }
    public static Neither<T> Wrong(T value) => new(value, true);
    public static Neither<T> Right(T value) => new(value, false);
    public Neither<Maybe<TResult?>> Bind<TResult>(Func<T?, Neither<Maybe<TResult?>>> func)
    => _isWrong ? func(_value) : Neither<Maybe<TResult?>>.Right(Maybe<TResult?>.Some(default));
    public Neither<Either<TResult?>> Bind<TResult>(Func<T?, Neither<Either<TResult?>>> func)
        => _isWrong ? func(_value) : Neither<Either<TResult?>>.Right(Either<TResult?>.Right(default));
    public Neither<Maybe<TResult?>> Map<TResult>(Func<T?, Maybe<TResult?>> func)
        => _isWrong ? Neither<Maybe<TResult?>>.Wrong(func(_value)) : Neither<Maybe<TResult?>>.Right(Maybe<TResult?>.Some(default));
    public Neither<Either<TResult?>> Map<TResult>(Func<T?, Either<TResult?>> func)
    => _isWrong ? Neither<Either<TResult?>>.Wrong(func(_value)) : Neither<Either<TResult?>>.Right(Either<TResult?>.Right(default));

    public T? GetValueOrDefault(T? defaultValue)
        => _isWrong ? _value : defaultValue;
    public bool TryGetValue(out T? value)
    {
        value = _value;
        return _isWrong;
    }
}