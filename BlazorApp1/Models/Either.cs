namespace BlazorApp1.Models;

public class Either<T>
{
    private readonly T? _value;
    private readonly bool _isRight;
    private Either(T? value, bool isRight = false)
    {
        _value = value;
        _isRight = isRight;
    }
    public static Either<T> Right(T value) => new(value, true);
    public static Either<T> Left(T value) => new(value, false);
    public Either<Maybe<TResult?>> Bind<TResult>(Func<T?, Either<Maybe<TResult?>>> func)
        => _isRight ? func(_value) : Either<Maybe<TResult?>>.Left(Maybe<TResult?>.Some(default));
    public Either<Maybe<TResult?>> Map<TResult>(Func<T?, Maybe<TResult?>> func)
        => _isRight ? Either<Maybe<TResult?>>.Right(func(_value)) : Either<Maybe<TResult?>>.Left(Maybe<TResult?>.Some(default));
    public T? GetValueOrDefault(T? defaultValue)
        => _isRight ? _value : defaultValue;
    public bool TryGetValue(out T? value)
    {
        value = _value;
        return _isRight;
    }
}
