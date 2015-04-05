USE MASTER
GO

IF NOT EXISTS(SELECT * FROM sys.databases
				WHERE name = 'MinushogskolanDb')

CREATE DATABASE MinushogskolanDb;
GO
USE MinushogskolanDb;
GO
CREATE SCHEMA Minushogskolan
GO

IF NOT EXISTS(SELECT * FROM sys.objects
				WHERE name = 'Minushogskolan.Student')

CREATE TABLE Minushogskolan.Student
(
	ID int not null IDENTITY(1,1),
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	BirthDate datetime not null,
	Adress varchar(50) null,
	City varchar(50) null,
	ActiveStudent bit not null DEFAULT 1,

	CONSTRAINT PK_Student_ID PRIMARY KEY (ID),

);


IF NOT EXISTS(SELECT * FROM sys.objects
				WHERE name = 'Minushogskolan.Teacher')

CREATE TABLE Minushogskolan.Teacher
(
	ID int not null IDENTITY(1,1),
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	BirthDate datetime not null,
	Adress varchar(50) null,
	City varchar(50) null,
	ActiveTeacher bit not null DEFAULT 1,

	CONSTRAINT PK_Teacher_ID PRIMARY KEY (ID)

);

IF NOT EXISTS(SELECT * FROM sys.objects
				WHERE name = 'Minushogskolan.Course')

CREATE TABLE Minushogskolan.Course
(
	ID int not null IDENTITY(1,1),
	Points int not null,
	Name varchar(50) not null,
	Info varchar(100) null,
	ActiveCourse bit not null DEFAULT 1,

	CONSTRAINT PK_Course_ID PRIMARY KEY (ID)
);

IF NOT EXISTS(SELECT * FROM sys.objects
				WHERE name = 'Minushogskolan.Grades')

CREATE TABLE Minushogskolan.Grades
(
	ID int not null IDENTITY(1,1),
	Name varchar(50) not null,

	CONSTRAINT PK_Grades_ID PRIMARY KEY (ID)
);

IF NOT EXISTS(SELECT * FROM sys.objects
				WHERE name = 'Minushogskolan.ActiveCourse')

CREATE TABLE Minushogskolan.ActiveCourse
(
	ID int not null IDENTITY(1,1),
	CourseID int not null,
	TeacherID int not null,
	StartDate datetime not null,
	EndDate datetime not null,
	CourseEnded bit not null DEFAULT 0,

	CONSTRAINT PK_ActiveCourse_ID PRIMARY KEY (ID),
	CONSTRAINT FK_ActiveCourse_Course_ID
			FOREIGN KEY (CourseID) REFERENCES Minushogskolan.Course(ID),
	CONSTRAINT FK_ActiveCourse_Teacher_ID
				FOREIGN KEY (TeacherID) REFERENCES Minushogskolan.Teacher(ID)
);

IF NOT EXISTS(SELECT * FROM sys.objects
				WHERE name = 'Minushogskolan.CourseStudent')

CREATE TABLE Minushogskolan.CourseStudent
(
	ID int not null IDENTITY(1,1),
	ActiveCourseID int not null,
	StudentID int not null,
	GradeID int null,

	CONSTRAINT PK_CourseStudent_ID PRIMARY KEY (ID),
	CONSTRAINT FK_CourseStudent_ActiveCourse_ID
				FOREIGN KEY (ActiveCourseID) REFERENCES Minushogskolan.ActiveCourse(ID),
	CONSTRAINT FK_CourseStudent_Student_ID
				FOREIGN KEY (StudentID) REFERENCES Minushogskolan.Student(ID),
	CONSTRAINT FK_CourseStudent_Grades_ID
				FOREIGN KEY (GradeID) REFERENCES Minushogskolan.Grades(ID)
);