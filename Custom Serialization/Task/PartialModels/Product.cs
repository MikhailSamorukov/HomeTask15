namespace Task.DB
{
    using System.Runtime.Serialization;
    using System;
    using System.Linq;

    [Serializable]
    public partial class Product : ISerializable
    {
        public Product(SerializationInfo info, StreamingContext context)
        {
            ProductID = info.GetInt32("ProductID");
            ProductName = info.GetString("ProductName");
            SupplierID = info.GetInt32("SupplierID");
            CategoryID = info.GetInt32("CategoryID");
            QuantityPerUnit = info.GetString("QuantityPerUnit");
            UnitPrice = info.GetDecimal("UnitPrice");
            UnitsInStock = info.GetInt16("UnitsInStock");
            UnitsOnOrder = info.GetInt16("UnitsOnOrder");
            ReorderLevel = info.GetInt16("ReorderLevel");
            Discontinued = info.GetBoolean("Discontinued");
            Category = null;
            var order_DetailsArray = (Order_Detail[])info.GetValue("Order_Details", typeof(Order_Detail[]));
            Order_Details = order_DetailsArray.ToList();
            Supplier = (Supplier)info.GetValue("Supplier", typeof(Supplier));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProductID", ProductID);
            info.AddValue(nameof(Order_Details), Order_Details.ToArray());
            info.AddValue("ProductName", ProductName);
            info.AddValue("SupplierID", SupplierID);
            info.AddValue("CategoryID", CategoryID);
            info.AddValue("QuantityPerUnit", QuantityPerUnit);
            info.AddValue("UnitPrice", UnitPrice);
            info.AddValue("UnitsInStock", UnitsInStock);
            info.AddValue("UnitsOnOrder", UnitsOnOrder);
            info.AddValue("ReorderLevel", ReorderLevel);
            info.AddValue("Discontinued", Discontinued);
            info.AddValue(nameof(Supplier), Supplier);
        }
    }
}