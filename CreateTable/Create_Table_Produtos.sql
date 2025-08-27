use maximatech;
	
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