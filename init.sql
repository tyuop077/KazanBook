DROP TABLE Books
DROP TABLE Authors

CREATE TABLE Books (
    _id INT NOT NULL IDENTITY(1,1),
    id UNIQUEIDENTIFIER NOT NULL UNIQUE,
    title VARCHAR(40),
    authorid VARCHAR(255),
    tags VARCHAR(255),
    PRIMARY KEY (_id)
)
CREATE TABLE Authors (
    _id INT NOT NULL IDENTITY(1,1),
    id UNIQUEIDENTIFIER NOT NULL UNIQUE,
    name VARCHAR(128),
    slogan VARCHAR(255),
    PRIMARY KEY (_id)
)

-- Example book, that would be shown on each project launch
INSERT INTO Books(id,title,authorid,tags)
VALUES ('45E02F55-4C3D-4E5D-8548-FCB6EBD7B40A', 'Example book', NULL, 'VERY;COOL;TAGS')

INSERT INTO Authors(id,name,slogan)
VALUES ('8E99AFA1-AB9C-4E55-AFF2-48D5FA5588EB', 'Someone', 'Hello World!')

INSERT INTO Books(id,title,authorid,tags)
VALUES (NEWID(), 'ITIS & Math = Stress', '8E99AFA1-AB9C-4E55-AFF2-48D5FA5588EB', 'ITIS;University;Kazan')

--SELECT id,title,authorid,tags FROM Books WHERE id = '0642F8A1-1248-445C-BEF2-09F16B5140D2'
--SELECT id,title,authorid,tags FROM Books
--SELECT * FROM Books
--SELECT id,name,slogan FROM Authors
--SELECT * FROM Authors