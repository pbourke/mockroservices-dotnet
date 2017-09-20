namespace Common
{
    public class MonetaryValue
    {
        public decimal Amount { get; }

        public MonetaryValue(decimal amount)
        {
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            var otherMoney = obj as MonetaryValue;
            if (otherMoney == null)
            {
                return false;
            }

            return this.Amount.Equals(otherMoney.Amount);
        }
    }
}