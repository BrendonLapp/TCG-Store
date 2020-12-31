using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

namespace TCG_Store.Controllers
{
    [Route("api/v1/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public List<Order> Get ()
        {
            List<Order> AllOrders = new List<Order>();
            List<OrderDTO> AllOrderDTOs = new List<OrderDTO>();
            OrderDataController OrderDataController = new OrderDataController();

            AllOrderDTOs = OrderDataController.GetAllOrders();

            foreach (var Order in AllOrderDTOs)
            {
                Order IncomingOrder = new Order
                {
                    OrderID = Order.OrderID,
                    AdminID = Order.AdminID,
                    CustomerID = Order.CustomerID,
                    ShippingAddress = Order.ShippingAddress,
                    ShippingPrice = Order.ShippingPrice,
                    ShippingID = Order.ShippingID,
                    StatusID = Order.StatusID,
                    PostalCode = Order.PostalCode,
                    City = Order.City,
                    Province = Order.Province,
                    SaleDate = Order.SaleDate,
                    UpdatedBy = Order.UpdatedBy,
                    PST = Order.PST,
                    HST = Order.HST,
                    GST = Order.GST,
                    SubTotal = Order.SubTotal,
                    Total = Order.Total
                };
                AllOrders.Add(IncomingOrder);
            }
            return AllOrders;
        }

        [Route("/CustomerID={CustomerID}")]
        [HttpGet]
        public Order Get (int CustomerID)
        {
            throw new NotImplementedException();
        }

        [Route("/CustomerID={CustomerID}/AllOrders")]
        [HttpGet]
        public List<Order> GetByCustomerOrders (int CustomerID)
        {
            List<Order> AllOrders = new List<Order>();
            List<OrderDTO> AllOrderDTOs = new List<OrderDTO>();
            OrderDataController OrderDataController = new OrderDataController();

            AllOrderDTOs = OrderDataController.GetAllOrdersByCustomerID(CustomerID);

            foreach (var Order in AllOrderDTOs)
            {
                Order IncomingOrder = new Order
                {
                    OrderID = Order.OrderID,
                    AdminID = Order.AdminID,
                    CustomerID = Order.CustomerID,
                    ShippingAddress = Order.ShippingAddress,
                    ShippingPrice = Order.ShippingPrice,
                    ShippingID = Order.ShippingID,
                    StatusID = Order.StatusID,
                    PostalCode = Order.PostalCode,
                    City = Order.City,
                    Province = Order.Province,
                    SaleDate = Order.SaleDate,
                    UpdatedBy = Order.UpdatedBy,
                    PST = Order.PST,
                    HST = Order.HST,
                    GST = Order.GST,
                    SubTotal = Order.SubTotal,
                    Total = Order.Total
                };
                AllOrders.Add(IncomingOrder);
            }
            return AllOrders;
        }

        [Route("/OrderStatus={OrderStatus}")]
        [HttpGet]
        public List<Order> Get (string OrderStatus)
        {
            List<Order> AllOrders = new List<Order>();
            List<OrderDTO> AllOrderDTOs = new List<OrderDTO>();
            OrderDataController OrderDataController = new OrderDataController();

            AllOrderDTOs = OrderDataController.GetAllOrdersByOrderStatus(OrderStatus);

            foreach (var Order in AllOrderDTOs)
            {
                Order IncomingOrder = new Order
                {
                    OrderID = Order.OrderID,
                    AdminID = Order.AdminID,
                    CustomerID = Order.CustomerID,
                    ShippingAddress = Order.ShippingAddress,
                    ShippingPrice = Order.ShippingPrice,
                    ShippingID = Order.ShippingID,
                    StatusID = Order.StatusID,
                    PostalCode = Order.PostalCode,
                    City = Order.City,
                    Province = Order.Province,
                    SaleDate = Order.SaleDate,
                    UpdatedBy = Order.UpdatedBy,
                    PST = Order.PST,
                    HST = Order.HST,
                    GST = Order.GST,
                    SubTotal = Order.SubTotal,
                    Total = Order.Total
                };
                AllOrders.Add(IncomingOrder);
            }
            return AllOrders;
        }

        [HttpPost]
        public bool Post ([FromBody] Order NewOrder)
        {
            bool Success;
            int OrderID;
            decimal GST;
            decimal HST;
            decimal PST;
            decimal SubTotal;
            decimal Total;

            OrderDataController OrderDataController = new OrderDataController();
            OrderItemDataController OrderItemsDataController = new OrderItemDataController();
            List<OrderItemDTO> OrderItems = new List<OrderItemDTO>();

            foreach (var OrderItem in NewOrder.OrderItems)
            {

            }

            OrderDTO NewOrderDTO = new OrderDTO
            {
                CustomerID = NewOrder.CustomerID,
                StatusID = NewOrder.StatusID,
                ShippingID = NewOrder.ShippingID,
                ShippingAddress = NewOrder.ShippingAddress,
                ShippingPrice = NewOrder.ShippingPrice,
                SaleDate = DateTime.Now,
                City = NewOrder.City,
                PostalCode = NewOrder.PostalCode,
                Province = NewOrder.Province,
                PST = NewOrder.PST,
                HST = NewOrder.HST,
                GST = NewOrder.GST,
                SubTotal = NewOrder.SubTotal,
                Total = NewOrder.Total
            };

            OrderID = OrderDataController.AddNewOrder(NewOrderDTO);

            foreach (var OrderItem in NewOrder.OrderItems)
            {
                OrderItemDTO NewOrderItem = new OrderItemDTO
                {
                    OrderID = OrderID,
                    OrderItemPrice = OrderItem.OrderItemPrice,
                    OrderQuantity = OrderItem.OrderQuantity,
                    InventoryID = OrderItem.InventoryID
                };
                OrderItems.Add(NewOrderItem);   
            }

            Success = true;
            return Success;
        }

        [HttpPut]
        public bool Put ([FromBody] Order UpdatedOrder)
        {
            throw new NotImplementedException();
        }
    }
}
