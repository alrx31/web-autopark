CREATE TABLE dbo.VehicleTypes(
    VehicleTypeId INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    TaxCoefficient FLOAT,
    PRIMARY KEY (VehicleTypeId)
);

CREATE TABLE dbo.Vehicle(
    VehicleId INT IDENTITY(1,1) NOT NULL,
    VehicleTypeId INT NOT NULL,
    Model VARCHAR(50) NOT NULL,
    RegistrationNumber VARCHAR(10) NOT NULL,
    Weight FLOAT NOT NULL,
    Year FLOAT NOT NULL,
    Mileage FLOAT NOT NULL,
    Color VARCHAR(10) NOT NULL,
    FuelConsumption FLOAT NOT NULL,
    PRIMARY KEY (VehicleId)
);

ALTER TABLE dbo.Vehicle ADD CONSTRAINT FK_vehicle_type FOREIGN KEY (VehicleTypeId) REFERENCES dbo.VehicleTypes(VehicleTypeId) ON DELETE CASCADE;

CREATE TABLE dbo.Orders(
    OrderId INT IDENTITY(1,1) NOT NULL,
    VehicleId INT NOT NULL,
    Date DATE NOT NULL 
    PRIMARY KEY (OrderId)
);

ALTER TABLE dbo.Orders ADD CONSTRAINT FK_vehicle_id FOREIGN KEY (VehicleId) REFERENCES dbo.Vehicle(VehicleId) ON DELETE CASCADE;

CREATE TABLE dbo.Components(
    ComponentId INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    PRIMARY KEY (ComponentId)
);

CREATE TABLE dbo.OrderItems(
    OrderItemId INT IDENTITY(1,1) NOT NULL,
    OrderId INT NOT NULL,
    ComponentId INT NOT NULL,
    Quantity FLOAT NOT NULL,
    PRIMARY KEY (OrderItemId)
);

ALTER TABLE dbo.OrderItems ADD CONSTRAINT FK_order_id FOREIGN KEY (OrderId) REFERENCES dbo.Orders (OrderId) ON DELETE CASCADE;
ALTER TABLE dbo.OrderItems ADD CONSTRAINT FK_component_id FOREIGN KEY (ComponentId) REFERENCES dbo.Components (ComponentId) ON DELETE CASCADE;