namespace CodelabHospedagens.Domain.ClienteAggregate
{
    public class Telefone
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }

        public Telefone(string numero, string tipo)
        {
            Numero = numero;
            Tipo = tipo;
        }
    }
}
