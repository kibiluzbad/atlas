namespace Atlas.UI.Domain
{
    public class Telefone
    {
        public virtual string Numero { get; private set; }
        public virtual Operadora Operadora { get; private set; }

        public Telefone(string numero, Operadora operadora)
        {
            Numero = numero;
            Operadora = operadora;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Telefone) obj);
        }

        protected bool Equals(Telefone other)
        {
            return string.Equals(Numero, other.Numero) && Operadora.Equals(other.Operadora);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Numero != null ? Numero.GetHashCode() : 0) * 397) ^ Operadora.GetHashCode();
            }
        }
    }
}