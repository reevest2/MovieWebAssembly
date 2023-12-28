USE MovieWebAssemblyAppDatabse;

--Insert Test Data
MERGE INTO [Core].[HotelRooms] AS target
USING (
    VALUES
        ('Room 101', 2, 150.00, 'Standard room with a double bed', '250 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        ('Room 202', 4, 250.00, 'Deluxe room with two queen beds and a balcony', '400 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        ('Room 303', 1, 100.00, 'Single room with a twin bed', '200 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        ('Room 404', 3, 200.00, 'Premium room with a king bed and a Jacuzzi', '500 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE())
) AS source(Name, Occupancy, RegularRate, Details, SqFt, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
ON (target.Name = source.Name AND target.Occupancy = source.Occupancy AND target.RegularRate = source.RegularRate)
WHEN NOT MATCHED THEN
    INSERT (Name, Occupancy, RegularRate, Details, SqFt, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
    VALUES (source.Name, source.Occupancy, source.RegularRate, source.Details, source.SqFt, source.CreatedBy, source.CreatedDate, source.UpdatedBy, source.UpdatedDate);
