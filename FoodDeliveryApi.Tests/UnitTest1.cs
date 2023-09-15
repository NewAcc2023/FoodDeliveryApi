using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Controllers;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Repository;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Moq;
using System.Reflection.Metadata;

namespace FoodDeliveryApi.Tests
{
	public class CustomerTests
	{
		private  CustomersController _CustomerController;
		private  CustomerRepository _repositoryMock;
		private DapperContext _dc;

		private  IConfiguration _configuration;
		private readonly string _connectionString = "server=DESKTOP-DGU940A\\SQLEXPRESS; database=FoodDelivery; Integrated Security=true; Encrypt=false;";
		[SetUp]
		public void Setup()
		{
			
			_dc = new DapperContext();
			_repositoryMock = new CustomerRepository(_dc);
			_CustomerController = new CustomersController(_repositoryMock);

		}

		[Test]
		public async Task GetCustomerTest()
		{

			//_repositoryMock.Setup(repo => repo.GetCustomers()).ReturnsAsync(new List<Customer>());

			// Act
			var result = await _CustomerController.GetCustomers();

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}
	}
}