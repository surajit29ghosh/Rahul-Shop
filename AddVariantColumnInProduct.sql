select * from dbo.Products 
update dbo.Products set MultiVariantProduct = 0

CREATE TABLE ProductVariants
(
    VariantId UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
    ProductId UNIQUEIDENTIFIER NOT NULL,
    VariantType NVARCHAR(10) NOT NULL,
    VariantName NVARCHAR(50) NOT NULL,
    VariantPrice MONEY NOT NULL DEFAULT(0),
    VariantActive BIT NOT NULL DEFAULT(0)
)