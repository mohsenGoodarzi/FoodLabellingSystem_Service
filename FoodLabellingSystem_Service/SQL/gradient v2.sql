use master;

go

CREATE database FoodDatabase;

go 

use FoodDatabase;

go

create table AppUser(
firstName nvarchar(100) not null default 'UN',
lastName nvarchar(100) not null default 'UN', 
companyName nvarchar(100) not null default 'NC',
userName char (100) not null default 'UN',
password nvarchar(128) not null,
email nvarchar (100) not null,
phone nvarchar (20) not null default '000',
status nvarchar(30) not null default 'Registered',
role nvarchar(30) not null,
emailConfirmed bit not null default 0,
phoneConfirmed bit not null default 0,
constraint PK_User_UserName primary key (userName),
constraint UK_User_Email unique (email),
constraint UK_User_Phone unique (phone),
constraint CK_User_Status check (status in('Suspended', 'Registered', 'Activated', 'Closed', 'Await_Email_Confirmation', 'Await_Phone_Confimation')),
constraint CK_User_Role check (role in('Administrator', 'Guest', 'Client', 'Manager', 'None'))
)


go
create table Warning(warningId varchar (50) not null default 'Not Specified',
					 message Text,
					 warningType varchar(50) not null  default 'Not Specified',
					 constraint PK_Warning_warningId primary key (warningId),
					 constraint CHK_Warning_warningId check (warningType in ('Allergic','Children Attention','Not Specified'))
					);
go

insert into Warning(warningId,warningType,message)values('Not Specified','Not Specified','No message');

go

create table CuisineType (CuisineTypeId varchar (50) not null,
							member varchar (50) not null,
							constraint PK_CuisineType_generalGroupId primary key (CuisineTypeId),
							constraint FK_CuisineType_Group_member foreign key (member) references CuisineType( CuisineTypeId)
							);

go

insert into CuisineType(CuisineTypeId,member) values ('Not Specified','Not Specified');

go


create table DishType (DishTypeId varchar (50) not null,
							member varchar (50) not null,
							constraint PK_DishType_DishTypeId primary key (DishTypeId),
							constraint FK_DishType_DishTypeId foreign key (member) references DishType( DishTypeId)
							);

go

insert into DishType(DishTypeId,member) values ('Not Specified','Not Specified');

go

create table Unit(unitId varchar(50) not null,
				  toGram float not null default 0.0,
				  constraint PK_Unit_unitId primary key (unitId),
				  );
				  
go

create table IngredientType (IngredientTypeId varchar (50) not null,
							member varchar (50) not null,
							constraint PK_IngredientType_IngredientTypeId primary key (IngredientTypeId),
							constraint FK_IngredientType_member foreign key (member) references IngredientType(IngredientTypeId)
							);

go

insert into IngredientType(IngredientTypeId,member) values ('Not Specified','Not Specified');
go
-- required data
insert into Unit(unitId,toGram) values ('Not Specified',0.0),
											('gram',1.0),
											('cup',128.0),
											('ounce',28.3495),
											('mililitr',1.0),
											('pint',568.26125),
											('tea spoon',4.2),
											('table spoon',21.25);

go

create table Ingredient (ingredientId varchar(50) not null, 
						description nvarchar (100) not null default 'gradience description',
						IngredientTypeId varchar (50) not null default 'Not Specified',
						unitId varchar (50) not null default 'Not Specified', 
						amount float not null default -1.0,
						fat  float not null default -1.0,
						carbs float not null default -1.0,
						protein float not null default -1.0,
						calory float not null default -1.0,
						warningId varchar (50) not null default 'Not Specified',
						constraint PK_Ingredient_ingredientId primary key (ingredientId),
						constraint FK_Ingredient_warningId foreign key (warningId) references Warning(warningId) on delete set default on update cascade,
						constraint FK_Ingredient_unitId foreign key (unitId) references Unit(unitId) on delete set default on update cascade,
						constraint FK_Ingredient_IngredientTypeId foreign key (IngredientTypeId) references IngredientType(IngredientTypeId) on delete set default on update cascade
						);

go

insert into Ingredient(ingredientId,IngredientTypeId,unitId,amount,fat,carbs,protein,calory)values('Not Specified','Not Specified','gram',0.0,0.0,0.0,0.0,0.0);

go

create table Food( foodId varchar (100) not null default 'Not Specified',
				  description text,
				  dishType varchar(50) not null default 'Not Specified',
				  cuisineType varchar(50) not null default 'Not Specified',
				  foodType varchar(50) not null default 'Not Specified',
				  userName char(100) not null default 'UN',
				  constraint PK_Food_foodId primary key (foodId),
				  constraint FK_Food_dishType foreign key (dishType) references DishType(DishTypeId) on delete set default on update cascade,
				  constraint FK_Food_cuisineType foreign key (cuisineType) references CuisineType(CuisineTypeId) on delete set default on update cascade,
				  constraint CHK_Food_foodType check (foodType in ('vegetarian','carnivore','Not Specified')),
				  constraint FK_Food_FirstName_LastName_CompanyName foreign key(userName) references AppUser(userName)on delete set default on update cascade
				  );

go


