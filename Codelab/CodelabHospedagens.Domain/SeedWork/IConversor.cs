namespace CodelabHospedagens.Domain.SeedWork
{
    public interface IConversor<TTipo, TOutroTipo>
    {
        TOutroTipo Converter(TTipo origem);
        TTipo Converter(TOutroTipo origem);
        IEnumerable<TOutroTipo> ConverterLista(IEnumerable<TTipo> origem);
    }
}
