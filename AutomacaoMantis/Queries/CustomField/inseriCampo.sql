INSERT INTO mantis_custom_field_table (name)
VALUES ('$customFieldName');

SELECT *
FROM mantis_custom_field_table
WHERE ID = (SELECT MAX(id) FROM mantis_custom_field_table);