CREATE FUNCTION getFat 
(
	-- Add the parameters for the function here
	@ingredientId varchar(50), @foodAmount float , @foodUnit varchar(50)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @ingredientAmount float , @ingredientUnit varchar(50), @toGram float, @fat float ,@Result float

	select @toGram = toGram, @ingredientUnit = Ingredient.unitId, @fat = fat, @ingredientAmount = amount from Unit, Ingredient where Ingredient.ingredientId = @ingredientId and Ingredient.unitId = Unit.unitId
	select @ingredientAmount = @toGram * @ingredientAmount
	
	if @foodUnit <> 'G'
		begin
			select @toGram = toGram from Unit where @foodUnit = unitId
			select @foodAmount = @foodAmount * @toGram
		end
		
	SELECT @Result = (@foodAmount/@ingredientAmount)* @fat;

	RETURN @Result

END
GO



CREATE FUNCTION getCarbs 
(
	-- Add the parameters for the function here
	@ingredientId varchar(50), @foodAmount float , @foodUnit varchar(50)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @ingredientAmount float , @ingredientUnit varchar(50), @toGram float, @carbs float ,@Result float

	select @toGram = toGram, @ingredientUnit = Ingredient.unitId, @carbs = carbs, @ingredientAmount = amount from Unit, Ingredient where Ingredient.ingredientId = @ingredientId and Ingredient.unitId = Unit.unitId
	select @ingredientAmount = @toGram * @ingredientAmount
	
	if @foodUnit <> 'G'
		begin
			select @toGram = toGram from Unit where @foodUnit = unitId
			select @foodAmount = @foodAmount * @toGram
		end
		
	SELECT @Result = (@foodAmount/@ingredientAmount)* @carbs;

	RETURN @Result

END
GO



CREATE FUNCTION getProtein 
(
	-- Add the parameters for the function here
	@ingredientId varchar(50), @foodAmount float , @foodUnit varchar(50)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @ingredientAmount float , @ingredientUnit varchar(50), @toGram float, @protein float ,@Result float

	select @toGram = toGram, @ingredientUnit = Ingredient.unitId, @protein = protein, @ingredientAmount = amount from Unit, Ingredient where Ingredient.ingredientId = @ingredientId and Ingredient.unitId = Unit.unitId
	select @ingredientAmount = @toGram * @ingredientAmount
	
	if @foodUnit <> 'G'
		begin
			select @toGram = toGram from Unit where @foodUnit = unitId
			select @foodAmount = @foodAmount * @toGram
		end
		
	SELECT @Result = (@foodAmount/@ingredientAmount)* @protein;

	RETURN @Result

END
GO

CREATE FUNCTION getCalory
(
	-- Add the parameters for the function here
	@ingredientId varchar(50), @foodAmount float , @foodUnit varchar(50)
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @ingredientAmount float , @ingredientUnit varchar(50), @toGram float, @calory float ,@Result float

	select @toGram = toGram, @ingredientUnit = Ingredient.unitId, @calory = calory, @ingredientAmount = amount from Unit, Ingredient where Ingredient.ingredientId = @ingredientId and Ingredient.unitId = Unit.unitId
	select @ingredientAmount = @toGram * @ingredientAmount
	
	if @foodUnit <> 'G'
		begin
			select @toGram = toGram from Unit where @foodUnit = unitId
			select @foodAmount = @foodAmount * @toGram
		end
		
	SELECT @Result = (@foodAmount/@ingredientAmount)* @calory;

	RETURN @Result

END
GO

create table Food_Ingredient(
							 foodId varchar(100) not null default 'Not Specified',
							 ingredientId varchar(50) not null default 'Not Specified',
							 unitId varchar (50) not null default 'Not Specified',
							 amount float not null default 0.0,
							 fat  as dbo.getFat(ingredientId,amount,unitId),
							 carbs as dbo.getCarbs(ingredientId,amount,unitId),
							 protein as dbo.getProtein(ingredientId,amount,unitId),
							 calory as  dbo.getCalory(ingredientId,amount,unitId),
							 constraint PK_Food_Ingredient_id primary key (foodId,ingredientId),
							 constraint FK_Food_Ingredient_foodId foreign key (foodId) references Food(foodId) on delete set default on update cascade ,
							 constraint FK_Food_Ingredient_unitId foreign key (unitId) references Unit(unitId) on delete set default on update cascade,
							 constraint FK_Food_Ingredient_ingredientId foreign key (ingredientId) references Ingredient(ingredientId) on delete set default
							 );

go  

--alter table dbo.Unit add toGram float  not null default 0.0;;

--alter table Ingredient add groupId varchar(20) not null default 'NS';

--alter table Ingredient add constraint FK_Ingredient_groupId foreign key (groupId) references generalGroup(groupId);




select * from IngredientType

go
-- select root group
select * from IngredientType where IngredientTypeId = member

go
-- select a group that has parent, they may or may not have children
select * from IngredientType where IngredientTypeId <> member

go


go 
 
go

-- select root items that  no children
select * from IngredientType where  IngredientTypeId  not in (select member from IngredientType where IngredientTypeId<>member) and IngredientTypeId = member


go

-- select items that has  parent and no children and no root items
select * from IngredientType where  IngredientTypeId  not in (select member from IngredientType) and IngredientTypeId <> member

go

-- select root items that have children
select * from IngredientType where  IngredientTypeId  in (select member from IngredientType where IngredientTypeId<>member) and IngredientTypeId = member

go

-- select items that have children and parent (no root items)
select * from IngredientType where  IngredientTypeId  in (select member from IngredientType where IngredientTypeId<>member) and IngredientTypeId <> member







-- select items that do not have children

select * from IngredientType where IngredientTypeId not in (select member from IngredientType where IngredientTypeId <> member) and member = 'Meat'

select member from IngredientType where IngredientTypeId <> member