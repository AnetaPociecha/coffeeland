drop table if exists products;
drop table if exists workers;

create table workers (
	workerId		integer primary key,
	role			enum('a','b','c') not null,
	email 			varchar(30) not null,
	password		varchar(100) not null
);

create table products (
	productId		integer primary key,
	name        	varchar(30) not null,
	price	    	integer	not null,
	imagePath		varchar(50) not null,
	productType		varchar(30) not null,
	description		varchar(100) not null
);


insert into workers(workerId,role,email,password)
values(0,'a','worker1@gmail.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9');

insert into products(productId,name,price,imagePath,productType,description) 
values(0,'Lavazza',1500,'/Client/dist/fb19c3e75271bc0544c1b121f7a28ce7.jpg','100% arabica','Good');

insert into products(productId,name,price,imagePath,productType,description) 
values(1,'Vergnano',2500,'/Client/dist/fb19c3e75271bc0544c1b121f7a28ce7.jpg','100% robusta','Strong');

insert into products(productId,name,price,imagePath,productType,description) 
values(2,'Vergnano Premium',1000,'/Client/dist/fb19c3e75271bc0544c1b121f7a28ce7.jpg','100% arabica','Strong and good');

insert into products(productId,name,price,imagePath,productType,description) 
values(3,'Jacobs',500,'/Client/dist/fb19c3e75271bc0544c1b121f7a28ce7.jpg','100% robusta','Okay');
commit;
