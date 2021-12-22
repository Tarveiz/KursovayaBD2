USE Kursovaya
GO

--SELECT * FROM Преподаватели

--SELECT CASE [Преподаватели].ФИО WHEN 'Тест Тестов Тестович' THEN 'Преподаватель' ELSE ' ' END, U.*  
--FROM [Преподаватели], [Учет_Кафедра] as U WHERE [Преподаватели].ID_Преподавателя = [U].ID_Преподавателя

--CREATE OR ALTER VIEW [view]([Название_издательства],[Название_кафедры],[Издатели],[Номера_работ],[Дата_основания],[ID_Преподавателя]) AS SELECT CASE [Издательство].Название_издательства WHEN '7777' THEN 'Выбранные' ELSE ' ' END, U.*  
--FROM [Издательство], [Учет_Кафедра] as U WHERE [Издательство].Название_издательства = [U].Издатели
--SELECT * FROM [view]

--Коррелированный и не коррелированный WHERE
--SELECT Дата_основания FROM Учет_Кафедра AS T1 WHERE Дата_основания = (SELECT Год_основания FROM Издательство AS T2 WHERE T1.Издатели = T2.Название_издательства)
--SELECT Тема FROM Работы WHERE Номер_работы > 3

--Использование ANY
--SELECT Номер_работы FROM (SELECT * FROM Работы, Преподаватели WHERE ФИО = Автор) AS R1
--WHERE R1.Номер_Работы = ANY(SELECT ID_Преподавателя FROM Преподаватели)

--Коррелированный и некоррелированный FROM
--SELECT Номера_работ FROM (SELECT Учет_Кафедра.Номера_работ, Работы.Тема, Работы.Номер_Работы, Учет_Кафедра.ID_Преподавателя FROM Учет_Кафедра, Работы WHERE ID_Преподавателя = Номер_Работы) AS R1, Преподаватели AS R2
--WHERE R1.ID_Преподавателя = R2.ID_Преподавателя
--SELECT * FROM Издательство WHERE Название_издательства = '1C'

--Коррелированный и некоррелированный SELECT
--SELECT Название_издательства,(SELECT Издатели FROM Учет_Кафедра WHERE Z.Название_издательства = Издатели) AS A FROM Издательство AS Z
--SELECT Издатели FROM Учет_Кафедра WHERE ID_Преподавателя = '2'

--HAVING с агрегатными функциями, параметром, содержащий группировку записей в многотабличном запросе
--DECLARE @J NVARCHAR(30);
--SET @J = 100;
--SELECT DISTINCT COUNT(ID_Преподавателя), AVG(Номер_Работы) FROM Учет_Кафедра, Работы
--WHERE Номер_Работы > 0
--GROUP BY ID_Преподавателя, Номер_Работы
--HAVING Номер_Работы < @J


--CREATE PROCEDURE INSERT_Procedure1
--    @Тема NVARCHAR(100),
--	@Дата_публикации date,
--    @Автор NVARCHAR(50)
--AS
--BEGIN
--    INSERT INTO Работы(Тема, Дата_публикации, Автор) 
--        VALUES (@Тема, @Дата_публикации, @Автор);
--END;


--CREATE PROCEDURE update_Преподаватели (@ID_Преподавателя int, @ФИО nvarchar(50), @KONT_Data nvarchar(100), @Publication bit) 
--AS
--	BEGIN TRANSACTION
--	DECLARE @id_cur int;
--	DECLARE @id_curr int = @ID_Преподавателя;
--	DECLARE @ФИО_cur nvarchar(50) = @ФИО;
--	DECLARE @KONT_Data_cur nvarchar(100) = @KONT_Data;
--	DECLARE @Publication_cur bit = @Publication;
--	SAVE TRANSACTION sp1;
--	DECLARE cur CURSOR
--	FOR SELECT @ID_Преподавателя FROM Преподаватели;
--	OPEN cur
--	FETCH NEXT FROM cur INTO @id_cur;
--	WHILE @@FETCH_STATUS = 0
--		BEGIN
--		SAVE TRANSACTION sp2;
--		IF @ID_Преподавателя = @id_cur
--			UPDATE [Преподаватели] SET ФИО=@ФИО, Контактные_данные=@KONT_Data, Публикации=@Publication WHERE ID_Преподавателя=@ID_Преподавателя;   
--		FETCH NEXT FROM cur INTO @id_cur;
--		END
--	COMMIT TRANSACTION




--CREATE PROCEDURE Delete_Procedure2
--    @Deletesys NVARCHAR(50)
--AS
--BEGIN
--    DELETE FROM Работы WHERE Автор = @Deletesys
--END;

--CREATE PROCEDURE UPDATE_Procedure3
--    @Тема NVARCHAR(100),
--	@Дата_публикации date,
--    @Автор NVARCHAR(50)
--AS
--BEGIN
--UPDATE Работы SET Тема = @Тема, Дата_публикации = @Дата_публикации, Автор = @Автор WHERE Автор = @Автор
--END;





--CREATE TRIGGER Trig1 
--ON Издательство 
--AFTER INSERT, UPDATE
--AS
--UPDATE Издательство
--SET Издательство.Сборники = 'Неопредел.'
--WHERE (Издательство.Название_издательства = (SELECT inserted.Название_издательства FROM inserted) and Издательство.Сборники = '');





--CREATE FUNCTION NonColiWher(@Num INT)
--Returns nvarchar(50)
--BEGIN
--DECLARE @Тема nvarchar(50)
--	SELECT @Тема = Тема FROM Работы WHERE Номер_работы > @Num
--    RETURN @Тема
--END;

--CREATE FUNCTION [dbo].[vectorFunction](@Type_ID nvarchar(50))
--RETURNS TABLE
--AS
-- RETURN (SELECT ID_Преподавателя FROM Преподаватели WHERE Преподаватели.ФИО = @Type_ID)



--CREATE TABLE Преподаватели(
--	ID_Преподавателя INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	ФИО NVARCHAR(50) NOT NULL,
--	Контактные_данные NVARCHAR(100),
--	Публикации bit)

--CREATE TABLE Издательство(
--	Название_издательства NVARCHAR(80) NOT NULL PRIMARY KEY,
--	Сборники NVARCHAR(30) NOT NULL,
--	Реквизиты NVARCHAR(100) NOT NULL,
--	Год_основания date NOT NULL)

--CREATE TABLE Учет_Кафедра(
--	Название_кафедры NVARCHAR(10) NOT NULL PRIMARY KEY,
--	Издатели NVARCHAR(80) NOT NULL,
--	Номера_работ int NOT NULL,
--	Дата_основания date,
--	ID_Преподавателя INT NOT NULL FOREIGN KEY REFERENCES Преподаватели(ID_Преподавателя)
--	)
--CREATE TABLE Работы(
--	Номер_Работы INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	Тема NVARCHAR(100) NOT NULL,
--	Дата_публикации date NOT NULL,
--	Автор NVARCHAR(50))


--ALTER TABLE Учет_Кафедра
--ADD FOREIGN KEY(Издатели) REFERENCES Издательство(Название_Издательства)
--ADD FOREIGN KEY(Номера_работ) REFERENCES Работы(Номер_Работы)
--ADD FOREIGN KEY (ID_Преподавателя) REFERENCES Преподаватели(ID_Преподавателя)
--DROP COLUMN Год_основания 
--ADD Год_основания date NOT NULL