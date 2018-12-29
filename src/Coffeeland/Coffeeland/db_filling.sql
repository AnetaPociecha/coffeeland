drop table if exists products;
drop table if exists products;
drop table if exists clients;
drop table if exists addresses;
drop table if exists order_entries;
drop table if exists workers;
drop table if exists orders;
drop table if exists complaints;
drop table if exists payments;

begin;

create table products (
	productId		integer primary key,
	name        	varchar(30) not null,
	price	    	integer	not null,
	imagePath		varchar(50) not null,
	productType		varchar(30) not null,
	description		varchar(100) not null
);



create table clients (
	clientId		integer primary key,
	email			varchar(30) not null,
	firstName		varchar(30) not null,
	lastName		varchar(30) not null,
	password		varchar(100) not null,
	newsletterEmail	varchar(30) not null
);

create table addresses (
	addressId		integer primary key,
	clientId		integer not null references clients,
	country			varchar(30) not null,
	city			varchar(30) not null,
	street			varchar(30) not null,
	ZIPCode			integer not null,
	buildingNumber	integer not null,
	apartmentNumber	varchar(30) not null,
	isActive 		bit default 1
);

create table workers (
	workerId		integer primary key,
	role			enum('a','b','c') not null,
	email 			varchar(30) not null,
	password		varchar(100) not null
);

create table orders (
	orderId			integer primary key,
	clientId		integer not null references clients,
	workerId		integer not null references workers,
	addressId		integer not null references addresses,
	status			integer not null,
	openDate		date not null,
	closeDate		date
);

create table order_entries (
	orderEntryId	integer primary key,
	orderId 		integer not null references orders,
	productId		integer not null references products,
	amount			integer not null
);

create table complaints (
	orderId   		integer not null references orders,
	workerId 		integer not null references workers,
	description		varchar(100) not null,
	openDate		date not null,
	isClosed		bit not null
);

create table payments (
	paymentId		integer primary key,
	orderId			integer not null references orders,
	amount			integer not null,
	openDate		date not null
);

insert into products(productId,name,price,imagePath,productType,description) 
values(0,'Lavazza',15,'./img.jpg','100% arabica','Good');

insert into products(productId,name,price,imagePath,productType,description) 
values(1,'Vergnano',25,'./img.jpg','100% robusta','Strong');

insert into workers(workerId,role,email,password)
values(0,'a','worker1@gmail.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9');

insert into workers(workerId,role,email,password)
values(1,'b','worker2@gmail.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9');

insert into clients(clientId,email,firstName,lastName,password,newsletterEmail)
values(0,'marek@gmail.com','Marek','Ochocki','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','marek@gmail.com');

insert into addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber,isActive) 
values(0,0,'Poland','Gdynia','Rzemieslnicza',30445,12,'1a',1);

insert into addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber,isActive) 
values(1,0,'Poland','Warsaw','Grodzka',25487,23,'1',1);

insert into orders(orderId,clientId,workerId,addressId,status,openDate) 
values(0,0,0,1,0,DATE '2018-05-12');

insert into order_entries(orderEntryId,orderId,productId,amount)
values(0,0,0,5);

insert into order_entries(orderEntryId,orderId,productId,amount)
values(1,0,1,2);

insert into payments(paymentId,orderId,amount,openDate)
values(0,0,125, DATE '2018-05-12;');

insert into orders(orderId,clientId,workerId,addressId,status,openDate) 
values(1,0,1,0,0,DATE '2018-10-18');

insert into order_entries(orderEntryId,orderId,productId,amount)
values(2,1,1,1);

insert into complaints(orderId,workerId,description,openDate,isClosed)
values(0,1,'I am dissatisfied',DATE '2018-10-22',0);

commit;