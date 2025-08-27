use maximatech;

CREATE TABLE usuarios (
  id INT PRIMARY KEY AUTO_INCREMENT,
  username VARCHAR(255) NOT NULL UNIQUE,
  encyptPassword VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL UNIQUE,
  nome_completo VARCHAR(255)
);