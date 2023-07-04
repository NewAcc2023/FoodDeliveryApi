using Dapper;
using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using System.Data;

namespace FoodDeliveryApi.Repository
{
    public class FoodOrderRepository : IFoodOrderRepository
    {
        private readonly DapperContext _context;
        public FoodOrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<string> CreateFoodOrder(int customerId, int deliveryAddressId, int driverId, int orderStatusId, int restaurantId, decimal deliveryFee, int totalAmount, DateTime orderDate, DateTime deliveryDate)
        {
            var query = $"INSERT INTO food_orders VALUES (@CustomerId,@DeliveryAddressId,@DriverId, @OrderStatusId,@RestaurantId,@DeliveryFee,@TotalAmount, @OrderDate, @DeliveryDate)" +
            " select @@ROWCOUNT";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", customerId, DbType.Int32);
            parameters.Add("DeliveryAddressId", deliveryAddressId, DbType.Int32);
            parameters.Add("DriverId", driverId, DbType.Int32);
            parameters.Add("OrderStatusId", orderStatusId, DbType.Int32);
            parameters.Add("RestaurantId", restaurantId, DbType.Int32);
            parameters.Add("DeliveryFee", deliveryFee, DbType.Decimal);
            parameters.Add("TotalAmount", totalAmount, DbType.Int32);
            parameters.Add("OrderDate", orderDate, DbType.DateTime);
            parameters.Add("DeliveryDate", deliveryDate, DbType.DateTime);

            int affectedRows = 0;

            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }
            if (affectedRows > 0)
            {
                return "Food Order Created!";
            }
            return "Food Order was not Created!";

        }


        public async Task<string> SetDriverFoodOrder(int orderId, int driverId)
        {
            var query = $"UPDATE food_orders SET driver_id = @DriverId WHERE id = @OrderId; select @@ROWCOUNT";

            int affectedRows = 0;
            var parameters = new DynamicParameters();
            parameters.Add("OrderId", orderId, DbType.Int32);
            parameters.Add("DriverId", driverId, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }
            if (affectedRows > 0)
            {
                return "Driver was Changed!";
            }
            return "Driver was not Changed!";
        }

        public async Task<string> CancelFoodOrder(int orderId)
        {
            var query = $"UPDATE food_orders SET order_status_id = @CancelledCode WHERE id = @OrderId; select @@ROWCOUNT";

            int affectedRows = 0;
            int cancelledCode = 0;
            using (var connection = _context.CreateConnection())
            {
                cancelledCode = await connection.QueryFirstAsync<int>("SELECT id FROM order_statuses WHERE order_status_name = 'Cancelled'");
            }
            var parameters = new DynamicParameters();
            parameters.Add("OrderId", orderId, DbType.Int32);
            parameters.Add("CancelledCode",cancelledCode, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }
            if (affectedRows > 0)
            {
                return "Order was Cancelled!";
            }
            return "Order was not Cancelled!";
        }
    }
}
