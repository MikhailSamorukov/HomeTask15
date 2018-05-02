namespace Task.TestHelpers
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using DB;

    public class OrderDetailSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail)obj;
            var order = new Order();
            PropertyCloner.CopyProperties(orderDetail.Order, order);
            order.Order_Details = new List<Order_Detail>();
            order.Customer = null;
            order.Employee = null;
            order.Shipper = null;
            var product = new Product();
            PropertyCloner.CopyProperties(orderDetail.Product, product);
            product.Order_Details = new List<Order_Detail>();
            product.Category = null;
            product.Supplier = null;
            info.AddValue(nameof(Order_Detail.Discount), orderDetail.Discount);
            info.AddValue(nameof(Order_Detail.Order), order);
            info.AddValue(nameof(Order_Detail.OrderID), orderDetail.OrderID);
            info.AddValue(nameof(Order_Detail.Product), product);
            info.AddValue(nameof(Order_Detail.ProductID), orderDetail.ProductID);
            info.AddValue(nameof(Order_Detail.Quantity), orderDetail.Quantity);
            info.AddValue(nameof(Order_Detail.UnitPrice), orderDetail.UnitPrice);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail)obj;
            orderDetail.Discount = info.GetSingle("Discount");
            orderDetail.Order = (Order)info.GetValue("Order", typeof(Order));
            orderDetail.OrderID = info.GetInt32("OrderID");
            orderDetail.Product = (Product)info.GetValue("Product", typeof(Product));
            orderDetail.ProductID = info.GetInt32("ProductID");
            orderDetail.Quantity = info.GetInt16("Quantity");
            orderDetail.UnitPrice = info.GetDecimal("UnitPrice");
            return orderDetail;
        }
    }
}