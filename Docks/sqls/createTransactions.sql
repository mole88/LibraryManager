CREATE TABLE transactions(
	id SERIAL PRIMARY KEY,
	visitor_id INT,
	book_id INT,
	date_taken DATE NOT NULL,
	due_date DATE,
	date_return DATE,
	
	CONSTRAINT fk_visitor
        FOREIGN KEY(visitor_id) 
        REFERENCES visitors(id)
        ON DELETE CASCADE, --если удалена запись о посетителе, все связанные транзакции удалятся
		
	CONSTRAINT fk_book
        FOREIGN KEY(book_id) 
        REFERENCES books(id)
        ON DELETE CASCADE --если удалена запись о книге, все связанные транзакции удалятся
);