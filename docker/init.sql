CREATE TABLE departamentos (
    ID int AUTO_INCREMENT, 
    Nome varchar(50) not null,
    PRIMARY KEY (ID)
);
insert into departamentos (Nome) Values ('Eletrônicos'), ('Roupas'), ('Alimentos'), ('Móveis');
CREATE TABLE produtos (
  id CHAR(36) NOT NULL DEFAULT (UUID()),
  Nome varchar(40) not null UNIQUE,
  descricao varchar(150)  not null,
  departamentoId int  not null, 
  preco DECIMAL(10, 2)  not null,
  status boolean ,
  PRIMARY KEY (id),
  FOREIGN KEY (departamentoId) REFERENCES departamentos (id)
);
CREATE TABLE usuarios (
  id INT PRIMARY KEY AUTO_INCREMENT,
  username VARCHAR(255) NOT NULL UNIQUE,
  encyptPassword VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL UNIQUE,
  nomeCompleto VARCHAR(255)
);
insert into usuarios (username,encyptPassword,email,nomeCompleto) Values ('eduardo123','PERTfaiTOedXPyDzSIThCg==:7s4gVKWA45+7wPX67ktUAwuKERQZzdQ5zwtaCswTf/c=','eduard@eduardo','Eduardo Mariano Tonaco')
	
			