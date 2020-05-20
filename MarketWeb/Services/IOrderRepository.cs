using Core.Models;
using System.Threading.Tasks;

namespace MarketWeb.Services
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
    }
}