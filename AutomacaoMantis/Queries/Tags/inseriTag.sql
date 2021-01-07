INSERT INTO mantis_tag_table (user_id, name, description)
VALUES (1, '$tagName', '$tagDescription');

SELECT id, name tagName, description tagDescription
FROM mantis_tag_table
WHERE ID = (SELECT MAX(id) FROM mantis_tag_table);