using DeliverIt13.Services.Models.ShipmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Contracts
{
    public interface IShipmentService
    {
        ShipmentGetDTO Get(int id);
        List<ShipmentGetDTO> GetAll();
        ShipmentCreateDTO Create(ShipmentCreateDTO shipment);
        bool Delete(int id);
        ShipmentUpdateDTO Update(ShipmentUpdateDTO shipmentDTO);
        ShipmentPublicDTO GetNextToArrive(int warehouseId);
        ShipmentPublicDTO GetStatus(int id);
        List<ShipmentPublicDTO> GetAllFiltered(Expression<Func<Shipment, bool>> predicate);
        int GetOnTheWay(string cityName);
    }
}
