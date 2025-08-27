use maximatech;

CREATE TABLE departamentos (
    ID int AUTO_INCREMENT, 
    Nome varchar(50) not null,
    PRIMARY KEY (ID)
);

insert into departamentos (Nome) Values ('Eletrônicos'), ('Roupas'), ('Alimentos'), ('Móveis')