Once the database and tables are crated, to create the EF6 model run this command through the package manager console, changing the connection string accordingly:

Scaffold-DbContext "Server=.\sqlexpress;Database=CarRental;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force