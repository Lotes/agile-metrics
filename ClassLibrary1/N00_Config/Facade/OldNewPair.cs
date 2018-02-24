namespace ClassLibrary1.N00_Config.Facade
{
    public class OldNewPair<T>
    {
        public OldNewPair(T old, T @new)
        {
            Old = old;
            New = @new;
        }

        public T Old { get; private set; }
        public T New { get; private set; }
    }
}