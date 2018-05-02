using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
		Northwind _dbContext;

		[TestInitialize]
		public void Initialize()
		{
			_dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
			var categories = _dbContext.Categories
			                        .Include("Products.Order_Details")
			                        .Include("Products.Supplier")
			                        .ToList();

			var c = categories.First();

			tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
			var products = _dbContext.Products
			                        .Include("Order_Details")
			                        .Include("Supplier")
			                        .ToList();

			tester.SerializeAndDeserialize(products);
		}


		[TestMethod]
		public void ISerializationSurrogate()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
			var orderDetails = _dbContext.Order_Details
			                            .Include(nameof(Order_Detail.Order))
			                            .Include(nameof(Order_Detail.Product))
                                        .ToList();

			tester.SerializeAndDeserialize(orderDetails);
		}

		[TestMethod]
		public void IDataContractSurrogate()
		{
		    _dbContext.Configuration.ProxyCreationEnabled = true;
		    _dbContext.Configuration.LazyLoadingEnabled = true;
		    var settings = new DataContractSerializerSettings
		    {
		        DataContractSurrogate = new OrdersContractSurrogate()
		    };
		    var serializer = new DataContractSerializer(typeof(IEnumerable<Order>), settings);
		    var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
		    var orders = _dbContext.Orders.ToList();
		    var result = tester.SerializeAndDeserialize(orders);
        }
	}
}
