namespace discount.Domain.Interface;

public interface IEventHandler<in T>
{
    public void Handle(T request);
}

public interface IEventHandler<in T,out O>
{
    public O Handle(T request);
}
