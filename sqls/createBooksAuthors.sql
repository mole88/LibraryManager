CREATE TABLE authors (
    id SERIAL PRIMARY KEY,
    fullname VARCHAR(255) NOT NULL
);

CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    year INT NOT NULL,
    author_id INT,
	is_available BOOLEAN DEFAULT TRUE,
	
    CONSTRAINT fk_author
        FOREIGN KEY(author_id) 
        REFERENCES authors(id)
        ON DELETE SET NULL 
);