USE Kursovaya
GO

--SELECT * FROM �������������

--SELECT CASE [�������������].��� WHEN '���� ������ ��������' THEN '�������������' ELSE ' ' END, U.*  
--FROM [�������������], [����_�������] as U WHERE [�������������].ID_������������� = [U].ID_�������������

--CREATE OR ALTER VIEW [view]([��������_������������],[��������_�������],[��������],[������_�����],[����_���������],[ID_�������������]) AS SELECT CASE [������������].��������_������������ WHEN '7777' THEN '���������' ELSE ' ' END, U.*  
--FROM [������������], [����_�������] as U WHERE [������������].��������_������������ = [U].��������
--SELECT * FROM [view]

--��������������� � �� ��������������� WHERE
--SELECT ����_��������� FROM ����_������� AS T1 WHERE ����_��������� = (SELECT ���_��������� FROM ������������ AS T2 WHERE T1.�������� = T2.��������_������������)
--SELECT ���� FROM ������ WHERE �����_������ > 3

--������������� ANY
--SELECT �����_������ FROM (SELECT * FROM ������, ������������� WHERE ��� = �����) AS R1
--WHERE R1.�����_������ = ANY(SELECT ID_������������� FROM �������������)

--��������������� � ����������������� FROM
--SELECT ������_����� FROM (SELECT ����_�������.������_�����, ������.����, ������.�����_������, ����_�������.ID_������������� FROM ����_�������, ������ WHERE ID_������������� = �����_������) AS R1, ������������� AS R2
--WHERE R1.ID_������������� = R2.ID_�������������
--SELECT * FROM ������������ WHERE ��������_������������ = '1C'

--��������������� � ����������������� SELECT
--SELECT ��������_������������,(SELECT �������� FROM ����_������� WHERE Z.��������_������������ = ��������) AS A FROM ������������ AS Z
--SELECT �������� FROM ����_������� WHERE ID_������������� = '2'

--HAVING � ����������� ���������, ����������, ���������� ����������� ������� � �������������� �������
--DECLARE @J NVARCHAR(30);
--SET @J = 100;
--SELECT DISTINCT COUNT(ID_�������������), AVG(�����_������) FROM ����_�������, ������
--WHERE �����_������ > 0
--GROUP BY ID_�������������, �����_������
--HAVING �����_������ < @J


--CREATE PROCEDURE INSERT_Procedure1
--    @���� NVARCHAR(100),
--	@����_���������� date,
--    @����� NVARCHAR(50)
--AS
--BEGIN
--    INSERT INTO ������(����, ����_����������, �����) 
--        VALUES (@����, @����_����������, @�����);
--END;


--CREATE PROCEDURE update_������������� (@ID_������������� int, @��� nvarchar(50), @KONT_Data nvarchar(100), @Publication bit) 
--AS
--	BEGIN TRANSACTION
--	DECLARE @id_cur int;
--	DECLARE @id_curr int = @ID_�������������;
--	DECLARE @���_cur nvarchar(50) = @���;
--	DECLARE @KONT_Data_cur nvarchar(100) = @KONT_Data;
--	DECLARE @Publication_cur bit = @Publication;
--	SAVE TRANSACTION sp1;
--	DECLARE cur CURSOR
--	FOR SELECT @ID_������������� FROM �������������;
--	OPEN cur
--	FETCH NEXT FROM cur INTO @id_cur;
--	WHILE @@FETCH_STATUS = 0
--		BEGIN
--		SAVE TRANSACTION sp2;
--		IF @ID_������������� = @id_cur
--			UPDATE [�������������] SET ���=@���, ����������_������=@KONT_Data, ����������=@Publication WHERE ID_�������������=@ID_�������������;   
--		FETCH NEXT FROM cur INTO @id_cur;
--		END
--	COMMIT TRANSACTION




--CREATE PROCEDURE Delete_Procedure2
--    @Deletesys NVARCHAR(50)
--AS
--BEGIN
--    DELETE FROM ������ WHERE ����� = @Deletesys
--END;

--CREATE PROCEDURE UPDATE_Procedure3
--    @���� NVARCHAR(100),
--	@����_���������� date,
--    @����� NVARCHAR(50)
--AS
--BEGIN
--UPDATE ������ SET ���� = @����, ����_���������� = @����_����������, ����� = @����� WHERE ����� = @�����
--END;





--CREATE TRIGGER Trig1 
--ON ������������ 
--AFTER INSERT, UPDATE
--AS
--UPDATE ������������
--SET ������������.�������� = '���������.'
--WHERE (������������.��������_������������ = (SELECT inserted.��������_������������ FROM inserted) and ������������.�������� = '');





--CREATE FUNCTION NonColiWher(@Num INT)
--Returns nvarchar(50)
--BEGIN
--DECLARE @���� nvarchar(50)
--	SELECT @���� = ���� FROM ������ WHERE �����_������ > @Num
--    RETURN @����
--END;

--CREATE FUNCTION [dbo].[vectorFunction](@Type_ID nvarchar(50))
--RETURNS TABLE
--AS
-- RETURN (SELECT ID_������������� FROM ������������� WHERE �������������.��� = @Type_ID)



--CREATE TABLE �������������(
--	ID_������������� INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	��� NVARCHAR(50) NOT NULL,
--	����������_������ NVARCHAR(100),
--	���������� bit)

--CREATE TABLE ������������(
--	��������_������������ NVARCHAR(80) NOT NULL PRIMARY KEY,
--	�������� NVARCHAR(30) NOT NULL,
--	��������� NVARCHAR(100) NOT NULL,
--	���_��������� date NOT NULL)

--CREATE TABLE ����_�������(
--	��������_������� NVARCHAR(10) NOT NULL PRIMARY KEY,
--	�������� NVARCHAR(80) NOT NULL,
--	������_����� int NOT NULL,
--	����_��������� date,
--	ID_������������� INT NOT NULL FOREIGN KEY REFERENCES �������������(ID_�������������)
--	)
--CREATE TABLE ������(
--	�����_������ INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	���� NVARCHAR(100) NOT NULL,
--	����_���������� date NOT NULL,
--	����� NVARCHAR(50))


--ALTER TABLE ����_�������
--ADD FOREIGN KEY(��������) REFERENCES ������������(��������_������������)
--ADD FOREIGN KEY(������_�����) REFERENCES ������(�����_������)
--ADD FOREIGN KEY (ID_�������������) REFERENCES �������������(ID_�������������)
--DROP COLUMN ���_��������� 
--ADD ���_��������� date NOT NULL