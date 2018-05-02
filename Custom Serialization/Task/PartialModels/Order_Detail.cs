namespace Task.DB
{
    using System.Runtime.Serialization;

    public partial class Order_Detail
    {
        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            Order = null;
            Product = null;
        }
    }
}