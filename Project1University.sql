create database University;
use University;

-- STUDENT TABLE IS BEING CREATED
CREATE TABLE [Student]([StudentId] int NOT NULL Identity(1, 1), [fName] varchar(40), [lName] varchar(40), [Email] varchar(40), [Pword] varchar(40),
			 CONSTRAINT [PK_Student_StudentId] PRIMARY KEY CLUSTERED(StudentId));

-- COURSES TABLE IS BEING CREATED
CREATE TABLE [Courses]([CourseId] int NOT NULL Identity(1, 1), [CourseName] varchar(40), [StartTime] Time, [CreditHours] Time);

ALTER TABLE [Courses] ADD CONSTRAINT [PK_Courses_CourseId] PRIMARY KEY CLUSTERED([CourseId])
		
-- Creating Junction Table between Students and Courses 
CREATE TABLE [CourseRoster]([StudentId] int NOT NULL, [CourseId] int NOT NULL CONSTRAINT [PK_StudentCourse] PRIMARY KEY
([StudentId], [CourseId]), FOREIGN KEY ([StudentId]) REFERENCES [Student] ([StudentId]), FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]));



--SELECT STATNEMENTS FREQUENTLY USED
SELECT * FROM Student;
SELECT * FROM Courses;
SELECT * FROM CourseRoster;

-- Student Tests Insert
INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Adam', 'Roth', 'AR@gmail.com', 'aroththeman');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Jessie', 'Owen', 'JOwen@gmail.com', 'jessiejess');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Shawn', 'Tanner', 'STanz@gmail.com', 'getitshawn');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Isaiah', 'Dognler', 'IsaDogn@gmail.com', 'Isnotadogn');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Leslie', 'Sandston', 'WindySandy@gmail.com', 'theshewitch');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Samantha', 'Kiara', 'Msbestgirl@gmail.com', 'longesthair');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Fred', 'Dustwind', 'Mywholefred@gmail.com', 'burgers');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Tiara', 'Johnson', 'TiaraTs@gmail.com', 'freepony');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Maury', 'Summers', 'EnglishGuysplz@gmail.com', 'accentz');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Marissa', 'Reeves', 'Mskeanu@gmail.com', 'outthematrix');

INSERT INTO Student([fName], [lName], [Email], [Pword])
		VALUES('Thomas', 'Shepard', 'WalkharderTom@gmail.com', 'youngmanoldman');

-- Math Insert into Major and Courses Table
INSERT INTO Major([MajorName])
			VALUES('Math');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Calculus 1', 1, '10:00', '02:00');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Trigonometry', 1, '12:00', '01:00');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Calculus 2', 1, '14:00', '02:00');

-- Computer Science Insert into Major and Courses Table
INSERT INTO Major([MajorName])
			VALUES('Computer Science');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('C# Programming', 2, '11:00', '02:00');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Cyber Security', 2, '16:00','01:00');
 
INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Computer Architecture', 2, '17:00', '01:00');

-- Biology Insert into Major and Tables
INSERT INTO Major([MajorName])
			VALUES('Biology');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Zoology', 3, '18:00', '01:00');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Cell Analysis', 3, '08:00', '02:00');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Genetics', 3, '10:00', '01:00');

-- Psychology insert into Major and Tables
INSERT INTO Major([MajorName])
			VALUES('Psychology');

INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Intro to Psychology', 4, '03:00', '01:00');
			
INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('History of Psychology', 4, '19:00', '01:00');
			
INSERT INTO Courses([CourseName], [MajorId], [StartTime], [CreditHours])
			VALUES('Makeup of the Brain', 4, '12:00', '02:00');

-- Test to Insert all Students in Calculus 1
INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(1, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(2, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(3, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(4, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(5, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(6, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(7, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(8, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(9, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(10, 7);

INSERT INTO CourseRoster([StudentId], [CourseId])
		VALUES(11, 7);

-- Function that takes a CourseId as a parameter and sees how many 
-- Students are taking that Course that matches the CourseId

CREATE FUNCTION ClassCensus(@CourseId[int])
	RETURNS[int]
	AS
		BEGIN
			DECLARE @Attendance [int]
			SELECT @Attendance = Count(CourseId)
			FROM CourseRoster
			WHERE CourseId = @CourseId;
			RETURN @Attendance
		END;

-- Me testing the Function I made
SELECT dbo.ClassCensus(7);


--This View is based on a selct statement that utilizes the function of seeing how many students are taking a class
--This View shows The CourseId, the CourseName, The Students currently taking the Course, and the Remaning Space left
CREATE VIEW ViewCoursePop
AS 
SELECT  CourseId, CourseName, dbo.ClassCensus(CourseId) StudentsInCourse, 20 - dbo.ClassCensus(CourseId) RemainingSpace
FROM Courses
WHERE (20 > (dbo.ClassCensus(CourseId)));

SELECT * FROM ViewCoursePop;

--Insert Student Procedure
CREATE PROCEDURE [AddNewStudentDetails]
(@fName varchar(40), @lName varchar(40), @Email varchar(40), @Pword varchar(40)) 
AS
BEGIN
	INSERT INTO [Student] VALUES(@fName, @lName, @Email, @Pword)
END 

--To View Students
CREATE PROCEDURE [GetStudents]
as 
BEGIN
	SELECT * FROM [Student]
END

--To Update Students
CREATE PROCEDURE [UpdateStudentDetails]
	(@StudentId int, @fName varchar(40), @lName varchar(40), @Email varchar(40), @Pword varchar(40))
	AS
	BEGIN
		UPDATE Student
		set fName = @fName,
		lName = @lName,
		Email = @Email,
		Pword = @Pword
		WHERE StudentId = @StudentId
END

--To Delete Students
CREATE PROCEDURE [DeleteStudentById](
	@StudentId int 
)
AS
BEGIN
	DELETE FROM Student WHERE StudentId = @StudentId
END

--To match student Login
CREATE PROCEDURE [StudentLogin](
	@Email varchar(40), @Pword varchar(40))
	AS
	BEGIN
		SELECT * FROM [Student] WHERE Email = @Email AND [Pword] = @Pword
		END
Select * FROM Student;

EXEC dbo.StudentLogin @Email = 'Msbestgirl@gmail.com', @Pword = 'longesthair';

Select Distinct StudentId, CourseName FROM CourseRoster INNER JOIN Courses ON CourseRoster.StudentId = CourseRoster.CourseId;
Select * from Student;
Select * from CourseRoster;

Create PROCEDURE[Schedule](
	@StudentId int)
AS
BEGIN
Select * FROM Courses 
INNER JOIN CourseRoster 
ON ([StudentID] = @StudentId 
AND CourseRoster.CourseId = Courses.CourseId)
END

Exec dbo.Schedule @StudentId = 5;

CREATE PROCEDURE [AddCourse](@StudentId int, @CourseId int)
AS 
BEGIN
INSERT INTO [CourseRoster]([StudentId], [CourseId])
VALUES( @StudentId, @CourseId);
END

CREATE PROCEDURE [AllCourses]
AS
BEGIN
SELECT * FROM Courses
END

EXEC dbo.AllCourses;
Select * From Student;