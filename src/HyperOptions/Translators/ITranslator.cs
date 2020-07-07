namespace HyperOptions.Translators
{
    public interface ITranslator<out TTarget>
    {
        TTarget Translate(string value);
    }
}
