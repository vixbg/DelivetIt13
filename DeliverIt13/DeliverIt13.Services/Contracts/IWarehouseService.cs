﻿using System.Collections.Generic;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Models;

namespace DeliverIt13.Services.Contracts
{
    public interface IWarehouseService
    {
        WarehouseGetDTO Get(int id);
        List<WarehousePublicDTO> GetAll();
        WarehouseCreateDTO Create(WarehouseCreateDTO warehouse);
        void Delete(int id);
        WarehouseUpdateDTO Update(WarehouseUpdateDTO warehouseDTO);


    }
}