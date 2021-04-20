using DeliverIt13.Services.Models.ShipmentDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Contracts
{
    public interface IShipmentService
    {
        ShipmentPublicDTO GetStatus(int id);

        ShipmentDTO GetNextToArrive();

        List<ShipmentDTO> GetAll();
        int GetCount();

        ShipmentDTO Create(ShipmentDTO shipment);

        bool Delete(int id);

        ShipmentUpdateDTO Update(int id, ShipmentUpdateDTO shipmentDTO);
    }
}
