using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.TestHelpers
{
    public class OrdersContractSurrogate : IDataContractSurrogate
    {
        private int _serializationTypesNestingLevel;

        public Type GetDataContractType(Type type)
        {
            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (_serializationTypesNestingLevel++ > 0)
            {
                return obj;
            }

            var proxyOrders = (IEnumerable<Order>)obj;

            return proxyOrders.Select(UnproxyOrder).ToList();
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            _serializationTypesNestingLevel = 0;
            return obj;
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            return typeDeclaration;
        }

        private Order UnproxyOrder(Order proxyOrder)
        {
            var order = new Order();
            PropertyCloner.CopyProperties(proxyOrder, order);
            order.Order_Details = UnproxyOrder_Details(order.Order_Details);
            order.Customer = UnproxyCustomer(order.Customer);
            order.Employee = UnproxyEmployee(order.Employee);
            order.Shipper = UnproxyShipper(order.Shipper);
            return order;
        }

        private ICollection<Order_Detail> UnproxyOrder_Details(IEnumerable<Order_Detail> proxyOrderDetails)
        {
            var orderDetails = new List<Order_Detail>();
            foreach (var proxyorderDetail in proxyOrderDetails)
            {
                var orderDetail = new Order_Detail();
                PropertyCloner.CopyProperties(proxyorderDetail, orderDetail);
                orderDetail.Order = null;
                orderDetail.Product = UnproxyProduct(orderDetail.Product);
                orderDetails.Add(orderDetail);
            }

            return orderDetails;
        }

        private Product UnproxyProduct(Product proxyProduct)
        {
            var product = new Product();
            PropertyCloner.CopyProperties(proxyProduct, product);
            product.Order_Details = new List<Order_Detail>();
            product.Category = null;
            product.Supplier = null;
            return product;
        }

        private Shipper UnproxyShipper(Shipper proxyShipper)
        {
            var shipper = new Shipper();
            PropertyCloner.CopyProperties(proxyShipper, shipper);
            shipper.Orders = new List<Order>();
            return shipper;
        }

        private Employee UnproxyEmployee(Employee proxyEmployee)
        {
            var employee = new Employee();
            PropertyCloner.CopyProperties(proxyEmployee, employee);
            employee.Orders = new List<Order>();
            employee.Employee1 = null;
            employee.Employees1 = new List<Employee>();
            employee.Territories = new List<Territory>();
            return employee;
        }

        private Customer UnproxyCustomer(Customer proxyCustomer)
        {
            var customer = new Customer();
            PropertyCloner.CopyProperties(proxyCustomer, customer);
            customer.Orders = new List<Order>();
            customer.CustomerDemographics = UnproxyCustomerDemographics(customer.CustomerDemographics);
            return customer;
        }

        private ICollection<CustomerDemographic> UnproxyCustomerDemographics(
        IEnumerable<CustomerDemographic> proxyCustomerDemographics)
        {
            var customerDemographics = new List<CustomerDemographic>();
            foreach (var proxyCustomerDemographic in proxyCustomerDemographics)
            {
                var customerDemographic = new CustomerDemographic();
                PropertyCloner.CopyProperties(proxyCustomerDemographic, customerDemographic);
                customerDemographic.Customers = new List<Customer>();
                customerDemographics.Add(customerDemographic);
            }

            return customerDemographics;
        }
    }
}