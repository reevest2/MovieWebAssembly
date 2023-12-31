
SET Identity_Insert [Core].[HotelRooms] ON;
MERGE INTO [Core].[HotelRooms] AS target
USING (
    VALUES
        (1,'Room 101', 2, 150.00, 'Standard room with a double bed', '250 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        (2,'Room 202', 4, 250.00, 'Deluxe room with two queen beds and a balcony', '400 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        (3,'Room 303', 1, 100.00, 'Single room with a twin bed', '200 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        (4,'Room 404', 3, 200.00, 'Premium room with a king bed and a Jacuzzi', '500 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE())
) AS source (Id, Name, Occupancy, RegularRate, Details, SqFt, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
ON target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (Id, Name, Occupancy, RegularRate, Details, SqFt, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
    VALUES (source.Id, source.Name, source.Occupancy, source.RegularRate, source.Details, source.SqFt, source.CreatedBy, source.CreatedDate, source.UpdatedBy, source.UpdatedDate);
SET Identity_Insert [Core].[HotelRooms] OFF;
GO


