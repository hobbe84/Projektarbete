USE MinushogskolanDb;
GO
IF NOT EXISTS (SELECT ID FROM Minushogskolan.Student) AND @@ROWCOUNT = 0
BEGIN
	-- Student Junk data
	BEGIN 
	INSERT INTO Minushogskolan.Student([FirstName],[LastName],[BirthDate],[Adress],[City]) VALUES('Evan','Cantu','03-07-93','923-2748 Sed Ave','Avellino'),('Lillian','Dillon','01-10-80','233-1489 Diam. St.','Newark'),('Hasad','Gregory','05-19-85','147-4980 Et, Ave','Ahmadpur East'),('Rama','Carver','06-26-80','5651 Ligula Rd.','Clearwater Municipal District'),('Carter','Booker','09-03-87','Ap #683-7813 Tristique Street','Dro');
	INSERT INTO Minushogskolan.Student([FirstName],[LastName],[BirthDate],[Adress],[City]) VALUES('Xantha','Hawkins','10-17-90','P.O. Box 690, 6516 Neque. Street','Burdinne'),('Anthony','Marshall','03-18-77','P.O. Box 474, 7240 Velit Av.','Sioux City'),('Duncan','Padilla','03-23-72','459-309 Ultricies Rd.','Fairbanks'),('Clark','Logan','05-21-72','137-4835 Ante Rd.','Paglieta'),('Dean','Alvarez','05-23-76','521-1861 Vestibulum Rd.','Aalen');
	INSERT INTO Minushogskolan.Student([FirstName],[LastName],[BirthDate],[Adress],[City]) VALUES('Jescie','Mcdaniel','08-02-75','P.O. Box 624, 6262 Magnis St.','Canterbury'),('Dieter','Sargent','09-02-92','Ap #697-9633 Laoreet St.','Rutland'),('Jaquelyn','Potts','08-05-84','P.O. Box 429, 7617 Lorem, Rd.','Subiaco'),('Colby','Dennis','07-02-81','Ap #606-2396 Sagittis. St.','Mattersburg'),('Alvin','Simpson','01-26-84','888-6171 Dignissim St.','Palayankottai');
	INSERT INTO Minushogskolan.Student([FirstName],[LastName],[BirthDate],[Adress],[City]) VALUES('Risa','Rich','07-23-83','9186 Magna Av.','Narbolia'),('Michael','Cruz','07-06-85','2236 Ac Avenue','Ramillies'),('Scarlet','Figueroa','11-19-90','P.O. Box 882, 4444 Justo Av.','Ereğli'),('Abigail','Odom','05-23-73','Ap #282-497 In Street','Bonnert'),('Judith','Pena','02-22-73','P.O. Box 731, 5818 Consectetuer Rd.','Meise');
	END

	--Teacher Junk data
	BEGIN
	INSERT INTO Minushogskolan.Teacher([FirstName],[LastName],[BirthDate],[Adress],[City]) VALUES('Lillith','Reyes','05-05-74','P.O. Box 199, 1277 Nec, Rd.','Feltre'),('Riley','Norman','05-24-80','Ap #942-1130 Morbi St.','Fort Smith'),('Mia','White','11-25-71','Ap #662-343 Augue Road','Vorst'),('Adara','Hunt','04-10-81','P.O. Box 492, 1738 Enim. Ave','Hondelange'),('Ciara','Mcfarland','12-10-95','P.O. Box 387, 5027 Lacus. Av.','Timkur');
	INSERT INTO Minushogskolan.Teacher([FirstName],[LastName],[BirthDate],[Adress],[City]) VALUES('Shelley','Torres','07-02-90','P.O. Box 383, 5001 Ligula Rd.','Haridwar'),('Garrett','Larson','05-01-89','805-784 Sed St.','Roio del Sangro'),('Craig','Dillon','06-20-90','179-4788 Tempor St.','Namur'),('Rogan','Rosario','02-25-87','P.O. Box 355, 7960 Non, Av.','Franeker'),('Colby','Rich','02-01-79','P.O. Box 740, 722 Sed Avenue','Rimbey');
	END

	-- Grades Junk data
	BEGIN
	INSERT INTO Minushogskolan.Grades([Name]) VALUES('A'),('B'),('C'),('D'),('E'),('F')
	END

	-- Courses Junk data
	BEGIN 
	INSERT INTO Minushogskolan.Course([Name],[Points],[Info]) VALUES('How not to Cook',600,'Lorem ipsum dolor'),('What to do during easters',100,'Lorem ipsum'),('In my Mind and not Your Mind',200,'Lorem ipsum dolor sit amet,'),('Computing - how to BE a NERD',355,'Lorem ipsum dolor'),('Build your car - How not to',600,'Lorem ipsum');
	INSERT INTO Minushogskolan.Course([Name],[Points],[Info]) VALUES('The style that was Yesterday',350,'Lorem ipsum dolor'),('How to wear your hats',260,'Lorem ipsum'),('How not to Build a House',400,'Lorem ipsum dolor'),('IKEA get rich building furnitures - How not to',750,'Lorem ipsum dolor sit'),('Organizing your Socks',400,'Lorem ipsum dolor sit amet,');
	END

	-- ActiveCourse Junk data
	BEGIN 
	INSERT INTO Minushogskolan.ActiveCourse([CourseID],[TeacherID],[StartDate],[EndDate]) VALUES((select ID from Minushogskolan.Course where Name = 'How not to Cook'),(select ID from Minushogskolan.Teacher where id = 1),'09-01-14','02-13-15'),((select ID from Minushogskolan.Course where Name = 'What to do during easters'),(select ID from Minushogskolan.Teacher where id = 2),'10-25-15','04-09-16'),((select ID from Minushogskolan.Course where Name = 'IKEA get rich building furnitures - How not to'),(select ID from Minushogskolan.Teacher where id = 3),'11-24-14','01-18-17'),((select ID from Minushogskolan.Course where Name = 'Computing - how to BE a NERD'),(select ID from Minushogskolan.Teacher where id = 4),'10-09-14','11-04-16'),((select ID from Minushogskolan.Course where Name = 'Organizing your Socks'),(select ID from Minushogskolan.Teacher where id = 5),'08-01-14','05-14-15');
	END

	-- CourseStudent Junk data
	BEGIN
	INSERT INTO Minushogskolan.CourseStudent([ActiveCourseID],[StudentID]) VALUES(1,1),(2,2),(3,3),(4,4),(5,5);
	INSERT INTO Minushogskolan.CourseStudent([ActiveCourseID],[StudentID]) VALUES(1,6),(2,7),(3,8),(4,9),(5,10);
	INSERT INTO Minushogskolan.CourseStudent([ActiveCourseID],[StudentID]) VALUES(1,11),(2,12),(3,13),(4,14),(5,15);
	INSERT INTO Minushogskolan.CourseStudent([ActiveCourseID],[StudentID]) VALUES(1,16),(2,17),(3,18),(4,19),(5,20);	
	END
END