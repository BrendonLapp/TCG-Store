CREATE PROCEDURE InsertIntoQuality
(
	@QualityPercentage DECIMAL(5,2) NULL,
	@QualityName VARCHAR(25) NULL,
	@QualityShortName VARCHAR(2) NULL
)
AS