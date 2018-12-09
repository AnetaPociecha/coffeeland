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
	blend			varchar(30) not null,
	description		varchar(100) not null
);



create table clients (
	clientId		integer primary key,
	email			varchar(30) not null,
	firstName		varchar(30) not null,
	lastName		varchar(30) not null,
	password		varchar(100) not null,
	newsletter		bit not null
);

create table addresses (
	addressId		integer primary key,
	clientId		integer not null references clients,
	country			varchar(30) not null,
	city			varchar(30) not null,
	street			varchar(30) not null,
	ZIPCode			varchar(30) not null,
	buildingNumber	integer not null,
	apartmentNumber	integer,
	isDefault 		bit default 0
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
	orderId 	integer not null references orders,
	productId	integer not null references products,
	amount		integer not null
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

commit;