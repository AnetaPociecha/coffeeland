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
	description		varchar(10000) not null
);


insert into workers(workerId,role,email,password)
values(0,'a','worker1@gmail.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9');

insert into products(productId,name,price,imagePath,productType,description) 
values(0,'Lavazza',1500,'./img.jpg','100% arabica','Heart holds a special place in my own coffee nerd evolution: I made my first great cup of coffee using its beans. And the roaster serves as a great entry point into the specialty coffee world. The Portland-based roaster churns out some extremely fruity beans, making it easy to pick up on their tasting notes. You’ll feel like the owner of a real advanced palette while saying things like, I really taste the mango on this one, while wisely rubbing your chin. It’s also widely available in third wave coffee shops—it’s getting Big to the point where I wake up in cold sweats after dreaming the roaster’s gone the way of Stumptown and Blue Bottle and gotten itself acquired by a corporation like Peets or Nestle. And in our GQ cupping, Heart was far and away the crowd favorite.');

insert into products(productId,name,price,imagePath,productType,description) 
values(1,'Vergnano',2500,'./img.jpg','100% robusta','Heart holds a special place in my own coffee nerd evolution: I made my first great cup of coffee using its beans. And the roaster serves as a great entry point into the specialty coffee world. The Portland-based roaster churns out some extremely fruity beans, making it easy to pick up on their tasting notes. You’ll feel like the owner of a real advanced palette while saying things like, I really taste the mango on this one, while wisely rubbing your chin. It’s also widely available in third wave coffee shops—it’s getting Big to the point where I wake up in cold sweats after dreaming the roaster’s gone the way of Stumptown and Blue Bottle and gotten itself acquired by a corporation like Peets or Nestle. And in our GQ cupping, Heart was far and away the crowd favorite.');

insert into products(productId,name,price,imagePath,productType,description) 
values(2,'Vergnano Premium',1000,'./img.jpg','100% arabica','Heart holds a special place in my own coffee nerd evolution: I made my first great cup of coffee using its beans. And the roaster serves as a great entry point into the specialty coffee world. The Portland-based roaster churns out some extremely fruity beans, making it easy to pick up on their tasting notes. You’ll feel like the owner of a real advanced palette while saying things like, I really taste the mango on this one, while wisely rubbing your chin. It’s also widely available in third wave coffee shops—it’s getting Big to the point where I wake up in cold sweats after dreaming the roaster’s gone the way of Stumptown and Blue Bottle and gotten itself acquired by a corporation like Peets or Nestle. And in our GQ cupping, Heart was far and away the crowd favorite.');

insert into products(productId,name,price,imagePath,productType,description) 
values(3,'Jacobs',500,'./img.jpg','100% robusta','Heart holds a special place in my own coffee nerd evolution: I made my first great cup of coffee using its beans. And the roaster serves as a great entry point into the specialty coffee world. The Portland-based roaster churns out some extremely fruity beans, making it easy to pick up on their tasting notes. You’ll feel like the owner of a real advanced palette while saying things like, I really taste the mango on this one, while wisely rubbing your chin. It’s also widely available in third wave coffee shops—it’s getting Big to the point where I wake up in cold sweats after dreaming the roaster’s gone the way of Stumptown and Blue Bottle and gotten itself acquired by a corporation like Peets or Nestle. And in our GQ cupping, Heart was far and away the crowd favorite.');
commit;
